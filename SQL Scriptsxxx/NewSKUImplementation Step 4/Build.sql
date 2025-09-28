





























































/****** Object:  UserDefinedFunction [dbo].[IsIFSSkuValid]    Script Date: 07/27/2017 11:29:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*

Print dbo.IsAttributeASkuSegment(2210)

Select SKU, dbo.GetSKU(CarrierID, ManufacturerID, ModelID, ColourID) from ReceiveDetail

210
214
226
243
244
357
530
560


*/

Create FUNCTION [dbo].[IsAttributeASkuSegment](@QuestionID numeric(18))
RETURNS Bit
AS
BEGIN

Declare @Valid Bit

Select @Valid = 0 -- False

if exists (Select * from Question where Name in ('Manufacturer','Carrier','Model','Memory','Colour','Unlocked Status','Grade','IsKitted','Refurb','Country')  and QuestionID = @QuestionID)
   Select @Valid = 1 -- TRUE   

Return @Valid
END
GO
/****** Object:  UserDefinedFunction [dbo].[IsSkuValid]    Script Date: 08/08/2017 14:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*

Print dbo.GetSKU(6,5,9,3)

Select SKU, dbo.GetSKU(CarrierID, ManufacturerID, ModelID, ColourID) from ReceiveDetail

*/

ALTER FUNCTION [dbo].[IsSkuValid](@IMEI nvarchar(50), @CarrierABBR nvarchar(25), @ManufacturerABBR nvarchar(25), @ModelABBR nvarchar(25), @ColourABBR nvarchar(25))
RETURNS Bit
AS
BEGIN

Return 1         -- for now, just return true. Cleanup required.

Declare @Valid Bit
Declare @mReceiveDetailID numeric(18),@mCarrierID numeric(18), @mManufacturerID numeric(18), @mModelID numeric(18), @mColourID numeric(18)

Select @Valid = 0


Select @mReceiveDetailID = ReceiveDetailID, @mCarrierID = CarrierID, @mManufacturerID = ManufacturerID, @mModelID = ModelID, @mColourID = ColourID 
  From ReceiveDetail 
 where ESN = @IMEI and Version = '000'

if (ISNULL(@mReceiveDetailID, -1) < 1)
   return @Valid
   
-- If we are not given one of these attributes, then find what is on the IMEI.   
if LEN(@CarrierABBR) = 0
   Select @CarrierABBR = Name 
     from [Option] O
     where OptionID = @mCarrierID
   
if LEN(@ManufacturerABBR) = 0
   Select @ManufacturerABBR = Name 
     from [Option] O
     where OptionID = @mManufacturerID

if LEN(@ModelABBR) = 0
   Select @ModelABBR = Name 
     from [Option] O
     where OptionID = @mModelID

if LEN(@ColourABBR) = 0
   Select @ColourABBR = Name 
     from [Option] O
     where OptionID = @mColourID

if exists(Select * from [dbo].[vwMasterCarrierManufacturerSKU]
            where ABBR_Manufacturer = @ManufacturerABBR
              and ABBR_Carrier = @CarrierABBR
              and ABBR_Model = @ModelABBR
              and ABBR_Colour = @ColourABBR)
   Select @Valid = 1


Return @Valid
END
GO









































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


Create VIEW [dbo].[vwSKUSegmentWithKeys]
AS


SELECT     Segment.QuestionID, Data.OptionID, Segment.Name, Segment.Description, Data.Sequence, Data.Name AS ABBR, Data.OptionText as TextValue, Data.MacroKey, Data.MicroKey, Data.HelpText
FROM         Question AS Segment INNER JOIN
                      [Option] AS Data ON Segment.QuestionID = Data.QuestionID
Where Segment.Name in ('Manufacturer','Carrier','Model','Memory','Colour','Unlocked Status','Grade','IsKitted','Refurb','Country') 
--Order by Segment.Name, Data.Sequence, Data.Name



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





