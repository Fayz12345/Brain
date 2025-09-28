
/****** Object:  StoredProcedure [dbo].[GetMasterDetailCellbieList_TemplateRawData_SearchGrid_03]    Script Date: 10/16/2019 11:48:20 ******/
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

Exec GetMasterDetailCellbieList_TemplateRawData_SearchGrid_03 'Error'
Exec GetMasterDetailCellbieList_TemplateRawData_SearchGrid_03 'Send'
Exec GetMasterDetailCellbieList_TemplateRawData_SearchGrid_03 'Success'

 Drop Procedure GetMasterDetailCellbieList_TemplateRawData_SearchGrid_03

*/

Create PROCEDURE [dbo].[GetMasterDetailCellbieList_TemplateRawData_SearchGrid_03]

      @mStatus nvarchar(50) = ''

AS
BEGIN
	SET NOCOUNT ON;
	
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED


-- Print @mIFSLocation

	
--Select @mShowGraveyard = case when @mShowGraveyard = 'Y' then 'Y' else 'N' end
	
--print 'Start:' + Convert(varchar(30),getdate(), 121)	
--      WAITFOR DELAY '00:00:30'
-- print 'Wait:' + Convert(varchar(20),getdate(), 120)	

Declare @mSaveID numeric(15)
Select Top 1 @mSaveID = ProcessID from Process where process.name = 'Save'
	
----if @mProjectName = 'All'
----   Select @mProjectName = ''
   		
--Declare @mClientLocationID numeric(18)
--Select 	@mClientLocationID = -1
--if len(ltrim(rtrim(@mClientCode))) > 0
--   Select @mClientLocationID = ClientLocationID from ClientLocation where scankey = @mClientCode

--Select @mClientLocationID = isnull(@mClientLocationID,-1)   
--if len(rtrim(ltrim(@mClientCode))) > 0 and @mClientLocationID = -1
--   select @mClientLocationID = -2

--Declare @dReceiveBeginDate Datetime
--Declare @dReceiveEndDate Datetime
--Select @dReceiveBeginDate = convert(datetime,@mReceiveBeginDate,101)
--Select @dReceiveEndDate = convert(datetime,@mReceiveEndDate,101)
--Select @dReceiveEndDate = dateadd(d,1,@dReceiveEndDate)

--Declare @mGraveYardStatusID numeric(18)
--Select @mGraveYardStatusID = ReceiveDetailStatusID FROM  ReceiveDetailStatus where Status = 'GraveYard'

--Declare @mProjectID numeric(18)
--Select @mProjectID = ProjectID from Project where Name = @mProjectName
--Select @mProjectID = isnull(@mProjectID, -1)
--if len(rtrim(ltrim(@mProjectName))) > 0 and @mProjectID = -1
--   select @mProjectID = -2
   
CREATE TABLE #xTemp(
	[source] [varchar](6) COLLATE Latin1_General_CI_AS NULL,
    [ReceiveHeaderID] [numeric] (18,0),
    [ReceiveDetailBulkID] [numeric] (18,0),
    [ReceiveDetailID] [numeric] (18,0),
    [ClientLocationID] [numeric] (18,0),
    [ProjectID] [numeric] (18,0),
	[ProjectName] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[Name] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[StoreNumber] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL,
	[StoreSuffix] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL,
	[Sequence] [int] NULL,
	[CompanyName] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[Process] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL,
	[Status] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL,
	[QTYPaper] [numeric](18, 0) NULL,
	[QTYRecorded] [numeric](18, 0) NULL,
	[QTYIntegrated] [numeric](18, 0) NULL,
	[ReceiveDate] [datetime] NOT NULL,
	[ReceiveDate_Date] nvarchar(10) NULL,
	[ReceiveDate_Time] nvarchar(10) NULL,	
	
	[WayBill] [nvarchar](500) COLLATE Latin1_General_CI_AS NULL,
	[ShipDate] [datetime] NULL,	
	[RMANumber] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[ProjectTag] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[MakeModelString] [nvarchar](200) COLLATE Latin1_General_CI_AS NULL,
	[ESN] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[Version] [nchar](3) COLLATE Latin1_General_CI_AS NULL,		
	[SwappedESN] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
