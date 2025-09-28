




















SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*

Select * from [Option] where QuestionID in (Select QuestionID from Question where Name = 'DeviceHandset')
Select * from [Option] where QuestionID in (Select QuestionID from Question where Name = 'Model')
Select * from QuestionStatus

INSERT INTO [MasterModelToDeviceHandset] ([StatusID], [ModelID],[DeviceHandsetID],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser]) VALUES (1, 1444,7370,GetDate(),'System',GetDate(),'System')
INSERT INTO [MasterModelToDeviceHandset] ([StatusID],[ModelID],[DeviceHandsetID],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser]) VALUES (1, 1445,7372,GetDate(),'System',GetDate(),'System')
INSERT INTO [MasterModelToDeviceHandset] ([StatusID],[ModelID],[DeviceHandsetID],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser]) VALUES (1, 1492,7371,GetDate(),'System',GetDate(),'System')
INSERT INTO [MasterModelToDeviceHandset] ([StatusID],[ModelID],[DeviceHandsetID],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser]) VALUES (1, 1493,7373,GetDate(),'System',GetDate(),'System')

Select * from MasterModelToDeviceHandset

Select MasterModelToDeviceHandsetID, 
       Mster.StatusID, Status.Status, 
       Mster.ModelID, Model.OptionText, Model.Name as ModelName,
       Mster.DeviceHandsetID, Device.OptionText, Device.Name as DeviceName,
       Mster.CreateDate, Mster.CreateUser,
       Mster.LastUpdateDate, Mster.LastUpdateUser
 from MasterModelToDeviceHandset Mster
Inner join [Option] model on Model.OptionID = ModelID
Inner join [Option] Device on Device.OptionID = DeviceHandsetID
Inner join QuestionStatus Status on Status.QuestionStatusID = Mster.StatusID

-- Drop Table MasterModelToDeviceHandset

*/


