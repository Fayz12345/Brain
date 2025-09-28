/****** Object:  StoredProcedure [dbo].[GetData_Pivot_RawData]    Script Date: 02/05/2018 20:29:20 ******/
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

Select * from REceiveDetail where Version = '000'


-- Start
--Declare @ProjectID numeric(18)
--Select @ProjectID = 7
-----------------------------------------------------------------------------
-- Get the Keys for the Data we want to report on.
Declare @IDList AS ListOfIDs 
Insert @IDList
Select top 500 ReceivedetailID from ReceiveDetail where Version = '000' order by Createdate -- ReceiveDetailID in (47971,47972,47973,47974,47975,47976,47977,47978,47979,47980,47981,47982,47983)
------------------------------------------------------------------------------
-- Create the Temp table required to house the pivot table data
Declare @RValue nvarchar(max)
exec Get_Pivot_RawData_AlterStatement @IDList, '#TempTable', @RValue output
Print @RValue
Create Table #TempTable (KeyID numeric(18))
EXEC sp_executesql @RValue
-- Select * from #TempTable
Print 'Just Printed TempTable'
------------------------------------------------------------------
-- Get the data we are interested in.
Insert #TempTable
--Select * from #TempTable
exec GetData_Pivot_RawData @IDList
------------------------------------------------------------------
-- Proof of concept -- report what we got.
Select ReceiveDetail.ESN
, ReceiveDetail.Version
--, ReceiveDetail.IFSLocation
, ReceiveDetail.SKU as Sku
--, ReceiveDetail.IFSCondition
, #TempTable.Carrier 
, #TempTable.Manufacturer
, #TempTable.Model
, #TempTable.Colour
, #TempTable.Conditions
, #TempTable.ShipTo
, #TempTable.PSlip
, #TempTable.[Out-Bound_WayBill-S]
from #TempTable
Inner join REceiveDetail on ReceiveDetail.ReceiveDetailID = #TempTable.KeyID
------------------------------------------------------------------
-- Clean up. 
Drop Table #TempTable 

Select * from #TempTable 
-- All Done

-- Select COunt(*), ProjectID from ReceiveDetail group by PRojectID
 

*/

ALTER PROCEDURE [dbo].[GetData_Pivot_RawData]
      @IDList ListOfIDs Readonly

AS
BEGIN
	SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	

--Declare @projectID numeric(18)
--Select @projectID = 5
-- In this case, the projects are used to deduce the Attributes that need to be reported.
Select distinct ProjectID
into #TempProjects 
From ReceiveDetail
Inner join @IDList b on b.ID = Receivedetail.ReceiveDetailID

-- Get all the Attributes within a project
--SELECT Distinct  Project.ProjectID, Project.Name as ProjectName, Question.QuestionID, Question.Name AS QuestionName, Question.Sequence, convert(numeric(15),0) as Included
--into #AttributeData
--  FROM Project 
-- Inner join #TempProjects on #TempProjects.ProjectID = Project.ProjectID    
-- INNER JOIN ProjectProcess ON Project.ProjectID = ProjectProcess.ProjectID 
-- INNER JOIN Process ON ProjectProcess.ProcessID = Process.ProcessID 
-- INNER JOIN ProcessQuestion ON Process.ProcessID = ProcessQuestion.ProcessID 
-- INNER JOIN Question ON ProcessQuestion.QuestionID = Question.QuestionID
-- Order by Sequence
------------------------------------------------------------------------------------------
-- Get all the Attributes
SELECT Distinct Convert(numeric(18),Question.QuestionID) as QuestionID, convert(varchar(100),Question.Name) AS QuestionName, convert(int,0) as Sequence, convert(numeric(15),0) as Included, Question.QuestionStatusID
into #AttributeData
FROM Question 
 Inner join QuestionStatus s on s.QuestionStatusID = Question.QuestionStatusID
 Where s.Status = 'Active'
 Order by Sequence, QuestionName
 
---- Get the Process Names.
--Insert #AttributeData  
--SELECT DISTINCT -1 as QuestionID, convert(varchar(100),Replace(isnull(Process.Name,'NoName'),' ', '_') + '_Created') as Name,1000000, CONVERT(numeric(15), 0) AS Included
--FROM  Process -- Order by Process.Name             
------------------------------------------------------------------------------------------
-- Get all the data for the projects



