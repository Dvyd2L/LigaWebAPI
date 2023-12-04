/****** Object:  Table [dbo].[Equipos]    Script Date: 30/11/2023 21:50:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Ciudad] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Jugadores]    Script Date: 30/11/2023 21:50:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jugadores](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](150) NOT NULL,
	[Edad] [int] NOT NULL,
	[Sueldo] [decimal](9, 2) NOT NULL,
	[Lesionado] [bit] NOT NULL,
	[EquipoId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 30/11/2023 21:50:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](500) NOT NULL,
	[Salt] [varbinary](max) NULL,
	[Rol] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Equipos] ON 
GO
INSERT [dbo].[Equipos] ([Id], [Nombre], [Ciudad]) VALUES (1, N'Equipo 1', N'Madrid')
GO
INSERT [dbo].[Equipos] ([Id], [Nombre], [Ciudad]) VALUES (2, N'Equipo 2', N'Barcelona')
GO
SET IDENTITY_INSERT [dbo].[Equipos] OFF
GO
SET IDENTITY_INSERT [dbo].[Jugadores] ON 
GO
INSERT [dbo].[Jugadores] ([Id], [Nombre], [Edad], [Sueldo], [Lesionado], [EquipoId]) VALUES (2, N'Juan', 25, CAST(1500.00 AS Decimal(9, 2)), 1, 1)
GO
INSERT [dbo].[Jugadores] ([Id], [Nombre], [Edad], [Sueldo], [Lesionado], [EquipoId]) VALUES (3, N'Pedro', 28, CAST(2000.00 AS Decimal(9, 2)), 0, 1)
GO
INSERT [dbo].[Jugadores] ([Id], [Nombre], [Edad], [Sueldo], [Lesionado], [EquipoId]) VALUES (4, N'Samuel', 30, CAST(1800.00 AS Decimal(9, 2)), 0, 1)
GO
INSERT [dbo].[Jugadores] ([Id], [Nombre], [Edad], [Sueldo], [Lesionado], [EquipoId]) VALUES (5, N'Felipe', 33, CAST(2100.00 AS Decimal(9, 2)), 0, 1)
GO
INSERT [dbo].[Jugadores] ([Id], [Nombre], [Edad], [Sueldo], [Lesionado], [EquipoId]) VALUES (6, N'Marcos', 23, CAST(1750.00 AS Decimal(9, 2)), 1, 1)
GO
INSERT [dbo].[Jugadores] ([Id], [Nombre], [Edad], [Sueldo], [Lesionado], [EquipoId]) VALUES (7, N'David', 26, CAST(2100.00 AS Decimal(9, 2)), 1, 2)
GO
INSERT [dbo].[Jugadores] ([Id], [Nombre], [Edad], [Sueldo], [Lesionado], [EquipoId]) VALUES (8, N'Fernando', 24, CAST(2650.00 AS Decimal(9, 2)), 0, 2)
GO
INSERT [dbo].[Jugadores] ([Id], [Nombre], [Edad], [Sueldo], [Lesionado], [EquipoId]) VALUES (9, N'Francisco', 26, CAST(1900.00 AS Decimal(9, 2)), 0, 2)
GO
INSERT [dbo].[Jugadores] ([Id], [Nombre], [Edad], [Sueldo], [Lesionado], [EquipoId]) VALUES (10, N'Lucas', 25, CAST(1100.00 AS Decimal(9, 2)), 0, 2)
GO
INSERT [dbo].[Jugadores] ([Id], [Nombre], [Edad], [Sueldo], [Lesionado], [EquipoId]) VALUES (11, N'Andrés', 29, CAST(3500.00 AS Decimal(9, 2)), 0, 2)
GO
SET IDENTITY_INSERT [dbo].[Jugadores] OFF
GO
ALTER TABLE [dbo].[Jugadores]  WITH CHECK ADD  CONSTRAINT [FK_Equipos_Jugadores] FOREIGN KEY([EquipoId])
REFERENCES [dbo].[Equipos] ([Id])
GO
ALTER TABLE [dbo].[Jugadores] CHECK CONSTRAINT [FK_Equipos_Jugadores]
GO
USE [master]
GO
ALTER DATABASE [Liga] SET  READ_WRITE 
GO