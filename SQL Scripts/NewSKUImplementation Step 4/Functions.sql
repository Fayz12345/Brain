

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



















