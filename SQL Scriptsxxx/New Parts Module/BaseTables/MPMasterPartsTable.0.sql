/****** Object:  Table [dbo].[MPMasterPartsTable]    Script Date: 05/07/2015 16:23:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO


/*


Drop Table [MPMasterPartsTableIFSLocationStorageLog]
Drop Table [MPMasterPartsTechAssignedLog]
Drop Table [MPMasterPartsTableModelList]
Drop Table [MPMasterPartsTableColourList]
Drop Table [MPMasterPartsTableIFSLocationStorage]
Drop Table [MPMasterPartsTableCarrierList]
Drop Table [MPMasterPartsRequestedLog]
Drop Table [MPMasterPartsTable]




*/




CREATE TABLE [dbo].[MPMasterPartsTable](
	[MPMasterPartsTableID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MasterPartsID] [numeric](18, 0) NOT NULL,
	[IFSWarehouseID] [numeric](18, 0) NULL,            -- IFS Project ID (another name)// Points back to the ClientLocationID
	[WareHouseID] [numeric](18, 0) NULL,               -- Internal Shop floor warehouse// Deduced from the ClientLocation Attached. -1 = GMPWHS
	[MasterPartsClassTypeID] [numeric](18, 0) NULL,

	[Quantity] [numeric](18, 0) NOT NULL,
	[GMPPartNumber] [nvarchar](30) NULL,
	[GMPPartDescription] [nvarchar](50) NULL,
	[PartNumber] [nvarchar](30) NOT NULL,


	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,
	
	--[Carrier] [varchar](500) NULL,
	--[Manufacturer] [nvarchar](50) NULL,
	--[Model] [varchar](500) NULL,
	--[MonthendQTY] [numeric](18, 0) NOT NULL,
	--[MonthEndDate] [datetime] NOT NULL,
	--[UnitPrice] [numeric](18, 2) NULL,
	--[MonthEndUnitPrice] [numeric](18, 2) NULL,
	--[QTYMin] [numeric](18, 0) NULL,
	--[QTYMax] [numeric](18, 0) NULL,
	--[QTYReorder] [numeric](18, 0) NULL,
	--[InWarrentyWorkPrice] [numeric](18, 2) NULL,
	--[MonthEndInWarrentyWorkPrice] [numeric](18, 2) NULL,
	--[AveragePurchasePrice] [numeric](18, 2) NULL,
	--[MonthEndAveragePurchasePrice] [numeric](18, 2) NULL,
 CONSTRAINT [PK_MPMasterPartsTable] PRIMARY KEY CLUSTERED 
(
	[MPMasterPartsTableID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 75) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[MPMasterPartsTable]  WITH CHECK ADD  CONSTRAINT [FK_MPMasterPartsTable_MPMasterParts] FOREIGN KEY([MasterPartsID])
REFERENCES [dbo].[MasterParts] ([MasterPartsID])
GO

ALTER TABLE [dbo].[MPMasterPartsTable] CHECK CONSTRAINT [FK_MPMasterPartsTable_MPMasterParts]
GO


ALTER TABLE [dbo].[MPMasterPartsTable] ADD  CONSTRAINT [DF_MPMasterPartsTable_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO


