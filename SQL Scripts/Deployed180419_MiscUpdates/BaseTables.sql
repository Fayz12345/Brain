/****** Object:  Table [dbo].[IFSPickListOrderDetail]    Script Date: 04/03/2018 14:59:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
--Drop Table BlackbeltTransDetail
CREATE TABLE [dbo].[BlackbeltTransDetail](
	[BlackbeltTransDetailID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[BlackbeltTransHeaderID] [numeric](18, 0) NOT NULL,
	[Status] [varchar](50) NULL,	
	[ProcessStatus] [numeric](18, 0) NULL,
	[ReceiveDetailID] [numeric](18, 0) NULL,	
	[QuestionID] [numeric](18, 0) NULL,
	[QuestionType] [nvarchar](20) NULL,	
	[OptionID] [numeric](18, 0) NULL,	
	[ItemAbbreviation] [nvarchar](50) NULL,

	[Key] [nvarchar](75) NULL,
	[Value] [nvarchar](75) NULL,
	[TranslationKey] [nvarchar](75) NULL,
	[TranslationValue] [nvarchar](75) NULL,
	[Message] [nvarchar](Max) NULL,
	
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,	

 CONSTRAINT [PK_BlackbeltTransDetail] PRIMARY KEY CLUSTERED 
(
	[BlackbeltTransDetailID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO


SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[BlackbeltTransDetail] ADD  CONSTRAINT [DF_BlackbeltTransDetail_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Table [dbo].[IFSPickListOrderHeader]    Script Date: 04/03/2018 14:58:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
-- Drop Table BlackbeltTransHeader

CREATE TABLE [dbo].[BlackbeltTransHeader](
	[BlackbeltTransHeaderID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[XMLFileHeaderID] [numeric](18, 0) NOT NULL,
	[ReceiveDetailID] [numeric](18, 0) NULL,	
	[ClientLocationID] [numeric](18, 0) NULL,	
	[ProjectID] [numeric](18, 0) NULL,	
	[ProcessID] [numeric](18, 0) NULL,	
	[CarrierID] [numeric](18, 0) NULL,	
	[ManufacturerID] [numeric](18, 0) NULL,	
	[ModelID] [numeric](18, 0) NULL,	
	[ColourID] [numeric](18, 0) NULL,	
	[GradeID] [numeric](18, 0) NULL,	
	[ESN] [nvarchar](50) NULL,
	[ClientLocationScanKey] [nvarchar](50) NULL,
	[ProjectName] [nvarchar](50) NULL,
	[ProcessScanKey] [nvarchar](50) NULL,
	[ProjectTag] [nvarchar](50) NULL,
	[Status] [varchar](50) NULL,
	[ProcessStatus] [numeric](18, 0) NULL,
	[RequestUser] [varchar](50) NULL,
	[Message] [nvarchar](Max) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,

 CONSTRAINT [PK_BlackbeltTransHeader] PRIMARY KEY CLUSTERED 
(
	[BlackbeltTransHeaderID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[BlackbeltTransHeader] ADD  CONSTRAINT [DF_BlackbeltTransHeader_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Table [dbo].[IFSPickListOrderHeader]    Script Date: 04/03/2018 14:58:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[BlackbeltTranslationListChangeLog](
	[BlackbeltTranslationListChangeLogID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[BlackbeltTranslationListID] [numeric](18, 0) NOT NULL,
	[Status] [varchar](50) NULL,
	[Catagory] [nvarchar](75) NULL,
	[SearchValue] [nvarchar](75) NULL,
	[Translation] [nvarchar](75) NULL,
	[ChangeCreateDate] [datetime] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,

 CONSTRAINT [PK_BlackbeltTranslationListChangeLog] PRIMARY KEY CLUSTERED 
(
	[BlackbeltTranslationListChangeLogID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[BlackbeltTranslationListChangeLog] ADD  CONSTRAINT [DF_BlackbeltTranslationListChangeLog_ChangeCreateDate]  DEFAULT (getdate()) FOR [ChangeCreateDate]
GO


/****** Object:  Table [dbo].[IFSPickListOrderHeader]    Script Date: 04/03/2018 14:58:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[BlackbeltTranslationList](
	[BlackbeltTranslationListID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Status] [varchar](50) NULL,
	[Catagory] [nvarchar](75) NULL,
	[SearchValue] [nvarchar](75) NULL,
	[Translation] [nvarchar](75) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,

 CONSTRAINT [PK_BlackbeltTranslationList] PRIMARY KEY CLUSTERED 
(
	[BlackbeltTranslationListID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[BlackbeltTranslationList] ADD  CONSTRAINT [DF_BlackbeltTranslationList_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

Create Index IX_BBTL_SearchKey on BlackbeltTranslationList(Status, Catagory, SearchValue)
Go

















