

/****** Object:  Table [dbo].[MasterPartsTableIFSLocationStorage]    Script Date: 05/07/2015 16:23:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MasterPartsTableIFSLocationStorage](
	[MasterPartsTableIFSLocationStorageID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MasterPartsTableID] [numeric](18, 0) NOT NULL,
	[MasterIFSLocationID] [numeric](18, 0) NOT NULL,
	[QTY] [smallint] NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MasterPartsTableIFSLocationStorage] PRIMARY KEY CLUSTERED 
(
	[MasterPartsTableIFSLocationStorageID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 75) ON [PRIMARY]
) ON [PRIMARY]

GO


