





















/*

-- Key the table, but remove what is in it.
-- The table is created back at the beginning of time.
-- But this implement, for Bridge is the first time we have utilized it.
Delete XMLFileHeader
Go
-----------------------------------------------------------------------------------

Drop Table BlackbeltTransDetail
GO

Drop Table BlackbeltTransHeader
Go


Drop Table BlackbeltTranslationListChangeLog
Go

Drop Table BlackbeltTranslationList
Go 

Drop Procedure Get_XMLTranslationValue
Go
Drop Procedure Job_PickUpXMLFiles
Go

Drop Procedure BlackBelt_ParseData
Go

Drop Procedure BlackBelt_ParseDataEdit
Go

Drop Procedure Job_LoadBlackBelt
Go

*/









































/****** Object:  UserDefinedFunction [dbo].[GetIFSSKU]    Script Date: 04/23/2020 13:21:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*

Print dbo.GetIFSCondtion(69510)

47981
47982
47983
47985
47987
47990


Select Top 100 ReceiveDetailID
Into #TempRD
 from ReceiveDetail
where Version = '000' -- and IFSCondition is null
Order by CreateDate Desc

Update ReceiveDetail set IFSCondition = dbo.GetIFSCondtion(ReceiveDetailID)
Where REceiveDetailID in (Select ReceiveDetailID from #TempRD)

Select * from ReceiveDetail
Where REceiveDetailID in (Select ReceiveDetailID from #TempRD)


Update ReceiveDetail set IFSCondition = NULL
Where REceiveDetailID in (Select ReceiveDetailID from #TempRD)

Drop table #TempRD
Select Top 1001 ReceiveDetailID, IFSCondition, dbo.GetIFSCondtion(ReceiveDetailID) from ReceiveDetail

Create Index Question_Condition2 on Question(IFS_Condition, IFS_Condition_Sequence)


*/

ALTER FUNCTION [dbo].[GetIFSSKU](@mReceiveDetailID numeric(18))
RETURNS nVarchar(50)
AS
BEGIN
Declare @mReturnValue nvarchar(50)
Select @mReturnValue = '';

Select @mReturnValue = SKU_Calc from vwSKUCalculated where ReceiveDetailID = @mReceiveDetailID
Select @mReturnValue = ISNULL(@mReturnValue,'')

Return @mReturnValue

END
GO
/****** Object:  UserDefinedFunction [dbo].[GetSKU]    Script Date: 04/23/2020 13:22:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*

Print dbo.GetSKU(6,5,9,3)

Select SKU, dbo.GetSKU(CarrierID, ManufacturerID, ModelID, ColourID) from ReceiveDetail

*/

ALTER FUNCTION [dbo].[GetSKU](@mCarrierID numeric(18),@mManufacturerID numeric(18),@mModelID numeric(18),@mColourID numeric(18))
RETURNS nVarchar(25)
AS
BEGIN
Declare @CarrierABBR nvarchar(25)
Declare @ManufacturerABBR nvarchar(25)
Declare @ModelABBR nvarchar(25)
Declare @ColourABBR nvarchar(25)

SELECT @CarrierABBR = [Option].Name FROM [Option] Where OptionID = @mCarrierID
SELECT @ManufacturerABBR = [Option].Name FROM [Option] Where OptionID = @mManufacturerID
SELECT @ModelABBR = [Option].Name FROM [Option] Where OptionID = @mModelID
SELECT @ColourABBR = [Option].Name FROM [Option] Where OptionID = @mColourID

Return isnull(@ManufacturerABBR,'') + '-' + isnull(@ModelABBR,'') + '-' + isnull(@CarrierABBR,'')
END
Go


/****** Object:  UserDefinedFunction [dbo].[GetSkuDescription]    Script Date: 04/23/2020 13:23:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*

Select SkuDescription = dbo.GetSkuDescription(3318,1701,1770,1446)

Select SKU, dbo.GetSkuDescription(CarrierID, ManufacturerID, ModelID, ColourID) from ReceiveDetail

*/

ALTER FUNCTION [dbo].[GetSkuDescription] (@CarrierID numeric(18),@ManufacturerID numeric(18),@ModelID numeric(18),@ColorID numeric(18))
RETURNS nVarchar(255)
AS
BEGIN
	Declare @Carrier nvarchar(255)
	Declare @Manufacturer nvarchar(255)
	Declare @Model nvarchar(255)
	Declare @Color nvarchar(255)

	Select @Manufacturer = [Option].OptionText FROM [Option] Where OptionID = @ManufacturerID
	Select @Model = [Option].OptionText FROM [Option] Where OptionID = @ModelID
	Select @Carrier = [Option].OptionText FROM [Option] Where OptionID = @CarrierID
	Select @Color = [Option].OptionText FROM [Option] Where OptionID = @ColorID

    -- Return isnull(@ManufacturerABBR,'') + '-' + isnull(@ModelABBR,'') + '-' + isnull(@CarrierABBR,'')
	Return isnull(@Manufacturer,'') + ' ' + isnull(@Model,'') + ' ' + isnull(@Carrier,'')
