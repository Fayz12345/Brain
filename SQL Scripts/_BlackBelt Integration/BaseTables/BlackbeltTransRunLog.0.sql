/****** Object:  Table [dbo].[IFSPickListOrderDetail]    Script Date: 04/03/2018 14:59:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--Drop Table BlackbeltTransRunLog

CREATE TABLE [dbo].[BlackbeltTransRunLog](
	[BlackbeltTransRunLogID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[BlackbeltLogParentID] [numeric](18, 0) NOT NULL,
	[BlackbeltTransHeaderID] [numeric](18, 0) NOT NULL,
	[BlackbeltTransDetailID] [numeric](18, 0) NOT NULL,
	[Status] [varchar](50) NULL,	
	[Message]  [varchar](500) NULL,	
	[Comment]  [varchar](500) NULL,	

	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,	

 CONSTRAINT [PK_BlackbeltTransRunLog] PRIMARY KEY CLUSTERED 
(
	[BlackbeltTransRunLogID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO


SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[BlackbeltTransRunLog] ADD  CONSTRAINT [DF_BlackbeltTransRunLog_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

Create Index IX_BlackbeltTransRunLog_Parent on BlackbeltTransRunLog(BlackBeltLogParentID)