-- Drop Table PricingSKURateTable
CREATE TABLE [dbo].[MasterModelToDeviceHandset](
	[MasterModelToDeviceHandsetID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[StatusID] [numeric](18, 0) NOT NULL,	-- This status pulls from the QuestionStatus Table.
	[ModelID] [numeric](18, 0) NOT NULL,
	[DeviceHandsetID] [numeric](18, 0) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,	
	
 CONSTRAINT [PK_MasterModelToDeviceHandset] PRIMARY KEY CLUSTERED 
(
	[MasterModelToDeviceHandsetID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MasterModelToDeviceHandset] ADD  CONSTRAINT [DF_MasterModelToDeviceHandset_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[MasterModelToDeviceHandset] ADD  CONSTRAINT [DF_MasterModelToDeviceHandset_CreateUser]  DEFAULT ('') FOR [CreateUser]
GO

























































































































SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>


/*

exec AddModelToDeviceHandSet 1444,7370, 'Jim'
exec AddModelToDeviceHandSet 1492,7370, 'Jim'
exec AddModelToDeviceHandSet 1726,7373, 'Jim'
exec AddModelToDeviceHandSet 9999,7373, 'Jim'
exec AddModelToDeviceHandSet 1726,9999, 'Jim'



Select * from MasterModelToDeviceHandset

Select MasterModelToDeviceHandsetID, 
       Mster.StatusID, Status.Status, 
       Mster.ModelID, Model.OptionText, Model.Name as ModelName,
       Mster.DeviceHandsetID, Device.OptionText, Device.Name as DeviceName,
       Mster.CreateDate, Mster.CreateUser,
       Mster.LastUpdateDate, Mster.LastUpdateUser
 from MasterModelToDeviceHandset Mster
Inner join [Option] model on Model.OptionID = ModelID
Inner join [Option] Device on Device.OptionID = DeviceHandsetID
Inner join QuestionStatus Status on Status.QuestionStatusID = Mster.StatusID

*/


-- =============================================
Create PROCEDURE [dbo].[AddModelToDeviceHandset]
    @mModelID numeric(18),
    @mDeviceHandsetID numeric(18),
	@mUserName varchar(50)
AS
BEGIN
SET NOCOUNT ON;

Declare @mStatusID numeric(18,0)
Select @mStatusID = (Select Top 1 QuestionStatusID from QuestionStatus where Status = 'Active')

if not exists(Select * from [Option] O inner join Question Q on O.QuestionID = Q.QuestionID and Q.Name = 'Model' and O.OptionID = @mModelID)
   begin
   print 'Model Not found:'  + convert(nvarchar(10), @mModelID) 
   return 1
   end

if not exists(Select * from [Option] O inner join Question Q on O.QuestionID = Q.QuestionID and Q.Name = 'DeviceHandset' and O.OptionID = @mDeviceHandsetID)
   begin
   print 'Device Handset Not found:'  + convert(nvarchar(10), @mDeviceHandsetID) 
   return 1
   end


if exists (Select * from MasterModelToDeviceHandset where ModelID = @mModelID)
   begin
   Update [MasterModelToDeviceHandset] set StatusID = @mStatusID, DeviceHandsetID = @mDeviceHandsetID, LastUpdateDate = getdate(), LastUpdateUser = @mUserName where ModelID = @mModelID
   print 'Updated:' + convert(nvarchar(10), @mModelID)
   return 0
   end

if Not Exists (Select * from MasterModelToDeviceHandset where ModelID = @mModelID)
   begin
   INSERT INTO [MasterModelToDeviceHandset] ([StatusID], [ModelID],[DeviceHandsetID],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser]) 
       VALUES (@mStatusID, @mModelID,@mDeviceHandsetID,GetDate(),@mUserName,GetDate(),@mUserName)
   print 'Inserted' + convert(nvarchar(10), @mModelID)    
   return 0
   End
  
Return 1

END



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>


/*

exec DeleteModelToDeviceHandset 1444, 'Jimx'
exec DeleteModelToDeviceHandset 1492, 'Jimx'
exec DeleteModelToDeviceHandset 9999, 'Jimx'


Select * from MasterModelToDeviceHandset

Select MasterModelToDeviceHandsetID, 
       Mster.StatusID, Status.Status, 
       Mster.ModelID, Model.OptionText, Model.Name as ModelName,
       Mster.DeviceHandsetID, Device.OptionText, Device.Name as DeviceName,
       Mster.CreateDate, Mster.CreateUser,
       Mster.LastUpdateDate, Mster.LastUpdateUser
 from MasterModelToDeviceHandset Mster
Inner join [Option] model on Model.OptionID = ModelID
Inner join [Option] Device on Device.OptionID = DeviceHandsetID
Inner join QuestionStatus Status on Status.QuestionStatusID = Mster.StatusID

*/

-- =============================================
Create PROCEDURE [dbo].[DeleteModelToDeviceHandset]
    @mModelID numeric(18),
	@mUserName varchar(50)
AS
BEGIN
SET NOCOUNT ON;

Declare @mStatusID numeric(18,0)
Select @mStatusID = (Select Top 1 QuestionStatusID from QuestionStatus where Status = 'Inactive')

if exists (Select * from MasterModelToDeviceHandset where ModelID = @mModelID)
   begin
   -- Delete [MasterModelToDeviceHandset] where ModelID = @mModelID
   Update [MasterModelToDeviceHandset] set StatusID = @mStatusID, LastUpdateDate = getdate(), LastUpdateUser = @mUserName where ModelID = @mModelID
   print 'Set to Inactive:' + convert(nvarchar(10), @mModelID)  
   Return 0 
   end

print 'Model Not found:' + convert(nvarchar(10), @mModelID)  
Return 0

END


/****** Object:  StoredProcedure [dbo].[Get_SKULookupChain]    Script Date: 03/10/2022 20:27:45 ******/
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

Declare @mModelID numeric(18)
Declare @mDeviceHandsetID numeric(18)
Declare @mDeviceHandSetName nvarchar(20)
Declare @mDeviceHandSetDescription nvarchar(50)

Select @mModelID = 6252

Exec Get_DeviceHandSetForThisModel @mModelID, @mDeviceHandsetID Output, @mDeviceHandSetName Output, @mDeviceHandSetDescription Output

Print 'ModelID:' + convert(nvarchar(20),@mModelID)
Print 'DeviceHandsetID:' + convert(nvarchar(20),@mDeviceHandsetID)
Print 'DeviceHandset Name:' + @mDeviceHandSetName
Print 'DeviceHandset Description:' + @mDeviceHandSetDescription

-----------------

Select * from MasterModelToDeviceHandset

Select MasterModelToDeviceHandsetID, 
       Mster.StatusID, Status.Status, 
       Mster.ModelID, Model.OptionText, Model.Name as ModelName,
       Mster.DeviceHandsetID, Device.OptionText, Device.Name as DeviceName,
       Mster.CreateDate, Mster.CreateUser,
       Mster.LastUpdateDate, Mster.LastUpdateUser
 from MasterModelToDeviceHandset Mster
Inner join [Option] model on Model.OptionID = ModelID
Inner join [Option] Device on Device.OptionID = DeviceHandsetID
Inner join QuestionStatus Status on Status.QuestionStatusID = Mster.StatusID

*/

Create PROCEDURE [dbo].[Get_DeviceHandSetForThisModel]

      @mModelID numeric(18),
      @mDeviceHandsetID numeric(18) output,
      @mDeviceHandSetName nvarchar(20) output,
      @mDeviceHandSetDescription nvarchar(50) output

AS
BEGIN
	SET NOCOUNT ON;

Select @mDeviceHandsetID = -1
Select @mDeviceHandSetName = ''
Select @mDeviceHandSetDescription = ''

if exists (Select * from [MasterModelToDeviceHandset] where ModelID = @mModelID)
   Select Top 1 @mDeviceHandsetID = [DeviceHandsetID], @mDeviceHandSetName = O.Name, @mDeviceHandSetDescription = O.OptionText
          From [MasterModelToDeviceHandset] M inner join [Option] O on M.DeviceHandsetID = O.OptionID
          where ModelID = @mModelID

------------------------------------------------     
 
return 0

END


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>


/*

exec RecordModelToDeviceHandset 7370,'1445,1493,1492,1726','','jmccomb'

--exec AddModelToDeviceHandSet 1444,7370, 'Jim'
--exec AddModelToDeviceHandSet 1492,7370, 'Jim'
--exec AddModelToDeviceHandSet 1726,7373, 'Jim'
--exec AddModelToDeviceHandSet 9999,7373, 'Jim'
--exec AddModelToDeviceHandSet 1726,9999, 'Jim'


Select * from MasterModelToDeviceHandset
Delete MasterModelToDeviceHandset

Select MasterModelToDeviceHandsetID, 
       Mster.StatusID, Status.Status, 
       Mster.ModelID, Model.OptionText, Model.Name as ModelName,
       Mster.DeviceHandsetID, Device.OptionText, Device.Name as DeviceName,
       Mster.CreateDate, Mster.CreateUser,
       Mster.LastUpdateDate, Mster.LastUpdateUser
 from MasterModelToDeviceHandset Mster
Inner join [Option] model on Model.OptionID = ModelID
Inner join [Option] Device on Device.OptionID = DeviceHandsetID
Inner join QuestionStatus Status on Status.QuestionStatusID = Mster.StatusID

*/


-- =============================================
Create PROCEDURE [dbo].[RecordModelToDeviceHandset]
    @mDeviceHandsetID numeric(18),
    @mModelKeyIDList varchar(8000),
	@mDeviceHandsetName varchar(20) = '',
	@mUserName varchar(50)
AS
BEGIN
SET NOCOUNT ON;

Declare @mStatusID numeric(18,0)
Select @mStatusID = (Select Top 1 QuestionStatusID from QuestionStatus where Status = 'Active')


Select ValueID as TargetModelID, 0 as processed into #ModelKeyList from dbo.fn_SplitDistinctNumeric(@mModelKeyIDList,',')

-- Remove any from NextProcessStep that are not in #ModelKeyList
Delete MasterModelToDeviceHandset  
 Where MasterModelToDeviceHandset.DeviceHandsetID = @mDeviceHandsetID and ModelID not in (Select TargetModelID from #ModelKeyList)

---- Remove any from #ModelKeyList that is in NextProcessStep
Update #ModelKeyList set Processed = 1 
 where TargetModelID in (select ModelID from MasterModelToDeviceHandset where MasterModelToDeviceHandset.DeviceHandsetID = @mDeviceHandsetID)

-- Add the rest to NextProcessStep
Insert MasterModelToDeviceHandset 
           ([DeviceHandsetID],[ModelID], [StatusID],[CreateDate] ,[CreateUser],[LastUpdateDate],[LastUpdateUser])

Select @mDeviceHandsetID, TargetModelID, @mStatusID, getdate(), @mUserName, getdate(), @mUserName
  from #ModelKeyList 
 where processed = 0
  
Return 1

END



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>


/*

exec UpdateAllDevicesModelToDeviceHandset 'Jimd'

Select * from MasterModelToDeviceHandset

Select MasterModelToDeviceHandsetID, 
       Mster.StatusID, Status.Status, 
       Mster.ModelID, Model.OptionText, Model.Name as ModelName,
       Mster.DeviceHandsetID, Device.OptionText, Device.Name as DeviceName,
       Mster.CreateDate, Mster.CreateUser,
       Mster.LastUpdateDate, Mster.LastUpdateUser
 from MasterModelToDeviceHandset Mster
Inner join [Option] model on Model.OptionID = ModelID
Inner join [Option] Device on Device.OptionID = DeviceHandsetID
Inner join QuestionStatus Status on Status.QuestionStatusID = Mster.StatusID

*/

-- =============================================
Create PROCEDURE [dbo].[UpdateAllDevicesModelToDeviceHandset]
	@mUserName varchar(50)
AS
BEGIN
SET NOCOUNT ON;

Declare @mCount numeric(18)
Declare @mCountNot numeric(18)
Declare @id numeric(18)
Declare @mModelID numeric(18)
Declare @mStatusID numeric(18,0)
Declare @mDeviceHandSetID numeric(18)
Declare @mAttributeValue nVarchar(50)

Select @mCount = 0
Select @mCountNot = 0
Select @mStatusID = (Select Top 1 QuestionStatusID from QuestionStatus where Status = 'Active')

Select ReceiveDetailID, ModelID, 0 as Processed into #TempxTable from ReceiveDetail

While exists(Select * from #TempxTable where Processed = 0)
      begin
      Select top 1 @id = ReceiveDetailID, @mModelID = #TempxTable.ModelID from #TempxTable where Processed = 0
	  if not exists(Select * from MasterModelToDeviceHandset Where ModelID = @mModelID and StatusID = @mStatusID)
	     begin
		 print 'Model To DeviceHandset not found RID:'  + convert(nvarchar(10), @id)  + ' Model:'  + convert(nvarchar(10), @mModelID) 
         Select @mCountNot = @mCountNot + 1
		 end

	  if exists(Select * from MasterModelToDeviceHandset Where ModelID = @mModelID and StatusID = @mStatusID)
	     begin
         Select Top 1 @mDeviceHandSetID = DeviceHandSetID from MasterModelToDeviceHandset Where ModelID = @mModelID and StatusID = @mStatusID
         Select @mAttributeValue = OptionText from [Option] where [OptionID] = @mDeviceHandSetID
         Select @mCount = @mCount + 1
         exec UpdateESNAttribute_NoProjectRestriction_BYID @id, 'DeviceHandset', @mAttributeValue, @mUserName
		 end

      update #TempxTable set Processed = 1 where ReceiveDetailID = @id
      end

print '---------------'
print 'Number of Devices Updated:' + convert(nvarchar(10), @mCount)  
print 'Number of Devices NOT Updated:' + convert(nvarchar(10), @mCountNot)  
Return 0

END


















































