/****** Object:  View [dbo].[vwMasterCarrierManufacturerSKU]    Script Date: 08/03/2017 13:59:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--Select Condition from MasterCarrierManufacturerLookup


CREATE VIEW [dbo].[vwMasterCarrierManufacturerLookupList_ABBR]
AS


SELECT     MasterCarrierManufacturerLookup.MasterCarrierManufacturerLookupID, MasterCarrierManufacturerLookup.StatusID
                    --, MasterCarrierManufacturerLookup.Carrier
                    --, MasterCarrierManufacturerLookup.Manufacturer
                    --, MasterCarrierManufacturerLookup.Model
                    --, MasterCarrierManufacturerLookup.Colour

                    , Carrier.Name AS Carrier                    
                    , Manufacturer.Name as Manufacturer
                    , Model.Name AS Model
                    , Colour.Name AS Colour

                    , Carrier.OptionText AS CarrierText
                    , Manufacturer.OptionText as ManufacturerText
                    , Model.OptionText AS ModelText
                    , Colour.OptionText AS ColourText

                    , MasterCarrierManufacturerLookup.Condition                    
                    , MasterCarrierManufacturerLookup.SKU, MasterCarrierManufacturerLookup.UPC, MasterCarrierManufacturerLookup.Description, MasterCarrierManufacturerLookup.WarrantyStickerPlacement, 
                      MasterCarrierManufacturerLookup.Device_Handset, MasterCarrierManufacturerLookup.Bar_Flip, MasterCarrierManufacturerLookup.CDMA_HSPA, MasterCarrierManufacturerLookup.Retire, 
                      MasterCarrierManufacturerLookup.CreateDate, MasterCarrierManufacturerLookup.CreateUser, MasterCarrierManufacturerLookup.LastUpdateDate, 
                      MasterCarrierManufacturerLookup.LastUpdateUser, MasterCarrierManufacturerLookup.OptionCarrierID, MasterCarrierManufacturerLookup.OptionManufacturerID, 
                      MasterCarrierManufacturerLookup.OptionModelID, MasterCarrierManufacturerLookup.OptionColourID, MasterCarrierManufacturerLookup.NickName, MasterCarrierManufacturerLookup.SKU_B, 
                      MasterCarrierManufacturerLookup.SKU_C, MasterCarrierManufacturerLookup.SKU_Loaner, MasterCarrierManufacturerLookup.UPC_2, MasterCarrierManufacturerLookup.UPC_3, 
                      MasterCarrierManufacturerLookup.Unit_OS
FROM         MasterCarrierManufacturerLookup INNER JOIN
                      [Option] AS Carrier ON MasterCarrierManufacturerLookup.OptionCarrierID = Carrier.OptionID INNER JOIN
                      [Option] AS Manufacturer ON MasterCarrierManufacturerLookup.OptionManufacturerID = Manufacturer.OptionID INNER JOIN
                      [Option] AS Model ON MasterCarrierManufacturerLookup.OptionModelID = Model.OptionID INNER JOIN
                      [Option] AS Colour ON MasterCarrierManufacturerLookup.OptionColourID = Colour.OptionID                                                                                       
                                                                                    



GO





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








































/****** Object:  StoredProcedure [dbo].[Utility_LoadAttributeValue_02]    Script Date: 08/03/2017 21:18:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
/*


Exec Utility_LoadAttributeValue 'Colour','Black'
Exec Utility_LoadAttributeValue 'Colour','Blue'
Exec Utility_LoadAttributeValue 'Colour','Brown'
Exec Utility_LoadAttributeValue 'Colour','Coral'
Exec Utility_LoadAttributeValue 'Colour','Fushia'
Exec Utility_LoadAttributeValue 'Colour','Gold'
Exec Utility_LoadAttributeValue 'Colour','Green'
Exec Utility_LoadAttributeValue 'Colour','Greg'
Exec Utility_LoadAttributeValue 'Colour','Grey'
Exec Utility_LoadAttributeValue 'Colour','Gun Metal'
Exec Utility_LoadAttributeValue 'Colour','Orange'
Exec Utility_LoadAttributeValue 'Colour','Pink'
Exec Utility_LoadAttributeValue 'Colour','Purple'
Exec Utility_LoadAttributeValue 'Colour','Red'
Exec Utility_LoadAttributeValue 'Colour','Red '
Exec Utility_LoadAttributeValue 'Colour','Red/Black'
Exec Utility_LoadAttributeValue 'Colour','Silver'
Exec Utility_LoadAttributeValue 'Colour','Silver/Grey'
Exec Utility_LoadAttributeValue 'Colour','violet'
Exec Utility_LoadAttributeValue 'Colour','White'
Exec Utility_LoadAttributeValue 'Colour','White/Black'
Exec Utility_LoadAttributeValue 'Colour','White/Purple'





*/

