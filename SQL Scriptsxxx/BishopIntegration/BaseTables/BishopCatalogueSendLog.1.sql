
/****** Object:  Table [dbo].[InvtTran_IFS]    Script Date: 05/28/2015 23:07:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--Drop Table [BishopCatalogueSendLog]

CREATE TABLE [dbo].[BishopCatalogueSendLog](
	[BishopCatalogueSendLogID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[BishopGroupID] [numeric](18, 0) NULL,	
	[BishopGroupLastID] [numeric](18, 0) NULL,	
	[SKU] [nvarchar](50) NULL,
	[Qty] [int] NULL,
	[LastOnHandQTY] [int] NULL,
	[DifferenceQty] [int] NULL,
	[ThisSendDate] [datetime] NULL,
	[LastSendDate] [datetime] NULL,
	[Allocated] [int] NULL,
	[Price] [numeric](18,7) Null,
	[SendType] [int] NULL
 CONSTRAINT [PK_BishopCatalogueSendLog] PRIMARY KEY CLUSTERED 
(
	[BishopCatalogueSendLogID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

Create Index BishopCatSKU on BishopCatalogueSendLog(SKU, SendType)
Go

Create Index BishopCatGroupID on BishopCatalogueSendLog(BishopGroupID, SKU, SendType)
Go

Create Index BishopCatGroupLastID on BishopCatalogueSendLog(BishopGroupLastID, SKU, SendType)
Go

