
--/****** Object:  StoredProcedure [dbo].[ADD_MasterCarrierManufacturerUPCLookup]    Script Date: 04/15/2020 18:08:49 ******/
--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO


---- =============================================
---- Author:		<Author,,Name>
---- Create date: <Create Date,,>
---- Description:	<Description,,>
---- =============================================

--/*
--exec InsertReceiveDetailItemAttributeList 3468,139,'TX_236;ddddddddd,CB_182;1,TX_240;dddddddddd,TX_230;dddddddddd,RD_229;1,CB_179;1,TX_226;dddddddd,DD_308;1,DD_210;1,DD_148;1,CB_176;1,TX_223;07/22/2011,RD_506;1,DD_503;1,DD_253;1', 'jmccomb'
--*/

--CREATE PROCEDURE [dbo].[Search_MasterCarrierManufacturerUPCLookup]

--      @mUPC nVarchar(250),
--      @mCarrierID numeric(18) output,
--      @mManufacturerID numeric(18) output,
--      @mModelID numeric(18) output,
--      @mcolourID numeric(18) output,
--      @mMessage nVarchar(4000) output

--AS
--BEGIN
--	SET NOCOUNT ON;


--Select @mMessage = ''
--Select @mCarrierID = -1
--Select @mManufacturerID = -1
--Select @mModelID = -1
--Select @mcolourID = -1


---- see if the UPC is already there.
--if not exists (select * from MasterCarrierManufacturerUPCLookup where UPC = @mUPC and Status = 'Active')
--   begin
--   Select @mMessage = 'Error: UPC NOT on file'
--   return 0
--   end

--Select @mCarrierID = OptionCarrierID, 
--       @mcolourID = OptionColourID, 
--       @mManufacturerID = OptionManufacturerID,
--       @mModelID = OptionModelID
--       From MasterCarrierManufacturerUPCLookup A
--       Inner Join MasterCarrierManufacturerLookup B on A.MasterCarrierManufacturerLookupID = B.MasterCarrierManufacturerLookupID
--       where A.UPC = @mUPC and Status = 'Active'
       
--Select @mCarrierID = ISNULL(@mCarrierID, -1)
--Select @mManufacturerID = ISNULL(@mManufacturerID, -1)
--Select @mModelID = ISNULL(@mModelID, -1)
--Select @mcolourID = ISNULL(@mcolourID, -1)

--Select @mMessage = 'Success: UPC Code Found: ' + CONVERT(nvarchar(10), @@IDENTITY)
--return 0

--END


--GO


