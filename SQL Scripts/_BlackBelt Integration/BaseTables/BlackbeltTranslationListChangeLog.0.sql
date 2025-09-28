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
	[Translation] [nvarchar](200) NULL,
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