Alter PROCEDURE [dbo].[Utility_LoadAttributeValue_WithDelete]
    @mAttributeName nVarchar(20),
    @mDelete int, 
    @mAttributeScankey nVarchar(50),
    @mAttributeItemName nVarchar(20),
    @mAttributeValue nVarchar(50),
    @mAttributeSeq nVarchar(10),
    @mUserName nVarchar(50),
    @mReturnMessage nvarchar(50) Output
   
AS
BEGIN
Set NOCOUNT ON
--Select Name from Question where Name = 'Colour'

Declare @mStatusID numeric(18)
Declare @mStatusDeleteID numeric(18)
Declare @mTypeID numeric(18)
Declare @mQuestionID numeric(18)
Declare @mOptionID numeric(18)

Select Top 1 @mQuestionID = QuestionID from Question where ltrim(rtrim(Question.Name)) = @mAttributeName
Select Top 1 @mTypeID = OptionTypeID from OptionType where [Type] = 'Other'
Select Top 1 @mStatusID = OptionStatusID from OptionStatus where Status = 'Active'
Select Top 1 @mStatusDeleteID = OptionStatusID from OptionStatus where Status = 'Inactive'
Select @mQuestionID = isnull(@mQuestionID, -1)
Select @mTypeID = isnull(@mTypeID, -1)
Select @mStatusID = isnull(@mStatusID, -1)
if @mQuestionID < 1 
   begin
   Select @mReturnMessage = 'Error: Question Not found ' + @mAttributeName
   Print 'Question Not found ' + @mAttributeName
   Return 0
   end
if @mTypeID < 1
   begin
   Select @mReturnMessage = 'Error: Type Not found ' + 'Other'
   Print 'Type Not found ' + 'Other'
   Return 0
   end
if @mStatusID < 1
   begin
   Select @mReturnMessage = 'Error: Status Not found ' + 'Active'
   Print 'Status Not found ' + 'Active'
   Return 0
   end   
   

Select @mOptionID = OptionID from [Option] where 1 = 1
                                           and QuestionID = @mQuestionID
                                           and OptionStatusID = @mStatusID
                                           and (Name = @mAttributeItemName
                                            or ScanKey = @mAttributeScankey
                                            or OptionText = @mAttributeValue)


-- Do we delete?
if (@mDelete > 0 and isnull(@mOptionID,-1) > 0)
    begin
    Update [Option] set OptionStatusID = @mStatusDeleteID, [LastUpdateDate]= GETDATE(), [LastUpdateUser] = @mUserName 
     where OptionID = @mOptionID
    Select @mReturnMessage = 'Updated: Status Set to Inactive '
    Print  'Status Set to Inactive '
    Return 0    
    end

-- Do we update?
if (isnull(@mOptionID,-1) > 0)
    begin
    Update [Option] set OptionStatusID = @mStatusID
         , [ScanKey] = @mAttributeScankey
         , [OptionText] = @mAttributeValue
         , [Name] = @mAttributeItemName
         , [Sequence] =  @mAttributeSeq                          
         , [LastUpdateDate]= GETDATE(), [LastUpdateUser] = @mUserName
     where OptionID = @mOptionID
    Select @mReturnMessage = 'Updated:'
    Print  'Attribute Updated'
    Return 0    
    end

-- Do we Add New?   
  
   
if Not Exists(Select OptionID from [Option] where QuestionID = @mQuestionID and OptionText =  @mAttributeValue )
   begin
   Print 'Insert:' + @mAttributeName + ':' + @mAttributeValue
   INSERT INTO [Option]
              ([ScanKey],[MacroKey]
              ,[OptionStatusID]
              ,[OptionTypeID]
              ,[OptionText]
              ,[HelpText]
              ,[QuestionID]
              ,[Name]
              ,[Sequence]
              ,[CreateDate]
              ,[CreateUser]
              ,[LastUpdateDate]
              ,[LastUpdateUser]
              ,[MicroKey])
     VALUES
           (@mAttributeScankey,''
           ,@mStatusID
           ,@mTypeID
           ,@mAttributeValue
           ,@mAttributeValue
           ,@mQuestionID
           ,@mAttributeItemName
           ,1
           ,getdate()
           ,@mUserName
           ,getdate()
           ,@mUserName
           ,'')  
    Select @mReturnMessage = 'Inserted'            
   end

