
/****** Object:  Table [dbo].[InvtTran_IFS]    Script Date: 05/28/2015 23:07:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--Drop Table [ReceiveDetail_APIInsertBatch]

CREATE TABLE [dbo].[ReceiveDetail_APIInsertBatch](
	[ReceiveDetail_APIInsertBatchID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ReceiveDetail_APIInsertID] [numeric](18, 0) NULL,
	[CreateDate] [DateTime] Null,
	[batch] [nvarchar](50) NULL,	
	[client] [nvarchar](50) NULL,	
	[username] [nvarchar](50) NULL,	
	[project] [nvarchar](50) NULL,
	[process] [nvarchar](20) NULL,
	[status] [nvarchar](50) NULL,		
	[Message] [nvarchar](MAX) NULL	
 CONSTRAINT [PK_ReceiveDetail_APIInsertBatch] PRIMARY KEY CLUSTERED 
(
	[ReceiveDetail_APIInsertBatchID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
