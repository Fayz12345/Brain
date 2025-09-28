

/****** Object:  Table [dbo].MasterIFSLocationStatus    Script Date: 05/07/2015 16:23:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].MasterIFSLocationStatus(
	[MasterIFSLocationStatusID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Status] [varchar](20) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MasterIFSLocationStatus] PRIMARY KEY CLUSTERED 
(
	[MasterIFSLocationStatusID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 75) ON [PRIMARY]
) ON [PRIMARY]

GO



ALTER TABLE [dbo].[MasterIFSLocationStatus] ADD  CONSTRAINT [DF_MasterIFSLocationStatus_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

Insert MasterIFSLocationStatus (Status, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser)
values ('Active',getdate(), 'jmccomb',getdate(), 'jmccomb')

Insert MasterIFSLocationStatus (Status, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser)
values ('InActive',getdate(), 'jmccomb',getdate(), 'jmccomb')
