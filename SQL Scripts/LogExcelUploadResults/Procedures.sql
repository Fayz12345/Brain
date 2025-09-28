
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


exec GetDeviceCatalogue

Select * from MasterModelMemoryLookup

*/


-- =============================================
Alter PROCEDURE [dbo].[GetDeviceCatalogue]

AS
BEGIN
SET NOCOUNT ON;

Select SKU, COUNT(*) as Qty, CONVERT(int, 0) as Allocated from ReceiveDetail r
Inner join ReceiveDetailStatus s on r.StatusID = s.ReceiveDetailStatusID
inner join ClientLocation CL on cl.ClientLocationID = r.ClientLocationID
inner join Client C on cl.ClientID = c.ClientID
Where Version = '000' and s.Status != 'GraveYard' 
Group By SKU
having count(*) > 0
Order by SKU


END


/****** Object:  StoredProcedure [dbo].[GetDeviceCataloguePartial]    Script Date: 05/17/2017 09:05:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>


/*


exec GetDeviceCataloguePartial 'ALC-BEL-40A-          -  BLK-  -B -  -           ,ALC-BEL-40A-          -  GRY-  -d -  -           ,ALC-BEL-50A-          -  GRY-  -d -  -           ,ALC-BEL-50A-          -  GRY-  -W -  -           ,ALC-KOO-170-          -  BLK-  -A -  -           ,ALC-KOO-170-          -  BLK-  -B -  -           ,ALC-KOO-170-          -  BLK-  -C -  -           ,ALC-KOO-170-          -  BLK-  -d -  -           '


Declare @List nvarchar(max)
Select @List = 'ALC-BEL-40A-          -  BLK-  -B -  -           ,ALC-BEL-40A-          -  GRY-  -d -  -           ,ALC-BEL-50A-          -  GRY-  -d -  -           ,ALC-BEL-50A-          -  GRY-  -W -  -           ,ALC-KOO-170-          -  BLK-  -A -  -           ,ALC-KOO-170-          -  BLK-  -B -  -           ,ALC-KOO-170-          -  BLK-  -C -  -           ,ALC-KOO-170-          -  BLK-  -d -  -           '
SELECT * into #Tempxxx FROM fn_Split(@List, ',')
Select * from #Tempxxx
Drop table #Tempxxx

*/


-- =============================================
Create PROCEDURE [dbo].[GetDeviceCataloguePartial]
     @List nvarchar(max)

AS
BEGIN
SET NOCOUNT ON;


-- SELECT * into #Tempxxx FROM fn_Split(@List, '/')


Declare @text nvarchar(max)
Select @text = @List
DECLARE @index int 
Declare @delimiter varchar(20)
Select @delimiter = ','



SELECT convert(numeric(18),0) as Processed, * into #Tempxxx FROM fn_Split(@List, @delimiter)
Select SKU, COUNT(*) as Qty, CONVERT(int, 0) as Allocated from ReceiveDetail r
Inner join ReceiveDetailStatus s on r.StatusID = s.ReceiveDetailStatusID
inner join ClientLocation CL on cl.ClientLocationID = r.ClientLocationID
inner join Client C on cl.ClientID = c.ClientID
inner join #Tempxxx L on L.value = r.SKU
Where Version = '000' and s.Status != 'GraveYard' -- and SKU in (Select value from @List)
Group By SKU
having count(*) > 0
Order by SKU

Drop Table #Tempxxx
END


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






















