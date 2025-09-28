/****** Object:  StoredProcedure [dbo].[Utility_ReplaceOptionAttributeID]    Script Date: 07/31/2017 11:37:10 ******/
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

Select * from Question where name = 'Model'
Select * from [Option] o where -- questionID = 14 and 
         exists(select * from [option] b where b.optiontext = o.optiontext and b.QuestionID = o.questionID and b.OptionID != o.OptionID)
 order by optiontext


declare @mRecords int
exec Utility_ReplaceOptionAttributeID 149, 150, @mRecords Output
Print 'Rows Affected:' + convert(nvarchar(20), @mRecords)


declare @mRecords int
exec Utility_ReplaceOptionAttributeID 150, 149, @mRecords Output
Print 'Rows Affected:' + convert(nvarchar(20), @mRecords)



*/

ALTER PROCEDURE [dbo].[Utility_ReplaceOptionAttributeID]
        @mSourceID numeric(18),
        @mTargetOptionID numeric(18),
        @mUserName nvarchar(20),
        @mRecordsAffected int output

AS
BEGIN
SET NOCOUNT ON;

DECLARE @Rows   int
Select @Rows = 0
---your query here

--Select @mRecordsAffected = 12
--return




Update MasterBucketTransactions Set  OptionID = @mTargetOptionID Where OptionID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT
Update ClientAnswerRestrict Set  OptionID = @mTargetOptionID, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName Where OptionID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT
Update QuestionDependencies Set  SourceOptionID = @mTargetOptionID, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName Where SourceOptionID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT
Update QuestionDependencies Set TargetOptionID = @mTargetOptionID, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName Where TargetOptionID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

Update [Option_Text_Defaults] Set  SourceOptionID = @mTargetOptionID, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName Where SourceOptionID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT
Update [Option_Text_Defaults] Set TargetOptionID = @mTargetOptionID, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName Where TargetOptionID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT






print 'step 1 done'

-- The following 4 statements could leave duplicate records in the MasterCarrierManufacturerLookup. 
--     because this utility changes attributes with x id to attribute with y id, we will leave these out of the look
-- 



/*
Update [MasterCarrierManufacturerLookup] set [OptionModelID] = 
Update [MasterCarrierManufacturerLookup] set [OptionManufacturerID] = 
Update [MasterCarrierManufacturerLookup] set [OptionCarrierID] = 
Update [MasterCarrierManufacturerLookup] set [OptionColourID] = 

Update [MasterModelMemoryLookup] set [ModelID] = 
Update [MasterModelMemoryLookup] set [MemoryID] = 
*/

-- if the target exists, delete the original, otherwise change the original.
--if exists(select * from MasterCarrierManufacturerLookup where OptionCarrierID = @mTargetOptionID)
--   begin 
--   Delete MasterCarrierManufacturerLookup  where OptionCarrierID = @mSourceID
--   SELECT @Rows= @Rows + @@ROWCOUNT     
--   end
--else   
--   begin
--   Update MasterCarrierManufacturerLookup set OptionCarrierID = @mTargetOptionID, Carrier = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
--   From MasterCarrierManufacturerLookup Inner join [Option] on @mTargetOptionID
--   where OptionCarrierID = @mSourceID
--   SELECT @Rows= @Rows + @@ROWCOUNT  
--   end   


--if exists(select * from MasterCarrierManufacturerLookup where OptionManufacturerID = @mTargetOptionID)
--   begin 
--   Delete MasterCarrierManufacturerLookup  where OptionManufacturerID = @mSourceID
--   SELECT @Rows= @Rows + @@ROWCOUNT     
--   end
--else
--   begin
--   Update MasterCarrierManufacturerLookup set OptionManufacturerID = @mTargetOptionID, Manufacturer = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
--   From MasterCarrierManufacturerLookup Inner join [Option] on [Option].OptionID = @mTargetOptionID
--   where OptionManufacturerID = @mSourceID
--   SELECT @Rows= @Rows + @@ROWCOUNT  
--   end  
   
-- if exists(select * from MasterCarrierManufacturerLookup where OptionModelID = @mTargetOptionID)
--   begin 
--   Delete MasterCarrierManufacturerLookup  where OptionModelID = @mSourceID
--   SELECT @Rows= @Rows + @@ROWCOUNT     
--   end
--else
--   begin
--   Update MasterCarrierManufacturerLookup set OptionModelID = @mTargetOptionID, Model = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
--   From MasterCarrierManufacturerLookup Inner join [Option] on [Option].OptionID = @mTargetOptionID
--   where OptionModelID = @mSourceID
--   SELECT @Rows= @Rows + @@ROWCOUNT  
--   end  

