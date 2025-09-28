/****** Object:  Table [dbo].[PhysicalInventoryCount]    Script Date: 05/07/2015 16:23:38 ******/
SET ANSI_NULLS ON
GO

/*

Drop Table PhysicalInventoryCount

*/

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PhysicalInventoryCount](
	[PhysicalInventoryCountID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ReceiveDetailID] [numeric](18, 0) NOT NULL,
	[MasterIFSLocationID] [numeric](18, 0) NOT NULL,
	[MasterIFSCondtionID] [numeric](18, 0) NOT NULL,
	[Status] [nvarchar](10) NULL,

	[IMEI] [nvarchar](50) NULL,

	[Batch] [nvarchar](25) NULL,
	[isBatchLocked] [Bit] NULL,
	[IFSSiteScan] [nvarchar](5) NULL,
	[IFSProjectScan] [nvarchar](10) NULL,
	[IFSSite] [nvarchar](5) NULL,
	[IFSProject] [nvarchar](10) NULL,
	[POReceiptDate] [nvarchar](10) NULL,

	[SKU] [nvarchar](25) NULL,
	[IFSLocation] [nvarchar](20) NULL,
	[IFSCondition] [nvarchar](50) NULL,
	[IFSConditionCode] [nvarchar](10) NULL,	
	[StatusMessage] [nvarchar](500) NULL,	
	[DuplicateFoundBatches] [nvarchar](500) NULL,	

    [CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,

 CONSTRAINT [PK_PhysicalInventoryCount] PRIMARY KEY CLUSTERED 
(
	[PhysicalInventoryCountID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 75) ON [PRIMARY]
) ON [PRIMARY]

GO

Create Index PHIC_ReceiveDetail on PhysicalInventoryCount(ReceiveDetailID)
Create Index PHIC_MasterIFSLocation on PhysicalInventoryCount(IFSLocation)
Create Index PHIC_MasterIFSCondtion on PhysicalInventoryCount(IFSCondition)
Create Index PHIC_SKU on PhysicalInventoryCount(SKU)
Create Index PHIC_Batch on PhysicalInventoryCount(Batch)
