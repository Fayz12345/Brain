



/****** Object:  View [dbo].[View_ReceiveDetail_Data]    Script Date: 11/24/2016 15:55:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[View_ReceiveDetail_Data]
AS
SELECT C.ClientID
     , CL.ClientLocationID
     , R.ReceiveDetailID
     , Item.ReceiveDetailItemID
     , Data.QuestionID
     , Data.OptionID
     , C.CompanyName
     , CL.ScanKey AS ScanKey_CL
     , CL.StoreSuffix as StoreSuffix_CL
     , CL.StoreNumber as StoreNumber_CL
     , CL.CompanyName AS CompanyName_CL
     , CL.AddressLine1 AS AddressLine1_CL
     , CL.AddressLine2 AS AddressLine2_CL
     , CL.City AS City_CL
     , CL.StateOrProvince AS StateOrProvince_CL
     , CL.PostalCode AS PostalCode_CL
     , CL.PhoneNumber AS PhoneNumber_CL
     , R.ESN
     , R.Version
     , S.Status
     , P.Name as Project_Name
     , R.RMANumber
     , R.ProjectTag      
     , R.ReceiveDate
     , dbo.GetReceivedDetailCurrentProcessName(R.ReceiveDetailID) as CurrentProcess
     , dbo.GetReceivedDetailCurrentProcessNameAndDate(R.ReceiveDetailID) as CurrentProcessx
     , R.Carrier
     , R.Manufacturer
     , R.Model
     , R.Colour
     , R.Grade
     , R.SKU
     , Data.Question
     , Data.Value    
     , Data.QuestionType      
     , R.MiscNote
     , R.CreateDate as CreateDate_ESN
     , R.CreateUser as CreateUser_ESN
     , R.LastUpdateDate as LastUpdateDate_ESN
     , R.LastUpdateUser as LastUpdateUser_ESN
     , Item.CreateDate AS CreateDate_Value
     , Item.CreateUser AS CreateUser_Value
     , Item.LastUpdateDate AS LastUpdateDate_Value
     , Item.LastUpdateUser AS LastUpdateUser_Value
FROM ReceiveDetail AS R 
INNER JOIN ClientLocation AS CL ON R.ClientLocationID = CL.ClientLocationID 
INNER JOIN Client AS C ON CL.ClientID = C.ClientID 
INNER JOIN ReceiveDetailItem AS Item ON R.ReceiveDetailID = Item.ReceiveDetailID 
INNER JOIN View_ReceiveDetailItem_Data AS Data ON Item.ReceiveDetailItemID = Data.ReceiveDetailItemID
INNER JOIN ReceiveDetailStatus S on R.StatusID = S.ReceiveDetailStatusID
Inner join Project P on P.ProjectID = R.ProjectID

GO


