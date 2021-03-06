USE [GenerationQA]
GO
/****** Object:  Table [dbo].[xEmailReminderRecipients]    Script Date: 2/4/2020 10:25:23 AM ******/
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
/****** Object:  Table [dbo].[xEmailReminderRecipientsForNotification]    Script Date: 2/4/2020 10:25:23 AM ******/
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
ALTER TABLE [dbo].[xEmailReminderRecipients]  WITH CHECK ADD FOREIGN KEY([SiteID])
REFERENCES [dbo].[SitesMaster] ([SiteId])
GO
ALTER TABLE [dbo].[xEmailReminderRecipients]  WITH CHECK ADD FOREIGN KEY([SiteID])
REFERENCES [dbo].[SitesMaster] ([SiteId])
GO
ALTER TABLE [dbo].[xEmailReminderRecipientsForNotification]  WITH CHECK ADD FOREIGN KEY([SiteID])
REFERENCES [dbo].[SitesMaster] ([SiteId])
GO
