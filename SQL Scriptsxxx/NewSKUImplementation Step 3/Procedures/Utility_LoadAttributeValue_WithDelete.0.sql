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