-- if exists(select * from MasterCarrierManufacturerLookup where OptionColourID = @mTargetOptionID)
--   begin 
--   Delete MasterCarrierManufacturerLookup  where OptionColourID = @mSourceID
--   SELECT @Rows= @Rows + @@ROWCOUNT     
--   end
--else
--   begin
--   Update MasterCarrierManufacturerLookup set OptionColourID = @mTargetOptionID, Colour = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
--   From MasterCarrierManufacturerLookup Inner join [Option] on [Option].OptionID = @mTargetOptionID

--   where OptionColourID = @mSourceID
--   SELECT @Rows= @Rows + @@ROWCOUNT  
--   end  

update MasterSKU set StatusID = 3, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName where CarrierID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

update MasterSKU set StatusID = 3, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName where ManufacturerID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

update MasterSKU set StatusID = 3, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName where ModelID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

update MasterSKU set StatusID = 3, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName where ColourID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT



--print 'step 2 done'

Update MasterPartsRequestedLog set CarrierID = @mTargetOptionID, Carrier = [Option].OptionID, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
From MasterPartsRequestedLog Inner join [Option] on [Option].OptionID = @mTargetOptionID
where CarrierID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

Update MasterPartsRequestedLog set ManufacturerID = @mTargetOptionID, Manufacturer = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
From MasterPartsRequestedLog Inner join [Option] on [Option].OptionID = @mTargetOptionID
 where ManufacturerID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

Update MasterPartsRequestedLog set ModelID = @mTargetOptionID, Model = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
From MasterPartsRequestedLog Inner join [Option] on [Option].OptionID = @mTargetOptionID
where ModelID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

Update MasterPartsRequestedLog set ColourID = @mTargetOptionID,  Colour = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
From MasterPartsRequestedLog Inner join [Option] on [Option].OptionID = @mTargetOptionID
where ColourID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

Update MasterPartsLinkTableModelList set ModelID = @mTargetOptionID, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
From MasterPartsLinkTableModelList Inner join [Option] on [Option].OptionID = @mTargetOptionID
where ModelID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT




--print 'step 3 done'



Update ReceiveDetail set CarrierID = @mTargetOptionID, Carrier = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
From ReceiveDetail Inner join [Option] on [Option].OptionID = @mTargetOptionID
where CarrierID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

Update ReceiveDetail set ManufacturerID = @mTargetOptionID, Manufacturer = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
From ReceiveDetail Inner join [Option] on [Option].OptionID = @mTargetOptionID
 where ManufacturerID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

Update ReceiveDetail set ModelID = @mTargetOptionID, Model = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
From ReceiveDetail Inner join [Option] on [Option].OptionID = @mTargetOptionID
where ModelID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

Update ReceiveDetail set ColourID = @mTargetOptionID,  Colour = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
From ReceiveDetail Inner join [Option] on [Option].OptionID = @mTargetOptionID
where ColourID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

Update ReceiveDetail set GradeID = @mTargetOptionID,  Grade = [Option].OptionText, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName
From ReceiveDetail Inner join [Option] on [Option].OptionID = @mTargetOptionID
where GradeID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

--print 'step ReceiveDetail done'


Update ReceiveDetailItem set OptionID = @mTargetOptionID, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName where OptionID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT
--Update ReceiveDetailItem_03 set OptionID = @mTargetOptionID where OptionID = @mSourceID
--SELECT @Rows= @Rows + @@ROWCOUNT
--Update REceiveDetailItem_Archive set OptionID = @mTargetOptionID where OptionID = @mSourceID
--SELECT @Rows= @Rows + @@ROWCOUNT
--Update ReceiveDetailItem_Archive_01 set OptionID = @mTargetOptionID where OptionID = @mSourceID
--SELECT @Rows= @Rows + @@ROWCOUNT
--Update ReceiveDetailItem_Archive_02 set OptionID = @mTargetOptionID where OptionID = @mSourceID
--SELECT @Rows= @Rows + @@ROWCOUNT
--Update ReceiveDetailItem_Deleted_01 set OptionID = @mTargetOptionID where OptionID = @mSourceID
--SELECT @Rows= @Rows + @@ROWCOUNT
Update ReceiveDetailPreReceiveAttribute set OptionID = @mTargetOptionID, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName where OptionID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT
Update ReceiveDetailItemBulk set OptionID = @mTargetOptionID, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName where OptionID = @mSourceID
SELECT @Rows= @Rows + @@ROWCOUNT

--print 'step ReceiveDetailItem done'



Update [Option] set OptionText = 'XX-' + ltrim(rtrim(OptionText)), OptionStatusID = 2, LastUpdateDate = GETDATE(), LastUpdateUser = @mUserName where OptionID = @mSourceID 
-- SELECT @Rows= @Rows + @@ROWCOUNT


Select @mRecordsAffected = @Rows
--print 'step Last done'


End
Go