/****** Object:  Table [dbo].[Participant]    Script Date: 03/28/2013 16:33:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Participant]') AND type in (N'U'))
DROP TABLE [dbo].[Participant];

/****** Object:  Default [DF_Patient_hasAuthorized]    Script Date: 03/28/2013 16:33:47 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Patient_hasAuthorized]') AND parent_object_id = OBJECT_ID(N'[dbo].[Participant]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Patient_hasAuthorized]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Participant] DROP CONSTRAINT [DF_Patient_hasAuthorized]
END


End

/****** Object:  Table [dbo].[Participant]    Script Date: 03/28/2013 16:33:47 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Participant]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Participant](
	[ParticipantID] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[FirstName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LastName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Email] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HVPersonID] [uniqueidentifier] NULL,
	[HVRecordID] [uniqueidentifier] NULL,
	[HVParticipantCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SecurityQuestion] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SecurityAnswer] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[HasAuthorized] [bit] NOT NULL
)
END


/****** Object:  Default [DF_Patient_hasAuthorized]    Script Date: 03/28/2013 16:33:47 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Patient_hasAuthorized]') AND parent_object_id = OBJECT_ID(N'[dbo].[Participant]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Patient_hasAuthorized]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Participant] ADD  CONSTRAINT [DF_Patient_hasAuthorized]  DEFAULT ((0)) FOR [HasAuthorized]
END


End

