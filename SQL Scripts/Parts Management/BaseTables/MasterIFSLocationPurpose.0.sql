
/*

Drop table MasterIFSLocationPurpose
Drop table MasterIFSLocationStatus
Drop table MasterIFSLocation
Drop Table InvtTran_IFS
Drop table MasterIFSLocation


*/

/****** Object:  Table [dbo].MasterIFSLocationPurpose    Script Date: 05/07/2015 16:23:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].MasterIFSLocationPurpose(
	[MasterIFSLocationPurposeID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Purpose] [varchar](20) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MasterIFSLocationPurpose] PRIMARY KEY CLUSTERED 
(
	[MasterIFSLocationPurposeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 75) ON [PRIMARY]
) ON [PRIMARY]

GO



ALTER TABLE [dbo].MasterIFSLocationPurpose ADD  CONSTRAINT [DF_MasterIFSLocationPurpose_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO


Insert MasterIFSLocationPurpose (Purpose, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser) values ('Stock',getdate(), 'jmccomb',getdate(), 'jmccomb')
Insert MasterIFSLocationPurpose (Purpose, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser) values ('Quarantine',getdate(), 'jmccomb',getdate(), 'jmccomb')
Insert MasterIFSLocationPurpose (Purpose, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser) values ('Staging',getdate(), 'jmccomb',getdate(), 'jmccomb')
Insert MasterIFSLocationPurpose (Purpose, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser) values ('Action',getdate(), 'jmccomb',getdate(), 'jmccomb')
Insert MasterIFSLocationPurpose (Purpose, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser) values ('Kan Con',getdate(), 'jmccomb',getdate(), 'jmccomb')
