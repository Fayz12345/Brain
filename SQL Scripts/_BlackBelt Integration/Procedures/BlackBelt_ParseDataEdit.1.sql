/****** Object:  StoredProcedure [dbo].[BlackBelt_ParseDataEdit]    Script Date: 06/21/2018 14:14:56 ******/
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
Select * from BlackBeltTransDetail Order by BlackBeltTransHeaderID, [key], BlackBeltTransDetailID
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



ALTER PROCEDURE [dbo].[BlackBelt_ParseDataEdit]
      @BlackbeltTransHeaderID numeric(18)
      , @iBlackbeltTransDetailID numeric(18)
      , @BlackbeltTransRunLogID numeric(18) OUTPUT
      , @Message nvarchar(500) OUTPUT

AS
BEGIN
	SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	


Print 'BlackBelt_ParseDataEdit----------------------------------------- Start:' + convert(nvarchar(20), getdate(),121)
Print 'Everything Coming here with a process Status of :10=passed, 3=Misc not used, -10=Error' + convert(nvarchar(20), getdate(),121)
Declare @SingleDetailOnly int
Select @iBlackbeltTransDetailID = isnull(@iBlackbeltTransDetailID, -1)
Select @SingleDetailOnly = 1 -- Set the default to This means Yes we only need to deal with one Detail Line.
if @iBlackbeltTransDetailID = -1
   begin
   Select @SingleDetailOnly = 0 -- This means No.
   end
   
Select @Message = 'Unknown Run'
if @SingleDetailOnly = 0
   begin
   Select @Message = 'Full Edit Run'
   end

if @SingleDetailOnly = 1
   begin
   Select @Message = 'Detail Specific Edit:BlackbeltTransDetailID=' + CONVERT(nvarchar(20), @iBlackbeltTransDetailID)
   end

INSERT INTO [BlackbeltTransRunLog] ([BlackbeltTransHeaderID], BlackbeltLogParentID, BlackbeltTransDetailID, [Status],[Message],[Comment],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
     VALUES (@BlackbeltTransHeaderID, @BlackbeltTransRunLogID, @iBlackbeltTransDetailID, 'Edit',@Message,@Message,GETDATE(),'Edit',GETDATE(),'Edit')



-- Select @Message = 'Edit:Success'
Declare @BlackbeltTransDetailID numeric(18)

Create Table #ErrorMessages
        (ID numeric(18,0) identity Not Null
        ,dID numeric(18,0) Null
        ,Message nvarchar(500) Not Null
        ,Level nvarchar(50) Not NULL
        ,ProcessLevel int not null)
Create Table #WarningMessages
        (ID numeric(18,0) identity Not Null
        ,dID numeric(18,0) Null
        ,Message nvarchar(500) Not Null
        ,Level nvarchar(50) Not NULL
        ,ProcessLevel int not null)
Declare @keyValue nvarchar(75)
Declare @ValueValue nvarchar(75)
Declare @ReturnValue nvarchar(75)
Declare @NewAdd int
Select @NewAdd = 0 ----------NO.
-- BlackBeltTransHeader.ProcessStatus should already be set to 10, Status Should = 'Parsed'
-- BlackbeltTransDetail.ProcessStatus should already be set to 10, Status Should = 'Parsed'
  
-- We want to exit if we do not have a valid Header (Only if a Full Run = SingleDetailOnly = 0)
if @SingleDetailOnly = 0 and not exists(Select * from BlackBeltTransHeader  where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ProcessStatus = 10 and (Status = 'Parsed' or  Status = 'Warning'))
   begin
   INSERT INTO [BlackbeltTransRunLog] ([BlackbeltTransHeaderID], BlackbeltLogParentID, BlackbeltTransDetailID, [Status],[Message],[Comment],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
        VALUES (@BlackbeltTransHeaderID, @BlackbeltTransRunLogID, @iBlackbeltTransDetailID, 'Edit:End','Invalid header process status != 10/Parsed or Warning','',GETDATE(),'Edit',GETDATE(),'Edit')
   return
   end

-- Set all things back to Start
-- Assumption. Any Device detail moving past this line is statusprocess 10 or above. This is set inside the "BlackBelt_ParseData" (an entry point)
Update BlackBeltTransHeader set ProcessStatus = 11, Status = 'Edit' where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ProcessStatus = 10 and @SingleDetailOnly = 0

Update BlackbeltTransDetail set ProcessStatus = 11, Status = 'Edit' 
where (BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ProcessStatus = 10 and @SingleDetailOnly = 0) 
   or (BlackbeltTransDetailID = @iBlackbeltTransDetailID and @SingleDetailOnly = 1)

-- See if we are updating an IMEI or if we are Adding a new one.
If Exists(Select 0 from BlackbeltTransHeader where  BlackBeltTransHeaderID = @BlackbeltTransHeaderID and isnull(ReceiveDetailID, -1) < 1)
   begin
   Select @NewAdd = 1 ---------- YES.
   end


if @NewAdd = 1
   --We need to be able to create a new Device. 
   -- If we have a new add, we need to check this.
   -- Do we have a Client Location
   -- Do we have a project
   -- Do we have a receive Process
   -- Do we have a proper SKU Combo/Carrier/Manufacturer/Model/Colour
   begin
        Update BlackBeltTransHeader set ProcessStatus = 12, Status = 'Edit:New' 
         where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ProcessStatus = 11 and @SingleDetailOnly = 0
        Update BlackbeltTransDetail set ProcessStatus = 12, Status = 'Edit:New' 
         where (BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ProcessStatus = 11 and @SingleDetailOnly = 0) 
            or (BlackbeltTransDetailID = @iBlackbeltTransDetailID and @SingleDetailOnly = 1)
   
	    Insert #ErrorMessages (Message, Level,ProcessLevel) Select 'Client loc ' + BlackbeltTransHeader.ClientLocationScanKey + ' not found.','Edit:New', 0 
	      from BlackbeltTransHeader where BlackBeltTransHeaderID = @BlackbeltTransHeaderID  and isnull(ClientLocationID, -1) < 1
	    Insert #ErrorMessages (Message, Level,ProcessLevel) Select 'Project ' + BlackbeltTransHeader.ClientLocationScanKey + ' not found.','Edit:New', 0 
	      from BlackbeltTransHeader where BlackBeltTransHeaderID = @BlackbeltTransHeaderID  and isnull(ProjectID, -1) < 1
	    Insert #ErrorMessages (Message, Level,ProcessLevel) Select 'Process ' + BlackbeltTransHeader.ClientLocationScanKey + ' not found.','Edit:New', 0 
	      from BlackbeltTransHeader where BlackBeltTransHeaderID = @BlackbeltTransHeaderID  and isnull(ProcessID, -1) < 1
	   
	    Insert #ErrorMessages (Message, Level, dID,ProcessLevel)
	    Select 'Carrier ' + isnull(BlackbeltTransDetail.Value,'') + ' not found.','Edit:New', BlackbeltTransDetail.BlackbeltTransDetailID, 0
	      from BlackbeltTransHeader A inner join BlackbeltTransDetail on BlackbeltTransDetail.BlackbeltTransHeaderID = A.BlackbeltTransHeaderID
	     where A.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and BlackbeltTransDetail.TranslationKey = 'Carrier' and isnull(CarrierID, -1) < 1

	    Insert #ErrorMessages (Message, Level, dID,ProcessLevel)
	    Select 'Manufacturer ' + isnull(BlackbeltTransDetail.Value,'') + ' not found.','Edit:New', BlackbeltTransDetail.BlackbeltTransDetailID, 0
	     from BlackbeltTransHeader A inner join BlackbeltTransDetail on BlackbeltTransDetail.BlackbeltTransHeaderID = A.BlackbeltTransHeaderID
	    where A.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and BlackbeltTransDetail.TranslationKey = 'Manufacturer' and isnull(ManufacturerID, -1) < 1

	   Insert #ErrorMessages (Message, Level, dID,ProcessLevel)
	   Select 'Model '  + isnull(BlackbeltTransDetail.Value,'') + ' not found.','Edit:New', BlackbeltTransDetail.BlackbeltTransDetailID, 0
	     from BlackbeltTransHeader A inner join BlackbeltTransDetail on BlackbeltTransDetail.BlackbeltTransHeaderID = A.BlackbeltTransHeaderID
	    where A.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and BlackbeltTransDetail.TranslationKey = 'Model' and isnull(ModelID, -1) < 1

       Insert #ErrorMessages (Message, Level, dID,ProcessLevel)
	   Select 'Colour '  + isnull(BlackbeltTransDetail.Value,'') + ' not found.','Edit:New' , BlackbeltTransDetail.BlackbeltTransDetailID, 0
         from BlackbeltTransHeader A inner join BlackbeltTransDetail on BlackbeltTransDetail.BlackbeltTransHeaderID = A.BlackbeltTransHeaderID
        where A.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and BlackbeltTransDetail.TranslationKey = 'Colour' and isnull(ColourID, -1) < 1

       If Not Exists(Select 0 FROM BlackbeltTransHeader  
                             INNER JOIN MasterCarrierManufacturerLookup 
                                ON BlackbeltTransHeader.CarrierID = MasterCarrierManufacturerLookup.OptionCarrierID AND BlackbeltTransHeader.ManufacturerID = MasterCarrierManufacturerLookup.OptionManufacturerID 
                               AND BlackbeltTransHeader.ModelID = MasterCarrierManufacturerLookup.OptionModelID 
                               AND BlackbeltTransHeader.ColourID = MasterCarrierManufacturerLookup.OptionColourID 
                             where BlackBeltTransHeaderID = @BlackbeltTransHeaderID)
          begin
          Insert #ErrorMessages (Message, Level,ProcessLevel)
          Select 'Invalid Sku Combo ','Edit:New', 0
          
          INSERT INTO [BlackbeltTransMessages] ([BlackbeltTransRunLogID],[BlackbeltTransDetailID],[Type],[Message],[Comment],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
          Select @BlackbeltTransRunLogID, null,'Error' ,'Invalid Sku Combo', '',GETDATE(),'Edit',GETDATE(),'Edit' from #ErrorMessages      
          end  

       -- at this point, we should only have one message per detail line.
       Update BlackbeltTransDetail Set Message = b.Message, Status = 'Error' 
         From BlackbeltTransDetail A Inner join #ErrorMessages B on A.BlackbeltTransDetailID = b.dID
        where A.Status = 'Edit:New' and ProcessLevel = 0
     
       Update #ErrorMessages set ProcessLevel = 100 where ProcessLevel = 0
   end

   
 -- finished with the new stuff,
 -- Bring everything back to where we look at the detail.
Update BlackBeltTransHeader set ProcessStatus = 15, Status = 'Edit' where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and Status != 'Error' and @SingleDetailOnly = 0
Update BlackbeltTransDetail set ProcessStatus = 15, Status = 'Edit' 
 where (BlackBeltTransHeaderID = @BlackbeltTransHeaderID and Status != 'Error' and ProcessStatus = 12 and @SingleDetailOnly = 0) 
or (BlackbeltTransDetailID = @iBlackbeltTransDetailID and Status != 'Error' and ProcessStatus = 12 and @SingleDetailOnly = 1)
 -- where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and Status != 'Error'


-- If we have any problems, lets skip over the detail edits.
if not exists (Select * from #ErrorMessages)
   begin
   -- We have to look to make sure the SKU is good... Incase we are updating, not adding.
   If Not Exists(Select 0 FROM BlackbeltTransHeader INNER JOIN MasterCarrierManufacturerLookup 
                            ON BlackbeltTransHeader.CarrierID = MasterCarrierManufacturerLookup.OptionCarrierID AND BlackbeltTransHeader.ManufacturerID = MasterCarrierManufacturerLookup.OptionManufacturerID 
                           AND BlackbeltTransHeader.ModelID = MasterCarrierManufacturerLookup.OptionModelID 
                           AND BlackbeltTransHeader.ColourID = MasterCarrierManufacturerLookup.OptionColourID where BlackBeltTransHeaderID = @BlackbeltTransHeaderID)
      begin
      Insert #ErrorMessages (Message, Level,ProcessLevel)
      Select 'Invalid Sku Combo ','Edit:New', 0
      
      INSERT INTO [BlackbeltTransMessages] ([BlackbeltTransRunLogID],[BlackbeltTransDetailID],[Type],[Message],[Comment],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
      Select @BlackbeltTransRunLogID, null,'Error' ,'Invalid Sku Combo.', '',GETDATE(),'Edit',GETDATE(),'Edit' from #ErrorMessages      
      
      
      end              
      
   -- These are really just warning problems.  These are not key attributes, so they can "sort of" be ignored.
   /* go through the rest of the attributes. Are there any attributes that were not located within the Brain.         */
   Insert #WarningMessages (Message, Level, dID,ProcessLevel)
   Select 'Attribute ' + isnull(BlackbeltTransDetail.[Key],'') + ' not found.','Edit', BlackbeltTransDetail.BlackbeltTransDetailID, 0
     from BlackbeltTransDetail where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ProcessStatus = 15 and isnull(QuestionID, -1) < 1 and Status != 'Error'
   Insert #WarningMessages (Message, Level, dID,ProcessLevel)
   Select 'Value ' + isnull(BlackbeltTransDetail.[Key],'') + ' not found.','Edit', BlackbeltTransDetail.BlackbeltTransDetailID, 0
     from BlackbeltTransDetail where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ProcessStatus = 15 and isnull(OptionID, -1) < 1 and Status != 'Error'           
   end
   
   
   
Update BlackbeltTransDetail Set Message = b.Message, Status = 'Warning' 
  From BlackbeltTransDetail A 
 Inner join #WarningMessages B on A.BlackbeltTransDetailID = b.dID
 where A.Status = 'Edit' and ProcessLevel = 0
Update #WarningMessages set ProcessLevel = 100 where ProcessLevel = 0    
   
Update BlackbeltTransDetail Set Message = b.Message, Status = 'Error' 
  From BlackbeltTransDetail A 
 Inner join #ErrorMessages B on A.BlackbeltTransDetailID = b.dID
 where A.Status = 'Edit' and ProcessLevel = 0
Update #ErrorMessages set ProcessLevel = 101 where ProcessLevel = 0  

Update BlackBeltTransHeader set ProcessStatus = 20, Status = 'Edited' 
 where BlackBeltTransHeaderID = @BlackbeltTransHeaderID  and [Status] = 'Edit' 
  and (ProcessStatus = 15) and @SingleDetailOnly = 0

Update BlackbeltTransDetail set ProcessStatus = -20, Status = 'Error' 
 where BlackBeltTransHeaderID = @BlackbeltTransHeaderID -- and [Status] = 'Edit' 
   and BlackbeltTransDetailID in (Select dID from #ErrorMessages)  
      
Update BlackbeltTransDetail set ProcessStatus = 20, Status = 'Edited' 
where (BlackBeltTransHeaderID = @BlackbeltTransHeaderID and [Status] = 'Edit' and (ProcessStatus = 15) and @SingleDetailOnly = 0) 
   or (BlackbeltTransDetailID = @iBlackbeltTransDetailID and [Status] = 'Edit' and (ProcessStatus = 15)and @SingleDetailOnly = 1)

Update BlackBeltTransHeader set ProcessStatus = 18, Status = 'Error' 
 where BlackBeltTransHeaderID = @BlackbeltTransHeaderID 
   and exists(Select * from #ErrorMessages) and @SingleDetailOnly = 0


   -- and  ( NOT BlackbeltTransDetailID in (Select dID from #ErrorMessages))             -- and ProcessStatus > 3                -- and ProcessStatus > 3         

if Not exists (Select * from #ErrorMessages) and not exists (select * from #WarningMessages)
   begin
   Select @Message = 'Edit:Succes'     
   end
  
 if Not exists (Select * from #ErrorMessages) and exists (select * from #WarningMessages)
   begin
   Select @Message = 'Edit:Warning'     
   end      
      
if exists (Select * from #ErrorMessages)
   begin
   Select @Message = 'Edit:Error' 
   Select @Message = @Message + '/' + Level + ' ' + Message from #ErrorMessages
   end
   
INSERT INTO [BlackbeltTransMessages] ([BlackbeltTransRunLogID],[BlackbeltTransDetailID],[Type],[Message],[Comment],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
     Select @BlackbeltTransRunLogID, dID,'Error' ,Message, '',GETDATE(),'Edit',GETDATE(),'Edit' from #ErrorMessages

INSERT INTO [BlackbeltTransMessages] ([BlackbeltTransRunLogID],[BlackbeltTransDetailID],[Type],[Message],[Comment],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
     Select @BlackbeltTransRunLogID, dID, 'Warning' ,Message, '',GETDATE(),'Edit',GETDATE(),'Edit' from #WarningMessages
     
Drop Table #ErrorMessages
Drop Table #WarningMessages

INSERT INTO [BlackbeltTransRunLog] ([BlackbeltTransHeaderID], BlackbeltLogParentID, BlackbeltTransDetailID, [Status],[Message],[Comment],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
VALUES (@BlackbeltTransHeaderID, @BlackbeltTransRunLogID, @iBlackbeltTransDetailID, 'Edit:End',@Message,convert(nvarchar(20),@BlackbeltTransRunLogID) + ':' + @Message,GETDATE(),'Edit',GETDATE(),'Edit')
     


      if @SingleDetailOnly = 1
         begin  
              Print 'Starting Create/Update:' + convert(nvarchar(20), getdate(),121)  
              Select @Message = ''
              EXEC BlackBelt_ParseDataCreate  @BlackbeltTransHeaderID, @iBlackbeltTransDetailID, @BlackbeltTransRunLogID output, @Message output
              Update BlackbeltTransHeader set Message = Message + '/' + @Message where BlackbeltTransHeaderID = @BlackbeltTransHeaderID
              Print 'Finished Create/Update:' + convert(nvarchar(20), getdate(),121) 
         end

Print 'BlackBelt_ParseEdit---------------------------------------- Finish:'  + convert(nvarchar(20), getdate(),121)
-- Everything that is good is leaving here with a process Status of 
-------------------------------------------------------------------------------------------------------------------
END
Go
