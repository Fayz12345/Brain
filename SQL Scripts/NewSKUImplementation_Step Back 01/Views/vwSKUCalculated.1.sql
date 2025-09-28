/****** Object:  View [dbo].[vwSKUCalculated]    Script Date: 04/06/2017 15:02:05 ******/
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

  Update ReceiveDetail set SKU = SKU_Calc
 From ReceiveDetail R
 Inner join vwSKUCalculated S on R.ReceiveDetailID = S.ReceiveDetailID
 
 Select SKU, SKU_Calc, len(SKU_Calc) as sl
 From ReceiveDetail R
 Inner join vwSKUCalculated S on R.ReceiveDetailID = S.ReceiveDetailID
 
 Alter Table ReceiveDetail Alter Column SKU nvarchar(50)

*/


ALTER VIEW [dbo].[vwSKUCalculated]
AS
SELECT     dbo.ReceiveDetail.ReceiveDetailID
         , dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Manufacturer',3,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Carrier',3,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Model',11,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Memory',5,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Colour',3,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Unlocked Status',2,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Grade',1,' ') + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Refurb',1,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Country',2,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'IsKitted',1,' ') + 
           space(10) as  SKU_CALC
         --, CASE WHEN len(isnull(Manufacturer.Name + Model.Name + dbo.GetIFSSKUCarrierSegment(ReceiveDetailID) + Colour.Name, '')) > 25 
         --       THEN LEFT(isnull(isnull(Manufacturer.Name,'XXX') + isnull(Model.Name,'XXX') + dbo.GetIFSSKUCarrierSegment(ReceiveDetailID) + isnull(Colour.Name,'XXX'), ''), 25) 
         --       ELSE isnull(isnull(Manufacturer.Name,'XXX') + isnull(Model.Name,'XXX') + dbo.GetIFSSKUCarrierSegment(ReceiveDetailID) + isnull(Colour.Name,'XXX'), '') + dbo.GetIFSSKUKittingSegment(ReceiveDetailID) 
         --  END AS SKU_CALC1
FROM dbo.ReceiveDetail
--LEFT OUTER JOIN dbo.[Option] AS Colour ON dbo.ReceiveDetail.ColourID = Colour.OptionID
--LEFT OUTER JOIN dbo.[Option] AS Model ON dbo.ReceiveDetail.ModelID = Model.OptionID 
--LEFT OUTER JOIN dbo.[Option] as Manufacturer ON dbo.ReceiveDetail.ManufacturerID = Manufacturer.OptionID




GO


