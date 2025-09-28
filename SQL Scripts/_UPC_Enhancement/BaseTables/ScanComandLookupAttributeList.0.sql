

/****** Object:  Table [dbo].[MasterCarrierManufacturerUPCLookup]    Script Date: 04/18/2020 11:12:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ScanComandLookupAttributeList](
	[ScanComandLookupAttributeListID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ScanComandLookupID] [numeric](18, 0) NOT NULL,
	[OptionID] [numeric](18, 0) NOT NULL,
	[Status] [nvarchar](10) NOT NULL,
	[SetValue] [nvarchar](250) NOT NULL,
	[Sequence] [int] NOT NULL,
	[CommandString] [nvarchar](250) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ScanComandLookupAttributeListAttribute] PRIMARY KEY CLUSTERED 
(
	[ScanComandLookupAttributeListID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

Create index IX_ScanComandLookupID on ScanComandLookupAttributeList(ScanComandLookupID)
Go

