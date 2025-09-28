/****** Object:  Table [dbo].[PricingURLMap]    Script Date: 06/29/2018 13:50:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PricingURLMap](
	[PricingURLMapID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[StatusID] [numeric](18, 0) NOT NULL,
	[LastMasterRunID] [numeric](18, 0) NOT NULL,
	[RunIntervalID] [numeric](18, 0) NOT NULL,
	[URL] [nvarchar](500) NULL,
	[EffectiveDate] [datetime] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PricingURLMap] PRIMARY KEY CLUSTERED 
(
	[PricingURLMapID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PricingURLMap]  WITH CHECK ADD  CONSTRAINT [FK_PricingURLMap_MasterCarrierManufacturerStatus] FOREIGN KEY([StatusID])
REFERENCES [dbo].[MasterCarrierManufacturerStatus] ([MasterCarrierManufacturerStatusID])
GO

ALTER TABLE [dbo].[PricingURLMap] CHECK CONSTRAINT [FK_PricingURLMap_MasterCarrierManufacturerStatus]

GO

ALTER TABLE [dbo].[PricingURLMap] ADD  CONSTRAINT [DF_PricingURLMap_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[PricingURLMap] ADD  CONSTRAINT [DF_PricingURLMap_CreateUser]  DEFAULT ('') FOR [CreateUser]
GO

ALTER TABLE [dbo].[PricingURLMap] ADD  CONSTRAINT [DF_PricingURLMap_LastUpdateDate]  DEFAULT (getdate()) FOR [LastUpdateDate]
GO

ALTER TABLE [dbo].[PricingURLMap] ADD  CONSTRAINT [DF_PricingURLMap_LastUpdateUser]  DEFAULT ('') FOR [LastUpdateUser]
GO