Return 1

END

GO




/****** Object:  StoredProcedure [dbo].[Utility_ReplaceOptionAttributeID]    Script Date: 07/31/2017 11:37:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

/*

Select * from Question where name = 'Model'
Select * from [Option] o where -- questionID = 14 and 
         exists(select * from [option] b where b.optiontext = o.optiontext and b.QuestionID = o.questionID and b.OptionID != o.OptionID)
 order by optiontext


declare @mRecords int
exec Utility_ReplaceOptionAttributeID 149, 150, @mRecords Output
Print 'Rows Affected:' + convert(nvarchar(20), @mRecords)


declare @mRecords int
exec Utility_ReplaceOptionAttributeID 150, 149, @mRecords Output
Print 'Rows Affected:' + convert(nvarchar(20), @mRecords)



*/

ALTER PROCEDURE [dbo].[Utility_ReplaceOptionAttributeID]
        @mSourceID numeric(18),
        @mTargetOptionID numeric(18),
        @mUserName nvarchar(20),
        @mRecordsAffected int output

AS
BEGIN
SET NOCOUNT ON;

DECLARE @Rows   int
Select @Rows = 0
---your query here

--Select @mRecordsAffected = 12
--return




Update MasterBucketTransactions Set  OptionID = @mTargetOptionID Where OptionID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT
Update ClientAnswerRestrict Set  OptionID = @mTargetOptionID, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName Where OptionID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT
Update QuestionDependencies Set  SourceOptionID = @mTargetOptionID, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName Where SourceOptionID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT
Update QuestionDependencies Set TargetOptionID = @mTargetOptionID, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName Where TargetOptionID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

Update [Option_Text_Defaults] Set  SourceOptionID = @mTargetOptionID, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName Where SourceOptionID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT
Update [Option_Text_Defaults] Set TargetOptionID = @mTargetOptionID, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName Where TargetOptionID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT






print 'step 1 done'

-- The following 4 statements could leave duplicate records in the MasterCarrierManufacturerLookup. 
--     because this utility changes attributes with x id to attribute with y id, we will leave these out of the look
-- 



/*
Update [MasterCarrierManufacturerLookup] set [OptionModelID] = 
Update [MasterCarrierManufacturerLookup] set [OptionManufacturerID] = 
Update [MasterCarrierManufacturerLookup] set [OptionCarrierID] = 
Update [MasterCarrierManufacturerLookup] set [OptionColourID] = 

Update [MasterModelMemoryLookup] set [ModelID] = 
Update [MasterModelMemoryLookup] set [MemoryID] = 
*/

-- if the target exists, delete the original, otherwise change the original.
--if exists(select * from MasterCarrierManufacturerLookup where OptionCarrierID = @mTargetOptionID)
--   begin 
--   Delete MasterCarrierManufacturerLookup  where OptionCarrierID = @mSourceID
--   SELECT @Rows= @Rows + @@ROWCOUNT     
--   end
--else   
--   begin
--   Update MasterCarrierManufacturerLookup set OptionCarrierID = @mTargetOptionID, Carrier = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
--   From MasterCarrierManufacturerLookup Inner join [Option] on @mTargetOptionID
--   where OptionCarrierID = @mSourceID
--   SELECT @Rows= @Rows + @@ROWCOUNT  
--   end   


--if exists(select * from MasterCarrierManufacturerLookup where OptionManufacturerID = @mTargetOptionID)
--   begin 
--   Delete MasterCarrierManufacturerLookup  where OptionManufacturerID = @mSourceID
--   SELECT @Rows= @Rows + @@ROWCOUNT     
--   end
--else
--   begin
--   Update MasterCarrierManufacturerLookup set OptionManufacturerID = @mTargetOptionID, Manufacturer = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
--   From MasterCarrierManufacturerLookup Inner join [Option] on [Option].OptionID = @mTargetOptionID
--   where OptionManufacturerID = @mSourceID
--   SELECT @Rows= @Rows + @@ROWCOUNT  
--   end  
   
