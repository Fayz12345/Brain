
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


