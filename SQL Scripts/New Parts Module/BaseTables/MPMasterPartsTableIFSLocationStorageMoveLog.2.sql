

/****** Object:  Table [dbo].[MPMasterPartsTableIFSLocationStorageMoveLog]    Script Date: 05/07/2015 16:23:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MPMasterPartsTableIFSLocationStorageMoveLog](
	[MPMasterPartsTableIFSLocationStorageMoveLogID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MPMasterPartsTableIFSLocationStorageID] [numeric](18, 0) NOT NULL,
	[MPMasterPartsTechAssignedLogID] [numeric](18, 0) NULL,
	[QTY] [smallint] NULL,
	[QTYChange] [smallint] NULL,
	[IFSLocation] [nvarchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MPMasterPartsTableIFSLocationStorageMoveLog] PRIMARY KEY CLUSTERED 
(
	[MPMasterPartsTableIFSLocationStorageMoveLogID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 75) ON [PRIMARY]
) ON [PRIMARY]

GO


