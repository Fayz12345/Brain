
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


