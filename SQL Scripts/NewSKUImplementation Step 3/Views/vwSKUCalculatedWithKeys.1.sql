


/****** Object:  View [dbo].[vwSKUCalculated]    Script Date: 07/27/2017 10:33:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






/*
Select * from vwSKUCalculated where ReceiveDetailID = 2892
Select dbo.GetIFSSKUCarrierSegment(ReceiveDetailID) from ReceiveDetail  where ReceiveDetailID = 127912
Select dbo.GetIFSSKUKittingSegment(ReceiveDetailID) from ReceiveDetail  where ReceiveDetailID = 127912

Select * from [Option] where OptionID = 1823

Select * from REceiveDetail where ReceiveDetailID = 127912
Select ReceiveDetail.ReceiveDetailID
       ,Manufacturer.Name as Manufactuer,
                                   Model.Name as Model,
                                   dbo.GetIFSSKUCarrierSegment(ReceiveDetailID) as Carrier,
                                   Colour.Name as Colour
FROM         dbo.[Option] AS Colour RIGHT OUTER JOIN
                      dbo.ReceiveDetail ON Colour.OptionID = dbo.ReceiveDetail.ColourID LEFT OUTER JOIN
                      dbo.[Option] AS Model ON dbo.ReceiveDetail.ModelID = Model.OptionID LEFT OUTER JOIN
                      dbo.[Option] as Manufacturer ON dbo.ReceiveDetail.ManufacturerID = Manufacturer.OptionID 
 where ReceiveDetailID = 127912
*/


--Create VIEW [dbo].[vwSKUCalculatedWithKeys]
--AS



--SELECT     ReceiveDetail.ESN, ReceiveDetail.Version, ReceiveDetailItem.Value
--          ,Manufacturer.QuestionID, Manufacturer.OptionID, Manufacturer.Name, Manufacturer.Description, Manufacturer.Sequence, Manufacturer.ABBR, Manufacturer.TextValue
--          ,Carrier.QuestionID, Carrier.OptionID, Carrier.Name, Carrier.Description, Carrier.Sequence, Carrier.ABBR, Carrier.TextValue
--          ,Model.QuestionID, Model.OptionID, Model.Name, Model.Description, Model.Sequence, Model.ABBR, Model.TextValue
--          ,Memory.QuestionID, Memory.OptionID, Memory.Name, Memory.Description, Memory.Sequence, Memory.ABBR, Memory.TextValue                    
--          ,Colour.QuestionID, Colour.OptionID, Colour.Name, Colour.Description, Colour.Sequence, Colour.ABBR, Colour.TextValue                    
--          ,UnlockedStatus.QuestionID, UnlockedStatus.OptionID, UnlockedStatus.Name, UnlockedStatus.Description, UnlockedStatus.Sequence, UnlockedStatus.ABBR, UnlockedStatus.TextValue                    
--          ,Grade.QuestionID, Grade.OptionID, Grade.Name, Grade.Description, Grade.Sequence, Grade.ABBR, Grade.TextValue                    
--          ,IsKitted.QuestionID, IsKitted.OptionID, IsKitted.Name, IsKitted.Description, IsKitted.Sequence, IsKitted.ABBR, IsKitted.TextValue                    
--          ,Refurb.QuestionID, Refurb.OptionID, Refurb.Name, Refurb.Description, Refurb.Sequence, Refurb.ABBR, Refurb.TextValue                    
--          ,Country.QuestionID, Country.OptionID, Country.Name, Country.Description, Country.Sequence, Country.ABBR, Country.TextValue               
--FROM         ReceiveDetail 
--INNER JOIN ReceiveDetailItem ON ReceiveDetail.ReceiveDetailID = ReceiveDetailItem.ReceiveDetailID 
--LEFT OUTER JOIN vwSKUSegmentWithKeys Manufacturer ON ReceiveDetailItem.OptionID = Manufacturer.OptionID and Manufacturer.Name = 'Manufacturer'
--LEFT OUTER JOIN vwSKUSegmentWithKeys Carrier ON ReceiveDetailItem.OptionID = Carrier.OptionID and Carrier.Name = 'Carrier'
--LEFT OUTER JOIN vwSKUSegmentWithKeys Model ON ReceiveDetailItem.OptionID = Model.OptionID and Model.Name = 'Model'
--LEFT OUTER JOIN vwSKUSegmentWithKeys Memory ON ReceiveDetailItem.OptionID = Memory.OptionID and Memory.Name = 'Memory'
--LEFT OUTER JOIN vwSKUSegmentWithKeys Colour ON ReceiveDetailItem.OptionID = Colour.OptionID and Colour.Name = 'Colour'
--LEFT OUTER JOIN vwSKUSegmentWithKeys UnlockedStatus ON ReceiveDetailItem.OptionID = UnlockedStatus.OptionID and UnlockedStatus.Name = 'Unlocked Status'
--LEFT OUTER JOIN vwSKUSegmentWithKeys Grade ON ReceiveDetailItem.OptionID = Grade.OptionID and Grade.Name = 'Grade'
--LEFT OUTER JOIN vwSKUSegmentWithKeys IsKitted ON ReceiveDetailItem.OptionID = IsKitted.OptionID and IsKitted.Name = 'IsKitted'
--LEFT OUTER JOIN vwSKUSegmentWithKeys Refurb ON ReceiveDetailItem.OptionID = Refurb.OptionID and Refurb.Name = 'Refurb'
--LEFT OUTER JOIN vwSKUSegmentWithKeys Country ON ReceiveDetailItem.OptionID = Country.OptionID and Country.Name = 'Country'

--Where not Manufacturer.QuestionID is null
--   or not Carrier.QuestionID is null
--   or not Model.QuestionID is null
--   or not Memory.QuestionID is null
--   or not Colour.QuestionID is null
--   or not UnlockedStatus.QuestionID is null
--   or not Grade.QuestionID is null
--   or not IsKitted.QuestionID is null
--   or not Refurb.QuestionID is null
--   or not Country.QuestionID is null
--Order by ReceiveDetail.ReceiveDetailID   
                  

--SELECT     ReceiveDetail.ReceiveDetailID, ReceiveDetail.ESN, ReceiveDetail.Version
--         , Manufacturer.Name as Manufacturer
--         , Carrier.Name as Carrier        
--         , Model.Name as Model
--         , Memory.Name as Memory
         

--         , Colour.Name as Colour                           
--FROM   ReceiveDetail
--Inner join [Option] Manufacturer ON Manufacturer.OptionID = ReceiveDetail.ManufacturerID
--INNER JOIN Question ON Question.QuestionID = Manufacturer.QuestionID AND Question.Name = 'Manufacturer'

--Inner join [Option] Model ON Model.OptionID = ReceiveDetail.ModelID
--INNER JOIN Question q1 ON q1.QuestionID = Model.QuestionID AND q1.Name = 'Model'

--Inner join [Option] Carrier ON Carrier.OptionID = ReceiveDetail.CarrierID
--INNER JOIN Question q2 ON q2.QuestionID = Carrier.QuestionID AND q2.Name = 'Carrier'

--Inner join [Option] Memory ON Memory.OptionID = ReceiveDetail.CarrierID
--INNER JOIN Question q4 ON (q4.QuestionID = Memory.QuestionID AND q4.Name = 'Memory') or Memory.QuestionID is null


--Inner join [Option] Colour ON Colour.OptionID = ReceiveDetail.ColourID
--INNER JOIN Question q3 ON q3.QuestionID = Colour.QuestionID AND q3.Name = 'Colour'

----Order by ESN, Version, ReceiveDetailID

/*
SELECT     dbo.ReceiveDetail.ReceiveDetailID
         , dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Manufacturer',3,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Carrier',3,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Model',11,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Memory',5,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Colour',3,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Unlocked Status',1,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Grade',1,' ') +  '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'IsKitted',2,' ') +  '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Refurb',1,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Country',2,' ') +
           space(9) as  SKU_CALC
         --, CASE WHEN len(isnull(Manufacturer.Name + Model.Name + dbo.GetIFSSKUCarrierSegment(ReceiveDetailID) + Colour.Name, '')) > 25 
         --       THEN LEFT(isnull(isnull(Manufacturer.Name,'XXX') + isnull(Model.Name,'XXX') + dbo.GetIFSSKUCarrierSegment(ReceiveDetailID) + isnull(Colour.Name,'XXX'), ''), 25) 
         --       ELSE isnull(isnull(Manufacturer.Name,'XXX') + isnull(Model.Name,'XXX') + dbo.GetIFSSKUCarrierSegment(ReceiveDetailID) + isnull(Colour.Name,'XXX'), '') + dbo.GetIFSSKUKittingSegment(ReceiveDetailID) 
         --  END AS SKU_CALC1
FROM dbo.ReceiveDetail
--LEFT OUTER JOIN dbo.[Option] AS Colour ON dbo.ReceiveDetail.ColourID = Colour.OptionID
--LEFT OUTER JOIN dbo.[Option] AS Model ON dbo.ReceiveDetail.ModelID = Model.OptionID 
--LEFT OUTER JOIN dbo.[Option] as Manufacturer ON dbo.ReceiveDetail.ManufacturerID = Manufacturer.OptionID
*/


/*


SELECT     ReceiveDetail.ReceiveDetailID, ReceiveDetail.ESN, ReceiveDetail.Version
         , Manufacturer.Name as Manufacturer
         , Model.Name as Model
         , Carrier.Name as Carrier
         , Colour.Name as Colour                           
FROM   ReceiveDetail
Inner join [Option] Manufacturer ON Manufacturer.OptionID = ReceiveDetail.ManufacturerID
INNER JOIN Question ON Question.QuestionID = Manufacturer.QuestionID AND Question.Name = 'Manufacturer'

Inner join [Option] Model ON Model.OptionID = ReceiveDetail.ModelID
INNER JOIN Question q1 ON q1.QuestionID = Model.QuestionID AND q1.Name = 'Model'

Inner join [Option] Carrier ON Carrier.OptionID = ReceiveDetail.CarrierID
INNER JOIN Question q2 ON q2.QuestionID = Carrier.QuestionID AND q2.Name = 'Carrier'

Inner join [Option] Colour ON Colour.OptionID = ReceiveDetail.ColourID
INNER JOIN Question q3 ON q3.QuestionID = Colour.QuestionID AND q3.Name = 'Colour'

Order by ESN, Version, ReceiveDetailID


*/


GO


