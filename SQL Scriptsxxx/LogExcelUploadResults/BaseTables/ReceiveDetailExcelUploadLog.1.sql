
/****** Object:  Table [dbo].[InvtTran_IFS]    Script Date: 05/28/2015 23:07:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*

  Select L.ESN, R.ESN, R.Version, L.ReceiveDetailID, R.ReceiveDetailID from [ReceiveDetailExcelUploadLog] L
  inner join ReceiveDetail R on r.ESN = l.ESN and r.Version = '000'
    where L.ReceiveDetailID = 0 and L.[STATUS] = 'Success'
  
  Update [ReceiveDetailExcelUploadLog] Set ReceiveDetailID = R.ReceiveDetailID
  from [ReceiveDetailExcelUploadLog] L
  inner join ReceiveDetail R on r.ESN = l.ESN and r.Version = '000'
    where L.ReceiveDetailID = 0 and L.[STATUS] = 'Success'

*/

--Drop Table [ReceiveDetailExcelUploadLog]

CREATE TABLE [dbo].[ReceiveDetailExcelUploadLog](
	[ReceiveDetailExcelUploadLogID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ReceiveDetailID] [numeric](18, 0) NULL,
	[KeyIdentifier] [nvarchar](100),
	[ESN] [nvarchar](50) NULL,	
	[STATUS] [nvarchar](20) NULL,
	[RecordType] [nvarchar](20) NULL,	
	[StartTimeDate] [datetime] NULL,
	[EndTimeDate] [datetime] NULL,
	[SaveTimeMS] [numeric](18, 0) NULL,
	[RecordDetailString] [nvarchar](max) NULL,
	[Message] [nvarchar](Max) NULL,

	[CreateIPAddress] [nvarchar](20) NULL,	
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL
 CONSTRAINT [PK_ReceiveDetailExcelUploadLog] PRIMARY KEY CLUSTERED 
(
	[ReceiveDetailExcelUploadLogID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ReceiveDetailExcelUploadLog] ADD  CONSTRAINT [DF_ReceiveDetailExcelUploadLog_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[ReceiveDetailExcelUploadLog] ADD  CONSTRAINT [DF_ReceiveDetailExcelUploadLog_CreateUser]  DEFAULT ('') FOR [CreateUser]
GO

Create Index X_KeyIdentifier on ReceiveDetailExcelUploadLog(KeyIdentifier)
Go

Create Index X_ESN on ReceiveDetailExcelUploadLog(ESN)
Go
