
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
