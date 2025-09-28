

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


