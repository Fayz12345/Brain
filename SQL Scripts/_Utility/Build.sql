



















/****** Object:  Table [dbo].[IFSPickListOrderDetail]    Script Date: 04/03/2018 14:59:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
--Drop Table BlackbeltTransDetail
CREATE TABLE [dbo].[BlackbeltTransDetail](
	[BlackbeltTransDetailID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[BlackbeltTransHeaderID] [numeric](18, 0) NOT NULL,
	[Status] [varchar](50) NULL,	
	[ProcessStatus] [numeric](18, 0) NULL,
	[ReceiveDetailID] [numeric](18, 0) NULL,	
	[QuestionID] [numeric](18, 0) NULL,
	[QuestionType] [nvarchar](20) NULL,	
	[OptionID] [numeric](18, 0) NULL,	
	[ItemAbbreviation] [nvarchar](50) NULL,

	[Key] [nvarchar](75) NULL,
	[Value] [nvarchar](75) NULL,
	[TranslationKey] [nvarchar](75) NULL,
	[TranslationValue] [nvarchar](75) NULL,
	[Message] [nvarchar](Max) NULL,
	
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,	

 CONSTRAINT [PK_BlackbeltTransDetail] PRIMARY KEY CLUSTERED 
(
	[BlackbeltTransDetailID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO


SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[BlackbeltTransDetail] ADD  CONSTRAINT [DF_BlackbeltTransDetail_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Table [dbo].[IFSPickListOrderHeader]    Script Date: 04/03/2018 14:58:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
-- Drop Table BlackbeltTransHeader

CREATE TABLE [dbo].[BlackbeltTransHeader](
	[BlackbeltTransHeaderID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[XMLFileHeaderID] [numeric](18, 0) NOT NULL,
	[ReceiveDetailID] [numeric](18, 0) NULL,	
	[ClientLocationID] [numeric](18, 0) NULL,	
	[ProjectID] [numeric](18, 0) NULL,	
	[ProcessID] [numeric](18, 0) NULL,	
	[CarrierID] [numeric](18, 0) NULL,	
	[ManufacturerID] [numeric](18, 0) NULL,	
	[ModelID] [numeric](18, 0) NULL,	
	[ColourID] [numeric](18, 0) NULL,	
	[GradeID] [numeric](18, 0) NULL,	
	[ESN] [nvarchar](50) NULL,
	[ClientLocationScanKey] [nvarchar](50) NULL,
	[ProjectName] [nvarchar](50) NULL,
	[ProcessScanKey] [nvarchar](50) NULL,
	[ProjectTag] [nvarchar](50) NULL,
	[Status] [varchar](50) NULL,
	[ProcessStatus] [numeric](18, 0) NULL,
	[RequestUser] [varchar](50) NULL,
	[Message] [nvarchar](Max) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,

 CONSTRAINT [PK_BlackbeltTransHeader] PRIMARY KEY CLUSTERED 
(
	[BlackbeltTransHeaderID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[BlackbeltTransHeader] ADD  CONSTRAINT [DF_BlackbeltTransHeader_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Table [dbo].[IFSPickListOrderHeader]    Script Date: 04/03/2018 14:58:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[BlackbeltTranslationListChangeLog](
	[BlackbeltTranslationListChangeLogID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[BlackbeltTranslationListID] [numeric](18, 0) NOT NULL,
	[Status] [varchar](50) NULL,
	[Catagory] [nvarchar](75) NULL,
	[SearchValue] [nvarchar](75) NULL,
	[Translation] [nvarchar](75) NULL,
	[ChangeCreateDate] [datetime] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,

 CONSTRAINT [PK_BlackbeltTranslationListChangeLog] PRIMARY KEY CLUSTERED 
(
	[BlackbeltTranslationListChangeLogID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[BlackbeltTranslationListChangeLog] ADD  CONSTRAINT [DF_BlackbeltTranslationListChangeLog_ChangeCreateDate]  DEFAULT (getdate()) FOR [ChangeCreateDate]
GO


/****** Object:  Table [dbo].[IFSPickListOrderHeader]    Script Date: 04/03/2018 14:58:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[BlackbeltTranslationList](
	[BlackbeltTranslationListID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Status] [varchar](50) NULL,
	[Catagory] [nvarchar](75) NULL,
	[SearchValue] [nvarchar](75) NULL,
	[Translation] [nvarchar](75) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,

 CONSTRAINT [PK_BlackbeltTranslationList] PRIMARY KEY CLUSTERED 
(
	[BlackbeltTranslationListID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[BlackbeltTranslationList] ADD  CONSTRAINT [DF_BlackbeltTranslationList_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

Create Index IX_BBTL_SearchKey on BlackbeltTranslationList(Status, Catagory, SearchValue)
Go





















































































































/****** Object:  StoredProcedure [dbo].[IFS_GetInvtTranBatch]    Script Date: 04/03/2018 22:10:20 ******/
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

exec IFS_GetInvtTranBatch 10
      
*/   




Create PROCEDURE [dbo].[Get_XMLTranslationValue]
      @QuestionName nvarchar(75)
     ,@LookupValue nvarchar(75) 
     ,@ReturnValue nVarchar(75) Output


AS
BEGIN

	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	
	
	Select @ReturnValue = @LookupValue



   
END
/****** Object:  StoredProcedure [dbo].[IFS_PickUpXMLFiles]    Script Date: 04/03/2018 11:39:26 ******/
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

exec Job_PickUpXMLFiles




--exec IFS_LoadPurchaseOrders
--exec IFS_LoadPickList



Delete XMLFileHeader where StatusID = 1

Select * from XMLFileHeader
Select * from  IFSPurchaseOrderHeader
Select PONumberOrderNo, SKUPartNO, QTYOrderQTY, Condition_Code,IFSProject, * from  IFSPurchaseOrderDetail

Select * from OrderHeader
Select * from OrderDetail where OrderHeaderID = 4364



SELECT     IFSPickListOrderHeader.OrderHeaderID, IFSPickListOrderDetail.OrderHeaderID AS Expr1, IFSPickListOrderHeader.Site, IFSPickListOrderHeader.OrderNumber, 
                      IFSPickListOrderDetail.LINE_NO, IFSPickListOrderDetail.QTY_ASSIGNED, IFSPickListOrderDetail.QTYREL_NO, IFSPickListOrderDetail.IFSLocation, 
                      IFSPickListOrderDetail.SKUPART_NO, IFSPickListOrderDetail.Project_ID, IFSPickListOrderDetail.IFSCONDITION_CODE
  FROM IFSPickListOrderHeader 
 INNER JOIN IFSPickListOrderDetail ON IFSPickListOrderHeader.XMLFileHeaderID = IFSPickListOrderDetail.XMLFileHeaderID 
                                  AND IFSPickListOrderHeader.OrderNumber = IFSPickListOrderDetail.OrderNumber 
                                  
                                  
Select ReceiveDetailID, ESN, Version, SKU, IFSLocation, IFSCondition from ReceiveDetail where ESN = 'SAMI33716GATTWHT-K'                                  



SELECT     IFSPickListOrderHeader.OrderHeaderID, IFSPickListOrderDetail.OrderHeaderID AS Expr1, IFSPickListOrderHeader.Site, IFSPickListOrderHeader.OrderNumber, 
                      IFSPickListOrderDetail.LINE_NO, IFSPickListOrderDetail.QTY_ASSIGNED, IFSPickListOrderDetail.QTYREL_NO, IFSPickListOrderDetail.IFSLocation, 
                      IFSPickListOrderDetail.SKUPART_NO, IFSPickListOrderDetail.Project_ID, IFSPickListOrderDetail.IFSCONDITION_CODE, ReceiveDetail.ESN, 
                      ReceiveDetail.Version
  FROM IFSPickListOrderHeader 
 INNER JOIN IFSPickListOrderDetail ON IFSPickListOrderHeader.XMLFileHeaderID = IFSPickListOrderDetail.XMLFileHeaderID 
                                  AND IFSPickListOrderHeader.OrderNumber = IFSPickListOrderDetail.OrderNumber 
 INNER JOIN ReceiveDetail ON  -- IFSPickListOrderDetail.IFSLocation = ReceiveDetail.IFSLocation AND 
                             IFSPickListOrderDetail.IFSCONDITION_CODE = ReceiveDetail.IFSCondition 
 INNER JOIN ClientLocation ON ReceiveDetail.ClientLocationID = ClientLocation.ClientLocationID  
                          AND IFSPickListOrderDetail.Project_ID = ClientLocation.IFSProject 
                          AND IFSPickListOrderHeader.Site = ClientLocation.IFSSite




