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