-- if exists(select * from MasterCarrierManufacturerLookup where OptionModelID = @mTargetOptionID)
--   begin 
--   Delete MasterCarrierManufacturerLookup  where OptionModelID = @mSourceID
--   SELECT @Rows= @Rows + @@ROWCOUNT     
--   end
--else
--   begin
--   Update MasterCarrierManufacturerLookup set OptionModelID = @mTargetOptionID, Model = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
--   From MasterCarrierManufacturerLookup Inner join [Option] on [Option].OptionID = @mTargetOptionID
--   where OptionModelID = @mSourceID
--   SELECT @Rows= @Rows + @@ROWCOUNT  
--   end  

-- if exists(select * from MasterCarrierManufacturerLookup where OptionColourID = @mTargetOptionID)
--   begin 
--   Delete MasterCarrierManufacturerLookup  where OptionColourID = @mSourceID
--   SELECT @Rows= @Rows + @@ROWCOUNT     
--   end
--else
--   begin
--   Update MasterCarrierManufacturerLookup set OptionColourID = @mTargetOptionID, Colour = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
--   From MasterCarrierManufacturerLookup Inner join [Option] on [Option].OptionID = @mTargetOptionID

--   where OptionColourID = @mSourceID
--   SELECT @Rows= @Rows + @@ROWCOUNT  
--   end  

update MasterSKU set StatusID = 3, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName where CarrierID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

update MasterSKU set StatusID = 3, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName where ManufacturerID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

update MasterSKU set StatusID = 3, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName where ModelID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

update MasterSKU set StatusID = 3, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName where ColourID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT



--print 'step 2 done'

Update MasterPartsRequestedLog set CarrierID = @mTargetOptionID, Carrier = [Option].OptionID, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
From MasterPartsRequestedLog Inner join [Option] on [Option].OptionID = @mTargetOptionID
where CarrierID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

Update MasterPartsRequestedLog set ManufacturerID = @mTargetOptionID, Manufacturer = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
From MasterPartsRequestedLog Inner join [Option] on [Option].OptionID = @mTargetOptionID
 where ManufacturerID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

Update MasterPartsRequestedLog set ModelID = @mTargetOptionID, Model = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
From MasterPartsRequestedLog Inner join [Option] on [Option].OptionID = @mTargetOptionID
where ModelID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

Update MasterPartsRequestedLog set ColourID = @mTargetOptionID,  Colour = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
From MasterPartsRequestedLog Inner join [Option] on [Option].OptionID = @mTargetOptionID
where ColourID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

Update MasterPartsLinkTableModelList set ModelID = @mTargetOptionID, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
From MasterPartsLinkTableModelList Inner join [Option] on [Option].OptionID = @mTargetOptionID
where ModelID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT




--print 'step 3 done'



Update ReceiveDetail set CarrierID = @mTargetOptionID, Carrier = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
From ReceiveDetail Inner join [Option] on [Option].OptionID = @mTargetOptionID
where CarrierID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

Update ReceiveDetail set ManufacturerID = @mTargetOptionID, Manufacturer = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
From ReceiveDetail Inner join [Option] on [Option].OptionID = @mTargetOptionID
 where ManufacturerID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

Update ReceiveDetail set ModelID = @mTargetOptionID, Model = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
From ReceiveDetail Inner join [Option] on [Option].OptionID = @mTargetOptionID
where ModelID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

Update ReceiveDetail set ColourID = @mTargetOptionID,  Colour = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
From ReceiveDetail Inner join [Option] on [Option].OptionID = @mTargetOptionID
where ColourID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

Update ReceiveDetail set GradeID = @mTargetOptionID,  Grade = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
From ReceiveDetail Inner join [Option] on [Option].OptionID = @mTargetOptionID
where GradeID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

--print 'step ReceiveDetail done'


