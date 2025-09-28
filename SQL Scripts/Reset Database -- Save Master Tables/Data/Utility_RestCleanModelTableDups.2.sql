/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2017 (14.0.3023)
    Source Database Engine Edition : Microsoft SQL Server Standard Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [BWUK_Sandbox]
GO
/****** Object:  StoredProcedure [dbo].[Utility_RestCleanModelTableDups]    Script Date: 2/11/2019 8:34:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------

--------------------------------------------------------------------------------------
/*

exec Utility_RestCleanModelTableDups

*/
--------------------------------------------------------------------------------------
--
*/




ALTER PROCEDURE [dbo].[Utility_RestCleanModelTableDups]

 AS

Begin
SET NOCOUNT ON
--Select * from QUestion where Name = 'Manufacturer'
--Select * from QUestion where Name = 'Model'
--Select * from QUestion where Name = 'Colour'
--Select * from QUestion where Name = 'Carrier'
--Select * from [Option] where QuestionID in (243, 244, 214, 210) order by QuestionID, OptionText, OptionID
--Select * from MasterCarrierManufacturerLookup
--Select * from OptionStatus
--Select * from [Option] where OptionStatusID != 1
DECLARE @RowCount1 INTEGER
DECLARE @RowCount2 INTEGER
DECLARE @RowCount3 INTEGER
DECLARE @RowCount4 INTEGER
Declare @Message nvarchar(500)




/*



Select QuestionID, OptionText, Count(*) as Freq from [Option] where QuestionID in (243, 244, 214, 210) Group by QuestionID, OptionText having count(*) > 1  order by QuestionID, OptionText



*/









Print '--------------------------------   Start   ---------------------------------------------------'
Select @Message = 'Get any duplicate Options on Questions.  (Make, Model, Colour, Carrier)'
Print @Message




Delete MasterCarrierManufacturerLookup  where StatusID = 3
Select @Message = 'Physical Delete of Lookup Records Status = inactive; so we are only dealing with active records:' + convert(nvarchar(10),@@ROWCOUNT)
Print @Message
Delete [Option] where OptionStatusID != 1
Select @Message = 'Physically Delete inactive option Records; so we are only dealing with active records:' + convert(nvarchar(10),@@ROWCOUNT)
Print @Message

Select QuestionID, OptionText, Count(*) as Freq into #Temp1 from [Option] where QuestionID in (243, 244, 214, 210) Group by QuestionID, OptionText having count(*) > 1  order by QuestionID, OptionText
Select @Message = 'Records Duplicated:' + convert(nvarchar(10),@@ROWCOUNT)
Print @Message

select Q.Name as QName, O.*, OptionID as MappedID, convert(int, 0) as process into #Temp2 
from [Option] O Inner join #Temp1 T on T.QuestionID = O.QuestionID and T.OptionText = O.OptionText
                inner join Question Q on T.QuestionID = Q.QuestionID

--Select * from #Temp1
--Select * from #Temp2
--Drop table #Temp1
--Drop table #Temp2
--return



Declare @Keep int
Declare @Remv int
Declare @ID numeric(18,0)
Declare @OptionText nvarchar(50)
Declare @DupID numeric(18,0)
Update #Temp2 set process = 0, MappedID = -1
Select @Keep = 0, @Remv = 0

print  'figure out the records we want to keep.    OptionID == MappedID -- Keep one and get rid of the others.'
While exists(Select * from #Temp2 where process = 0)
      begin
	  Select top 1 @ID = optionID, @OptionText = OptionText from #Temp2 where process = 0
      Update #Temp2 set process = 1, mappedID = @ID where #Temp2.OptionID = @ID
	  Select @Keep = @Keep + 1
	  while exists(Select * from #Temp2 where process = 0 and OptionID != @ID and OptionText = @OptionText)
	        begin
			Select Top 1 @DupID = OptionID from #Temp2 where process = 0 and OptionID != @ID and OptionText = @OptionText
			Update #Temp2 set process = 1, mappedID = @ID where #Temp2.OptionID = @DupID
            Select @Remv = @Remv + 1
			end
	  end

Select @Message = 'Records To Be Retained:' + convert(nvarchar(10),@Keep)
Print @Message
Select @Message = 'Records To Be Remapped/Removed:' + convert(nvarchar(10),@Remv)
Print @Message

-----------------------------------------------------------------
print 'We need to Make Inactive the attribute we deam duplicate that found inside the option table'
Update [Option] set OptionStatusID = 2 where OptionID in (Select OptionID from #Temp2 T where T.OptionID != T.MappedID)
Select @Message = 'Option Records set to Deleted:' + convert(nvarchar(10),@@ROWCOUNT)
Print @Message




print 'Now we need to Map and MasterCarrierManufacturer Duplicates over to the good one we want to keep'
Select L.MasterCarrierManufacturerLookupID, MappedID, T.OptionText into #temp3 from MasterCarrierManufacturerLookup L
Inner join #Temp2 T on T.OptionID = L.OptionModelID and T.OptionID != T.MappedID
Select @Message = 'Getting a list of Lookup Records that need to be remapped.:' + convert(nvarchar(10),@@ROWCOUNT)
Print @Message


Update MasterCarrierManufacturerLookup set OptionModelID = MappedID, Model = OptionText
From MasterCarrierManufacturerLookup L Inner join #temp3 T on T.MasterCarrierManufacturerLookupID = L.MasterCarrierManufacturerLookupID
Select @Message = 'Lookup Model Remapped:' + convert(nvarchar(10),@@ROWCOUNT)
Print @Message
 
 
print 'Do some Cleanup'
Drop Table #Temp1
Drop Table #Temp2
Drop Table #Temp3



----------------------------------------------------------------------------------------------------------
Print 'Looking at the MasterCarrierManufacturerLookup Table'
Print 'Make sure the record status is inactive if the option is.'
--Print 'Deal with Carrier'   ------------------------------------------------------------
--Select StatusID, O.OptionStatusID from MasterCarrierManufacturerLookup L
--Inner join [Option] O on L.OptionCarrierID = O.OptionID 
--where O.OptionStatusID = 2

--Print 'Deal with Colour'   ------------------------------------------------------------
--Select StatusID, O.OptionStatusID from MasterCarrierManufacturerLookup L
--Inner join [Option] O on L.OptionColourID = O.OptionID 
--where O.OptionStatusID = 2

--Print 'Deal with Manufacturer'   ------------------------------------------------------------
--Select StatusID, O.OptionStatusID from MasterCarrierManufacturerLookup L
--Inner join [Option] O on L.OptionManufacturerID = O.OptionID 
--where O.OptionStatusID = 2

--Print 'Deal with Model'   ------------------------------------------------------------
--Select StatusID, O.OptionStatusID from MasterCarrierManufacturerLookup L
--Inner join [Option] O on L.OptionModelID = O.OptionID 
--where O.OptionStatusID = 2

--Select * from MasterCarrierManufacturerLookup where StatusID = 2
--Select * from MasterCarrierManufacturerLookup where StatusID = 3
--Select * from MasterCarrierManufacturerLookup where StatusID = 4

Update MasterCarrierManufacturerLookup set StatusID = 3 where not OptionCarrierID in (Select OptionID from [Option] where OptionID = OptionCarrierID)
Select @Message = 'Set Inactive any orphan Carriers:' + convert(nvarchar(10),@@ROWCOUNT)
Print @Message
Update MasterCarrierManufacturerLookup set StatusID = 3 where not OptionManufacturerID in (Select OptionID from [Option] where OptionID = OptionManufacturerID)
Select @Message = 'Set Inactive any orphan Manufacturer:' + convert(nvarchar(10),@@ROWCOUNT)
Print @Message
Update MasterCarrierManufacturerLookup set StatusID = 3 where not OptionModelID in (Select OptionID from [Option] where OptionID = OptionModelID)
Select @Message = 'Set Inactive any orphan Model:' + convert(nvarchar(10),@@ROWCOUNT)
Print @Message
Update MasterCarrierManufacturerLookup set StatusID = 3 where not OptionColourID in (Select OptionID from [Option] where OptionID = OptionColourID)
Select @Message = 'Set Inactive any orphan Colour:' + convert(nvarchar(10),@@ROWCOUNT)
Print @Message
Delete MasterCarrierManufacturerLookup  where StatusID = 3
Select @Message = 'Physical Delete of Lookup Records Status = inactive.:' + convert(nvarchar(10),@@ROWCOUNT)
Print @Message
Delete [Option] where OptionStatusID != 1
Select @Message = 'Physically Delete inactive option Records; so we are only dealing with active records:' + convert(nvarchar(10),@@ROWCOUNT)
Print @Message

Print '--------------------------------   DONE   ---------------------------------------------------'
-- SET NOCOUNT OFF
End