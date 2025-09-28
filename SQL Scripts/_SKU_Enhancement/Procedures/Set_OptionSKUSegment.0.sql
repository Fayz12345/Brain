/****** Object:  StoredProcedure [dbo].[Get_ScanComandLookupLink]    Script Date: 04/21/2020 16:18:13 ******/
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
exec [Set_OptionSKUSegment] 1444, '6666555', @mMessage output
Print @mMessage
exec [Set_OptionSKUSegment] 2226, 'JimTextUPC2', @mMessage output
Print @mMessage

Select * from [Option] where OptionID = 1444
Select * from [Option] where OptionID = 2226

*/

Create PROCEDURE [dbo].[Set_OptionSKUSegment]

      @OptionID numeric(18),
      @SKUSegment nvarchar(20),
      @mMessage nVarchar(4000) output

AS
BEGIN
	SET NOCOUNT ON;
	
	
	
Select @mMessage = ''
if NOT Exists (Select * from [Option] O inner join OptionStatus S on O.OptionStatusID = S.OptionStatusID where S.Status = 'Active' and O.OptionID = @OptionID)
   begin
   Select @mMessage = 'Error: Option Not found:' + CONVERT(nvarchar(20), @OptionID)
   return 0
   end
if Exists (Select * from [Option] O inner join OptionStatus S on O.OptionStatusID = S.OptionStatusID where S.Status = 'Active' and O.OptionID = @OptionID and O.Name = @SKUSegment)
   begin
   Select @mMessage = 'Warning:Segment already established'
   return 0
   end
if Exists (Select * from [Option] O inner join OptionStatus S on O.OptionStatusID = S.OptionStatusID where S.Status = 'Active' and O.OptionID != @OptionID and O.Name = @SKUSegment)
   begin
   Select @mMessage = 'Error: Segment used on another option'
   return 0
   end
	
Update [Option]	set Name = @SKUSegment where OptionID = @OptionID 
   Select @mMessage = 'Success: Segment Updated'
	
return 0

END


GO


