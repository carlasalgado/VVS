
/* 
 * SQL Server Script
 *
 * This script can be executed from MS Sql Server Management Studio Express,
 * but also it is possible to use a command Line syntax:
 *
 *    > sqlcmd.exe -U [user] -P [password] -I -i SqlServerCreateTables.sql
 *
 */

 

/* 
 * Drop tables.                                                             
 * NOTE: before dropping a table (when re-executing the script), the tables 
 * having columns acting as foreign keys of the table to be dropped must be 
 * dropped first (otherwise, the corresponding checks on those tables could 
 * not be done).                                                            
 */

USE practicamad



/* Drop Table Contiene if already exists */
/* Un grupo tiene recomendaciones de eventos
   Una recomendación recomienda a varios grupos */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Contiene]') 
AND type in ('U')) DROP TABLE [Contiene]
GO

/* Drop Table Pertenece if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Pertenece]') 
AND type in ('U')) DROP TABLE [Pertenece]
GO

/* Drop Table Etiquetado if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Etiquetado]') 
AND type in ('U')) DROP TABLE [Etiquetado]
GO

/* Drop Table Recomendacion if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Recomendacion]') 
AND type in ('U')) DROP TABLE [Recomendacion]
GO

/* Drop Table Comentario if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Comentario]') 
AND type in ('U')) DROP TABLE [Comentario]
GO

/* Drop Table Evento if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Evento]') 
AND type in ('U')) DROP TABLE [Evento]
GO

/* Drop Table Etiqueta if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Etiqueta]') 
AND type in ('U')) DROP TABLE [Etiqueta]
GO

/* Drop Table Grupo if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Grupo]') 
AND type in ('U')) DROP TABLE [Grupo]
GO

/* Drop Table Usuario if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserProfile]') AND type in ('U'))
DROP TABLE [UserProfile]
GO


/*
 * Create tables.
 * UserProfile table is created. Indexes required for the 
 * most common operations are also defined.
 */

/*  UserProfile */

CREATE TABLE UserProfile (
	usrId bigint IDENTITY(1,1) NOT NULL,
	loginName varchar(30) NOT NULL,
	enPassword varchar(50) NOT NULL,
	firstName varchar(30) NOT NULL,
	lastName varchar(40) NOT NULL,
	email varchar(60) NOT NULL,
	language varchar(2) NOT NULL,
	country varchar(2) NOT NULL,

	CONSTRAINT [PK_UserProfile] PRIMARY KEY (usrId),
	CONSTRAINT [UniqueKey_Login] UNIQUE (loginName)
)


CREATE NONCLUSTERED INDEX [IX_UserProfileIndexByLoginName]
ON [UserProfile] ([loginName] ASC)

PRINT N'Table UserProfile created.'
GO


CREATE TABLE Grupo(
	idGrupo bigint IDENTITY(1,1) NOT NULL,
	nombre varchar(20) NOT NULL,
	descripcion varchar(100) NOT NULL,

    CONSTRAINT [PK_Grupo] PRIMARY KEY (idGrupo ASC)
) 

PRINT N'Table Grupo created.'
GO

CREATE TABLE Etiqueta(
	idEtiqueta bigint IDENTITY(1,1) NOT NULL,
	nombre varchar(20) NOT NULL,

    CONSTRAINT [PK_Etiqueta] PRIMARY KEY (idEtiqueta ASC)
) 

PRINT N'Table Etiqueta created.'
GO

CREATE TABLE Evento(
	idEvento bigint IDENTITY(1,1) NOT NULL,
	nombre varchar(100) NOT NULL,
	fecha datetime NOT NULL,
	reseña varchar(255) NOT NULL,

	CONSTRAINT [PK_Evento] PRIMARY KEY (idEvento ASC)
)


PRINT N'Table Evento created.'
GO

CREATE TABLE Comentario(
	idComentario bigint IDENTITY(1,1) NOT NULL,
	idEvento bigint NOT NULL,
	usrId bigint NOT NULL,
	texto varchar(100) NOT NULL,
	fecha datetime NOT NULL,

    CONSTRAINT [PK_Comentario] PRIMARY KEY (idComentario ASC),

	CONSTRAINT [FK_Evento] FOREIGN KEY(idEvento)
        REFERENCES Evento (idEvento) ON DELETE CASCADE,
	CONSTRAINT [FK_Usuario] FOREIGN KEY(usrId)
        REFERENCES UserProfile (usrId) ON DELETE CASCADE

) 

PRINT N'Table Comentario created.'
GO

CREATE TABLE Recomendacion(
	idRecomendacion bigint IDENTITY(1,1) NOT NULL,
	idEvento bigint NOT NULL,
	texto varchar(100),
	fecha datetime NOT NULL,

	CONSTRAINT [PK_Recomendado] PRIMARY KEY (idRecomendacion ASC),

	CONSTRAINT [FK_Evento_Recomendacion] FOREIGN KEY(idEvento)
        REFERENCES Evento (idEvento) ON DELETE CASCADE,
) 

PRINT N'Table Recomendacion created.'
GO

CREATE TABLE Etiquetado(
	idComentario bigint NOT NULL,
	idEtiqueta bigint NOT NULL,

	CONSTRAINT [PK_Etiquetado] PRIMARY KEY (idEtiqueta, idComentario ASC),

	CONSTRAINT [FK_Etiqueta_Etiquetado] FOREIGN KEY(idEtiqueta)
        REFERENCES Etiqueta (idEtiqueta) ON DELETE CASCADE,
	CONSTRAINT [FK_Comentario_Etiquetado] FOREIGN KEY(idComentario)
        REFERENCES Comentario (idComentario) ON DELETE CASCADE
) 

PRINT N'Table Etiquetado created.'
GO

CREATE TABLE Pertenece(
	idGrupo bigint NOT NULL,
	usrId bigint NOT NULL,

	CONSTRAINT [PK_Pertenece] PRIMARY KEY (idGrupo, usrId ASC),

	CONSTRAINT [FK_Grupo_Pertenece] FOREIGN KEY(idGrupo)
        REFERENCES Grupo (idGrupo) ON DELETE CASCADE,
	CONSTRAINT [FK_Usuario_Pertenece] FOREIGN KEY(usrId)
        REFERENCES UserProfile (usrId) ON DELETE CASCADE
) 

PRINT N'Table Pertenece created.'
GO

CREATE TABLE Contiene(
	idGrupo bigint NOT NULL,
	idRecomendacion bigint NOT NULL,

	CONSTRAINT [PK_Contiene] PRIMARY KEY (idGrupo, idRecomendacion ASC),

	CONSTRAINT [FK_Grupo_Contiene] FOREIGN KEY(idGrupo)
        REFERENCES Grupo (idGrupo) ON DELETE CASCADE,
	CONSTRAINT [FK_Recomendacion_Pertenece] FOREIGN KEY(idRecomendacion)
        REFERENCES Recomendacion (idRecomendacion) ON DELETE CASCADE
) 

PRINT N'Table Contiene created.'
GO