END

Go



/****** Object:  UserDefinedFunction [dbo].[GetSKUFromMaster]    Script Date: 04/23/2020 13:24:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*

Print dbo.GetSKU(6,5,9,3)

Select SKU, dbo.GetSKU(CarrierID, ManufacturerID, ModelID, ColourID) from ReceiveDetail

*/

ALTER FUNCTION [dbo].[GetSKUFromMaster](@mCarrierID numeric(18),@mManufacturerID numeric(18),@mModelID numeric(18),@mColourID numeric(18))
RETURNS nVarchar(25)
AS
BEGIN
Declare @SKU nvarchar(25)



Select @SKU = [dbo].[GetSKU](@mCarrierID,@mManufacturerID ,@mModelID,@mColourID)

--if Exists(Select SKU from MasterSKU where CarrierID = @mCarrierID and ManufacturerID = @mManufacturerID and ModelID = @mModelID and ColourID = @mColourID)
--   Select @SKU = SKU from MasterSKU where CarrierID = @mCarrierID and ManufacturerID = @mManufacturerID and ModelID = @mModelID and ColourID = @mColourID
--else
--   Select @SKU = 'Unknown'


--Declare @CarrierABBR nvarchar(25)
--Declare @ManufacturerABBR nvarchar(25)
--Declare @ModelABBR nvarchar(25)
--Declare @ColourABBR nvarchar(25)

--SELECT @CarrierABBR = [Option].Name FROM [Option] Where OptionID = @mCarrierID
--SELECT @ManufacturerABBR = [Option].Name FROM [Option] Where OptionID = @mManufacturerID
--SELECT @ModelABBR = [Option].Name FROM [Option] Where OptionID = @mModelID
--SELECT @ColourABBR = [Option].Name FROM [Option] Where OptionID = @mColourID

--Return isnull(@ManufacturerABBR,'') + isnull(@ModelABBR,'') + isnull(@CarrierABBR,'') + isnull(@ColourABBR,'')
return @SKU
END
Go
USE [BW_Sandbox02]
GO
/****** Object:  UserDefinedFunction [dbo].[GetSKUSegment]    Script Date: 04/23/2020 13:39:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

/*

Print dbo.GetSKUSegment(2892,'Unlocking Receive',3)
Print dbo.GetSKUSegment(2892,'Carrier',3,'y')
Print dbo.GetSKUSegment(2892,'Manufacturer',9,'y')
Print dbo.GetSKUSegment(2892,'Carrier',3)
Print dbo.GetSKUSegment(2892,'Carrier',3)
Select * from ReceiveDetail 
2892
2893
2894
2895
2896
2897
2898
2899
2900
2901
2902
2903
2904
2905
2906
2907
2908
2909

*/

ALTER FUNCTION [dbo].[GetSKUSegment](@mReceiveDetailID numeric(18), @mQuestionName nvarchar(20), @PadLength int, @Default nvarchar(10))
RETURNS nvarchar(15)
AS
BEGIN
Declare @mReturnValue nvarchar(50)
--Select @mQuestionName = 'Unlocking Receive'
--Declare @mCarrierSegment nvarchar(20)
--Declare @mUnlockSegment nvarchar(20)

Select @mReturnValue = [Option].Name
       FROM ReceiveDetailItem 
               INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID AND ReceiveDetailItem.OptionID = [Option].OptionID 
               INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
               WHERE (ReceiveDetailItem.ReceiveDetailID = @mReceiveDetailID) AND ((Question.Name = @mQuestionName))

Select @mReturnValue = ISNULL(@mReturnValue,replicate(@Default,@PadLength))
--if (LEN(@mReturnValue) != @PadLength)
--    begin
--    select @mReturnValue = RIGHT(replicate(@Default,@PadLength) + @mReturnValue, @PadLength)
--    end

Return ltrim(rtrim(@mReturnValue))

END




/****** Object:  View [dbo].[vwSKUCalculated]    Script Date: 04/23/2020 13:25:33 ******/
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


