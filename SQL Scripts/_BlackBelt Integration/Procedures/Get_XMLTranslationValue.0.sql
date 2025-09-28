/****** Object:  StoredProcedure [dbo].[Get_XMLTranslationValue]    Script Date: 04/09/2018 14:48:37 ******/
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

exec IFS_GetInvtTranBatch 10

Select * from BlackbeltTranslationList

Delete BlackbeltTranslationList where BlackbeltTranslationListID = 11      
Delete BlackbeltTranslationList where BlackbeltTranslationListID = 14
14
*/   

ALTER PROCEDURE [dbo].[Get_XMLTranslationValue]
      @QuestionName nvarchar(75)
     ,@LookupValue nvarchar(200) 
     ,@ReturnValue nVarchar(200) Output


AS
BEGIN

	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	
	Select @LookupValue = ISNULL(@LookupValue, '')
	
	Select @ReturnValue = @LookupValue
	if not exists(Select * from BlackbeltTranslationList where Catagory = @QuestionName and SearchValue = @LookupValue)
	   begin
	   insert BlackbeltTranslationList ([Status],[Catagory],[SearchValue],[Translation],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
	   values ('Active', @QuestionName, @LookupValue, @LookupValue, getdate(), 'System', GETDATE(), 'System')
	   end
	
   Select @ReturnValue = [Translation]
  FROM BlackbeltTranslationList where Catagory = @QuestionName and SearchValue = @LookupValue
	
   
END
/****** Object:  StoredProcedure [dbo].[IFS_PickUpXMLFiles]    Script Date: 04/03/2018 11:39:26 ******/
SET ANSI_NULLS ON
