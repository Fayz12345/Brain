
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


