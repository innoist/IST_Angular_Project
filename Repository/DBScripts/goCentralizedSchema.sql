USE [IST_Angular]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Telephone] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[ImageName] [nvarchar](max) NULL,
	[DateOfBirth] [datetime] NULL,
	[Qualification] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Filter]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Filter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DisplayValue] [nvarchar](100) NULL,
	[FilterCategoryId] [int] NOT NULL,
	[RecCreatedOn] [datetime] NOT NULL,
	[RecLastUpdatedById] [nvarchar](128) NOT NULL,
	[RecCreatedById] [nvarchar](128) NOT NULL,
	[RecLastUpdatedOn] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_Filter_IsDelete]  DEFAULT ((0)),
 CONSTRAINT [PK_Filters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FilterCategory]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FilterCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DisplayValue] [nvarchar](100) NULL,
	[RecCreatedById] [nvarchar](128) NOT NULL,
	[RecCreatedOn] [datetime] NOT NULL,
	[RecLastUpdatedById] [nvarchar](128) NOT NULL,
	[RecLastUpdatedOn] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_FilterCategory_IsDeleted]  DEFAULT ((0)),
 CONSTRAINT [PK_FilterCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Menu]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[MenuId] [int] IDENTITY(1,1) NOT NULL,
	[MenuKey] [int] NOT NULL,
	[MenuTitle] [nvarchar](max) NULL,
	[SortOrder] [int] NOT NULL,
	[MenuTargetController] [nvarchar](max) NULL,
	[MenuImagePath] [nvarchar](max) NULL,
	[MenuFunction] [nvarchar](max) NULL,
	[PermissionKey] [nvarchar](max) NULL,
	[IsRootItem] [bit] NOT NULL,
	[ParentItem_MenuId] [int] NULL,
 CONSTRAINT [PK_dbo.Menu] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MenuRight]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuRight](
	[MenuRightId] [int] IDENTITY(1,1) NOT NULL,
	[Menu_MenuId] [int] NULL,
	[Role_Id] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.MenuRight] PRIMARY KEY CLUSTERED 
(
	[MenuRightId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Solution]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Solution](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Location] [nvarchar](100) NOT NULL CONSTRAINT [DF_Solutions_Location]  DEFAULT ((1)),
	[MaintentanceHours] [int] NULL,
	[SecurityInfo] [nvarchar](100) NULL,
	[TypeId] [int] NOT NULL,
	[OwnerId] [int] NOT NULL,
	[Image] [nvarchar](max) NULL,
	[Active] [bit] NULL CONSTRAINT [DF_Solutions_Active]  DEFAULT ((1)),
	[RecCreatedById] [nvarchar](128) NOT NULL,
	[RecCreatedOn] [datetime] NOT NULL,
	[RecLastUpdatedById] [nvarchar](128) NOT NULL,
	[RecLastUpdatedOn] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_Solution_IsDeleted]  DEFAULT ((0)),
 CONSTRAINT [PK_dbo.Projects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SolutionFavourite]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolutionFavourite](
	[SolutionId] [int] NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_FavouriteSolution_1] PRIMARY KEY CLUSTERED 
(
	[SolutionId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SolutionFilter]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolutionFilter](
	[SolutionId] [int] NOT NULL,
	[FilterId] [int] NOT NULL,
 CONSTRAINT [PK_SolutionFilter] PRIMARY KEY CLUSTERED 
(
	[SolutionId] ASC,
	[FilterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SolutionOwner]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolutionOwner](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DisplayValue] [nvarchar](100) NULL,
	[RecCreatedOn] [datetime] NOT NULL,
	[RecLastUpdatedById] [nvarchar](128) NOT NULL,
	[RecCreatedById] [nvarchar](128) NOT NULL,
	[RecLastUpdatedOn] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_SolutionOwner_IsDeleted_1]  DEFAULT ((0)),
 CONSTRAINT [PK_SolutionOwners] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SolutionRating]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolutionRating](
	[RatingId] [int] IDENTITY(1,1) NOT NULL,
	[SolutionId] [int] NOT NULL,
	[Rating] [int] NULL,
	[Comments] [nvarchar](max) NULL,
	[IsReply] [bit] NULL,
	[ReplyParentId] [int] NULL,
	[RecCreatedById] [nvarchar](128) NOT NULL,
	[RecCreatedOn] [datetime] NOT NULL,
	[RecLastUpdatedById] [nvarchar](128) NOT NULL,
	[RecLastUpdatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_SolutionRating] PRIMARY KEY CLUSTERED 
(
	[RatingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SolutionSearchHistory]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolutionSearchHistory](
	[SolutionSearchHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[SearchString] [nvarchar](100) NOT NULL,
	[SearchTime] [datetime] NOT NULL,
	[RecCreatedById] [nvarchar](128) NOT NULL,
	[RecCreatedOn] [datetime] NOT NULL,
	[RecLastUpdatedById] [nvarchar](128) NOT NULL,
	[RecLastUpdatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_SolutionSearchHistory] PRIMARY KEY CLUSTERED 
(
	[SolutionSearchHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SolutionTag]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolutionTag](
	[SolutionId] [int] NOT NULL,
	[TagId] [int] NOT NULL,
 CONSTRAINT [PK_SolutionTag] PRIMARY KEY CLUSTERED 
(
	[SolutionId] ASC,
	[TagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SolutionType]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolutionType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DisplayValue] [nvarchar](100) NULL,
	[RecCreatedById] [nvarchar](128) NOT NULL,
	[RecCreatedOn] [datetime] NOT NULL,
	[RecLastUpdatedById] [nvarchar](128) NOT NULL,
	[RecLastUpdatedOn] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_SolutionType_IsDeleted_1]  DEFAULT ((0)),
 CONSTRAINT [PK_SolutionTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SolutionUsageHistory]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolutionUsageHistory](
	[SolutionUsageHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[SolutionId] [int] NOT NULL,
	[UsageType] [int] NOT NULL,
	[UsageTime] [datetime] NOT NULL,
	[RecCreatedById] [nvarchar](128) NOT NULL,
	[RecCreatedOn] [datetime] NOT NULL,
	[RecLastUpdatedById] [nvarchar](128) NOT NULL,
	[RecLastUpdatedOn] [datetime] NOT NULL,
	[SentTo] [nvarchar](50) NULL,
	[IsOpened] [bit] NOT NULL CONSTRAINT [DF_SolutionUsageHistory_IsOpened]  DEFAULT ((0)),
 CONSTRAINT [PK_SolutionUsageHistory] PRIMARY KEY CLUSTERED 
(
	[SolutionUsageHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tag]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tag](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DisplayValue] [nvarchar](max) NULL,
	[TagGroupId] [int] NOT NULL,
	[RecCreatedOn] [datetime] NOT NULL,
	[RecLastUpdatedById] [nvarchar](128) NOT NULL,
	[RecCreatedById] [nvarchar](128) NOT NULL,
	[RecLastUpdatedOn] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_Tag_IsDeleted]  DEFAULT ((0)),
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TagGroup]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TagGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DisplayValue] [nvarchar](max) NULL,
	[RecCreatedOn] [datetime] NOT NULL,
	[RecLastUpdatedById] [nvarchar](128) NOT NULL,
	[RecCreatedById] [nvarchar](128) NOT NULL,
	[RecLastUpdatedOn] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_TagGroup_IsDeleted]  DEFAULT ((0)),
 CONSTRAINT [PK_TagGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Filter]  WITH CHECK ADD  CONSTRAINT [FK_Filter_AspNetUsers_CreatedById] FOREIGN KEY([RecCreatedById])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Filter] CHECK CONSTRAINT [FK_Filter_AspNetUsers_CreatedById]
