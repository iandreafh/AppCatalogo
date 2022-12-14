USE [master]
GO
/****** Object:  Database [Catalogo]    Script Date: 31/10/2022 14:34:02 ******/
CREATE DATABASE [Catalogo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Catalogo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Catalogo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Catalogo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Catalogo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Catalogo] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Catalogo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Catalogo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Catalogo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Catalogo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Catalogo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Catalogo] SET ARITHABORT OFF 
GO
ALTER DATABASE [Catalogo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Catalogo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Catalogo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Catalogo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Catalogo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Catalogo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Catalogo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Catalogo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Catalogo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Catalogo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Catalogo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Catalogo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Catalogo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Catalogo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Catalogo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Catalogo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Catalogo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Catalogo] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Catalogo] SET  MULTI_USER 
GO
ALTER DATABASE [Catalogo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Catalogo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Catalogo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Catalogo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Catalogo] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Catalogo] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Catalogo] SET QUERY_STORE = OFF
GO
USE [Catalogo]
GO
/****** Object:  Table [dbo].[Peliculas]    Script Date: 31/10/2022 14:34:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Peliculas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [nvarchar](50) NOT NULL,
	[Estreno] [datetime] NOT NULL,
	[Genero] [nvarchar](30) NULL,
	[Valoracion] [int] NULL
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [Catalogo] SET  READ_WRITE 
GO
