/****** Object:  Table [dbo].[OrderHeader]    Script Date: 07/11/2018 16:18:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO


Alter Table OrderDetail Add Discount numeric(18,8)
GO/****** Object:  Table [dbo].[OrderHeader]    Script Date: 07/11/2018 16:18:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO


Alter Table OrderHeader Add OrderType nvarchar(25) NULL
GO
Alter Table OrderHeader Add Currency nvarchar(25) NULL
GO
Alter Table OrderHeader Add Freight numeric(18,2) NULL
GO
Alter Table OrderHeader Add Tax numeric(18,2) NULL
GO
Alter Table OrderHeader Add TaxRate numeric(18,8) NULL
GO



















