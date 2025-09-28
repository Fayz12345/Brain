

/****** Object:  Trigger [dbo].[Log_Item_Change]    Script Date: 06/21/2017 13:53:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create TRIGGER [dbo].[Log_Item_Change]
 ON [dbo].[ReceiveDetailItem]
  AFTER UPDATE, INSERT
AS
BEGIN
Set NOCOUNT ON
--// If the Attribute is the "Location" attribute, we want to record the new one to the log.

Declare @mReceiveDetailID numeric(18)
Declare @mOptionID numeric(18)
Declare @mFrom nvarchar(50)
Declare @mTo nvarchar(50)
Declare @mBin nvarchar(50)

Select @mOptionID = optionID, @mReceiveDetailID = ReceiveDetailID from inserted
Select @mOptionID = isnull(@mOptionID, -1)


IF (SELECT COUNT(*) FROM inserted) > 0 and (SELECT COUNT(*) FROM Deleted) < 1
 BEGIN
  if exists(Select [option].QuestionID from [Option] 
             inner join Question on Question.QuestionID = [Option].Questionid 
             where Question.Name = 'Location' and [option].optionID = @mOptionID)
     begin
         Select @mBin = ReceiveDetailItem.Value 
           FROM ReceiveDetailItem 
          INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
          INNER JOIN  Question ON [Option].QuestionID = Question.QuestionID
          WHERE (ReceiveDetailItem.ReceiveDetailID = @mReceiveDetailID) AND 
                (ReceiveDetailItem.Version = 0) AND (Question.Name = N'Bin')
         Select @mTo = OptionText from [Option] where OptionID = @mOptionID
         Select @mBin = ISNULL(@mBin,'')
         Select @mTo = ISNULL(@mTo,'')
         Select @mFrom = ISNULL(@mFrom,'')                  
         Insert ReceiveDetailXBinXLocationLog (ReceiveDetailID, BinNumber, LocationFrom, LocationTo, CreateDate, CreateUser) 
                                        Select ReceiveDetailID, @mBin,'New',@mTo, getdate(), LastUpdateUser from inserted
     end
     Insert ReceiveDetailItem_Archive_02 (DateMoved, ReceiveDetailItemID, ReceiveHeaderID, ReceiveDetailID, Version, OptionID, Value, ReceiveDate, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser)
     Select getdate(), inserted.ReceiveDetailItemID, inserted.ReceiveHeaderID, inserted.ReceiveDetailID, inserted.Version, inserted.OptionID, inserted.Value, inserted.ReceiveDate, inserted.CreateDate, inserted.CreateUser, inserted.LastUpdateDate, inserted.LastUpdateUser from inserted
 END
 
IF (SELECT COUNT(*) FROM Deleted) > 0
 BEGIN
  if exists(Select [option].QuestionID from [Option] inner join Question on Question.QuestionID = [Option].Questionid where Question.Name = 'Location' and [option].optionID = @mOptionID)
     begin
         Select @mBin = ReceiveDetailItem.Value 
           FROM ReceiveDetailItem 
          INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
          INNER JOIN  Question ON [Option].QuestionID = Question.QuestionID
          WHERE (ReceiveDetailItem.ReceiveDetailID = @mReceiveDetailID) AND 
                (ReceiveDetailItem.Version = 0) AND (Question.Name = N'Bin')     
         Select @mTo = OptionText from [Option] where OptionID = @mOptionID     
         Select @mOptionID = optionID from Deleted          
         Select @mFrom = OptionText from [Option] where OptionID = @mOptionID
         Select @mBin = ISNULL(@mBin,'')
         Select @mTo = ISNULL(@mTo,'')
         Select @mFrom = ISNULL(@mFrom,'')             
         Insert ReceiveDetailXBinXLocationLog (ReceiveDetailID, BinNumber, LocationFrom, LocationTo, CreateDate, CreateUser) 
                                        Select ReceiveDetailID, @mBin,@mFrom,@mTo, getdate(), LastUpdateUser from inserted     
     end
     -- if the OptionID and Value are the same between the inserted and Deleted, then we don't want to add it.
     
     Insert ReceiveDetailItem_Archive_02 (DateMoved, ReceiveDetailItemID, ReceiveHeaderID, ReceiveDetailID, Version, OptionID, Value, ReceiveDate, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser)
     Select getdate(), inserted.ReceiveDetailItemID, inserted.ReceiveHeaderID, inserted.ReceiveDetailID, inserted.Version, inserted.OptionID, inserted.Value, inserted.ReceiveDate, inserted.CreateDate, inserted.CreateUser, inserted.LastUpdateDate, inserted.LastUpdateUser 
     from inserted
     Inner join Deleted on inserted.ReceiveDetailItemID = Deleted.ReceiveDetailItemID 
     Where inserted.Value <> Deleted.Value
 END
	
END




/****** Object:  Trigger [dbo].[ProcessStepChange_B]    Script Date: 06/21/2017 13:50:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

Create TRIGGER [dbo].[ProcessStepChange_B]
   ON  [dbo].[ReceiveDetail]
  AFTER UPDATE, INSERT
AS 
BEGIN


	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;
	
Declare @ISFTransactionDirective smallint
	
Declare @mReceiveDetailID numeric(18)
Declare @mUserName nvarchar(50)
Declare @List Table (ReceiveDetailID numeric(18))
Declare @mS nvarchar(1)
Declare @mL nvarchar(1)
Declare @mC nvarchar(1)
Select @mS = ' '
Select @mL = ' '
Select @mC = ' '

    
IF (SELECT COUNT(*) FROM inserted) > 0 and (SELECT COUNT(*) FROM Deleted) < 1
 BEGIN
 
 
     BEGIN TRY
     
     
        -- RAISERROR with severity 11-19 will cause execution to 
        -- jump to the CATCH block.
     --RAISERROR ('Invalid Sku.', -- Message text.
     --           16, -- Severity.
     --           1 -- State.
     --           );
                   
    Insert into ReceiveDetailVersionChangeLog  (ReceiveDetailID,Version,CreateDate,CreateUser)
    Select ReceiveDetailID,Version,LastUpdateDate,LastUpdateUser from inserted 
    
    Insert into ReceiveDetailSKUChangeLog (ReceiveDetailID, SKU, CreateDate,CreateUser)
    Select ReceiveDetailID, SKU, getdate(),LastUpdateUser from inserted 
    
    Insert into ReceiveDetailIFSLocationLog (ReceiveDetailID, IFSLocation, MiscText, CreateDate,CreateUser)
    Select ReceiveDetailID, IFSLocation, '', getdate(),LastUpdateUser from inserted 
    
    Insert into ReceiveDetailConditionChangeLog (ReceiveDetailID, IFS_Condition, CreateDate,CreateUser)
    Select ReceiveDetailID, IFSCondition, getdate(),LastUpdateUser from inserted 
    
    Insert into ReceiveDetailIFSInOutLog ([ReceiveDetailID],[NewisIFSLocked],[NewIFSSite],[NewIFSProject],[NewSku],[NewLocation],[NewCondition],[NewCreatedDate],[NewCreateUser])
    Select ReceiveDetailID, isIFSLocked, '','',SKU, IFSLocation, IFSCondition, getdate(),LastUpdateUser from inserted 
                   
                   
    END TRY
    BEGIN CATCH
        rollback transaction
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;
    
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Use RAISERROR inside the CATCH block to return error
        -- information about the original error that caused
        -- execution to jump to the CATCH block.
        RAISERROR (@ErrorMessage, -- Message text.
                   @ErrorSeverity, -- Severity.
                   @ErrorState -- State.
                   );
    END CATCH;
  
   RETURN
 END

IF UPDATE(Version)
   BEGIN
   Insert into ReceiveDetailVersionChangeLog  (ReceiveDetailID,Version,CreateDate,CreateUser)
   Select ReceiveDetailID,Version,LastUpdateDate,LastUpdateUser from inserted 
   --RETURN
   END

IF UPDATE(SKU)
   BEGIN
    Select @mS = 'S'
   Insert into ReceiveDetailSKUChangeLog (ReceiveDetailID, SKU, CreateDate,CreateUser)
   Select ReceiveDetailID, SKU, getdate(),LastUpdateUser from inserted 
   where not inserted.SKU is null   
   --RETURN
   END
   
IF UPDATE(IFSLocation)
   BEGIN
    Select @mL = 'L'
    Insert into ReceiveDetailIFSLocationLog (ReceiveDetailID, IFSLocation, MiscText, CreateDate,CreateUser)
    Select ReceiveDetailID, IFSLocation, '', GETDATE(),LastUpdateUser from inserted 
    where not inserted.IFSLocation is null    
   --RETURN
   END

IF UPDATE(IFSCondition)
   BEGIN
    Select @mC = 'C'
    Insert into ReceiveDetailConditionChangeLog (ReceiveDetailID, IFS_Condition, CreateDate,CreateUser)
    Select inserted.ReceiveDetailID, inserted.IFSCondition, getdate(),inserted.LastUpdateUser from inserted 
    where not inserted.IFSCondition is null 
   END
   
if UPDATE(ProcessID)
   BEGIN
   Declare @mProcessID numeric(18)
   Select @mProcessID = ProcessID from inserted
   Declare @mProcessName nvarchar(20)
   Select @mProcessName = Name from Process where ProcessID = @mProcessID

   Insert into ReceiveDetailProcessLog (ReceiveDetailID,ProcessID,ProcessText,MiscText,CreateDate,CreateUser)
   Select ReceiveDetailID,ProcessID,@mProcessName,'',LastUpdateDate,LastUpdateUser
   from inserted
   END
   
   
   
 IF UPDATE(SKU)or UPDATE(IFSLocation) or UPDATE(IFSCondition)
 BEGIN
    Declare @FromSku nvarchar(50),
            @FromLocation [nvarchar](50),
            @FromCondition [nvarchar](50),
            @ToSku nvarchar(50),
            @ToLocation [nvarchar](50),
            @ToCondition [nvarchar](50)
 
    
    While exists (Select ReceiveDetailID from inserted where not ReceiveDetailID in (Select ReceiveDetailID from @List))
    begin
    Select Top 1 @mReceiveDetailID = ReceiveDetailID, @mUserName = LastUpdateUser, 
                 @ISFTransactionDirective = isnull(ISFTransactionDirective, -1),
                 @ToSku = inserted.Sku,
                 @ToLocation = inserted.IFSLocation,
                 @ToCondition = inserted.IFSCondition
           from inserted where not ReceiveDetailID in (Select ReceiveDetailID from @List)
    insert @List (ReceiveDetailID) values (@mReceiveDetailID)
    Select Top 1 @FromSku = Deleted.Sku,
                 @FromLocation = Deleted.IFSLocation,
                 @FromCondition = Deleted.IFSCondition
           from Deleted where ReceiveDetailID = @mReceiveDetailID
           
           

    if @ISFTransactionDirective = -1
        Select @ISFTransactionDirective = [dbo].[GetIFSDirective]('Normal',-1)
  
    Update ReceiveDetail set ISFTransactionDirective = -1 where ReceiveDetailID = @mReceiveDetailID
    
     Insert ReceiveDetailTriggerLog (ReceiveDetailID, MiscDesc, CreateDate, CreateUser)
    Values (@mReceiveDetailID, 'SKU/LOC/COND change: Dir=' + convert(nvarchar(20),@ISFTransactionDirective), GETDATE(), @mUserName)           
       
    
    exec IFS_GenerateInvtTran_B @mReceiveDetailID, @ISFTransactionDirective , 
         @FromSku, @FromLocation, @FromCondition, @ToSku, @ToLocation, @ToCondition,
         @mUserName, -1,''
    --exec IFS_GenerateInvtTran @mReceiveDetailID, @ISFTransactionDirective , @mS, @mL, @mC, @mUserName, -1,''    
    end
END
    -- Insert statements for trigger here

END




















