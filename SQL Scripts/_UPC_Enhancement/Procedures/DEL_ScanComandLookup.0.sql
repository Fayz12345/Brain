

/****** Object:  StoredProcedure [dbo].[DEL_MasterCarrierManufacturerUPCLookup]    Script Date: 04/18/2020 13:17:00 ******/
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
exec DEL_ScanComandLookup 'JimTextUPC1' , 'jmccomb', @mMessage output
Print @mMessage

Select * from ScanComandLookup


Drop Procedure DEL_MasterCarrierManufacturerUPCLookup
GO



*/

CREATE PROCEDURE [dbo].[DEL_ScanComandLookup]

      @mScanCode nVarchar(250),
      @mUserName nvarchar(50) = '',
      @mMessage nVarchar(4000) output

AS
BEGIN
	SET NOCOUNT ON;


Select @mMessage = ''
-- see if the UPC is already there.
if NOT exists (select * from ScanComandLookup where [ScanCode] = @mScanCode and Status = 'Active')
   begin
   Select @mMessage = 'Warning: Scancode NOT on file'
   return 0
   end

-- Verify there is a proper combo
Update ScanComandLookup Set 
       LastUpdateDate = GETDATE(), 
       LastUpdateUser = @mUserName, 
       Status = 'Deleted' 
       where [ScanCode] = @mScanCode and Status = 'Active'

Select @mMessage = 'Success: Scancode Code Deleted: '
return 0

END


GO


