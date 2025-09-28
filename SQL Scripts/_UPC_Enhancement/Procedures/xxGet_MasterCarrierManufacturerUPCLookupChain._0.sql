
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


