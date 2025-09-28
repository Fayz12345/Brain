
/****** Object:  StoredProcedure [dbo].[ADD_MasterCarrierManufacturerUPCLookup]    Script Date: 04/18/2020 13:09:39 ******/
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
exec ADD_ScanComandLookup 'JimTextUPC1', 'jmccomb', @mMessage output
Print @mMessage


Select * from ScanComandLookup

*/

--Drop Procedure ADD_MasterCarrierManufacturerUPCLookup
--GO


Create PROCEDURE [dbo].[ADD_ScanComandLookup]

      @mScanCode nVarchar(250),
      @mUserName nvarchar(50) = '',
      @mMessage nVarchar(4000) output

AS
BEGIN
	SET NOCOUNT ON;


Select @mMessage = ''
-- see if the UPC is already there.
if exists (select * from ScanComandLookup where [ScanCode] = @mScanCode and Status = 'Active')
   begin
   Select @mMessage = 'Warning: Scancode already on file'
   return 0
   end

-- Verify there is a proper combo

--Declare @mLookupID numeric(18,0)
--Select @mLookupID = MasterCarrierManufacturerLookupID from MasterCarrierManufacturerLookup A
--                  Inner join MasterCarrierManufacturerStatus B on A.StatusID = B.MasterCarrierManufacturerStatusID
--                       where OptionCarrierID = @mCarrierID 
--                         and OptionColourID = @mcolourID 
--                         and OptionManufacturerID = @mManufacturerID 
--                         and OptionModelID = @mModelID
--                         and Status = 'Active'
--Select @mLookupID = ISNULL(@mLookupID, -1)

--if (@mLookupID < 1)
--   begin
--   Select @mMessage = 'Error: Invalid Attribute Combo'
--   return 0
--   end
   
-- Print 'Lookup:' + @mScanCode
   
INSERT INTO ScanComandLookup
           (ScanCode, Status ,[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
    VALUES (@mScanCode,'Active',GETDATE(),@mUserName,GETDATE(),@mUserName)

Select @mMessage = 'Success: Scancode Code Added: ' + CONVERT(nvarchar(10), @@IDENTITY)
return 0

END


GO


