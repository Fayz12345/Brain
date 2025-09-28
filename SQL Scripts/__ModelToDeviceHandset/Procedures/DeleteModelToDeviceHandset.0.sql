
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


