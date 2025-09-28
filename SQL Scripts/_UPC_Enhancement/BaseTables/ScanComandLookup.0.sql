
--Drop Table MasterCarrierManufacturerUPCLookup
--Go

/****** Object:  Table [dbo].[MasterCarrierManufacturerUPCLookup]    Script Date: 04/18/2020 13:05:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ScanComandLookup](
	[ScanComandLookupID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](10) NOT NULL,
	[ScanCode] [nvarchar](250) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ScanComandLookup] PRIMARY KEY CLUSTERED 
(
	[ScanComandLookupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO


