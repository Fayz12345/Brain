
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


