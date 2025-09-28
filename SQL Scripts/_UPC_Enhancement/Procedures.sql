
/****** Object:  StoredProcedure [dbo].[ADD_MasterCarrierManufacturerUPCLookup]    Script Date: 04/18/2020 13:09:39 ******/
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
exec ADD_ScanComandLookup 'JimTextUPC1', 'jmccomb', @mMessage output
Print @mMessage


Select * from ScanComandLookup

*/

--Drop Procedure ADD_MasterCarrierManufacturerUPCLookup
--GO


Alter PROCEDURE [dbo].[ADD_ScanComandLookup]

      @mScanCode nVarchar(250),
      @mUserName nvarchar(50) = '',
      @mMessage nVarchar(4000) output

AS
BEGIN
	SET NOCOUNT ON;


Select @mMessage = ''
-- see if the UPC is already there.
if exists (select * from ScanComandLookup where [ScanCode] = @mScanCode and Status = 'Active')
   begin
   Select @mMessage = 'Warning: Scancode already on file'
   return 0
   end

-- Verify there is a proper combo

--Declare @mLookupID numeric(18,0)
--Select @mLookupID = MasterCarrierManufacturerLookupID from MasterCarrierManufacturerLookup A
--                  Inner join MasterCarrierManufacturerStatus B on A.StatusID = B.MasterCarrierManufacturerStatusID
--                       where OptionCarrierID = @mCarrierID 
--                         and OptionColourID = @mcolourID 
--                         and OptionManufacturerID = @mManufacturerID 
--                         and OptionModelID = @mModelID
--                         and Status = 'Active'
--Select @mLookupID = ISNULL(@mLookupID, -1)

--if (@mLookupID < 1)
--   begin
--   Select @mMessage = 'Error: Invalid Attribute Combo'
--   return 0
--   end
   
-- Print 'Lookup:' + @mScanCode
   
INSERT INTO ScanComandLookup
           (ScanCode, Status ,[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
    VALUES (@mScanCode,'Active',GETDATE(),@mUserName,GETDATE(),@mUserName)

Select @mMessage = 'Success: Scancode Code Added: ' + CONVERT(nvarchar(10), @@IDENTITY)
return 0

END


GO




/****** Object:  StoredProcedure [dbo].[DEL_MasterCarrierManufacturerUPCLookup]    Script Date: 04/18/2020 13:17:00 ******/
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
exec DEL_ScanComandLookup 'JimTextUPC1' , 'jmccomb', @mMessage output
Print @mMessage

Select * from ScanComandLookup


Drop Procedure DEL_MasterCarrierManufacturerUPCLookup
GO



*/

CREATE PROCEDURE [dbo].[DEL_ScanComandLookup]

      @mScanCode nVarchar(250),
      @mUserName nvarchar(50) = '',
      @mMessage nVarchar(4000) output

AS
BEGIN
	SET NOCOUNT ON;


Select @mMessage = ''
-- see if the UPC is already there.
if NOT exists (select * from ScanComandLookup where [ScanCode] = @mScanCode and Status = 'Active')
   begin
   Select @mMessage = 'Warning: Scancode NOT on file'
   return 0
   end

-- Verify there is a proper combo
Update ScanComandLookup Set 
       LastUpdateDate = GETDATE(), 
       LastUpdateUser = @mUserName, 
       Status = 'Deleted' 
       where [ScanCode] = @mScanCode and Status = 'Active'

Select @mMessage = 'Success: Scancode Code Deleted: '
return 0

END


GO




/****** Object:  StoredProcedure [dbo].[DEL_MasterCarrierManufacturerUPCLookup]    Script Date: 04/18/2020 13:17:00 ******/
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
exec DEL_ScanComandLookup 'JimTextUPC1' , 'jmccomb', @mMessage output
Print @mMessage

Select * from ScanComandLookup


Drop Procedure DEL_MasterCarrierManufacturerUPCLookup
GO



*/

CREATE PROCEDURE [dbo].[DEL_ScanComandLookupAttributeList]

      @ScanComandLookupAttributeListID [numeric](18, 0) ,
      @mUserName nvarchar(50) = '',
      @mMessage nVarchar(4000) output

AS
BEGIN
	SET NOCOUNT ON;


Select @mMessage = ''
-- see if the UPC is already there.
if NOT exists (select * from ScanComandLookupAttributeList where ScanComandLookupAttributeListID = @ScanComandLookupAttributeListID)
   begin
   Select @mMessage = 'Warning: Scancode Attribute NOT on file'
   return 0
   end

-- Verify there is a proper combo
Update ScanComandLookupAttributeList Set 
       LastUpdateDate = GETDATE(), 
       LastUpdateUser = @mUserName, 
       Status = 'Deleted' 
       where ScanComandLookupAttributeListID = @ScanComandLookupAttributeListID

Select @mMessage = 'Success: Scancode Attribute Deleted: '
return 0

END


GO



/****** Object:  StoredProcedure [dbo].[Get_MasterCarrierManufacturerUPCLookupChain]    Script Date: 04/18/2020 15:29:36 ******/
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
exec [Get_ScanComandLookupChain] 'JimTextUPC1', @mMessage output
Print @mMessage
*/

Create PROCEDURE [dbo].[Get_ScanComandLookupChain]

      @mScanCode nVarchar(250),
      @mMessage nVarchar(4000) output

AS
BEGIN
	SET NOCOUNT ON;

Declare @ScanCodeID numeric(15, 0)
Select @ScanCodeID = -1
select @ScanCodeID = ScanComandLookupID from ScanComandLookup where ScanCode = @mScanCode and Status = 'Active'
Select @ScanCodeID = ISNULL(@ScanCodeID, -1)
Select @mMessage = ''
-- see if the UPC is already there.
if @ScanCodeID < 1
   begin
   Select @mMessage = ''        -- Leave it empty so calling procedure will see no results and move on to the next "assumption".
   return 0
   end
   
 
Select @mMessage = @mMessage + CommandString 
  from vwScanComandLookupChain A
 where A.ScanComandLookupID = @ScanCodeID
 Order by ChainSequence, OptionSequence, QuestionSequence     
   
--Select @mMessage = @mMessage + CommandString 
--  from ScanComandLookupAttributeList A
--  Inner Join [Option] B on A.OptionID = B.OptionID
--  Inner Join [Question] C on B.QuestionID = C.QuestionID
-- where A.ScanComandLookupID = @ScanCodeID order by A.Sequence, C.Sequence
   
  
------------------------------------------------     
 
return 0

END




GO



/****** Object:  StoredProcedure [dbo].[Get_ScanComandLookupLink]    Script Date: 04/18/2020 17:55:04 ******/
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
exec [Get_ScanComandLookupLink] 1444, 'xx', @mMessage output
Print @mMessage
exec [Get_ScanComandLookupLink] 2226, 'JimTextUPC2', @mMessage output
Print @mMessage
*/

CREATE PROCEDURE [dbo].[Get_ScanComandLookupLink]

      @OptionID numeric(18),
      @SetTextValue nvarchar(25),
      @mMessage nVarchar(4000) output

AS
BEGIN
	SET NOCOUNT ON;
declare @mReturnText nvarchar(4000)
declare @macroKey nchar(2)
Declare @mReturnMessage nvarchar(200)
Declare @mTable nvarchar(20)
Declare @mID nvarchar(20)
--Declare @mValue nvarchar(50)
Declare @mControlType nvarchar(2)
Select @mMessage = ''


--Select @mValue = ''
Select @mTable = ''
Select @mID = ''
Select @mControlType = ''
Select @mReturnMessage = ''
Select @macroKey = ''
Select @mReturnText = ''

Select @mReturnMessage = Description + ':' + OptionText
     , @mTable = 'Option'
     , @macroKey = [Option].MacroKey
     , @mID = convert(nvarchar(20), [Option].OptionID)
     , @mControlType = Case when QuestionType.Type = 'Dropdown' then 'DD'
                            when QuestionType.Type = 'CheckBox' then 'CB'
                            when QuestionType.Type = 'RadialButton' then 'RD'   
                            when QuestionType.Type = 'Calendar' then 'CA'                   
                            else 'TX' END
      From [Option] 
     Inner Join Question on Question.QuestionID = [Option].QuestionID
     INNER JOIN QuestionType ON Question.QuestionTypeID = QuestionType.QuestionTypeID     
     where OptionID = @OptionID
     

Select @mReturnMessage = isnull(@mReturnMessage, '')
Select @mReturnText = @mTable + ':' + @mID + ':' + @mReturnMessage + ':' + @macroKey + ':' + @mControlType + ';'  
  
    
if @mControlType = 'TX'
   begin
   Select @mReturnText = @mTable + ':' + @mID + ':' + @mReturnMessage + ':' + @macroKey + ':' + @mControlType + ':' + @SetTextValue + ';'   
   end 
  
Select @mMessage = @mReturnText

return 0

END


/****** Object:  StoredProcedure [dbo].[ADD_MasterCarrierManufacturerUPCLookup]    Script Date: 04/15/2020 18:08:49 ******/
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
exec InsertReceiveDetailItemAttributeList 3468,139,'TX_236;ddddddddd,CB_182;1,TX_240;dddddddddd,TX_230;dddddddddd,RD_229;1,CB_179;1,TX_226;dddddddd,DD_308;1,DD_210;1,DD_148;1,CB_176;1,TX_223;07/22/2011,RD_506;1,DD_503;1,DD_253;1', 'jmccomb'
*/

CREATE PROCEDURE [dbo].[Search_MasterCarrierManufacturerUPCLookup]

      @mUPC nVarchar(250),
      @mCarrierID numeric(18) output,
      @mManufacturerID numeric(18) output,
      @mModelID numeric(18) output,
      @mcolourID numeric(18) output,
      @mMessage nVarchar(4000) output

AS
BEGIN
	SET NOCOUNT ON;


Select @mMessage = ''
Select @mCarrierID = -1
Select @mManufacturerID = -1
Select @mModelID = -1
Select @mcolourID = -1


-- see if the UPC is already there.
if not exists (select * from MasterCarrierManufacturerUPCLookup where UPC = @mUPC and Status = 'Active')
   begin
   Select @mMessage = 'Error: UPC NOT on file'
   return 0
   end

Select @mCarrierID = OptionCarrierID, 
       @mcolourID = OptionColourID, 
       @mManufacturerID = OptionManufacturerID,
       @mModelID = OptionModelID
       From MasterCarrierManufacturerUPCLookup A
       Inner Join MasterCarrierManufacturerLookup B on A.MasterCarrierManufacturerLookupID = B.MasterCarrierManufacturerLookupID
       where A.UPC = @mUPC and Status = 'Active'
       
Select @mCarrierID = ISNULL(@mCarrierID, -1)
Select @mManufacturerID = ISNULL(@mManufacturerID, -1)
Select @mModelID = ISNULL(@mModelID, -1)
Select @mcolourID = ISNULL(@mcolourID, -1)

Select @mMessage = 'Success: UPC Code Found: ' + CONVERT(nvarchar(10), @@IDENTITY)
return 0

END


GO



--/****** Object:  StoredProcedure [dbo].[Search_MasterCarrierManufacturerUPCLookup]    Script Date: 04/16/2020 13:43:53 ******/
--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO



---- =============================================
---- Author:		<Author,,Name>
---- Create date: <Create Date,,>
---- Description:	<Description,,>
---- =============================================

--/*
--Declare @mMessage nvarchar(4000)
--exec [Get_MasterCarrierManufacturerUPCLookupChain] 'JimTextUPC2', @mMessage output
--Print @mMessage
--*/

--Alter PROCEDURE [dbo].[Get_MasterCarrierManufacturerUPCLookupChain]

--      @mUPC nVarchar(250),
--      @mMessage nVarchar(4000) output

--AS
--BEGIN
--	SET NOCOUNT ON;

--declare @mCarrierID numeric(18)
--declare @mManufacturerID numeric(18)
--declare @mModelID numeric(18)
--declare @mcolourID numeric(18)
--declare @mReturnText nvarchar(4000)
--declare @macroKey nchar(2)
--Declare @mReturnMessage nvarchar(200)
--Declare @mTable nvarchar(20)
--Declare @mID nvarchar(20)
--Declare @mValue nvarchar(50)
--Declare @mControlType nvarchar(2)
--Select @mMessage = ''


--Select @mValue = ''
--Select @mTable = ''
--Select @mID = ''
--Select @mControlType = ''
--Select @mReturnMessage = ''
--Select @macroKey = ''
--Select @mReturnText = ''


--Select @mCarrierID = -1
--Select @mManufacturerID = -1
--Select @mModelID = -1
--Select @mcolourID = -1

---- see if the UPC is already there.
--if not exists (select * from MasterCarrierManufacturerUPCLookup where UPC = @mUPC and Status = 'Active')
--   begin
--   Select @mMessage = ''        -- Leave it empty so calling procedure will see no results and move on to the next "assumption".
--   return 0
--   end

--Select @mCarrierID = OptionCarrierID, 
--       @mcolourID = OptionColourID, 
--       @mManufacturerID = OptionManufacturerID,
--       @mModelID = OptionModelID
--       From MasterCarrierManufacturerUPCLookup A
--       Inner Join MasterCarrierManufacturerLookup B on A.MasterCarrierManufacturerLookupID = B.MasterCarrierManufacturerLookupID
--       where A.UPC = @mUPC and Status = 'Active'
       
--Select @mCarrierID = ISNULL(@mCarrierID, -1)
--Select @mManufacturerID = ISNULL(@mManufacturerID, -1)
--Select @mModelID = ISNULL(@mModelID, -1)
--Select @mcolourID = ISNULL(@mcolourID, -1)

---- Get the Carrier String
--Select @mReturnMessage = Description + ':' + OptionText
--     , @mTable = 'Option'
--     , @macroKey = [Option].MacroKey
--     , @mID = convert(nvarchar(20), [Option].OptionID)
--     , @mControlType = Case when QuestionType.Type = 'Dropdown' then 'DD'
--                            when QuestionType.Type = 'CheckBox' then 'CB'
--                            when QuestionType.Type = 'RadialButton' then 'RD'   
--                            when QuestionType.Type = 'Calendar' then 'CA'                   
--                            else 'TX' END
--      From [Option] 
--     Inner Join Question on Question.QuestionID = [Option].QuestionID
--     INNER JOIN QuestionType ON Question.QuestionTypeID = QuestionType.QuestionTypeID     
--     where OptionID = @mCarrierID
     

--Select @mReturnMessage = isnull(@mReturnMessage, '')
--Select @mReturnText = @mTable + ':' + @mID + ':' + @mReturnMessage + ':' + @macroKey + ':' + @mControlType + ';'   
  
--Select @mMessage = @mMessage + @mReturnText
--Select @mValue = ''
--Select @mTable = ''
--Select @mID = ''
--Select @macroKey = ''
--Select @mControlType = ''
--Select @mReturnMessage = ''
--Select @mReturnText = ''
--------------------------------------------------     
---- Get the @mManufacturerID String
--Select @mReturnMessage = Description + ':' + OptionText
--     , @mTable = 'Option'
--     , @macroKey = [Option].MacroKey
--     , @mID = convert(nvarchar(20), [Option].OptionID)
--     , @mControlType = Case when QuestionType.Type = 'Dropdown' then 'DD'
--                            when QuestionType.Type = 'CheckBox' then 'CB'
--                            when QuestionType.Type = 'RadialButton' then 'RD'   
--                            when QuestionType.Type = 'Calendar' then 'CA'                   
--                            else 'TX' END
--      From [Option] 
--     Inner Join Question on Question.QuestionID = [Option].QuestionID
--     INNER JOIN QuestionType ON Question.QuestionTypeID = QuestionType.QuestionTypeID     
--     where OptionID = @mManufacturerID
--Select @mReturnMessage = isnull(@mReturnMessage, '')
--Select @mReturnText = @mTable + ':' + @mID + ':' + @mReturnMessage + ':' + @macroKey + ':' + @mControlType + ';'
--Select @mMessage = @mMessage + @mReturnText
--Select @mValue = ''
--Select @mTable = ''
--Select @mID = ''
--Select @macroKey = ''
--Select @mControlType = ''
--Select @mReturnMessage = ''
--Select @mReturnText = ''
--------------------------------------------------     
--     -- Get the @mModelID String
--Select @mReturnMessage = Description + ':' + OptionText
--     , @mTable = 'Option'
--     , @macroKey = [Option].MacroKey
--     , @mID = convert(nvarchar(20), [Option].OptionID)
--     , @mControlType = Case when QuestionType.Type = 'Dropdown' then 'DD'
--                            when QuestionType.Type = 'CheckBox' then 'CB'
--                            when QuestionType.Type = 'RadialButton' then 'RD'   
--                            when QuestionType.Type = 'Calendar' then 'CA'                   
--                            else 'TX' END
--      From [Option] 
--     Inner Join Question on Question.QuestionID = [Option].QuestionID
--     INNER JOIN QuestionType ON Question.QuestionTypeID = QuestionType.QuestionTypeID     
--     where OptionID = @mModelID
--Select @mReturnMessage = isnull(@mReturnMessage, '')
--Select @mReturnText = @mTable + ':' + @mID + ':' + @mReturnMessage + ':' + @macroKey + ':' + @mControlType + ';'
--Select @mMessage = @mMessage + @mReturnText
--Select @mValue = ''
--Select @mTable = ''
--Select @mID = ''
--Select @macroKey = ''
--Select @mControlType = ''
--Select @mReturnMessage = ''
--Select @mReturnText = ''
--------------------------------------------------     
--     -- Get the @mcolourID String
--Select @mReturnMessage = Description + ':' + OptionText
--     , @mTable = 'Option'
--     , @macroKey = [Option].MacroKey
--     , @mID = convert(nvarchar(20), [Option].OptionID)
--     , @mControlType = Case when QuestionType.Type = 'Dropdown' then 'DD'
--                            when QuestionType.Type = 'CheckBox' then 'CB'
--                            when QuestionType.Type = 'RadialButton' then 'RD'   
--                            when QuestionType.Type = 'Calendar' then 'CA'                   
--                            else 'TX' END
--      From [Option] 
--     Inner Join Question on Question.QuestionID = [Option].QuestionID
--     INNER JOIN QuestionType ON Question.QuestionTypeID = QuestionType.QuestionTypeID     
--     where OptionID = @mcolourID
--Select @mReturnMessage = isnull(@mReturnMessage, '')
--Select @mReturnText = @mTable + ':' + @mID + ':' + @mReturnMessage + ':' + @macroKey + ':' + @mControlType + ';'
--Select @mMessage = @mMessage + @mReturnText
--Select @mValue = ''
--Select @mTable = ''
--Select @mID = ''
--Select @macroKey = ''
--Select @mControlType = ''
--Select @mReturnMessage = ''
--Select @mReturnText = ''
--------------------------------------------------     
 
--return 0

--END



--GO





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



/****** Object:  StoredProcedure [dbo].[ADD_ScanComandLookupAttributeList]    Script Date: 04/19/2020 18:42:13 ******/
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
Declare @mScanComandLookupID  numeric (18,0) 
Declare @mScanComandLookupAttributeListID  numeric (18,0) 
Exec ADD_ScanComandLookupAttributeList_B 'JimTextUPC8', 2226, 'SCAN', 11, 'jmccomb', @mScanComandLookupID output, @mScanComandLookupAttributeListID output, @mMessage output
Print @mMessage
Print @mScanComandLookupID
Print @mScanComandLookupAttributeListID


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


Create PROCEDURE [dbo].[ADD_ScanComandLookupAttributeList_B]

      @mScanCode nvarchar(250),
      @mOptionID numeric (18, 0),
      @mSetValue nVarchar(250),
      @mSequence int, 
      @mUserName nvarchar(50) = '',
      @mScanComandLookupID numeric (18,0) output,    
      @mScanComandLookupAttributeListID numeric (18,0) output,     
      @mMessage nVarchar(4000) output

AS
BEGIN
	SET NOCOUNT ON;

Declare @mCommandString nvarchar(250)
Declare @mAttributeQuestionName nvarchar(20)
Select @mCommandString = '';
Select @mScanComandLookupAttributeListID = -1
Select @mScanComandLookupID = -1


Select @mMessage = ''
-- see if the UPC is already there.
select @mScanComandLookupID = ScanComandLookupID from ScanComandLookup where ScanComandLookup.ScanCode = @mScanCode and Status = 'Active'
select @mScanComandLookupID = ISNULL(@mScanComandLookupID, -1)
if @mScanComandLookupID < 1
   begin
      exec ADD_ScanComandLookup @mScanCode, @mUserName, @mMessage output
      select @mScanComandLookupID = ScanComandLookupID from ScanComandLookup where ScanComandLookup.ScanCode = @mScanCode and Status = 'Active'
      select @mScanComandLookupID = ISNULL(@mScanComandLookupID, -1)
      if @mScanComandLookupID < 1
         begin   
         Select @mMessage = 'Error: Unable to create ScanCommandLookup:' + @mMessage + @mScanCode
         return 0
         end
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




GO




















