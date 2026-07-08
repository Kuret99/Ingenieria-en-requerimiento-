# Manual del instalador — Parte 2: crear la base automáticamente

Objetivo: que al instalar la app, el instalador **cree la base `Ing.Software` con sus
tablas y datos** en la instancia de SQL, sin que el usuario abra SSMS.

---

## ⚠️ Leé esto antes (el punto que más falla)

El custom action del instalador corre con la **cuenta de la instalación** (normalmente
`LocalSystem` o el usuario que ejecuta el setup como administrador). Esa cuenta tiene
que tener permiso en SQL Server para **crear una base** (rol `sysadmin` o `dbcreator`).

En SQL Express local muchas veces `LocalSystem` NO es sysadmin, y ahí el custom action
falla con "login failed" aunque el script esté perfecto.

Por eso te dejo **dos formas** y mi recomendación:

- **A) Custom action en el instalador** (lo que elegiste): funciona, pero depende de que
  la cuenta de instalación tenga permisos en SQL.
- **B) Inicializador en el primer arranque de la app** (alternativa): la app, al abrir,
  chequea si la base existe y si no la crea. Corre como **el usuario**, que casi siempre
  sí tiene permisos sobre su SQL Express local. Es más simple de probar y depurar.

Te explico A completo (abajo) y te dejo B al final por si A te da problemas de permisos.

---

## Paso 1 — El script SQL (idempotente, que se pueda correr sin romper)

Tu `Script v8` ya crea tablas + datos, pero **empieza con `USE [Ing.Software]`**, o sea
asume que la base ya existe. Para el instalador necesitás que **primero cree la base si
no está**. Armá un archivo, por ejemplo `CrearBaseIngSoftware.sql`, con esta estructura:

```sql
-- 1) crear la base solo si no existe
IF DB_ID('Ing.Software') IS NULL
BEGIN
    CREATE DATABASE [Ing.Software];
END
GO

USE [Ing.Software];
GO

-- 2) crear tablas solo si no existen (envolvé cada CREATE TABLE)
IF OBJECT_ID('dbo.Usuarios_43BO', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Usuarios_43BO] ( /* ... tus columnas ... */ );
END
GO
-- ...repetir para Rol_43BO, Patente_43BO, Rol_Patente, Bitacora_43BO, DV_43BO, etc...

-- 3) datos semilla solo si la tabla está vacía (para no duplicar al reinstalar)
IF NOT EXISTS (SELECT 1 FROM [dbo].[Patente_43BO])
BEGIN
    SET IDENTITY_INSERT [dbo].[Patente_43BO] ON;
    INSERT [dbo].[Patente_43BO] ([IdPatente_43BO],[NombrePatente_43BO]) VALUES (1, N'GestionUsuarios_Acceso');
    -- ... el resto, incluidas las nuevas 21/22/23 ...
    SET IDENTITY_INSERT [dbo].[Patente_43BO] OFF;
END
GO
```

Idea clave: **todo con `IF NOT EXISTS` / `IF ... IS NULL`** para que se pueda correr más
de una vez sin explotar. Podés adaptar tu `Script v8` metiéndole estos `IF`.

> Alternativa a este script: si ya tenés un `.bak` con todos los datos, en vez de crear
> con script podés **restaurar el .bak** (`RESTORE DATABASE`). El cableado es igual; solo
> cambia lo que corre el custom action. Decime si preferís esa vía.

Guardá el `.sql` en una carpeta conocida del proyecto (después lo agregamos al Setup).

---

## Paso 2 — Proyecto que ejecuta el script (Installer Class)

Los custom actions de Visual Studio Installer Projects llaman a una **clase Installer**.
Creamos un proyecto chico para eso.

1. En la solución: `Add > New Project > Class Library (.NET Framework)`, nombralo
   `InstaladorBD_43BO` (mismo .NET Framework que la app, 4.8).
2. Agregá una clase `InstaladorBD.cs` con esto:

```csharp
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;

namespace InstaladorBD_43BO
{
    [RunInstaller(true)]
    public class InstaladorBD : Installer
    {
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);

            // estos parametros llegan desde el Setup (CustomActionData, ver Paso 3)
            string instancia = Context.Parameters["instancia"]; // ej: .\SQLEXPRESS
            string carpeta   = Context.Parameters["targetdir"];  // carpeta de instalacion
            if (string.IsNullOrWhiteSpace(instancia)) instancia = @".\SQLEXPRESS";
            carpeta = (carpeta ?? "").Trim();

            string script = Path.Combine(carpeta, "CrearBaseIngSoftware.sql");
            if (!File.Exists(script))
                throw new InstallException("No se encontro el script: " + script);

            // sqlcmd entiende los GO del script (por eso lo usamos en vez de SqlCommand)
            var psi = new ProcessStartInfo
            {
                FileName               = "sqlcmd",
                Arguments              = $"-S \"{instancia}\" -E -b -i \"{script}\"",
                UseShellExecute        = false,
                CreateNoWindow         = true,
                RedirectStandardError  = true,
                RedirectStandardOutput = true
            };

            using (var p = Process.Start(psi))
            {
                string salida = p.StandardOutput.ReadToEnd();
                string error  = p.StandardError.ReadToEnd();
                p.WaitForExit();

                if (p.ExitCode != 0)
                    throw new InstallException("Error creando la base:\n" + error + "\n" + salida);
            }
        }
    }
}
```

