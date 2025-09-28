/****** Object:  StoredProcedure [dbo].[Update_ReceiveDetailSKUFields]    Script Date: 10/18/2017 13:31:25 ******/
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

Exec UpdateESNAttribute_NoProjectRestriction_BYIDHeaderSwitch 1216995, 'Carrier','BEL',0,'jm'
Exec UpdateESNAttribute_NoProjectRestriction_BYIDHeaderSwitch 1216995, 'Carrier','FID',0,'jm'
exec Utility_RebuildReceiveDetailHeaderAttributes_ThisIMEI_BYID 1216995, -1
*/

ALTER PROCEDURE [dbo].[Update_ReceiveDetailSKUFields]
    @mReceiveDetailID numeric(18),
    @mCarrierID numeric(18),
    @mManufacturerID numeric(18),
    @mModelID numeric(18),
    @mMemoryID numeric(18),    
    @mColourID numeric(18),
    @mUserName nvarchar(50) ='XXXX'
    
   
AS
BEGIN
Set NOCOUNT on

Declare @CarrierQuestionID numeric(18)
Declare @ManufacturerQuestionID numeric(18)
Declare @ModelQuestionID numeric(18)
Declare @ColourQuestionID numeric(18)
--Declare @MemoryQuestionID numeric(18)


Declare @CarrierDesc nvarchar(50)
Declare @CarrierName nvarchar(50)
Declare @ManufacturerDesc nvarchar(50)
Declare @ManufacturerName nvarchar(50)
Declare @ModelDesc nvarchar(50)
Declare @ModelName nvarchar(50)
Declare @MemoryDesc nvarchar(50)
--Declare @MemoryName nvarchar(50)

Declare @ColourDesc nvarchar(50)
Declare @ColourName nvarchar(50)

Declare @CarrierItemID numeric(18)
Declare @ManufacturerItemID numeric(18)
Declare @ModelItemID numeric(18)
--Declare @MemoryItemID numeric(18)
Declare @ColourItemID numeric(18)

Select @mMemoryID = -1

Select @CarrierQuestionID = QuestionID, @CarrierDesc = OptionText, @CarrierName = Name from [Option] where OptionID = @mCarrierID 
Select @ManufacturerQuestionID = QuestionID, @ManufacturerDesc = OptionText, @ManufacturerName = Name from [Option] where OptionID = @mManufacturerID 
Select @ModelQuestionID = QuestionID, @ModelDesc = OptionText, @ModelName = Name from [Option] where OptionID = @mModelID 
Select @ColourQuestionID = QuestionID, @ColourDesc = OptionText, @ColourName = Name from [Option] where OptionID = @mColourID 

--Select @MemoryQuestionID = QuestionID, @MemoryDesc = OptionText, @MemoryName = Name from [Option] where OptionID = @mMemoryID 


Select @CarrierItemID = ReceiveDetailItemID 
  from ReceiveDetailItem  
 Inner join [Option] O on O.OptionID = ReceiveDetailItem.OptionID
 where ReceiveDetailID = @mReceiveDetailID and O.QuestionID = @CarrierQuestionID

Select @ManufacturerItemID = ReceiveDetailItemID 
  from ReceiveDetailItem  
 Inner join [Option] O on O.OptionID = ReceiveDetailItem.OptionID
 where ReceiveDetailID = @mReceiveDetailID and O.QuestionID = @ManufacturerQuestionID

Select @ModelItemID = ReceiveDetailItemID 
  from ReceiveDetailItem  
 Inner join [Option] O on O.OptionID = ReceiveDetailItem.OptionID
 where ReceiveDetailID = @mReceiveDetailID and O.QuestionID = @ModelQuestionID

Select @ColourItemID = ReceiveDetailItemID 
  from ReceiveDetailItem  
 Inner join [Option] O on O.OptionID = ReceiveDetailItem.OptionID
 where ReceiveDetailID = @mReceiveDetailID and O.QuestionID = @ColourQuestionID

--Select @MemoryItemID = ReceiveDetailItemID 
--  from ReceiveDetailItem  
-- Inner join [Option] O on O.OptionID = ReceiveDetailItem.OptionID
-- where ReceiveDetailID = @mReceiveDetailID and O.QuestionID = @MemoryQuestionID

Update ReceiveDetailItem set OptionID = @mCarrierID where ReceiveDetailItemID = @CarrierItemID
Update ReceiveDetailItem set OptionID = @mManufacturerID where ReceiveDetailItemID = @ManufacturerItemID
Update ReceiveDetailItem set OptionID = @mModelID where ReceiveDetailItemID = @ModelItemID
Update ReceiveDetailItem set OptionID = @mColourID where ReceiveDetailItemID = @ColourItemID
--Update ReceiveDetailItem set OptionID = @mMemoryID where ReceiveDetailItemID = @MemoryItemID

Update REceiveDetail Set ManufacturerID = @mManufacturerID, Manufacturer = @ManufacturerDesc
                       , CarrierID = @mCarrierID, Carrier = @CarrierDesc
                       , ModelID = @mModelID, Model = @ModelDesc
                       , SKU = dbo.GetIFSSKU(@mReceiveDetailID)
where ReceiveDetail.ReceiveDetailID = @mReceiveDetailID

Update REceiveDetail Set SKU = dbo.GetIFSSKU(@mReceiveDetailID)
where ReceiveDetail.ReceiveDetailID = @mReceiveDetailID
Return 0

END
