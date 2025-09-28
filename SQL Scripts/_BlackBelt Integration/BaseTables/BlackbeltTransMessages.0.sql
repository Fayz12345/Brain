/****** Object:  Table [dbo].[IFSPickListOrderDetail]    Script Date: 04/03/2018 14:59:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
--Drop Table BlackbeltTransMessages

CREATE TABLE [dbo].[BlackbeltTransMessages](
	[BlackbeltTransMessagesID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[BlackbeltTransRunLogID] [numeric](18, 0) NOT NULL,
	[BlackbeltTransDetailID] [numeric](18, 0) NULL,
	[Type] [varchar](50) NULL,
	[Message] [varchar](500) NULL,
	[Comment] [varchar](500) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_BlackbeltTransMessages] PRIMARY KEY CLUSTERED 
(
	[BlackbeltTransMessagesID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO


SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[BlackbeltTransMessages] ADD  CONSTRAINT [DF_BlackbeltTransMessages_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
