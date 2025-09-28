



















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




USE [GMP_Data]
GO

/****** Object:  Table [dbo].[MPMasterPartsRequestedLog]    Script Date: 05/07/2015 16:24:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MPMasterPartsRequestedLog](
	[MPMasterPartsRequestedLogID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MPMasterPartsTechAssignedLogID] [numeric](18, 0) NULL,
	[Status] [nvarchar](20) NOT NULL,
	[ReceiveDetailID] [numeric](18, 0) NOT NULL,


	[CarrierID] [numeric](18, 0) NULL,
	[ManufacturerID] [numeric](18, 0) NULL,
	[ModelID] [numeric](18, 0) NULL,
	[ColourID] [numeric](18, 0) NULL,
	[IFSRequestLocation] [nvarchar](50) NOT NULL,
	[RequestedPart] [nvarchar](200) NOT NULL,
	[PartNote] [nvarchar](200) NOT NULL,
	[TechUser] [nvarchar](50) NOT NULL,
	[CancelDate] [datetime] NULL,
	[CancelUser] [nvarchar](50) NULL,

	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MPMasterPartsRequestedLog] PRIMARY KEY CLUSTERED 
(
	[MPMasterPartsRequestedLogID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MPMasterPartsRequestedLog]  WITH CHECK ADD  CONSTRAINT [FK_MPMasterPartsRequestedLog_ReceiveDetail] FOREIGN KEY([ReceiveDetailID])
REFERENCES [dbo].[ReceiveDetail] ([ReceiveDetailID])
GO

ALTER TABLE [dbo].[MPMasterPartsRequestedLog] CHECK CONSTRAINT [FK_MPMasterPartsRequestedLog_ReceiveDetail]
GO

ALTER TABLE [dbo].[MPMasterPartsRequestedLog] ADD  CONSTRAINT [DF_MPMasterPartsRequestedLog_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO



/****** Object:  Table [dbo].[MPMasterPartsTableCarrierList]    Script Date: 05/07/2015 16:23:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MPMasterPartsTableCarrierList](
	[MPMasterPartsTableCarrierListID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MPMasterPartsTableID] [numeric](18, 0) NOT NULL,
	[CarrierID] [numeric](18, 0) NOT NULL,
	[isActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MPMasterPartsTableCarrierList] PRIMARY KEY CLUSTERED 
(
	[MPMasterPartsTableCarrierListID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MPMasterPartsTableCarrierList]  WITH CHECK ADD  CONSTRAINT [FK_MPMasterPartsTableCarrierList_MPMasterPartsTable] FOREIGN KEY([MPMasterPartsTableID])
REFERENCES [dbo].[MPMasterPartsTable] ([MPMasterPartsTableID])
GO

ALTER TABLE [dbo].[MPMasterPartsTableCarrierList] CHECK CONSTRAINT [FK_MPMasterPartsTableCarrierList_MPMasterPartsTable]
GO

ALTER TABLE [dbo].[MPMasterPartsTableCarrierList] ADD  CONSTRAINT [DF_MPMasterPartsTableCarrierList_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO




/****** Object:  Table [dbo].[MPMasterPartsTableColourList]    Script Date: 05/07/2015 16:23:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MPMasterPartsTableColourList](
	[MPMasterPartsTableColourListID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MPMasterPartsTableID] [numeric](18, 0) NOT NULL,
	[ColourID] [numeric](18, 0) NOT NULL,
	[isActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MPMasterPartsTableColourList] PRIMARY KEY CLUSTERED 
(
	[MPMasterPartsTableColourListID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MPMasterPartsTableColourList]  WITH CHECK ADD  CONSTRAINT [FK_MPMasterPartsTableColourList_MPMasterPartsTable] FOREIGN KEY([MPMasterPartsTableID])
REFERENCES [dbo].[MPMasterPartsTable] ([MPMasterPartsTableID])
GO

ALTER TABLE [dbo].[MPMasterPartsTableColourList] CHECK CONSTRAINT [FK_MPMasterPartsTableColourList_MPMasterPartsTable]
GO

ALTER TABLE [dbo].[MPMasterPartsTableColourList] ADD  CONSTRAINT [DF_MPMasterPartsTableColourList_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO





/****** Object:  Table [dbo].[MPMasterPartsTableIFSLocationStorage]    Script Date: 05/07/2015 16:23:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MPMasterPartsTableIFSLocationStorage](
	[MPMasterPartsTableIFSLocationStorageID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MPMasterPartsTableID] [numeric](18, 0) NOT NULL,
	[IFSLocation] [nvarchar](50) NULL,
	[QTY] [smallint] NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MPMasterPartsTableIFSLocationStorage] PRIMARY KEY CLUSTERED 
(
	[MPMasterPartsTableIFSLocationStorageID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 75) ON [PRIMARY]
) ON [PRIMARY]

GO



/****** Object:  Table [dbo].[MPMasterPartsTableModelList]    Script Date: 05/07/2015 16:23:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MPMasterPartsTableModelList](
	[MPMasterPartsTableModelListID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MPMasterPartsTableID] [numeric](18, 0) NOT NULL,
	[ModelID] [numeric](18, 0) NOT NULL,
	[isActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MPMasterPartsTableModelList] PRIMARY KEY CLUSTERED 
(
	[MPMasterPartsTableModelListID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MPMasterPartsTableModelList]  WITH CHECK ADD  CONSTRAINT [FK_MPMasterPartsTableModelList_MPMasterPartsTable] FOREIGN KEY([MPMasterPartsTableID])
REFERENCES [dbo].[MPMasterPartsTable] ([MPMasterPartsTableID])
GO

ALTER TABLE [dbo].[MPMasterPartsTableModelList] CHECK CONSTRAINT [FK_MPMasterPartsTableModelList_MPMasterPartsTable]
GO

ALTER TABLE [dbo].[MPMasterPartsTableModelList] ADD  CONSTRAINT [DF_MPMasterPartsTableModelList_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO




/****** Object:  Table [dbo].[MPMasterPartsTechAssignedLog]    Script Date: 05/07/2015 16:24:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MPMasterPartsTechAssignedLog](
	[MPMasterPartsTechAssignedLogID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MPMasterPartsTableID] [numeric](18, 0) NOT NULL,
	[MPMasterPartsTableIFSLocationStorageLogID] [numeric](18, 0) NOT NULL,
	[MPMasterPartsRequestedLogID] [numeric](18, 0) NULL,

	[Status] [nvarchar](20) NULL,
	[ReceiveDetailID] [numeric](18, 0) NULL,
	[ReceiveDetailItemID] [numeric](18, 0) NULL,
	[TechName] [nvarchar](50) NOT NULL,
	[GMPPartNumber] [nvarchar](30) NULL,
	[Description] [nvarchar](500) NULL,
	[QTY] [numeric](18, 0) NOT NULL,

	[UnitAssignedDate] [datetime] NULL,
	[UnitAssignedUser] [nvarchar](50) NULL,
	[ShippedDate] [datetime] NULL,
	[ShippedUser] [nvarchar](50) NULL,
	[ReturnedDate] [datetime] NULL,
	[ReturnedUser] [nvarchar](50) NULL,



	--[PickDate] [datetime] NULL,
	--[PickUser] [nvarchar](50) NULL,
	--[RunnerDate] [datetime] NULL,
	--[RunnerUser] [nvarchar](50) NULL,
	--[FillDate] [datetime] NULL,
	--[FillUser] [nvarchar](50) NULL,
	--[OutOfStockDate] [datetime] NULL,
	--[OutOfStockUser] [nvarchar](50) NULL,



	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,

	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MPMasterPartsTechAssignedLog] PRIMARY KEY CLUSTERED 
(
	[MPMasterPartsTechAssignedLogID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MPMasterPartsTechAssignedLog]  WITH CHECK ADD  CONSTRAINT [FK_MPMasterPartsTechAssignedLog_MPMasterPartsRequestedLog] FOREIGN KEY([MPMasterPartsRequestedLogID])
REFERENCES [dbo].[MPMasterPartsRequestedLog] ([MPMasterPartsRequestedLogID])
GO

ALTER TABLE [dbo].[MPMasterPartsTechAssignedLog] CHECK CONSTRAINT [FK_MPMasterPartsTechAssignedLog_MPMasterPartsRequestedLog]
GO

ALTER TABLE [dbo].[MPMasterPartsTechAssignedLog] ADD  CONSTRAINT [DF_MPMasterPartsTechAssignedLog_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO


--/****** Object:  Table [dbo].[PartNumberBucketInventorySource]    Script Date: 05/07/2015 18:35:36 ******/
--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO

--CREATE TABLE [dbo].[PartNumberBucketInventorySource](
--	[PartNumberBucketInventorySourceID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
--	[MasterPartsLinkTableID] [numeric](18, 0) NOT NULL,
--	[PartNumberBucketInventoryTransactionTypeID] [numeric](18, 0) NOT NULL,
--	[Desc] [nvarchar](50) NULL,
--	[ReceiveDetailID] [numeric](18, 0) NOT NULL,
--	[Quantity] [numeric](18, 0) NOT NULL,
--	[CreateDate] [datetime] NOT NULL,
--	[CreateUser] [nvarchar](50) NOT NULL,
--	[LastUpdateDate] [datetime] NOT NULL,
--	[LastUpdateUser] [nvarchar](50) NOT NULL,
--	[UnitPrice] [numeric](18, 2) NULL,
--	[UnitPurchasePrice] [numeric](18, 2) NULL,
--	[AveragePurchasePrice] [numeric](18, 2) NULL,
--	[MasterPartsLinkTablePriceListID] [numeric](18, 0) NULL,
-- CONSTRAINT [PK_PartNumberBucketInventorySource] PRIMARY KEY CLUSTERED 
--(
--	[PartNumberBucketInventorySourceID] ASC
--)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 75) ON [PRIMARY]
--) ON [PRIMARY]