Alter VIEW [dbo].[vwSKUCalculated]
AS
SELECT     dbo.ReceiveDetail.ReceiveDetailID
         , dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Manufacturer',15,' ') + '-' + 
           --dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Carrier',3,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Model',15,' ') + '-' + 
           --dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Memory',5,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Colour',15,' ') +
           --dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Unlocked Status',1,' ') + '-' + 
           --dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Grade',1,' ') +  '-' + 
           --dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'IsKitted',2,' ') +  '-' + 
           --dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Refurb',1,' ') + '-' + 
           --dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Country',2,' ') +
           space(9) as  SKU_CALC
         --, CASE WHEN len(isnull(Manufacturer.Name + Model.Name + dbo.GetIFSSKUCarrierSegment(ReceiveDetailID) + Colour.Name, '')) > 25 
         --       THEN LEFT(isnull(isnull(Manufacturer.Name,'XXX') + isnull(Model.Name,'XXX') + dbo.GetIFSSKUCarrierSegment(ReceiveDetailID) + isnull(Colour.Name,'XXX'), ''), 25) 
         --       ELSE isnull(isnull(Manufacturer.Name,'XXX') + isnull(Model.Name,'XXX') + dbo.GetIFSSKUCarrierSegment(ReceiveDetailID) + isnull(Colour.Name,'XXX'), '') + dbo.GetIFSSKUKittingSegment(ReceiveDetailID) 
         --  END AS SKU_CALC1
FROM dbo.ReceiveDetail
--LEFT OUTER JOIN dbo.[Option] AS Colour ON dbo.ReceiveDetail.ColourID = Colour.OptionID
--LEFT OUTER JOIN dbo.[Option] AS Model ON dbo.ReceiveDetail.ModelID = Model.OptionID 
--LEFT OUTER JOIN dbo.[Option] as Manufacturer ON dbo.ReceiveDetail.ManufacturerID = Manufacturer.OptionID


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






















































































/****** Object:  StoredProcedure [dbo].[Get_ScanComandLookupChain]    Script Date: 04/21/2020 16:16:59 ******/
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
Declare @mMessage nvarchar(4000)
--exec [Get_SKULookupChain] 'adaa-rrboukbbb-chyccy', @mMessage output
exec [Get_SKULookupChain] 'aaa-boukbbb-ccc', @mMessage output
Print @mMessage
*/

Create PROCEDURE [dbo].[Get_SKULookupChain]

      @mScanCode nVarchar(250),
      @mMessage nVarchar(4000) output

AS
BEGIN
	SET NOCOUNT ON;

if CHARINDEX('-', @mScanCode)  < 2
   begin
   return 0
   end
   
declare @CarrierID numeric(18,0)
declare @ManufacturerID numeric(18,0)
declare @ModelID numeric(18,0)
declare @ColourID numeric(18,0)

declare @Manufacturer nvarchar(20)
declare @Model nvarchar(20)
declare @Colour nvarchar(20)

declare @mLength int
declare @mFirstDash int
declare @mSecondDash int

Select @mLength = LEN(@mScanCode) 
Select @mFirstDash = CHARINDEX('-', @mScanCode) 
Select @mSecondDash = CHARINDEX('-', @mScanCode, @mFirstDash + 1) 
Select @Manufacturer = SUBSTRING(@mScanCode, 1, @mFirstDash - 1)
Select @Model = SUBSTRING(@mScanCode, @mFirstDash + 1, @mSecondDash - @mFirstDash - 1)
Select @Colour = SUBSTRING(@mScanCode, @mSecondDash + 1, @mLength - @mSecondDash)


--Select top 1 @CarrierID = OptionID from [Option] O inner join Question Q on O.QuestionID = Q.QuestionID
--Inner join QuestionStatus QS on QS.QuestionStatusID = Q.QuestionStatusID
--Inner join OptionStatus OS on OS.OptionStatusID = O.OptionStatusID
--Where Q.Name = 'Carrier' and QS.Status = 'Active' and OS.Status = 'Active'
--Order by O.Sequence





Select @ManufacturerID = OptionID from [Option] O inner join OptionStatus S on O.OptionStatusID = S.OptionStatusID where Name = @Manufacturer and Status = 'Active'
Select @ModelID = OptionID from [Option] O inner join OptionStatus S on O.OptionStatusID = S.OptionStatusID where Name = @Model and Status = 'Active'
Select @ColourID = OptionID from [Option] O inner join OptionStatus S on O.OptionStatusID = S.OptionStatusID where Name = @Colour and Status = 'Active'

Select top 1 @CarrierID = OptionCarrierID from MasterCarrierManufacturerLookup Lup
Inner join MasterCarrierManufacturerStatus lS on LS.MasterCarrierManufacturerStatusID = lup.StatusID
where ls.Status = 'Active' and OptionColourID = @ColourID and OptionManufacturerID = @ManufacturerID and OptionModelID = @ModelID

--Select * from MasterCarrierManufacturerLookup Lup
--Inner join MasterCarrierManufacturerStatus lS on LS.MasterCarrierManufacturerStatusID = lup.StatusID
--where ls.Status = 'Active'



--Print @CarrierID
--Print @ManufacturerID
--Print @ModelID
--Print @ColourID

