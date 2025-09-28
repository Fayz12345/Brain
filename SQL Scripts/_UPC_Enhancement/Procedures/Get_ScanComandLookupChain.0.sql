
/****** Object:  StoredProcedure [dbo].[Get_MasterCarrierManufacturerUPCLookupChain]    Script Date: 04/18/2020 15:29:36 ******/
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
exec [Get_ScanComandLookupChain] 'JimTextUPC1', @mMessage output
Print @mMessage
*/

Create PROCEDURE [dbo].[Get_ScanComandLookupChain]

      @mScanCode nVarchar(250),
      @mMessage nVarchar(4000) output

AS
BEGIN
	SET NOCOUNT ON;

Declare @ScanCodeID numeric(15, 0)
Select @ScanCodeID = -1
select @ScanCodeID = ScanComandLookupID from ScanComandLookup where ScanCode = @mScanCode and Status = 'Active'
Select @ScanCodeID = ISNULL(@ScanCodeID, -1)
Select @mMessage = ''
-- see if the UPC is already there.
if @ScanCodeID < 1
   begin
   Select @mMessage = ''        -- Leave it empty so calling procedure will see no results and move on to the next "assumption".
   return 0
   end
   
 
Select @mMessage = @mMessage + CommandString 
  from vwScanComandLookupChain A
 where A.ScanComandLookupID = @ScanCodeID
 Order by ChainSequence, OptionSequence, QuestionSequence     
   
--Select @mMessage = @mMessage + CommandString 
--  from ScanComandLookupAttributeList A
--  Inner Join [Option] B on A.OptionID = B.OptionID
--  Inner Join [Question] C on B.QuestionID = C.QuestionID
-- where A.ScanComandLookupID = @ScanCodeID order by A.Sequence, C.Sequence
   
  
------------------------------------------------     
 
return 0

END




GO


