

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