Update ReceiveDetailItem set OptionID = @mTargetOptionID, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName where OptionID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT
--Update ReceiveDetailItem_03 set OptionID = @mTargetOptionID where OptionID = @mSourceID
--SELECT @Rows= @Rows + @@ROWCOUNT
--Update REceiveDetailItem_Archive set OptionID = @mTargetOptionID where OptionID = @mSourceID
--SELECT @Rows= @Rows + @@ROWCOUNT
--Update ReceiveDetailItem_Archive_01 set OptionID = @mTargetOptionID where OptionID = @mSourceID
--SELECT @Rows= @Rows + @@ROWCOUNT
--Update ReceiveDetailItem_Archive_02 set OptionID = @mTargetOptionID where OptionID = @mSourceID
--SELECT @Rows= @Rows + @@ROWCOUNT
--Update ReceiveDetailItem_Deleted_01 set OptionID = @mTargetOptionID where OptionID = @mSourceID
--SELECT @Rows= @Rows + @@ROWCOUNT
Update ReceiveDetailPreReceiveAttribute set OptionID = @mTargetOptionID, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName where OptionID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT
Update ReceiveDetailItemBulk set OptionID = @mTargetOptionID, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName where OptionID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

--print 'step ReceiveDetailItem done'



Update [Option] set OptionText = 'XX-' + ltrim(rtrim(OptionText)), OptionStatusID = 2, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName where OptionID = @mSourceID 
-- SELECT @Rows= @Rows + @@ROWCOUNT


Select @mRecordsAffected = @Rows
--print 'step Last done'


End


/****** Object:  StoredProcedure [dbo].[Utility_ReplaceOptionAttributeID_GO]    Script Date: 07/31/2017 11:37:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

/*

Select * from Question where name = 'Model'
Select * from [Option] o where -- questionID = 14 and 
         exists(select * from [option] b where b.optiontext = o.optiontext and b.QuestionID = o.questionID and b.OptionID != o.OptionID)
 order by optiontext


declare @mRecords int
exec Utility_ReplaceOptionAttributeID 149, 150, @mRecords Output
Print 'Rows Affected:' + convert(nvarchar(20), @mRecords)



exec Utility_ReplaceOptionAttributeID_GO 842,843

exec Utility_ReplaceOptionAttributeID_GO 2443,1872
exec Utility_ReplaceOptionAttributeID_GO 4569,4450
exec Utility_ReplaceOptionAttributeID_GO 2498,1722
exec Utility_ReplaceOptionAttributeID_GO 1757,1445
exec Utility_ReplaceOptionAttributeID_GO 2499,1759
exec Utility_ReplaceOptionAttributeID_GO 1768,1444
exec Utility_ReplaceOptionAttributeID_GO 2524,2520
exec Utility_ReplaceOptionAttributeID_GO 2568,2564
exec Utility_ReplaceOptionAttributeID_GO 2569,2565
exec Utility_ReplaceOptionAttributeID_GO 2566,1784
exec Utility_ReplaceOptionAttributeID_GO 2570,1784
exec Utility_ReplaceOptionAttributeID_GO 2571,2567
exec Utility_ReplaceOptionAttributeID_GO 5841,5840
exec Utility_ReplaceOptionAttributeID_GO 2383,1869
exec Utility_ReplaceOptionAttributeID_GO 2476,2475
exec Utility_ReplaceOptionAttributeID_GO 3272,2648
exec Utility_ReplaceOptionAttributeID_GO 6160,5995
exec Utility_ReplaceOptionAttributeID_GO 2421,1840
exec Utility_ReplaceOptionAttributeID_GO 2342,1891
exec Utility_ReplaceOptionAttributeID_GO 3359,3315
exec Utility_ReplaceOptionAttributeID_GO 2481,1899
exec Utility_ReplaceOptionAttributeID_GO 2795,2794
exec Utility_ReplaceOptionAttributeID_GO 2731,1771
exec Utility_ReplaceOptionAttributeID_GO 3235,2627

exec Utility_ReplaceOptionAttributeID_GO 2570, 2566

2566
2570



*/

ALTER PROCEDURE [dbo].[Utility_ReplaceOptionAttributeID_GO]
        @mSourceID numeric(18),
        @mTargetOptionID numeric(18),
        @mUserName nvarchar(20)

AS
BEGIN
SET NOCOUNT ON;


declare @mRecords int
declare @mTotalRecords int
Select @mTotalRecords = 0
exec Utility_ReplaceOptionAttributeID @mSourceID, @mTargetOptionID, @mUserName, @mRecords Output
Print 'Rows Affected:' + convert(nvarchar(20), @mRecords) + ' Source:' + convert(nvarchar(20), @mSourceID) + ' Target:' + convert(nvarchar(20), @mTargetOptionID)
Select @mTotalRecords = @mTotalRecords + isnull(@mRecords,0)


End
GO











































































