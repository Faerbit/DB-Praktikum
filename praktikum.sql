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
    [Nickname] [varchar](100) not null,
    [Passwort] [varchar](32) not null,
    [E-Mail]   [varchar](100) not null,
    [Vorname]  [varchar](100),
    [Nachname] [varchar](100)
)
go

create table [Student](
    [BenutzerID] integer primary key foreign key references [Benutzer](ID) not null,
    [Matrikelnummer] integer not null,
    [Einschreibedatum] [datetime] not null default CURRENT_TIMESTAMP,
	constraint fk_benutzer_student foreign key (BenutzerID) references [Benutzer](ID) on delete cascade
)
go

create table [Professor](
    [BenutzerID] integer primary key foreign key references [Benutzer](ID),
    [BüroNr] [varchar](100) not null,
    [Titel] [varchar](100)
	constraint fk_benutzer_professor foreign key (BenutzerID) references [Benutzer](ID) on delete cascade
)
go

CREATE TABLE [Forum](
    [ID] integer identity (0,1) primary key,
	[Bezeichnung] [varchar](100) not null,
	[OberforumID] [integer]
)
GO

CREATE TABLE [Diskussion](
    ID integer identity (0, 1) primary key,
	[Titel] varchar(100) not null,
	[AnzahlSichtungen] integer default 0 not null,
	[ForumID] integer foreign key references [Forum](ID) not null
)
GO

CREATE TABLE [Beitrag](
    ID integer identity(0,1) primary key,
	[Mitteilung] [varchar](8000) not null,
	[Änderungsdatum] [datetime] not null default CURRENT_TIMESTAMP,
	[Veröffentlichungsdatum] [datetime] not null default CURRENT_TIMESTAMP,
    [DiskussionsID] [integer] foreign key references [Diskussion](ID) not null,
    [BenutzerID] [integer] foreign key references [Benutzer](ID) not null
)
GO

create table [Modul](
    ID integer identity(0,1) primary key,
    [FachNr] integer not null,
    [Bezeichnung] [varchar](100) not null,
    [BetreuerID] integer foreign key references [Benutzer](ID) not null,
    [ForumID] integer foreign key references [Forum](ID) not null,
	constraint cnstr_FachNr check(FachNr > 500 and FachNr < 999)
)
go


create table [Dokument](
    ID integer identity(0,1) primary key,
    [Titel] [varchar](100) not null,
    [Datei] [varbinary](max) not null,
    [Kategorie] [varchar](100),
    [BenutzerID] integer foreign key references [Benutzer](ID) not null,
    [ModulID] integer foreign key references [Modul](ID) not null,
    [Bereitstellungsdatum] [datetime] not null default CURRENT_TIMESTAMP,
	constraint cnstr_Kategorie check(Kategorie = 'Vorlesung' or Kategorie = 'Übung' or Kategorie = 'Praktikum' or Kategorie = 'Sonstiges')
)
go



create table [BeitragDokument] (
    [BeitragID] [integer] foreign key references [Beitrag](ID) not null,
    [DokumentID] [integer] foreign key references [Dokument](ID) not null
)
go
