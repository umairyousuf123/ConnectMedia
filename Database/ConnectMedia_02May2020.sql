USE [ConnectMedia]
GO
/****** Object:  Table [dbo].[Building]    Script Date: 5/2/2020 3:09:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Building](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Desc] [varchar](250) NULL,
	[Address] [varchar](50) NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsDel] [bit] NULL,
 CONSTRAINT [PK_Building] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Category]    Script Date: 5/2/2020 3:09:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsDel] [bit] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Classified]    Script Date: 5/2/2020 3:09:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Classified](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NULL,
	[Content] [varchar](max) NULL,
	[Start] [date] NULL,
	[End] [date] NULL,
	[Playlist] [varchar](max) NULL,
	[Status] [varchar](50) NULL,
	[ContactNumber] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsDel] [bit] NULL,
 CONSTRAINT [PK_Classified] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[News]    Script Date: 5/2/2020 3:09:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[News](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](50) NULL,
	[Heading] [varchar](250) NULL,
	[Description] [nvarchar](max) NULL,
	[IssueDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsDel] [bit] NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Notice]    Script Date: 5/2/2020 3:09:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Notice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NULL,
	[CategoryId] [int] NULL,
	[Content] [varchar](max) NULL,
	[Duration] [bigint] NULL,
	[StartDate] [date] NULL,
	[StartTime] [time](7) NULL,
	[EndDate] [date] NULL,
	[EndTime] [time](7) NULL,
	[Expire] [bit] NULL,
	[Playlist] [varchar](max) NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsDel] [bit] NULL,
 CONSTRAINT [PK_Notice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NoticePlaylist]    Script Date: 5/2/2020 3:09:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoticePlaylist](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PlaylistId] [int] NULL,
	[ClassifiedId] [int] NULL,
	[NoticeId] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsDel] [bit] NULL,
 CONSTRAINT [PK_NoticePlaylist] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Playlist]    Script Date: 5/2/2020 3:09:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Playlist](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsDel] [bit] NULL,
 CONSTRAINT [PK_Playlist] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PlaylistBuilding]    Script Date: 5/2/2020 3:09:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlaylistBuilding](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BuildingId] [int] NULL,
	[PlaylistId] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsDel] [bit] NULL,
 CONSTRAINT [PK_PlaylistBuilding] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ResgisterUser]    Script Date: 5/2/2020 3:09:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ResgisterUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TeamName] [varchar](250) NULL,
	[SerialNo] [varchar](250) NULL,
	[Name] [varchar](250) NULL,
	[Email] [varchar](250) NULL,
	[Phone] [varchar](250) NULL,
	[RegistrationNumber] [varchar](250) NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[IsActive] [bit] NULL CONSTRAINT [DF_ResgisterUser_IsActive]  DEFAULT ((1)),
	[IsDel] [bit] NULL CONSTRAINT [DF_ResgisterUser_IsDel]  DEFAULT ((0)),
 CONSTRAINT [PK_ResgisterUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Role]    Script Date: 5/2/2020 3:09:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] NOT NULL,
	[RoleName] [varchar](20) NULL,
	[isDel] [bit] NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SSPwd]    Script Date: 5/2/2020 3:09:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SSPwd](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NULL,
	[password] [varchar](50) NULL,
 CONSTRAINT [PK_SSPwd] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Template]    Script Date: 5/2/2020 3:09:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Template](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NULL,
	[Title] [varchar](50) NULL,
	[Description] [varchar](250) NULL,
	[Content] [nvarchar](max) NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsDel] [bit] NULL,
 CONSTRAINT [PK_NoticeTemplates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UploadFile]    Script Date: 5/2/2020 3:09:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UploadFile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UploadType] [varchar](50) NULL,
	[Title] [varchar](50) NULL,
	[Desc] [varchar](250) NULL,
	[FileName] [varchar](250) NULL,
	[FilePath] [varchar](250) NULL,
	[Portion] [int] NULL,
	[Sequence] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsDel] [bit] NULL,
 CONSTRAINT [PK_UploadFiles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/2/2020 3:09:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [varchar](50) NULL,
	[ContactNumber] [varchar](15) NULL,
	[Password] [varchar](max) NULL,
	[BuildingIds] [varchar](50) NULL CONSTRAINT [DF_Users_BuildingId]  DEFAULT ((0)),
	[RoleId] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsDel] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Building] ON 

INSERT [dbo].[Building] ([Id], [Name], [Desc], [Address], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (1, N'Building A', N'This is User For Air Condition Ads', N'ABC', 2, CAST(N'2020-04-22 19:41:52.950' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[Building] ([Id], [Name], [Desc], [Address], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (2, N'Building B', N'This is User For TV Ads', N'BCD', 2, CAST(N'2020-04-22 19:43:41.993' AS DateTime), NULL, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[Building] OFF
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (1, N'TV', 1, CAST(N'2020-04-06 21:01:55.417' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[Category] ([Id], [Name], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (2, N'AC', 1, CAST(N'2020-04-06 21:01:55.417' AS DateTime), NULL, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[Classified] ON 

INSERT [dbo].[Classified] ([Id], [Title], [Content], [Start], [End], [Playlist], [Status], [ContactNumber], [Name], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (1, N'New Classified Updated', N'In this module you are introduced to some of the key capabilities and features available as part of Microsoft Dynamics 365. This introduction includes a discussion about the digital transformation and the need to create modern, modular business applications that work together on a single platform. Finally, we learned about the different business applications available as part of customer engagement, finance and operations, and the Power Platform.', CAST(N'2020-04-27' AS Date), CAST(N'2020-04-27' AS Date), NULL, N'Approval', N'0349000001', N'Fahad', 4, CAST(N'2020-04-27 17:36:13.997' AS DateTime), NULL, CAST(N'2020-04-27 12:37:18.983' AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[Classified] OFF
SET IDENTITY_INSERT [dbo].[News] ON 

INSERT [dbo].[News] ([Id], [Type], [Heading], [Description], [IssueDate], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (1, N'Breaking News', N'New York to redistribute ventilators amid shortage', N'New York saw its highest single-day increase in deaths, up by 562 to 2,935 - nearly half of all virus-related US deaths recorded yesterday.

The White House may advise those in virus hotspots to wear face coverings in public to help stem the spread.

The US now has 245,658 Covid-19 cases.

A shortage of several hundred ventilators in New York City, the epicentre of the outbreak in the US, prompted Mr Cuomo to say that he will order the machines be taken from various parts of the state and give them to harder-hit areas.111122', CAST(N'2020-04-03 23:33:06.060' AS DateTime), 1, CAST(N'2020-04-03 18:39:59.000' AS DateTime), 2, CAST(N'2020-04-09 12:06:15.760' AS DateTime), 1, 1)
INSERT [dbo].[News] ([Id], [Type], [Heading], [Description], [IssueDate], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (2, N'Normal', N'Should more of us wear face masks to help slow the spread of coronavirus?', N'Should more of us wear face masks to help slow the spread of coronavirus?1', CAST(N'2020-04-03 23:41:05.003' AS DateTime), 2, CAST(N'2020-04-03 18:43:35.513' AS DateTime), 1, CAST(N'2020-04-03 19:05:34.817' AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[News] OFF
SET IDENTITY_INSERT [dbo].[Notice] ON 

INSERT [dbo].[Notice] ([Id], [Name], [CategoryId], [Content], [Duration], [StartDate], [StartTime], [EndDate], [EndTime], [Expire], [Playlist], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (1, N'Admin Notice name', 1, N'<p><u><em><strong>My Name is Muhammad Fahad</strong></em></u></p>
', 15, CAST(N'2020-04-27' AS Date), CAST(N'23:00:00' AS Time), CAST(N'2020-04-27' AS Date), CAST(N'11:59:00' AS Time), 0, NULL, 2, CAST(N'2020-04-27 16:49:47.417' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[Notice] ([Id], [Name], [CategoryId], [Content], [Duration], [StartDate], [StartTime], [EndDate], [EndTime], [Expire], [Playlist], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (2, N'User Notice name', 1, N'<p><em><strong>MY name is user testing1</strong></em></p>
', 13, CAST(N'2020-04-27' AS Date), CAST(N'11:59:00' AS Time), CAST(N'2020-04-27' AS Date), CAST(N'11:59:00' AS Time), 0, NULL, 4, CAST(N'2020-04-27 17:08:43.463' AS DateTime), 4, CAST(N'2020-04-27 17:26:12.130' AS DateTime), 1, 0)
INSERT [dbo].[Notice] ([Id], [Name], [CategoryId], [Content], [Duration], [StartDate], [StartTime], [EndDate], [EndTime], [Expire], [Playlist], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (3, N'Salman', 2, N'<p><u><em><strong>Salman AC Add</strong></em></u></p>
', 20, CAST(N'2020-04-27' AS Date), CAST(N'10:00:00' AS Time), CAST(N'2020-04-27' AS Date), CAST(N'23:00:00' AS Time), 0, NULL, 4, CAST(N'2020-04-27 17:23:48.867' AS DateTime), 4, CAST(N'2020-04-27 17:26:56.327' AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[Notice] OFF
SET IDENTITY_INSERT [dbo].[NoticePlaylist] ON 

INSERT [dbo].[NoticePlaylist] ([Id], [PlaylistId], [ClassifiedId], [NoticeId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (1, 2, 1, NULL, 4, CAST(N'2020-04-27 17:36:14.253' AS DateTime), 4, CAST(N'2020-04-27 17:37:15.250' AS DateTime), 1, 1)
INSERT [dbo].[NoticePlaylist] ([Id], [PlaylistId], [ClassifiedId], [NoticeId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (2, 3, 1, NULL, 4, CAST(N'2020-04-27 17:36:14.253' AS DateTime), 4, CAST(N'2020-04-27 17:37:15.250' AS DateTime), 1, 1)
INSERT [dbo].[NoticePlaylist] ([Id], [PlaylistId], [ClassifiedId], [NoticeId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (3, 2, 1, NULL, 4, CAST(N'2020-04-27 17:36:14.253' AS DateTime), 4, CAST(N'2020-04-27 17:37:15.250' AS DateTime), 1, 1)
INSERT [dbo].[NoticePlaylist] ([Id], [PlaylistId], [ClassifiedId], [NoticeId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (4, 3, 1, NULL, 4, CAST(N'2020-04-27 17:36:14.253' AS DateTime), 4, CAST(N'2020-04-27 17:37:15.250' AS DateTime), 1, 1)
INSERT [dbo].[NoticePlaylist] ([Id], [PlaylistId], [ClassifiedId], [NoticeId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (5, 2, 1, NULL, 4, CAST(N'2020-04-27 17:37:22.883' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[NoticePlaylist] ([Id], [PlaylistId], [ClassifiedId], [NoticeId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (6, 3, 1, NULL, 4, CAST(N'2020-04-27 17:37:25.173' AS DateTime), NULL, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[NoticePlaylist] OFF
SET IDENTITY_INSERT [dbo].[Playlist] ON 

INSERT [dbo].[Playlist] ([Id], [Name], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (1, N'Admin User Playlist', 2, CAST(N'2020-04-27 11:48:34.487' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[Playlist] ([Id], [Name], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (2, N'User Playlist New', 4, CAST(N'2020-04-27 11:48:34.487' AS DateTime), 4, CAST(N'2020-04-27 12:22:58.467' AS DateTime), 1, 0)
INSERT [dbo].[Playlist] ([Id], [Name], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (3, N'New User playlist for Building B', 4, CAST(N'2020-04-27 12:22:49.400' AS DateTime), NULL, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[Playlist] OFF
SET IDENTITY_INSERT [dbo].[PlaylistBuilding] ON 

INSERT [dbo].[PlaylistBuilding] ([Id], [BuildingId], [PlaylistId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (1, 1, 1, 2, CAST(N'2020-04-27 16:48:34.503' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[PlaylistBuilding] ([Id], [BuildingId], [PlaylistId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (2, 2, 2, 4, CAST(N'2020-04-27 16:56:46.703' AS DateTime), 4, CAST(N'2020-04-27 16:56:55.470' AS DateTime), 1, 1)
INSERT [dbo].[PlaylistBuilding] ([Id], [BuildingId], [PlaylistId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (3, 2, 2, 4, CAST(N'2020-04-27 16:56:46.703' AS DateTime), 4, CAST(N'2020-04-27 16:56:55.470' AS DateTime), 1, 1)
INSERT [dbo].[PlaylistBuilding] ([Id], [BuildingId], [PlaylistId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (4, 2, 2, 4, CAST(N'2020-04-27 16:56:55.517' AS DateTime), 4, CAST(N'2020-04-27 16:59:39.460' AS DateTime), 1, 1)
INSERT [dbo].[PlaylistBuilding] ([Id], [BuildingId], [PlaylistId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (5, 2, 2, 4, CAST(N'2020-04-27 17:06:12.370' AS DateTime), 4, CAST(N'2020-04-27 17:22:58.447' AS DateTime), 1, 1)
INSERT [dbo].[PlaylistBuilding] ([Id], [BuildingId], [PlaylistId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (6, 2, 3, 4, CAST(N'2020-04-27 17:22:49.483' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[PlaylistBuilding] ([Id], [BuildingId], [PlaylistId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (7, 2, 2, 4, CAST(N'2020-04-27 17:06:12.370' AS DateTime), 4, CAST(N'2020-04-27 17:22:58.447' AS DateTime), 1, 1)
INSERT [dbo].[PlaylistBuilding] ([Id], [BuildingId], [PlaylistId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (8, 1, 2, 4, CAST(N'2020-04-27 17:22:58.470' AS DateTime), NULL, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[PlaylistBuilding] OFF
SET IDENTITY_INSERT [dbo].[ResgisterUser] ON 

INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (1, N'Aciano', N'1', N'Bernice Pittman1', N'ezatogdi1@niribrur.es', N'23233', N'seq+0011', NULL, CAST(N'2020-04-18 00:48:37.907' AS DateTime), 2, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (2, N'Aciano', N'2', N'Myrtie Boyd', N'odo@hiz.cr', N'(415) 625-3364', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (3, N'Aciano', N'3', N'Lewis Norman', N'tetwi@af.ax', N'(432) 762-5434', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (4, N'Aciano', N'4', N'Mabelle Cortez', N'sah@pasralbe.gg', N'(360) 752-3189', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (5, N'Aciano', N'5', N'Julian Foster', N'tafredu@sagos.pt', N'(717) 379-8190', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (6, N'Aciano', N'6', N'Fanny McGuire', N'pegudofoc@zigmom.tc', N'(363) 212-3413', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (7, N'Aciano', N'7', N'Mabelle Tucker', N'sekjof@houlu.kr', N'(448) 558-7017', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (8, N'Aciano', N'8', N'Johnny Warren', N'da@wekverwu.qa', N'(830) 902-9698', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (9, N'Aciano', N'9', N'Leonard Foster', N'vodo@sucibhis.bd', N'(307) 212-5391', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (10, N'Aciano', N'10', N'Vernon Baker', N'viuraen@ubcov.tn', N'(337) 827-2699', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (11, N'Aciano', N'11', N'Emma Lowe', N'ohemavak@vig.st', N'(316) 378-8487', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (12, N'Aciano', N'12', N'Lee Vega', N'davuh@mewcodzon.gt', N'(413) 781-6465', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (13, N'Aciano', N'13', N'Rhoda Ramos', N'nu@et.fj', N'(969) 612-4018', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (14, N'Aciano', N'14', N'Donald Powell', N'kojfopgip@iwke.bg', N'(200) 901-2901', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (15, N'Aciano', N'15', N'Hulda Shaw', N'akve@op.vn', N'(818) 340-1313', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (16, N'Aciano', N'16', N'Flora Johnson', N'jarkasnan@soocje.gd', N'(914) 661-9184', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (17, N'Aciano', N'17', N'Randy Henry', N'bum@fesulo.aq', N'(501) 924-1601', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (18, N'Aciano', N'18', N'Jeffrey Barrett', N'firzues@vazeno.lr', N'(531) 866-8178', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (19, N'Aciano', N'19', N'Mattie Henderson', N'kicarlos@jicet.sd', N'(850) 763-9077', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (20, N'Aciano', N'20', N'Etta Sutton', N'wivabo@bemi.sn', N'(541) 239-7363', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (21, N'Aciano', N'21', N'Gerald Todd', N'muwhejpan@levmuj.gov', N'(905) 522-9740', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (22, N'Aciano', N'22', N'Jacob Hansen', N'piwa@ulnapako.lc', N'(743) 883-8478', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (23, N'Aciano', N'23', N'Bill Massey', N'ir@zatehi.cc', N'(803) 535-5043', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (24, N'Aciano', N'24', N'Susan Lawson', N'osipofi@bom.br', N'(580) 666-4607', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (25, N'Aciano', N'25', N'Raymond Gordon', N'iled@zavebse.gb', N'(237) 805-9792', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (26, N'Aciano', N'26', N'Dale Blake', N'lud@te.gov', N'(367) 981-8697', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (27, N'Aciano', N'27', N'Agnes Rodriquez', N'nuf@anki.cw', N'(230) 708-5230', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (28, N'Aciano', N'28', N'Agnes Patrick', N'kerfe@wovoguw.gb', N'(686) 290-5801', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (29, N'Aciano', N'29', N'Barry Erickson', N'tobowow@uka.us', N'(718) 560-3252', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (30, N'Aciano', N'30', N'Arthur Figueroa', N'amfuwbaf@dehega.ua', N'(709) 762-9273', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (31, N'Aciano', N'31', N'Sophia Mendoza', N'seuw@pabetu.an', N'(810) 879-3449', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (32, N'Aciano', N'32', N'Louis Morales', N'limauc@javfenwu.ir', N'(703) 471-8115', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (33, N'Aciano', N'33', N'Myrtie Higgins', N'cif@vamalubiw.hr', N'(681) 854-3870', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (34, N'Aciano', N'34', N'Keith Caldwell', N'ze@noc.lv', N'(305) 402-5966', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (35, N'Aciano', N'35', N'Estelle Ford', N'otfe@riot.ki', N'(529) 614-9248', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (36, N'Aciano', N'36', N'Viola Scott', N'tigih@av.kp', N'(685) 818-4353', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (37, N'Aciano', N'37', N'Katie Lindsey', N'jide@vurviv.eg', N'(535) 857-5706', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (38, N'Aciano', N'38', N'Susan Jimenez', N'muzo@sizebju.mc', N'(458) 452-5355', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (39, N'Aciano', N'39', N'Rosetta Schmidt', N'timzeoz@mig.bs', N'(583) 920-6960', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (40, N'Aciano', N'40', N'Kenneth Gordon', N'itazac@cit.sn', N'(383) 840-5952', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (41, N'Aciano', N'41', N'Mamie Glover', N'et@no.ao', N'(527) 522-8636', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (42, N'Aciano', N'42', N'Gene Richardson', N'nazo@ewi.id', N'(618) 998-5520', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (43, N'Aciano', N'43', N'Roy Potter', N'paezo@zobvuak.bi', N'(345) 903-9018', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (44, N'Aciano', N'44', N'Ivan Webb', N'ezidu@dujju.vc', N'(882) 231-6594', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (45, N'Aciano', N'45', N'Mary Walton', N'ugolar@ucubijic.lb', N'(966) 687-5113', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (46, N'Aciano', N'46', N'Sam Mathis', N'feguev@sihkaef.gy', N'(247) 599-4815', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (47, N'Aciano', N'47', N'Ida Bennett', N'dap@ti.ve', N'(408) 736-7848', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (48, N'Aciano', N'48', N'Joshua Lee', N'hunvi@ogegotniz.ci', N'(712) 952-2806', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (49, N'Aciano', N'49', N'Luis Sanders', N'uku@ko.ae', N'(408) 268-1524', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (50, N'Aciano', N'50', N'Beatrice Owen', N'beja@si.aw', N'(632) 556-2083', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (51, N'Aciano', N'51', N'Bruce Reed', N'apinefo@pen.bo', N'(371) 969-6010', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (52, N'Aciano', N'52', N'Luke Pittman', N'bagazov@nir.ke', N'(987) 519-6725', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (53, N'Aciano', N'53', N'Marian Abbott', N'pukkub@mik.bw', N'(226) 494-4617', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (54, N'Aciano', N'54', N'Julian Perry', N'za@iferilu.fr', N'(445) 811-4543', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (55, N'Aciano', N'55', N'Melvin Curtis', N'goafaga@wi.uy', N'(720) 576-5728', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (56, N'Aciano', N'56', N'Willie Brooks', N'puken@ubairuis.pf', N'(327) 690-1936', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (57, N'Aciano', N'57', N'Sue Adkins', N'bojaprik@ni.cl', N'(348) 992-3936', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (58, N'Aciano', N'58', N'Dean Byrd', N'dumelje@wenuluri.sz', N'(327) 262-9352', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (59, N'Aciano', N'59', N'Matilda Morgan', N'hihveti@ofefilow.tt', N'(310) 298-2309', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (60, N'Aciano', N'60', N'Adam Brock', N'ibelaju@epibemtoc.hr', N'(874) 634-2883', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (61, N'Aciano', N'61', N'Ola Griffin', N'ofse@masru.th', N'(456) 422-8853', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (62, N'Aciano', N'62', N'Clarence Steele', N'owefla@gij.fi', N'(349) 341-2843', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (63, N'Aciano', N'63', N'Mark Parsons', N'habeh@kaj.mt', N'(315) 684-4534', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (64, N'Aciano', N'64', N'Ann Casey', N'ec@huz.sm', N'(787) 426-2389', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (65, N'Aciano', N'65', N'Amy Coleman', N'nivopu@fodtaz.pg', N'(770) 601-8473', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (66, N'Aciano', N'66', N'Franklin Stephens', N'du@daarmuj.id', N'(245) 712-5705', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (67, N'Aciano', N'67', N'Belle Simmons', N'ig@nev.gov', N'(444) 211-6705', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (68, N'Aciano', N'68', N'Pauline Aguilar', N'ved@nizaeji.cm', N'(364) 894-3181', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (69, N'Aciano', N'69', N'Ralph Perez', N'zivico@zohuhu.km', N'(232) 356-9870', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (70, N'Aciano', N'70', N'Alan Baker', N'loikufu@la.tn', N'(918) 826-5459', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (71, N'Aciano', N'71', N'Jean Reese', N'javwem@wut.na', N'(738) 253-7924', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (72, N'Aciano', N'72', N'Evan Herrera', N'ictokbet@asawo.sz', N'(763) 354-5170', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (73, N'Aciano', N'73', N'Rosalie Carpenter', N'diwsom@lopumsap.net', N'(980) 664-5997', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (74, N'Aciano', N'74', N'Andrew Dean', N'cenuec@vecew.sa', N'(701) 613-7779', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (75, N'Aciano', N'75', N'Ruby Wong', N'guneze@fat.mv', N'(386) 625-8753', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (76, N'Aciano', N'76', N'Ella Sparks', N'uwotubce@rotu.cy', N'(818) 532-6362', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (77, N'Aciano', N'77', N'Howard Dixon', N'ru@vuwecijam.th', N'(850) 761-5075', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (78, N'Aciano', N'78', N'Helen Wheeler', N'saubweb@eg.va', N'(220) 219-3167', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (79, N'Aciano', N'79', N'Leona Blair', N'effodvo@fotteru.mt', N'(543) 291-5743', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (80, N'Aciano', N'80', N'Jesse Alvarado', N'tidni@ov.bb', N'(976) 247-3958', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (81, N'Aciano', N'81', N'Lois Daniel', N'bekigaru@gaz.ne', N'(810) 877-2760', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (82, N'Aciano', N'82', N'Elva Pearson', N'vi@le.sc', N'(310) 448-1821', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (83, N'Aciano', N'83', N'Robert Wolfe', N'raj@cekezi.gu', N'(454) 929-8316', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (84, N'Aciano', N'84', N'Peter Peterson', N'izajoaro@nabu.kn', N'(812) 262-1035', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (85, N'Aciano', N'85', N'Louis Higgins', N'fa@ugaci.pk', N'(217) 706-3943', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (86, N'Aciano', N'86', N'Tyler Buchanan', N'anenir@keele.pn', N'(519) 244-8394', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (87, N'Aciano', N'87', N'Edwin Coleman', N'tute@fo.ph', N'(572) 239-1572', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (88, N'Aciano', N'88', N'Bernard Reyes', N'wu@ziwmawdu.tz', N'(270) 875-2240', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), 2, CAST(N'2020-04-17 19:47:57.187' AS DateTime), 1, 1)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (89, N'Aciano', N'89', N'Teresa Reyes', N'afoubo@sa.sa', N'(770) 878-1803', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (90, N'Aciano', N'90', N'Katherine Mendoza', N'ac@ceuzgo.eu', N'(347) 920-1170', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (91, N'Aciano', N'91', N'Marian McKenzie', N'abfo@loofge.lt', N'(268) 786-2380', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (92, N'Aciano', N'92', N'Violet Adams', N'ti@gaca.cr', N'(864) 731-7879', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (93, N'Aciano', N'93', N'Curtis Dawson', N'wokit@taz.nz', N'(653) 738-3464', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (94, N'Aciano', N'94', N'Warren Wise', N'cilavi@ejgoh.sl', N'(285) 989-7219', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (95, N'Aciano', N'95', N'Sallie Byrd', N'sek@jepopiz.fj', N'(222) 712-6230', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (96, N'Aciano', N'96', N'Philip Owens', N'lihis@zewero.lr', N'(825) 339-8342', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (97, N'Aciano', N'97', N'Luis Holland', N'gom@geshal.tw', N'(771) 721-3373', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (98, N'Aciano', N'98', N'Katie Lane', N'evo@uhukobil.pt', N'(618) 572-8897', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (99, N'Aciano', N'99', N'Juan Quinn', N'cugoz@tuploed.ad', N'(775) 212-2228', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
GO
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (100, N'Aciano', N'100', N'Virgie Drake', N'jowvo@puvuse.ie', N'(423) 674-4547', N'seq+001', 2, CAST(N'2020-04-18 00:47:33.530' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[ResgisterUser] ([Id], [TeamName], [SerialNo], [Name], [Email], [Phone], [RegistrationNumber], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (101, N'HDK', N'10210', N'Fhaad', N'fahad@gmail.com', N'1245642', N'45616461', 2, CAST(N'2020-04-30 17:27:11.903' AS DateTime), NULL, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[ResgisterUser] OFF
INSERT [dbo].[Role] ([Id], [RoleName], [isDel]) VALUES (1, N'Master', 0)
INSERT [dbo].[Role] ([Id], [RoleName], [isDel]) VALUES (2, N'Super Admin', 0)
INSERT [dbo].[Role] ([Id], [RoleName], [isDel]) VALUES (3, N'Admin', 0)
INSERT [dbo].[Role] ([Id], [RoleName], [isDel]) VALUES (4, N'Manager', 0)
INSERT [dbo].[Role] ([Id], [RoleName], [isDel]) VALUES (5, N'User', 0)
SET IDENTITY_INSERT [dbo].[Template] ON 

INSERT [dbo].[Template] ([Id], [CategoryId], [Title], [Description], [Content], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (1, 2, N'Salman', N'Salman Desc', N'<p><u><em><strong>Salman AC Add</strong></em></u></p>
', 2, CAST(N'2020-04-18 00:38:09.207' AS DateTime), NULL, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[Template] OFF
SET IDENTITY_INSERT [dbo].[UploadFile] ON 

INSERT [dbo].[UploadFile] ([Id], [UploadType], [Title], [Desc], [FileName], [FilePath], [Portion], [Sequence], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (1, N'PDF', N'Admin User PDF', N'Admin user pdf upload testing', N'c5f2e14e-596b-4d1e-96fd-8dd3627df3fe.pdf', N'E:\Important\Git\ConnectMedia\ConnectMedia\wwwroot\Pdf\c5f2e14e-596b-4d1e-96fd-8dd3627df3fe.pdf', 0, 0, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UploadFile] ([Id], [UploadType], [Title], [Desc], [FileName], [FilePath], [Portion], [Sequence], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (2, N'Doc', N'Admin User Doc', N'Admin User Doc', N'New Microsoft Word Document.docx', N'E:\Important\Git\ConnectMedia\ConnectMedia\wwwroot\WordDocument\7ec030d5-fe9e-490d-81f9-83e60dbe7805.docx', 0, 0, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UploadFile] ([Id], [UploadType], [Title], [Desc], [FileName], [FilePath], [Portion], [Sequence], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (3, N'Video', N'Video 1', N'Video 1', N'94221239_562516324649411_7350458236851859151_n.mp4', N'E:\Important\Git\ConnectMedia\ConnectMedia\wwwroot\Pdf\94221239_562516324649411_7350458236851859151_n.mp4', 2, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UploadFile] ([Id], [UploadType], [Title], [Desc], [FileName], [FilePath], [Portion], [Sequence], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (4, N'Video', N'Bank', N'Acinao Team', N'50dc2a70-1198-47a1-bb5f-6421dbf29114.mp4', N'E:\Important\Git\ConnectMedia\ConnectMedia\wwwroot\Pdf\50dc2a70-1198-47a1-bb5f-6421dbf29114.mp4', 0, 0, 2, CAST(N'2020-05-02 02:37:14.863' AS DateTime), NULL, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[UploadFile] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [ContactNumber], [Password], [BuildingIds], [RoleId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (1, N'Muhammad', N'Fahad', N'Fahad@gmail.com', N'+92115', N'admin', N'2,1', 1, 1, CAST(N'2020-04-03 17:37:38.757' AS DateTime), NULL, NULL, 1, 0)
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [ContactNumber], [Password], [BuildingIds], [RoleId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (2, N'Syed', N'Taimoor', N'Taimoor@gmail.com', NULL, N'admin', N'1,2', 2, 1, CAST(N'2020-04-03 15:51:33.583' AS DateTime), 1, CAST(N'2020-04-03 19:06:14.377' AS DateTime), 1, 0)
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [ContactNumber], [Password], [BuildingIds], [RoleId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (3, N'Salik', N'Yar Khan', N'Salik@gmail.com', NULL, N'admin', N'2,1', 3, 2, CAST(N'2020-04-03 21:42:30.167' AS DateTime), 2, CAST(N'2020-04-05 21:47:47.450' AS DateTime), 1, 0)
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [ContactNumber], [Password], [BuildingIds], [RoleId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsActive], [IsDel]) VALUES (4, N'Muhammad Fahad', N'Fahad', N'Muhammad.Fahad@aciano.net', N'1234567', N'admin', N'1,2', 5, 2, CAST(N'2020-04-23 12:44:53.270' AS DateTime), 2, CAST(N'2020-04-23 12:49:38.857' AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
