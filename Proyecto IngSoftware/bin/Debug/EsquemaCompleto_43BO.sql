/* ============================================================
   Ing.Software - Script completo (esquema + datos)
   Para el instalador: crea la base si no existe y luego
   carga todas las tablas y datos (incluye patentes 21/22/23).
   Correr conectado a 'master'.
   ============================================================ */
IF DB_ID(N'Ing.Software') IS NULL
BEGIN
    CREATE DATABASE [Ing.Software];
END
GO

USE [Ing.Software]
GO
/****** Objeto: Table [dbo].[Bitacora_43BO] Fecha de script: 7/7/2026 19:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bitacora_43BO](
	[IdEvento_43BO] [int] IDENTITY(1,1) NOT NULL,
	[DNIuser_43BO] [int] NOT NULL,
	[Fecha_43BO] [datetime] NOT NULL,
	[Modulo_43BO] [nvarchar](50) NOT NULL,
	[Evento_43BO] [nvarchar](50) NOT NULL,
	[Criticidad_43BO] [int] NOT NULL,
 CONSTRAINT [PK_Bitacora_43BO] PRIMARY KEY CLUSTERED 
(
	[IdEvento_43BO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[DV_43BO] Fecha de script: 7/7/2026 19:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DV_43BO](
	[NombreTabla_43BO] [varchar](50) NOT NULL,
	[DVH_43BO] [varchar](100) NOT NULL,
	[DVV_43BO] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[NombreTabla_43BO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Familia_43BO] Fecha de script: 7/7/2026 19:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Familia_43BO](
	[IdFamilia_43BO] [int] IDENTITY(1,1) NOT NULL,
	[NombreFamilia_43BO] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Familia_43BO] PRIMARY KEY CLUSTERED 
(
	[IdFamilia_43BO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Familia_Familia] Fecha de script: 7/7/2026 19:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Familia_Familia](
	[IdFamiliaPadre_43BO] [int] NOT NULL,
	[IdFamiliaHijo_43BO] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdFamiliaPadre_43BO] ASC,
	[IdFamiliaHijo_43BO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Patente_43BO] Fecha de script: 7/7/2026 19:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patente_43BO](
	[IdPatente_43BO] [int] IDENTITY(1,1) NOT NULL,
	[NombrePatente_43BO] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Patente_43BO] PRIMARY KEY CLUSTERED 
(
	[IdPatente_43BO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Patente_Familia] Fecha de script: 7/7/2026 19:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patente_Familia](
	[IdFamilia_43BO] [int] NOT NULL,
	[IdPatente_43BO] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdFamilia_43BO] ASC,
	[IdPatente_43BO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Rol_43BO] Fecha de script: 7/7/2026 19:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol_43BO](
	[IdRol_43BO] [int] IDENTITY(1,1) NOT NULL,
	[NombreRol_43BO] [varchar](50) NOT NULL,
 CONSTRAINT [PK__Rol_43BO__C5D277CC5292C17E] PRIMARY KEY CLUSTERED 
(
	[IdRol_43BO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Rol_Familia] Fecha de script: 7/7/2026 19:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol_Familia](
	[IdRol_43BO] [int] NOT NULL,
	[IdFamilia_43BO] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRol_43BO] ASC,
	[IdFamilia_43BO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Rol_Patente] Fecha de script: 7/7/2026 19:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol_Patente](
	[IdRol_43BO] [int] NOT NULL,
	[IdPatente_43BO] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRol_43BO] ASC,
	[IdPatente_43BO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Usuarios_43BO] Fecha de script: 7/7/2026 19:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios_43BO](
	[IdUser_43BO] [int] IDENTITY(1,1) NOT NULL,
	[DNI_43BO] [int] NOT NULL,
	[Nombre_43BO] [nvarchar](50) NOT NULL,
	[Apellido_43BO] [nvarchar](50) NOT NULL,
	[Bloqueado_43BO] [bit] NOT NULL,
	[Email_43BO] [nvarchar](50) NOT NULL,
	[Activo_43BO] [bit] NOT NULL,
	[Hash_43BO] [nvarchar](max) NOT NULL,
	[Rol_43BO] [int] NOT NULL,
	[Idioma_43BO] [varchar](10) NULL,
 CONSTRAINT [PK_Usuarios_43BO_1] PRIMARY KEY CLUSTERED 
(
	[DNI_43BO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Bitacora_43BO] ON 

INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2, 46498814, CAST(N'2026-04-28T22:59:33.523' AS DateTime), N'Usuario', N'Desactivar', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (3, 46498814, CAST(N'2026-05-01T19:16:03.517' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4, 45678912, CAST(N'2026-05-01T19:24:56.973' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5, 45678912, CAST(N'2026-05-01T19:25:10.033' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6, 45678912, CAST(N'2026-05-01T19:38:25.577' AS DateTime), N'Usuario', N'Desactivar', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7, 45678912, CAST(N'2026-05-01T19:38:30.883' AS DateTime), N'Usuario', N'Desactivar', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (8, 45678912, CAST(N'2026-05-01T19:38:35.127' AS DateTime), N'Usuario', N'Desactivar', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9, 46498814, CAST(N'2026-05-01T19:40:32.417' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (10, 46498814, CAST(N'2026-05-01T19:44:11.987' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (11, 46498814, CAST(N'2026-05-01T19:44:19.613' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (12, 46498814, CAST(N'2026-05-04T09:31:21.610' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (13, 46498814, CAST(N'2026-05-04T09:34:33.910' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (14, 46498814, CAST(N'2026-05-04T09:38:14.650' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (15, 46498814, CAST(N'2026-05-04T10:39:42.423' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (16, 46498814, CAST(N'2026-05-04T10:44:03.170' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1012, 46498814, CAST(N'2026-05-05T20:43:55.083' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1013, 46498814, CAST(N'2026-05-05T20:47:59.050' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1014, 46498814, CAST(N'2026-05-05T20:48:41.173' AS DateTime), N'Usuario', N'Desactivar', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1015, 46498814, CAST(N'2026-05-05T20:55:42.433' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1016, 46498814, CAST(N'2026-05-05T20:56:40.040' AS DateTime), N'Usuario', N'Desactivar', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1017, 46498814, CAST(N'2026-05-05T20:59:20.567' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1018, 46498814, CAST(N'2026-05-05T20:59:32.203' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1019, 46498814, CAST(N'2026-05-05T21:06:06.640' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1020, 46498814, CAST(N'2026-05-05T21:06:18.547' AS DateTime), N'Usuario', N'Desactivar', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1021, 46498814, CAST(N'2026-05-05T21:06:27.523' AS DateTime), N'Usuario', N'Desactivar', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1022, 46498814, CAST(N'2026-05-05T21:08:46.037' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1023, 46498814, CAST(N'2026-05-05T21:10:06.123' AS DateTime), N'Usuario', N'Crear', 3)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1024, 46498815, CAST(N'2026-05-05T21:16:10.407' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1025, 46498815, CAST(N'2026-05-05T21:17:41.120' AS DateTime), N'Usuario', N'Desactivar', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1026, 46498815, CAST(N'2026-05-05T21:19:12.133' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1027, 46498815, CAST(N'2026-05-05T21:20:23.730' AS DateTime), N'Usuario', N'Desactivar', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1028, 46498815, CAST(N'2026-05-05T21:28:58.837' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1029, 46498815, CAST(N'2026-05-05T21:29:48.677' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1030, 46498815, CAST(N'2026-05-05T21:30:38.853' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1031, 46498815, CAST(N'2026-05-05T22:17:47.457' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1032, 46498815, CAST(N'2026-05-05T22:18:12.670' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1033, 46498815, CAST(N'2026-05-05T22:18:24.200' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1034, 46498814, CAST(N'2026-05-05T22:18:50.033' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1035, 46498814, CAST(N'2026-05-05T22:19:23.063' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1036, 46498814, CAST(N'2026-05-05T22:19:36.393' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1037, 46498814, CAST(N'2026-05-17T14:01:23.223' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1038, 46498814, CAST(N'2026-05-17T14:04:56.717' AS DateTime), N'Usuario', N'Crear', 3)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1039, 46498814, CAST(N'2026-05-17T14:06:36.130' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1040, 11222333, CAST(N'2026-05-17T14:06:56.343' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1041, 11222333, CAST(N'2026-05-17T14:08:57.833' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1042, 11222333, CAST(N'2026-05-17T14:09:07.830' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1043, 46498814, CAST(N'2026-05-17T15:08:25.547' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1044, 46498814, CAST(N'2026-05-17T15:09:14.797' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1045, 46498814, CAST(N'2026-05-17T15:11:47.137' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1046, 11222333, CAST(N'2026-05-17T15:12:12.147' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1047, 46498814, CAST(N'2026-05-17T17:19:21.363' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1048, 46498814, CAST(N'2026-05-17T17:19:40.460' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1049, 46498814, CAST(N'2026-05-17T17:27:26.087' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1050, 46498814, CAST(N'2026-05-17T17:32:55.947' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1051, 46498814, CAST(N'2026-05-17T17:39:58.203' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1052, 46498814, CAST(N'2026-05-17T18:04:12.113' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1053, 46498814, CAST(N'2026-05-17T18:05:34.337' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1054, 46498814, CAST(N'2026-05-17T18:05:48.570' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1055, 46498814, CAST(N'2026-05-17T18:12:01.313' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1056, 46498814, CAST(N'2026-05-17T18:15:26.203' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1057, 46498815, CAST(N'2026-05-17T18:24:09.350' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1058, 46498815, CAST(N'2026-05-17T18:36:55.210' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1059, 46498815, CAST(N'2026-05-17T19:04:30.153' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1060, 46498815, CAST(N'2026-05-17T19:30:41.543' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1061, 46498815, CAST(N'2026-05-17T19:48:22.387' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1062, 46498815, CAST(N'2026-05-17T21:37:39.717' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1063, 46498815, CAST(N'2026-05-17T21:41:48.400' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1064, 46498815, CAST(N'2026-05-17T21:45:35.667' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1065, 46498815, CAST(N'2026-05-17T21:52:29.140' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1066, 46498815, CAST(N'2026-05-17T21:59:31.167' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1067, 46498815, CAST(N'2026-05-17T22:03:48.243' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1068, 46498815, CAST(N'2026-05-17T22:04:28.977' AS DateTime), N'Usuario', N'Crear', 3)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1069, 46498815, CAST(N'2026-05-17T22:12:24.137' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1070, 46498815, CAST(N'2026-05-17T22:15:24.750' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1071, 46498815, CAST(N'2026-05-17T22:47:47.820' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1072, 46498815, CAST(N'2026-05-17T22:52:16.390' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1073, 46498815, CAST(N'2026-05-17T23:16:21.640' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1074, 46498815, CAST(N'2026-05-17T23:26:46.127' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1075, 46498815, CAST(N'2026-05-17T23:30:10.503' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1076, 46498815, CAST(N'2026-05-17T23:32:26.030' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1077, 46498815, CAST(N'2026-05-17T23:35:59.543' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1078, 46498815, CAST(N'2026-05-17T23:37:07.357' AS DateTime), N'Usuario', N'Crear', 3)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1079, 46498815, CAST(N'2026-05-18T19:52:45.777' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1080, 46498815, CAST(N'2026-05-18T19:53:52.420' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1081, 46498815, CAST(N'2026-05-18T19:54:24.733' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1082, 46498815, CAST(N'2026-05-18T23:01:28.363' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1083, 46498815, CAST(N'2026-05-18T23:03:59.097' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1084, 46498815, CAST(N'2026-05-19T13:51:52.460' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1085, 46498815, CAST(N'2026-05-19T13:52:18.720' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1086, 46498815, CAST(N'2026-05-19T13:53:05.403' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1087, 46498815, CAST(N'2026-05-19T13:58:02.213' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1088, 46498815, CAST(N'2026-05-19T14:07:34.900' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1089, 46498815, CAST(N'2026-05-19T17:30:10.410' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1090, 46498815, CAST(N'2026-05-19T17:36:45.173' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1091, 46498815, CAST(N'2026-05-19T18:02:43.507' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1092, 46498814, CAST(N'2026-05-19T18:04:14.537' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1093, 46498815, CAST(N'2026-05-19T19:03:10.887' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1094, 46498815, CAST(N'2026-05-19T20:16:33.807' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1095, 46498815, CAST(N'2026-05-19T20:19:32.907' AS DateTime), N'Usuario', N'Login', 1)
GO
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1096, 46498815, CAST(N'2026-05-19T20:27:45.950' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1097, 46498815, CAST(N'2026-05-19T20:30:51.247' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1098, 46498815, CAST(N'2026-05-19T20:42:33.170' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1099, 46498815, CAST(N'2026-05-19T20:43:08.177' AS DateTime), N'Usuario', N'Crear', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1100, 46498815, CAST(N'2026-05-19T21:04:00.507' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1101, 46498815, CAST(N'2026-05-19T21:04:15.713' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1102, 46498815, CAST(N'2026-05-19T22:04:42.720' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1103, 46498815, CAST(N'2026-05-19T23:14:37.670' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1104, 46498815, CAST(N'2026-05-19T23:14:45.930' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1105, 46498815, CAST(N'2026-05-19T23:14:51.480' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1106, 46498815, CAST(N'2026-05-19T23:15:30.440' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1107, 46498815, CAST(N'2026-05-19T23:15:38.990' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1108, 46498815, CAST(N'2026-05-19T23:15:49.353' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1109, 46498815, CAST(N'2026-05-19T23:16:40.073' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1110, 46498815, CAST(N'2026-05-19T23:16:46.517' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1111, 46498814, CAST(N'2026-05-19T23:17:31.423' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1112, 46498814, CAST(N'2026-05-19T23:18:22.543' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1113, 46498815, CAST(N'2026-05-19T23:19:01.923' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1114, 46498815, CAST(N'2026-05-19T23:19:10.560' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1115, 46498815, CAST(N'2026-05-19T23:19:14.527' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1116, 46498814, CAST(N'2026-05-19T23:19:40.900' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1117, 46498815, CAST(N'2026-05-19T23:21:25.803' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1118, 46498815, CAST(N'2026-05-19T23:21:34.957' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1119, 46498815, CAST(N'2026-05-19T23:21:39.453' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1120, 46498814, CAST(N'2026-05-19T23:21:53.993' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1121, 46498814, CAST(N'2026-05-19T23:22:18.297' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1122, 46498815, CAST(N'2026-05-19T23:25:33.673' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1123, 46498815, CAST(N'2026-05-19T23:25:40.923' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1124, 46498815, CAST(N'2026-05-19T23:25:43.817' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1125, 46498814, CAST(N'2026-05-19T23:26:33.883' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1126, 46498814, CAST(N'2026-05-19T23:26:52.977' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1127, 46498814, CAST(N'2026-05-19T23:29:23.877' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1128, 46498815, CAST(N'2026-05-19T23:32:20.603' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1129, 46498815, CAST(N'2026-05-19T23:32:28.340' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1130, 46498815, CAST(N'2026-05-19T23:32:32.310' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1131, 46498814, CAST(N'2026-05-19T23:32:48.040' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1132, 46498814, CAST(N'2026-05-19T23:33:08.430' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1133, 46498815, CAST(N'2026-05-22T19:38:53.847' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1134, 46498815, CAST(N'2026-05-22T19:43:59.360' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1135, 46498815, CAST(N'2026-05-22T19:44:26.737' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1136, 46498815, CAST(N'2026-05-22T19:44:35.693' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1137, 46498815, CAST(N'2026-05-22T19:44:41.140' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1138, 46498814, CAST(N'2026-05-22T19:44:59.213' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1139, 46498814, CAST(N'2026-05-22T19:45:50.863' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1140, 46498814, CAST(N'2026-05-22T19:46:03.713' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1141, 46498814, CAST(N'2026-05-22T19:46:10.467' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1142, 46498815, CAST(N'2026-05-22T20:31:15.597' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1143, 46498815, CAST(N'2026-05-22T20:34:30.233' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1144, 46498815, CAST(N'2026-05-22T20:59:35.323' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1145, 46498815, CAST(N'2026-05-22T21:05:20.200' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1146, 46498815, CAST(N'2026-05-22T21:25:48.267' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1147, 46498815, CAST(N'2026-05-22T21:30:22.180' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1148, 46498815, CAST(N'2026-05-22T21:39:36.967' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1149, 46498815, CAST(N'2026-05-22T21:42:55.137' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1150, 46498815, CAST(N'2026-05-22T21:46:34.113' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1151, 46498815, CAST(N'2026-05-22T21:49:56.463' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (1152, 46498815, CAST(N'2026-05-22T21:56:17.473' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2133, 46498815, CAST(N'2026-05-25T18:39:29.893' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2134, 46498815, CAST(N'2026-05-25T18:39:39.180' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2135, 46498815, CAST(N'2026-05-25T18:39:42.977' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2136, 46498814, CAST(N'2026-05-25T18:40:03.090' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2137, 46498814, CAST(N'2026-05-25T18:40:33.603' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2138, 46498814, CAST(N'2026-05-25T18:41:01.017' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2139, 46498814, CAST(N'2026-05-25T18:41:05.653' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2140, 46498815, CAST(N'2026-05-25T18:41:50.817' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2141, 46498815, CAST(N'2026-05-25T18:43:09.573' AS DateTime), N'Usuario', N'Crear', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2142, 46498815, CAST(N'2026-05-25T18:43:18.920' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2143, 22666555, CAST(N'2026-05-25T18:43:29.513' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2144, 22666555, CAST(N'2026-05-25T18:44:49.913' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2145, 22666555, CAST(N'2026-05-25T18:44:54.850' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2146, 22666555, CAST(N'2026-05-25T18:45:15.613' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2147, 46498815, CAST(N'2026-05-25T18:49:41.223' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2148, 46498815, CAST(N'2026-05-25T18:55:07.163' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2149, 46498815, CAST(N'2026-05-25T18:57:34.147' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2150, 46498815, CAST(N'2026-05-25T18:58:57.257' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2151, 46498815, CAST(N'2026-05-25T19:02:49.033' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2152, 46498815, CAST(N'2026-05-25T19:06:11.297' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2153, 46498815, CAST(N'2026-05-25T19:09:14.843' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2154, 46498815, CAST(N'2026-05-25T19:18:04.673' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2155, 46498815, CAST(N'2026-05-25T19:20:40.633' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2156, 46498815, CAST(N'2026-05-25T19:27:22.827' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2157, 46498815, CAST(N'2026-05-25T19:28:03.130' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2158, 46498815, CAST(N'2026-05-25T19:33:18.640' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2159, 46498815, CAST(N'2026-05-25T19:51:34.670' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (2160, 46498815, CAST(N'2026-05-25T19:55:54.467' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (3149, 46498815, CAST(N'2026-06-02T18:05:02.517' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (3150, 46498815, CAST(N'2026-06-02T18:05:51.910' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (3151, 46498815, CAST(N'2026-06-02T18:06:00.037' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (3152, 46498815, CAST(N'2026-06-02T18:06:04.070' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (3153, 46498815, CAST(N'2026-06-02T18:07:59.050' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (3154, 46498815, CAST(N'2026-06-02T21:37:30.343' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (3155, 46498815, CAST(N'2026-06-02T21:39:14.287' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (3156, 46498815, CAST(N'2026-06-02T22:20:28.900' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (3157, 46498815, CAST(N'2026-06-02T22:24:35.567' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (3158, 46498815, CAST(N'2026-06-02T22:26:38.957' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (3159, 46498815, CAST(N'2026-06-02T22:32:05.697' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (3160, 46498815, CAST(N'2026-06-02T22:35:49.127' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (3161, 46498815, CAST(N'2026-06-02T22:39:36.777' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (3162, 46498815, CAST(N'2026-06-02T22:55:47.363' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (3163, 46498815, CAST(N'2026-06-02T22:58:35.960' AS DateTime), N'Usuario', N'Login', 1)
GO
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (3164, 46498815, CAST(N'2026-06-02T23:01:28.313' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4153, 46498815, CAST(N'2026-06-06T17:58:04.167' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4154, 46498815, CAST(N'2026-06-06T18:01:20.487' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4155, 46498815, CAST(N'2026-06-06T18:08:29.223' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4156, 46498815, CAST(N'2026-06-06T19:03:10.017' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4157, 46498815, CAST(N'2026-06-06T19:06:10.713' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4158, 46498815, CAST(N'2026-06-06T19:10:37.617' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4159, 46498815, CAST(N'2026-06-06T19:34:05.603' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4160, 46498815, CAST(N'2026-06-06T19:39:22.367' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4161, 46498815, CAST(N'2026-06-06T20:02:25.760' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4162, 46498815, CAST(N'2026-06-06T20:07:23.187' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4163, 46498815, CAST(N'2026-06-06T20:15:36.760' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4164, 46498815, CAST(N'2026-06-06T20:16:54.747' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4165, 46498815, CAST(N'2026-06-06T20:24:19.833' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4166, 46498815, CAST(N'2026-06-06T20:33:39.813' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4167, 46498815, CAST(N'2026-06-06T20:46:42.773' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4168, 46498815, CAST(N'2026-06-06T21:04:11.293' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4169, 46498815, CAST(N'2026-06-06T21:14:21.410' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4170, 46498815, CAST(N'2026-06-06T21:18:50.363' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4171, 46498815, CAST(N'2026-06-06T21:21:58.623' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4172, 46498815, CAST(N'2026-06-06T21:28:04.917' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4173, 46498815, CAST(N'2026-06-06T21:30:51.273' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4174, 46498815, CAST(N'2026-06-06T21:46:55.563' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4175, 46498815, CAST(N'2026-06-06T22:17:46.870' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4176, 46498815, CAST(N'2026-06-06T22:29:00.410' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4177, 46498815, CAST(N'2026-06-06T22:38:43.433' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4178, 46498815, CAST(N'2026-06-06T22:40:20.260' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4179, 46498815, CAST(N'2026-06-06T22:49:55.873' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (4180, 46498815, CAST(N'2026-06-06T22:51:47.947' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5175, 46498815, CAST(N'2026-06-08T15:42:51.393' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5176, 46498815, CAST(N'2026-06-08T16:15:08.657' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5177, 46498815, CAST(N'2026-06-08T16:38:02.100' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5178, 46498815, CAST(N'2026-06-08T17:31:10.657' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5179, 46498815, CAST(N'2026-06-08T17:34:44.403' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5180, 46498815, CAST(N'2026-06-08T18:17:41.370' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5181, 46498815, CAST(N'2026-06-08T18:23:38.687' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5182, 46498815, CAST(N'2026-06-08T18:27:37.993' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5183, 46498815, CAST(N'2026-06-08T18:38:03.157' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5184, 46498815, CAST(N'2026-06-08T19:09:03.407' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5185, 46498815, CAST(N'2026-06-08T19:10:22.860' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5186, 46498815, CAST(N'2026-06-08T19:22:32.367' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5187, 46498815, CAST(N'2026-06-08T19:25:15.367' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5188, 46498815, CAST(N'2026-06-08T19:26:40.437' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5189, 46498815, CAST(N'2026-06-08T19:34:04.730' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5190, 46498815, CAST(N'2026-06-08T19:40:50.907' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5191, 46498815, CAST(N'2026-06-08T19:48:00.480' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5192, 46498815, CAST(N'2026-06-08T19:48:57.400' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5193, 46498815, CAST(N'2026-06-08T20:02:01.493' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5194, 46498815, CAST(N'2026-06-08T20:07:53.870' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5195, 46498815, CAST(N'2026-06-08T20:18:45.013' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5196, 46498815, CAST(N'2026-06-08T20:36:08.810' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5197, 46498815, CAST(N'2026-06-08T20:39:32.510' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5198, 46498815, CAST(N'2026-06-08T20:42:41.790' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5199, 46498815, CAST(N'2026-06-08T20:51:01.290' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5200, 46498815, CAST(N'2026-06-08T20:58:02.617' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5201, 46498815, CAST(N'2026-06-08T21:02:23.793' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5202, 46498815, CAST(N'2026-06-08T21:07:49.987' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5203, 46498815, CAST(N'2026-06-08T21:09:41.413' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5204, 46498815, CAST(N'2026-06-08T21:10:29.777' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5205, 46498815, CAST(N'2026-06-08T21:13:32.450' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (5206, 46498815, CAST(N'2026-06-08T21:18:57.240' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6196, 46498815, CAST(N'2026-06-09T16:13:07.830' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6197, 46498815, CAST(N'2026-06-09T16:18:23.263' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6198, 46498815, CAST(N'2026-06-09T16:39:46.300' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6199, 46498815, CAST(N'2026-06-09T16:50:43.330' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6200, 46498815, CAST(N'2026-06-09T16:57:31.067' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6201, 46498815, CAST(N'2026-06-09T17:00:37.250' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6202, 46498815, CAST(N'2026-06-09T17:09:43.867' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6203, 46498815, CAST(N'2026-06-09T17:15:58.303' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6204, 46498815, CAST(N'2026-06-09T17:26:00.843' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6205, 46498815, CAST(N'2026-06-09T21:18:09.333' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6206, 46498815, CAST(N'2026-06-09T21:26:35.867' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6207, 46498815, CAST(N'2026-06-09T21:27:01.107' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6208, 46498815, CAST(N'2026-06-09T21:27:15.600' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6209, 46498815, CAST(N'2026-06-09T21:30:07.753' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6210, 46498815, CAST(N'2026-06-09T21:33:08.623' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6211, 46498815, CAST(N'2026-06-09T21:35:22.120' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6212, 46498815, CAST(N'2026-06-09T21:37:20.477' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6213, 46498815, CAST(N'2026-06-09T21:37:34.803' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6214, 46498815, CAST(N'2026-06-09T21:52:48.790' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6215, 46498815, CAST(N'2026-06-09T21:57:50.690' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6216, 46498815, CAST(N'2026-06-09T22:08:44.857' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6217, 46498815, CAST(N'2026-06-09T22:09:51.643' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6218, 46498815, CAST(N'2026-06-09T22:34:17.237' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6219, 46498815, CAST(N'2026-06-09T22:49:02.177' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6220, 46498815, CAST(N'2026-06-09T22:50:24.970' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6221, 46498815, CAST(N'2026-06-09T23:03:53.130' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6222, 46498815, CAST(N'2026-06-09T23:06:43.823' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6223, 46498815, CAST(N'2026-06-09T23:28:05.447' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6224, 46498815, CAST(N'2026-06-15T16:11:47.453' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6225, 46498815, CAST(N'2026-06-15T17:39:04.383' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6226, 46498815, CAST(N'2026-06-15T17:39:10.263' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6227, 46498815, CAST(N'2026-06-15T17:42:54.187' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6228, 46498815, CAST(N'2026-06-15T18:55:28.090' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6229, 46498815, CAST(N'2026-06-15T18:57:43.807' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6230, 46498815, CAST(N'2026-06-15T19:20:37.710' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6231, 46498815, CAST(N'2026-06-15T19:42:40.687' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6232, 46498815, CAST(N'2026-06-15T19:55:53.333' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6233, 46498815, CAST(N'2026-06-15T19:56:46.760' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6234, 46498814, CAST(N'2026-06-15T19:57:11.110' AS DateTime), N'Usuario', N'Login', 1)
GO
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6235, 46498814, CAST(N'2026-06-15T20:07:11.840' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6236, 46498814, CAST(N'2026-06-15T20:09:08.047' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6237, 46498814, CAST(N'2026-06-15T20:11:20.130' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6238, 46498815, CAST(N'2026-06-15T20:11:51.400' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6239, 46498815, CAST(N'2026-06-15T20:12:56.210' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6240, 46498814, CAST(N'2026-06-15T20:13:09.850' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6241, 46498814, CAST(N'2026-06-15T20:20:35.567' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (6242, 46498814, CAST(N'2026-06-15T20:20:50.757' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7225, 46498815, CAST(N'2026-06-16T14:56:08.817' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7226, 46498815, CAST(N'2026-06-16T15:00:09.663' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7227, 46498815, CAST(N'2026-06-16T15:56:54.400' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7228, 46498815, CAST(N'2026-06-16T16:03:54.180' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7229, 46498815, CAST(N'2026-06-16T16:14:03.377' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7230, 46498815, CAST(N'2026-06-16T16:44:52.400' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7231, 46498815, CAST(N'2026-06-16T16:59:59.217' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7232, 46498815, CAST(N'2026-06-16T17:00:11.143' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7233, 46498815, CAST(N'2026-06-16T17:05:49.287' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7234, 46498815, CAST(N'2026-06-16T17:06:12.617' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7235, 46498815, CAST(N'2026-06-16T17:10:48.367' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7236, 46498815, CAST(N'2026-06-16T17:11:02.413' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7237, 46498815, CAST(N'2026-06-16T17:11:41.377' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7238, 46498815, CAST(N'2026-06-16T17:11:51.213' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7239, 46498815, CAST(N'2026-06-16T17:18:27.657' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7240, 46498815, CAST(N'2026-06-16T17:18:39.030' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7241, 46498815, CAST(N'2026-06-16T17:25:14.750' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7242, 46498815, CAST(N'2026-06-16T17:25:25.550' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7243, 46498815, CAST(N'2026-06-16T17:34:22.137' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7244, 46498815, CAST(N'2026-06-16T17:34:34.200' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7245, 46498815, CAST(N'2026-06-16T18:08:02.593' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7246, 46498815, CAST(N'2026-06-16T18:10:16.317' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7247, 46498814, CAST(N'2026-06-16T18:11:00.057' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7248, 46498814, CAST(N'2026-06-16T18:11:10.420' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7249, 46498815, CAST(N'2026-06-20T18:56:03.630' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7250, 46498815, CAST(N'2026-06-20T19:19:15.200' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7251, 46498815, CAST(N'2026-06-20T19:19:30.140' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7252, 46498815, CAST(N'2026-06-20T19:20:38.653' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7253, 46498815, CAST(N'2026-06-20T19:27:26.877' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7254, 46498815, CAST(N'2026-06-20T19:28:07.390' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7255, 46498815, CAST(N'2026-06-20T19:28:29.053' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7256, 46498815, CAST(N'2026-06-21T02:28:41.363' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7257, 46498815, CAST(N'2026-06-21T02:28:58.773' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7258, 46498815, CAST(N'2026-06-21T02:29:07.517' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7259, 46498815, CAST(N'2026-06-21T02:31:37.203' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7260, 46498815, CAST(N'2026-06-21T02:33:24.547' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7261, 46498815, CAST(N'2026-06-21T02:36:18.603' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7262, 46498815, CAST(N'2026-06-21T02:37:45.477' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7263, 46498815, CAST(N'2026-06-21T02:38:31.983' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7264, 46498815, CAST(N'2026-06-21T02:40:35.237' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7265, 46498815, CAST(N'2026-06-21T02:46:06.260' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7266, 46498815, CAST(N'2026-06-21T03:09:34.763' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7267, 46498815, CAST(N'2026-06-21T03:14:20.843' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7268, 46498815, CAST(N'2026-06-21T03:15:33.527' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7269, 46498815, CAST(N'2026-06-21T11:29:33.567' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7270, 46498815, CAST(N'2026-06-21T11:44:30.547' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7271, 46498815, CAST(N'2026-06-21T11:47:11.513' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7272, 46498815, CAST(N'2026-06-21T11:50:26.143' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7273, 46498815, CAST(N'2026-06-21T12:43:05.120' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7274, 46498815, CAST(N'2026-06-21T12:46:24.693' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7275, 46498815, CAST(N'2026-06-21T12:58:56.850' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7276, 46498815, CAST(N'2026-06-21T13:01:19.347' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7277, 46498815, CAST(N'2026-06-21T13:17:50.597' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7278, 46498815, CAST(N'2026-06-21T13:23:46.113' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7279, 46498815, CAST(N'2026-06-21T13:30:30.450' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7280, 46498815, CAST(N'2026-06-21T13:31:22.067' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7281, 46498815, CAST(N'2026-06-21T13:33:02.540' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7282, 46498815, CAST(N'2026-06-21T13:33:49.827' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7283, 46498815, CAST(N'2026-06-21T13:37:41.663' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7284, 46498815, CAST(N'2026-06-21T14:18:51.303' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7285, 46498815, CAST(N'2026-06-21T14:27:32.870' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7286, 46498815, CAST(N'2026-06-21T14:29:35.110' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7287, 46498815, CAST(N'2026-06-21T14:43:36.527' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7288, 46498815, CAST(N'2026-06-21T14:50:26.287' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7289, 46498815, CAST(N'2026-06-21T14:53:50.567' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7290, 46498815, CAST(N'2026-06-21T15:03:48.753' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7291, 46498815, CAST(N'2026-06-21T15:09:47.940' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7292, 46498815, CAST(N'2026-06-21T15:19:33.430' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7293, 46498815, CAST(N'2026-06-21T15:26:55.760' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7294, 46498815, CAST(N'2026-06-21T15:28:15.460' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7295, 46498815, CAST(N'2026-06-21T15:35:11.163' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7296, 46498815, CAST(N'2026-06-21T15:40:39.640' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7297, 46498815, CAST(N'2026-06-21T15:44:07.283' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7298, 46498815, CAST(N'2026-06-21T15:45:23.043' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7299, 46498815, CAST(N'2026-06-21T15:56:17.237' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7300, 46498815, CAST(N'2026-06-21T16:46:36.160' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7301, 46498815, CAST(N'2026-06-21T16:48:59.327' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7302, 46498815, CAST(N'2026-06-21T17:08:30.693' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7303, 46498815, CAST(N'2026-06-21T17:21:23.490' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7304, 46498815, CAST(N'2026-06-21T17:40:07.817' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7305, 46498815, CAST(N'2026-06-21T17:40:15.343' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7306, 46498815, CAST(N'2026-06-21T17:40:50.437' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7307, 46498815, CAST(N'2026-06-21T17:52:42.853' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7308, 46498815, CAST(N'2026-06-21T17:53:24.330' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7309, 46498815, CAST(N'2026-06-21T17:53:32.033' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7310, 46498815, CAST(N'2026-06-21T17:53:38.237' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (7311, 46498815, CAST(N'2026-06-21T17:53:59.390' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (8300, 46498815, CAST(N'2026-06-22T16:27:51.630' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (8301, 46498815, CAST(N'2026-06-22T16:39:48.483' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (8302, 46498815, CAST(N'2026-06-22T16:47:11.280' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (8303, 46498815, CAST(N'2026-06-22T16:49:08.710' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (8304, 46498815, CAST(N'2026-06-22T16:51:18.660' AS DateTime), N'Usuario', N'Login', 1)
GO
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (8305, 46498815, CAST(N'2026-06-22T16:55:19.823' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (8306, 46498815, CAST(N'2026-06-22T17:00:53.140' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (8307, 46498815, CAST(N'2026-06-22T17:04:36.723' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (8308, 46498815, CAST(N'2026-06-22T17:08:55.923' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (8309, 46498815, CAST(N'2026-06-22T17:14:34.477' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (8310, 46498815, CAST(N'2026-06-22T17:17:57.750' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (8311, 46498815, CAST(N'2026-06-22T17:20:17.463' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (8312, 46498815, CAST(N'2026-06-22T17:26:59.893' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (8313, 46498815, CAST(N'2026-06-22T17:29:44.213' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (8314, 46498815, CAST(N'2026-06-22T17:56:40.383' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (8315, 46498815, CAST(N'2026-06-22T18:10:03.020' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (8316, 46498815, CAST(N'2026-06-22T18:15:45.410' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9315, 46498815, CAST(N'2026-06-23T00:12:07.433' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9316, 46498815, CAST(N'2026-06-23T00:19:47.573' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9317, 46498815, CAST(N'2026-06-23T00:26:31.273' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9318, 46498815, CAST(N'2026-06-23T00:31:48.097' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9319, 46498815, CAST(N'2026-06-23T00:36:56.110' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9320, 46498815, CAST(N'2026-06-23T00:46:05.777' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9321, 46498815, CAST(N'2026-06-23T00:57:17.117' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9322, 46498815, CAST(N'2026-06-23T01:05:57.880' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9323, 46498815, CAST(N'2026-06-23T01:11:54.507' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9324, 46498815, CAST(N'2026-06-23T01:33:33.990' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9325, 46498815, CAST(N'2026-06-23T01:33:49.997' AS DateTime), N'Perfiles', N'CrearRol', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9326, 46498815, CAST(N'2026-06-23T01:34:00.077' AS DateTime), N'Perfiles', N'ModificarRol', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9327, 46498815, CAST(N'2026-06-23T01:34:02.453' AS DateTime), N'Perfiles', N'AsignarRol', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9328, 46498815, CAST(N'2026-06-23T01:36:58.377' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9329, 46498815, CAST(N'2026-06-23T01:45:23.887' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9330, 46498815, CAST(N'2026-06-23T01:45:55.520' AS DateTime), N'Perfiles', N'CrearRol', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9331, 46498815, CAST(N'2026-06-23T01:46:20.753' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9332, 46498815, CAST(N'2026-06-23T01:46:28.323' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9333, 46498815, CAST(N'2026-06-23T01:51:53.190' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9334, 46498815, CAST(N'2026-06-23T01:52:47.487' AS DateTime), N'Perfiles', N'CrearRol', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9335, 46498815, CAST(N'2026-06-23T01:58:06.660' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9336, 46498815, CAST(N'2026-06-23T01:58:43.687' AS DateTime), N'Perfiles', N'CrearRol', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9337, 46498815, CAST(N'2026-06-23T01:58:45.277' AS DateTime), N'Perfiles', N'AsignarRol', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9338, 46498815, CAST(N'2026-06-23T02:12:35.613' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9339, 46498815, CAST(N'2026-06-23T02:19:09.160' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9340, 46498815, CAST(N'2026-06-23T02:29:45.263' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9341, 46498815, CAST(N'2026-06-23T02:37:25.750' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9342, 46498815, CAST(N'2026-06-23T02:40:31.527' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9343, 46498815, CAST(N'2026-06-23T02:49:12.960' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9344, 46498815, CAST(N'2026-06-23T02:57:15.393' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9345, 46498815, CAST(N'2026-06-23T03:04:07.190' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9346, 46498815, CAST(N'2026-06-23T03:06:22.453' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9347, 46498815, CAST(N'2026-06-23T03:10:02.837' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9348, 46498815, CAST(N'2026-06-23T03:16:31.653' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9349, 46498815, CAST(N'2026-06-23T14:00:55.903' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9350, 46498815, CAST(N'2026-06-23T14:02:03.777' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9351, 46498815, CAST(N'2026-06-23T14:07:02.870' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9352, 46498815, CAST(N'2026-06-23T14:13:02.580' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9353, 46498815, CAST(N'2026-06-23T14:13:19.687' AS DateTime), N'Perfiles', N'CrearRol', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9354, 46498815, CAST(N'2026-06-23T14:13:57.690' AS DateTime), N'Perfiles', N'EliminarRol', 3)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9355, 46498815, CAST(N'2026-06-23T14:14:00.510' AS DateTime), N'Perfiles', N'EliminarRol', 3)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9356, 46498815, CAST(N'2026-06-23T14:56:10.907' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9357, 46498815, CAST(N'2026-06-23T15:26:22.193' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9358, 46498815, CAST(N'2026-06-23T15:27:00.920' AS DateTime), N'Perfiles', N'AsignarRol', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9359, 46498815, CAST(N'2026-06-23T20:07:34.133' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9360, 46498815, CAST(N'2026-06-23T20:40:14.157' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9361, 46498815, CAST(N'2026-06-23T20:40:32.120' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9362, 46498815, CAST(N'2026-06-23T20:40:41.027' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9363, 46498815, CAST(N'2026-06-24T02:55:28.510' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9364, 46498815, CAST(N'2026-06-24T03:05:43.943' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9365, 46498815, CAST(N'2026-06-24T03:08:12.253' AS DateTime), N'Perfiles', N'AsignarFamilia', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9366, 46498815, CAST(N'2026-06-24T03:08:21.853' AS DateTime), N'Perfiles', N'AsignarFamilia', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9367, 46498815, CAST(N'2026-06-24T03:09:39.190' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9368, 46498815, CAST(N'2026-06-24T03:15:34.040' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9369, 46498815, CAST(N'2026-06-24T03:20:41.373' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9370, 46498815, CAST(N'2026-06-24T03:27:51.303' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9371, 46498815, CAST(N'2026-06-24T03:37:28.047' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9372, 46498815, CAST(N'2026-06-24T03:44:19.830' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9373, 46498815, CAST(N'2026-06-24T03:50:00.080' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9374, 46498815, CAST(N'2026-06-24T04:02:52.397' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9375, 46498815, CAST(N'2026-06-24T04:04:57.433' AS DateTime), N'Perfiles', N'AsignarFamilia', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9376, 46498815, CAST(N'2026-06-24T04:11:37.263' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9377, 46498815, CAST(N'2026-06-24T04:12:07.087' AS DateTime), N'Perfiles', N'AsignarFamilia', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9378, 46498815, CAST(N'2026-06-24T04:12:14.260' AS DateTime), N'Perfiles', N'AsignarFamilia', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9379, 46498815, CAST(N'2026-06-24T04:15:00.050' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9380, 46498815, CAST(N'2026-06-24T04:15:14.847' AS DateTime), N'Perfiles', N'AsignarFamilia', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9381, 46498815, CAST(N'2026-06-24T04:15:24.620' AS DateTime), N'Perfiles', N'AsignarFamilia', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9382, 46498815, CAST(N'2026-06-24T04:18:17.057' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9383, 46498815, CAST(N'2026-06-24T04:20:08.347' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9384, 46498815, CAST(N'2026-06-24T04:20:22.863' AS DateTime), N'Perfiles', N'AsignarFamilia', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9385, 46498815, CAST(N'2026-06-24T04:20:52.180' AS DateTime), N'Perfiles', N'AsignarFamilia', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9386, 46498815, CAST(N'2026-06-24T04:20:58.610' AS DateTime), N'Perfiles', N'AsignarFamilia', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9387, 46498815, CAST(N'2026-06-24T04:25:12.237' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9388, 46498815, CAST(N'2026-06-24T04:25:45.867' AS DateTime), N'Perfiles', N'AsignarFamilia', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9389, 46498815, CAST(N'2026-06-24T04:31:00.377' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9390, 46498815, CAST(N'2026-06-24T04:31:22.093' AS DateTime), N'Perfiles', N'AsignarFamilia', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9391, 46498815, CAST(N'2026-06-24T04:32:07.877' AS DateTime), N'Perfiles', N'AsignarFamilia', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9392, 46498815, CAST(N'2026-06-24T04:32:14.887' AS DateTime), N'Perfiles', N'AsignarFamilia', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9393, 46498815, CAST(N'2026-06-24T04:37:40.513' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9394, 46498815, CAST(N'2026-06-24T04:38:29.900' AS DateTime), N'Perfiles', N'AsignarFamilia', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9395, 46498815, CAST(N'2026-06-24T04:44:34.930' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9396, 46498815, CAST(N'2026-06-24T04:44:48.830' AS DateTime), N'Perfiles', N'CrearRol', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9397, 46498815, CAST(N'2026-06-24T04:44:59.920' AS DateTime), N'Perfiles', N'AsignarRol', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9398, 46498815, CAST(N'2026-06-24T04:45:03.480' AS DateTime), N'Perfiles', N'EliminarRol', 3)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9399, 46498815, CAST(N'2026-06-24T05:04:22.127' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9400, 46498815, CAST(N'2026-06-24T05:04:41.577' AS DateTime), N'Perfiles', N'AsignarRol', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9401, 46498815, CAST(N'2026-06-24T05:04:42.263' AS DateTime), N'Perfiles', N'AsignarRol', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9402, 46498815, CAST(N'2026-06-24T05:04:51.640' AS DateTime), N'Usuario', N'Logout', 1)
GO
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9403, 46498815, CAST(N'2026-06-24T05:05:00.503' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9404, 46498815, CAST(N'2026-06-24T05:13:56.267' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9405, 46498815, CAST(N'2026-06-24T05:20:56.080' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9406, 46498815, CAST(N'2026-06-24T05:21:06.780' AS DateTime), N'Perfiles', N'AsignarFamilia', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9407, 46498815, CAST(N'2026-06-24T05:21:52.210' AS DateTime), N'Perfiles', N'AsignarFamilia', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9408, 46498815, CAST(N'2026-06-24T05:26:37.913' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9409, 46498815, CAST(N'2026-06-24T05:26:53.283' AS DateTime), N'Perfiles', N'AsignarFamilia', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9410, 46498815, CAST(N'2026-06-24T05:35:43.430' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9411, 46498815, CAST(N'2026-06-24T05:36:01.090' AS DateTime), N'Perfiles', N'AsignarFamilia', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9412, 46498815, CAST(N'2026-06-24T05:41:34.673' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9413, 46498815, CAST(N'2026-06-24T05:43:18.810' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9414, 46498815, CAST(N'2026-06-24T05:46:21.903' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9415, 46498815, CAST(N'2026-06-24T05:49:42.597' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9416, 46498815, CAST(N'2026-07-06T23:51:10.567' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9417, 46498815, CAST(N'2026-07-07T01:09:46.810' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9418, 46498815, CAST(N'2026-07-07T01:21:36.460' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9419, 46498815, CAST(N'2026-07-07T01:23:46.693' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9420, 46498815, CAST(N'2026-07-07T01:43:07.720' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9421, 46498815, CAST(N'2026-07-07T01:49:43.863' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9422, 46498815, CAST(N'2026-07-07T01:49:45.693' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9423, 46498815, CAST(N'2026-07-07T01:49:47.920' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9424, 46498815, CAST(N'2026-07-07T01:51:07.250' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9425, 46498815, CAST(N'2026-07-07T01:51:07.933' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9426, 46498815, CAST(N'2026-07-07T01:51:09.940' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9427, 46498815, CAST(N'2026-07-07T01:51:29.300' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9428, 46498815, CAST(N'2026-07-07T02:04:11.027' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9429, 46498815, CAST(N'2026-07-07T02:04:22.327' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9430, 46498815, CAST(N'2026-07-07T02:04:36.370' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9431, 46498814, CAST(N'2026-07-07T02:05:30.527' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9432, 46498814, CAST(N'2026-07-07T02:05:51.553' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9433, 46498814, CAST(N'2026-07-07T02:06:13.540' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9434, 46498814, CAST(N'2026-07-07T02:06:32.317' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9435, 46498815, CAST(N'2026-07-07T02:06:40.430' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9436, 46498815, CAST(N'2026-07-07T02:07:42.907' AS DateTime), N'Perfiles', N'AsignarRol', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9437, 46498815, CAST(N'2026-07-07T02:07:51.960' AS DateTime), N'Perfiles', N'EliminarRol', 3)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9438, 46498815, CAST(N'2026-07-07T02:07:58.100' AS DateTime), N'Perfiles', N'EliminarRol', 3)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9439, 46498815, CAST(N'2026-07-07T02:09:00.360' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9440, 46498814, CAST(N'2026-07-07T02:09:22.173' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9441, 46498814, CAST(N'2026-07-07T02:12:06.980' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9442, 46498815, CAST(N'2026-07-07T02:12:14.957' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9443, 46498814, CAST(N'2026-07-07T02:19:36.977' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9444, 46498814, CAST(N'2026-07-07T02:21:27.640' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9445, 46498814, CAST(N'2026-07-07T02:21:39.867' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9446, 46498814, CAST(N'2026-07-07T02:24:47.113' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9447, 46498815, CAST(N'2026-07-07T02:24:55.620' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9448, 46498815, CAST(N'2026-07-07T02:25:13.830' AS DateTime), N'Perfiles', N'AsignarRol', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9449, 46498815, CAST(N'2026-07-07T02:25:16.067' AS DateTime), N'Perfiles', N'AsignarRol', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9450, 46498815, CAST(N'2026-07-07T02:25:22.497' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9451, 46498815, CAST(N'2026-07-07T02:25:32.077' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9452, 46498815, CAST(N'2026-07-07T02:26:32.080' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9453, 46498815, CAST(N'2026-07-07T02:26:41.797' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9454, 46498815, CAST(N'2026-07-07T02:27:01.413' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9455, 46498815, CAST(N'2026-07-07T02:27:07.980' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9456, 46498815, CAST(N'2026-07-07T02:27:27.900' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9457, 46498815, CAST(N'2026-07-07T02:27:45.030' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9458, 46498815, CAST(N'2026-07-07T02:28:01.473' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9459, 46498815, CAST(N'2026-07-07T02:28:30.403' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9460, 46498815, CAST(N'2026-07-07T02:28:48.830' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9461, 46498815, CAST(N'2026-07-07T15:30:35.277' AS DateTime), N'Admin', N'RecalcularDV', 3)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9462, 46498815, CAST(N'2026-07-07T15:30:41.190' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9463, 46498815, CAST(N'2026-07-07T15:31:45.707' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9464, 46498815, CAST(N'2026-07-07T15:31:55.503' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9465, 46498814, CAST(N'2026-07-07T15:40:24.183' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9466, 46498814, CAST(N'2026-07-07T15:40:29.250' AS DateTime), N'Usuario', N'Logout', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9467, 46498814, CAST(N'2026-07-07T15:41:09.290' AS DateTime), N'Usuario', N'Bloqueo', 3)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9468, 46498815, CAST(N'2026-07-07T15:41:29.223' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9469, 46498815, CAST(N'2026-07-07T15:41:35.483' AS DateTime), N'Usuario', N'Desbloqueo', 2)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9470, 46498815, CAST(N'2026-07-07T16:03:39.633' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9471, 46498815, CAST(N'2026-07-07T16:15:50.523' AS DateTime), N'Usuario', N'Login', 1)
INSERT [dbo].[Bitacora_43BO] ([IdEvento_43BO], [DNIuser_43BO], [Fecha_43BO], [Modulo_43BO], [Evento_43BO], [Criticidad_43BO]) VALUES (9472, 46498815, CAST(N'2026-07-07T16:16:08.947' AS DateTime), N'Admin', N'Backup', 2)
SET IDENTITY_INSERT [dbo].[Bitacora_43BO] OFF
GO
INSERT [dbo].[DV_43BO] ([NombreTabla_43BO], [DVH_43BO], [DVV_43BO]) VALUES (N'Bitacora_43BO', N'1562005', N'1562005')
INSERT [dbo].[DV_43BO] ([NombreTabla_43BO], [DVH_43BO], [DVV_43BO]) VALUES (N'Familia_43BO', N'6420', N'6420')
INSERT [dbo].[DV_43BO] ([NombreTabla_43BO], [DVH_43BO], [DVV_43BO]) VALUES (N'Familia_Familia', N'201', N'201')
INSERT [dbo].[DV_43BO] ([NombreTabla_43BO], [DVH_43BO], [DVV_43BO]) VALUES (N'Patente_43BO', N'52508', N'52508')
INSERT [dbo].[DV_43BO] ([NombreTabla_43BO], [DVH_43BO], [DVV_43BO]) VALUES (N'Patente_Familia', N'581', N'581')
INSERT [dbo].[DV_43BO] ([NombreTabla_43BO], [DVH_43BO], [DVV_43BO]) VALUES (N'Rol_43BO', N'9293', N'9293')
INSERT [dbo].[DV_43BO] ([NombreTabla_43BO], [DVH_43BO], [DVV_43BO]) VALUES (N'Rol_Familia', N'0', N'0')
INSERT [dbo].[DV_43BO] ([NombreTabla_43BO], [DVH_43BO], [DVV_43BO]) VALUES (N'Rol_Patente', N'4088', N'4088')
INSERT [dbo].[DV_43BO] ([NombreTabla_43BO], [DVH_43BO], [DVV_43BO]) VALUES (N'TOTAL_BD_43BO', N'1697337', N'1697337')
INSERT [dbo].[DV_43BO] ([NombreTabla_43BO], [DVH_43BO], [DVV_43BO]) VALUES (N'Usuarios_43BO', N'62241', N'62241')
GO
SET IDENTITY_INSERT [dbo].[Familia_43BO] ON 

INSERT [dbo].[Familia_43BO] ([IdFamilia_43BO], [NombreFamilia_43BO]) VALUES (4, N'PatentesVendedor')
INSERT [dbo].[Familia_43BO] ([IdFamilia_43BO], [NombreFamilia_43BO]) VALUES (13, N'BasicoOperario')
INSERT [dbo].[Familia_43BO] ([IdFamilia_43BO], [NombreFamilia_43BO]) VALUES (14, N'PatentesBasico')
INSERT [dbo].[Familia_43BO] ([IdFamilia_43BO], [NombreFamilia_43BO]) VALUES (16, N'PatentesGerente')
SET IDENTITY_INSERT [dbo].[Familia_43BO] OFF
GO
INSERT [dbo].[Familia_Familia] ([IdFamiliaPadre_43BO], [IdFamiliaHijo_43BO]) VALUES (13, 14)
GO
SET IDENTITY_INSERT [dbo].[Patente_43BO] ON 

INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (1, N'GestionUsuarios_Acceso')
INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (2, N'GestionUsuarios_Alta')
INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (3, N'GestionUsuarios_Desbloquear')
INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (4, N'GestionUsuarios_Modificar')
INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (5, N'GestionUsuarios_ActivarDesactivar')
INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (6, N'GestionUsuarios_Listar')
INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (7, N'Auditoria_Acceso')
INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (8, N'Auditoria_Consultar')
INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (9, N'Auditoria_Imprimir')
INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (10, N'Menu_SeccionAdmin')
INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (11, N'Menu_SeccionMaster')
INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (12, N'Menu_SeccionVenta')
INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (13, N'Menu_SeccionCompra')
INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (14, N'Menu_SeccionReporte')
INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (15, N'GestionPerfiles_Acceso')
INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (16, N'GestionPerfiles_AsignarRelaciones')
INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (17, N'GestionPerfiles_ConfigurarEstructura')
INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (18, N'GestionUsuarios_Aplicar')
INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (19, N'Usuario_CambioContraseña')
INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (20, N'Usuario_CambioIdioma')
INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (21, N'Admin_Backup')
INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (22, N'Admin_RecalcularDV')
INSERT [dbo].[Patente_43BO] ([IdPatente_43BO], [NombrePatente_43BO]) VALUES (23, N'Admin_Restore')
SET IDENTITY_INSERT [dbo].[Patente_43BO] OFF
GO
INSERT [dbo].[Patente_Familia] ([IdFamilia_43BO], [IdPatente_43BO]) VALUES (4, 8)
INSERT [dbo].[Patente_Familia] ([IdFamilia_43BO], [IdPatente_43BO]) VALUES (13, 7)
INSERT [dbo].[Patente_Familia] ([IdFamilia_43BO], [IdPatente_43BO]) VALUES (16, 7)
INSERT [dbo].[Patente_Familia] ([IdFamilia_43BO], [IdPatente_43BO]) VALUES (16, 9)
GO
SET IDENTITY_INSERT [dbo].[Rol_43BO] ON 

INSERT [dbo].[Rol_43BO] ([IdRol_43BO], [NombreRol_43BO]) VALUES (1, N'Administrador')
INSERT [dbo].[Rol_43BO] ([IdRol_43BO], [NombreRol_43BO]) VALUES (2, N'Basico')
INSERT [dbo].[Rol_43BO] ([IdRol_43BO], [NombreRol_43BO]) VALUES (9, N'Vendedor')
INSERT [dbo].[Rol_43BO] ([IdRol_43BO], [NombreRol_43BO]) VALUES (16, N'ParaSassa')
INSERT [dbo].[Rol_43BO] ([IdRol_43BO], [NombreRol_43BO]) VALUES (1016, N'Gerente')
INSERT [dbo].[Rol_43BO] ([IdRol_43BO], [NombreRol_43BO]) VALUES (1025, N'ParaPepe')
INSERT [dbo].[Rol_43BO] ([IdRol_43BO], [NombreRol_43BO]) VALUES (1026, N'PruebaBitacora')
INSERT [dbo].[Rol_43BO] ([IdRol_43BO], [NombreRol_43BO]) VALUES (1027, N'pruebaauditoria2')
SET IDENTITY_INSERT [dbo].[Rol_43BO] OFF
GO
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1, 1)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1, 2)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1, 3)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1, 4)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1, 5)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1, 6)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1, 7)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1, 8)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1, 9)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1, 10)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1, 11)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1, 12)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1, 13)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1, 14)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1, 15)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1, 16)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1, 17)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1, 18)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1, 19)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1, 20)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1, 21)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1, 22)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1, 23)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (16, 7)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (16, 10)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (16, 19)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (16, 20)
INSERT [dbo].[Rol_Patente] ([IdRol_43BO], [IdPatente_43BO]) VALUES (1025, 16)
GO
SET IDENTITY_INSERT [dbo].[Usuarios_43BO] ON 

INSERT [dbo].[Usuarios_43BO] ([IdUser_43BO], [DNI_43BO], [Nombre_43BO], [Apellido_43BO], [Bloqueado_43BO], [Email_43BO], [Activo_43BO], [Hash_43BO], [Rol_43BO], [Idioma_43BO]) VALUES (1003, 11222333, N'Martin', N'Martinez', 0, N'Martin@gmail', 1, N'b2835a48f99bdbe2c0176f286c6059721a374011d32d89d16795266f1e2c922d', 2, NULL)
INSERT [dbo].[Usuarios_43BO] ([IdUser_43BO], [DNI_43BO], [Nombre_43BO], [Apellido_43BO], [Bloqueado_43BO], [Email_43BO], [Activo_43BO], [Hash_43BO], [Rol_43BO], [Idioma_43BO]) VALUES (1008, 22666555, N'Pepe', N'Pepe', 0, N'Pepe@gmail.com', 1, N'7c9e7c1494b2684ab7c19d6aff737e460fa9e98d5a234da1310c97ddf5691834', 2, NULL)
INSERT [dbo].[Usuarios_43BO] ([IdUser_43BO], [DNI_43BO], [Nombre_43BO], [Apellido_43BO], [Bloqueado_43BO], [Email_43BO], [Activo_43BO], [Hash_43BO], [Rol_43BO], [Idioma_43BO]) VALUES (1004, 44555666, N'Marcelo', N'Caceres', 0, N'marce@gmail.com', 1, N'a36b91001a3b0fe052b4b3f882a1d16f6963cbc82d5c770d5565c7dc4e217502', 2, NULL)
INSERT [dbo].[Usuarios_43BO] ([IdUser_43BO], [DNI_43BO], [Nombre_43BO], [Apellido_43BO], [Bloqueado_43BO], [Email_43BO], [Activo_43BO], [Hash_43BO], [Rol_43BO], [Idioma_43BO]) VALUES (2, 45678912, N'Monica', N'Argento', 0, N'Jose@gmail.com', 1, N'd2353870a9735aa607f2a61141f61aa255ff802070ecdcf83af0119f9bf22933', 2, NULL)
INSERT [dbo].[Usuarios_43BO] ([IdUser_43BO], [DNI_43BO], [Nombre_43BO], [Apellido_43BO], [Bloqueado_43BO], [Email_43BO], [Activo_43BO], [Hash_43BO], [Rol_43BO], [Idioma_43BO]) VALUES (1, 46498814, N'Joaquin', N'Sassaroli', 0, N'hola.,com', 1, N'4d486479143cb4342e68a9bb77d327a6621a99e96c19b88089263759383c8961', 16, N'es')
INSERT [dbo].[Usuarios_43BO] ([IdUser_43BO], [DNI_43BO], [Nombre_43BO], [Apellido_43BO], [Bloqueado_43BO], [Email_43BO], [Activo_43BO], [Hash_43BO], [Rol_43BO], [Idioma_43BO]) VALUES (1002, 46498815, N'Juan', N'Ortega', 0, N'juanpi@gmail.com', 1, N'b460b1982188f11d175f60ed670027e1afdd16558919fe47023ecd38329e0b7f', 1, N'es')
INSERT [dbo].[Usuarios_43BO] ([IdUser_43BO], [DNI_43BO], [Nombre_43BO], [Apellido_43BO], [Bloqueado_43BO], [Email_43BO], [Activo_43BO], [Hash_43BO], [Rol_43BO], [Idioma_43BO]) VALUES (1007, 66123456, N'Javier', N'fernandez', 0, N'hola@gmail.com', 1, N'8b98d66c6502057ec2c970917bed152e5d58563434e19a0292453ff18eea9930', 2, NULL)
INSERT [dbo].[Usuarios_43BO] ([IdUser_43BO], [DNI_43BO], [Nombre_43BO], [Apellido_43BO], [Bloqueado_43BO], [Email_43BO], [Activo_43BO], [Hash_43BO], [Rol_43BO], [Idioma_43BO]) VALUES (1006, 77888999, N'Juan', N'Ortega', 0, N'ortegita@gmail.com', 1, N'cae98ae06faab98103fd533e4b8636ab350dbeaabf0e42ee498e31b0147d7eb9', 1, NULL)
SET IDENTITY_INSERT [dbo].[Usuarios_43BO] OFF
GO
/****** Objeto: Index [UQ_DNI] Fecha de script: 7/7/2026 19:15:46 ******/
ALTER TABLE [dbo].[Usuarios_43BO] ADD  CONSTRAINT [UQ_DNI] UNIQUE NONCLUSTERED 
(
	[DNI_43BO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Usuarios_43BO] ADD  CONSTRAINT [DF_Usuarios_43BO_Bloqueado_43BO]  DEFAULT ((0)) FOR [Bloqueado_43BO]
GO
ALTER TABLE [dbo].[Usuarios_43BO] ADD  CONSTRAINT [DF_Usuarios_43BO_Activo_43BO]  DEFAULT ((1)) FOR [Activo_43BO]
GO
ALTER TABLE [dbo].[Usuarios_43BO] ADD  DEFAULT ('es') FOR [Idioma_43BO]
GO
ALTER TABLE [dbo].[Bitacora_43BO]  WITH CHECK ADD  CONSTRAINT [FK_Bitacora_43BO_Usuarios_43BO] FOREIGN KEY([DNIuser_43BO])
REFERENCES [dbo].[Usuarios_43BO] ([DNI_43BO])
GO
ALTER TABLE [dbo].[Bitacora_43BO] CHECK CONSTRAINT [FK_Bitacora_43BO_Usuarios_43BO]
GO
ALTER TABLE [dbo].[Familia_Familia]  WITH CHECK ADD  CONSTRAINT [FK__Familia_F__IdFam__2BFE89A6] FOREIGN KEY([IdFamiliaPadre_43BO])
REFERENCES [dbo].[Familia_43BO] ([IdFamilia_43BO])
GO
ALTER TABLE [dbo].[Familia_Familia] CHECK CONSTRAINT [FK__Familia_F__IdFam__2BFE89A6]
GO
ALTER TABLE [dbo].[Familia_Familia]  WITH CHECK ADD  CONSTRAINT [FK__Familia_F__IdFam__2CF2ADDF] FOREIGN KEY([IdFamiliaHijo_43BO])
REFERENCES [dbo].[Familia_43BO] ([IdFamilia_43BO])
GO
ALTER TABLE [dbo].[Familia_Familia] CHECK CONSTRAINT [FK__Familia_F__IdFam__2CF2ADDF]
GO
ALTER TABLE [dbo].[Patente_Familia]  WITH NOCHECK ADD FOREIGN KEY([IdFamilia_43BO])
REFERENCES [dbo].[Familia_43BO] ([IdFamilia_43BO])
GO
ALTER TABLE [dbo].[Patente_Familia]  WITH NOCHECK ADD FOREIGN KEY([IdPatente_43BO])
REFERENCES [dbo].[Patente_43BO] ([IdPatente_43BO])
GO
ALTER TABLE [dbo].[Rol_Familia]  WITH CHECK ADD FOREIGN KEY([IdFamilia_43BO])
REFERENCES [dbo].[Familia_43BO] ([IdFamilia_43BO])
GO
ALTER TABLE [dbo].[Rol_Familia]  WITH CHECK ADD  CONSTRAINT [FK__Rol_Famil__IdRol__245D67DE] FOREIGN KEY([IdRol_43BO])
REFERENCES [dbo].[Rol_43BO] ([IdRol_43BO])
GO
ALTER TABLE [dbo].[Rol_Familia] CHECK CONSTRAINT [FK__Rol_Famil__IdRol__245D67DE]
GO
ALTER TABLE [dbo].[Rol_Patente]  WITH NOCHECK ADD FOREIGN KEY([IdPatente_43BO])
REFERENCES [dbo].[Patente_43BO] ([IdPatente_43BO])
GO
ALTER TABLE [dbo].[Rol_Patente]  WITH NOCHECK ADD  CONSTRAINT [FK__Rol_Paten__IdRol__1EA48E88] FOREIGN KEY([IdRol_43BO])
REFERENCES [dbo].[Rol_43BO] ([IdRol_43BO])
GO
ALTER TABLE [dbo].[Rol_Patente] CHECK CONSTRAINT [FK__Rol_Paten__IdRol__1EA48E88]
GO
ALTER TABLE [dbo].[Usuarios_43BO]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_43BO_Rol_43BO] FOREIGN KEY([Rol_43BO])
REFERENCES [dbo].[Rol_43BO] ([IdRol_43BO])
GO
ALTER TABLE [dbo].[Usuarios_43BO] CHECK CONSTRAINT [FK_Usuarios_43BO_Rol_43BO]
GO
/****** Objeto: StoredProcedure [dbo].[DesvincularComponente_43BO] Fecha de script: 7/7/2026 19:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DesvincularComponente_43BO]
    @idPadre INT, 
    @idHijo INT, 
    @tipo VARCHAR(30)
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @tipo = 'RolPatente' 
        DELETE FROM Rol_Patente WHERE IdRol_43BO = @idPadre AND IdPatente_43BO = @idHijo;
    ELSE IF @tipo = 'RolFamilia' 
        DELETE FROM Rol_Familia WHERE IdRol_43BO = @idPadre AND IdFamilia_43BO = @idHijo;
    ELSE IF @tipo = 'PatenteFamilia' 
        DELETE FROM Patente_Familia WHERE IdFamilia_43BO = @idPadre AND IdPatente_43BO = @idHijo;
    ELSE IF @tipo = 'FamiliaFamilia' 
        DELETE FROM Familia_Familia WHERE IdFamiliaPadre_43BO = @idPadre AND IdFamiliaHijo_43BO = @idHijo;
END

GO
/****** Objeto: StoredProcedure [dbo].[EliminarComponente_43BO] Fecha de script: 7/7/2026 19:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarComponente_43BO]
    @idComponente INT,
    @tipo VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @tipo = 'Rol'
    BEGIN
        DELETE FROM Rol_Patente WHERE IdRol_43BO = @idComponente;
        DELETE FROM Rol_Familia WHERE IdRol_43BO = @idComponente;
        DELETE FROM Rol_43BO WHERE IdRol_43BO = @idComponente;
    END
    ELSE IF @tipo = 'Familia'
    BEGIN
        DELETE FROM Rol_Familia WHERE IdFamilia_43BO = @idComponente;
        DELETE FROM Patente_Familia WHERE IdFamilia_43BO = @idComponente;
        DELETE FROM Familia_Familia WHERE IdFamiliaPadre_43BO = @idComponente OR IdFamiliaHijo_43BO = @idComponente;
        DELETE FROM Familia_43BO WHERE IdFamilia_43BO = @idComponente;
    END
END

GO
/****** Objeto: StoredProcedure [dbo].[InsertarComponente_43BO] Fecha de script: 7/7/2026 19:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertarComponente_43BO]
    @nombre VARCHAR(100),
    @tipo VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @tipo = 'Familia'
    BEGIN
        INSERT INTO Familia_43BO (NombreFamilia_43BO) VALUES (@nombre);
        SELECT SCOPE_IDENTITY();
    END
    ELSE IF @tipo = 'Rol'
    BEGIN
        INSERT INTO Rol_43BO (NombreRol_43BO) VALUES (@nombre);
        SELECT SCOPE_IDENTITY();
    END
END

GO
/****** Objeto: StoredProcedure [dbo].[ListarTodo_43BO] Fecha de script: 7/7/2026 19:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ListarTodo_43BO]
    @tipo VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @tipo = 'Patente'
        SELECT IdPatente_43BO AS Id_43BO, NombrePatente_43BO AS Nombre_43BO FROM Patente_43BO;
    ELSE IF @tipo = 'Familia'
        SELECT IdFamilia_43BO AS Id_43BO, NombreFamilia_43BO AS Nombre_43BO FROM Familia_43BO;
    ELSE IF @tipo = 'Rol'
        SELECT IdRol_43BO AS Id_43BO, NombreRol_43BO AS Nombre_43BO FROM Rol_43BO;
END

GO
/****** Objeto: StoredProcedure [dbo].[ModificarNombreComponente_43BO] Fecha de script: 7/7/2026 19:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[ModificarNombreComponente_43BO]
    @Id INT,
    @NuevoNombre VARCHAR(100),
    @Tipo VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Pasamos a mayúsculas y limpiamos espacios por las dudas
    DECLARE @TipoLimpio VARCHAR(20) = UPPER(TRIM(@Tipo));

    IF @TipoLimpio = 'ROL'
    BEGIN
        UPDATE Rol_43BO 
        SET NombreRol_43BO = @NuevoNombre 
        WHERE IdRol_43BO = @Id;
    END
    ELSE IF @TipoLimpio = 'FAMILIA'
    BEGIN
        UPDATE Familia_43BO 
        SET NombreFamilia_43BO = @NuevoNombre 
        WHERE IdFamilia_43BO = @Id;
    END
END

GO
/****** Objeto: StoredProcedure [dbo].[ObtenerComponentesHijos_43BO] Fecha de script: 7/7/2026 19:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerComponentesHijos_43BO]
    @idPadre INT,
    @tipo VARCHAR(30)
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @tipo = 'PatentesRol'
        SELECT rp.IdPatente_43BO AS Id_43BO, p.NombrePatente_43BO AS Nombre_43BO 
        FROM Rol_Patente rp 
        INNER JOIN Patente_43BO p ON rp.IdPatente_43BO = p.IdPatente_43BO 
        WHERE rp.IdRol_43BO = @idPadre;
        
    ELSE IF @tipo = 'FamiliasRol'
        SELECT rf.IdFamilia_43BO AS Id_43BO, f.NombreFamilia_43BO AS Nombre_43BO 
        FROM Rol_Familia rf 
        INNER JOIN Familia_43BO f ON rf.IdFamilia_43BO = f.IdFamilia_43BO 
        WHERE rf.IdRol_43BO = @idPadre;
        
    ELSE IF @tipo = 'PatentesFamilia'
        SELECT pf.IdPatente_43BO AS Id_43BO, p.NombrePatente_43BO AS Nombre_43BO 
        FROM Patente_Familia pf 
        INNER JOIN Patente_43BO p ON pf.IdPatente_43BO = p.IdPatente_43BO 
        WHERE pf.IdFamilia_43BO = @idPadre;
        
    ELSE IF @tipo = 'FamiliasHijas'
        SELECT ff.IdFamiliaHijo_43BO AS Id_43BO, f.NombreFamilia_43BO AS Nombre_43BO 
        FROM Familia_Familia ff 
        INNER JOIN Familia_43BO f ON ff.IdFamiliaHijo_43BO = f.IdFamilia_43BO 
        WHERE ff.IdFamiliaPadre_43BO = @idPadre;
END

GO
/****** Objeto: StoredProcedure [dbo].[ObtenerIdsPermisosPorRol_43BO] Fecha de script: 7/7/2026 19:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerIdsPermisosPorRol_43BO]
    @idRol INT
AS
BEGIN
    SET NOCOUNT ON;
    WITH CTE_Familias AS (
        SELECT IdFamilia_43BO 
        FROM Rol_Familia 
        WHERE IdRol_43BO = @idRol
        UNION ALL
        SELECT ff.IdFamiliaHijo_43BO
        FROM Familia_Familia ff
        INNER JOIN CTE_Familias c ON ff.IdFamiliaPadre_43BO = c.IdFamilia_43BO
    )
    SELECT IdPatente_43BO FROM Rol_Patente WHERE IdRol_43BO = @idRol
    UNION
    SELECT pf.IdPatente_43BO 
    FROM Patente_Familia pf
    INNER JOIN CTE_Familias f ON pf.IdFamilia_43BO = f.IdFamilia_43BO;
END

GO
/****** Objeto: StoredProcedure [dbo].[VincularComponente_43BO] Fecha de script: 7/7/2026 19:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 1. CORRECCIÓN AL SP DE VINCULACIÓN
CREATE PROCEDURE [dbo].[VincularComponente_43BO]
    @idPadre INT, 
    @idHijo INT, 
    @tipo VARCHAR(30)
AS
BEGIN
    SET NOCOUNT OFF; -- Activado para que devuelva las filas afectadas a C#
    
    IF @tipo = 'RolPatente' 
        INSERT INTO Rol_Patente (IdRol_43BO, IdPatente_43BO) VALUES (@idPadre, @idHijo);
    ELSE IF @tipo = 'RolFamilia' 
        INSERT INTO Rol_Familia (IdRol_43BO, IdFamilia_43BO) VALUES (@idPadre, @idHijo);
    ELSE IF @tipo = 'PatenteFamilia' 
        INSERT INTO Patente_Familia (IdFamilia_43BO, IdPatente_43BO) VALUES (@idPadre, @idHijo);
    ELSE IF @tipo = 'FamiliaFamilia' 
        INSERT INTO Familia_Familia (IdFamiliaPadre_43BO, IdFamiliaHijo_43BO) VALUES (@idPadre, @idHijo);
END

GO
