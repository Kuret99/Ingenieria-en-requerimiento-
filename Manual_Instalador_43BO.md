# Manual para armar el instalador (Ing. Software 43BO)

Guía informal para copiar/pegar en el manual. Pensada para Visual Studio Installer
Projects + SQL Server Express local.

---

## 0. Cómo pensar el instalador (leer esto primero)

El instalador **solo copia tu aplicación** (el .exe, las .dll, la carpeta `Idiomas`
y el `App.config`) y crea los accesos directos. **No instala SQL Server ni crea la
base de datos**: eso son cosas aparte.

Para que la app funcione en la PC destino (la del profe) hacen falta 3 cosas:

1. **.NET Framework 4.8** instalado.
2. **SQL Server Express** instalado, con la base **`Ing.Software`** ya creada (con tus
   tablas y datos).
3. El **`App.config`** apuntando a la instancia de SQL correcta.

El instalador puede ocuparse de 1 y 3. La base (punto 2) la vemos aparte.

---

## 1. La cadena de conexión ahora es configurable (cambio ya hecho en el código)

Antes la conexión estaba fija en el código a `Richard\SQLEXPRESS`, así que solo
funcionaba en la máquina del que la escribió. Ahora `AccesoBD_43BO` la lee del
`App.config`:

```xml
<connectionStrings>
  <add name="Ing.Software"
       connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=Ing.Software;Integrated Security=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

- Si el `App.config` tiene la clave `Ing.Software`, usa esa cadena.
- Si no la encuentra, usa por defecto `Data Source=.\SQLEXPRESS` (la instancia
  SQLEXPRESS local de la misma máquina).

`.\SQLEXPRESS` significa "SQL Express instalado en esta misma computadora". Por eso,
si el profe instala SQL Express local, **funciona sin tocar nada**.

Hay 3 formas de fijar la instancia en la PC destino:

- **(a) A mano:** abrir el `App.config` instalado y cambiar `Data Source` por el
  nombre de la instancia del profe. Simple pero manual.
- **(b) El instalador la pide:** una pantalla del instalador donde el usuario escribe
  la instancia, y una *custom action* que la guarda en el `App.config` (ver punto 4).
- **(c) Autodetectar:** una *custom action* que lista las instancias de SQL de la PC y
  deja elegir (ver punto 4). Esto es "lo que hizo tu amigo".

---

## 2. Requisitos previos en la PC destino

- Windows con **.NET Framework 4.8**.
- **SQL Server Express** (misma versión con la que desarrollás, o compatible).
- **SQL Server Management Studio (SSMS)** — opcional, solo si vas a crear/restaurar la
  base a mano.

---

## 3. Armar el Setup con Visual Studio Installer Projects

### 3.1 Instalar la extensión
1. En Visual Studio: `Extensions > Manage Extensions`.
2. Buscar **"Microsoft Visual Studio Installer Projects"**, instalar y reiniciar VS.

### 3.2 Crear el proyecto de instalación
1. Clic derecho en la **Solución** > `Add > New Project`.
2. Elegir **Setup Project** (queda, por ejemplo, `Setup43BO`).

### 3.3 Meter la aplicación
1. Clic derecho en el Setup > `Add > Project Output...`.
2. Elegir el proyecto de la GUI (`Proyecto IngSoftware`) y **Primary Output**.
   Esto arrastra el `.exe`, las `.dll` (BLL, DAL, Servicios, BE, Newtonsoft, iTextSharp)
   y el `App.config` (queda como `Proyecto IngSoftware.exe.config`).

### 3.4 Agregar la carpeta Idiomas (IMPORTANTE)
La app carga los textos desde una subcarpeta `Idiomas` **al lado del .exe**. Si no la
copiás, la app abre sin traducciones.
1. En el editor **File System** del Setup, clic derecho en `Application Folder`
   `> Add > Folder`, nombrala **Idiomas**.
2. Dentro de esa carpeta `> Add > File...` y agregá `es.json`, `en.json`, `pr.json`.
   (Verificá también que en el proyecto esos .json estén como *Copy to Output = Copy if newer*.)

### 3.5 Prerequisitos (.NET y SQL Express)
1. Clic derecho en el Setup > `Properties`.
2. Botón **Prerequisites...**.
3. Tildar **.NET Framework 4.8** y, si aparece, **SQL Server Express**.
4. Opción "Download prerequisites from the same location as my application" (así el
   instalador queda autocontenido).

> Nota: el bootstrapper de SQL Express puede no venir listado según tu versión de VS. Si
> no está, lo más práctico es documentar "instalar SQL Express antes" o incluir su
> instalador aparte.

### 3.6 Accesos directos
1. En **File System**, clic derecho en `User's Desktop` > `Create New Shortcut` >
   apuntar al **Primary Output**. Renombralo (ej. "Ing Software").