--Select * from MasterCarrierManufacturerStatus
--Select * from MasterCarrierManufacturerLookup

if ISNULL(@CarrierID, -1) < 1 or ISNULL(@ManufacturerID, -1) < 1 or ISNULL(@ModelID, -1) < 1  or ISNULL(@ColourID, -1) < 1 
   Begin
   Return 0
   End

Declare @Text nvarchar(1000)
Exec Get_ScanComandLookupLink @CarrierID, '', @Text output
Select @mMessage = @Text
Exec Get_ScanComandLookupLink @ManufacturerID, '', @Text output
Select @mMessage = @mMessage + @Text
Exec Get_ScanComandLookupLink @ModelID, '', @Text output
Select @mMessage = @mMessage + @Text
Exec Get_ScanComandLookupLink @ColourID, '', @Text output
Select @mMessage = @mMessage + @Text

------------------------------------------------     
 
return 0

END





GO


/****** Object:  StoredProcedure [dbo].[Get_ScanComandLookupLink]    Script Date: 04/21/2020 16:18:13 ******/
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
Declare @mMessage nvarchar(4000)
exec [Set_OptionSKUSegment] 1444, '6666555', @mMessage output
Print @mMessage
exec [Set_OptionSKUSegment] 2226, 'JimTextUPC2', @mMessage output
Print @mMessage

Select * from [Option] where OptionID = 1444
Select * from [Option] where OptionID = 2226

*/

Create PROCEDURE [dbo].[Set_OptionSKUSegment]

      @OptionID numeric(18),
      @SKUSegment nvarchar(20),
      @mMessage nVarchar(4000) output

AS
BEGIN
	SET NOCOUNT ON;
	
	
	
Select @mMessage = ''
if NOT Exists (Select * from [Option] O inner join OptionStatus S on O.OptionStatusID = S.OptionStatusID where S.Status = 'Active' and O.OptionID = @OptionID)
   begin
   Select @mMessage = 'Error: Option Not found:' + CONVERT(nvarchar(20), @OptionID)
   return 0
   end
if Exists (Select * from [Option] O inner join OptionStatus S on O.OptionStatusID = S.OptionStatusID where S.Status = 'Active' and O.OptionID = @OptionID and O.Name = @SKUSegment)
   begin
   Select @mMessage = 'Warning:Segment already established'
   return 0
   end
if Exists (Select * from [Option] O inner join OptionStatus S on O.OptionStatusID = S.OptionStatusID where S.Status = 'Active' and O.OptionID != @OptionID and O.Name = @SKUSegment)
   begin
   Select @mMessage = 'Error: Segment used on another option'
   return 0
   end
	
Update [Option]	set Name = @SKUSegment where OptionID = @OptionID 
   Select @mMessage = 'Success: Segment Updated'
	
return 0

END


GO






/****** Object:  StoredProcedure [dbo].[Get_ScanComandLookupChain]    Script Date: 04/21/2020 16:16:59 ******/
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
Declare @mMessage nvarchar(4000)
exec [Get_ScanComandLookupChain] 'JimTextUPC1', @mMessage output
Print @mMessage
exec [Get_ScanComandLookupChain] 'aaa-boukbbb-ccc', @mMessage output
Print @mMessage
*/

ALTER PROCEDURE [dbo].[Get_ScanComandLookupChain]

      @mScanCode nVarchar(250),
      @mMessage nVarchar(4000) output

AS
BEGIN
	SET NOCOUNT ON;


exec [Get_SKULookupChain] @mScanCode, @mMessage output
if LEN(@mMessage) > 0
   begin
   return
   end

Declare @ScanCodeID numeric(15, 0)
Select @ScanCodeID = -1
select @ScanCodeID = ScanComandLookupID from ScanComandLookup where ScanCode = @mScanCode and Status = 'Active'
Select @ScanCodeID = ISNULL(@ScanCodeID, -1)
Select @mMessage = ''
-- see if the UPC is already there.
if @ScanCodeID < 1
   begin
   Select @mMessage = ''        -- Leave it empty so calling procedure will see no results and move on to the next "assumption".
   return 0
   end
 
Select @mMessage = @mMessage + CommandString 
  from vwScanComandLookupChain A
 where A.ScanComandLookupID = @ScanCodeID
 Order by ChainSequence, OptionSequence, QuestionSequence  
--Select @mMessage = @mMessage + CommandString 
--  from ScanComandLookupAttributeList A
--  Inner Join [Option] B on A.OptionID = B.OptionID
--  Inner Join [Question] C on B.QuestionID = C.QuestionID
-- where A.ScanComandLookupID = @ScanCodeID order by A.Sequence, C.Sequence
   
  
------------------------------------------------     
 
return 0

END





GO
















































































