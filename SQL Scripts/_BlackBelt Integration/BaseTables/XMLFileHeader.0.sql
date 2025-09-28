/****** Object:  Table [dbo].[IFSXMLFileHeader]    Script Date: 04/03/2018 11:56:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Drop Table XMLFileHeader

CREATE TABLE [dbo].[XMLFileHeader](
	[XMLFileHeaderID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[XMLData] [xml] NULL,
	[FileName] [nvarchar](75) NULL,
	[FileType] [nvarchar](5) NULL,
	[StatusID] [numeric](18, 0) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_XMLFileHeader] PRIMARY KEY CLUSTERED 
(
	[XMLFileHeaderID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[XMLFileHeader] ADD  CONSTRAINT [DF_XMLFileHeaderXML_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO




