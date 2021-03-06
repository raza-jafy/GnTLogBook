USE [GenerationQA]
GO
/****** Object:  Table [dbo].[xEmailReminderRecipients]    Script Date: 1/23/2020 9:26:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[xEmailReminderRecipients](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SiteID] [varchar](10) NULL,
	[ReminderNo] [int] NULL,
	[ToRecipients] [varchar](max) NULL,
	[CCRecipients] [varchar](max) NULL,
	[RedgDateTime] [datetime] NULL DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[xEmailReminderRecipientsForNotification]    Script Date: 1/23/2020 9:26:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[xEmailReminderRecipientsForNotification](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SiteID] [varchar](10) NULL,
	[ReminderNo] [int] NULL,
	[ToRecipients] [varchar](max) NULL,
	[CCRecipients] [varchar](max) NULL,
	[RedgDateTime] [datetime] NULL DEFAULT (getdate()),
	[RType] [varchar](max) NULL,
	[Active] [bigint] NULL,
 CONSTRAINT [PK_xEmailReminderRecipientsForNotification] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[xEmailReminderRecipients] ON 

INSERT [dbo].[xEmailReminderRecipients] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime]) VALUES (1, N'BQ1', 1, N'tahirmalik.muhammad@ke.com.pk;arshad.farooqui@ke.com.pk', N'shahbaz.naser@ke.com.pk;naseem.raza@ke.com.pk;waqas.ansari@ke.com.pk;muneer.ul.haq@ke.com.pk;faizan.ahmed@ke.com.pk', CAST(N'2019-12-24 13:12:24.913' AS DateTime))
INSERT [dbo].[xEmailReminderRecipients] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime]) VALUES (2, N'BQ2', 1, N'mehdi.ali@ke.com.pk;aamir.fayyaz@ke.com.pk', N'rehman.sheikh@ke.com.pk;aslam.warsi@ke.com.pk;imranur.rahim@ke.com.pk;afzal.sher@ke.com.pk;mazhar.haider@ke.com.pk;muhammad.ansar@ke.com.pk;umair.abid@ke.com.pk;ikhtiar.ahmed@ke.com.pk;hassan.sajjad@ke.com.pk;umer.khalid@ke.com.pk', CAST(N'2019-12-24 13:13:55.703' AS DateTime))
INSERT [dbo].[xEmailReminderRecipients] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime]) VALUES (3, N'KPC', 1, N'imtiaz.alam@ke.com.pk;javed.mateen@ke.com.pk', N'asad.rahman@ke.com.pk;kaleemullah.khan@ke.com.pk;khurram.khan@ke.com.pk;gotam.ram@ke.com.pk;imran.mukhtar@ke.com.pk;muhammad.anas@ke.com.pk;sajjad.raza@ke.com.pk;abbas.zulfiqar@ke.com.pk;waseem.sabir@ke.com.pk;talha.khubaib@ke.com.pk', CAST(N'2019-12-24 13:16:06.580' AS DateTime))
INSERT [dbo].[xEmailReminderRecipients] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime]) VALUES (4, N'SGT', 1, N'saqib.shaheen@ke.com.pk;mahsan.qureshi@ke.com.pk', N'toqeer.alam@ke.com.pk;javed.mumtaz@ke.com.pk;syed.zeeshan@ke.com.pk;mfarhan.alam@ke.com.pk;raza.jamal@ke.com.pk;junaid.waseem@ke.com.pk;sohaib.khan@ke.com.pk;nida.nawab@ke.com.pk', CAST(N'2019-12-24 13:18:42.520' AS DateTime))
INSERT [dbo].[xEmailReminderRecipients] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime]) VALUES (5, N'KGT', 1, N'adnan.ahmad@ke.com.pk;Mirza.saad@ke.com.pk', N'amir.alisyed@ke.com.pk;shahrukh.Abbas@ke.com.pk;wajid.shaikh@ke.com.pk;zarak.Noveed@ke.com.pk;mustafa.Khanzada@ke.com.pk;mustafa.Suleman@ke.com.pk;Syed.irfan@ke.com.pk', CAST(N'2019-12-24 13:20:33.853' AS DateTime))
INSERT [dbo].[xEmailReminderRecipients] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime]) VALUES (6, N'BQ1', 3, N'razzaq.ahmad@ke.com.pk', N'tahirmalik.muhammad@ke.com.pk;arshad.farooqui@ke.com.pk;mustafa.husain@ke.com.pk;sm.adeel@ke.com.pk;mzafar.iqbal@ke.com.pk;asmara.shahid@ke.com.pk', CAST(N'2019-12-24 13:23:22.993' AS DateTime))
INSERT [dbo].[xEmailReminderRecipients] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime]) VALUES (7, N'BQ2', 3, N'tahir.ali@ke.com.pk', N'mehdi.ali@ke.com.pk;aamir.fayyaz@ke.com.pk;mustafa.husain@ke.com.pk;sm.adeel@ke.com.pk;mzafar.iqbal@ke.com.pk;asmara.shahid@ke.com.pk', CAST(N'2019-12-24 13:24:52.417' AS DateTime))
INSERT [dbo].[xEmailReminderRecipients] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime]) VALUES (8, N'KPC', 3, N'nowshad.alam@ke.com.pk', N'imtiaz.alam@ke.com.pk;javed.mateen@ke.com.pk;mustafa.husain@ke.com.pk;sm.adeel@ke.com.pk;mzafar.iqbal@ke.com.pk;asmara.shahid@ke.com.pk', CAST(N'2019-12-24 13:26:00.413' AS DateTime))
INSERT [dbo].[xEmailReminderRecipients] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime]) VALUES (9, N'SGT', 3, N'muhammad.s@ke.com.pk', N'saqib.shaheen@ke.com.pk;mahsan.qureshi@ke.com.pk;mustafa.husain@ke.com.pk;sm.adeel@ke.com.pk;mzafar.iqbal@ke.com.pk;asmara.shahid@ke.com.pk', CAST(N'2019-12-24 14:31:37.373' AS DateTime))
INSERT [dbo].[xEmailReminderRecipients] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime]) VALUES (10, N'KGT', 3, N'nowshad.alam@ke.com.pk', N'adnan.ahmad@ke.com.pk;Mirza.saad@ke.com.pk;mustafa.husain@ke.com.pk;sm.adeel@ke.com.pk;mzafar.iqbal@ke.com.pk;asmara.shahid@ke.com.pk', CAST(N'2019-12-24 14:33:21.287' AS DateTime))
SET IDENTITY_INSERT [dbo].[xEmailReminderRecipients] OFF
SET IDENTITY_INSERT [dbo].[xEmailReminderRecipientsForNotification] ON 

INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (1, N'BQ1', 1, N'tahirmalik.muhammad@ke.com.pk', N'shahbaz.naser@ke.com.pk', CAST(N'2020-01-08 12:59:26.040' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (2, N'BQ1', 1, N'arshad.farooqui@ke.com.pk', N'naseem.raza@ke.com.pk', CAST(N'2020-01-08 13:01:18.033' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (3, N'BQ1', 1, N'shahbaz.naser@ke.com.pk', N'waqas.ansari@ke.com.pk', CAST(N'2020-01-08 13:02:21.210' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (4, N'BQ1', 1, N'naseem.raza@ke.com.pk', N'muneer.ul.haq@ke.com.pk', CAST(N'2020-01-08 13:02:40.920' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (5, N'BQ1', 1, N'waqas.ansari@ke.com.pk', N'faizan.ahmed@ke.com.pk', CAST(N'2020-01-08 13:03:06.130' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (7, N'BQ1', 1, N'muneer.ul.haq@ke.com.pk', N'rehman.sheikh@ke.com.pk', CAST(N'2020-01-08 13:05:00.413' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (8, N'BQ1', 1, N'faizan.ahmed@ke.com.pk', N'aslam.warsi@ke.com.pk', CAST(N'2020-01-08 13:05:35.690' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (9, N'BQ2', 1, N'mehdi.ali@ke.com.pk', N'imranur.rahim@ke.com.pk', CAST(N'2020-01-08 13:06:06.460' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (10, N'BQ2', 1, N'aamir.fayyaz@ke.com.pk', N'afzal.sher@ke.com.pk', CAST(N'2020-01-08 13:06:16.277' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (11, N'BQ2', 1, N'rehman.sheikh@ke.com.pk', N'mazhar.haider@ke.com.pk', CAST(N'2020-01-08 13:06:24.983' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (12, N'BQ2', 1, N'aslam.warsi@ke.com.pk', N'muhammad.ansar@ke.com.pk', CAST(N'2020-01-08 13:06:33.613' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (13, N'BQ2', 1, N'imranur.rahim@ke.com.pk', N'umair.abid@ke.com.pk', CAST(N'2020-01-08 13:06:41.327' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (14, N'BQ2', 1, N'afzal.sher@ke.com.pk', N'ikhtiar.ahmed@ke.com.pk', CAST(N'2020-01-08 13:06:49.407' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (15, N'BQ2', 1, N'mazhar.haider@ke.com.pk', N'hassan.sajjad@ke.com.pk', CAST(N'2020-01-08 13:07:02.907' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (16, N'BQ2', 1, N'muhammad.ansar@ke.com.pk', N'umer.khalid@ke.com.pk', CAST(N'2020-01-08 13:07:11.863' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (17, N'BQ2', 1, N'umair.abid@ke.com.pk', N'asad.rahman@ke.com.pk', CAST(N'2020-01-08 13:07:50.343' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (18, N'BQ2', 1, N'ikhtiar.ahmed@ke.com.pk', N'kaleemullah.khan@ke.com.pk', CAST(N'2020-01-08 13:08:39.987' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (19, N'BQ2', 1, N'hassan.sajjad@ke.com.pk', N'khurram.khan@ke.com.pk', CAST(N'2020-01-08 13:09:19.997' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (20, N'BQ2', 1, N'umer.khalid@ke.com.pk', N'gotam.ram@ke.com.pk', CAST(N'2020-01-08 13:09:26.250' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (21, N'KPC', 1, N'imtiaz.alam@ke.com.pk', N'imran.mukhtar@ke.com.pk', CAST(N'2020-01-08 13:09:38.860' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (22, N'KPC', 1, N'javed.mateen@ke.com.pk', N'muhammad.anas@ke.com.pk', CAST(N'2020-01-08 13:09:45.360' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (23, N'KPC', 1, N'asad.rahman@ke.com.pk', N'sajjad.raza@ke.com.pk', CAST(N'2020-01-08 13:09:51.437' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (24, N'KPC', 1, N'kaleemullah.khan@ke.com.pk', N'abbas.zulfiqar@ke.com.pk', CAST(N'2020-01-08 13:09:58.090' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (25, N'KPC', 1, N'khurram.khan@ke.com.pk', N'waseem.sabir@ke.com.pk', CAST(N'2020-01-08 13:10:04.817' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (26, N'KPC', 1, N'gotam.ram@ke.com.pk', N'talha.khubaib@ke.com.pk', CAST(N'2020-01-08 13:10:06.583' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (27, N'KPC', 1, N'imran.mukhtar@ke.com.pk', N'toqeer.alam@ke.com.pk', CAST(N'2020-01-08 13:10:24.307' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (28, N'KPC', 1, N'muhammad.anas@ke.com.pk', N'javed.mumtaz@ke.com.pk', CAST(N'2020-01-08 13:11:25.040' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (29, N'KPC', 1, N'sajjad.raza@ke.com.pk', N'syed.zeeshan@ke.com.pk', CAST(N'2020-01-08 13:11:58.340' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (30, N'KPC', 1, N'abbas.zulfiqar@ke.com.pk', N'mfarhan.alam@ke.com.pk', CAST(N'2020-01-08 13:12:04.557' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (31, N'KPC', 1, N'waseem.sabir@ke.com.pk', N'raza.jamal@ke.com.pk', CAST(N'2020-01-08 13:12:12.077' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (32, N'KPC', 1, N'talha.khubaib@ke.com.pk', N'junaid.waseem@ke.com.pk', CAST(N'2020-01-08 13:12:18.713' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (33, N'SGT', 1, N'saqib.shaheen@ke.com.pk', N'sohaib.khan@ke.com.pk', CAST(N'2020-01-08 13:12:24.627' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (34, N'SGT', 1, N'mahsan.qureshi@ke.com.pk', N'nida.nawab@ke.com.pk', CAST(N'2020-01-08 13:12:31.333' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (35, N'SGT', 1, N'toqeer.alam@ke.com.pk', N'amir.alisyed@ke.com.pk', CAST(N'2020-01-08 14:50:40.563' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (36, N'SGT', 1, N'javed.mumtaz@ke.com.pk', N'shahrukh.Abbas@ke.com.pk', CAST(N'2020-01-08 14:51:14.690' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (37, N'SGT', 1, N'syed.zeeshan@ke.com.pk', N'wajid.shaikh@ke.com.pk', CAST(N'2020-01-08 14:51:46.840' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (38, N'SGT', 1, N'mfarhan.alam@ke.com.pk', N'zarak.Noveed@ke.com.pk', CAST(N'2020-01-08 14:51:53.760' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (39, N'SGT', 1, N'raza.jamal@ke.com.pk', N'mustafa.Khanzada@ke.com.pk', CAST(N'2020-01-08 14:52:02.640' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (40, N'SGT', 1, N'junaid.waseem@ke.com.pk', N'mustafa.Suleman@ke.com.pk', CAST(N'2020-01-08 14:52:08.867' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (41, N'SGT', 1, N'sohaib.khan@ke.com.pk', N'Syed.irfan@ke.com.pk', CAST(N'2020-01-08 14:52:17.440' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (42, N'SGT', 1, N'nida.nawab@ke.com.pk', N'tahirmalik.muhammad@ke.com.pk', CAST(N'2020-01-08 14:53:01.800' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (43, N'KGT', 1, N'adnan.ahmad@ke.com.pk', N'arshad.farooqui@ke.com.pk', CAST(N'2020-01-08 14:54:27.960' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (44, N'KGT', 1, N'Mirza.saad@ke.com.pk', N'mustafa.husain@ke.com.pk', CAST(N'2020-01-08 14:54:35.260' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (45, N'KGT', 1, N'amir.alisyed@ke.com.pk', N'sm.adeel@ke.com.pk', CAST(N'2020-01-08 14:55:10.350' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (46, N'KGT', 1, N'shahrukh.Abbas@ke.com.pk', N'mzafar.iqbal@ke.com.pk', CAST(N'2020-01-08 14:55:16.763' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (47, N'KGT', 1, N'wajid.shaikh@ke.com.pk', N'asmara.shahid@ke.com.pk', CAST(N'2020-01-08 14:55:23.193' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (48, N'KGT', 1, N'zarak.Noveed@ke.com.pk', N'mehdi.ali@ke.com.pk', CAST(N'2020-01-08 14:55:55.157' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (49, N'KGT', 1, N'mustafa.Khanzada@ke.com.pk', N'aamir.fayyaz@ke.com.pk', CAST(N'2020-01-08 14:56:28.247' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (50, N'KGT', 1, N'mustafa.Suleman@ke.com.pk', N'mustafa.husain@ke.com.pk', CAST(N'2020-01-08 14:56:34.827' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (51, N'KGT', 1, N'Syed.irfan@ke.com.pk', N'sm.adeel@ke.com.pk', CAST(N'2020-01-08 14:56:40.983' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (52, N'BQ1', 2, N'razzaq.ahmad@ke.com.pk', N'mzafar.iqbal@ke.com.pk', CAST(N'2020-01-08 14:56:47.157' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (53, N'BQ1', 2, N'tahirmalik.muhammad@ke.com.pk', N'asmara.shahid@ke.com.pk', CAST(N'2020-01-08 14:56:54.330' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (54, N'BQ1', 2, N'arshad.farooqui@ke.com.pk', N'imtiaz.alam@ke.com.pk', CAST(N'2020-01-08 14:57:31.487' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (55, N'BQ1', 2, N'mustafa.husain@ke.com.pk', N'javed.mateen@ke.com.pk', CAST(N'2020-01-08 14:57:59.490' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (56, N'BQ1', 2, N'sm.adeel@ke.com.pk', N'mustafa.husain@ke.com.pk', CAST(N'2020-01-08 14:58:06.570' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (57, N'BQ1', 2, N'mzafar.iqbal@ke.com.pk', N'sm.adeel@ke.com.pk', CAST(N'2020-01-08 14:58:12.680' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (58, N'BQ1', 2, N'asmara.shahid@ke.com.pk', N'mzafar.iqbal@ke.com.pk', CAST(N'2020-01-08 14:58:19.290' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (59, N'BQ2', 2, N'tahir.ali@ke.com.pk', N'asmara.shahid@ke.com.pk', CAST(N'2020-01-08 14:58:26.130' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (60, N'BQ2', 2, N'mehdi.ali@ke.com.pk', N'saqib.shaheen@ke.com.pk', CAST(N'2020-01-08 14:59:08.300' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (61, N'BQ2', 2, N'aamir.fayyaz@ke.com.pk', N'mahsan.qureshi@ke.com.pk', CAST(N'2020-01-08 14:59:43.680' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (62, N'BQ2', 2, N'mustafa.husain@ke.com.pk', N'mustafa.husain@ke.com.pk', CAST(N'2020-01-08 14:59:50.407' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (63, N'BQ2', 2, N'sm.adeel@ke.com.pk', N'sm.adeel@ke.com.pk', CAST(N'2020-01-08 14:59:56.663' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (64, N'BQ2', 2, N'mzafar.iqbal@ke.com.pk', N'mzafar.iqbal@ke.com.pk', CAST(N'2020-01-08 15:00:02.933' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (65, N'BQ2', 2, N'asmara.shahid@ke.com.pk', N'asmara.shahid@ke.com.pk', CAST(N'2020-01-08 15:00:12.060' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (66, N'KPC', 2, N'nowshad.alam@ke.com.pk', N'adnan.ahmad@ke.com.pk', CAST(N'2020-01-08 15:00:39.693' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (67, N'KPC', 2, N'imtiaz.alam@ke.com.pk', N'Mirza.saad@ke.com.pk', CAST(N'2020-01-08 15:01:08.783' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (68, N'KPC', 2, N'javed.mateen@ke.com.pk', N'mustafa.husain@ke.com.pk', CAST(N'2020-01-08 15:01:15.083' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (69, N'KPC', 2, N'mustafa.husain@ke.com.pk', N'sm.adeel@ke.com.pk', CAST(N'2020-01-08 15:01:22.120' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (70, N'KPC', 2, N'sm.adeel@ke.com.pk', N'mzafar.iqbal@ke.com.pk', CAST(N'2020-01-08 15:01:28.800' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (71, N'KPC', 2, N'mzafar.iqbal@ke.com.pk', N'asmara.shahid@ke.com.pk', CAST(N'2020-01-08 15:01:36.097' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (72, N'KPC', 2, N'asmara.shahid@ke.com.pk', NULL, CAST(N'2020-01-14 14:43:04.183' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (73, N'SGT', 2, N'muhammad.s@ke.com.pk', NULL, CAST(N'2020-01-14 14:45:20.050' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (74, N'SGT', 2, N'saqib.shaheen@ke.com.pk', NULL, CAST(N'2020-01-14 14:46:02.383' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (75, N'SGT', 2, N'mahsan.qureshi@ke.com.pk', NULL, CAST(N'2020-01-14 14:46:09.230' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (76, N'SGT', 2, N'mustafa.husain@ke.com.pk', NULL, CAST(N'2020-01-14 14:46:15.977' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (77, N'SGT', 2, N'sm.adeel@ke.com.pk', NULL, CAST(N'2020-01-14 14:46:21.893' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (78, N'SGT', 2, N'mzafar.iqbal@ke.com.pk', NULL, CAST(N'2020-01-14 14:46:29.593' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (79, N'SGT', 2, N'asmara.shahid@ke.com.pk', NULL, CAST(N'2020-01-14 14:46:37.973' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (80, N'KGT', 2, N'nowshad.alam@ke.com.pk', NULL, CAST(N'2020-01-14 14:47:33.617' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (81, N'KGT', 2, N'adnan.ahmad@ke.com.pk', NULL, CAST(N'2020-01-14 14:48:06.880' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (82, N'KGT', 2, N'Mirza.saad@ke.com.pk', NULL, CAST(N'2020-01-14 14:48:13.230' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (83, N'KGT', 2, N'mustafa.husain@ke.com.pk', NULL, CAST(N'2020-01-14 14:48:20.150' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (84, N'KGT', 2, N'sm.adeel@ke.com.pk', NULL, CAST(N'2020-01-14 14:48:26.417' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (85, N'KGT', 2, N'mzafar.iqbal@ke.com.pk', NULL, CAST(N'2020-01-14 14:48:32.820' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (86, N'KGT', 2, N'asmara.shahid@ke.com.pk', NULL, CAST(N'2020-01-14 14:48:41.023' AS DateTime), N'CC', 1)
SET IDENTITY_INSERT [dbo].[xEmailReminderRecipientsForNotification] OFF
ALTER TABLE [dbo].[xEmailReminderRecipients]  WITH CHECK ADD FOREIGN KEY([SiteID])
REFERENCES [dbo].[SitesMaster] ([SiteId])
GO
ALTER TABLE [dbo].[xEmailReminderRecipients]  WITH CHECK ADD FOREIGN KEY([SiteID])
REFERENCES [dbo].[SitesMaster] ([SiteId])
GO
ALTER TABLE [dbo].[xEmailReminderRecipientsForNotification]  WITH CHECK ADD FOREIGN KEY([SiteID])
REFERENCES [dbo].[SitesMaster] ([SiteId])
GO
