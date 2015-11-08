USE [praktikum]

GO

-- neue Entities sollten Sie auch mit einer entsprechenden IF EXISTS DROP Zeile behandeln, damit bei Neuausführung der DDL-Befehle die Tabellen alle neu angelegt werden können

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Diskussion]') AND type in (N'U'))
DROP TABLE [Diskussion]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Beitrag]') AND type in (N'U'))
DROP TABLE [Beitrag]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Forum]') AND type in (N'U'))
DROP TABLE [Forum]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Modul]') AND type in (N'U'))
DROP TABLE [Modul]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Dokument]') AND type in (N'U'))
DROP TABLE [Dokument]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Benutzer]') AND type in (N'U'))
DROP TABLE [Benutzer]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Student]') AND type in (N'U'))
DROP TABLE [Student]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Professor]') AND type in (N'U'))
DROP TABLE [Professor]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BeitragDokument]') AND type in (N'U'))
DROP TABLE [BeitragDokument]

GO
-- Entities

CREATE TABLE [Forum](
    [ID] integer identity (0,1) primary key,
	[Bezeichnung] [varchar](100),
	[OberforumID] [integer]
)
GO

CREATE TABLE [Diskussion](
    ID integer identity (0, 1) primary key,
	[Titel] [varchar](100),
	[AnzahlSichtungen] integer,
	[ForumID] [integer] foreign key
)
GO

CREATE TABLE [Beitrag](
    ID integer identity(0,1) primary key,
	[Mitteilung] [varchar](10000),
	[Änderungsdatum] [datetime],
	[Veröffentlichungsdatum] [datetime],
    [DiskussionsID] [integer] foreign key
    [BenutzerID] [integer] foreign key
)
GO

create table [Dokument](
    ID integer identity(0,1) primary key,
    [Titel] [varchar](100),
    [Datei] [varbinary](max),
    [Kategorie] [varchar](100),
    [BenutzerID] integer foreign key,
    [ModulID] integer foreign key,
    [Bereitstellungsdatum] [datetime]
)
go

create table [Modul](
    ID integer identity(0,1) primary key,
    [FachNr] integer,
    [Bezeichnung] [varchar](100),
    [BetreuerID] integer foreign key,
    [ForumID] integer foreign key
)
go

create table [Benutzer](
    ID integer identity(0,1) primary key,
    [Nickname] [varchar](100),
    [Passwort] [varchar](32),
    [E-Mail]   [varchar](100),
    [Vorname]  [varchar](100),
    [Nachname] [varchar](100)
)
go

create table [Student](
    [BenutzerID] integer foreign key,
    [Matrikelnummer] integer,
    [Einschreibedatum] [datetime]
)
go

create table [Professor](
    [BenutzerID] integer foreign key,
    [BüroNr] [varchar](100),
    [Titel] [varchar](100)
)
go

create table [BeitragDokument] (
    [BeitragID] [integer] foreign key,
    [DokumentID] [integer] foreign key
)
go
