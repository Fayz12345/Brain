USE [GMP_Data]
GO

/****** Object:  Table [dbo].[MPMasterPartsRequestedLog]    Script Date: 05/07/2015 16:24:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MPMasterPartsRequestedLog](
	[MPMasterPartsRequestedLogID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MPMasterPartsTechAssignedLogID] [numeric](18, 0) NULL,
	[Status] [nvarchar](20) NOT NULL,
	[ReceiveDetailID] [numeric](18, 0) NOT NULL,


	[CarrierID] [numeric](18, 0) NULL,
	[ManufacturerID] [numeric](18, 0) NULL,
	[ModelID] [numeric](18, 0) NULL,
	[ColourID] [numeric](18, 0) NULL,
	[IFSRequestLocation] [nvarchar](50) NOT NULL,
	[RequestedPart] [nvarchar](200) NOT NULL,
	[PartNote] [nvarchar](200) NOT NULL,
	[TechUser] [nvarchar](50) NOT NULL,
	[CancelDate] [datetime] NULL,
	[CancelUser] [nvarchar](50) NULL,

	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MPMasterPartsRequestedLog] PRIMARY KEY CLUSTERED 
(
	[MPMasterPartsRequestedLogID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MPMasterPartsRequestedLog]  WITH CHECK ADD  CONSTRAINT [FK_MPMasterPartsRequestedLog_ReceiveDetail] FOREIGN KEY([ReceiveDetailID])
REFERENCES [dbo].[ReceiveDetail] ([ReceiveDetailID])
GO

ALTER TABLE [dbo].[MPMasterPartsRequestedLog] CHECK CONSTRAINT [FK_MPMasterPartsRequestedLog_ReceiveDetail]
GO

ALTER TABLE [dbo].[MPMasterPartsRequestedLog] ADD  CONSTRAINT [DF_MPMasterPartsRequestedLog_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO


