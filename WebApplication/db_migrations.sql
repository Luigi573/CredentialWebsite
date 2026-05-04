CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;
IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260427225222_FirstMigration')
BEGIN
    CREATE TABLE `AspNetRoles` (
        `Id` varchar(255) NOT NULL,
        `Name` varchar(256) NULL,
        `NormalizedName` varchar(256) NULL,
        `ConcurrencyStamp` longtext NULL,
        PRIMARY KEY (`Id`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260427225222_FirstMigration')
BEGIN
    CREATE TABLE `Centros` (
        `ID_Centro` int NOT NULL AUTO_INCREMENT,
        `nombre` longtext NOT NULL,
        `clave` varchar(100) NOT NULL,
        PRIMARY KEY (`ID_Centro`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260427225222_FirstMigration')
BEGIN
    CREATE TABLE `CiclosEscolares` (
        `ID_PeriodoEscolar` int NOT NULL AUTO_INCREMENT,
        `nombre` longtext NOT NULL,
        `fechaInicio` datetime(6) NOT NULL,
        `fechaFin` datetime(6) NOT NULL,
        `activo` tinyint(1) NOT NULL,
        PRIMARY KEY (`ID_PeriodoEscolar`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260427225222_FirstMigration')
BEGIN
    CREATE TABLE `AspNetRoleClaims` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `RoleId` varchar(255) NOT NULL,
        `ClaimType` longtext NULL,
        `ClaimValue` longtext NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260427225222_FirstMigration')
BEGIN
    CREATE TABLE `AspNetUsers` (
        `Id` varchar(255) NOT NULL,
        `nombre` varchar(100) NOT NULL,
        `ID_Centro` int NOT NULL,
        `UserName` varchar(256) NULL,
        `NormalizedUserName` varchar(256) NULL,
        `Email` varchar(256) NULL,
        `NormalizedEmail` varchar(256) NULL,
        `EmailConfirmed` tinyint(1) NOT NULL,
        `PasswordHash` longtext NULL,
        `SecurityStamp` longtext NULL,
        `ConcurrencyStamp` longtext NULL,
        `PhoneNumber` longtext NULL,
        `PhoneNumberConfirmed` tinyint(1) NOT NULL,
        `TwoFactorEnabled` tinyint(1) NOT NULL,
        `LockoutEnd` datetime NULL,
        `LockoutEnabled` tinyint(1) NOT NULL,
        `AccessFailedCount` int NOT NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_AspNetUsers_Centros_ID_Centro` FOREIGN KEY (`ID_Centro`) REFERENCES `Centros` (`ID_Centro`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260427225222_FirstMigration')
BEGIN
    CREATE TABLE `Alumnos` (
        `ID_Alumno` int NOT NULL AUTO_INCREMENT,
        `ID_Centro` int NOT NULL,
        `nombres` NVARCHAR(50) NOT NULL,
        `apellidos` NVARCHAR(50) NOT NULL,
        `curp` varchar(18) NOT NULL,
        `semestre` int NOT NULL,
        `activo` tinyint(1) NOT NULL,
        `nss` varchar(11) NULL,
        `tipoSangre` longtext NULL,
        `tutor` longtext NULL,
        `telefonoTutor` longtext NULL,
        `imagen` longtext NULL,
        `ID_PeriodoEscolar` int NOT NULL,
        PRIMARY KEY (`ID_Alumno`),
        CONSTRAINT `FK_Alumnos_Centros_ID_Centro` FOREIGN KEY (`ID_Centro`) REFERENCES `Centros` (`ID_Centro`) ON DELETE CASCADE,
        CONSTRAINT `FK_Alumnos_CiclosEscolares_ID_PeriodoEscolar` FOREIGN KEY (`ID_PeriodoEscolar`) REFERENCES `CiclosEscolares` (`ID_PeriodoEscolar`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260427225222_FirstMigration')
BEGIN
    CREATE TABLE `AspNetUserClaims` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `UserId` varchar(255) NOT NULL,
        `ClaimType` longtext NULL,
        `ClaimValue` longtext NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260427225222_FirstMigration')
BEGIN
    CREATE TABLE `AspNetUserLogins` (
        `LoginProvider` varchar(128) NOT NULL,
        `ProviderKey` varchar(128) NOT NULL,
        `ProviderDisplayName` longtext NULL,
        `UserId` varchar(255) NOT NULL,
        PRIMARY KEY (`LoginProvider`, `ProviderKey`),
        CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260427225222_FirstMigration')
BEGIN
    CREATE TABLE `AspNetUserRoles` (
        `UserId` varchar(255) NOT NULL,
        `RoleId` varchar(255) NOT NULL,
        PRIMARY KEY (`UserId`, `RoleId`),
        CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260427225222_FirstMigration')
BEGIN
    CREATE TABLE `AspNetUserTokens` (
        `UserId` varchar(255) NOT NULL,
        `LoginProvider` varchar(128) NOT NULL,
        `Name` varchar(128) NOT NULL,
        `Value` longtext NULL,
        PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
        CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260427225222_FirstMigration')
BEGIN
    INSERT INTO `Centros` (`ID_Centro`, `clave`, `nombre`)
    VALUES (1, '30ETH0224D', 'Telebachillerato Coacotla');
    SELECT ROW_COUNT();

    INSERT INTO `Centros` (`ID_Centro`, `clave`, `nombre`)
    VALUES (2, '30ETH0206O', 'Telebachillerato Nopalapan');
    SELECT ROW_COUNT();

    INSERT INTO `Centros` (`ID_Centro`, `clave`, `nombre`)
    VALUES (3, '30ETH0472L', 'Telebachillerato Lealtad de Muñoz');
    SELECT ROW_COUNT();

    INSERT INTO `Centros` (`ID_Centro`, `clave`, `nombre`)
    VALUES (4, '30ETH0134L', 'Telebachillerato Isla');
    SELECT ROW_COUNT();

END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260427225222_FirstMigration')
BEGIN
    INSERT INTO `CiclosEscolares` (`ID_PeriodoEscolar`, `fechaFin`, `activo`, `nombre`, `fechaInicio`)
    VALUES (1, '2025-06-30 00:00:00.000000', TRUE, '2024-2025', '2024-09-01 00:00:00.000000');
    SELECT ROW_COUNT();

END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260427225222_FirstMigration')
BEGIN
    CREATE INDEX `IX_Alumnos_ID_Centro` ON `Alumnos` (`ID_Centro`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260427225222_FirstMigration')
BEGIN
    CREATE INDEX `IX_Alumnos_ID_PeriodoEscolar` ON `Alumnos` (`ID_PeriodoEscolar`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260427225222_FirstMigration')
BEGIN
    CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON `AspNetRoleClaims` (`RoleId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260427225222_FirstMigration')
BEGIN
    CREATE UNIQUE INDEX `RoleNameIndex` ON `AspNetRoles` (`NormalizedName`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260427225222_FirstMigration')
BEGIN
    CREATE INDEX `IX_AspNetUserClaims_UserId` ON `AspNetUserClaims` (`UserId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260427225222_FirstMigration')
BEGIN
    CREATE INDEX `IX_AspNetUserLogins_UserId` ON `AspNetUserLogins` (`UserId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260427225222_FirstMigration')
BEGIN
    CREATE INDEX `IX_AspNetUserRoles_RoleId` ON `AspNetUserRoles` (`RoleId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260427225222_FirstMigration')
BEGIN
    CREATE INDEX `EmailIndex` ON `AspNetUsers` (`NormalizedEmail`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260427225222_FirstMigration')
BEGIN
    CREATE INDEX `IX_AspNetUsers_ID_Centro` ON `AspNetUsers` (`ID_Centro`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260427225222_FirstMigration')
BEGIN
    CREATE UNIQUE INDEX `UserNameIndex` ON `AspNetUsers` (`NormalizedUserName`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260427225222_FirstMigration')
BEGIN
    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20260427225222_FirstMigration', '10.0.7');
END;

COMMIT;

