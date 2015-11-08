USE [praktikum]

GO

-- neue Entities sollten Sie auch mit einer entsprechenden IF EXISTS DROP Zeile behandeln, damit bei Neuausführung der DDL-Befehle die Tabellen alle neu angelegt werden können
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BeitragDokument]') AND type in (N'U'))
DROP TABLE [BeitragDokument]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Beitrag]') AND type in (N'U'))
DROP TABLE [Beitrag]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Dokument]') AND type in (N'U'))
DROP TABLE [Dokument]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Student]') AND type in (N'U'))
DROP TABLE [Student]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Professor]') AND type in (N'U'))
DROP TABLE [Professor]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Modul]') AND type in (N'U'))
DROP TABLE [Modul]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Benutzer]') AND type in (N'U'))
DROP TABLE [Benutzer]

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Diskussion]') AND type in (N'U'))
DROP TABLE [Diskussion]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Forum]') AND type in (N'U'))
DROP TABLE [Forum]

GO








-- Entities

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
    [BenutzerID] integer foreign key references [Benutzer](ID),
    [Matrikelnummer] integer,
    [Einschreibedatum] [datetime]
)
go

create table [Professor](
    [BenutzerID] integer foreign key references [Benutzer](ID),
    [BüroNr] [varchar](100),
    [Titel] [varchar](100)
)
go

CREATE TABLE [Forum](
    [ID] integer identity (0,1) primary key,
	[Bezeichnung] [varchar](100),
	[OberforumID] [integer]
)
GO

CREATE TABLE [Diskussion](
    ID integer identity (0, 1) primary key,
	[Titel] varchar(100),
	[AnzahlSichtungen] integer,
	[ForumID] integer foreign key references [Forum](ID)
)
GO

CREATE TABLE [Beitrag](
    ID integer identity(0,1) primary key,
	[Mitteilung] [varchar](8000),
	[Änderungsdatum] [datetime],
	[Veröffentlichungsdatum] [datetime],
    [DiskussionsID] [integer] foreign key references [Diskussion](ID),
    [BenutzerID] [integer] foreign key references [Benutzer](ID)
)
GO

create table [Modul](
    ID integer identity(0,1) primary key,
    [FachNr] integer,
    [Bezeichnung] [varchar](100),
    [BetreuerID] integer foreign key references [Benutzer](ID),
    [ForumID] integer foreign key references [Forum](ID)
)
go


create table [Dokument](
    ID integer identity(0,1) primary key,
    [Titel] [varchar](100),
    [Datei] [varbinary](max),
    [Kategorie] [varchar](100),
    [BenutzerID] integer foreign key references [Benutzer](ID),
    [ModulID] integer foreign key references [Modul](ID),
    [Bereitstellungsdatum] [datetime]
)
go



create table [BeitragDokument] (
    [BeitragID] [integer] foreign key references [Beitrag](ID),
    [DokumentID] [integer] foreign key references [Dokument](ID)
)
go
