USE [master]
GO
/****** Object:  Database [PUNTO_DE_VENTA]    Script Date: 30/4/2022 11:14:39 ******/
CREATE DATABASE [PUNTO_DE_VENTA]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PUNTO_DE_VENTA', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PUNTO_DE_VENTA.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PUNTO_DE_VENTA_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PUNTO_DE_VENTA_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PUNTO_DE_VENTA].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET ARITHABORT OFF 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET RECOVERY FULL 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET  MULTI_USER 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PUNTO_DE_VENTA', N'ON'
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET QUERY_STORE = OFF
GO
USE [PUNTO_DE_VENTA]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_ObtenerDiferenciaDeTiempo]    Script Date: 30/4/2022 11:14:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_ObtenerDiferenciaDeTiempo](
	@CEDULA INT
)
RETURNS VARCHAR(200)
AS
BEGIN 
	DECLARE @FechCreacion DATE,
	@FechaModificacion DATE,
	@DiferenciaAnno INT,
	@DiferenciaMes INT,
	@DiferenciaDia INT,
	@RESPONSE VARCHAR(200)


	SELECT TOP 1 @FechaModificacion = [FECHA_CAMBIO] FROM [dbo].[BITACORA] WHERE CEDULA_CLIENTE = @CEDULA ORDER BY FECHA_CAMBIO DESC
	SELECT @FechCreacion = [FECHA_CREADO] FROM [dbo].[CLIENTE] WHERE CEDULA = @CEDULA 

	SELECT @DiferenciaAnno =  DATEDIFF(YEAR, @FechCreacion, @FechaModificacion);
	SELECT @DiferenciaMes =  DATEDIFF(MONTH, @FechCreacion, @FechaModificacion);
	SELECT @DiferenciaDia =  DATEDIFF(DAY, @FechCreacion, @FechaModificacion);

	SET @RESPONSE = 'La diferencia de tiempo es de '+ CONVERT(varchar(5), @DiferenciaAnno)+ ' años, ' + CONVERT(varchar(5), @DiferenciaMes) +' meses y '+ CONVERT(varchar(5), @DiferenciaDia ) + ' dias';
	RETURN @RESPONSE;

END


