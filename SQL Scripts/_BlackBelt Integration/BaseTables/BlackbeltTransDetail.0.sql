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
	[Value] [nvarchar](200) NULL,
	[TranslationKey] [nvarchar](75) NULL,
	[TranslationValue] [nvarchar](200) NULL,
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
