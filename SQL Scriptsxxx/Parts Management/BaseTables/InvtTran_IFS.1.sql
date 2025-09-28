
/****** Object:  Table [dbo].[InvtTran_IFS]    Script Date: 05/28/2015 23:07:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[InvtTran_IFS](
	[InvtTranID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ReceiveDetailID] [numeric](18, 0) NULL,
	[ProcessID] [numeric](18, 0) NULL,
	[Quantity] [int] NULL,
	[IFSSite] [nvarchar](5) NULL,
	[IFSProject] [nvarchar](10) NULL,
	[POVendor] [nvarchar](50) NULL,
	[PONumber] [nvarchar](12) NULL,
	[POReceiptDate] [nvarchar](10) NULL,
	[POLine] [nvarchar](4) NULL,
	[POCost] [numeric](18, 5) NULL,
	[FromSku] [nvarchar](25) NULL,
	[FromLocation] [nvarchar](50) NULL,
	[FromCondition] [nvarchar](50) NULL,
	[ToSku] [nvarchar](25) NULL,
	[ToLocation] [nvarchar](50) NULL,
	[ToCondition] [nvarchar](50) NULL,
	[ToSKUID] [numeric](18, 0) NULL,
	[ToLocationID] [numeric](18, 0) NULL,
	[ToConditionID] [numeric](18, 0) NULL,
	[Directive] [smallint] NULL,
	[MiscNote] [nvarchar](100) NULL,
	[StatusID] [numeric](18, 0) NULL,
	[CreatedDate] [datetime] NULL,
	[CreateUser] [nvarchar](50) NULL,
	[CreateSource] [nvarchar](25) NULL,
	[RetrievedBatch] [int] NULL,
	[RetrievedDate] [datetime] NULL,
	[RetrievedUser] [nvarchar](50) NULL,
	[PartNumberBucketInventoryPlacementID] [numeric](18, 0) NULL,
	[PartNumberBucketInventorySourceID] [numeric](18, 0) NULL,
	[MasterPartsLinkTableID] [nvarchar](18) NULL,
 CONSTRAINT [PK_InvtTran_IFS] PRIMARY KEY CLUSTERED 
(
	[InvtTranID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


