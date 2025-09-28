

/****** Object:  Table [dbo].[MasterPartsTableIFSLocationLog]    Script Date: 05/07/2015 16:23:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MasterPartsTableIFSLocationStorageChangeLog](
	[MasterPartsTableIFSLocationStorageChangeLogID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MasterPartsTableIFSLocationStorageID] [numeric](18, 0) NOT NULL,
	[MasterIFSLocationID] [numeric](18, 0) NOT NULL,
	[MasterPartsTechAssignedLogID] [numeric](18, 0) NULL,
	[QTY] [smallint] NULL,
	[QTYChange] [smallint] NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MasterPartsTableIFSLocationStorageChangeLog] PRIMARY KEY CLUSTERED 
(
	[MasterPartsTableIFSLocationStorageChangeLogID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 75) ON [PRIMARY]
) ON [PRIMARY]

GO


