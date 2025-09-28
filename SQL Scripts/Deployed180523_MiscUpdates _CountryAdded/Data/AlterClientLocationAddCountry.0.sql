Alter Table ClientLocation Add 
	[AddressLine3] [nvarchar](50) NULL,
	[AddressLine4] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL
Go

Alter table ClientLocation Alter Column StateOrProvince nvarchar(50)
Go

Alter table ClientLocation Alter Column CompanyName nvarchar(200)
GO
