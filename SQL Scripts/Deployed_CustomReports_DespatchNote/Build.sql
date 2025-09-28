




















Alter Table SystemTimeLog Add SaveTimeBrowserMS numeric(18, 0) null
go


















































































/****** Object:  View [dbo].[ViewWorkScreenSaveLog]    Script Date: 02/13/2018 17:09:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*


Select * from ViewWorkScreenSaveLog


*/
Alter VIEW [dbo].[ViewWorkScreenSaveLog]
AS
SELECT     ReceiveDetail.ESN, ReceiveDetail.Version, Process.Name as ProcessName
      , SystemTimeLog.[SystemTimeLogID]
      , SystemTimeLog.[RecordType]
      , SystemTimeLog.[ReceiveDetailID]
      , SystemTimeLog.[ProcessID]
      , SystemTimeLog.[MasterPartsRequestedLogID]
      , SystemTimeLog.[StartTimeDate]
      , SystemTimeLog.[EndTimeDate]
      , SystemTimeLog.[SaveTimeMS]
      , SystemTimeLog.[SaveTimeBrowserMS]
      , SystemTimeLog.[RecordDetailString]
      , SystemTimeLog.[CreateIPAddress]
      , SystemTimeLog.[CreateDate]
      , SystemTimeLog.[CreateUser]
      , SystemTimeLog.[LastUpdateDate]
      , SystemTimeLog.[LastUpdateUser]
FROM         SystemTimeLog INNER JOIN
                      ReceiveDetail ON SystemTimeLog.ReceiveDetailID = ReceiveDetail.ReceiveDetailID INNER JOIN
                      Process ON SystemTimeLog.ProcessID = Process.ProcessID
where RecordType = 'WorkScreenSave' 


GO








































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
/****** Object:  StoredProcedure [dbo].[Report_UnitView]    Script Date: 02/05/2018 22:07:31 ******/
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

exec Report_C_DespatchNote 'AS098'


Exec UpdateESNAttribute '014476002653547','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476002653661','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476000627170','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476002605034','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476000195467','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476002075543','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476001768874','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476000703096','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476002566343','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476001583430','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '013768006366394','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476000198420','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476000200135','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476002065155','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476001579602','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '013768009185049','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476002693535','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '013682000178236','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476001096821','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014077003578582','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014077003927300','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014077002494427','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014077001750209','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014077000239634','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476001772637','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014505000315186','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014505000246589','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '014476002377402','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '357116071074440','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DN6H5E5EDFHY','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DN6H5EOLDFHY','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DN6H5E1XDFHY','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DN6H5DZ5DFHY','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DN6H5E67DFHY','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DMPFQ74GDFHY','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DN6H5E0EDFHY','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DN6H5E0BDFHY','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFW2CMDFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFW26BDFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFV4S7DFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFVER6DFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFW25XDFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DN6GT7CSDFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFW2FBDFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFB4BGDFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFW2BEDFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFW2OSDFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute '012923007524993','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFW25UDFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFW23QDFJO','PSlip','AS098','jjjj'
Exec UpdateESNAttribute 'DLXFV9J6DFJO','PSlip','AS098','jjjj'

*/

Create PROCEDURE [dbo].[Report_C_DespatchNote]
          
      @PSlip nvarchar(200)
AS
	SET NOCOUNT ON;

Begin	
	
-- Start
--Declare @ProjectID numeric(18)
--Select @ProjectID = 7
-----------------------------------------------------------------------------
-- Get the Keys for the Data we want to report on.
Declare @IDList AS ListOfIDs 
Insert @IDList
Select ReceiveDetailID from ReceiveDetailItem I
Inner join [Option] O on I.OptionID = o.OptionID
inner join Question q on o.QuestionID = q.QuestionID
where q.Name = 'PSlip' and I.Value = @PSlip
--Select top 500 ReceivedetailID from ReceiveDetail where Version = '000' order by Createdate -- ReceiveDetailID in (47971,47972,47973,47974,47975,47976,47977,47978,47979,47980,47981,47982,47983)




------------------------------------------------------------------------------
-- Create the Temp table required to house the pivot table data
Declare @RValue nvarchar(max)
exec Get_Pivot_RawData_AlterStatement @IDList, '#TempTable', @RValue output
Print @RValue
Create Table #TempTable (KeyID numeric(18))
EXEC sp_executesql @RValue
-- Select * from #TempTable
--Print 'Just Printed TempTable'
------------------------------------------------------------------
-- Get the data we are interested in.
Insert #TempTable
--Select * from #TempTable
exec GetData_Pivot_RawData @IDList
------------------------------------------------------------------
-- Proof of concept -- report what we got.
Select ReceiveDetail.ReceiveDetailID
, ReceiveDetail.ClientLocationID
, #TempTable.PSlip
, #TempTable.ShipTo
, ReceiveDetail.ESN
, ReceiveDetail.Version
, ReceiveDetail.SKU as Sku
, #TempTable.Carrier 
, #TempTable.Manufacturer
, #TempTable.Model
, #TempTable.Colour
, #TempTable.Conditions
, #TempTable.[Grade]
, #TempTable.[Out-Bound_WayBill-S]
, CONVERT(int, 1) as Freq
from #TempTable
Inner join REceiveDetail on ReceiveDetail.ReceiveDetailID = #TempTable.KeyID
Order by Manufacturer, Model, Colour, Conditions
------------------------------------------------------------------
-- Clean up. 
Drop Table #TempTable 

