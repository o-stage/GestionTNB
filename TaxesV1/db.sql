--Enable the containment feature at the SQL Server
USE master
GO

sp_configure 'show advanced options', 1
GO
RECONFIGURE
GO

sp_configure 'CONTAINED DATABASE AUTHENTICATION', 1
GO
RECONFIGURE
GO

sp_configure 'show advanced options', 0
GO
RECONFIGURE
GO

--Create the database
CREATE DATABASE TestDb CONTAINMENT = PARTIAL;
go
USE TaxesV2;
--Create tables
create table Constants
(
    [Key] varchar(50) not null,
    Value float       not null,
    Date  datetime    not null,
    primary key ([Key], Date)
)
go
INSERT INTO Constants ([Key], Value, Date)
VALUES (N'TaxSureEconomie', 2, N'1800-01-01 00:00:00.000');
INSERT INTO Constants ([Key], Value, Date)
VALUES (N'TaxSureEconomie', 3, N'2022-01-01 00:00:00.000');
INSERT INTO Constants ([Key], Value, Date)
VALUES (N'TaxSureImmeuble', 8, N'1800-01-01 00:00:00.000');
INSERT INTO Constants ([Key], Value, Date)
VALUES (N'TaxSureImmeuble', 9, N'2022-01-01 00:00:00.000');
INSERT INTO Constants ([Key], Value, Date)
VALUES (N'TaxSureVilla', 8, N'1800-01-01 00:00:00.000');
INSERT INTO Constants ([Key], Value, Date)
VALUES (N'TaxSureVilla', 9, N'2022-01-01 00:00:00.000');

create table Redevable
(
    ID     varchar(50)                         not null
        primary key,
    Nom    varchar(50)                         not null,
    Prenom varchar(50)                         not null,
    Type   varchar(20)
        check ([Type] = 'PM' OR [Type] = 'PF') not null,
    Tel    varchar(50)                         not null
)
go


create table Terrain
(
    NTF                varchar(50)                       not null primary key,
    Lieu               varchar(max)                      not null,
    SuperficeBrute     float                             not null,
    SuperficeTaxable   float                             not null,
    Type               varchar(10)  default 'economique' not null
        check ([Type] = 'villa' OR [Type] = 'immeuble' OR [Type] = 'economique') not null,
    Etat               nvarchar(10) default 'NonBati'    not null
        check ([Etat] = 'Construction' OR [Etat] = 'NonBati' OR [Etat] = 'bati') not null,
    DateChangementEtat datetime                          not null,
)
go

create table Dossier
(
    NDossier    varchar(50) not null
        primary key,
    DateDossier date        not null,
    DateDebut   date        not null,
    RedevableId varchar(50) not null
        references Redevable,
    TerrainID   varchar(50) not null
        references Terrain
)
go

create table Declaration
(
    ID              int         not null
        primary key,
    NDossier        varchar(50) not null
        references Dossier,
    DateDeclaration date        not null,
    Payer           bit         not null,
    Anne            int         not null,
    NQuitance       varchar(20),
    NAvis           int,
    constraint Declaration_pk
        unique (NDossier, Anne)
)
go


