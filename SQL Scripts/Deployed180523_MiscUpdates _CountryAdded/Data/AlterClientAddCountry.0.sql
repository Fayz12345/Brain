Alter Table Client Add 
	[AddressLine3] [nvarchar](50) NULL,
	[AddressLine4] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL
GO

Alter table Client Alter Column StateOrProvince nvarchar(50)
Go

Alter table Client Alter Column CompanyName nvarchar(200)
GO