- `-E` = conexión confiable (Integrated Security), `-S` = instancia, `-b` = devuelve
  error si el SQL falla, `-i` = archivo de entrada.
- Necesita que **`sqlcmd`** esté en el PATH. Viene con las herramientas de SQL Server;
  si la PC tiene SQL Express + SSMS, suele estar. Si no, se pasa la ruta completa.

---

## Paso 3 — Cablearlo en el Setup

1. **Agregar el script** al instalador: en el editor **File System**, `Application Folder`
   `> Add > File...` y elegí `CrearBaseIngSoftware.sql`. Así queda copiado junto al .exe.
2. **Agregar el ejecutor**: en el Setup, `Add > Project Output...` > proyecto
   `InstaladorBD_43BO` > **Primary Output**. (Copia la .dll del instalador a la carpeta.)
3. **Definir el custom action**: clic derecho en el Setup > `View > Custom Actions`.
   En el nodo **Install**, `Add Custom Action...` > `Application Folder` >
   **Primary Output from InstaladorBD_43BO**.
4. **Pasarle los parámetros**: seleccioná ese custom action, y en **Properties** poné en
   `CustomActionData`:

   ```
   /instancia=".\SQLEXPRESS" /targetdir="[TARGETDIR]\"
   ```

   > Gotcha conocido: `[TARGETDIR]` termina en `\`, y el `\"` puede "comerse" la comilla.
   > Si te da error de ruta, usá `/targetdir="[TARGETDIR] "` (con un espacio antes de la
   > comilla) y en el código ya hacés `.Trim()`. Es el bug clásico de este instalador.

5. Recompilá el Setup (Release) y listo: al instalar, corre el script y crea la base.

---

## Paso 4 — Probarlo

1. Instalá en una VM o en la PC destino con SQL Express local.
2. Si algo falla, el mensaje de `InstallException` te dice qué (login, sqlcmd no
   encontrado, script no encontrado, error SQL).
3. Comprobá en SSMS que aparezca la base `Ing.Software` con las tablas y datos.

Errores típicos y qué significan:

- **"Login failed for user 'NT AUTHORITY\SYSTEM'"** → la cuenta de instalación no tiene
  permiso en SQL. Solución rápida: en SSMS, agregá ese login como `sysadmin`, o usá la
  alternativa B (abajo).
- **"'sqlcmd' no se reconoce..."** → no está en el PATH. Poné la ruta completa a
  `sqlcmd.exe` en el `FileName`.
- **Error de ruta del script** → es el gotcha del `[TARGETDIR]\`, aplicá el espacio.

---

## Alternativa B — Crear la base en el primer arranque (más confiable)

Si el custom action te pelea con permisos, esto es lo más simple y robusto. Corre como
**el usuario**, que sí suele tener permisos en su SQL local. En `Program.cs`, antes de
abrir el Login:

```csharp
[STAThread]
static void Main()
{
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);

    AsegurarBaseDeDatos_43BO();   // <-- crea la base si no existe

    Application.Run(new Login());
}

static void AsegurarBaseDeDatos_43BO()
{
    try
    {
        string instancia = @".\SQLEXPRESS"; // o leerlo del App.config
        using (var con = new System.Data.SqlClient.SqlConnection(
                   $"Data Source={instancia};Initial Catalog=master;Integrated Security=True"))
        {
            con.Open();
            using (var cmd = new System.Data.SqlClient.SqlCommand("SELECT DB_ID('Ing.Software')", con))
            {
                if (cmd.ExecuteScalar() != DBNull.Value) return; // ya existe, no hago nada
            }
        }

        // no existe -> corro el script con sqlcmd (mismo comando que el custom action)
        string script = System.IO.Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "CrearBaseIngSoftware.sql");

        var psi = new System.Diagnostics.ProcessStartInfo
        {
            FileName = "sqlcmd",
            Arguments = $"-S \".\\SQLEXPRESS\" -E -b -i \"{script}\"",
            UseShellExecute = false, CreateNoWindow = true
        };
        using (var p = System.Diagnostics.Process.Start(psi)) p.WaitForExit();
    }
    catch (Exception ex)
    {
        System.Windows.Forms.MessageBox.Show("No se pudo preparar la base: " + ex.Message);
    }
}
```

Con B **no necesitás el proyecto InstaladorBD ni el custom action**: solo agregás el
`.sql` al Setup (Paso 3.1) para que quede junto al .exe.

---

## Resumen de decisión

- Querés que sea "todo en el instalador" → **camino A** (Pasos 1-4), ojo con permisos.
- Querés que sea a prueba de balas y fácil de depurar → **camino B** (solo copiás el .sql).
- En ambos, el `.sql` es el mismo. Empezá por armar ese script idempotente; es el 80% del trabajo.
