
/****** Object:  Table [dbo].[InvtTran_IFS]    Script Date: 05/28/2015 23:07:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--Drop Table [BishopCatalogueSendLog]

CREATE TABLE [dbo].[ReceiveDetail_APIInsertLog](
	[ReceiveDetail_APIInsertLogID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ReceiveDetailID] [numeric](18, 0) NULL,	
	[JSONin] [nvarchar](Max) NULL,
	[JSONFirstReply] [nvarchar](Max) NULL,
	[JSONout] [nvarchar](Max) NULL,
	[DateTimein] [datetime] NULL,
	[DateTimeout] [datetime] NULL,
	[CreateUser] [nvarchar](50) NOT NULL
 CONSTRAINT [PK_ReceiveDetail_APIInsertLog] PRIMARY KEY CLUSTERED 
(
	[ReceiveDetail_APIInsertLogID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ReceiveDetail_APIInsertLog] ADD  CONSTRAINT [DF_ReceiveDetail_APIInsertLog_CreateDate]  DEFAULT (getdate()) FOR [DateTimein]
GO