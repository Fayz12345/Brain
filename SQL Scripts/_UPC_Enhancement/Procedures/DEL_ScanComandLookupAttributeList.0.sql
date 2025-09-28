

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

CREATE PROCEDURE [dbo].[DEL_ScanComandLookupAttributeList]

      @ScanComandLookupAttributeListID [numeric](18, 0) ,
      @mUserName nvarchar(50) = '',
      @mMessage nVarchar(4000) output

AS
BEGIN
	SET NOCOUNT ON;


Select @mMessage = ''
-- see if the UPC is already there.
if NOT exists (select * from ScanComandLookupAttributeList where ScanComandLookupAttributeListID = @ScanComandLookupAttributeListID)
   begin
   Select @mMessage = 'Warning: Scancode Attribute NOT on file'
   return 0
   end

-- Verify there is a proper combo
Update ScanComandLookupAttributeList Set 
       LastUpdateDate = GETDATE(), 
       LastUpdateUser = @mUserName, 
       Status = 'Deleted' 
       where ScanComandLookupAttributeListID = @ScanComandLookupAttributeListID

Select @mMessage = 'Success: Scancode Attribute Deleted: '
return 0

END


GO


