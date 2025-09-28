
/****** Object:  StoredProcedure [dbo].[Utility_LoadAttributeValue_04]    Script Date: 10/16/2019 11:57:51 ******/
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

Exec Utility_LoadAttributeValue_03 'Discr Type','Buyers Remorse','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Type','DOA Accessories','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Type','DOA Hardware','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Type','Extended Warranty','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Type','Hardware','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Type','In Warranty','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Type','Loaners','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Type','Misc','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Type','Out of Warranty','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Type','Remix/Recall','jmccomb'

Exec Utility_LoadAttributeValue_03 'Discr OutCome','Information Received','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr OutCome','Moved to Non Sell','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr OutCome','Printed Paper Work','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr OutCome','Recycled','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr OutCome','Resolved by GMP','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr OutCome','Shipped Back to Store','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr OutCome','Shipped to Head Office','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr OutCome','Shipped/Redirected to Correct Location','jmccomb'

Exec Utility_LoadAttributeValue_03 'Discr Div','SG','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Div','TB','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Div','TM','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Div','WE','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Div','WW','jmccomb'

exec Utility_LoadAttributeValue_03 'Discr Desc','Apple ID Lock','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Box IMEI Transferred not Phone','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Customer Abuse','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Extra Item','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','IMEI Different than Paper Work','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Invalid/No Waybill','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Missing Item','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','MSC 1yr Warranty Period','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Name on POP Different','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','No POP/POR/PO','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','No Response to the Quote','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','No Service Request','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','No Stock Transfer','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Non Glentel Product','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Not in Non Sell','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Not Part of Remix','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Not picked up from Store','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Past Return Period','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Quote Accepted by the Customer','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Quote Rejected by the Customer','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Service Provider 1st year warranty period','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Service Request Incomplete','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Shipped to Wrong Location','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Wrong Item','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Wrong/No Product in Case','jmccomb'

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
