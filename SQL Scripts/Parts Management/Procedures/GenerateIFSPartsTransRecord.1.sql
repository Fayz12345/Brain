

--/****** Object:  Table [dbo].MasterIFSLocation    Script Date: 05/07/2015 16:23:38 ******/
--SET ANSI_NULLS ON
--GO

--/*

--Drop table MasterIFSLocation
--*/

--SET QUOTED_IDENTIFIER ON
--GO

--CREATE TABLE [dbo].MasterIFSLocation(
--	[MasterIFSLocationID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
--	[PurposeID] [numeric](18, 0) NOT NULL,
--	[StatusID] [numeric](18, 0) NOT NULL,
--	[IsWhip] [bit] NOT NULL,
--	[IFSLocation] [nvarchar](50) NOT NULL,
--	[Description] [nvarchar](100) NULL,
--	[DeviceRollup] [nvarchar](50) NULL,
--	[PartRollup] [nvarchar](50) NULL,
--	[PickLevel] [nvarchar](10) NOT NULL,

--	[CreateDate] [datetime] NOT NULL,
--	[CreateUser] [nvarchar](50) NOT NULL,
--	[LastUpdateDate] [datetime] NOT NULL,
--	[LastUpdateUser] [nvarchar](50) NOT NULL,
-- CONSTRAINT [PK_MasterIFSLocation] PRIMARY KEY CLUSTERED 
--(
--	[MasterIFSLocationID] ASC
--)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 75) ON [PRIMARY]
--) ON [PRIMARY]

--GO

--ALTER TABLE [dbo].MasterIFSLocation ADD  CONSTRAINT [DF_MasterIFSLocation_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
--GO

--Create Index MasterIFSLocationPurposeID on MasterIFSLocation([PurposeID])
--Go
--Create Index MasterIFSLocationStatusID on MasterIFSLocation([StatusID])
--Go