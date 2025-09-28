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




