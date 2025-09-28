/****** Object:  StoredProcedure [dbo].[Get_Pivot_RawData_AlterStatement]    Script Date: 02/05/2018 21:09:10 ******/
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
Declare @RValue nvarchar(max)
Declare @IDList AS ListOfIDs 
Insert @IDList       -- values (5)
Select top 20 ReceivedetailID from ReceiveDetail order by createdate desc  
-- Select * from @IDList

exec Get_Pivot_RawData_AlterStatement @IDList, '#TempTable', @RValue output
Print @RValue

Create Table #TempTable (KeyID numeric(18))

EXEC sp_executesql @RValue

Select * from #TempTable
 
Drop Table #TempTable 


 
 

*/

ALTER PROCEDURE [dbo].[Get_Pivot_RawData_AlterStatement]
      @IDList ListOfIDs Readonly,
      @ALterTableName nvarchar(50),
      @AlterStatement nvarchar(max) output

AS
BEGIN
	SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	

-- There may be a need to send in a list if KEY_ID fields
-- Use that to filter out the records that get pivoted.
--print 'aaaaaaaa'

--Select distinct ProjectID
--into #TempProjects 
--From ReceiveDetail
--Inner join @IDList b on b.ID = Receivedetail.ReceiveDetailID

-- Get all the Attributes within a project
-- SELECT Distinct  Project.ProjectID, Project.Name as ProjectName, Question.QuestionID, Question.Name AS QuestionName, Question.Sequence, convert(numeric(15),0) as Included
--SELECT Distinct  Convert(numeric(18),Question.QuestionID) as QuestionID, convert(varchar(100),Question.Name) AS QuestionName, Question.Sequence, convert(numeric(15),0) as Included
--into #AttributeData
--  FROM 
-- Question 
--  Order by Sequence     
  
SELECT Distinct Convert(numeric(18),Question.QuestionID) as QuestionID, convert(varchar(100),Question.Name) AS QuestionName, convert(int,0) as Sequence, convert(numeric(15),0) as Included, Question.QuestionStatusID
into #AttributeData
  FROM 
 Question 
 Inner join QuestionStatus s on s.QuestionStatusID = Question.QuestionStatusID
 Where s.Status = 'Active'
 Order by Sequence, QuestionName  
  
  
--Insert #AttributeData  
--SELECT DISTINCT -1 as QuestionID, convert(varchar(100),Replace(isnull(Process.Name,'NoName'),' ', '_') + '_Created') as Name,1000000, CONVERT(numeric(15), 0) AS Included
---- into #TempProcessName
--FROM  Process -- Order by Sequence 
                

Update #AttributeData set QuestionName = Replace(QuestionName, ' ', '_')
Select @AlterStatement = 'Alter table ' + @ALterTableName + ' Add '

Declare @FieldName nvarchar(100)

while exists (Select * from #AttributeData where Included = 0)
      begin
      Select top 1 @FieldName = QuestionName from #AttributeData where Included = 0 Order by QuestionName, Sequence
      Update #AttributeData set Included = 1 where QuestionName = @FieldName
      Select @AlterStatement = @AlterStatement + '[' + @FieldName + '] nvarchar(200)'
      if exists(Select * from #AttributeData where Included = 0)
         Select @AlterStatement = @AlterStatement + ','
      end
      
Drop Table #AttributeData

END
go
