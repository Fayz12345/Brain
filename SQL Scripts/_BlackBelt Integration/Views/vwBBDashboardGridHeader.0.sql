/****** Object:  View [dbo].[vwBlackbeltTranslationList]    Script Date: 06/27/2018 15:30:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*

Select * from vwBBDashboardGridHeader

*/




CREATE VIEW [dbo].[vwBBDashboardGridHeader]
AS
  
Select BlackbeltTransHeaderID
     , XMLFileHeaderID
     , CreateDate
     , LastUpdateDate
     , ESN
     , ProjectTag
     , ProjectName
     , Message
     , Status
     , ProcessStatus
     , ReceiveDetailID
     , ClientLocationID
     , ClientLocationScanKey
     , ProcessScanKey
     , ProjectID
     , ProcessID
     , CarrierID
     , ManufacturerID
     , ModelID
     , ColourID
     , GradeID
  from BlackbeltTransHeader


GO