-- Select * from #TempTable 
-- All Done	
return
END

Go
/****** Object:  StoredProcedure [dbo].[Report_C_DespatchNoteDataDump]    Script Date: 02/13/2018 21:08:19 ******/
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

exec Report_C_DespatchNoteDataDump 'AS098'

[Out-Bound WayBill-S]
Select * from QUestion where Name = 'Out-Bound WayBill-S'
Select * from [option] where QuestionID = 248
Select * from ReceiveDetailItem where optionid = 2252

Select * from QUestion where Name = 'PSlip'
Select * from [option] where QuestionID = 565
Select * from ReceiveDetailItem where optionid = 7479 and Value = 'AS098'

Select * from QUestion where Name like 'Out%'



Declare @RDID numeric(18)
-- Select ReceiveDetailID, convert(int, 0) as process into #Tempx from ReceiveDetailItem where optionid = 7479 and Value = 'AS098'
while exists(Select * from #Tempx where process = 0)
      begin
      select @RDID = ReceiveDetailID from #Tempx where process = 0
      Update #Tempx set process = 1 where ReceiveDetailID = @RDID
      exec UpdateESNAttribute_NoProjectRestriction_BYID @RDID, 'Out-Bound WayBill-S','0wb9IIyyD34i','jimx'
      end

Update #Tempx set process = 0


exec Report_C_DespatchNote 'AS098'

*/

Create PROCEDURE [dbo].[Report_C_DespatchNoteDataDump]
          
      @PSlip nvarchar(200)
AS
	SET NOCOUNT ON;
	
BEGIN	
-- Start
--Declare @ProjectID numeric(18)
--Select @ProjectID = 7
-----------------------------------------------------------------------------
-- Get the Keys for the Data we want to report on.
Declare @IDList AS ListOfIDs 
Insert @IDList
Select ReceiveDetailID from ReceiveDetailItem I
Inner join [Option] O on I.OptionID = o.OptionID
inner join Question q on o.QuestionID = q.QuestionID
where q.Name = 'PSlip' and I.Value = @PSlip
--Select top 500 ReceivedetailID from ReceiveDetail where Version = '000' order by Createdate -- ReceiveDetailID in (47971,47972,47973,47974,47975,47976,47977,47978,47979,47980,47981,47982,47983)




------------------------------------------------------------------------------
-- Create the Temp table required to house the pivot table data
Declare @RValue nvarchar(max)
exec Get_Pivot_RawData_AlterStatement @IDList, '#TempTable', @RValue output
--Print @RValue
Create Table #TempTable (KeyID numeric(18))
EXEC sp_executesql @RValue
-- Select * from #TempTable
--Print 'Just Printed TempTable'
------------------------------------------------------------------
-- Get the data we are interested in.
Insert #TempTable
--Select * from #TempTable
exec GetData_Pivot_RawData @IDList


Select ReceiveDetail.ReceiveDetailID,
    ReceiveDetail.ClientLocationID,
    Client.ClientID as cClientID,
    Client.Name as cName,
    Client.CompanyName as cCompanyName,
    Client.ContactName as cContactName,
    Client.BillingAddress as cBillingAddress,
    Client.AddressLine1 as cAddressLine1,
    Client.AddressLine2 as cAddressLine2,
    Client.City as cCity,
    Client.StateOrProvince as cStateOrProvince,
    Client.PostalCode as cPostalCode,
    Client.PhoneNumber as cPhoneNumber,
    Client.FaxNumber as cFaxNumber,
    Client.EmailAddress as cEmailAddress,

    Client.RMASuffix as cRMASuffix,
    Client.isVendorGroup as cisVendorGroup,
    Client.ProductTag as cProductTag,
    Client.UserName as cUserName,

    ClientLocation.ClientLocationID as lClientLocationID,
    ClientLocation.Name as lName,
    ClientLocation.StoreNumber as lStoreNumber,
    ClientLocation.StoreSuffix as lStoreSuffix,
    ClientLocation.ScanKey as lScanKey,
    ClientLocation.MacroKey as lMacroKey,
    ClientLocation.Sequence as lSequence,
    ClientLocation.CompanyName as lCompanyName,
    ClientLocation.ContactName as lContactName,
    ClientLocation.BillingAddress as lBillingAddress,
    ClientLocation.AddressLine1 as lAddressLine1,
    ClientLocation.AddressLine2 as lAddressLine2,
    ClientLocation.City as lCity,
    ClientLocation.StateOrProvince as lStateOrProvince,
    ClientLocation.PostalCode as lPostalCode,
    ClientLocation.PhoneNumber as lPhoneNumber,
    ClientLocation.FaxNumber as lFaxNumber,
    ClientLocation.EmailAddress as lEmailAddress,
    ClientLocation.UserName as lUserName

, ReceiveDetail.ESN
, ReceiveDetail.Version
, ReceiveDetail.SKU as Sku
, #TempTable.* 
--into Template_DespatchNote
from #TempTable
Inner join REceiveDetail on ReceiveDetail.ReceiveDetailID = #TempTable.KeyID
Inner Join ClientLocation on ClientLocation.ClientLocationID = ReceiveDetail.ClientLocationID
inner join Client on Client.ClientID = ClientLocation.ClientID
Order by Manufacturer, Model, Colour




--Select * from Template_DespatchNote
Drop Table #TempTable 

-- Select * from #TempTable 
-- All Done	
END
go
















































































