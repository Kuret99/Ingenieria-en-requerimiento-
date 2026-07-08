USE [Ing.Software]
GO

/*
    Script v9 - Patentes del modulo Admin + registro en Bitacora
    -----------------------------------------------------------------
    Agrega 3 patentes nuevas para las acciones de mantenimiento de la BD
    y las asigna al rol Administrador.

    IMPORTANTE: los IdPatente_43BO DEBEN coincidir con el enum
    Servicios.Permisos_43BO del codigo:

        Admin_Backup       = 21
        Admin_RecalcularDV = 22
        Admin_Restore      = 23

    El script es idempotente: se puede correr mas de una vez sin duplicar.
*/

SET NOCOUNT ON;

/* ------------------------------------------------------------------ */
/* 1) Alta de las patentes con id fijo (la columna es IDENTITY)        */
/* ------------------------------------------------------------------ */
SET IDENTITY_INSERT [dbo].[Patente_43BO] ON;

IF NOT EXISTS (SELECT 1 FROM [dbo].[Patente_43BO] WHERE [IdPatente_43BO] = 21)
    INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (21, N'Admin_Backup');

IF NOT EXISTS (SELECT 1 FROM [dbo].[Patente_43BO] WHERE [IdPatente_43BO] = 22)
    INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (22, N'Admin_RecalcularDV');

IF NOT EXISTS (SELECT 1 FROM [dbo].[Patente_43BO] WHERE [IdPatente_43BO] = 23)
    INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (23, N'Admin_Restore');

SET IDENTITY_INSERT [dbo].[Patente_43BO] OFF;
GO

/* ------------------------------------------------------------------ */
/* 2) Asignacion de las patentes al rol Administrador                  */
/*    Se resuelve el id del rol por nombre para no depender de que     */
/*    sea siempre 1.                                                   */
/* ------------------------------------------------------------------ */
DECLARE @idAdmin INT =
    (SELECT TOP 1 [IdRol_43BO]
       FROM [dbo].[Rol_43BO]
      WHERE LTRIM(RTRIM([NombreRol_43BO])) = 'Administrador');

IF @idAdmin IS NULL
BEGIN
    RAISERROR('No se encontro el rol Administrador. No se asignaron las patentes.', 16, 1);
END
ELSE
BEGIN
    IF NOT EXISTS (SELECT 1 FROM [dbo].[Rol_Patente] WHERE [IdRol_43BO] = @idAdmin AND [IdPatente_43BO] = 21)
        INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (@idAdmin, 21);

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Rol_Patente] WHERE [IdRol_43BO] = @idAdmin AND [IdPatente_43BO] = 22)
        INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (@idAdmin, 22);

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Rol_Patente] WHERE [IdRol_43BO] = @idAdmin AND [IdPatente_43BO] = 23)
        INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (@idAdmin, 23);
END
GO

/* ------------------------------------------------------------------ */
/* 3) Control: se listan las patentes admin y a que roles quedaron     */
/* ------------------------------------------------------------------ */
SELECT p.[IdPatente_43BO], p.[NombrePatente_43BO], r.[NombreRol_43BO]
  FROM [dbo].[Patente_43BO] p
  LEFT JOIN [dbo].[Rol_Patente] rp ON rp.[IdPatente_43BO] = p.[IdPatente_43BO]
  LEFT JOIN [dbo].[Rol_43BO]     r  ON r.[IdRol_43BO]     = rp.[IdRol_43BO]
 WHERE p.[IdPatente_43BO] IN (21, 22, 23)
 ORDER BY p.[IdPatente_43BO];
GO