--	[PIN] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[Date_QC] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL,

	[ML_Condition] [nvarchar](20) NULL,	
	[ML_NickName] [nvarchar](20) NULL,	
	[ML_Description] [nvarchar](200) NULL,
	[ML_UPC] [nvarchar](20) NULL,
	[ML_SKU] [nvarchar](20) NULL,
	[ML_SKU_B] [nvarchar](20) NULL,
	[ML_SKU_C] [nvarchar](20) NULL,		
	[ML_SKU_Loaner] [nvarchar](20) NULL,	
	[ML_WarrantyStickerPlacement] [nvarchar](200) NULL,
	[ML_Device_Handset] [nvarchar](50) NULL,
	[ML_Bar_Flip] [nvarchar](20) NULL,
	[ML_CDMA_HSPA] [nvarchar](50) NULL,
	[ML_Retire] [nvarchar](20) NULL	,
	[LastUpdateDate] [datetime] Not NULL,
	[LastUpdateUser] [nvarchar](50) Not NULL
	
	
	
	
	
	)

Create Index Temp_01 on #xTemp (ReceiveDetailID)
Create Index Temp_02 on #xTemp (ProjectID)



CREATE TABLE #TempZX(
	[ReceiveDetailID] [numeric](18, 0) NULL,
	[CurrentLogID] [numeric](18, 0) NULL,
	[StartLogID] [numeric](18, 0) NULL,
    )
    
Create Index Temp_10 on #Tempzx([ReceiveDetailID])    
Create Index Temp_11 on #Tempzx([CurrentLogID])
Create Index Temp_12 on #Tempzx([StartLogID])


CREATE TABLE #TempRD(
	[CellbieStatus] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL,
	[MiscText] [nvarchar](500) COLLATE Latin1_General_CI_AS NULL,
	[LastUpdateDate_Cellbie] [datetime] NOT NULL,
	
	
	[SendParamAgree] nVarchar(20) NULL,
	[SendParamMessage] nVarchar(200) NULL,
	[SendReturnMessage] nVarchar(200) NULL,
	

	[ReceiveDetailID] [numeric](18, 0) NULL,
	[ProcessID] [numeric](18, 0) NULL,
	[CurrentProcessID] [numeric](18, 0) NULL,
	[StartProcessID] [numeric](18, 0) NULL,
	[CurrentProcessName] nVarchar(20) NULL,
	[StartProcessName] nVarchar(20) NULL,
	[ESN] nvarchar(50),
	[Status] nvarchar(20),
	[Process] numeric(15) NULL,
	[SKU] nvarchar(50) NULL,
	[IFSLocation] nvarchar(20) NULL,
	[IFSCondition] nvarchar(50) NULL,
	[BIN] nvarchar(20) NULL,	
	[LastUpdateDate] [datetime] NULL,
	[LastUpdateUser] [nvarchar](50) NULL
    )

Create Index Temp_03 on #TempRD (ReceiveDetailID)
Create Index Temp_04 on #TempRD (ProcessID)

CREATE TABLE #TempBin(
	[ReceiveDetailID] [numeric](18, 0) NULL
    )

Create Index Temp_25 on #TempBin (ReceiveDetailID)
	

 Insert #TempRD (CellbieStatus, MiscText, LastUpdateDate_Cellbie,
        ReceiveDetailID, ProcessID,CurrentProcessID,StartProcessID,ESN,Status, Process, SKU, IFSLocation, IFSCondition,[SendParamAgree],[SendParamMessage],[SendReturnMessage])
 Select ReceiveDetail.Status, MiscText, LastUpdateDate_Cellbie,
        ReceiveDetail.ReceiveDetailID, ProcessID,@mSaveID,@mSaveID,ReceiveDetail.ESN,ReceiveDetailStatus.Status,0, ReceiveDetail.SKU, ReceiveDetail.IFSLocation, ReceiveDetail.IFSCondition,'','',''
   From vwReceiveDetailCellbie as ReceiveDetail
  INNER JOIN ReceiveDetailStatus ON ReceiveDetail.StatusID = ReceiveDetailStatus.ReceiveDetailStatusID    
  --INNER JOIN OrderDetailReceiveDetail ON ReceiveDetail.ReceiveDetailID = OrderDetailReceiveDetail.ReceiveDetailID 
  --INNER JOIN OrderDetail ON OrderDetailReceiveDetail.OrderDetailID = OrderDetail.OrderDetailID 
  --INNER JOIN OrderHeader ON OrderDetail.OrderHeaderID = OrderHeader.OrderHeaderID
  WHERE     (ReceiveDetail.Status=@mStatus)
      
