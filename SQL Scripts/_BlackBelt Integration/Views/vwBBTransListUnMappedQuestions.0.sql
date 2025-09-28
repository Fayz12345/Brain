

/****** Object:  View [dbo].[vwBBTransListUnMappedQuestions]    Script Date: 06/15/2018 18:50:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*

Select * from vwBlackbeltTranslationList
Select * from vwBBTransListUnMappedQuestions order by QuestionID, Question_Raw
Select * from vwBBTransListUnMappedOptions order by QuestionID, Question_Raw
Select * from vwBBTransListMapped order by QuestionID, Question_Raw


Select Distinct QTID, QuestionID, Question_Raw, Question_Trans from vwBlackbeltTranslationList where QuestionID is null
Select QTID, QVID, QuestionID, OptionID, Question_Raw, Question_Trans, Value_Raw, Value_Trans from vwBlackbeltTranslationList where (QUestionID is not null and optionID is null)
Select QTID, QVID, QuestionID, OptionID, Question_Raw, Question_Trans, Value_Raw, Value_Trans from vwBlackbeltTranslationList where (QUestionID is not null and optionID is not null)

Update BlackbeltTranslationList set Translation = 'Red' where BlackbeltTranslationListID = 25

Select * from BlackbeltTranslationList where BlackbeltTranslationListID = 25
*/




Create VIEW [dbo].[vwBBTransListUnMappedQuestions]
AS

Select Distinct QTID, QuestionID, Question_Raw, Question_Trans from vwBlackbeltTranslationList where QuestionID is null


GO



