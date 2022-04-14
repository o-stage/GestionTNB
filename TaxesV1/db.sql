create table Constants
(
    [Key] varchar(50) not null,
    Value float       not null,
    Date  datetime    not null,
    primary key ([Key], Date)
)
go
INSERT INTO Constants ([Key], Value, Date) VALUES (N'TaxSureEconomie', 2, N'1800-01-01 00:00:00.000');
INSERT INTO Constants ([Key], Value, Date) VALUES (N'TaxSureEconomie', 3, N'2022-01-01 00:00:00.000');
INSERT INTO Constants ([Key], Value, Date) VALUES (N'TaxSureImmeuble', 8, N'1800-01-01 00:00:00.000');
INSERT INTO Constants ([Key], Value, Date) VALUES (N'TaxSureImmeuble', 9, N'2022-01-01 00:00:00.000');
INSERT INTO Constants ([Key], Value, Date) VALUES (N'TaxSureVilla', 8, N'1800-01-01 00:00:00.000');
INSERT INTO Constants ([Key], Value, Date) VALUES (N'TaxSureVilla', 9, N'2022-01-01 00:00:00.000');

create table users
(
    CIN       varchar(50) not null
        primary key,
    USER_NAME varchar(50),
    PASSWORD  varchar(50),
    ROLE      varchar(20)
)
go

create table Redevable
(
    ID     varchar(50) not null
        primary key,
    Nom    varchar(50),
    Prenom varchar(50),
    Type   varchar(20)
        check ([Type] = 'PM' OR [Type] = 'PF'),
    Tel varchar(50)
)
go


create table Terrain
(
    NTF                varchar(50)                       not null
        primary key,
    Lieu               varchar(max),
    SuperficeBrute     float,
    SuperficeTaxable   float,
    Type               varchar(10)  default 'economique' not null
        check ([Type] = 'villa' OR [Type] = 'immeuble' OR [Type] = 'economique'),
    Etat               nvarchar(10) default 'NonBati'    not null
        check ([Etat] = 'Construction' OR [Etat] = 'NonBati' OR [Etat] = 'bati'),
    DateChangementEtat datetime
)
go

create table Dossier
(
    NDossier    varchar(50) not null
        primary key,
    DateDossier date,
    DateDebut   date,
    RedevableId varchar(50)
        references Redevable,
    TerrainID   varchar(50)
        references Terrain
)
go

create table Declaration
(
    ID              int not null
        primary key,
    NDossier        varchar(50)
        references Dossier,
    DateDeclaration date,
    Payer           bit,
    Anne            int
)
go
insert into users values ('550565','sbai','pass','admin')