-- Update XMLFileHeader set FileType = 'PL' where StatusID = 1
/*
Delete XMLFileHeader
Delete IFSPurchaseOrderHeader
Delete IFSPurchaseOrderDetail
*/

*/

Create PROCEDURE [dbo].[Job_PickUpXMLFiles]


AS
BEGIN
	SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	


Declare @sql nvarchar(max)   
Declare @FolderLocation nvarchar(500)
Declare @FileName nvarchar(50)
Declare @XMLFile nvarchar(500)
Declare @FileType nvarchar(5)
--Select @FolderLocation = 'D:\FTP\Prod\SFC_In'
-- Select @FolderLocation = 'D:\IFSPOXMLFiles_Prod'
--Select @FolderLocation = 'ftp://brcit:Ingram.123@bright4.brightpoint.com/immcanadabrc/prod/in/'
-- Select @FolderLocation = 'D:\IFSPOXMLFiles_Sandbox'
--Select @FolderLocation = 'ftp://brcit:Ingram.123@bright4.brightpoint.com/immcanadabrc/test/in/'
Select @FolderLocation = 'C:\Temp\BlackBeltDropBox\In'

-- Grab the data from the FTP site and make ready for import.
----Exec xp_cmdshell 'powershell.exe -file D:\FTP\Script\DownloadFromIFS.ps1 -ExecutionPolicy Unrestricted'


-- Import.
/* Get All the Files in the folder */
IF OBJECT_ID('tempdb..#DirectoryTree') IS NOT NULL
   DROP TABLE #DirectoryTree;

CREATE TABLE #DirectoryTree (
       id int IDENTITY(1,1)
      ,subdirectory nvarchar(512)
      ,depth int
      ,isfile bit
      ,IsProcessed bit);

INSERT #DirectoryTree (subdirectory,depth,isfile)
EXEC master.sys.xp_dirtree @FolderLocation,1,1;

Update #DirectoryTree set IsProcessed = 0
Update #DirectoryTree set IsProcessed = 1 where isfile != 1 or RIGHT(subdirectory,4) != '.xml'

--SELECT * FROM #DirectoryTree
----WHERE isfile = 1 AND RIGHT(subdirectory,4) = '.XML'
--ORDER BY id

--return

while exists (Select * from #DirectoryTree where IsProcessed = 0)
      begin
      Select top 1 @FileName = subdirectory from #DirectoryTree where IsProcessed = 0 order by subdirectory
      Update #DirectoryTree set IsProcessed = 1 where subdirectory = @FileName
      Select @FileType = case when LEFT(@FileName, 9) = 'BlackBelt' then 'BB'                              
                              else 'UNK' end
      

      /*
      Declare @FileName nvarchar(50)
      Select @FileName = 'ifs_picklist001.xml'
      print case when LEFT(@FileName, 12) = 'ifs_picklist' then 'SO' when LEFT(@FileName, 17) = 'ifs_purchaseorder' then 'PO' else 'UNK' end
      
      PO :ifs_purchaseorder*.xml
      SO:ifs_picklist*.xml
      */  


      
      /* Place XLM into table */
      Select @XMLFile = @FolderLocation + '\' + @FileName
      print 'Loading:' + @XMLFile      
      /* The 'OPENROWSET' does not like variables. It needs string literals. to offset that problem, I have to create a string of the command and then execute that string. Pain in the Butt. */
      Select @sql = 'INSERT INTO XMLFileHeader(XMLData, FileName,FileType, StatusID, CreateDate,CreateUser,LastUpdateDate,LastUpdateUser)'
      Select @sql = @sql + ' SELECT CONVERT(XML, BulkColumn) AS BulkColumn, ''' + @FileName + ''',''' + @FileType + ''', 1, GETDATE(), ''SQLJob'',GETDATE(), ''SQLJob''' 
      Select @sql = @sql + ' FROM OPENROWSET(BULK ''' + @XMLFile + ''', SINGLE_BLOB) AS x;'
      Exec(@sql)



      /* Delete read XML file. */
      print 'Deleting file:' + @XMLFile
      Select @sql = 'xp_cmdshell ''' + 'del ' + @XMLFile + ''''
      Print @SQL
      Exec(@sql)

      End

 
END
Go


/****** Object:  StoredProcedure [dbo].[BlackBelt_ParseData]    Script Date: 04/06/2018 11:45:00 ******/
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
Declare @Message nvarchar(500)
Exec BlackBelt_ParseData 28, @Message output
Print @Message


Select * from BlackBeltTransHeader

*/


Create PROCEDURE [dbo].[BlackBelt_ParseData]
      @BlackbeltTransHeaderID numeric(18)
      , @Message nvarchar(500) OUTPUT

AS
BEGIN
	SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	

Select @Message = 'Succes: other text can go here'
Declare @BlackbeltTransDetailID numeric(18)

Declare @keyValue nvarchar(75)
Declare @ValueValue nvarchar(75)
Declare @ReturnValue nvarchar(75)

-- Select * from Project 

      -- Set all things back to Start
      Update BlackBeltTransHeader set ProcessStatus = 2, Status = 'Parse'
                             where BlackBeltTransHeaderID = @BlackbeltTransHeaderID 
                             
      -- See if we have an active IMEI out there if it is not already known.
      Update BlackBeltTransHeader set ReceiveDetailID = (Select top 1 ReceiveDetailID from ReceiveDetail B where B.ESN = BlackbeltTransHeader.ESN and B.Version = '000')
                             where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ISNULL(ReceiveDetailID, -1) < 1
                             
      -- Try and link up the ClientLocationID
      Update BlackBeltTransHeader set ClientLocationID = (Select ClientLocationID from ClientLocation where ScanKey = BlackbeltTransHeader.ClientLocationScanKey)
                             where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ISNULL(ClientLocationID, -1) < 1
                             
       -- Try and link up the ProjectID
      Update BlackBeltTransHeader set ProjectID = (Select ProjectID from Project where Name = BlackbeltTransHeader.ProjectName)
                             where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ISNULL(ProjectID, -1) < 1   
                                                      
       -- Try and link up the ProcessID
      Update BlackBeltTransHeader set ProcessID = (Select ProcessID from Process where ScanKey = BlackbeltTransHeader.ProcessScanKey)
                             where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and ISNULL(ProcessID, -1) < 1                            
      
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
    
      -- Move the ReceiveDetailID down to the lower levels in error or New as well as reset things back to start.
      Update BlackbeltTransDetail set ReceiveDetailID = H.ReceiveDetailID, ProcessStatus = 1, Status = 'Parse'
      From BlackbeltTransDetail D
      Inner join BlackbeltTransHeader H on D.BlackbeltTransHeaderID = H.BlackbeltTransHeaderID
      where D.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and  (D.Status = 'Error' or D.Status = 'New' or D.Status = 'Parse')

      
      -- Because of process Status, this should be all that were reset above      
      -- Translate the field names into Questions
      while exists(Select * from BlackBeltTransDetail D where D.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and D.ProcessStatus = 1)
        begin
        Select top 1 @BlackbeltTransDetailID = BlackbeltTransDetailID ,
                     @keyValue = 'Question',
                     @ValueValue = [Key]       
          from  BlackBeltTransDetail D where D.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and D.ProcessStatus = 1
           
        Exec Get_XMLTranslationValue  @keyValue, @ValueValue,  @ReturnValue OUTPUT 
        Update  BlackBeltTransDetail set processStatus = 2, TranslationKey = @ReturnValue  where BlackbeltTransDetailID = @BlackbeltTransDetailID
       end
      -- Translate the field Values into proper Brain Values.
     while exists(Select * from BlackBeltTransDetail D where D.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and D.ProcessStatus = 2)
       begin
       Select top 1 @BlackbeltTransDetailID = BlackbeltTransDetailID ,
                    @keyValue = TranslationKey,
                    @ValueValue = Value
              from  BlackBeltTransDetail D where D.BlackBeltTransHeaderID = @BlackbeltTransHeaderID and D.ProcessStatus = 2
               
       Exec Get_XMLTranslationValue  @keyValue, @ValueValue,  @ReturnValue OUTPUT 
       Update  BlackBeltTransDetail set processStatus = 3, TranslationValue = @ReturnValue  where BlackbeltTransDetailID = @BlackbeltTransDetailID
       end
  
    
       -- Start matching up the data elements. (First, ID the Questions)
     Update BlackbeltTransDetail set QuestionID = (Select QUestionID from Question where Name = BlackbeltTransDetail.TranslationKey), ProcessStatus = 4
        where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and [Status] = 'Parse' and ProcessStatus = 3
   
       -- We need to know what type of Question we are dealing with
     Update BlackbeltTransDetail set QuestionType = t.Type
         From BlackbeltTransDetail a
        inner join Question q on a.QuestionID = q.QuestionID
        inner join QuestionType t on q.QuestionTypeID = t.QuestionTypeID
        where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and [Status] = 'Parse' and ProcessStatus = 4
   
   
       -- Update anything that could be a dropdown, radio button etc. Anything that is a list.
     Update BlackbeltTransDetail set OptionID = x.OptionID, ItemAbbreviation = x.name,ProcessStatus = 5
         From BlackbeltTransDetail
        Inner join [option] x on BlackbeltTransDetail.QuestionID = x.QuestionID and BlackbeltTransDetail.TranslationValue = x.OptionText
        where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and [Status] = 'Parse' and ProcessStatus = 4     

       -- Update for keyboard etc types. The first option record is the one we have to match.
     Update BlackbeltTransDetail set OptionID = x.OptionID, ItemAbbreviation = BlackbeltTransDetail.TranslationValue,ProcessStatus = 5
         From BlackbeltTransDetail
        Inner join [option] x on BlackbeltTransDetail.QuestionID = x.QuestionID -- and BlackbeltTransDetail.TranslationValue = x.OptionText
        where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and [Status] = 'Parse' and ProcessStatus = 4 
          and QuestionType in ('Keyboard','Calendar','Calc','Numeric','currency','Num3Digit','Text20Digit','Text3Digit','Text10Digit','Text18Digit','Text50Digit' )
  --  
   
     Update BlackbeltTransDetail set Status = 'Error', Message = 'Unable to find proper Value in TheBrain'
        where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and [Status] = 'Parse' and ProcessStatus > 3 and OptionID is null -- and not QUestionID is null
        
     If exists(Select * from  BlackbeltTransDetail where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and [Status] = 'Error' )
         begin
         Select @Message = 'Error: Some Attributes not found'       
         end

      -- If we don't have a device already within the system, we need to get the data from the Detail.
      
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

      Update BlackBeltTransHeader set ProcessStatus = 10, Status = 'Parsed' where BlackBeltTransHeaderID = @BlackbeltTransHeaderID 
      Update BlackbeltTransDetail set ProcessStatus = 10, Status = 'Parsed' where BlackBeltTransHeaderID = @BlackbeltTransHeaderID and [Status] = 'Parse' and ProcessStatus > 3

--------------------------------------------------------------------------------------------------------------------
 
END
/****** Object:  StoredProcedure [dbo].[BlackBelt_ParseData]    Script Date: 04/06/2018 11:45:00 ******/
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
Declare @Message nvarchar(500)
Exec BlackBelt_ParseData 4, @Message output
Print @Message

*/


Create PROCEDURE [dbo].[BlackBelt_ParseDataEdit]
      @BlackbeltTransHeaderID numeric(18)
      , @Message nvarchar(500) OUTPUT

AS
BEGIN
	SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	

Select @Message = 'Succes: other text can go here'
Declare @BlackbeltTransDetailID numeric(18)

Declare @keyValue nvarchar(75)
Declare @ValueValue nvarchar(75)
Declare @ReturnValue nvarchar(75)
Declare @NewAdd int
Select @NewAdd = 0 ----------NO.
-- BlackBeltTransHeader.ProcessStatus should already be set to 10, Status Should = 'Parsed'
-- BlackbeltTransDetail.ProcessStatus should already be set to 10, Status Should = 'Parsed'
  
      -- Set all things back to Start
      Update BlackBeltTransHeader set ProcessStatus = 11, Status = 'edit' where BlackBeltTransHeaderID = @BlackbeltTransHeaderID 
      
      -- See if we are updating an IMEI or if we are Adding a new one.
      If Exists(Select * from BlackbeltTransHeader where  BlackBeltTransHeaderID = @BlackbeltTransHeaderID and isnull(ReceiveDetailID, -1) < 1)
         begin
         Select @NewAdd = 1 ---------- YES.
         end
      

      -- If we have a new add, we need to check this.
         -- Do we have a Client Location
         -- Do we have a project
         -- Do we have a receive Process
         -- Do we have a proper SKU Combo
         
         
         
    
--------------------------------------------------------------------------------------------------------------------
 
END


/****** Object:  StoredProcedure [dbo].[Job_LoadBlackBelt]    Script Date: 04/06/2018 15:29:16 ******/
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
Exec Job_LoadBlackBelt

Select * from BlackBeltTransHeader
Select * from BlackBeltTransDetail -- where BlackBeltTransHeaderID = 12

Select * from XMLFileHeader
Delete BlackBeltTransHeader
Delete BlackBeltTransDetail
Select * from QUestion where name = 'Carrier'
Select * from [option] where questionid = 210 and name = 'Gold'


*/


Create PROCEDURE [dbo].[Job_LoadBlackBelt]


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


DECLARE @XML AS XML, @hDoc AS INT, @SQL NVARCHAR (MAX)



While exists (Select * from XMLFileHeader where StatusID = 1 and FileType = 'BB')
      begin

      
DECLARE @TempHeaderb TABLE
       (Processed int,
        TempHeaderID numeric(18, 0) identity,
        ReceiveDetailID numeric(18, 0),
        BlackbeltTransHeaderID numeric(18, 0),
        XMLFileHeaderID numeric(18, 0),
        [IMEI] [nvarchar](75),
        [ClientLocation] [nvarchar](75),
        [ProjectName] [nvarchar](50),
        [ProcessScanKey] [nvarchar](50),
        [ProjectTag] [nvarchar](50),
	    [Carrier] [nvarchar](75),
	    [Manufacturer] [nvarchar](75),
	    [Model] [nvarchar](75),
	    [DeviceColor] [nvarchar](75),
	    [HandsetMemorySize] [nvarchar](75),	
	    [DeviceGrade] [nvarchar](75),	           	           
	    [StartDate] [nvarchar](75),
	    [StartTime] [nvarchar](75),
	    [UserName] [nvarchar](75),
	    [mMemoryCapacity] [nvarchar](75),
	    [mFMIPSamAccLock] [nvarchar](75),
	    [mUSB] [nvarchar](75),
	    [CarrierUnlocked] [nvarchar](75))     
      
Select * into #TempHeader from @TempHeaderb where TempHeaderID = -1      
      
      Select top 1 @XMLFileHeaderID = XMLFileHeaderID from XMLFileHeader where StatusID = 1 and FileType = 'BB' order by FileName
      Update XMLFileHeader set StatusID = 2 where XMLFileHeaderID = @XMLFileHeaderID
      SELECT @XML = XMLData FROM XMLFileHeader where XMLFileHeaderID = @XMLFileHeaderID
      --Print convert(nvarchar(max),@XML)
      --return
      Print 'Opening the document'
      EXEC sp_xml_preparedocument @hDoc OUTPUT, @XML

      
      
      Print 'Ready to Select from DataWipt'
         Insert #TempHeader (Processed
                             , XMLFileHeaderID
                             , IMEI
                             , ClientLocation
                             , ProjectName
                             , ProjectTag
                             , ProcessScanKey
                             , Carrier
                             , Manufacturer
                             , Model
                             , [DeviceColor]
                             , [HandsetMemorySize]
                             , [DeviceGrade]
                             , [StartDate] -- 
                             , [StartTime]
                             , [UserName]
                             , [mMemoryCapacity]
                             , [mFMIPSamAccLock]
                             , [mUSB]
                             , [CarrierUnlocked])
         SELECT 0, @XMLFileHeaderID
                             , IMEI
                             , 'BW1'
                             , 'Bridge Product'
                             , 'Project Tag'
                             , 'RAPO'
                             , 'Bell Mobility'
                             , Manufacturer
                             , Model
                             , [DeviceColor]
                             , [HandsetMemorySize]
                             , [DeviceGrade]
                             , [StartDate] -- 
                             , [StartTime]
                             , [UserName]
                             , [mMemoryCapacity]
                             , [mFMIPSamAccLock]
                             , [mUSB]
                             , [CarrierUnlocked]
         FROM OPENXML(@hDoc, 'DataWipe')
         WITH ([IMEI] [nvarchar](75) 'IMEI',
	           [Manufacturer] [nvarchar](75) 'Manufacturer',
	           [Model] [nvarchar](75) 'Model',
	           [DeviceColor] [nvarchar](75) 'DeviceColor',
	           [HandsetMemorySize] [nvarchar](75) 'HandsetMemorySize',		
	           [DeviceGrade] [nvarchar](75) 'DeviceGrade',		           	           
	           [StartDate] [nvarchar](75) 'StartDate', -- 		           	           
	           [StartTime] [nvarchar](75) 'StartTime', -- 	
	           [UserName] [nvarchar](75) 'UserName',
	           [mMemoryCapacity] [nvarchar](75) 'ManualEntry/mMemoryCapacity',
	           [mFMIPSamAccLock] [nvarchar](75) 'ManualEntry/mFMIPSamAccLock',
	           [mUSB] [nvarchar](75) 'ManualEntry/mUSB',
	           [CarrierUnlocked] [nvarchar](75) 'ManualEntry/CarrierUnlocked')
	           
	    while exists (Select * from #TempHeader where Processed = 0)       
	          begin
	          Select Top 1 @TempHeaderID = TempHeaderID from  #TempHeader where Processed = 0
	          Update #TempHeader set Processed = 1 where TempHeaderID = @TempHeaderID

              Select @CreateDate =  Convert(datetime, StartDate + ' ' + [StartTime], 103) from #TempHeader where TempHeaderID = @TempHeaderID
              -- Select * from #TempHeader where TempHeaderID = @TempHeaderID
	          
	          /* Create the BlackbeltTrans Record  */
	          Insert BlackBeltTransHeader (XMLFileHeaderID, ESN, ClientLocationScanKey
                             , ProjectName, ProcessScanKey
                             , ProjectTag,  Status, ProcessStatus, RequestUser, CreateUser, CreateDate, LastUpdateDate, LastUpdateUser)
	          Select XMLFileHeaderID, IMEI, ClientLocation
                             , ProjectName, ProcessScanKey
                             , ProjectTag, 'New', 1, UserName, UserName, @CreateDate, @CreateDate, UserName from #TempHeader where TempHeaderID = @TempHeaderID
	          Select @BlackbeltTransHeaderID = @@IDENTITY
	          
	          Update #TempHeader set BlackbeltTransHeaderID = @BlackbeltTransHeaderID
	          --                       ,ReceiveDetailID = (Select top 1 ReceiveDetailID from ReceiveDetail B where B.ESN = #TempHeader.IMEI and B.Version = '000')
	                                 where TempHeaderID = @TempHeaderID

	          /* Create the Detail Records for the Items we wish to keep. */
	          
	          
	          
	          Insert BlackbeltTransDetail (BlackbeltTransHeaderID, ReceiveDetailID, Status, ProcessStatus, [Key], Value, CreateUser, CreateDate, LastUpdateDate, LastUpdateUser)
	                 Select BlackbeltTransHeaderID, ReceiveDetailID, 'New', 1, 'Carrier',Carrier, UserName, @CreateDate, @CreateDate, UserName from #TempHeader where TempHeaderID = @TempHeaderID
	          Insert BlackbeltTransDetail (BlackbeltTransHeaderID, ReceiveDetailID, Status, ProcessStatus, [Key], Value, CreateUser, CreateDate, LastUpdateDate, LastUpdateUser)
	                 Select BlackbeltTransHeaderID, ReceiveDetailID, 'New', 1, 'Manufacturer',Manufacturer, UserName, @CreateDate, @CreateDate, UserName from #TempHeader where TempHeaderID = @TempHeaderID
	          Insert BlackbeltTransDetail (BlackbeltTransHeaderID, ReceiveDetailID, Status, ProcessStatus, [Key], Value, CreateUser, CreateDate, LastUpdateDate, LastUpdateUser)
	                 Select BlackbeltTransHeaderID, ReceiveDetailID, 'New', 1, 'Model',Model, UserName, @CreateDate, @CreateDate, UserName from #TempHeader where TempHeaderID = @TempHeaderID
	          Insert BlackbeltTransDetail (BlackbeltTransHeaderID, ReceiveDetailID, Status, ProcessStatus, [Key], Value, CreateUser, CreateDate, LastUpdateDate, LastUpdateUser)
	                 Select BlackbeltTransHeaderID, ReceiveDetailID, 'New', 1, 'Colour',DeviceColor, UserName, @CreateDate, @CreateDate, UserName from #TempHeader where TempHeaderID = @TempHeaderID
	          Insert BlackbeltTransDetail (BlackbeltTransHeaderID, ReceiveDetailID, Status, ProcessStatus, [Key], Value, CreateUser, CreateDate, LastUpdateDate, LastUpdateUser)
	                 Select BlackbeltTransHeaderID, ReceiveDetailID, 'New', 1, 'Memory', HandsetMemorySize, UserName, @CreateDate, @CreateDate, UserName from #TempHeader where TempHeaderID = @TempHeaderID
	          Insert BlackbeltTransDetail (BlackbeltTransHeaderID, ReceiveDetailID, Status, ProcessStatus, [Key], Value, CreateUser, CreateDate, LastUpdateDate, LastUpdateUser)
	                 Select BlackbeltTransHeaderID, ReceiveDetailID, 'New', 1, 'Grade', DeviceGrade, UserName, @CreateDate, @CreateDate, UserName from #TempHeader where TempHeaderID = @TempHeaderID
	          --Insert BlackbeltTransDetail (BlackbeltTransHeaderID, ReceiveDetailID, Status, ProcessStatus, [Key], Value, CreateUser, CreateDate, LastUpdateDate, LastUpdateUser)
	          --       Select BlackbeltTransHeaderID, ReceiveDetailID, 'New', 1, 'StartDate', StartDate, UserName, @CreateDate, @CreateDate, UserName from #TempHeader where TempHeaderID = @TempHeaderID	 
	          --Insert BlackbeltTransDetail (BlackbeltTransHeaderID, ReceiveDetailID, Status, ProcessStatus, [Key], Value, CreateUser, CreateDate, LastUpdateDate, LastUpdateUser)
	          --       Select BlackbeltTransHeaderID, ReceiveDetailID, 'New', 1, 'UserName', UserName, UserName, @CreateDate, @CreateDate, UserName from #TempHeader where TempHeaderID = @TempHeaderID	 	          
	          Insert BlackbeltTransDetail (BlackbeltTransHeaderID, ReceiveDetailID, Status, ProcessStatus, [Key], Value, CreateUser, CreateDate, LastUpdateDate, LastUpdateUser)
	                 Select BlackbeltTransHeaderID, ReceiveDetailID, 'New', 1, 'mUSB', mUSB, UserName, @CreateDate, @CreateDate, UserName from #TempHeader where TempHeaderID = @TempHeaderID	 	          



              EXEC BlackBelt_ParseData  @BlackbeltTransHeaderID, @Message output
              Update BlackbeltTransHeader set Message = @Message 
                                             --,Status = Case when Substring(@Message, 1, 5) = 'Error' then 'Error' else 'New' end
----Print Substring('Error: other text can go here',1,5)      
-- Print Convert(datetime, '28 February 2018' + ' ' + '17:03:51', 103)                                       
--Print STR_TO_DATE('28 February 2018', '%d %M %Y')
--SELECT STR_TO_DATE('August,5,2017', '%M %e %Y')
               where BlackbeltTransHeaderID = @BlackbeltTransHeaderID
	          end
	          
	          
        Delete #TempHeader
        EXEC sp_xml_removedocument @hDoc
        
                             
                                     
        Drop Table #TempHeader
                                     	                                 
      END
--------------------------------------------------------------------------------------------------------------------
 
END












































































