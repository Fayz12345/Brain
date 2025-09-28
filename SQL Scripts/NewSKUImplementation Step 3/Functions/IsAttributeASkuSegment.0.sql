

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