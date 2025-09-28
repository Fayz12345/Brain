/****** Object:  StoredProcedure [dbo].[RecordProjectDefinitionProcess]    Script Date: 05/01/2017 17:11:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>


/*


exec RecordModelDefinitionMemory 6171,'7465,7467','','jmccomb'

Select * from MasterModelMemoryLookup

*/


-- =============================================
Alter PROCEDURE [dbo].[RecordModelDefinitionMemory]
    @mModelID numeric(18),
    @mMemoryKeyIDList varchar(8000),
	@mModelName varchar(20) = '',
	@mUserName varchar(50)
AS
BEGIN
SET NOCOUNT ON;

Declare @mStatusID numeric(18,0)
Select @mStatusID = 1
Select ValueID as TargetMemoryID, 0 as processed into #MemoryKeyList from dbo.fn_SplitDistinctNumeric(@mMemoryKeyIDList,',')

-- Remove any from NextProcessStep that are not in #NextStepList
Delete MasterModelMemoryLookup  
 Where MasterModelMemoryLookup.ModelID = @mModelID and MemoryID not in (Select TargetMemoryID from #MemoryKeyList)

---- Remove any from #NextStepList that is in NextProcessStep
Update #MemoryKeyList set Processed = 1 
 where TargetMemoryID in (select MemoryID from MasterModelMemoryLookup where MasterModelMemoryLookup.ModelID = @mModelID)

-- Add the rest to NextProcessStep
Insert MasterModelMemoryLookup 
           ([ModelID] ,[MemoryID], StatusID,[CreateDate] ,[CreateUser],[LastUpdateDate],[LastUpdateUser])
Select @mModelID, TargetMemoryID, (SELECT MasterCarrierManufacturerStatusID
  FROM [MasterCarrierManufacturerStatus] where Status = 'Active'), getdate(), @mUserName, getdate(), @mUserName
  from #MemoryKeyList 
 where processed = 0


Return 0

END


