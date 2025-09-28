
/****** Object:  StoredProcedure [dbo].[Get_ScanComandLookupLink]    Script Date: 04/18/2020 17:55:04 ******/
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
exec [Get_ScanComandLookupLink] 1444, 'xx', @mMessage output
Print @mMessage
exec [Get_ScanComandLookupLink] 2226, 'JimTextUPC2', @mMessage output
Print @mMessage
*/

CREATE PROCEDURE [dbo].[Get_ScanComandLookupLink]

      @OptionID numeric(18),
      @SetTextValue nvarchar(25),
      @mMessage nVarchar(4000) output

AS
BEGIN
	SET NOCOUNT ON;
declare @mReturnText nvarchar(4000)
declare @macroKey nchar(2)
Declare @mReturnMessage nvarchar(200)
Declare @mTable nvarchar(20)
Declare @mID nvarchar(20)
--Declare @mValue nvarchar(50)
Declare @mControlType nvarchar(2)
Select @mMessage = ''


--Select @mValue = ''
Select @mTable = ''
Select @mID = ''
Select @mControlType = ''
Select @mReturnMessage = ''
Select @macroKey = ''
Select @mReturnText = ''

Select @mReturnMessage = Description + ':' + OptionText
     , @mTable = 'Option'
     , @macroKey = [Option].MacroKey
     , @mID = convert(nvarchar(20), [Option].OptionID)
     , @mControlType = Case when QuestionType.Type = 'Dropdown' then 'DD'
                            when QuestionType.Type = 'CheckBox' then 'CB'
                            when QuestionType.Type = 'RadialButton' then 'RD'   
                            when QuestionType.Type = 'Calendar' then 'CA'                   
                            else 'TX' END
      From [Option] 
     Inner Join Question on Question.QuestionID = [Option].QuestionID
     INNER JOIN QuestionType ON Question.QuestionTypeID = QuestionType.QuestionTypeID     
     where OptionID = @OptionID
     

Select @mReturnMessage = isnull(@mReturnMessage, '')
Select @mReturnText = @mTable + ':' + @mID + ':' + @mReturnMessage + ':' + @macroKey + ':' + @mControlType + ';'  
  
    
if @mControlType = 'TX'
   begin
   Select @mReturnText = @mTable + ':' + @mID + ':' + @mReturnMessage + ':' + @macroKey + ':' + @mControlType + ':' + @SetTextValue + ';'   
   end 
  
Select @mMessage = @mReturnText

return 0

END

