

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