--SELECT  convert(numeric(18),ReceiveDetail.ReceiveDetailID) as ReceiveDetailID, ReceiveDetailItem.Value, Question.Name as QuestionName
SELECT  convert(numeric(18),ReceiveDetail.ReceiveDetailID) as ReceiveDetailID
 , case when QuestionType.Type = 'RadialButton' and Value = '1' then [Option].OptionText
        when QuestionType.Type = 'RadialButton' and Value = '0' then [Option].OptionText
        when QuestionType.Type = 'DropDown' and Value = '1' then [Option].OptionText
        when QuestionType.Type = 'DropDown' and Value = '0' then [Option].OptionText
        when QuestionType.Type = 'Checkbox' and Value = '1' then [Option].OptionText
        when QuestionType.Type = 'Checkbox' and Value = '0' then dbo.GetReceivedQuestionAnswerString_OptionID(ReceiveDetail.ReceiveDetailID,[Option].OptionID) -- [Option].OptionText
        else ReceiveDetailItem.Value end as Value
 , Question.Name as QuestionName
into #Data
FROM         ReceiveDetail 
Inner join @IDList d on d.ID = REceiveDetail.ReceiveDetailID
INNER JOIN ReceiveDetailItem ON ReceiveDetail.ReceiveDetailID = ReceiveDetailItem.ReceiveDetailID 
INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID AND ReceiveDetailItem.OptionID = [Option].OptionID 
INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
INNER JOIN QuestionType ON Question.QuestionTypeID = QuestionType.QuestionTypeID


--Select * from #Data


-----------------------------------------
---- Get the Process data.
--Select ReceiveDetailProcessLog.ReceiveDetailID, Process.Name, Min(ReceiveDetailProcessLog.CreateDate) as CreateDate
-- into #xTemp001
-- From @IDList d
-- INNER JOIN ReceiveDetailProcessLog ON d.ID = ReceiveDetailProcessLog.ReceiveDetailID 
-- INNER JOIN Process ON ReceiveDetailProcessLog.ProcessID = Process.ProcessID
--Group by ReceiveDetailProcessLog.ReceiveDetailID,Process.Name

--insert #Data (ReceiveDetailID
--      ,Value
--      ,QuestionName
--      )
--SELECT ReceiveDetailID
--      ,convert(nvarchar(50),CreateDate,101)
--      ,Replace(isnull(Name,'NoName'), ' ', '_') + '_Created'        
-- From #xTemp001

------------------------------------------------
-- Now we need to make sure all questions are represented inside Data.

--Update #AttributeData set Included = 1 from #AttributeData Inner join #Data on #AttributeData.QuestionID = #Data.QuestionID
--Insert #Data (Value, QuestionName, QuestionID)
--Select '', QuestionName, QuestionID from #AttributeData where Included = 0

Update #AttributeData set Included = 1 from #AttributeData Inner join #Data on #AttributeData.QuestionName = #Data.QuestionName
Insert #Data (Value, QuestionName)
Select '', QuestionName from #AttributeData where Included = 0

-- WE need to make sure there are no spaces within the Question Name.
Update #Data set QuestionName = Replace(QuestionName, ' ', '_')
Update #AttributeData set QuestionName = Replace(QuestionName, ' ', '_')


-- Select * from #AttributeData



-- We need to create a string of Question Names for the Pivot to work
DECLARE @columns nvarchar(max)
SELECT @columns = COALESCE(@columns + ',[' + cast(QuestionName as varchar) + ']', '[' + cast(QuestionName as varchar)+ ']')
FROM #AttributeData
GROUP BY QuestionName, Sequence
Print @columns

-- select * from #Data
-- return


-- Build the Pivot Query and execute it.
DECLARE @query NVARCHAR(MAX)
SET @query = '
SELECT *
into #Temp321
FROM #Data
PIVOT
(
MAX(Value)
FOR [QuestionName]
IN (' + @columns + ')
)
AS p


Select * from #Temp321 where not ReceiveDetailID is null order by ReceiveDetailID

'

EXECUTE(@query)



--Drop table ##Temp321
Drop Table #AttributeData
Drop table #Data

END
go
