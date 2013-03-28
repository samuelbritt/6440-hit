/****** Object:  Table [dbo].[Participant]    Script Date: 03/28/2013 16:33:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Participant]') AND type in (N'U'))
DROP TABLE [dbo].[Participant]
GO
/****** Object:  Default [DF_Patient_hasAuthorized]    Script Date: 03/28/2013 16:33:47 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Patient_hasAuthorized]') AND parent_object_id = OBJECT_ID(N'[dbo].[Participant]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Patient_hasAuthorized]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Participant] DROP CONSTRAINT [DF_Patient_hasAuthorized]
END


End
GO
/****** Object:  Table [dbo].[Participant]    Script Date: 03/28/2013 16:33:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Participant]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Participant](
	[ParticipantID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LastName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Email] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HVPersonID] [uniqueidentifier] NULL,
	[HVRecordID] [uniqueidentifier] NULL,
	[HVParticipantCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SecurityQuestion] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SecurityAnswer] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[HasAuthorized] [bit] NOT NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[ParticipantID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Participant] ON
INSERT [dbo].[Participant] ([ParticipantID], [FirstName], [LastName], [Email], [HVPersonID], [HVRecordID], [HVParticipantCode], [SecurityQuestion], [SecurityAnswer], [HasAuthorized]) VALUES (62, N'p1', N'p1', N'p1', N'9112a498-4cef-4cf7-9981-1f2679640e62', N'906701ab-0469-4e75-afa5-d93cd80ff75c', N'JKVX-GRMJ-VZMR-MTJK-ZRTG', N'p1', N'p1', 1)
SET IDENTITY_INSERT [dbo].[Participant] OFF
/****** Object:  Default [DF_Patient_hasAuthorized]    Script Date: 03/28/2013 16:33:47 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Patient_hasAuthorized]') AND parent_object_id = OBJECT_ID(N'[dbo].[Participant]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Patient_hasAuthorized]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Participant] ADD  CONSTRAINT [DF_Patient_hasAuthorized]  DEFAULT ((0)) FOR [HasAuthorized]
END


End
GO