--Select * from #TempRD
--return  
-- We want to get the start and Current process (Min/Max).

Insert #TempZX
Select #TempRD.ReceiveDetailID, Max(ReceiveDetailProcessLog.ReceiveDetailProcessLogID), Min(ReceiveDetailProcessLog.ReceiveDetailProcessLogID)
  From #TempRD
 Inner join ReceiveDetailProcessLog on ReceiveDetailProcessLog.ReceiveDetailID = #TempRD.ReceiveDetailID
 Inner Join Process on Process.ProcessID = ReceiveDetailProcessLog.ProcessID
 Where process.name != 'Save'
 Group By #TempRD.ReceiveDetailID
 
Update #TempRD set [StartProcessID] = Process.ProcessID, StartProcessName = Process.Name
  From #TempRD
 Inner join #TempZX on #TempZX.ReceiveDetailID = #TempRD.ReceiveDetailID
 Inner join ReceiveDetailProcessLog on ReceiveDetailProcessLog.ReceiveDetailProcessLogID = #TempZX.[StartLogID]
 Inner Join Process on Process.ProcessID = ReceiveDetailProcessLog.ProcessID

Update #TempRD set CurrentProcessID = Process.ProcessID, CurrentProcessName = Process.Name, LastUpdateDate = ReceiveDetailProcessLog.CreateDate, LastUpdateUser = ReceiveDetailProcessLog.CreateUser
  From #TempRD
 Inner join #TempZX on #TempZX.ReceiveDetailID = #TempRD.ReceiveDetailID
 Inner join ReceiveDetailProcessLog on ReceiveDetailProcessLog.ReceiveDetailProcessLogID = #TempZX.[CurrentLogID]
 Inner Join Process on Process.ProcessID = ReceiveDetailProcessLog.ProcessID

Delete #TempRD where LastUpdateDate is  null

--Select * from #TempRD
--return 
----------------------------------------------------------
 
