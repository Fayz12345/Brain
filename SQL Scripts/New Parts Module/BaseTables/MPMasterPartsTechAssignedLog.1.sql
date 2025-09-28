
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


