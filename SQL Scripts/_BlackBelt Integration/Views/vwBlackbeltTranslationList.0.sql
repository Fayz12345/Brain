
/****** Object:  View [dbo].[vwBlackbeltTranslationList]    Script Date: 06/15/2018 18:50:35 ******/
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




CREATE VIEW [dbo].[vwBlackbeltTranslationList]
AS
SELECT     BlackbeltTranslationList.BlackbeltTranslationListID AS QTID, BlackbeltTranslationList_1.BlackbeltTranslationListID AS QVID, BlackbeltTranslationList.Catagory, 
                      BlackbeltTranslationList.SearchValue AS Question_Raw, BlackbeltTranslationList.Translation AS Question_Trans, BlackbeltTranslationList_1.SearchValue AS Value_Raw, 
                      BlackbeltTranslationList_1.Translation AS Value_Trans, Question.QuestionID, [Option].OptionID
FROM         BlackbeltTranslationList 
INNER JOIN   BlackbeltTranslationList AS BlackbeltTranslationList_1 ON BlackbeltTranslationList.SearchValue = BlackbeltTranslationList_1.Catagory 
LEFT OUTER JOIN Question ON BlackbeltTranslationList.Translation = Question.Name
LEFT OUTER JOIN [Option] ON BlackbeltTranslationList_1.Translation = [Option].OptionText  and Question.QuestionID = [Option].QuestionID
WHERE     (BlackbeltTranslationList.Catagory = N'Question') 


GO