insert #xTemp
	SELECT 'Detail' as source, 
			ReceiveDetail.ReceiveHeaderID, 
			ReceiveDetail.ReceiveDetailBulkID, 
			ReceiveDetail.ReceiveDetailID,
			ClientLocation.ClientLocationID,
			ReceiveDetail.ProjectID, 	
			Project.Name,		
			ClientLocation.Name, 
			ClientLocation.StoreNumber, 
			ClientLocation.StoreSuffix, 
			ClientLocation.Sequence, 
			ClientLocation.CompanyName, 
			convert(varchar(20),'') as Process,
			ReceiveDetailStatus.Status,
			convert(numeric(18),0) as QTYPaper, 
			ReceiveDetail.QTYIntegrated as QTYRecorded, 
			ReceiveDetail.QTYIntegrated, 
			ReceiveDetail.CreateDate, 
			convert(nvarchar(10),ReceiveDetail.CreateDate,111),
			convert(nvarchar(10),ReceiveDetail.CreateDate,108), 

			'',
			Null,
			ReceiveDetail.RMANumber, 
			ReceiveDetail.ProjectTag, 
			dbo.GetCarrierMakeModelColourAnswerString(#TempRD.ReceiveDetailID),
			ReceiveDetail.ESN, ReceiveDetail.Version,'','',            --  ReceiveDetail.ICB, ReceiveDetail.PIN,
			'','','','','','','','','','','','','',
			#TempRD.LastUpdateDate,
			#TempRD.LastUpdateUser

	   FROM #TempRD 
	  INNER JOIN ReceiveDetail ON #TempRD.ReceiveDetailID = ReceiveDetail.ReceiveDetailID 
      INNER JOIN ReceiveDetailStatus ON ReceiveDetail.StatusID = ReceiveDetailStatus.ReceiveDetailStatusID	  
	  INNER JOIN ClientLocation ON ReceiveDetail.ClientLocationID = ClientLocation.ClientLocationID
	  INNER JOIN Project on ReceiveDetail.ProjectId = Project.ProjectID
	  order by ClientLocation.Name, Project.Name, RMANumber, ProjectTag, Process, ESN

--Select * from #xTemp 
--return 


Update #xTemp set [Date_QC] = dbo.GetReceivedQuestionAnswerString_03(#xTemp.ReceiveDetailID, 'Date QC')
  from #xTemp 


UPdate #xTemp set WayBill = OrderHeader.WaybillNumber, ShipDate = OrderHeader.Shippeddate
  From #xTemp
 INNER JOIN OrderDetailReceiveDetail ON #xTemp.ReceiveDetailID = OrderDetailReceiveDetail.ReceiveDetailID 
 INNER JOIN OrderDetail ON OrderDetailReceiveDetail.OrderDetailID = OrderDetail.OrderDetailID 
 INNER JOIN OrderHeader ON OrderDetail.OrderHeaderID = OrderHeader.OrderHeaderID 
 
 
 UPdate #xTemp set WayBill = 'Process Shipped', ShipDate = 
( SELECT TOP (1) ReceiveDetailProcessLog.CreateDate
    FROM ReceiveDetailProcessLog 
   INNER JOIN Process ON ReceiveDetailProcessLog.ProcessID = Process.ProcessID
   WHERE (Process.Name = 'Shipping') AND (ReceiveDetailProcessLog.ReceiveDetailID = #xTemp.ReceiveDetailID))
Where ShipDate is null   


----------------------------------------------------------------

   
Update #xTemp set SwappedESN = 
 (Select Top 1 IMEISwappedOut 
    From #xTemp z
   Inner join ReceiveDetailIMEISwappedLog on z.ReceiveDetailID = ReceiveDetailIMEISwappedLog.ReceiveDetailID
   where z.ReceiveDetailID = #xTemp.ReceiveDetailID order by CreateDate Desc);   



   
   
Select #TempRD.CellbieStatus,
       #TempRD.MiscText,
       #TempRD.LastUpdateDate_Cellbie,
       #TempRD.[SendParamAgree],#TempRD.[SendParamMessage],#TempRD.[SendReturnMessage],
       #xTemp.source,
       #xTemp.ReceiveHeaderID,
       #xTemp.ReceiveDetailBulkID,
       #xTemp.ReceiveDetailID,
       #xTemp.ClientLocationID,
       #xTemp.ProjectID,
       
       #xTemp.Name,
       #xTemp.StoreNumber,
       #xTemp.StoreSuffix,
       #xTemp.Sequence,
       #xTemp.CompanyName,
       #xTemp.ProjectName,
       #xTemp.Status,
       #TempRD.StartProcessName,
       #TempRD.CurrentProcessName,

       -- #xTemp.QTYPaper,
       -- #xTemp.QTYRecorded,
       #xTemp.QTYIntegrated,
       #xTemp.MakeModelString,
       #xTemp.ESN,
       #xTemp.Version,
       #xTemp.SwappedESN,
       #xTemp.RMANumber,
       #xTemp.ProjectTag,
       #xTemp.WayBill,
       #xTemp.ReceiveDate,
       #xTemp.ReceiveDate_Date,
       #xTemp.ReceiveDate_Time,

       #xTemp.ShipDate,
       #xTemp.Date_QC,

       #xTemp.ML_SKU,
       #xTemp.ML_UPC,
       #xTemp.ML_Description,
       #xTemp.ML_WarrantyStickerPlacement,
       #xTemp.ML_Device_Handset,
       #xTemp.ML_Bar_Flip,
       #xTemp.ML_CDMA_HSPA,
       #xTemp.ML_Retire,
       #TempRD.SKU,
       #TempRD.IFSLocation,       
       --dbo.GetReceivedQuestionAnswerString_03(#xTemp.ReceiveDetailID, 'Storage Location') as IFSLocation,
       #TempRD.IFSCondition,
       #xTemp.LastUpdateDate,
       #xTemp.LastUpdateUser
       
--into vwReceiveDetailCellbie_Grid
  from #xTemp
Inner join #TempRD on #xTemp.ReceiveDetailID = #TempRD.ReceiveDetailID;





-- print 'Data Out:' + Convert(varchar(20),getdate(), 120)	
Drop Table #TempRD
--Drop Table #temp123
--Drop Table #temp123_4
Drop Table #xTemp
Drop Table #TempZX
-- Drop Table #Temp321

END
