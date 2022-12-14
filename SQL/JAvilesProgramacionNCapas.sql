USE [master]
GO
/****** Object:  Database [JAvilesProgramacionNCapas]    Script Date: 12/6/2022 1:13:11 PM ******/
CREATE DATABASE [JAvilesProgramacionNCapas]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'JAvilesProgramacionNCapas', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\JAvilesProgramacionNCapas.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'JAvilesProgramacionNCapas_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\JAvilesProgramacionNCapas_log.ldf' , SIZE = 2112KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [JAvilesProgramacionNCapas].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET ARITHABORT OFF 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET  ENABLE_BROKER 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET RECOVERY FULL 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET  MULTI_USER 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET DB_CHAINING OFF 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'JAvilesProgramacionNCapas', N'ON'
GO
USE [JAvilesProgramacionNCapas]
GO
/****** Object:  StoredProcedure [dbo].[AreaGetAll]    Script Date: 12/6/2022 1:13:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[AreaGetAll]

AS Select IdArea, Nombre from Area
GO
/****** Object:  StoredProcedure [dbo].[ColoniaGetAll]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ColoniaGetAll]
AS Select IdColonia, Nombre, IdMunicipio from Colonia


GO
/****** Object:  StoredProcedure [dbo].[ColoniaGetByIdMunicipio]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[ColoniaGetByIdMunicipio]

@IdMunicipio int
as select IdColonia, Colonia.Nombre,CodigoPostal,IdMunicipio from Colonia 

where Colonia.IdMunicipio =@IdMunicipio
GO
/****** Object:  StoredProcedure [dbo].[DepartamentoAdd]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[DepartamentoAdd](
@Nombre varchar(50),
@IdArea int)

as insert into
Departamento(Nombre,IdArea)
values
(@Nombre,@IdArea)
GO
/****** Object:  StoredProcedure [dbo].[DepartamentoDelete]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[DepartamentoDelete]
@IdDepartamento int 
As
Delete From Departamento where IdDepartamento = @IdDepartamento
GO
/****** Object:  StoredProcedure [dbo].[DepartamentoGetAll]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DepartamentoGetAll]
as select IdDepartamento, Departamento.Nombre, Departamento.IdArea,
 Area.Nombre as 'NombreArea' from Departamento

inner join Area on Departamento.IdArea =Area.IdArea
GO
/****** Object:  StoredProcedure [dbo].[DepartamentoGetByArea]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DepartamentoGetByArea]

@IdArea int 
as select Departamento.IdDepartamento,Departamento.Nombre,Departamento.IdArea,
Area.Nombre as 'NombreArea' from Departamento

inner join Area on Departamento.IdArea = @IdArea

GO
/****** Object:  StoredProcedure [dbo].[DepartamentoGetById]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[DepartamentoGetById]
@IdDepartamento int
as select
IdDepartamento, Departamento.Nombre, Departamento.IdArea,
 Area.Nombre as 'NombreArea' from Departamento

inner join Area on Departamento.IdArea =Area.IdArea

where Departamento.IdDepartamento = @IdDepartamento
GO
/****** Object:  StoredProcedure [dbo].[DepartamentoUpdate]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[DepartamentoUpdate](
@IdDepartamento int,
@Nombre varchar(50),
@IdArea int)

as update Departamento
set 
Nombre = @Nombre,
IdArea =@IdArea

where IdDepartamento = @IdDepartamento
GO
/****** Object:  StoredProcedure [dbo].[EstadoGetByIdPais]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[EstadoGetByIdPais]
@IdPais int
as select IdEstado, Estado.Nombre,Estado.IdPais from Estado 

where Estado.IdPais =@IdPais
GO
/****** Object:  StoredProcedure [dbo].[MunicipioGetAll]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[MunicipioGetAll]
AS Select IdMunicipio, Nombre, IdEstado from Municipio
GO
/****** Object:  StoredProcedure [dbo].[MunicipioGetByIdEstado]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[MunicipioGetByIdEstado]

@IdEstado int
as select IdMunicipio, Municipio.Nombre,Municipio.IdEstado from Municipio 

where Municipio.IdEstado =@IdEstado
GO
/****** Object:  StoredProcedure [dbo].[PaisGetAll]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[PaisGetAll]
AS Select IdPais, Nombre from pais
GO
/****** Object:  StoredProcedure [dbo].[ProductoAdd]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ProductoAdd] 
(@Nombre varchar (200),
@PrecioUnitario DECIMAL(18,2),
@Stock int,
@IdProovedor int,
@IdDepartamento int,
@Descripcion varchar(500),
@Imagen varchar(max),
@NombreD varchar(20))
As
Insert into Producto (Nombre,PrecioUnitario,Stock,IdProovedor,IdDepartamento,Descripcion,Imagen)
values (@Nombre, @PrecioUnitario, @Stock,@IdProovedor,@IdDepartamento,@Descripcion,@Imagen)


GO
/****** Object:  StoredProcedure [dbo].[ProductoDelete]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[ProductoDelete]
	@IdProducto int

	As

	Delete From Producto where IdProducto=@IdProducto;
GO
/****** Object:  StoredProcedure [dbo].[ProductoGetAll]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ProductoGetAll] 
@Nombre varchar(50),
@IdProovedor INT

AS 
IF(@IdProovedor >0)
begin
select 
Producto.IdProducto,
Producto.Nombre,
PrecioUnitario,
Producto.Stock,
Proovedor.IdProovedor, 
Proovedor.Nombre as'NombreProovedor', 
Departamento.IdDepartamento, 
Departamento.Nombre as 'NombreDepartamento', 
Producto.Descripcion, 
Area.IdArea,
Area.Nombre as 'NombreArea',
Producto.Imagen


from Producto

Inner join Proovedor on Producto.IdProovedor = Proovedor.IdProovedor

inner join Departamento on Producto.IdDepartamento = Departamento.IdDepartamento

inner join Area on Departamento.IdArea =Area.IdArea

where Producto.Nombre Like '%'+@Nombre +'%' and Producto.IdProovedor = @IdProovedor
end
else
begin

select 
Producto.IdProducto,
Producto.Nombre,
PrecioUnitario,
Producto.Stock,
Proovedor.IdProovedor, 
Proovedor.Nombre as'NombreProovedor', 
Departamento.IdDepartamento, 
Departamento.Nombre as 'NombreDepartamento', 
Producto.Descripcion, 
Area.IdArea,
Area.Nombre as 'NombreArea',
Producto.Imagen


from Producto

Inner join Proovedor on Producto.IdProovedor = Proovedor.IdProovedor

inner join Departamento on Producto.IdDepartamento = Departamento.IdDepartamento

inner join Area on Departamento.IdArea =Area.IdArea

where Producto.Nombre Like '%'+@Nombre +'%' 
end
GO
/****** Object:  StoredProcedure [dbo].[ProductoGetById]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ProductoGetById] @IdProducto int
As Select IdProducto,
Producto.Nombre,
PrecioUnitario,
Stock,
Proovedor.IdProovedor,
Proovedor.Nombre as 'NombreProovedor',
Departamento.IdDepartamento, 
Departamento.Nombre as 'NombreDepartamento',
Descripcion,
Area.IdArea,
Area.Nombre as 'NombreArea',
Producto.Imagen


from Producto 

Inner join Proovedor on Producto.IdProovedor = Proovedor.IdProovedor

inner join Departamento on Producto.IdDepartamento = Departamento.IdDepartamento

inner join Area on Departamento.IdArea =Area.IdArea

where Producto.IdProducto = @IdProducto
GO
/****** Object:  StoredProcedure [dbo].[ProductoUpdate]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ProductoUpdate]
	(@IdProducto int,
	@Nombre varchar(200),
	@PrecioUnitario Decimal(18,2),
	@stock int,
	@IdProovedor int,
	@IdDepartamento int,
	@Descripcion varchar(500),
	@Imagen varchar(max))

	As
	update Producto 
	Set Nombre=@Nombre,
		PrecioUnitario=@PrecioUnitario,
		Stock=@stock,
		IdProovedor=@IdProovedor,
		IdDepartamento=@IdDepartamento,
		Descripcion=@Descripcion,
		Imagen=@Imagen

		where IdProducto=@IdProducto
GO
/****** Object:  StoredProcedure [dbo].[ProveedorGetAll]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[ProveedorGetAll]
as select IdProovedor, Telefono, Nombre from Proovedor
GO
/****** Object:  StoredProcedure [dbo].[RolGetAll]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RolGetAll]
AS Select IdRol, Nombre from Rol
GO
/****** Object:  StoredProcedure [dbo].[UsuarioAdd]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioAdd]
(
@Nombre varchar (50),
@ApellidoPaterno varchar(50),
@ApellidoMaterno varchar(50),
@FechaDeNacimiento VARCHAR(15),
@Sexo char(2),
@UserName varchar (254),
@Email varchar (254),
@Password varchar(50),
@Telefono varchar(50),
@Celular varchar(20),
@CURP varchar(50),
@IdRol int,
@Imagen varchar(max),
@Calle varchar(50),
@NumeroInterior varchar(20),
@NumeroExterior varchar(20),
@IdColonia int)


AS

INSERT INTO [Usuario]
           (
           Nombre
		   ,ApellidoPaterno
           ,ApellidoMaterno
           ,FechaDeNacimiento
           ,Sexo
		   ,UserName
		   ,Email
		   ,Password
           ,Telefono
           ,Celular
           ,CURP
		   ,IdRol
		   ,Imagen
		   ,Status
		   )

VALUES

(
@Nombre,@ApellidoPaterno,@ApellidoMaterno,CONVERT(DATE,@FechaDeNacimiento,105),@Sexo,@UserName,@Email,@Password,@Telefono,@Celular,@CURP,@IdRol,@Imagen,1);

Insert into Direccion(Calle,NumeroInterior,NumeroExterior,IdColonia,IdUsuario)
Values(@Calle,
@NumeroInterior,
@NumeroExterior,
@IdColonia,
@@IDENTITY)
GO
/****** Object:  StoredProcedure [dbo].[UsuarioChangeStatus]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[UsuarioChangeStatus]
@IdUsuario int,
@Satatus bit
as 
update Usuario set status = @Satatus
where IdUsuario = @IdUsuario
GO
/****** Object:  StoredProcedure [dbo].[UsuarioDelete]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioDelete]
@IdUsuario int
AS
delete from Direccion
where Direccion.IdUsuario = @IdUsuario

DELETE FROM Usuario WHERE IdUsuario = @IdUsuario;


GO
/****** Object:  StoredProcedure [dbo].[UsuarioGetAll]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[UsuarioGetAll]
@Nombre varchar(50),
@ApellidoPaterno varchar(50),
@IdRol INT
As
If(@IdRol > 0)
begin
	  Select  usuario.IdUsuario,
	usuario.Nombre,
	usuario.ApellidoPaterno,
	usuario.ApellidoMaterno,
	usuario.FechaDeNacimiento,
	usuario.Sexo,
	usuario.UserName,
	usuario.Email,
	usuario.[Password],
	usuario.Telefono,
	usuario.Celular,
	usuario.CURP,
	Usuario.IdRol,
	Rol.Nombre as 'NombreRol',
	Usuario.Imagen,
	Usuario.Status,
	Direccion.IdDireccion,
	Direccion.Calle,
	Direccion.NumeroInterior,
	Direccion.NumeroExterior,
	Colonia.IdColonia,
	Colonia.Nombre as 'NombreColonia',
	Colonia.CodigoPostal,
	Municipio.IdMunicipio,
	Municipio.Nombre as 'NombreMunicipio',
	Estado.IdEstado,
	Estado.Nombre as 'NombreEstado',
	pais.IdPais,
	pais.Nombre as 'NombrePais'

	From Usuario 
	INNER JOIN Rol ON Usuario.IdRol = Rol.IdRol
	INNER JOIN Direccion ON Usuario.IdUsuario = Direccion.IdUsuario
	INNER JOIN Colonia ON Direccion.IdColonia = Colonia.IdColonia
	INNER JOIN Municipio ON Colonia.IdMunicipio = Municipio.IdMunicipio
	INNER JOIN Estado ON Municipio.IdEstado = Estado.IdEstado
	INNER JOIN pais ON Estado.IdPais = pais.IdPais

	where Usuario.Nombre Like '%' + @Nombre + '%' AND ApellidoPaterno Like '%' + @ApellidoPaterno + '%' And Usuario.IdRol = @IdRol
	end
	else
	begin 

	Select  usuario.IdUsuario,
	usuario.Nombre,
	usuario.ApellidoPaterno,
	usuario.ApellidoMaterno,
	usuario.FechaDeNacimiento,
	usuario.Sexo,
	usuario.UserName,
	usuario.Email,
	usuario.[Password],
	usuario.Telefono,
	usuario.Celular,
	usuario.CURP,
	Usuario.IdRol,
	Rol.Nombre as 'NombreRol',
	Usuario.Imagen,
	Usuario.Status,
	Direccion.IdDireccion,
	Direccion.Calle,
	Direccion.NumeroInterior,
	Direccion.NumeroExterior,
	Colonia.IdColonia,
	Colonia.Nombre as 'NombreColonia',
	Colonia.CodigoPostal,
	Municipio.IdMunicipio,
	Municipio.Nombre as 'NombreMunicipio',
	Estado.IdEstado,
	Estado.Nombre as 'NombreEstado',
	pais.IdPais,
	pais.Nombre as 'NombrePais'

	From Usuario 
	INNER JOIN Rol ON Usuario.IdRol = Rol.IdRol
	INNER JOIN Direccion ON Usuario.IdUsuario = Direccion.IdUsuario
	INNER JOIN Colonia ON Direccion.IdColonia = Colonia.IdColonia
	INNER JOIN Municipio ON Colonia.IdMunicipio = Municipio.IdMunicipio
	INNER JOIN Estado ON Municipio.IdEstado = Estado.IdEstado
	INNER JOIN pais ON Estado.IdPais = pais.IdPais

	where Usuario.Nombre Like '%' + @Nombre + '%' AND ApellidoPaterno Like '%' + @ApellidoPaterno + '%'
	end

GO
/****** Object:  StoredProcedure [dbo].[UsuarioGetById]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[UsuarioGetById]
	@IdUsuario int
	As
    Select  usuario.IdUsuario,usuario.Nombre,ApellidoPaterno,ApellidoMaterno,FechaDeNacimiento,Sexo,UserName,Email,[Password],Telefono,Celular,CURP,Usuario.IdRol, 
	Rol.Nombre as 'NombreRol',Usuario.Imagen,Usuario.Status,
	
	Direccion.IdDireccion,
	Direccion.Calle,
	Direccion.NumeroInterior,
	Direccion.NumeroExterior,
	Direccion.IdColonia,
	Colonia.Nombre as 'NombreColonia',
	Colonia.CodigoPostal,
	Colonia.IdMunicipio,
	Municipio.Nombre as 'NombreMunicipio',
	Municipio.IdEstado,
	Estado.Nombre as 'NombreEstado',
	Estado.IdPais,
	pais.Nombre as 'NombrePais'

	From Usuario 

	Inner join Rol on Usuario.IdRol = Rol.IdRol
	inner join Direccion on Usuario.IdUsuario = Direccion.IdUsuario
	inner join Colonia on Direccion.IdColonia = Colonia.IdColonia
	inner join Municipio on Colonia.IdMunicipio = Municipio.IdMunicipio
	inner join Estado on Municipio.IdEstado = Estado.IdEstado
	inner join pais on Estado.IdPais = pais.IdPais

	where Usuario.IdUsuario =@IdUsuario
GO
/****** Object:  StoredProcedure [dbo].[UsuarioUpdate]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[UsuarioUpdate]

(@IdUsuario int,
@Nombre varchar (50),
@ApellidoPaterno varchar(50),
@ApellidoMaterno varchar(50),
@FechaDeNacimiento varchar(15),
@Sexo char(2),
@UserName varchar (254),
@Email varchar (254),
@Password varchar(50),
@Telefono varchar(50),
@Celular varchar(20),
@CURP varchar(50),
@IdRol int,
@Imagen varchar(max),
@Calle varchar(50),
@NumeroInterior varchar(20),
@NumeroExterior varchar(20),
@IdColonia int)
As
update Usuario
	SET 
		Nombre =@Nombre,
		ApellidoPaterno =@ApellidoPaterno,
		ApellidoMaterno =@ApellidoMaterno,
		FechaDeNacimiento=CONVERT(DATE,@FechaDeNacimiento,105),
		Sexo=@Sexo,
		UserName=@UserName,
		Email=@Email,
		Password=@Password,
		Telefono=@Telefono,
		Celular=@Celular,
		CURP=@CURP,
		IdRol=@IdRol,
		Imagen = @Imagen
		where Usuario.IdUsuario=@IdUsuario

update Direccion
	set 
	Calle = @Calle,
	NumeroInterior = @NumeroInterior,
	NumeroExterior = @NumeroExterior,
	IdColonia = @IdColonia
	where Direccion.IdUsuario = @IdUsuario

GO
/****** Object:  Table [dbo].[Area]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Area](
	[IdArea] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdArea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Colonia]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Colonia](
	[Nombre] [varchar](50) NOT NULL,
	[CodigoPostal] [varchar](50) NOT NULL,
	[IdMunicipio] [int] NULL,
	[IdColonia] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdColonia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Departamento]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Departamento](
	[IdDepartamento] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](20) NOT NULL,
	[IdArea] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDepartamento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Direccion]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Direccion](
	[IdDireccion] [int] IDENTITY(1,1) NOT NULL,
	[Calle] [varchar](50) NOT NULL,
	[NumeroInterior] [varchar](50) NOT NULL,
	[NumeroExterior] [varchar](50) NOT NULL,
	[IdColonia] [int] NULL,
	[IdUsuario] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDireccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Estado]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Estado](
	[IdEstado] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[IdPais] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEstado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Municipio]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Municipio](
	[IdMunicipio] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[IdEstado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdMunicipio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pais]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pais](
	[IdPais] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPais] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Producto](
	[IdProducto] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[PrecioUnitario] [decimal](18, 2) NOT NULL,
	[Stock] [int] NOT NULL,
	[IdProovedor] [int] NULL,
	[IdDepartamento] [int] NULL,
	[Descripcion] [varchar](500) NULL,
	[Imagen] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Proovedor]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Proovedor](
	[IdProovedor] [int] IDENTITY(1,1) NOT NULL,
	[Telefono] [varchar](20) NOT NULL,
	[Nombre] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProovedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Rol](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](20) NULL,
 CONSTRAINT [PK__Rol__2A49584CD66D381D] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 12/6/2022 1:13:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[ApellidoPaterno] [varchar](50) NOT NULL,
	[ApellidoMaterno] [varchar](50) NOT NULL,
	[FechaDeNacimiento] [date] NOT NULL,
	[Sexo] [char](1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Email] [varchar](254) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Telefono] [varchar](20) NOT NULL,
	[Celular] [varchar](20) NULL,
	[CURP] [varchar](50) NOT NULL,
	[IdRol] [int] NULL,
	[Imagen] [varchar](max) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK__Usuario__5B65BF97E4A96AFC] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Usuario__C9F284563F55EA18] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Colonia]  WITH CHECK ADD FOREIGN KEY([IdMunicipio])
REFERENCES [dbo].[Municipio] ([IdMunicipio])
GO
ALTER TABLE [dbo].[Departamento]  WITH CHECK ADD FOREIGN KEY([IdArea])
REFERENCES [dbo].[Area] ([IdArea])
GO
ALTER TABLE [dbo].[Direccion]  WITH CHECK ADD FOREIGN KEY([IdColonia])
REFERENCES [dbo].[Colonia] ([IdColonia])
GO
ALTER TABLE [dbo].[Direccion]  WITH CHECK ADD  CONSTRAINT [FK__Direccion__IdUsu__6EF57B66] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Direccion] CHECK CONSTRAINT [FK__Direccion__IdUsu__6EF57B66]
GO
ALTER TABLE [dbo].[Estado]  WITH CHECK ADD FOREIGN KEY([IdPais])
REFERENCES [dbo].[pais] ([IdPais])
GO
ALTER TABLE [dbo].[Municipio]  WITH CHECK ADD FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD FOREIGN KEY([IdDepartamento])
REFERENCES [dbo].[Departamento] ([IdDepartamento])
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD FOREIGN KEY([IdProovedor])
REFERENCES [dbo].[Proovedor] ([IdProovedor])
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK__Usuario__IdRol__4316F928] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK__Usuario__IdRol__4316F928]
GO
USE [master]
GO
ALTER DATABASE [JAvilesProgramacionNCapas] SET  READ_WRITE 
GO