GO
ALTER TABLE [dbo].[Filter]  WITH CHECK ADD  CONSTRAINT [FK_Filter_AspNetUsers_UpdatedById] FOREIGN KEY([RecLastUpdatedById])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Filter] CHECK CONSTRAINT [FK_Filter_AspNetUsers_UpdatedById]
GO
ALTER TABLE [dbo].[Filter]  WITH CHECK ADD  CONSTRAINT [FK_Filters_FilterCategories] FOREIGN KEY([FilterCategoryId])
REFERENCES [dbo].[FilterCategory] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Filter] CHECK CONSTRAINT [FK_Filters_FilterCategories]
GO
ALTER TABLE [dbo].[FilterCategory]  WITH CHECK ADD  CONSTRAINT [FK_FilterCategory_AspNetUsers_CreatedById] FOREIGN KEY([RecCreatedById])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[FilterCategory] CHECK CONSTRAINT [FK_FilterCategory_AspNetUsers_CreatedById]
GO
ALTER TABLE [dbo].[FilterCategory]  WITH CHECK ADD  CONSTRAINT [FK_FilterCategory_AspNetUsers_UpdatedById] FOREIGN KEY([RecLastUpdatedById])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[FilterCategory] CHECK CONSTRAINT [FK_FilterCategory_AspNetUsers_UpdatedById]
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Menu_dbo.Menu_ParentItem_MenuId] FOREIGN KEY([ParentItem_MenuId])
REFERENCES [dbo].[Menu] ([MenuId])
GO
ALTER TABLE [dbo].[Menu] CHECK CONSTRAINT [FK_dbo.Menu_dbo.Menu_ParentItem_MenuId]
GO
ALTER TABLE [dbo].[MenuRight]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MenuRight_dbo.AspNetRoles_Role_Id] FOREIGN KEY([Role_Id])
REFERENCES [dbo].[AspNetRoles] ([Id])
GO
ALTER TABLE [dbo].[MenuRight] CHECK CONSTRAINT [FK_dbo.MenuRight_dbo.AspNetRoles_Role_Id]
GO
ALTER TABLE [dbo].[MenuRight]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MenuRight_dbo.Menu_Menu_MenuId] FOREIGN KEY([Menu_MenuId])
REFERENCES [dbo].[Menu] ([MenuId])
GO
ALTER TABLE [dbo].[MenuRight] CHECK CONSTRAINT [FK_dbo.MenuRight_dbo.Menu_Menu_MenuId]
GO
ALTER TABLE [dbo].[Solution]  WITH CHECK ADD  CONSTRAINT [FK_Solution_AspNetUsers_CreatedById] FOREIGN KEY([RecCreatedById])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Solution] CHECK CONSTRAINT [FK_Solution_AspNetUsers_CreatedById]
GO
ALTER TABLE [dbo].[Solution]  WITH CHECK ADD  CONSTRAINT [FK_Solution_AspNetUsers_UpdatedById] FOREIGN KEY([RecLastUpdatedById])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Solution] CHECK CONSTRAINT [FK_Solution_AspNetUsers_UpdatedById]
GO
ALTER TABLE [dbo].[Solution]  WITH CHECK ADD  CONSTRAINT [FK_Solutions_SolutionOwners] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[SolutionOwner] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Solution] CHECK CONSTRAINT [FK_Solutions_SolutionOwners]
GO
ALTER TABLE [dbo].[Solution]  WITH CHECK ADD  CONSTRAINT [FK_Solutions_SolutionTypes] FOREIGN KEY([TypeId])
REFERENCES [dbo].[SolutionType] ([Id])
GO
ALTER TABLE [dbo].[Solution] CHECK CONSTRAINT [FK_Solutions_SolutionTypes]
GO
ALTER TABLE [dbo].[SolutionFavourite]  WITH CHECK ADD  CONSTRAINT [FK_FavouriteSolution_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[SolutionFavourite] CHECK CONSTRAINT [FK_FavouriteSolution_AspNetUsers]
GO
ALTER TABLE [dbo].[SolutionFavourite]  WITH CHECK ADD  CONSTRAINT [FK_FavouriteSolution_Solution] FOREIGN KEY([SolutionId])
REFERENCES [dbo].[Solution] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SolutionFavourite] CHECK CONSTRAINT [FK_FavouriteSolution_Solution]
GO
ALTER TABLE [dbo].[SolutionFilter]  WITH CHECK ADD  CONSTRAINT [FK_SolutionFilter_Filter] FOREIGN KEY([FilterId])
REFERENCES [dbo].[Filter] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SolutionFilter] CHECK CONSTRAINT [FK_SolutionFilter_Filter]
GO
ALTER TABLE [dbo].[SolutionFilter]  WITH CHECK ADD  CONSTRAINT [FK_SolutionFilter_Solution] FOREIGN KEY([SolutionId])
REFERENCES [dbo].[Solution] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SolutionFilter] CHECK CONSTRAINT [FK_SolutionFilter_Solution]
GO
ALTER TABLE [dbo].[SolutionOwner]  WITH CHECK ADD  CONSTRAINT [FK_SolutionOwner_AspNetUsers_CreatedById] FOREIGN KEY([RecCreatedById])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[SolutionOwner] CHECK CONSTRAINT [FK_SolutionOwner_AspNetUsers_CreatedById]
GO
ALTER TABLE [dbo].[SolutionOwner]  WITH CHECK ADD  CONSTRAINT [FK_SolutionOwner_AspNetUsers_UpdatedById] FOREIGN KEY([RecLastUpdatedById])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[SolutionOwner] CHECK CONSTRAINT [FK_SolutionOwner_AspNetUsers_UpdatedById]
GO
ALTER TABLE [dbo].[SolutionRating]  WITH CHECK ADD  CONSTRAINT [FK_SolutionRating_Solution] FOREIGN KEY([SolutionId])
REFERENCES [dbo].[Solution] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SolutionRating] CHECK CONSTRAINT [FK_SolutionRating_Solution]
GO
ALTER TABLE [dbo].[SolutionSearchHistory]  WITH CHECK ADD  CONSTRAINT [FK_SolutionSearchHistory_AspNetUsersCreated] FOREIGN KEY([RecCreatedById])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[SolutionSearchHistory] CHECK CONSTRAINT [FK_SolutionSearchHistory_AspNetUsersCreated]
GO
ALTER TABLE [dbo].[SolutionSearchHistory]  WITH CHECK ADD  CONSTRAINT [FK_SolutionSearchHistory_AspNetUsersUpdated] FOREIGN KEY([RecLastUpdatedById])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[SolutionSearchHistory] CHECK CONSTRAINT [FK_SolutionSearchHistory_AspNetUsersUpdated]
GO
ALTER TABLE [dbo].[SolutionTag]  WITH CHECK ADD  CONSTRAINT [FK_SolutionTag_Solution] FOREIGN KEY([SolutionId])
REFERENCES [dbo].[Solution] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SolutionTag] CHECK CONSTRAINT [FK_SolutionTag_Solution]
GO
ALTER TABLE [dbo].[SolutionTag]  WITH CHECK ADD  CONSTRAINT [FK_SolutionTag_Tag] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tag] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SolutionTag] CHECK CONSTRAINT [FK_SolutionTag_Tag]
GO
ALTER TABLE [dbo].[SolutionType]  WITH CHECK ADD  CONSTRAINT [FK_SolutionType_AspNetUsers_CreatedById] FOREIGN KEY([RecCreatedById])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[SolutionType] CHECK CONSTRAINT [FK_SolutionType_AspNetUsers_CreatedById]
GO
ALTER TABLE [dbo].[SolutionType]  WITH CHECK ADD  CONSTRAINT [FK_SolutionType_AspNetUsers_UpdatedById] FOREIGN KEY([RecLastUpdatedById])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[SolutionType] CHECK CONSTRAINT [FK_SolutionType_AspNetUsers_UpdatedById]
GO
ALTER TABLE [dbo].[SolutionUsageHistory]  WITH CHECK ADD  CONSTRAINT [FK_SolutionUsageHistory_AspNetUsers] FOREIGN KEY([RecCreatedById])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[SolutionUsageHistory] CHECK CONSTRAINT [FK_SolutionUsageHistory_AspNetUsers]
GO
ALTER TABLE [dbo].[SolutionUsageHistory]  WITH CHECK ADD  CONSTRAINT [FK_SolutionUsageHistory_AspNetUsersUpdated] FOREIGN KEY([RecLastUpdatedById])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[SolutionUsageHistory] CHECK CONSTRAINT [FK_SolutionUsageHistory_AspNetUsersUpdated]
GO
ALTER TABLE [dbo].[SolutionUsageHistory]  WITH CHECK ADD  CONSTRAINT [FK_SolutionUsageHistory_Solution] FOREIGN KEY([SolutionId])
REFERENCES [dbo].[Solution] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SolutionUsageHistory] CHECK CONSTRAINT [FK_SolutionUsageHistory_Solution]
GO
ALTER TABLE [dbo].[Tag]  WITH CHECK ADD  CONSTRAINT [FK_Tag_AspNetUsers_CreatedById] FOREIGN KEY([RecCreatedById])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Tag] CHECK CONSTRAINT [FK_Tag_AspNetUsers_CreatedById]
GO
ALTER TABLE [dbo].[Tag]  WITH CHECK ADD  CONSTRAINT [FK_Tag_AspNetUsers_UpdatedById] FOREIGN KEY([RecLastUpdatedById])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Tag] CHECK CONSTRAINT [FK_Tag_AspNetUsers_UpdatedById]
GO
ALTER TABLE [dbo].[Tag]  WITH CHECK ADD  CONSTRAINT [FK_Tags_TagGroups] FOREIGN KEY([TagGroupId])
REFERENCES [dbo].[TagGroup] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tag] CHECK CONSTRAINT [FK_Tags_TagGroups]
GO
ALTER TABLE [dbo].[TagGroup]  WITH CHECK ADD  CONSTRAINT [FK_TagGroup_AspNetUsers_CreatedById] FOREIGN KEY([RecCreatedById])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[TagGroup] CHECK CONSTRAINT [FK_TagGroup_AspNetUsers_CreatedById]
GO
ALTER TABLE [dbo].[TagGroup]  WITH CHECK ADD  CONSTRAINT [FK_TagGroup_AspNetUsers_UpdatedById] FOREIGN KEY([RecLastUpdatedById])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[TagGroup] CHECK CONSTRAINT [FK_TagGroup_AspNetUsers_UpdatedById]
GO
/****** Object:  StoredProcedure [dbo].[DeleteFilter]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE  PROCEDURE [dbo].[DeleteFilter](@DeleteType bit, @FilterId int)

AS
if(@DeleteType=1)

	BEGIN
	delete from dbo.SolutionFilter
	where FilterId = @FilterId and SolutionId in (select Id from dbo.Solution where Id in (select SolutionId from SolutionFilter where FilterId = @FilterId))

	delete from dbo.Filter
	where Id = @FilterId

	delete from dbo.Solution
	where Id in (select SolutionId from SolutionFilter where FilterId = @FilterId)

	End
else if(@DeleteType=0)

	BEGIN
	Update Filter
	set IsDeleted = 1
	where Id = @FilterId

	Update Solution
	set IsDeleted = 1
	where Id in (select SolutionId from SolutionFilter where FilterId = @FilterId)
	End

GO
/****** Object:  StoredProcedure [dbo].[DeleteFilterCategory]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





Create  PROCEDURE [dbo].[DeleteFilterCategory](@DeleteType bit, @FilterCategoryId int)

AS
if(@DeleteType=0)
	BEGIN

	Update FilterCategory
	set IsDeleted = 1
	where Id = @FilterCategoryId

	Update Filter
	set IsDeleted = 1
	where FilterCategoryId = @FilterCategoryId

	Update Solution
	set IsDeleted = 1
	where Id in
	(
	 select SolutionId from SolutionFilter
	 where FilterId in 
	  (select Id from Filter where FilterCategoryId = @FilterCategoryId)
	 )
	End
else if(@DeleteType=1)
	BEGIN
	
	delete from dbo.SolutionFilter
	where FilterId in (select Id from dbo.Filter where FilterCategoryId = @FilterCategoryId)

	delete from dbo.Filter
	where FilterCategoryId = @FilterCategoryId
	
	delete from dbo.Solution
	where Id in
	(
	 select SolutionId from SolutionFilter
	 where FilterId in 
	  (select Id from Filter where FilterCategoryId = @FilterCategoryId)
	 )
	 
	delete from dbo.FilterCategory
	where Id = @FilterCategoryId

	End



GO
/****** Object:  StoredProcedure [dbo].[RemoveFilter]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE  PROCEDURE [dbo].[RemoveFilter](@FilterId int)

AS

BEGIN

Update Filter
set IsDeleted = 1
where Id = @FilterId

Update Solution
set IsDeleted = 1
where Id in (select SolutionId from SolutionFilter where FilterId = @FilterId)

End


GO
/****** Object:  StoredProcedure [dbo].[RemoveFilterCategory]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




Create  PROCEDURE [dbo].[RemoveFilterCategory](@FilterCategoryId int)

AS

BEGIN

Update FilterCategory
set IsDeleted = 1
where Id = @FilterCategoryId

Update Filter
set IsDeleted = 1
where FilterCategoryId = @FilterCategoryId

Update Solution
set IsDeleted = 1
where Id in
(
 select SolutionId from SolutionFilter
 where FilterId in 
  (select Id from Filter where FilterCategoryId = @FilterCategoryId)
 )


End


GO
/****** Object:  StoredProcedure [dbo].[RemoveSolutionOwner]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE  PROCEDURE [dbo].[RemoveSolutionOwner](@OwnerId int)

AS

BEGIN

Update SolutionOwner
set IsDeleted = 1
where Id = @OwnerId

Update Solution
set IsDeleted = 1
where OwnerId = @OwnerId

End



GO
/****** Object:  StoredProcedure [dbo].[RemoveSolutionType]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE  PROCEDURE [dbo].[RemoveSolutionType](@TypeId int)

AS

BEGIN

Update SolutionType
set IsDeleted = 1
where Id = @TypeId

Update Solution
set IsDeleted = 1
where TypeId = @TypeId

End



GO
/****** Object:  StoredProcedure [dbo].[RemoveTag]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE  PROCEDURE [dbo].[RemoveTag](@TagId int)

AS

BEGIN

Update Tag
set IsDeleted = 1
where Id = @TagId

Update Solution
set IsDeleted = 1
where Id in (select SolutionId from SolutionTag where TagId = @TagId)

End


GO
/****** Object:  StoredProcedure [dbo].[RemoveTagGroup]    Script Date: 24/02/2017 4:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




Create  PROCEDURE [dbo].[RemoveTagGroup](@TagGroupId int)

AS

BEGIN

Update TagGroup
set IsDeleted = 1
where Id = @TagGroupId

Update Tag
set IsDeleted = 1
where TagGroupId = @TagGroupId

Update Solution
set IsDeleted = 1
where Id in
(
 select SolutionId from SolutionTag
 where TagId in 
  (select Id from Tag where TagGroupId = @TagGroupId)
 )


End


GO
