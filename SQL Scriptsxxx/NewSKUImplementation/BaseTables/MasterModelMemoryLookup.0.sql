/****** Object:  Table [dbo].[MasterCarrierManufacturerLookup]    Script Date: 04/27/2017 19:55:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MasterModelMemoryLookup](
	[MasterModelMemoryLookupID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[StatusID] [numeric](18, 0) NOT NULL,
	[ModelID] [numeric](18, 0) NOT NULL,
	[MemoryID] [numeric](18, 0) NOT NULL,
	[Retire] [nvarchar](20) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MasterModelMemoryLookup] PRIMARY KEY CLUSTERED 
(
	[MasterModelMemoryLookupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 75) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MasterModelMemoryLookup]  WITH CHECK ADD  CONSTRAINT [FK_MasterModelMemoryLookup_MasterCarrierManufacturerStatus] FOREIGN KEY([StatusID])
REFERENCES [dbo].[MasterCarrierManufacturerStatus] ([MasterCarrierManufacturerStatusID])
GO

ALTER TABLE [dbo].[MasterModelMemoryLookup]  WITH CHECK ADD  CONSTRAINT [FK_MasterModelMemoryLookup_OptionModel] FOREIGN KEY([ModelID])
REFERENCES [dbo].[Option] ([OptionID])
GO

ALTER TABLE [dbo].[MasterModelMemoryLookup]  WITH CHECK ADD  CONSTRAINT [FK_MasterModelMemoryLookup_OptionMemory] FOREIGN KEY([MemoryID])
REFERENCES [dbo].[Option] ([OptionID])
GO

ALTER TABLE [dbo].[MasterModelMemoryLookup] CHECK CONSTRAINT [FK_MasterModelMemoryLookup_MasterCarrierManufacturerStatus]
GO

ALTER TABLE [dbo].[MasterModelMemoryLookup] ADD  CONSTRAINT [DF_MasterModelMemoryLookup_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[MasterModelMemoryLookup] ADD  CONSTRAINT [DF_MasterModelMemoryLookup_CreateUser]  DEFAULT ('') FOR [CreateUser]
GO

ALTER TABLE [dbo].[MasterModelMemoryLookup] ADD  CONSTRAINT [DF_MasterModelMemoryLookup_LastUpdateDate]  DEFAULT (getdate()) FOR [LastUpdateDate]
GO

ALTER TABLE [dbo].[MasterModelMemoryLookup] ADD  CONSTRAINT [DF_MasterModelMemoryLookup_LastUpdateUser]  DEFAULT ('') FOR [LastUpdateUser]
GO

Create Index MasterModelMemory on [dbo].[MasterModelMemoryLookup](ModelID, MemoryID)
GO
