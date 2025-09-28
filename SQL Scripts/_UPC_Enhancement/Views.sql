/****** Object:  View [dbo].[vwIFS_InvtTran]    Script Date: 04/18/2020 16:45:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*

Select * from vwScanComandLookupChain




Select * from InvtTran_IFS  Directive*/
CREATE VIEW [dbo].[vwScanComandLookupChain]
AS

SELECT ScanComandLookup.ScanCode
     , ScanComandLookupAttributeList.SetValue
     , ScanComandLookupAttributeList.CommandString
     , [Option].ScanKey
     , [Option].OptionText
     , Question.Name
     , ScanComandLookupAttributeList.Sequence ChainSequence
     , [Option].Sequence OptionSequence
     , Question.Sequence QuestionSequence
     , ScanComandLookup.ScanComandLookupID
     , ScanComandLookupAttributeList.ScanComandLookupAttributeListID
     , [Option].OptionID
     , Question.QuestionID
FROM         [Option] 
inner join [OptionStatus] OS on [Option].OptionStatusID = OS.OptionStatusID
INNER JOIN  Question ON [Option].QuestionID = Question.QuestionID 
Inner join QuestionStatus QS on QS.QuestionStatusID = Question.QuestionStatusID
INNER JOIN  ScanComandLookupAttributeList ON [Option].OptionID = ScanComandLookupAttributeList.OptionID 
INNER JOIN  ScanComandLookup ON ScanComandLookupAttributeList.ScanComandLookupID = ScanComandLookup.ScanComandLookupID
Where OS.Status = 'Active' and QS.Status = 'Active' and ScanComandLookupAttributeList.Status = 'Active' and ScanComandLookup.Status = 'Active'


GO






















