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

