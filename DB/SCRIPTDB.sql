CREATE DATABASE Horarios
GO
USE Horarios
GO
/****** Object:  UserDefinedFunction [dbo].[SplitGuidStringToTable]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[SplitGuidStringToTable]
(
    @Input NVARCHAR(MAX)
)
RETURNS @Output TABLE ([GuidValue] UNIQUEIDENTIFIER)
AS
BEGIN
    DECLARE @Pos INT, @NextPos INT, @Value NVARCHAR(50)

    SET @Input = @Input + ',' -- Asegura procesamiento del último valor
    SET @Pos = CHARINDEX(',', @Input)

    WHILE @Pos > 0
    BEGIN
        SET @Value = LTRIM(RTRIM(LEFT(@Input, @Pos - 1)))
        IF ISDATE(@Value) = 0 -- Para evitar errores
            INSERT INTO @Output ([GuidValue]) VALUES (TRY_CAST(@Value AS UNIQUEIDENTIFIER))
        SET @Input = RIGHT(@Input, LEN(@Input) - @Pos)
        SET @Pos = CHARINDEX(',', @Input)
    END

    RETURN
END
GO
/****** Object:  Table [dbo].[Estados]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estados](
	[IdEstado] [int] NOT NULL,
	[Estado] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEstado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Horarios]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Horarios](
	[IdHorario] [uniqueidentifier] NOT NULL,
	[IdPersona] [uniqueidentifier] NOT NULL,
	[Fecha] [date] NOT NULL,
	[HoraEntrada] [datetime] NULL,
	[HoraSalida] [datetime] NULL,
	[HoraInicioAlmuerzo] [datetime] NULL,
	[HoraFinAlmuerzo] [datetime] NULL,
	[IdEstado] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdHorario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personas]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personas](
	[Id] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](100) NULL,
	[Apellido] [nvarchar](100) NULL,
	[Direccion] [nvarchar](255) NULL,
	[Telefono] [nvarchar](15) NULL,
	[Correo] [nvarchar](100) NULL,
	[Activo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] NOT NULL,
	[Tipo] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[IdPersona] [uniqueidentifier] NOT NULL,
	[Hash] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsuariosxRoles]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuariosxRoles](
	[IdPersona] [uniqueidentifier] NOT NULL,
	[IdRol] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPersona] ASC,
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Horarios] ADD  DEFAULT (newid()) FOR [IdHorario]
GO
ALTER TABLE [dbo].[Horarios] ADD  DEFAULT ((1)) FOR [IdEstado]
GO
ALTER TABLE [dbo].[Personas] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Personas] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Horarios]  WITH CHECK ADD FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Personas] ([Id])
GO
ALTER TABLE [dbo].[Horarios]  WITH CHECK ADD  CONSTRAINT [FK_Horarios_Estados] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Estados] ([IdEstado])
GO
ALTER TABLE [dbo].[Horarios] CHECK CONSTRAINT [FK_Horarios_Estados]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Personas] ([Id])
GO
ALTER TABLE [dbo].[UsuariosxRoles]  WITH CHECK ADD FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Personas] ([Id])
GO
ALTER TABLE [dbo].[UsuariosxRoles]  WITH CHECK ADD FOREIGN KEY([IdRol])
REFERENCES [dbo].[Roles] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[ActivarPersona]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[ActivarPersona]
@Id UNIQUEIDENTIFIER
AS
BEGIN 
UPDATE Personas
SET Activo=1
WHERE @Id=Id
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarHashUsuario]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarHashUsuario]
    @IdPersona UNIQUEIDENTIFIER,
    @NuevoHash NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Personas WHERE Id = @IdPersona)
    BEGIN
        UPDATE Usuarios
        SET [Hash] = @NuevoHash
        WHERE IdPersona = @IdPersona;

        SELECT 'OK' AS Resultado;
    END
    ELSE
    BEGIN
        SELECT 'ERROR: Persona no encontrada' AS Resultado;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[AddPersona]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[AddPersona]
    @Nombre VARCHAR(50),
	@Apellido VARCHAR(150),
	@Direccion VARCHAR(200),
    @Telefono NVARCHAR(9),
    @Correo NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Id UNIQUEIDENTIFIER;
    SET @Id = NEWID();

    BEGIN TRANSACTION;
    BEGIN TRY
        INSERT INTO Personas (Id, Nombre, Apellido, Direccion, Telefono, Correo)
		VALUES (@Id, @Nombre, @Apellido, @Direccion, @Telefono, @Correo);

		INSERT INTO Usuarios(IdPersona)
		VALUES (@Id);

		INSERT INTO UsuariosxRoles(IdRol, IdPersona)
		VALUES(2, @Id);

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[AddRol]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[AddRol]
    @Id int,
	@Tipo VARCHAR(13)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        INSERT INTO Roles(Id, Tipo)
		VALUES (@Id, @Tipo);

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[AddRolxUsuario]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[AddRolxUsuario]
    @Id int,
	@IdPersona UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        INSERT INTO UsuariosxRoles(IdRol, IdPersona)
		VALUES (@Id, @IdPersona);

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Agregar_Usuario_MW]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[Agregar_Usuario_MW]
    @Hash varchar(max),
	@Nombre VARCHAR(50),
	@Apellido VARCHAR(150),
	@Direccion VARCHAR(200),
	@Telefono VARCHAR(9),
	@Correo VARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @IdPersona UNIQUEIDENTIFIER;
    SET @IdPersona = NEWID();

    BEGIN TRY
        BEGIN TRANSACTION;

		INSERT INTO Personas(Id, Nombre, Apellido, Direccion, Telefono, Correo)
		VALUES(@IdPersona, @Nombre, @Apellido, @Direccion, @Telefono, @Correo);

        INSERT INTO Usuarios (IdPersona, Hash)
        VALUES (@IdPersona, @Hash);

		INSERT INTO UsuariosxRoles(IdPersona, IdRol)
        VALUES (@IdPersona, 2);

		SELECT @IdPersona;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[DeletePersona]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeletePersona]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
       
        -- Marca la persona como inactiva
        UPDATE Personas
        SET Activo = 0
        WHERE Id = @Id;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteRol]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[DeleteRol]
	@Id int
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        DELETE FROM Roles
			WHERE Id=@Id

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteRolxUsuario]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[DeleteRolxUsuario]
	@IdPersona UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        DELETE FROM UsuariosxRoles
			WHERE IdPersona=@IdPersona

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[EditPersona]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[EditPersona]
	@Id UNIQUEIDENTIFIER,
    @Nombre VARCHAR(50),
	@Apellido VARCHAR(150),
	@Direccion VARCHAR(200),
    @Telefono NVARCHAR(9),
    @Correo NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        UPDATE Personas
			SET Nombre = @Nombre,
			Apellido=@Apellido,
			Direccion=@Direccion,
			Telefono = @Telefono,
			Correo=@Correo
		WHERE @Id=Id;

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[EditRol]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[EditRol]
	@Id int,
	@Tipo VARCHAR(13)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        UPDATE Roles
			SET Tipo= @Tipo
		WHERE @Id=Id;

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[EditRolxUsuario]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[EditRolxUsuario]
    @Id int,
	@IdPersona UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        UPDATE  UsuariosxRoles
        SET IdRol= @Id
		WHERE IdPersona=@IdPersona;

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[GetEstado]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetEstado]
    @IdPersona UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    -- Obtener el estado del día actual
    SELECT 
        IdPersona,
        Fecha,
        HoraEntrada,
        HoraSalida,
        HoraInicioAlmuerzo,
        HoraFinAlmuerzo,
        IdEstado
    FROM Horarios
    WHERE IdPersona = @IdPersona
    AND Fecha = CAST(GETDATE() AS DATE);
    
    -- Si no hay registros para hoy, retornamos un mensaje de que aún no se ha iniciado la jornada
    IF NOT EXISTS (
        SELECT 1 FROM Horarios
        WHERE IdPersona = @IdPersona 
        AND Fecha = CAST(GETDATE() AS DATE)
    )
    BEGIN
        RAISERROR('No se ha registrado ningún horario para hoy. La jornada aún no ha iniciado.', 16, 1);
        RETURN;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[GetHorarios]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetHorarios]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        h.IdPersona,
        p.Nombre,
        p.Apellido,  
        h.Fecha,
        h.HoraEntrada,
        h.HoraSalida,
        h.HoraInicioAlmuerzo,
        h.HoraFinAlmuerzo,
        e.Estado AS EstadoDescripcion,
        h.IdEstado
    FROM 
        Horarios h
    JOIN 
        Personas p ON h.IdPersona = p.Id
    JOIN 
        Estados e ON h.IdEstado = e.IdEstado  
    ORDER BY 
        h.Fecha DESC, h.HoraEntrada DESC;  
END
GO
/****** Object:  StoredProcedure [dbo].[GetHorariosFiltrados]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetHorariosFiltrados]
    @FechaInicio DATE,
    @FechaFin DATE,
    @IdsPersona NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Convertir string a tabla
    DECLARE @IdsTabla TABLE (IdPersona UNIQUEIDENTIFIER)

    IF @IdsPersona IS NOT NULL AND LEN(@IdsPersona) > 0
    BEGIN
        INSERT INTO @IdsTabla (IdPersona)
        SELECT GuidValue FROM dbo.SplitGuidStringToTable(@IdsPersona)
    END

    SELECT 
        h.IdPersona,
        p.Nombre,
        p.Apellido,  
        h.Fecha,
        h.HoraEntrada,
        h.HoraSalida,
        h.HoraInicioAlmuerzo,
        h.HoraFinAlmuerzo,
        e.Estado AS EstadoDescripcion,
        h.IdEstado
    FROM 
        Horarios h
    JOIN 
        Personas p ON h.IdPersona = p.Id
    JOIN 
        Estados e ON h.IdEstado = e.IdEstado
    WHERE 
        h.Fecha BETWEEN @FechaInicio AND @FechaFin
        AND p.Activo = 1
        AND (@IdsPersona IS NULL OR h.IdPersona IN (SELECT IdPersona FROM @IdsTabla))
    ORDER BY 
        h.Fecha DESC, h.HoraEntrada DESC;
END
GO
/****** Object:  StoredProcedure [dbo].[GetHorariosPorPersona]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetHorariosPorPersona]
    @IdPersona UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        IdHorario,
        IdPersona,
        Fecha,
        HoraEntrada,
        HoraSalida,
        HoraInicioAlmuerzo,
        HoraFinAlmuerzo
    FROM 
        Horarios
    WHERE 
        IdPersona = @IdPersona
    ORDER BY 
        Fecha DESC;
END;
select * from Horarios
GO
/****** Object:  StoredProcedure [dbo].[GetPersona]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetPersona]
	@Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
        SELECT Id, Nombre, Apellido, Direccion, Telefono, Correo
		FROM Personas
		WHERE Id=@Id;
END
GO
/****** Object:  StoredProcedure [dbo].[GetPersonas]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetPersonas]
AS
BEGIN
SELECT * FROM Personas
END
GO
/****** Object:  StoredProcedure [dbo].[MarcarEntrada]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MarcarEntrada]
    @IdPersona UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar que no haya ya una marca para el día de hoy
    IF EXISTS (
        SELECT 1 FROM Horarios
        WHERE IdPersona = @IdPersona AND Fecha = CAST(GETDATE() AS DATE)
    )
    BEGIN
        RAISERROR('Ya existe una marca de entrada para hoy.', 16, 1);
        RETURN;
    END

    -- Insertar nueva fila
    INSERT INTO Horarios (IdHorario, IdPersona, Fecha, HoraEntrada, IdEstado)
    VALUES (NEWID(), @IdPersona, CAST(GETDATE() AS DATE), GETDATE(), 1);
END
GO
/****** Object:  StoredProcedure [dbo].[MarcarFinalizacionJornada]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MarcarFinalizacionJornada]
    @IdPersona UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar que ya se haya marcado el regreso del almuerzo
    IF NOT EXISTS (
        SELECT 1 FROM Horarios
        WHERE IdPersona = @IdPersona
        AND Fecha = CAST(GETDATE() AS DATE)

    )
    BEGIN
        RAISERROR('No se ha registrado el regreso del almuerzo para hoy.', 16, 1);
        RETURN;
    END

    -- Validar que la jornada no haya sido finalizada ya
    IF EXISTS (
        SELECT 1 FROM Horarios
        WHERE IdPersona = @IdPersona
        AND Fecha = CAST(GETDATE() AS DATE)
        AND HoraSalida IS NOT NULL
    )
    BEGIN
        RAISERROR('Ya se ha registrado la finalización de la jornada para hoy.', 16, 1);
        RETURN;
    END

    -- Actualizar estado y hora de salida (finalización de jornada)
    UPDATE Horarios
    SET HoraSalida = GETDATE(),
        IdEstado = 4
    WHERE IdPersona = @IdPersona
    AND Fecha = CAST(GETDATE() AS DATE);
END
GO
/****** Object:  StoredProcedure [dbo].[MarcarInicioAlmuerzo]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MarcarInicioAlmuerzo]
    @IdPersona UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar que ya haya una marca de entrada
    IF NOT EXISTS (
        SELECT 1 FROM Horarios
        WHERE IdPersona = @IdPersona 
        AND Fecha = CAST(GETDATE() AS DATE) 
        AND HoraEntrada IS NOT NULL
    )
    BEGIN
        RAISERROR('No se ha registrado una hora de entrada para hoy.', 16, 1);
        RETURN;
    END

    -- Validar que el almuerzo no haya sido marcado ya
    IF EXISTS (
        SELECT 1 FROM Horarios
        WHERE IdPersona = @IdPersona
        AND Fecha = CAST(GETDATE() AS DATE)
        AND HoraInicioAlmuerzo IS NOT NULL
    )
    BEGIN
        RAISERROR('Ya se ha marcado el inicio de almuerzo para hoy.', 16, 1);
        RETURN;
    END

    -- Actualizar estado y hora de inicio del almuerzo
    UPDATE Horarios
    SET HoraInicioAlmuerzo = GETDATE(),
        IdEstado = 2
    WHERE IdPersona = @IdPersona
    AND Fecha = CAST(GETDATE() AS DATE);
END
GO
/****** Object:  StoredProcedure [dbo].[MarcarRegresoAlmuerzo]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MarcarRegresoAlmuerzo]
    @IdPersona UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar que ya se haya marcado el inicio del almuerzo
    IF NOT EXISTS (
        SELECT 1 FROM Horarios
        WHERE IdPersona = @IdPersona
        AND Fecha = CAST(GETDATE() AS DATE)
        AND HoraInicioAlmuerzo IS NOT NULL
    )
    BEGIN
        RAISERROR('No se ha registrado el inicio de almuerzo para hoy.', 16, 1);
        RETURN;
    END

    -- Validar que el regreso del almuerzo no haya sido marcado ya
    IF EXISTS (
        SELECT 1 FROM Horarios
        WHERE IdPersona = @IdPersona
        AND Fecha = CAST(GETDATE() AS DATE)
        AND HoraFinAlmuerzo IS NOT NULL
    )
    BEGIN
        RAISERROR('Ya se ha marcado el regreso del almuerzo para hoy.', 16, 1);
        RETURN;
    END

    -- Actualizar estado y hora de regreso del almuerzo
    UPDATE Horarios
    SET HoraFinAlmuerzo = GETDATE(),
        IdEstado = 3
    WHERE IdPersona = @IdPersona
    AND Fecha = CAST(GETDATE() AS DATE);
END
GO
/****** Object:  StoredProcedure [dbo].[Obtener_RolesxUsuario]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[Obtener_RolesxUsuario]
    @Correo varchar(100)
AS
BEGIN
    SET NOCOUNT ON;

	SELECT R.Id, R.Tipo
	FROM Roles R
	INNER JOIN UsuariosxRoles UR ON UR.IdRol=R.Id
	INNER JOIN Usuarios U ON U.IdPersona= UR.IdPersona
	INNER JOIN Personas P ON P.Id=U.IdPersona
	WHERE @Correo=P.Correo;
END;
GO
/****** Object:  StoredProcedure [dbo].[Obtener_Usuario_MW]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[Obtener_Usuario_MW]
    @Correo varchar(100)
AS
BEGIN
    SET NOCOUNT ON;

	SELECT U.IdPersona, P.Correo, U.Hash
	FROM Usuarios U
	INNER JOIN Personas P ON U.IdPersona=P.Id
	WHERE @Correo=Correo;
END;
GO
/****** Object:  StoredProcedure [dbo].[RolesxUsuario]    Script Date: 28/5/2025 20:28:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create Procedure  [dbo].[RolesxUsuario]
@IdPersona UNIQUEIDENTIFIER
AS
BEGIN

SELECT R.Id, R.Tipo, RU.IdPersona
FROM UsuariosxRoles RU
INNER JOIN Roles R ON R.Id=RU.IdRol
WHERE RU.IdPersona=@IdPersona;

END
GO
ALTER DATABASE Horarios SET  READ_WRITE 
GO
Insert into Roles Values (1, 'admin')
Insert into Roles Values (2, 'trabajador')
Insert into Roles Values (3, 'recursos humanos')
DECLARE @AdminId UNIQUEIDENTIFIER = NEWID();

INSERT INTO Personas (Id, Nombre, Apellido, Direccion, Telefono, Correo)
VALUES (
    @AdminId,
    'Administrador',
    'Municipalidad',
    'TI',
    '1234-5678',
    'admin@gmail.com'
);

INSERT INTO Usuarios (IdPersona, Hash)
VALUES (
    @AdminId,
    'A1276B01964E099DF1868BF3E7519561EB7F981BA5CFD17FD6713F9251BD8789'
);

INSERT INTO UsuariosxRoles (IdRol, IdPersona)
VALUES (
    1,
    @AdminId
);

