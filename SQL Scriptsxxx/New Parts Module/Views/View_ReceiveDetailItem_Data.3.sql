/****** Object:  View [dbo].[View_ReceiveDetailItem_Data]    Script Date: 11/24/2016 15:55:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[View_ReceiveDetailItem_Data]
AS
SELECT ReceiveDetailItem.ReceiveDetailItemID, Question.QuestionID, [Option].OptionID
     , case when QuestionType.Type IN ('Keyboard', 'Calc', 'Calendar', 'Currency', 'Numeric','Text20Digit','Num3Digit','Text3Digit','Text10Digit','Text18Digit','Text50Digit') AND (dbo.ReceiveDetailItem.Version = 0) then ReceiveDetailItem.Value
            when QuestionType.Type IN ('Checkbox', 'DropDown', 'RadialButton') AND (ReceiveDetailItem.Value = '1') AND (ReceiveDetailItem.Version = 0) then [Option].OptionText
            else 'xxxx' end as Value
     , ReceiveDetailItem.Value ValueRI
     , [Option].OptionText ValueOP
     , QuestionType.Type QuestionType
     , Question.Name Question
     , [Option].Name AS Abbreviation
FROM         QuestionType INNER JOIN
                      Question ON QuestionType.QuestionTypeID = Question.QuestionTypeID INNER JOIN
                      [Option] ON Question.QuestionID = [Option].QuestionID INNER JOIN
                      ReceiveDetailItem ON [Option].OptionID = ReceiveDetailItem.OptionID

GO