2. Repetir en `User's Programs Menu` si querés que aparezca en el menú inicio.

### 3.7 Compilar
1. Poné el Setup en **Release**.
2. Build del Setup > genera `setup.exe` + `Setup43BO.msi` en `bin\Release`.
   Eso es lo que llevás a la otra PC.

---

## 4. Seleccionar la instancia de SQL ("lo que hizo tu amigo")

Los Installer Projects traen diálogos de UI **limitados** (pantallas tipo "Textboxes").
Con eso podés pedir la instancia por texto, pero para **detectarlas automáticamente**
necesitás una *custom action*: un mini programa que corre durante la instalación.

### 4.1 Listar las instancias de SQL de la máquina
```csharp
using System.Data.Sql;

var instancias = SqlDataSourceEnumerator.Instance.GetDataSources();
foreach (System.Data.DataRow fila in instancias.Rows)
{
    string servidor  = fila["ServerName"].ToString();
    string instancia = fila["InstanceName"].ToString();   // ej. SQLEXPRESS
    string completo  = string.IsNullOrEmpty(instancia) ? servidor : servidor + "\\" + instancia;
    // mostrar "completo" en un combo para que el usuario elija
}
```

### 4.2 Escribir la instancia elegida en el App.config instalado
```csharp
using System.Configuration;

// abre el App.config del ejecutable instalado y reemplaza el Data Source
var config = ConfigurationManager.OpenExeConfiguration(rutaDelExeInstalado);
var cs = config.ConnectionStrings.ConnectionStrings["Ing.Software"];
cs.ConnectionString =
    $"Data Source={instanciaElegida};Initial Catalog=Ing.Software;Integrated Security=True";
config.Save(ConfigurationSaveMode.Modified);
```

Esto se empaqueta como **Custom Action** en el Setup (pestaña *Custom Actions*, evento
*Install*). "Propiedades SQL" que te mencionó tu amigo seguramente es la combinación de:
el **prerequisito de SQL Express** + esta **custom action** que detecta la instancia y
la guarda en el `App.config`.

> Consejo: para entregar rápido, la opción más simple y robusta es dejar `.\SQLEXPRESS`
> por defecto y, si hace falta, editar el `App.config` a mano. La custom action de
> autodetección es "lindo para tener" pero agrega complejidad.

---

## 5. La base de datos en la PC destino (lo detallamos después)

La app **no crea el esquema sola**; la base tiene que existir en la instancia. Opciones
para dejarla lista (esto lo vemos en detalle en el próximo paso):

- **(a) A mano:** correr tu script `.sql` en SSMS una vez.
- **(b) Custom action del instalador:** que ejecute `sqlcmd -S <instancia> -i script.sql`
  al final de la instalación.
- **(c) Restaurar/adjuntar** el `.bak` o `.mdf` que ya generaste con los datos.

Como ya creaste una base con datos para que el profe se conecte, la (c) (restaurar un
`.bak`) suele ser la más cómoda. Cuando quieras, armamos el script limpio y vemos cómo
engancharlo al instalador.

---

## 6. Recordatorio de backups en la PC destino

- La carpeta por defecto es `C:\Backups43BO` (se crea sola al abrir la pantalla de Backup).
- El `.bak` lo escribe **la cuenta de servicio de SQL Server**, no la app. Esa cuenta
  necesita permiso de escritura en la carpeta. Si da "Operating system error 5", darle
  permiso de *Modificar* a la cuenta del servicio o elegir otra carpeta local.

---

## Checklist final antes de entregar

- [ ] Recompilaste la solución en Release.
- [ ] El Setup incluye Primary Output + carpeta `Idiomas` con los 3 .json.
- [ ] Prerequisitos: .NET 4.8 (y SQL Express si corresponde).
- [ ] `App.config` con `Data Source` correcto (o custom action de instancia).
- [ ] En la PC destino: SQL Express instalado + base `Ing.Software` creada con datos.
- [ ] Probaste login, cambio de idioma, backup y auditoría en la PC destino.
