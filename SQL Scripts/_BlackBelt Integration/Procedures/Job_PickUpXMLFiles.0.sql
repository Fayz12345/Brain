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
