


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
