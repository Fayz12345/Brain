
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


