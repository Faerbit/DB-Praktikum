USE [Praktikum]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BeitragDokument]') AND type in (N'U'))
DROP TABLE [BeitragDokument]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DokumenteHeftenAnBeiträge]') AND type in (N'U'))
DROP TABLE [DokumenteHeftenAnBeiträge]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Beitrag]') AND type in (N'U')) 
DROP TABLE [Beitrag]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Beiträge]') AND type in (N'U'))
DROP TABLE [Beiträge]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Dokument]') AND type in (N'U'))
DROP TABLE [Dokument]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Dokumente]') AND type in (N'U'))
DROP TABLE [Dokumente]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Student]') AND type in (N'U'))
DROP TABLE [Student]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Studenten]') AND type in (N'U'))
DROP TABLE [Studenten]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Mitarbeiter]') AND type in (N'U'))
DROP TABLE [Mitarbeiter]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Professor]') AND type in (N'U'))
DROP TABLE [Professor]

GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Modul]') AND type in (N'U'))
DROP TABLE [Modul]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Module]') AND type in (N'U'))
DROP TABLE [Module]

GO




IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Diskussion]') AND type in (N'U'))
DROP TABLE [Diskussion]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Diskussionen]') AND type in (N'U'))
DROP TABLE [Diskussionen]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Professoren]') AND type in (N'U'))
DROP TABLE [Professoren]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Benutzer]') AND type in (N'U'))
DROP TABLE [Benutzer]

GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Foren]') AND type in (N'U'))
DROP TABLE [Foren]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Forum]') AND type in (N'U'))
DROP TABLE [Forum]

GO







SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Beiträge](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Mitteilung] [text] NOT NULL,
	[DiskussionsID] [int] NOT NULL,
	[Benutzer] [varchar](20) NOT NULL,
	[Änderungsdatum] [datetime] NULL,
PRIMARY KEY CLUSTERED
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Benutzer](
	[Nickname] [varchar](20) NOT NULL,
	[Vorname] [varchar](50),
	[Nachname] [varchar](50),
	[Passwort] [binary](16) NOT NULL,
	[Email] [varchar](100) UNIQUE,
PRIMARY KEY CLUSTERED 
(
	[Nickname] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Diskussionen](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Titel] [varchar](100) NOT NULL DEFAULT ('Kein Titel'),
	[AnzahlSichtungen] [int] NOT NULL DEFAULT ((0)),
	[ForumID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Dokumente](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Kategorie] [varchar](10) NOT NULL,
	[Datei] [varbinary](max) NULL,
	[Titel] [varchar](100) NOT NULL,
	[Bereitstellungsdatum] [datetime] NOT NULL,
	[Benutzer] [varchar](20) NOT NULL,
	[ModulID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DokumenteHeftenAnBeiträge](
	[BeitragsID] [int] NOT NULL,
	[DokumentenID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BeitragsID] ASC,
	[DokumentenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Foren](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Bezeichnung] [varchar](50) NOT NULL,
	[OberforumID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Mitarbeiter](
	[Nickname] [varchar](20) NOT NULL,
	[RaumNr] [varchar](10) NOT NULL,
	[Aufgabenbereich] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Nickname] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Module](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Bezeichnung] [varchar](100) NOT NULL,
	[FachNummer] [int] NOT NULL,
	[Verantwortlicher] [varchar](20) NOT NULL,
	[ForumID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Professoren](
	[Nickname] [varchar](20) NOT NULL,
	[AkademischerTitel] [varchar](30) NOT NULL,
	[Büro] [varchar](30),
PRIMARY KEY CLUSTERED 
(
	[Nickname] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Studenten](
	[Nickname] [varchar](20) NOT NULL,
	[Matrikel] [int] NOT NULL,
	[EinschreibeDatum] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Nickname] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Benutzer] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Studenten] ADD UNIQUE NONCLUSTERED 
(
	[Matrikel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Beiträge]  WITH CHECK ADD FOREIGN KEY([Benutzer])
REFERENCES [dbo].[Benutzer] ([Nickname])
GO
ALTER TABLE [dbo].[Beiträge]  WITH CHECK ADD FOREIGN KEY([DiskussionsID])
REFERENCES [dbo].[Diskussionen] ([ID])
GO
ALTER TABLE [dbo].[Diskussionen]  WITH CHECK ADD FOREIGN KEY([ForumID])
REFERENCES [dbo].[Foren] ([ID])
GO
ALTER TABLE [dbo].[Dokumente]  WITH CHECK ADD FOREIGN KEY([Benutzer])
REFERENCES [dbo].[Benutzer] ([Nickname])
GO
ALTER TABLE [dbo].[Dokumente]  WITH CHECK ADD FOREIGN KEY([ModulID])
REFERENCES [dbo].[Module] ([ID])
GO
ALTER TABLE [dbo].[DokumenteHeftenAnBeiträge]  WITH CHECK ADD FOREIGN KEY([BeitragsID])
REFERENCES [dbo].[Beiträge] ([ID])
GO
-- diese Zeile war nicht korrekt in der ersten Version dieser Datei --
ALTER TABLE [dbo].[DokumenteHeftenAnBeiträge]  WITH CHECK ADD FOREIGN KEY([DokumentenID])
REFERENCES [dbo].[Dokumente] ([ID])
GO
ALTER TABLE [dbo].[Foren]  WITH CHECK ADD FOREIGN KEY([OberforumID])
REFERENCES [dbo].[Foren] ([ID])
GO
ALTER TABLE [dbo].[Mitarbeiter]  WITH CHECK ADD FOREIGN KEY([Nickname])
REFERENCES [dbo].[Benutzer] ([Nickname])
GO
ALTER TABLE [dbo].[Module]  WITH CHECK ADD FOREIGN KEY([ForumID])
REFERENCES [dbo].[Foren] ([ID])
GO
ALTER TABLE [dbo].[Module]  WITH CHECK ADD FOREIGN KEY([Verantwortlicher])
REFERENCES [dbo].[Professoren] ([Nickname])
GO
ALTER TABLE [dbo].[Professoren]  WITH CHECK ADD FOREIGN KEY([Nickname])
REFERENCES [dbo].[Benutzer] ([Nickname])
GO
ALTER TABLE [dbo].[Studenten]  WITH CHECK ADD FOREIGN KEY([Nickname])
REFERENCES [dbo].[Benutzer] ([Nickname])
GO
ALTER TABLE [dbo].[Benutzer]  WITH CHECK ADD CHECK  ((len([Passwort])>=(6)))
GO
ALTER TABLE [dbo].[Dokumente]  WITH CHECK ADD CHECK  (([Kategorie]='Sonstiges' OR [Kategorie]='Praktikum' OR [Kategorie]='Übung' OR [Kategorie]='Vorlesung'))
GO
ALTER TABLE [dbo].[Module]  WITH CHECK ADD CHECK  (([FachNummer]>=(500) AND [FachNummer]<(1000)))
GO
