/****** Object:  View [dbo].[vwBBTransListValidQuestion]    Script Date: 06/21/2018 16:15:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*

Select * from vwBlackbeltTranslationList
Select * from vwBBTransListInvalidQuestion
Select * from vwBBTransListValidQuestion
Select * from vwBBTransListUnMappedQuestions
Select * from vwBBTransListUnMappedOptions
Select * from vwBBTransListMapped


Select Distinct QTID, QuestionID, Question_Raw, Question_Trans from vwBlackbeltTranslationList where QuestionID is null
Select QTID, QVID, QuestionID, OptionID, Question_Raw, Question_Trans, Value_Raw, Value_Trans from vwBlackbeltTranslationList where (QUestionID is not null and optionID is null)
Select QTID, QVID, QuestionID, OptionID, Question_Raw, Question_Trans, Value_Raw, Value_Trans from vwBlackbeltTranslationList where (QUestionID is not null and optionID is not null)

Update BlackbeltTranslationList set Translation = 'Red' where BlackbeltTranslationListID = 25

Select * from BlackbeltTranslationList where BlackbeltTranslationListID = 25
*/




Create VIEW [dbo].[vwBBTransListValidQuestion]
AS

Select * from BlackbeltTranslationList where Catagory != 'Question' and exists(Select * from BlackbeltTranslationList B where Catagory = 'Question' and Translation = BlackbeltTranslationList.Catagory)



GO


