

/****** Object:  StoredProcedure [dbo].[Get_ScanComandLookupChain]    Script Date: 04/21/2020 16:16:59 ******/
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
exec [Get_ScanComandLookupChain] 'aaa-boukbbb-ccc', @mMessage output
Print @mMessage
*/

ALTER PROCEDURE [dbo].[Get_ScanComandLookupChain]

      @mScanCode nVarchar(250),
      @mMessage nVarchar(4000) output

AS
BEGIN
	SET NOCOUNT ON;


exec [Get_SKULookupChain] @mScanCode, @mMessage output
if LEN(@mMessage) > 0
   begin
   return
   end

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


