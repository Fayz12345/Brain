/****** Object:  StoredProcedure [dbo].[Utility_LoadAttributeValue_02]    Script Date: 08/03/2017 21:18:57 ******/
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


Exec Utility_LoadAttributeValue 'Colour','Black'
Exec Utility_LoadAttributeValue 'Colour','Blue'
Exec Utility_LoadAttributeValue 'Colour','Brown'
Exec Utility_LoadAttributeValue 'Colour','Coral'
Exec Utility_LoadAttributeValue 'Colour','Fushia'
Exec Utility_LoadAttributeValue 'Colour','Gold'
Exec Utility_LoadAttributeValue 'Colour','Green'
Exec Utility_LoadAttributeValue 'Colour','Greg'
Exec Utility_LoadAttributeValue 'Colour','Grey'
Exec Utility_LoadAttributeValue 'Colour','Gun Metal'
Exec Utility_LoadAttributeValue 'Colour','Orange'
Exec Utility_LoadAttributeValue 'Colour','Pink'
Exec Utility_LoadAttributeValue 'Colour','Purple'
Exec Utility_LoadAttributeValue 'Colour','Red'
Exec Utility_LoadAttributeValue 'Colour','Red '
Exec Utility_LoadAttributeValue 'Colour','Red/Black'
Exec Utility_LoadAttributeValue 'Colour','Silver'
Exec Utility_LoadAttributeValue 'Colour','Silver/Grey'
Exec Utility_LoadAttributeValue 'Colour','violet'
Exec Utility_LoadAttributeValue 'Colour','White'
Exec Utility_LoadAttributeValue 'Colour','White/Black'
Exec Utility_LoadAttributeValue 'Colour','White/Purple'





*/

Alter PROCEDURE [dbo].[Utility_LoadAttributeValue_WithDelete]
    @mAttributeName nVarchar(20),
    @mDelete int, 
    @mAttributeScankey nVarchar(50),
    @mAttributeItemName nVarchar(20),
    @mAttributeValue nVarchar(50),
    @mAttributeSeq nVarchar(10),
    @mUserName nVarchar(50),
    @mReturnMessage nvarchar(50) Output
   
AS
BEGIN
Set NOCOUNT ON
--Select Name from Question where Name = 'Colour'

Declare @mStatusID numeric(18)
Declare @mStatusDeleteID numeric(18)
Declare @mTypeID numeric(18)
Declare @mQuestionID numeric(18)
Declare @mOptionID numeric(18)

Select Top 1 @mQuestionID = QuestionID from Question where ltrim(rtrim(Question.Name)) = @mAttributeName
Select Top 1 @mTypeID = OptionTypeID from OptionType where [Type] = 'Other'
Select Top 1 @mStatusID = OptionStatusID from OptionStatus where Status = 'Active'
Select Top 1 @mStatusDeleteID = OptionStatusID from OptionStatus where Status = 'Inactive'
Select @mQuestionID = isnull(@mQuestionID, -1)
Select @mTypeID = isnull(@mTypeID, -1)
Select @mStatusID = isnull(@mStatusID, -1)
if @mQuestionID < 1 
   begin
   Select @mReturnMessage = 'Error: Question Not found ' + @mAttributeName
   Print 'Question Not found ' + @mAttributeName
   Return 0
   end
if @mTypeID < 1
   begin
   Select @mReturnMessage = 'Error: Type Not found ' + 'Other'
   Print 'Type Not found ' + 'Other'
   Return 0
   end
if @mStatusID < 1
   begin
   Select @mReturnMessage = 'Error: Status Not found ' + 'Active'
   Print 'Status Not found ' + 'Active'
   Return 0
   end   
   

Select @mOptionID = OptionID from [Option] where 1 = 1
                                           and QuestionID = @mQuestionID
                                           and OptionStatusID = @mStatusID
                                           and (Name = @mAttributeItemName
                                            or ScanKey = @mAttributeScankey
                                            or OptionText = @mAttributeValue)


-- Do we delete?
if (@mDelete > 0 and isnull(@mOptionID,-1) > 0)
    begin
    Update [Option] set OptionStatusID = @mStatusDeleteID, [LastUpdateDate]= GETDATE(), [LastUpdateUser] = @mUserName 
     where OptionID = @mOptionID
    Select @mReturnMessage = 'Updated: Status Set to Inactive '
    Print  'Status Set to Inactive '
    Return 0    
    end

-- Do we update?
if (isnull(@mOptionID,-1) > 0)
    begin
    Update [Option] set OptionStatusID = @mStatusID
         , [ScanKey] = @mAttributeScankey
         , [OptionText] = @mAttributeValue
         , [Name] = @mAttributeItemName
         , [Sequence] =  @mAttributeSeq                          
         , [LastUpdateDate]= GETDATE(), [LastUpdateUser] = @mUserName
     where OptionID = @mOptionID
    Select @mReturnMessage = 'Updated:'
    Print  'Attribute Updated'
    Return 0    
    end

-- Do we Add New?   
  
   
if Not Exists(Select OptionID from [Option] where QuestionID = @mQuestionID and OptionText =  @mAttributeValue )
   begin
   Print 'Insert:' + @mAttributeName + ':' + @mAttributeValue
   INSERT INTO [Option]
              ([ScanKey],[MacroKey]
              ,[OptionStatusID]
              ,[OptionTypeID]
              ,[OptionText]
              ,[HelpText]
              ,[QuestionID]
              ,[Name]
              ,[Sequence]
              ,[CreateDate]
              ,[CreateUser]
              ,[LastUpdateDate]
              ,[LastUpdateUser]
              ,[MicroKey])
     VALUES
           (@mAttributeScankey,''
           ,@mStatusID
           ,@mTypeID
           ,@mAttributeValue
           ,@mAttributeValue
           ,@mQuestionID
           ,@mAttributeItemName
           ,1
           ,getdate()
           ,@mUserName
           ,getdate()
           ,@mUserName
           ,'')  
    Select @mReturnMessage = 'Inserted'            
   end

Return 1

END

GO




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


/****** Object:  StoredProcedure [dbo].[Utility_ReplaceOptionAttributeID_GO]    Script Date: 07/31/2017 11:37:14 ******/
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



exec Utility_ReplaceOptionAttributeID_GO 842,843

exec Utility_ReplaceOptionAttributeID_GO 2443,1872
exec Utility_ReplaceOptionAttributeID_GO 4569,4450
exec Utility_ReplaceOptionAttributeID_GO 2498,1722
exec Utility_ReplaceOptionAttributeID_GO 1757,1445
exec Utility_ReplaceOptionAttributeID_GO 2499,1759
exec Utility_ReplaceOptionAttributeID_GO 1768,1444
exec Utility_ReplaceOptionAttributeID_GO 2524,2520
exec Utility_ReplaceOptionAttributeID_GO 2568,2564
exec Utility_ReplaceOptionAttributeID_GO 2569,2565
exec Utility_ReplaceOptionAttributeID_GO 2566,1784
exec Utility_ReplaceOptionAttributeID_GO 2570,1784
exec Utility_ReplaceOptionAttributeID_GO 2571,2567
exec Utility_ReplaceOptionAttributeID_GO 5841,5840
exec Utility_ReplaceOptionAttributeID_GO 2383,1869
exec Utility_ReplaceOptionAttributeID_GO 2476,2475
exec Utility_ReplaceOptionAttributeID_GO 3272,2648
exec Utility_ReplaceOptionAttributeID_GO 6160,5995
exec Utility_ReplaceOptionAttributeID_GO 2421,1840
exec Utility_ReplaceOptionAttributeID_GO 2342,1891
exec Utility_ReplaceOptionAttributeID_GO 3359,3315
exec Utility_ReplaceOptionAttributeID_GO 2481,1899
exec Utility_ReplaceOptionAttributeID_GO 2795,2794
exec Utility_ReplaceOptionAttributeID_GO 2731,1771
exec Utility_ReplaceOptionAttributeID_GO 3235,2627

exec Utility_ReplaceOptionAttributeID_GO 2570, 2566

2566
2570



*/

ALTER PROCEDURE [dbo].[Utility_ReplaceOptionAttributeID_GO]
        @mSourceID numeric(18),
        @mTargetOptionID numeric(18),
        @mUserName nvarchar(20)

AS
BEGIN
SET NOCOUNT ON;


declare @mRecords int
declare @mTotalRecords int
Select @mTotalRecords = 0
exec Utility_ReplaceOptionAttributeID @mSourceID, @mTargetOptionID, @mUserName, @mRecords Output
Print 'Rows Affected:' + convert(nvarchar(20), @mRecords) + ' Source:' + convert(nvarchar(20), @mSourceID) + ' Target:' + convert(nvarchar(20), @mTargetOptionID)
Select @mTotalRecords = @mTotalRecords + isnull(@mRecords,0)


End
GO















