/****** Object:  StoredProcedure [dbo].[Utility_LoadAttributeValue_03]    Script Date: 11/09/2017 13:24:31 ******/
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

Declare @mMessage nvarchar(500)
Exec Utility_LoadAttributeValue_04 'Colour','0001','ABBR', 'RED', '0100','jmccomb', @mMessage Output
Print @mMessage



Select * from Question where name = 'Discr Type'
Select * from [option] where questionid = 78

Delete [option] where questionid = 78

*/

Create PROCEDURE [dbo].[Utility_LoadAttributeValue_04]
    
    @mAttributeName nVarchar(20),
    @mAttributeScankey nVarchar(50) = '',
    @mAttributeItemName nVarchar(20) = '',    
    @mAttributeValue nVarchar(50),    
    @mAttributeSeq nVarchar(10) = '',
    @mUserName nVarchar(50),
    @mMessage nvarchar(500) OUTPUT
   
AS
BEGIN
Set NOCOUNT ON
--Select Name from Question where Name = 'Colour'

Select @mMessage = ''
Declare @mStatusID numeric(18)
Declare @mTypeID numeric(18)
Declare @mQuestionID numeric(18)

Select Top 1 @mQuestionID = QuestionID from Question where ltrim(rtrim(Question.Name)) = @mAttributeName
Select Top 1 @mTypeID = OptionTypeID from OptionType where [Type] = 'Other'
Select Top 1 @mStatusID = OptionStatusID from OptionStatus where Status = 'Active'
Select @mQuestionID = isnull(@mQuestionID, -1)
Select @mTypeID = isnull(@mTypeID, -1)
Select @mStatusID = isnull(@mStatusID, -1)
if @mQuestionID < 1 
   begin
   Select @mMessage = 'Question Not found ' + @mAttributeName
   Print 'Question Not found ' + @mAttributeName
   Return 0
   end
if @mTypeID < 1
   begin
   Select @mMessage = 'Type Not found ' + 'Other'  
   Print 'Type Not found ' + 'Other'
   Return 0
   end
if @mStatusID < 1
   begin
   Select @mMessage = 'Status Not found ' + 'Active'  
   Print 'Status Not found ' + 'Active'
   Return 0
   end   


if @mAttributeName = 'Model' or
   @mAttributeName = 'Carrier' or
   @mAttributeName = 'Manufacturer' or
   @mAttributeName = 'Colour'
   begin
   if exists (Select * from [Option] where QuestionID = @mQuestionID and Name = @mAttributeItemName)
     begin
     Select @mMessage = @mAttributeName + ':' + @mAttributeItemName + ' already on file. (ABBR)'   
     Print @mAttributeName + ':' + @mAttributeItemName + ' already on file. (ABBR)'
     Return 0
     End
   end 

   if exists (Select OptionID from [Option] where QuestionID = @mQuestionID and OptionText =  @mAttributeValue)
     begin
     Select @mMessage = @mAttributeName + ':' + @mAttributeValue + ' already on file. (VALUE)'   
     Print @mAttributeName + ':' + @mAttributeValue + ' already on file. (VALUE)'
     Return 0
     End

   


   Select @mMessage = 'Success, Added:' + @mAttributeName + ':' + @mAttributeValue + ' - ABBR:' + @mAttributeItemName
   Print 'Success, Added:' + @mAttributeName + ':' + @mAttributeValue
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
           ,'BulkAdd'
           ,getdate()
           ,'BulkAdd'
           ,'') 
             

declare @mID numeric(18)   
Select @mID  = @@IDENTITY
if len(@mAttributeScankey) < 1
   begin
   Select @mAttributeScankey = 'O' + CONVERT(nvarchar(10),@mID)
   Update [Option] set [ScanKey] = @mAttributeScankey where OptionID = @mID
   end

Return 1

END
