
/****** Object:  StoredProcedure [dbo].[ADD_ScanComandLookupAttributeList]    Script Date: 04/19/2020 18:51:41 ******/
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
Declare @mMessage nvarchar(4000)
Declare @mScanComandLookupAttributeListID  numeric (18,0)
exec ADD_ScanComandLookup 'JimTextUPC1', 'jmccomb',@mScanComandLookupAttributeListID output, @mMessage output
Print @mMessage
Print mScanComandLookupAttributeListID


1444,1445,1943

Declare @mMessage nvarchar(4000)
Declare @mScanComandLookupAttributeListID  numeric (18,0) 
Exec ADD_ScanComandLookupAttributeList 2, 1445, 'valuetext', 11, 'jmccomb', @mScanComandLookupAttributeListID output, @mMessage output
Print @mMessage
Print @mScanComandLookupAttributeListID


Select * from ScanComandLookUp
Select * from ScanComandLookupAttributeList

Select * from [option] o inner join Question Q on O.QuestionID = Q.QuestionID where Q.Name = 'Model'

*/

--Drop Procedure ADD_MasterCarrierManufacturerUPCLookup
--GO


CREATE PROCEDURE [dbo].[ADD_ScanComandLookupAttributeList]


      @mScanComandLookupID numeric (18, 0),
      @mOptionID numeric (18, 0),
      @mSetValue nVarchar(250),
      @mSequence int, 
      @mUserName nvarchar(50) = '',
      @mScanComandLookupAttributeListID numeric (18,0) output,      
      @mMessage nVarchar(4000) output

AS
BEGIN
	SET NOCOUNT ON;

Declare @mScanCode nvarchar(250)
Declare @mCommandString nvarchar(250)
Declare @mAttributeQuestionName nvarchar(20)
Select @mCommandString = '';
Select @mScanComandLookupAttributeListID = -1


Select @mMessage = ''
-- see if the UPC is already there.
if Not exists (select * from ScanComandLookup where ScanComandLookupID = @mScanComandLookupID and Status = 'Active')
   begin
   Select @mMessage = 'Error: ScancodeID Not Found:' + CONVERT(nvarchar(20), @mScanComandLookupID)
   return 0
   end
   
if Not exists (select * from [Option] O inner join OptionStatus S on O.OptionStatusID = S.OptionStatusID where OptionID = @mOptionID and Status = 'Active')
   begin
   Select @mMessage = 'Error: OptionID Not found:' + CONVERT(nvarchar(20), @mOptionID)
   return 0
   end

Select @mAttributeQuestionName = Q.Name 
  from Question Q 
 inner join [Option] O on Q.QuestionID = O.QuestionID 
 Where OptionID = @mOptionID

if exists (select * from ScanComandLookupAttributeList where ScanComandLookupID = @mScanComandLookupID 
                                                             and OptionID = @mOptionID and Status = 'Active')
   begin
   Select @mMessage = 'Error: Attribute link already on file:' + CONVERT(nvarchar(20), @mScanComandLookupID)
   return 0
   end  
       
if exists (

Select C.*
  from Question Q 
 inner join [Option] O on Q.QuestionID = O.QuestionID 
 inner join ScanComandLookupAttributeList C on C.OptionID = O.OptionID
 WHere ScanComandLookupID = @mScanComandLookupID and C.Status = 'Active' and Q.Name = @mAttributeQuestionName)
   begin
   Select @mMessage = 'Error: Question already on file:' + @mAttributeQuestionName
   return 0
   end    
   
if (@mSetValue = 'SCAN')
    begin
    Select @mScanCode = ScanCode from ScanComandLookup where ScanComandLookupID = @mScanComandLookupID and Status = 'Active'
    Exec  [Get_ScanComandLookupLink]  @mOptionID, @mScanCode, @mCommandString output    
    end
else
    begin
    Exec  [Get_ScanComandLookupLink]  @mOptionID, @mSetValue, @mCommandString output    
    end    

-- Print 'Lookup:' + @mScanCode
INSERT INTO [dbo].[ScanComandLookupAttributeList]
           ([ScanComandLookupID]
           ,[OptionID]
           ,[Status]
           ,[SetValue]
           ,[Sequence]
           ,[CommandString]
           ,[CreateDate]
           ,[CreateUser]
           ,[LastUpdateDate]
           ,[LastUpdateUser])
     VALUES
           (@mScanComandLookupID
           ,@mOptionID
           ,'Active'
           ,@mSetValue
           ,@mSequence
           ,@mCommandString
           ,GETDATE()
           ,@mUserName
           ,GETDATE()
           ,@mUserName)


Select @mScanComandLookupAttributeListID = @@IDENTITY
Select @mMessage = 'Success: Scancode Attribute Added: ' + CONVERT(nvarchar(10), @mScanComandLookupAttributeListID)
return 0

END

Go

