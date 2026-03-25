DROP DATABASE credenciales;
CREATE DATABASE credenciales;
USE credenciales;

CREATE TABLE Maestros(
    ID_Centro INT,
    nombre NVARCHAR(100) NOT NULL,
    imagen TEXT
);

CREATE TABLE Alumnos(
	ID_Alumno INT NOT NULL AUTO_INCREMENT,
    PRIMARY KEY(ID_Alumno),
    ID_Centro INT NOT NULL,
    nombres NVARCHAR(50) NOT NULL,
    apellidos NVARCHAR(50) NOT NULL,
    curp VARCHAR(18) NOT NULL,
    periodoEscolar NVARCHAR(9) NOT NULL,
    semestre TINYINT NOT NULL,
    activo BOOLEAN DEFAULT TRUE,
    nss VARCHAR(11),
    tipoSangre VARCHAR(3),
    tutor NVARCHAR(100) ,
    telefonoTutor VARCHAR(12),
    imagen TEXT
);

CREATE TABLE Centros(
	ID_Centro INT AUTO_INCREMENT,
    PRIMARY KEY(ID_Centro),
    nombre NVARCHAR(100) NOT NULL,
    clave VARCHAR(10)
);

ALTER TABLE Maestros ADD CONSTRAINT FK_IDCentro_Maestros FOREIGN KEY(ID_Centro) REFERENCES Centros(ID_Centro) ON DELETE CASCADE;
ALTER TABLE Alumnos ADD CONSTRAINT FK_IDCentro_Alumnos FOREIGN KEY(ID_Centro) REFERENCES Centros(ID_Centro) ON DELETE CASCADE;

INSERT INTO Centros(ID_Centro, nombre, clave) VALUES(1, "Telebachillerato Coacotla", "30ETH0224D");
INSERT INTO Centros(ID_Centro, nombre, clave) VALUES(2, "Telebachillerato Nopalapan", "30ETH0204Q");
INSERT INTO Centros(ID_Centro, nombre, clave) VALUES(3, "Telebachillerato Lealtad de Muñoz", "30ETH0204Q");