--SELECT dbo.fn_ObtenerDiferenciaDeTiempo(117740749)
GO
/****** Object:  Table [dbo].[BITACORA]    Script Date: 30/4/2022 11:14:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BITACORA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CEDULA_CLIENTE] [int] NOT NULL,
	[ESTADO_ACTUAL] [varchar](25) NOT NULL,
	[DESCRIPCION_CAMBIO] [varchar](200) NOT NULL,
	[FECHA_CAMBIO] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CLIENTE]    Script Date: 30/4/2022 11:14:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CLIENTE](
	[CEDULA] [int] NOT NULL,
	[NOMBRE] [varchar](50) NOT NULL,
	[APELLIDOS] [varchar](75) NOT NULL,
	[ESTADO] [varchar](25) NOT NULL,
	[TELEFONO] [int] NOT NULL,
	[EMAIL] [varchar](75) NOT NULL,
	[FECHA_CREADO] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[CEDULA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BITACORA]  WITH CHECK ADD FOREIGN KEY([CEDULA_CLIENTE])
REFERENCES [dbo].[CLIENTE] ([CEDULA])
GO
/****** Object:  StoredProcedure [dbo].[usp_CambiarEstadoCliente]    Script Date: 30/4/2022 11:14:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_CambiarEstadoCliente]
	@CEDULA INT,
	@ESTADO VARCHAR(25)

AS
BEGIN TRY
	DECLARE @DESCRIPCION_CAMBIO VARCHAR(200)
	IF @ESTADO = 'COMPRO'
		SET @DESCRIPCION_CAMBIO = 'El cliente realizo la compra'
	ELSE IF  @ESTADO = 'CANCELO'
		SET @DESCRIPCION_CAMBIO = 'El cliente cancelo la compra'
	ELSE IF @ESTADO = 'SEGUIMIENTO'
		SET @DESCRIPCION_CAMBIO = 'El cliente se encuentra en seguimiento'
	ELSE 
		SET @DESCRIPCION_CAMBIO ='Se realizó el cambio del estado del cliente a ' + @ESTADO

	UPDATE CLIENTE
	SET ESTADO = @ESTADO
	WHERE CEDULA = @CEDULA

	EXEC usp_InsertarEnBitacora @CEDULA, @ESTADO, @DESCRIPCION_CAMBIO
END TRY
BEGIN CATCH
	ROLLBACK
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[usp_CrearCliente]    Script Date: 30/4/2022 11:14:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_CrearCliente]
	@CEDULA INT,
	@NOMBRE VARCHAR(50),
	@APELLIDOS VARCHAR(75),
	@TELEFONO INT,
	@EMAIL VARCHAR(75)
AS
BEGIN TRY
	INSERT INTO CLIENTE([CEDULA], [NOMBRE], [APELLIDOS], [ESTADO], [TELEFONO], [EMAIL], [FECHA_CREADO]) VALUES(
		@CEDULA, @NOMBRE, @APELLIDOS, 'NUEVO', @TELEFONO ,@EMAIL, GETDATE()
	)
END TRY
BEGIN CATCH
	ROLLBACK;
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[usp_InsertarEnBitacora]    Script Date: 30/4/2022 11:14:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_InsertarEnBitacora]
	@CEDULA_CLIENTE INT,
	@ESTADO_ACTUAL VARCHAR(25),
	@DESCRIPCION_CAMBIO VARCHAR(200)
AS
BEGIN TRY
	INSERT INTO BITACORA ([CEDULA_CLIENTE], [ESTADO_ACTUAL], [DESCRIPCION_CAMBIO], [FECHA_CAMBIO]) VALUES(
		@CEDULA_CLIENTE, @ESTADO_ACTUAL, @DESCRIPCION_CAMBIO, GETDATE()
	)

END TRY
BEGIN CATCH
	ROLLBACK
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[usp_ModificarUsuario]    Script Date: 30/4/2022 11:14:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_ModificarUsuario]
	@CEDULA INT,
	@NOMBRE VARCHAR(50),
	@APELLIDOS VARCHAR(75),
	@TELEFONO INT,
	@EMAIL VARCHAR(75)
AS
BEGIN TRY
	DECLARE @CEDULA_UNMODIFIED INT,
	@NOMBRE_UNMODIFIED VARCHAR(50),
	@APELLIDOS_UNMODIFIED VARCHAR(75),
	@ESTADO_UNMODIFIED VARCHAR(25),
	@TELEFONO_UNMODIFIED INT,
	@EMAIL_UNMODIFIED VARCHAR(75),
	@DESCRIPCION_CAMBIO VARCHAR(200)

	SET @DESCRIPCION_CAMBIO = '';

	SELECT @CEDULA_UNMODIFIED = [CEDULA], @NOMBRE_UNMODIFIED = [NOMBRE], @APELLIDOS_UNMODIFIED = [APELLIDOS], 
		@ESTADO_UNMODIFIED = [ESTADO], @TELEFONO_UNMODIFIED = [TELEFONO], @EMAIL_UNMODIFIED = [EMAIL] FROM CLIENTE
	WHERE CEDULA = @CEDULA

	IF @NOMBRE != @NOMBRE_UNMODIFIED
		SET @DESCRIPCION_CAMBIO += 'Se actualiza el nombre del cliente, ';
	IF @APELLIDOS != @APELLIDOS_UNMODIFIED
		SET @DESCRIPCION_CAMBIO += 'Se actualizan los apellidos del cliente, ';
	IF @TELEFONO != @TELEFONO_UNMODIFIED
		SET @DESCRIPCION_CAMBIO += 'Se actualiza el teléfono del cliente, ';
	IF @EMAIL != @EMAIL_UNMODIFIED
		SET @DESCRIPCION_CAMBIO += 'Se actualiza el email del cliente, ';

	EXEC usp_InsertarEnBitacora @CEDULA, 'MODIFICADO', @DESCRIPCION_CAMBIO


	UPDATE CLIENTE
	SET NOMBRE = @NOMBRE, APELLIDOS = @APELLIDOS, ESTADO = 'MODIFICADO', TELEFONO = @TELEFONO, EMAIL = @EMAIL
	WHERE CEDULA = @CEDULA

END TRY
BEGIN CATCH
	ROLLBACK
END CATCH
GO
USE [master]
GO
ALTER DATABASE [PUNTO_DE_VENTA] SET  READ_WRITE 
GO