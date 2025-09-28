/****** Object:  StoredProcedure [dbo].[BlackBelt_ParseData]    Script Date: 06/21/2018 14:00:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Select * from ReceiveDetail where ESN = '358761058175568' and Version = '000'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

/*


Update XMLFileHeader set StatusID = 1 where StatusID = 2 and FileType = 'BB'
Exec Job_LoadBlackBelt02

Select * from XMLFileHeader
Select * from BlackBeltTransHeader
Select * from BlackBeltTransDetail Order by BlackBeltTransHeaderID, BlackBeltTransDetailID
Select * from BlackbeltTransRunLog order by BlackBeltLogParentID, BlackbeltTransRunLogID
Select * from BlackbeltTransMessages


-- Select * from XMLFileHeader
Delete BlackBeltTransHeader
Delete BlackBeltTransDetail
Delete BlackbeltTransMessages
Delete BlackbeltTransRunLog
Select * from QUestion where name = 'Carrier'
Select * from [option] where questionid = 210 and name = 'Gold'

-----------------------------------
              Declare @Message nvarchar(500)
              Declare @BlackbeltTransRunLogID numeric(18)
              Select @Message = ''
              EXEC BlackBelt_ParseData  101, 2952, @BlackbeltTransRunLogID output, @Message output
              Print @Message

Select * from BlackBeltTransDetail -- where BlackBeltTransHeaderID = 75
------------------------------------

Update XMLFileHeader set StatusID = 1



Opening the document
Ready to Select from DataWipt
I am here
Getting the IMEI :2018-06-15 19:52:07.
Start Translating:2018-06-15 19:52:07.
Getting Defaults:2018-06-15 19:52:07.
Looking for requied fields:2018-06-15 19:52:07.
Add any of the UserAdded attributes if they did not come down with the XML
Updateing Header with required field stuff.:2018-06-15 19:52:07.
Looking for Existing IMEI:2018-06-15 19:52:07.
Looking for Client Location:2018-06-15 19:52:07.
Looking for Project:2018-06-15 19:52:07.
Looking for Process:2018-06-15 19:52:07.
and I am here
Finished Parse Succes:Parse
Finished Edit Error:Edit/Edit:New Manufacturer samsung not found./Edit:New Model SM-G920W8 not found./Edit:New Invalid Sku Combo 
















Declare @BlackbeltTransRunLogID numeric(18)
Declare @Message nvarchar(500)
Exec BlackBelt_ParseData 77, @BlackbeltTransRunLogID, @Message output
Print @Message


Select * from BlackBeltTransHeader

*/

ALTER PROCEDURE [dbo].[BlackBelt_ParseData]
        @BlackbeltTransHeaderID numeric(18)
      , @iBlackbeltTransDetailID numeric(18)
      , @BlackbeltTransRunLogID numeric(18) OUTPUT
      , @Message nvarchar(500) OUTPUT

AS
BEGIN
	SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	

Print 'BlackBelt_ParseData----------------------------------------- Start:' + convert(nvarchar(20), getdate(),121)

Select @iBlackbeltTransDetailID = isnull(@iBlackbeltTransDetailID, -1)
Declare @SingleDetailOnly int
Select @SingleDetailOnly = 1 -- This means Yes.


--Select @Message = 'Detail Specific Run:BlackbeltTransDetailID=' + CONVERT(nvarchar(20), @iBlackbeltTransDetailID)
if @iBlackbeltTransDetailID = -1
   begin
   Select @SingleDetailOnly = 0 -- This means No.
   end

Select @Message = 'Unknown Run'
if @SingleDetailOnly = 0
   begin
   Select @Message = 'Full Run'
   end

if @SingleDetailOnly = 1
   begin
   Select @Message = 'Detail Specific Run:BlackbeltTransDetailID=' + CONVERT(nvarchar(20), @iBlackbeltTransDetailID)
   end
   

INSERT INTO [BlackbeltTransRunLog] ([BlackbeltTransHeaderID], BlackbeltLogParentID, BlackbeltTransDetailID, [Status],[Message],[Comment],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
     VALUES (@BlackbeltTransHeaderID, -1, @iBlackbeltTransDetailID,  'Parse',@Message,@Message,GETDATE(),'Parse',GETDATE(),'Parse')
Select @BlackbeltTransRunLogID = @@IDENTITY
Update BlackbeltTransRunLog set BlackbeltLogParentID = @BlackbeltTransRunLogID where BlackbeltTransRunLogID = @BlackbeltTransRunLogID 
   


Declare @BlackbeltTransDetailID numeric(18)
Declare @keyValue nvarchar(200)
Declare @ValueValue nvarchar(200)
Declare @ReturnValue nvarchar(200)
Declare @xReturnValue nvarchar(200)

Create Table #ErrorMessages
        (ID numeric(18,0) identity Not Null
        ,dID numeric(18,0) Null
        ,Message nvarchar(500) Not Null
        ,Level nvarchar(10) Not NULL)
-- Select * from Project 

                         
      -- We need to make sure we have some of the basic attributes required to set up a Device.
      Declare @CreateDate datetime
      Declare @UserName nvarchar(75)
      Declare @IMEI nvarchar(50)
      Declare @AddToClientLocation nvarchar(50)
      Declare @BlackBeltProjectName nvarchar(50)
      Declare @BlackBeltProjectTag nvarchar(50)
      Declare @BlackBeltProcessSKey nvarchar(50)
      Declare @BlackBeltCarrier nvarchar(50)
      Select @AddToClientLocation = ''
      Select @BlackBeltProjectName = ''
      Select @BlackBeltProjectTag = ''
      Select @BlackBeltProcessSKey = ''
      Select @BlackBeltCarrier = ''
     
      Select @CreateDate = getdate()
      Select @UserName = 'JLoadBB02'


      -----------------------------------------------------------------------------------------------------      
      Select @IMEI = Value from BlackbeltTransDetail where [Key] = 'IMEI' and BlackBeltTransHeaderID = @BlackbeltTransHeaderID 
      Print 'IMEI(' + @IMEI + '):' + convert(nvarchar(20), getdate(),121)
   
      if (@SingleDetailOnly = 1)
         begin
         -- Reset the status back to start to allow for reprocess
         Update  BlackBeltTransDetail set processStatus = 1, TranslationKey = null, TranslationValue = null, QuestionID = null, QuestionType = null, OptionID = null, ItemAbbreviation=null, Message = null where BlackbeltTransDetailID = @iBlackbeltTransDetailID
         Print 'Setting Process Status back to 1:' + convert(nvarchar(20), getdate(),121)
         end
   
      if (@SingleDetailOnly = 0)         -- Only do if a Full Run
          begin
          -- Set all things back to Start
          Update BlackBeltTransHeader set ProcessStatus = 1
                                         ,Status = 'Translate'
                                         ,ESN = @IMEI
                                         ,ClientLocationScanKey = @AddToClientLocation
                                         ,ProjectName = @BlackBeltProjectName
                                         ,ProcessScanKey = @BlackBeltProcessSKey
                                         ,ProjectTag = @BlackBeltProjectTag
                                    where BlackBeltTransHeaderID = @BlackbeltTransHeaderID 
         end
      
      -- Because of process Status, this should be all that were reset above    
      -- Select * from BlackBeltTransDetail  
      ----------------------------------------------------------------------------------------------------------------------------------------- 
      Print 'Start Translating(' + @IMEI + '):' + convert(nvarchar(20), getdate(),121)
      -- Translate the field names into Questions
      while exists(Select * from BlackBeltTransDetail D 
                     where (D.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and D.ProcessStatus = 1 and @SingleDetailOnly = 0) 
                        or (D.BlackbeltTransDetailID = @iBlackbeltTransDetailID and D.ProcessStatus = 1 and @SingleDetailOnly = 1))
            begin
             Select top 1 @BlackbeltTransDetailID = BlackbeltTransDetailID ,
                          @keyValue = 'Question',
                          @ValueValue = [Key]       
               from BlackBeltTransDetail D  
              where (D.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and D.ProcessStatus = 1 and @SingleDetailOnly = 0) 
                 or (D.BlackbeltTransDetailID = @iBlackbeltTransDetailID and @SingleDetailOnly = 1)
              Exec Get_XMLTranslationValue  @keyValue, @ValueValue,  @ReturnValue OUTPUT 
              --Print 'Setting Question Translation Key=' + @KeyValue + ' Value=' + @ValueValue + ' ReturnValue=' + @ReturnValue + ' ' + convert(nvarchar(20), getdate(),121)
              Update  BlackBeltTransDetail set processStatus = 2, TranslationKey = @ReturnValue  where BlackbeltTransDetailID = @BlackbeltTransDetailID
            end
            
              
      -- Translate the field Values into proper Brain Values.
      while exists(Select * from BlackBeltTransDetail D where  (D.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and D.ProcessStatus = 2 and @SingleDetailOnly = 0) 
                                                            or (D.BlackbeltTransDetailID = @iBlackbeltTransDetailID  and D.ProcessStatus = 2and @SingleDetailOnly = 1))
            begin
            Select top 1 @BlackbeltTransDetailID = BlackbeltTransDetailID ,
                         @keyValue = TranslationKey,
                         @ValueValue = Value
              from  BlackBeltTransDetail D 
              where  (D.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and D.ProcessStatus = 2 and @SingleDetailOnly = 0) 
                  or (D.BlackbeltTransDetailID = @iBlackbeltTransDetailID  and D.ProcessStatus = 2 and @SingleDetailOnly = 1)
               
              Exec Get_XMLTranslationValue  @keyValue, @ValueValue,  @ReturnValue OUTPUT 
              --Print 'Setting Option Translation Key=' + @KeyValue + ' Value=' + @ValueValue + ' ReturnValue=' + @ReturnValue + ' ' +  convert(nvarchar(20), getdate(),121)
              Update  BlackBeltTransDetail set processStatus = 3, TranslationValue = @ReturnValue  where BlackbeltTransDetailID = @BlackbeltTransDetailID
            end

       --Select * from BlackBeltTransDetail
      -----------------------------------------------------------------------------------------------------------------------------------------
      
      if @SingleDetailOnly = 0
         begin
              Print 'Getting Defaults:' + convert(nvarchar(20), getdate(),121)
	 
              Select Top 1 @AddToClientLocation = Data from SystemData where DataKey = 'BlackBeltClientLocat'  
              if LEN(isnull(@AddToClientLocation,'')) = 0
                 Begin
                 Select @AddToClientLocation = 'BW1'
                 End
              Select Top 1 @BlackBeltProjectName = Data from SystemData where DataKey = 'BlackBeltProjectName'  
              if LEN(isnull(@BlackBeltProjectName,'')) = 0
                 Begin
                 Select @BlackBeltProjectName = 'Bridge Product'
                 End
              Select Top 1 @BlackBeltProjectTag = Data from SystemData where DataKey = 'BlackBeltProjectTag'  
              if LEN(isnull(@BlackBeltProjectTag,'')) = 0
                 Begin
                 Select @BlackBeltProjectTag = 'PTag'
                 End
              Select Top 1 @BlackBeltProcessSKey = Data from SystemData where DataKey = 'BlackBeltProcessSKey'  
              if LEN(isnull(@BlackBeltProcessSKey,'')) = 0
                 Begin
                 Select @BlackBeltProcessSKey = 'RAPO'
                 End
              Select Top 1 @BlackBeltCarrier = Data from SystemData where DataKey = 'BlackBeltCarrier'  
              if LEN(isnull(@BlackBeltCarrier,'')) = 0
                 Begin
                 Select @BlackBeltCarrier = 'Bell Mobility'
                 End            
        
              --Print 'Looking for requied fields:' + convert(nvarchar(20), getdate(),121)
        
             -- This may need to be performed down below. I need to look for specific pieces of data, if they are not there, I need to insert it.
             -- but it needs to be translated first.    
             --Print 'Add any of the UserAdded attributes if they did not come down with the XML'
             Declare @id numeric(18)
             Declare @T nvarchar(75)
             -- If we got something coming in, we need to set our defaults to it. Cause we move them over to the header at the end.
             Select @T = TranslationValue from BlackbeltTransDetail where [TranslationKEY] = 'ClientLocation' and BlackbeltTransHeaderID = @BlackbeltTransHeaderID
             select @AddToClientLocation = ISNULL(@T, @AddToClientLocation)
             Select @T = TranslationValue from BlackbeltTransDetail where [TranslationKEY] = 'ProjectName' and BlackbeltTransHeaderID = @BlackbeltTransHeaderID
             select @BlackBeltProjectName = ISNULL(@T, @BlackBeltProjectName)
             Select @T = TranslationValue from BlackbeltTransDetail where [TranslationKEY] = 'ProjectTag' and BlackbeltTransHeaderID = @BlackbeltTransHeaderID
             select @BlackBeltProjectTag = ISNULL(@T, @BlackBeltProjectTag)
             Select @T = TranslationValue from BlackbeltTransDetail where [TranslationKEY] = 'RAPO' and BlackbeltTransHeaderID = @BlackbeltTransHeaderID
             select @BlackBeltProcessSKey = ISNULL(@T, @BlackBeltProcessSKey)
             Select @T = TranslationValue from BlackbeltTransDetail where [TranslationKEY] = 'Carrier' and BlackbeltTransHeaderID = @BlackbeltTransHeaderID
             select @BlackBeltCarrier = ISNULL(@T, @BlackBeltCarrier)
             
             
             if not exists(Select * from BlackbeltTransDetail where [TranslationKEY] = 'ClientLocation' and BlackbeltTransHeaderID = @BlackbeltTransHeaderID)
                begin
                Select @keyValue = 'BlackBeltClientLocat'
                Select @ValueValue = @AddToClientLocation
                Exec Get_XMLTranslationValue  @keyValue, @ValueValue,  @ReturnValue OUTPUT 
                
                Select @keyValue = 'Question'
                Select @ValueValue = 'BlackBeltClientLocat'
                Exec Get_XMLTranslationValue  @keyValue, @ValueValue,  @xReturnValue OUTPUT         
                
                Insert BlackbeltTransDetail (BlackbeltTransHeaderID, ReceiveDetailID, Status, ProcessStatus, [Key], Value, TranslationValue, TranslationKey, CreateUser, CreateDate, LastUpdateDate, LastUpdateUser) 
                Values (@BlackbeltTransHeaderID, null, 'New', 3, 'BlackBeltClientLocat', @AddToClientLocation,@ReturnValue,@xReturnValue, @UserName, @CreateDate, @CreateDate, @UserName)
                select @id = @@IDENTITY
                INSERT INTO [BlackbeltTransMessages] ([BlackbeltTransRunLogID],[BlackbeltTransDetailID],[Type],[Message],[Comment],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
                Values (@BlackbeltTransRunLogID, @id, 'Warning' ,'Client Location Default Added', @ReturnValue,GETDATE(),'Parse',GETDATE(),'Parse')
     
                Select @AddToClientLocation = @ReturnValue
                
                end
             if not exists(Select * from BlackbeltTransDetail where [TranslationKEY] = 'ProjectName' and BlackbeltTransHeaderID = @BlackbeltTransHeaderID)
                begin
                Select @keyValue = 'BlackBeltProjectName'
                Select @ValueValue = @BlackBeltProjectName
                Exec Get_XMLTranslationValue  @keyValue, @ValueValue,  @ReturnValue OUTPUT 
                
                Select @keyValue = 'Question'
                Select @ValueValue = 'BlackBeltProjectName'
                Exec Get_XMLTranslationValue  @keyValue, @ValueValue,  @xReturnValue OUTPUT         
                
                Insert BlackbeltTransDetail (BlackbeltTransHeaderID, ReceiveDetailID, Status, ProcessStatus, [Key], Value, TranslationValue, TranslationKey, CreateUser, CreateDate, LastUpdateDate, LastUpdateUser) 
                Values (@BlackbeltTransHeaderID, null, 'New', 3, 'BlackBeltProjectName', @BlackBeltProjectName,@ReturnValue,@xReturnValue, @UserName, @CreateDate, @CreateDate, @UserName)
                select @id = @@IDENTITY
                INSERT INTO [BlackbeltTransMessages] ([BlackbeltTransRunLogID],[BlackbeltTransDetailID],[Type],[Message],[Comment],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
                Values (@BlackbeltTransRunLogID, @id, 'Warning' ,'Project Name Default Added', @ReturnValue,GETDATE(),'Parse',GETDATE(),'Parse')
                Select @BlackBeltProjectName = @ReturnValue
                end
             if not exists(Select * from BlackbeltTransDetail where [TranslationKEY] = 'ProjectTag' and BlackbeltTransHeaderID = @BlackbeltTransHeaderID)
                begin
                Select @keyValue = 'BlackBeltProjectTag'
                Select @ValueValue = @BlackBeltProjectTag
                Exec Get_XMLTranslationValue  @keyValue, @ValueValue,  @ReturnValue OUTPUT 
                
                Select @keyValue = 'Question'
                Select @ValueValue = 'BlackBeltProjectTag'
                Exec Get_XMLTranslationValue  @keyValue, @ValueValue,  @xReturnValue OUTPUT         
                
                Insert BlackbeltTransDetail (BlackbeltTransHeaderID, ReceiveDetailID, Status, ProcessStatus, [Key], Value, TranslationValue, TranslationKey, CreateUser, CreateDate, LastUpdateDate, LastUpdateUser) 
                Values (@BlackbeltTransHeaderID, -1, 'New', 3, 'BlackBeltProjectTag', @BlackBeltProjectTag,@ReturnValue,@xReturnValue, @UserName, @CreateDate, @CreateDate, @UserName)
                select @id = @@IDENTITY
                INSERT INTO [BlackbeltTransMessages] ([BlackbeltTransRunLogID],[BlackbeltTransDetailID],[Type],[Message],[Comment],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
                Values (@BlackbeltTransRunLogID, @id, 'Warning' ,'Project Tag Default Added', @ReturnValue,GETDATE(),'Parse',GETDATE(),'Parse')
                Select @BlackBeltProjectTag = @ReturnValue
                end
             if not exists(Select * from BlackbeltTransDetail where [TranslationKEY] = 'RAPO' and BlackbeltTransHeaderID = @BlackbeltTransHeaderID)
                begin
                Select @keyValue = 'BlackBeltProcessSKey'
                Select @ValueValue = @BlackBeltProcessSKey
                Exec Get_XMLTranslationValue  @keyValue, @ValueValue,  @ReturnValue OUTPUT 
                
                Select @keyValue = 'Question'
                Select @ValueValue = 'BlackBeltProcessSKey'
                Exec Get_XMLTranslationValue  @keyValue, @ValueValue,  @xReturnValue OUTPUT         
                
                Insert BlackbeltTransDetail (BlackbeltTransHeaderID, ReceiveDetailID, Status, ProcessStatus, [Key], Value, TranslationValue, TranslationKey, CreateUser, CreateDate, LastUpdateDate, LastUpdateUser) 
                Values (@BlackbeltTransHeaderID, null, 'New', 3, 'BlackBeltProcessSKey', @BlackBeltProcessSKey,@ReturnValue,@xReturnValue, @UserName, @CreateDate, @CreateDate, @UserName)
                select @id = @@IDENTITY
                INSERT INTO [BlackbeltTransMessages] ([BlackbeltTransRunLogID],[BlackbeltTransDetailID],[Type],[Message],[Comment],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
                Values (@BlackbeltTransRunLogID, @id, 'Warning' ,'ProcessKey Default Added', @ReturnValue,GETDATE(),'Parse',GETDATE(),'Parse')
                Select @BlackBeltProcessSKey = @ReturnValue
                end
             if not exists(Select * from BlackbeltTransDetail where [TranslationKEY] = 'Carrier' and BlackbeltTransHeaderID = @BlackbeltTransHeaderID)
                begin
                Select @keyValue = 'BlackBeltCarrier'
                Select @ValueValue = @BlackBeltCarrier
                Exec Get_XMLTranslationValue  @keyValue, @ValueValue,  @ReturnValue OUTPUT 
                
                Select @keyValue = 'Question'
                Select @ValueValue = 'BlackBeltCarrier'
                Exec Get_XMLTranslationValue  @keyValue, @ValueValue,  @xReturnValue OUTPUT         
                
                Insert BlackbeltTransDetail (BlackbeltTransHeaderID, ReceiveDetailID, Status, ProcessStatus, [Key], Value, TranslationValue, TranslationKey, CreateUser, CreateDate, LastUpdateDate, LastUpdateUser) 
                Values (@BlackbeltTransHeaderID, null, 'New', 3, 'BlackBeltCarrier', @BlackBeltCarrier,@ReturnValue,@xReturnValue, @UserName, @CreateDate, @CreateDate, @UserName)
                select @id = @@IDENTITY
                INSERT INTO [BlackbeltTransMessages] ([BlackbeltTransRunLogID],[BlackbeltTransDetailID],[Type],[Message],[Comment],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
                Values (@BlackbeltTransRunLogID, @id, 'Warning' ,'Carrier Default Added', @ReturnValue,GETDATE(),'Parse',GETDATE(),'Parse')
                Select @BlackBeltCarrier = @ReturnValue
                end
             --Print 'Updateing Header with required field stuff.:' + convert(nvarchar(20), getdate(),121)      
             Update BlackBeltTransHeader set ClientLocationScanKey = @AddToClientLocation
                                            ,ProjectName = @BlackBeltProjectName
                                            ,ProcessScanKey = @BlackBeltProcessSKey
                                            ,ProjectTag = @BlackBeltProjectTag
                                      where  BlackBeltTransHeaderID = @BlackbeltTransHeaderID 
         end                          

      -- Move the ReceiveDetailID down to the lower levels in error or New as well as reset things back to start.
      Update BlackbeltTransDetail set ReceiveDetailID = H.ReceiveDetailID, ProcessStatus = 3, Status = 'Parse'
      From BlackbeltTransDetail D
      Inner join BlackbeltTransHeader H on D.BlackbeltTransHeaderID = H.BlackbeltTransHeaderID
      where (D.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and D.ProcessStatus = 3 and @SingleDetailOnly = 0) 
         or (D.BlackbeltTransDetailID = @iBlackbeltTransDetailID  and D.ProcessStatus = 3 and @SingleDetailOnly = 1)

       -- Start matching up the data elements. (First, ID the Questions)
      Update BlackbeltTransDetail set QuestionID = (Select QUestionID from Question where Name = BlackbeltTransDetail.TranslationKey)               --, ProcessStatus = 4
      where (BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ProcessStatus = 3 and @SingleDetailOnly = 0) 
         or (BlackbeltTransDetailID = @iBlackbeltTransDetailID  and ProcessStatus = 3 and @SingleDetailOnly = 1)
 --        where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ProcessStatus = 3 --  and [Status] = 'Parse' 
 
      
      Print 'Advancing all those with a question' + convert(nvarchar(20), getdate(),121)
       Update BlackbeltTransDetail set ProcessStatus = 4
       where ((BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ProcessStatus = 3 and @SingleDetailOnly = 0) 
           or (BlackbeltTransDetailID = @iBlackbeltTransDetailID  and ProcessStatus = 3 and @SingleDetailOnly = 1))
            and QuestionID is not null
 --       where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and QuestionID is not null

       -- We need to know what type of Question we are dealing with
      Update BlackbeltTransDetail set QuestionType = t.Type
        From BlackbeltTransDetail a
        inner join Question q on a.QuestionID = q.QuestionID
        inner join QuestionType t on q.QuestionTypeID = t.QuestionTypeID
      where (BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ProcessStatus = 4 and @SingleDetailOnly = 0) 
         or (BlackbeltTransDetailID = @iBlackbeltTransDetailID  and ProcessStatus = 4 and @SingleDetailOnly = 1)
  --      where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ProcessStatus = 4 -- and [Status] = 'Parse'

     Update BlackbeltTransDetail set OptionID = x.OptionID, ItemAbbreviation = x.name                           --,ProcessStatus = 5
         From BlackbeltTransDetail
        Inner join [option] x on BlackbeltTransDetail.QuestionID = x.QuestionID and BlackbeltTransDetail.TranslationValue = x.OptionText
      where (BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ProcessStatus = 4 and @SingleDetailOnly = 0) 
         or (BlackbeltTransDetailID = @iBlackbeltTransDetailID  and ProcessStatus = 4 and @SingleDetailOnly = 1)
    --    where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ProcessStatus = 4      --  and [Status] = 'Parse'


     Print 'Advancing all those with valid data for each question, dropdowns check boxes lists.' + convert(nvarchar(20), getdate(),121)
      Update BlackbeltTransDetail set ProcessStatus = 5
       where ((BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ProcessStatus = 3 and @SingleDetailOnly = 0) 
           or (BlackbeltTransDetailID = @iBlackbeltTransDetailID  and ProcessStatus = 3 and @SingleDetailOnly = 1))
             and OptionID is not null      
       -- where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and OptionID is not null       
       
       -- Update for keyboard etc types. The first option record is the one we have to match.
     Update BlackbeltTransDetail set OptionID = x.OptionID, ItemAbbreviation = x.name           -- ,ProcessStatus = 5
         From BlackbeltTransDetail
        Inner join [option] x on BlackbeltTransDetail.QuestionID = x.QuestionID -- and BlackbeltTransDetail.TranslationValue = x.OptionText
       where ((BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ProcessStatus = 4 and @SingleDetailOnly = 0) 
           or (BlackbeltTransDetailID = @iBlackbeltTransDetailID  and ProcessStatus = 4 and @SingleDetailOnly = 1))
             and QuestionType in ('Keyboard','Calendar','Calc','Numeric','currency','Num3Digit','Text20Digit','Text3Digit','Text10Digit','Text18Digit','Text50Digit' )   
      --  where BlackBeltTransHeaderID = @BlackbeltTransHeaderID  and ProcessStatus = 4 -- and [Status] = 'Parse'
       --   and QuestionType in ('Keyboard','Calendar','Calc','Numeric','currency','Num3Digit','Text20Digit','Text3Digit','Text10Digit','Text18Digit','Text50Digit' )
          
     Print 'Advancing all those with valid data for each question, text, calendar etc' + convert(nvarchar(20), getdate(),121)
     Update BlackbeltTransDetail set ProcessStatus = 5
       where ((BlackBeltTransHeaderID = @BlackbeltTransHeaderID and @SingleDetailOnly = 0) 
           or (BlackbeltTransDetailID = @iBlackbeltTransDetailID and @SingleDetailOnly = 1))
             and OptionID is not null      
 --       where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and OptionID is not null        

                             
      if @SingleDetailOnly = 0
         begin 
              -----------------------------------------------------------------------------------------------------------------------------------------                            
              -- See if we have an active IMEI out there if it is not already known.
              Print 'Looking for Existing IMEI:' + convert(nvarchar(20), getdate(),121)  
              Update BlackBeltTransHeader set ReceiveDetailID = (Select top 1 ReceiveDetailID from ReceiveDetail B where B.ESN = BlackbeltTransHeader.ESN and B.Version = '000')
                                     where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ISNULL(ReceiveDetailID, -1) < 1
                             
              -- Try and link up the ClientLocationID
              Print 'Looking for Client Location:' + convert(nvarchar(20), getdate(),121)  
              Update BlackBeltTransHeader set ClientLocationID = (Select ClientLocationID from ClientLocation where ScanKey = BlackbeltTransHeader.ClientLocationScanKey)
                                     where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ISNULL(ClientLocationID, -1) < 1
                             
               -- Try and link up the ProjectID
              Print 'Looking for Project:' + convert(nvarchar(20), getdate(),121)  
              Update BlackBeltTransHeader set ProjectID = (Select ProjectID from Project where Name = BlackbeltTransHeader.ProjectName)
                                     where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ISNULL(ProjectID, -1) < 1   
                                                      
               -- Try and link up the ProcessID
              Print 'Looking for Process:' + convert(nvarchar(20), getdate(),121)  
              Update BlackBeltTransHeader set ProcessID = (Select ProcessID from Process where ScanKey = BlackbeltTransHeader.ProcessScanKey)
                                     where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ISNULL(ProcessID, -1) < 1                            
      


              -- if we have something in the XML, it will take president over the device.
              Update BlackbeltTransHeader set CarrierID = B.OptionID
                 From BlackbeltTransHeader A inner join BlackbeltTransDetail B on A.BlackBeltTransHeaderID = B.BlackBeltTransHeaderID 
                 where A.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and B.TranslationKey = 'Carrier'
              Update BlackbeltTransHeader set ManufacturerID = B.OptionID
                 From BlackbeltTransHeader A inner join BlackbeltTransDetail B on A.BlackBeltTransHeaderID = B.BlackBeltTransHeaderID 
                 where A.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and B.TranslationKey = 'Manufacturer'
              Update BlackbeltTransHeader set ModelID = B.OptionID
                 From BlackbeltTransHeader A inner join BlackbeltTransDetail B on A.BlackBeltTransHeaderID = B.BlackBeltTransHeaderID 
                 where A.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and B.TranslationKey = 'Model'
              Update BlackbeltTransHeader set ColourID = B.OptionID
                 From BlackbeltTransHeader A inner join BlackbeltTransDetail B on A.BlackBeltTransHeaderID = B.BlackBeltTransHeaderID 
                 where A.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and B.TranslationKey = 'Colour'
              Update BlackbeltTransHeader set GradeID = B.OptionID
                 From BlackbeltTransHeader A inner join BlackbeltTransDetail B on A.BlackBeltTransHeaderID = B.BlackBeltTransHeaderID 
                 where A.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and B.TranslationKey = 'Grade'
              Update BlackbeltTransHeader set MemoryID = B.OptionID
                 From BlackbeltTransHeader A inner join BlackbeltTransDetail B on A.BlackBeltTransHeaderID = B.BlackBeltTransHeaderID 
                 where A.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and B.TranslationKey = 'Memory'
        

         end

      if @SingleDetailOnly = 1
         begin 
              ----------------------------------------------------------------------------------------------------------------------------------------- 
              if exists(Select * from BlackBeltTransDetail where BlackbeltTransDetailID = @BlackbeltTransDetailID and TranslationKey = 'Carrier')
                 begin    
                 Update BlackbeltTransHeader set CarrierID = B.OptionID
                   From BlackbeltTransHeader A inner join BlackbeltTransDetail B on A.BlackBeltTransHeaderID = B.BlackBeltTransHeaderID 
                  where A.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and B.BlackbeltTransDetailID = @BlackbeltTransDetailID and TranslationKey = 'Carrier'
                 end   
              if exists(Select * from BlackBeltTransDetail where BlackbeltTransDetailID = @BlackbeltTransDetailID and TranslationKey = 'Manufacturer')
                 begin
                 Update BlackbeltTransHeader set ManufacturerID = B.OptionID
                   From BlackbeltTransHeader A inner join BlackbeltTransDetail B on A.BlackBeltTransHeaderID = B.BlackBeltTransHeaderID 
                  where A.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and B.BlackbeltTransDetailID = @BlackbeltTransDetailID and TranslationKey = 'Manufacturer'
                 end
              if exists(Select * from BlackBeltTransDetail where BlackbeltTransDetailID = @BlackbeltTransDetailID and TranslationKey = 'Model')
                 begin    
                 Update BlackbeltTransHeader set ModelID = B.OptionID
                   From BlackbeltTransHeader A inner join BlackbeltTransDetail B on A.BlackBeltTransHeaderID = B.BlackBeltTransHeaderID 
                  where A.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and B.BlackbeltTransDetailID = @BlackbeltTransDetailID and TranslationKey = 'Model'
                 end                  
                if exists(Select * from BlackBeltTransDetail where BlackbeltTransDetailID = @BlackbeltTransDetailID and TranslationKey = 'Colour')
                 begin    
                Update BlackbeltTransHeader set ColourID = B.OptionID
                  From BlackbeltTransHeader A inner join BlackbeltTransDetail B on A.BlackBeltTransHeaderID = B.BlackBeltTransHeaderID 
                 where A.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and B.BlackbeltTransDetailID = @BlackbeltTransDetailID and TranslationKey = 'Colour'
                 end                  
                if exists(Select * from BlackBeltTransDetail where BlackbeltTransDetailID = @BlackbeltTransDetailID and TranslationKey = 'Grade')
                 begin    
                 Update BlackbeltTransHeader set GradeID = B.OptionID
                   From BlackbeltTransHeader A inner join BlackbeltTransDetail B on A.BlackBeltTransHeaderID = B.BlackBeltTransHeaderID 
                  where A.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and B.BlackbeltTransDetailID = @BlackbeltTransDetailID and TranslationKey = 'Grade'
                 end                  
                if exists(Select * from BlackBeltTransDetail where BlackbeltTransDetailID = @BlackbeltTransDetailID and TranslationKey = 'Memory')
                 begin    
                 Update BlackbeltTransHeader set MemoryID = B.OptionID
                   From BlackbeltTransHeader A inner join BlackbeltTransDetail B on A.BlackBeltTransHeaderID = B.BlackBeltTransHeaderID 
                  where A.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and B.BlackbeltTransDetailID = @BlackbeltTransDetailID and TranslationKey = 'Memory'
                 end                 
              -----------------------------------------------------------------------------------------------------------------------------------------   
         end 

      -- If we do have a known Device already, we want to use the specifics from it.
      Update BlackbeltTransHeader set ClientLocationID = B.ClientLocationID
                                    , ProjectID = B.ProjectID
                                    , ProcessID = B.ProcessID
                                    , CarrierID = B.CarrierID
                                    , ManufacturerID = B.ManufacturerID
                                    , ModelID = B.ModelID
                                    , ColourID = B.ColourID
                                    , GradeID = B.GradeID
        From BlackbeltTransHeader A inner join ReceiveDetail B on A.ReceiveDetailID = B.ReceiveDetailID
        and A.BlackbeltTransHeaderID = @BlackbeltTransHeaderID

      Update BlackbeltTransDetail Set Message = b.Message
        From BlackbeltTransDetail A
       Inner join #ErrorMessages B on A.BlackbeltTransDetailID = b.dID
            
      Update BlackbeltTransDetail set ProcessStatus = 10, Status = 'Parsed'  
      where (BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ProcessStatus = 5 and @SingleDetailOnly = 0) 
         or (BlackbeltTransDetailID = @iBlackbeltTransDetailID  and ProcessStatus = 5 and @SingleDetailOnly = 1)
         
      Update BlackbeltTransDetail set ProcessStatus =  3, Status = 'Misc'  
      where (BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ProcessStatus = 3 and @SingleDetailOnly = 0) 
         or (BlackbeltTransDetailID = @iBlackbeltTransDetailID  and ProcessStatus = 3 and @SingleDetailOnly = 1)
      -- BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ProcessStatus = 3
      
      Update BlackbeltTransDetail set ProcessStatus = -10, Status = 'Error' 
      where (BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ProcessStatus = 4 and @SingleDetailOnly = 0) 
         or (BlackbeltTransDetailID = @iBlackbeltTransDetailID  and ProcessStatus = 4 and @SingleDetailOnly = 1)
      -- BlackBeltTransHeaderID = @BlackbeltTransHeaderID and (ProcessStatus = 4 or ProcessStatus = 2 or ProcessStatus = 1)
 
 
 
                             
      if @SingleDetailOnly = 0
         begin       
              Update BlackBeltTransHeader set ProcessStatus = 10, Status = 'Parsed' where BlackBeltTransHeaderID = @BlackbeltTransHeaderID  
      
      
              if Exists (Select * from BlackbeltTransDetail where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ProcessStatus = -10)
                 begin
                 Update BlackBeltTransHeader set ProcessStatus = 10, Status = 'Warning' where BlackBeltTransHeaderID = @BlackbeltTransHeaderID           
                 ---- We need to see if any of the error ones are Required.
                 If Exists (Select * from BlackbeltTransDetail where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ProcessStatus = 3 and TranslationKey in (Select Data from SystemData where DataKey = 'BlackBeltReq4Add'))
                    Update BlackBeltTransHeader set ProcessStatus = 8, Status = 'Error' where BlackBeltTransHeaderID = @BlackbeltTransHeaderID  
                 end
         end
         
      if Not exists (Select * from #ErrorMessages)
         begin
         Select @Message = 'Parse:Success'     
         end
           
      if exists (Select * from #ErrorMessages)
         begin
         Select @Message = 'Parse:Error' 
         INSERT INTO [BlackbeltTransRunLog] ([BlackbeltTransHeaderID], BlackbeltLogParentID, BlackbeltTransDetailID, [Status],[Message],[Comment],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
         Select @BlackbeltTransHeaderID, @BlackbeltTransRunLogID, @iBlackbeltTransDetailID, 'Parse:Error', Message, Message,GETDATE(),'Parse',GETDATE(),'Parse' from #ErrorMessages
         end
      
      INSERT INTO [BlackbeltTransRunLog] ([BlackbeltTransHeaderID], BlackbeltLogParentID, BlackbeltTransDetailID, [Status],[Message],[Comment],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
      VALUES (@BlackbeltTransHeaderID, @BlackbeltTransRunLogID, @iBlackbeltTransDetailID, 'Parse:End',@Message,convert(nvarchar(20),@BlackbeltTransRunLogID) + ':' + @Message,GETDATE(),'Parse',GETDATE(),'Parse')

                                      
      if @SingleDetailOnly = 1
         begin  
              Print 'Starting Edit:' + convert(nvarchar(20), getdate(),121)  
              Select @Message = ''
              EXEC BlackBelt_ParseDataEdit  @BlackbeltTransHeaderID, @iBlackbeltTransDetailID, @BlackbeltTransRunLogID output, @Message output
              Update BlackbeltTransHeader set Message = Message + '/' + @Message where BlackbeltTransHeaderID = @BlackbeltTransHeaderID
              Print 'Finished Edit:' + convert(nvarchar(20), getdate(),121) 
         end

--------------------------------------------------------------------------------------------------------------------
 
--Print 'Finished BlackBelt_ParseData(' + @IMEI + '):' + convert(nvarchar(20), getdate(),121)
Print 'Everything leaving here with a process Status of :10=passed, 3=Misc not used, -10=Error' + convert(nvarchar(20), getdate(),121)
Print 'BlackBelt_ParseData---------------------------------------- Finish:' + @IMEI + '):' + convert(nvarchar(20), getdate(),121)
-- 
END
Go
