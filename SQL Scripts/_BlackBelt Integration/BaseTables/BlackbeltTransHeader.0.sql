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
	[MemoryID] [numeric](18, 0) NULL,	
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
