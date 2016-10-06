-- Indexes for primary keys have been explicitly created.

-- ---------- Table for validation queries from the connection pool. ----------

DROP TABLE PingTable;
CREATE TABLE PingTable (foo CHAR(1));

DROP TABLE Apuesta;
DROP TABLE Opcion;
DROP TABLE TipoApuesta;
DROP TABLE Evento;
DROP TABLE Categoria;
DROP TABLE Usuario;
-- ------------------------------ UserProfile ----------------------------------

CREATE TABLE Usuario (
	idUsuario BIGINT NOT NULL AUTO_INCREMENT,
    login VARCHAR(30) NOT NULL,
    contrasena VARCHAR(30) NOT NULL, 
    nombre VARCHAR(30) NOT NULL,
    apellido VARCHAR(40) NOT NULL, 
    email VARCHAR(30) NOT NULL,
    CONSTRAINT Usuario_PK PRIMARY KEY (idUsuario),
    CONSTRAINT LoginNameUniqueKey UNIQUE (login)
) ENGINE = InnoDB;

CREATE INDEX IndexLoginUsuario ON Usuario (login);

CREATE TABLE Categoria(
	idCategoria BIGINT NOT NULL AUTO_INCREMENT,
	nombre VARCHAR(100) NOT NULL,
	CONSTRAINT Categoria_PK PRIMARY KEY (idCategoria)
)ENGINE = InnoDB;

CREATE TABLE Evento(
	idEvento BIGINT NOT NULL AUTO_INCREMENT,
	nombre VARCHAR(100) NOT NULL,
	fecha TIMESTAMP NOT NULL,
	idCategoria BIGINT NOT NULL,
	CONSTRAINT Evento_PK PRIMARY KEY (idEvento),
	CONSTRAINT CategoriaFK FOREIGN KEY(idCategoria)
        REFERENCES Categoria (idCategoria)
)ENGINE = InnoDB;

CREATE TABLE TipoApuesta(
	idTipoApuesta BIGINT NOT NULL AUTO_INCREMENT,
	pregunta VARCHAR(100) NOT NULL,
	idEvento BIGINT NOT NULL,
	CONSTRAINT TiopApuesta_PK PRIMARY KEY (idTipoApuesta),
	CONSTRAINT EventoFK FOREIGN KEY(idEvento)
        REFERENCES Evento (idEvento)
)ENGINE = InnoDB;

CREATE TABLE Opcion(
	idOpcion BIGINT NOT NULL AUTO_INCREMENT,
	opcion VARCHAR(30) NOT NULL,
	cuota DOUBLE NOT NULL,
	ganadora BOOLEAN,
	idTipoApuesta BIGINT,
	CONSTRAINT Opcion_PK PRIMARY KEY (idOpcion),
	CONSTRAINT TipoApuestaFK FOREIGN KEY(idTipoApuesta)
        REFERENCES TipoApuesta (idTipoApuesta)
) ENGINE = InnoDB;

CREATE TABLE Apuesta(
	idApuesta BIGINT NOT NULL AUTO_INCREMENT,
	fecha TIMESTAMP NOT NULL,
	importe DOUBLE NOT NULL,
	idUsuario BIGINT NOT NULL,
	idOpcion BIGINT NOT NULL,
	CONSTRAINT Apuesta_PK PRIMARY KEY (idApuesta),
	CONSTRAINT UsuarioLoginFK FOREIGN KEY(idUsuario)
        REFERENCES Usuario (idUsuario),
    CONSTRAINT OpcionFK FOREIGN KEY(idOpcion)
        REFERENCES Opcion (idOpcion)
)ENGINE = InnoDB;


