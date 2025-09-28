
/****** Object:  Table [dbo].[MPMasterPartsTableColourList]    Script Date: 05/07/2015 16:23:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MPMasterPartsTableColourList](
	[MPMasterPartsTableColourListID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MPMasterPartsTableID] [numeric](18, 0) NOT NULL,
	[ColourID] [numeric](18, 0) NOT NULL,
	[isActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MPMasterPartsTableColourList] PRIMARY KEY CLUSTERED 
(
	[MPMasterPartsTableColourListID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MPMasterPartsTableColourList]  WITH CHECK ADD  CONSTRAINT [FK_MPMasterPartsTableColourList_MPMasterPartsTable] FOREIGN KEY([MPMasterPartsTableID])
REFERENCES [dbo].[MPMasterPartsTable] ([MPMasterPartsTableID])
GO

ALTER TABLE [dbo].[MPMasterPartsTableColourList] CHECK CONSTRAINT [FK_MPMasterPartsTableColourList_MPMasterPartsTable]
GO

ALTER TABLE [dbo].[MPMasterPartsTableColourList] ADD  CONSTRAINT [DF_MPMasterPartsTableColourList_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO



