/****** Object:  StoredProcedure [dbo].[Job_LoadBlackBelt02]    Script Date: 06/21/2018 14:01:01 ******/
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
Select * from BlackBeltTransDetail -- where BlackBeltTransHeaderID = 75
Select * from BlackbeltTransRunLog
--Select * from BlackbeltTransMessages


Select * from XMLFileHeader
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


*/


ALTER PROCEDURE [dbo].[Job_LoadBlackBelt02]


AS
BEGIN
	SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	


Declare @XMLFileHeaderID numeric(18)
Declare @TempHeaderID numeric(18)
Declare @BlackbeltTransHeaderID numeric(18)
Declare @BlackbeltTransDetailID numeric(18)

Declare @keyValue nvarchar(75)
Declare @ValueValue nvarchar(75)
Declare @ReturnValue nvarchar(75)
Declare @Message nvarchar(500)
Declare @CreateDate datetime
Declare @UserName nvarchar(75)


DECLARE @XML AS XML, @hDoc AS INT, @SQL NVARCHAR (MAX)
 
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
	 

Print 'Job_LoadBlackBelt02-------------------------------------------------------------- Start:' + convert(nvarchar(20), getdate(),121)
Print 'Starting Job_LoadBlackBelt02:' + convert(nvarchar(20), getdate(),121) 

While exists (Select * from XMLFileHeader where StatusID = 1 and FileType = 'BB')
      begin
  
   
      Print 'Getting the HTML:' + convert(nvarchar(20), getdate(),121)    
      Select top 1 @XMLFileHeaderID = XMLFileHeaderID from XMLFileHeader where StatusID = 1 and FileType = 'BB' order by FileName
      Update XMLFileHeader set StatusID = 2 where XMLFileHeaderID = @XMLFileHeaderID
      SELECT @XML = XMLData FROM XMLFileHeader where XMLFileHeaderID = @XMLFileHeaderID

      Print 'Preparing the the document:' + convert(nvarchar(20), getdate(),121) 
      EXEC sp_xml_preparedocument @hDoc OUTPUT, @XML
      
      Print 'Reading the document Datawipe Records:' + convert(nvarchar(20), getdate(),121)  
      Select @XMLFileHeaderID as XMLFileHeaderID, * 
      into #Tempx
      FROM OPENXML(@hDoc, 'DataWipe')	
      EXEC sp_xml_removedocument @hDoc

	
      Print 'Creating BlackBeltTransHeader:' + convert(nvarchar(20), getdate(),121) 
      Insert BlackBeltTransHeader (XMLFileHeaderID, ESN, ClientLocationScanKey
                              , ProjectName, ProcessScanKey
                              , ProjectTag,  Status, ProcessStatus, RequestUser, CreateUser, CreateDate, LastUpdateDate, LastUpdateUser)
      Values (@XMLFileHeaderID, @Imei, @AddToClientLocation
                              , @BlackBeltProjectName, @BlackBeltProcessSKey
                              , @BlackBeltProjectTag, 'New', 1, @UserName, @UserName, @CreateDate, @CreateDate, @UserName)
      Select @BlackbeltTransHeaderID = @@IDENTITY



      Print 'Adding the Datawipe Detail to the New Header Record:' + convert(nvarchar(20), getdate(),121) 
      Insert BlackbeltTransDetail (BlackbeltTransHeaderID, ReceiveDetailID, Status, ProcessStatus, [Key], Value, CreateUser, CreateDate, LastUpdateDate, LastUpdateUser)
	  Select @BlackbeltTransHeaderID, -1, 'New', 1, H.localname, D.text, @UserName, @CreateDate, @CreateDate, @UserName from #TempX H 
       inner join #Tempx D on H.id = D.parentid
	   where H.id != 0
	   order by H.localname	          

      Drop Table #Tempx


      -- Tested (Good to go)
      Declare @BlackbeltTransRunLogID numeric(18)
      Select @Message = ''
      EXEC BlackBelt_ParseData  @BlackbeltTransHeaderID, -1, @BlackbeltTransRunLogID output, @Message output
      Update BlackbeltTransHeader set Message = @Message where BlackbeltTransHeaderID = @BlackbeltTransHeaderID

      
      ----Print 'Starting Edit:' + convert(nvarchar(20), getdate(),121)  
      Select @Message = ''
      EXEC BlackBelt_ParseDataEdit  @BlackbeltTransHeaderID, -1, @BlackbeltTransRunLogID output, @Message output
      Update BlackbeltTransHeader set Message = Message + '/' + @Message where BlackbeltTransHeaderID = @BlackbeltTransHeaderID
      ----Print 'Finished Edit:' + convert(nvarchar(20), getdate(),121)       
      

      Select @Message = ''
      EXEC BlackBelt_ParseDataCreate  @BlackbeltTransHeaderID, -1, @BlackbeltTransRunLogID output, @Message output
      Update BlackbeltTransHeader set Message = Message + '/' + @Message where BlackbeltTransHeaderID = @BlackbeltTransHeaderID
      ----Print 'Finished Edit:' + convert(nvarchar(20), getdate(),121)       
            
      Print 'Job_LoadBlackBelt02----------------------------------------------------------- Finished:' + convert(nvarchar(20), getdate(),121)
      
      
                   
                                     	                                 
      END
--------------------------------------------------------------------------------------------------------------------
 
END
go
