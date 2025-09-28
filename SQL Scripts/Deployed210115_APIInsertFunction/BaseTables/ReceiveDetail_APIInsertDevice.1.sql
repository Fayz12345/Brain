
/****** Object:  Table [dbo].[InvtTran_IFS]    Script Date: 05/28/2015 23:07:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--Drop Table [ReceiveDetail_APIInsertDevice]

CREATE TABLE [dbo].[ReceiveDetail_APIInsertDevice](
	[ReceiveDetail_APIInsertDeviceID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ReceiveDetail_APIInsertBatchID] [numeric](18, 0) NULL,
	[ReceiveDetailID] [numeric](18, 0) NULL,
	[ESN] [nvarchar](50) NULL,	
	[Status] [nvarchar](50) NULL,	
	[Message] [nvarchar](MAX) NULL	
 CONSTRAINT [PK_ReceiveDetail_APIInsertDevice] PRIMARY KEY CLUSTERED 
(
	[ReceiveDetail_APIInsertDeviceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
