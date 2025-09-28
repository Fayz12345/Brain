/****** Object:  Table [dbo].[OrderHeader]    Script Date: 07/11/2018 16:18:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO


Alter Table OrderDetail Add Discount numeric(18,8)
GO