
/****** Object:  View [dbo].[vwBBDashboardGridHeader]    Script Date: 06/28/2018 14:39:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*

Select * from vwBBDashboardGridHeader
Select * from vwBBDashboardGridDetail where BlackbeltTransHeaderID = 158 order by case when StatusDetail = 'Error' then 10
                                                                                       when StatusDetail = 'Parsed' then 20
                                                                                       when StatusDetail = 'Misc' then 50 else 25 end

*/




CREATE VIEW [dbo].[vwBBDashboardGridDetail]
AS
  
SELECT     H.BlackbeltTransHeaderID, H.XMLFileHeaderID, H.CreateDate, H.LastUpdateDate, H.ESN, H.ProjectTag, H.ProjectName, H.Message, H.Status, H.ProcessStatus, H.ReceiveDetailID, 
                      H.ClientLocationID, H.ClientLocationScanKey, H.ProcessScanKey, H.ProjectID, H.ProcessID, H.CarrierID, H.ManufacturerID, H.ModelID, H.ColourID, H.GradeID, D.BlackbeltTransDetailID, 
                      D.Status AS StatusDetail, D.ProcessStatus AS ProcessStatusDetail, D.ReceiveDetailID AS ReceiveDetailIDDetail, D.QuestionID, D.QuestionType, D.OptionID, D.ItemAbbreviation, D.[Key], D.Value, 
                      D.TranslationKey, D.TranslationValue, D.Message AS MessageDetail, D.CreateDate AS CreateDateDetail, D.CreateUser AS CreateUserDetail, D.LastUpdateDate AS LastUpdateDateDetail, 
                      D.LastUpdateUser AS LastUpdateUserDetail, Q.Name AS NameQuestion, Q.Description AS Question, O.Name AS AbbrOption, O.OptionText, O.ScanKey AS ScankeyOption
FROM         BlackbeltTransDetail AS D INNER JOIN
                      vwBBDashboardGridHeader AS H ON H.BlackbeltTransHeaderID = D.BlackbeltTransHeaderID LEFT OUTER JOIN
                      [Option] AS O ON D.OptionID = O.OptionID LEFT OUTER JOIN
                      Question AS Q ON D.QuestionID = Q.QuestionID



GO


                      