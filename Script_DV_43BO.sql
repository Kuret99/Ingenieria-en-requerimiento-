-- =====================================================================
-- Script para la tabla DV_43BO (Digitos Verificadores)
-- Ejecutar sobre la BD del proyecto antes de correr el sistema.
-- =====================================================================
USE [Ing.Software];
GO

-- 1) Si la tabla no existe, se crea
IF OBJECT_ID('DV_43BO', 'U') IS NULL
BEGIN
    CREATE TABLE DV_43BO (
        NombreTabla_43BO VARCHAR(100) NOT NULL PRIMARY KEY,
        DVH_43BO         BIGINT       NOT NULL,
        DVV_43BO         BIGINT       NOT NULL
    );
END
GO

-- 2) Si la tabla YA existia (como en tu diagrama) pero con otros tipos de
--    datos en DVH/DVV, descomenta y ejecuta estas lineas para asegurarte
--    de que soporten los valores acumulados (BIGINT):
--
-- ALTER TABLE DV_43BO ALTER COLUMN DVH_43BO BIGINT NOT NULL;
-- ALTER TABLE DV_43BO ALTER COLUMN DVV_43BO BIGINT NOT NULL;
-- GO

-- 3) Carpeta de backups: crear C:\Backups43BO y dar permisos de escritura
--    a la cuenta de servicio de SQL Server (NT Service\MSSQL$SQLEXPRESS).
--    El BACKUP DATABASE lo escribe el motor, no la aplicacion.