--GO

--ALTER TABLE [dbo].[PartNumberBucketInventorySource]  WITH CHECK ADD  CONSTRAINT [FK_PartNumberBucketInventorySource_MasterPartsLinkTable] FOREIGN KEY([MasterPartsLinkTableID])
--REFERENCES [dbo].[MasterPartsLinkTable] ([MasterPartsLinkTableID])
--GO

--ALTER TABLE [dbo].[PartNumberBucketInventorySource] CHECK CONSTRAINT [FK_PartNumberBucketInventorySource_MasterPartsLinkTable]
--GO

--ALTER TABLE [dbo].[PartNumberBucketInventorySource]  WITH CHECK ADD  CONSTRAINT [FK_PartNumberBucketInventorySource_PartNumberBucketInventorysourceType] FOREIGN KEY([PartNumberBucketInventoryTransactionTypeID])
--REFERENCES [dbo].[PartNumberBucketInventoryTransactionType] ([PartNumberBucketInventorysourceTypeID])
--GO

--ALTER TABLE [dbo].[PartNumberBucketInventorySource] CHECK CONSTRAINT [FK_PartNumberBucketInventorySource_PartNumberBucketInventorysourceType]
--GO

--ALTER TABLE [dbo].[PartNumberBucketInventorySource] ADD  CONSTRAINT [DF_PartNumberBucketInventorySource_Quantity]  DEFAULT ((1)) FOR [Quantity]
--GO

--ALTER TABLE [dbo].[PartNumberBucketInventorySource] ADD  CONSTRAINT [DF_PartNumberBucketInventorySource_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
--GO



--/****** Object:  Table [dbo].[PartNumberBucketInventoryTransactionType]    Script Date: 05/07/2015 18:35:27 ******/
--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO

--CREATE TABLE [dbo].[PartNumberBucketInventoryTransactionType](
--	[PartNumberBucketInventorysourceTypeID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
--	[Type] [nvarchar](20) NOT NULL,
--	[CreateDate] [datetime] NOT NULL,
--	[CreateUser] [nvarchar](50) NOT NULL,
--	[LastUpdateDate] [datetime] NOT NULL,
--	[LastUpdateUser] [nvarchar](50) NOT NULL,
--	[Factor] [numeric](18, 0) NULL,
-- CONSTRAINT [PK_PartNumberBucketInventorysourceType] PRIMARY KEY CLUSTERED 
--(
--	[PartNumberBucketInventorysourceTypeID] ASC
--)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 75) ON [PRIMARY]
--) ON [PRIMARY]

--GO

--ALTER TABLE [dbo].[PartNumberBucketInventoryTransactionType] ADD  CONSTRAINT [DF_PartNumberBucketInventorysourceType_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
--GO

--ALTER TABLE [dbo].[PartNumberBucketInventoryTransactionType] ADD  CONSTRAINT [DF_PartNumberBucketInventorysourceType_CreateUser]  DEFAULT ('') FOR [CreateUser]
--GO

--ALTER TABLE [dbo].[PartNumberBucketInventoryTransactionType] ADD  CONSTRAINT [DF_PartNumberBucketInventorysourceType_LastUpdateDate]  DEFAULT (getdate()) FOR [LastUpdateDate]
--GO

--ALTER TABLE [dbo].[PartNumberBucketInventoryTransactionType] ADD  CONSTRAINT [DF_PartNumberBucketInventorysourceType_LastUpdateUser]  DEFAULT ('') FOR [LastUpdateUser]
--GO






/****** Object:  Table [dbo].[MPMasterPartsTableIFSLocationLog]    Script Date: 05/07/2015 16:23:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MPMasterPartsTableIFSLocationStorageLog](
	[MPMasterPartsTableIFSLocationStorageLogID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MPMasterPartsTableIFSLocationStorageID] [numeric](18, 0) NOT NULL,
	[MPMasterPartsTechAssignedLogID] [numeric](18, 0) NULL,
	[QTY] [smallint] NULL,
	[QTYChange] [smallint] NULL,
	[IFSLocation] [nvarchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MPMasterPartsTableIFSLocationStorageLog] PRIMARY KEY CLUSTERED 
(
	[MPMasterPartsTableIFSLocationStorageLogID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 75) ON [PRIMARY]
) ON [PRIMARY]

GO






































































































































































































