/****** Object:  StoredProcedure [dbo].[PcpValidLogin]    Script Date: 04/10/2013 08:09:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PcpValidLogin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PcpValidLogin]

/****** Object:  Table [dbo].[Participant]    Script Date: 04/10/2013 08:09:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Participant]') AND type in (N'U'))
DROP TABLE [dbo].[Participant]

/****** Object:  Table [dbo].[PCP]    Script Date: 04/10/2013 08:09:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PCP]') AND type in (N'U'))
DROP TABLE [dbo].[PCP]

/****** Object:  Default [DF__Participa__Parti__20C1E124]    Script Date: 04/10/2013 08:09:52 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__Participa__Parti__20C1E124]') AND parent_object_id = OBJECT_ID(N'[dbo].[Participant]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Participa__Parti__20C1E124]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Participant] DROP CONSTRAINT [DF__Participa__Parti__20C1E124]
END


End

/****** Object:  Default [DF_Patient_hasAuthorized]    Script Date: 04/10/2013 08:09:52 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Patient_hasAuthorized]') AND parent_object_id = OBJECT_ID(N'[dbo].[Participant]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Patient_hasAuthorized]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Participant] DROP CONSTRAINT [DF_Patient_hasAuthorized]
END


End

/****** Object:  Table [dbo].[PCP]    Script Date: 04/10/2013 08:09:52 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PCP]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PCP](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Institution] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Email] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PhoneNumber] [char](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Password] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_PCP] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_PCP] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END

SET IDENTITY_INSERT [dbo].[PCP] ON
INSERT [dbo].[PCP] ([ID], [LName], [FName], [Institution], [Email], [PhoneNumber], [Password]) VALUES (1, N'pcp1', N'pcp1', N'Peidmont', N'samuelbritt+pcp1@gmail.com', N'1234567890', N'pcp1')
INSERT [dbo].[PCP] ([ID], [LName], [FName], [Institution], [Email], [PhoneNumber], [Password]) VALUES (4, N'pcp2', N'pcp2', N'Northside', N'samuelbritt+pcp2@gmail.com', N'1234567890', N'pcp2')
INSERT [dbo].[PCP] ([ID], [LName], [FName], [Institution], [Email], [PhoneNumber], [Password]) VALUES (6, N'pcp3', N'pcp3', N'Peidmont', N'samuelbritt+pcp3@gmail.com', N'1234567890', N'pcp3')
SET IDENTITY_INSERT [dbo].[PCP] OFF
/****** Object:  Table [dbo].[Participant]    Script Date: 04/10/2013 08:09:52 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Participant]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Participant](
	[ParticipantID] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LastName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Email] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HVPersonID] [uniqueidentifier] NULL,
	[HVRecordID] [uniqueidentifier] NULL,
	[HVParticipantCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SecurityQuestion] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SecurityAnswer] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[HasAuthorized] [bit] NOT NULL,
 CONSTRAINT [PK__Particip__7227997E1ED998B2] PRIMARY KEY CLUSTERED 
(
	[ParticipantID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END

INSERT [dbo].[Participant] ([ParticipantID], [FirstName], [LastName], [Email], [HVPersonID], [HVRecordID], [HVParticipantCode], [SecurityQuestion], [SecurityAnswer], [HasAuthorized]) VALUES (N'7a68cd51-95eb-4bcd-a8aa-9573376b2c39', N'p4', N'p4', N'samuelbritt+p4@gmail.com', N'9112a498-4cef-4cf7-9981-1f2679640e62', N'906701ab-0469-4e75-afa5-d93cd80ff75c', N'JKFJ-FNMJ-NRRM-CRFN-JYJF', N'p4', N'p4', 1)
INSERT [dbo].[Participant] ([ParticipantID], [FirstName], [LastName], [Email], [HVPersonID], [HVRecordID], [HVParticipantCode], [SecurityQuestion], [SecurityAnswer], [HasAuthorized]) VALUES (N'7a36700f-9898-4ddd-8c73-c0506143f59a', N'p5', N'p5', N'samuelbritt+p5@gmail.com', N'00000000-0000-0000-0000-000000000000', N'00000000-0000-0000-0000-000000000000', N'JKJV-HQMN-QXGV-QZTK-ZVXQ', N'p5', N'p5', 0)
INSERT [dbo].[Participant] ([ParticipantID], [FirstName], [LastName], [Email], [HVPersonID], [HVRecordID], [HVParticipantCode], [SecurityQuestion], [SecurityAnswer], [HasAuthorized]) VALUES (N'22cfd392-4d45-4d8a-b59d-c13818ee5456', N'p2', N'p2', N'samuelbritt+p2@gmail.com', N'9112a498-4cef-4cf7-9981-1f2679640e62', N'906701ab-0469-4e75-afa5-d93cd80ff75c', N'JKZK-TGMH-HPRN-FNJZ-XHVQ', N'p2', N'p2', 1)
INSERT [dbo].[Participant] ([ParticipantID], [FirstName], [LastName], [Email], [HVPersonID], [HVRecordID], [HVParticipantCode], [SecurityQuestion], [SecurityAnswer], [HasAuthorized]) VALUES (N'f4f82743-8ce7-4cf1-8fe1-c87374470d3c', N'p1', N'p1', N'samuelbritt+p1@gmail.com', N'9112a498-4cef-4cf7-9981-1f2679640e62', N'906701ab-0469-4e75-afa5-d93cd80ff75c', N'JKYQ-QRMV-PXYT-QHTX-CVZY', N'p1', N'p1', 1)
INSERT [dbo].[Participant] ([ParticipantID], [FirstName], [LastName], [Email], [HVPersonID], [HVRecordID], [HVParticipantCode], [SecurityQuestion], [SecurityAnswer], [HasAuthorized]) VALUES (N'2b084bbe-0ddb-4f19-be3d-d7da44423aec', N'p7', N'p7', N'samuelbritt+p7@gmail.com', N'00000000-0000-0000-0000-000000000000', N'00000000-0000-0000-0000-000000000000', N'JKPX-GMMY-KKPF-RVMG-GRJC', N'p7', N'p7', 0)
INSERT [dbo].[Participant] ([ParticipantID], [FirstName], [LastName], [Email], [HVPersonID], [HVRecordID], [HVParticipantCode], [SecurityQuestion], [SecurityAnswer], [HasAuthorized]) VALUES (N'1e348eb9-9359-419f-ab81-f51623a1e02a', N'p6', N'p6', N'samuelbritt+p6@gmail.com', N'00000000-0000-0000-0000-000000000000', N'00000000-0000-0000-0000-000000000000', N'JKXT-YCMR-YZJP-ZHQJ-YKFV', N'p6', N'p6', 0)
INSERT [dbo].[Participant] ([ParticipantID], [FirstName], [LastName], [Email], [HVPersonID], [HVRecordID], [HVParticipantCode], [SecurityQuestion], [SecurityAnswer], [HasAuthorized]) VALUES (N'812195ee-a0c9-47ca-8b64-f8a50c190590', N'p3', N'p3', N'samuelbritt+p3@gmail.com', N'9112a498-4cef-4cf7-9981-1f2679640e62', N'906701ab-0469-4e75-afa5-d93cd80ff75c', N'JKVT-QHMZ-VKCY-NNZT-KVHK', N'p3', N'p3', 1)
/****** Object:  StoredProcedure [dbo].[PcpValidLogin]    Script Date: 04/10/2013 08:09:52 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PcpValidLogin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create Procedure [dbo].[PcpValidLogin]
 (
 @Email VarChar(256), 
 @Password VarChar(256),
 @isValid int OUTPUT
 )
 AS
set @isValid = (SELECT count(*) FROM [dbo].PCP
WHERE Email = @Email And Password = @Password)

if(@isValid = 1)
begin
set @isValid = 1	--Login is Correct
end
else
begin
set @isValid = 0	--Bad login
end
return' 
END

/****** Object:  Default [DF__Participa__Parti__20C1E124]    Script Date: 04/10/2013 08:09:52 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__Participa__Parti__20C1E124]') AND parent_object_id = OBJECT_ID(N'[dbo].[Participant]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Participa__Parti__20C1E124]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Participant] ADD  CONSTRAINT [DF__Participa__Parti__20C1E124]  DEFAULT (newid()) FOR [ParticipantID]
END


End

/****** Object:  Default [DF_Patient_hasAuthorized]    Script Date: 04/10/2013 08:09:52 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Patient_hasAuthorized]') AND parent_object_id = OBJECT_ID(N'[dbo].[Participant]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Patient_hasAuthorized]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Participant] ADD  CONSTRAINT [DF_Patient_hasAuthorized]  DEFAULT ((0)) FOR [HasAuthorized]
END


End

