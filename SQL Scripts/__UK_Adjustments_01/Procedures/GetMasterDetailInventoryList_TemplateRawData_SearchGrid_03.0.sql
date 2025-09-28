
/****** Object:  StoredProcedure [dbo].[GetMasterDetailInventoryList_TemplateRawData_SearchGrid_03]    Script Date: 5/27/2020 11:42:23 AM ******/
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

GetMasterDetailInventoryList_TemplateRawData_SearchGrid_03 '','','','','','','','','','','10','','','','','','','','','','',''
352815040258068
355192040053163
355192040141281
351961040078576
352815040671096
352815040603891
357899040116448
357899040015616
357899040100236
Select * from ReceiveDetail where Version = '000'

GetMasterDetailInventoryList_TemplateRawData_SearchGrid_03 ''
                                                       ,'','','','',''
                                                       ,'','','','',''
                                                       ,'','',''
                                                       ,''
                                                       ,'','',''
                                                       ,'','','N'
                                                       ,'','','','REC_OUT',''
                                                       
                                                       
                                                       
                                                       
GetMasterDetailInventoryList_TemplateRawData_SearchGrid_03_Test '','','','','','','','','','','','','','','SwappedOut','','','','','','',''


358566041284835
358566047223134
                                                        1  2  3  4  5  6  7  8  9  10 11 12 13 14 15      16 17 18 19 20 21 22

Select ESN from ReceiveDetail
 

*/

ALTER PROCEDURE [dbo].[GetMasterDetailInventoryList_TemplateRawData_SearchGrid_03]

      @mProjectName nvarchar(50) = '',      -- 1
      @mClientCode nvarchar(50) = '',        -- Location/Dealer	
      @mRMANumber nvarchar(50) = '',
      @mProjectTag nvarchar(50) = '',
      @mReceiveBeginDate nvarchar(10) = '',  -- Rx Date	
      @mReceiveEndDate nvarchar(10) = '',
      @mQCBeginDate nvarchar(10) = '',    
      @mQCEndDate nvarchar(10) = '',  
      @mShippedBeginDate nvarchar(10) = '',    
      @mShippedEndDate nvarchar(10) = '',
      @mBinNumber nvarchar(50) = '',  
      @mHobble nvarchar(50) = '',  
      -- Aditional Template fields.
      @mStatus nvarchar(50) = '',
      @mClient nvarchar(50) = '',  
      @mIMEI nvarchar(50) = '',  	
      @mCarrier nvarchar(50) = '',  
      @mManufacturer nvarchar(50) = '',  	
      @mModel nvarchar(50) = '',  
      @mColour nvarchar(50) = '',  
      @mSKU nvarchar(50) = '',  
      
      @mShowGraveyard char(1) = 'N',
      @mUserName nvarchar(50) = '',           
      @mReplacementIMEI nvarchar(50) = '',
      @mIFSSku nvarchar(50) = '',
      @mIFSLocation nvarchar(50) = '',
      @mIFSCondition nvarchar(50) = ''

AS
BEGIN
	SET NOCOUNT ON;
	
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED


Print @mIFSLocation

	
Select @mShowGraveyard = case when @mShowGraveyard = 'Y' then 'Y' else 'N' end
	
--print 'Start:' + Convert(varchar(30),getdate(), 121)	
--      WAITFOR DELAY '00:00:30'
-- print 'Wait:' + Convert(varchar(20),getdate(), 120)	

Declare @mSaveID numeric(15)
Select Top 1 @mSaveID = ProcessID from Process where process.name = 'Save'
	
if @mProjectName = 'All'
   Select @mProjectName = ''
   		
Declare @mClientLocationID numeric(18)
Select 	@mClientLocationID = -1
if len(ltrim(rtrim(@mClientCode))) > 0
   Select @mClientLocationID = ClientLocationID from ClientLocation where scankey = @mClientCode

Select @mClientLocationID = isnull(@mClientLocationID,-1)   
if len(rtrim(ltrim(@mClientCode))) > 0 and @mClientLocationID = -1
   select @mClientLocationID = -2

Declare @dReceiveBeginDate Datetime
Declare @dReceiveEndDate Datetime
Select @dReceiveBeginDate = convert(datetime,@mReceiveBeginDate,101)
Select @dReceiveEndDate = convert(datetime,@mReceiveEndDate,101)
Select @dReceiveEndDate = dateadd(d,1,@dReceiveEndDate)

Declare @mGraveYardStatusID numeric(18)
Select @mGraveYardStatusID = ReceiveDetailStatusID FROM  ReceiveDetailStatus where Status = 'GraveYard'

Declare @mProjectID numeric(18)
Select @mProjectID = ProjectID from Project where Name = @mProjectName
Select @mProjectID = isnull(@mProjectID, -1)
if len(rtrim(ltrim(@mProjectName))) > 0 and @mProjectID = -1
   select @mProjectID = -2
   
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

--CREATE TABLE #Temp123(
--	[ReceiveDetailID] [numeric](18, 0) NULL,
--	[ReceiveDetailItemID] [numeric](18, 0) NULL,	
--	[Variable] [nvarchar](20) NOT NULL,
--	[VariableValue] [nvarchar](500) NULL,
--	[OptionID] [numeric](18, 0) NULL,		
--	[QuestionID] [numeric](18, 0) NULL,
--	[QuestionType] [nvarchar](20) NULL,
--	[QuestionName] [nvarchar](20) NULL,
--	[Date_QC] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL
--    ) 
--Create Index Temp_02 on #Temp123 (ReceiveDetailID)

--CREATE TABLE #Temp123_4(
--	-- [ReceiveDetailID] [numeric](18, 0) NULL,
--	[ID] [numeric](18, 0) NULL,	
--	[Variable] [nvarchar](20) NOT NULL,
--	[VariableValue] [nvarchar](500) NULL,
--    ) 
--Create Index Temp_22 on #Temp123_4 (ID)

----CREATE TABLE #Temp123(
----	[ReceiveDetailID] [numeric](18, 0) NULL,
----	[Variable] [nvarchar](20) NOT NULL,
----	[VaribleValue] [nvarchar](50) NULL,
----	[QuestionID] [numeric](18, 0) NULL,
----	[AnswerString] [nvarchar(50) NULL
----		
----)


CREATE TABLE #TempZX(
	[ReceiveDetailID] [numeric](18, 0) NULL,
	[CurrentLogID] [numeric](18, 0) NULL,
	[StartLogID] [numeric](18, 0) NULL,
    )
    
Create Index Temp_10 on #Tempzx([ReceiveDetailID])    
Create Index Temp_11 on #Tempzx([CurrentLogID])
Create Index Temp_12 on #Tempzx([StartLogID])


CREATE TABLE #TempRD(
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

--CREATE TABLE #TempPR(
--	[ReceiveDetailID] [numeric](18, 0) NULL,
--	[ProcessID] [numeric](18, 0) NULL,
--	[ProcessName] nVarchar(20) NULL,	
--    )

--Create Index Temp_05 on #TempPR (ReceiveDetailID)
--Create Index Temp_06 on #TempPR ([ProcessID])

CREATE TABLE #TempBin(
	[ReceiveDetailID] [numeric](18, 0) NULL
    )

Create Index Temp_25 on #TempBin (ReceiveDetailID)



if (LEN(RTRIM(ltrim(@mHobble))) > 0)
    begin
    Insert #TempRD (ReceiveDetailID, ProcessID,CurrentProcessID,StartProcessID,ESN,Status, Process, SKU, IFSLocation, IFSCondition)
   Select ReceiveDetail.ReceiveDetailID, ProcessID,@mSaveID,@mSaveID,ReceiveDetail.ESN,Status,0, ReceiveDetail.SKU, ReceiveDetail.IFSLocation, ReceiveDetail.IFSCondition
     From ReceiveDetail
    INNER JOIN ReceiveDetailStatus ON ReceiveDetail.StatusID = ReceiveDetailStatus.ReceiveDetailStatusID    
    INNER JOIN OrderDetailReceiveDetail ON ReceiveDetail.ReceiveDetailID = OrderDetailReceiveDetail.ReceiveDetailID 
    INNER JOIN OrderDetail ON OrderDetailReceiveDetail.OrderDetailID = OrderDetail.OrderDetailID 
    INNER JOIN OrderHeader ON OrderDetail.OrderHeaderID = OrderHeader.OrderHeaderID
    WHERE     (OrderHeader.OrderNumber = @mHobble)
     and (ReceiveDetail.ProjectID = @mProjectID or @mProjectID = -1) 
     and (ReceiveDetail.ClientLocationID = @mClientLocationID or @mClientLocationID = -1) 
    end
    
if (LEN(RTRIM(ltrim(@mHobble))) = 0)
    begin
if isdate(@mReceiveBeginDate) = 0
   Insert #TempRD (ReceiveDetailID, ProcessID,CurrentProcessID,StartProcessID,ESN,Status, Process, SKU, IFSLocation, IFSCondition)
   Select ReceiveDetailID, ProcessID,@mSaveID,@mSaveID,ESN,Status,0, ReceiveDetail.SKU, ReceiveDetail.IFSLocation, ReceiveDetail.IFSCondition
     From ReceiveDetail
    INNER JOIN ReceiveDetailStatus ON ReceiveDetail.StatusID = ReceiveDetailStatus.ReceiveDetailStatusID     
     
   Where (ReceiveDetail.ProjectID = @mProjectID or @mProjectID = -1) 
     and (ReceiveDetail.ClientLocationID = @mClientLocationID or @mClientLocationID = -1) 
     and ((@mShowGraveyard = 'N' and ReceiveDetail.StatusID != @mGraveYardStatusID)
      or  (@mShowGraveyard = 'Y' and ReceiveDetail.StatusID = @mGraveYardStatusID))
      
     and ((LEN(@mIMEI) > 0 and ReceiveDetail.ESN = @mIMEI) or (LEN(@mIMEI) = 0))      
     and ((LEN(@mIFSSku) > 0 and ReceiveDetail.SKU = @mIFSSku) or (LEN(@mIFSSku) = 0))
     and ((LEN(@mSku) > 0 and ReceiveDetail.SKU = @mSku) or (LEN(@mSku) = 0))             
     and ((LEN(@mIFSLocation) > 0 and dbo.GetReceivedQuestionAnswerString_03(ReceiveDetail.ReceiveDetailID, 'Storage Location') = @mIFSLocation) or (LEN(@mIFSLocation) = 0))      
     and ((LEN(@mIFSCondition) > 0 and ReceiveDetail.IFSCondition = @mIFSCondition) or (LEN(@mIFSCondition) = 0)) 
     
                        
     
if isdate(@mReceiveBeginDate) = 1
   Insert #TempRD (ReceiveDetailID, ProcessID,CurrentProcessID,StartProcessID,ESN,Status, Process, SKU, IFSLocation, IFSCondition)
   Select ReceiveDetailID, ProcessID,@mSaveID,@mSaveID,ESN,Status,0, ReceiveDetail.SKU, ReceiveDetail.IFSLocation, ReceiveDetail.IFSCondition
     From ReceiveDetail
    INNER JOIN ReceiveDetailStatus ON ReceiveDetail.StatusID = ReceiveDetailStatus.ReceiveDetailStatusID     
    
   Where (ReceiveDetail.ProjectID = @mProjectID or @mProjectID = -1) 
     and (ReceiveDetail.ClientLocationID = @mClientLocationID or @mClientLocationID = -1) 
     and ((@mShowGraveyard = 'N' and ReceiveDetail.StatusID != @mGraveYardStatusID)
      or  (@mShowGraveyard = 'Y' and ReceiveDetail.StatusID = @mGraveYardStatusID))
      
     and ((LEN(@mIMEI) > 0 and ReceiveDetail.ESN = @mIMEI) or (LEN(@mIMEI) = 0))      
     and ((LEN(@mIFSSku) > 0 and ReceiveDetail.SKU = @mIFSSku) or (LEN(@mIFSSku) = 0))  
     and ((LEN(@mSku) > 0 and ReceiveDetail.SKU = @mSku) or (LEN(@mSku) = 0))       
     and ((LEN(@mIFSLocation) > 0 and dbo.GetReceivedQuestionAnswerString_03(ReceiveDetail.ReceiveDetailID, 'Storage Location') = @mIFSLocation) or (LEN(@mIFSLocation) = 0))      
     and ((LEN(@mIFSCondition) > 0 and ReceiveDetail.IFSCondition = @mIFSCondition) or (LEN(@mIFSCondition) = 0))      
      
     and ReceiveDate >= @dReceiveBeginDate and ReceiveDate < @dReceiveEndDate
   END     
   




 

--if isdate(@mReceiveBeginDate) = 0
--   Insert #TempRD (ReceiveDetailID, ProcessID,CurrentProcessID,StartProcessID,ESN,Status, Process)
--   Select ReceiveDetailID, ProcessID,@mSaveID,@mSaveID,ESN,Status,0
--     From ReceiveDetail
--    INNER JOIN ReceiveDetailStatus ON ReceiveDetail.StatusID = ReceiveDetailStatus.ReceiveDetailStatusID     
--     
--   Where (ReceiveDetail.ProjectID = @mProjectID or @mProjectID = -1) 
--     and (ReceiveDetail.ClientLocationID = @mClientLocationID or @mClientLocationID = -1) 
--     and ((@mShowGraveyard = 'N' and ReceiveDetail.StatusID != @mGraveYardStatusID)
--      or  (@mShowGraveyard = 'Y' and ReceiveDetail.StatusID = @mGraveYardStatusID))
--     
--if isdate(@mReceiveBeginDate) = 1
--   Insert #TempRD (ReceiveDetailID, ProcessID,CurrentProcessID,StartProcessID,ESN,Status, Process)
--   Select ReceiveDetailID, ProcessID,@mSaveID,@mSaveID,ESN,Status, 0
--     From ReceiveDetail
--    INNER JOIN ReceiveDetailStatus ON ReceiveDetail.StatusID = ReceiveDetailStatus.ReceiveDetailStatusID     
--   Where (ReceiveDetail.ProjectID = @mProjectID or @mProjectID = -1) 
--     and (ReceiveDetail.ClientLocationID = @mClientLocationID or @mClientLocationID = -1) 
--     and ((@mShowGraveyard = 'N' and ReceiveDetail.StatusID != @mGraveYardStatusID)
--      or  (@mShowGraveyard = 'Y' and ReceiveDetail.StatusID = @mGraveYardStatusID))
--     and ReceiveDate >= @dReceiveBeginDate and ReceiveDate < @dReceiveEndDate
   
if len(@mStatus) > 0
   Begin
   update #TempRD set Process = 1 where Status = @mStatus
   Delete #TempRD where isnull(Process,0) != 1
   update #TempRD set Process = 0   
   
   End

if len(@mIMEI) > 0
   Begin
   update #TempRD set Process = 1 where ESN = @mIMEI
   -- We need to pick up any that were swapped.
   --Select * from #TempRD
   
   --Select * from ReceiveDetailIMEISwappedLog where ReceiveDetailID = 664264
   
   
   Insert #TempRD (ReceiveDetailID, ProcessID,CurrentProcessID,StartProcessID,ESN,Status, Process, SKU, IFSLocation, IFSCondition)
   Select ReceiveDetail.ReceiveDetailID, ReceiveDetail.ProcessID,@mSaveID,@mSaveID,ReceiveDetail.ESN,ReceiveDetailStatus.Status,1, ReceiveDetail.SKU, ReceiveDetail.IFSLocation, ReceiveDetail.IFSCondition
     From ReceiveDetail
    INNER JOIN ReceiveDetailStatus ON ReceiveDetail.StatusID = ReceiveDetailStatus.ReceiveDetailStatusID     
    Inner join ReceiveDetailIMEISwappedLog on ReceiveDetail.ReceiveDetailID = ReceiveDetailIMEISwappedLog.ReceiveDetailID   
    Where ReceiveDetailIMEISwappedLog.IMEISwappedOut = @mIMEI and ReceiveDetailIMEISwappedLog.IMEISwappedIn <> ReceiveDetailIMEISwappedLog.IMEISwappedOut
    
   --Select * from #TempRD 
   
   Delete #TempRD where isnull(Process,0) != 1
   update #TempRD set Process = 0   
    
  
   End


--Select * from #xTemp

--return
   
if len(@mCarrier) > 0
   Begin
   Delete #TempBin
   insert #TempBin
   SELECT ReceiveDetailItem.ReceiveDetailID
     FROM ReceiveDetailItem 
    INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
    INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
    WHERE (Question.Name = N'Carrier') 
      AND (isnull([Option].OptionText,'') = @mCarrier) 
      AND (ReceiveDetailItem.Version = 0)
      
   update #TempRD set Process = 1 where ReceiveDetailID in (Select ReceiveDetailID from #TempBin)
   Delete #TempRD where isnull(Process,0) != 1
   update #TempRD set Process = 0   
   
   End
   
if len(@mManufacturer) > 0
   Begin
   Delete #TempBin
   insert #TempBin
   SELECT ReceiveDetailItem.ReceiveDetailID
     FROM ReceiveDetailItem 
    INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
    INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
    WHERE (Question.Name = N'Manufacturer') 
      AND (isnull([Option].OptionText,'') = @mManufacturer) 
      AND (ReceiveDetailItem.Version = 0)
      
   update #TempRD set Process = 1 where ReceiveDetailID in (Select ReceiveDetailID from #TempBin)
   Delete #TempRD where isnull(Process,0) != 1
   update #TempRD set Process = 0   
   
   End
   
if len(@mModel) > 0
   Begin
   Delete #TempBin
   insert #TempBin
   SELECT ReceiveDetailItem.ReceiveDetailID
     FROM ReceiveDetailItem 
    INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
    INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
    WHERE (Question.Name = N'Model') 
      AND (isnull([Option].OptionText,'') = @mModel) 
      AND (ReceiveDetailItem.Version = 0)
      
   update #TempRD set Process = 1 where ReceiveDetailID in (Select ReceiveDetailID from #TempBin)
   Delete #TempRD where isnull(Process,0) != 1
   update #TempRD set Process = 0   
   
   End
   
if len(@mColour) > 0
   Begin
   Delete #TempBin
   insert #TempBin
   SELECT ReceiveDetailItem.ReceiveDetailID
     FROM ReceiveDetailItem 
    INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
    INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
    WHERE (Question.Name = N'Colour') 
      AND (isnull([Option].OptionText,'') = @mColour) 
      AND (ReceiveDetailItem.Version = 0)
      
   update #TempRD set Process = 1 where ReceiveDetailID in (Select ReceiveDetailID from #TempBin)
   Delete #TempRD where isnull(Process,0) != 1
   update #TempRD set Process = 0   
   
   End         

if len(@mSKU) > 0
   Begin
   Delete #TempBin
   --insert #TempBin
   --SELECT ReceiveDetailItem.ReceiveDetailID
   --  FROM ReceiveDetailItem 
   -- INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
   -- INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
   -- WHERE (Question.Name = N'SKU') 
   --   AND (isnull(ReceiveDetailItem.Value,'') = @mSKU) 
   --   AND (ReceiveDetailItem.Version = 0)
   insert #TempBin
   SELECT ReceiveDetailID
     FROM ReceiveDetail 
    WHERE (isnull(SKU,'') = @mSKU) 
      
   update #TempRD set Process = 1 where ReceiveDetailID in (Select ReceiveDetailID from #TempBin)
   Delete #TempRD where isnull(Process,0) != 1
   update #TempRD set Process = 0   
   
   End
   
if len(@mReplacementIMEI) > 0
   Begin
   Delete #TempBin
   insert #TempBin
   SELECT ReceiveDetailItem.ReceiveDetailID
     FROM ReceiveDetailItem 
    INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
    INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
    WHERE (Question.Name = N'Replacement IMEI') 
      AND (isnull(ReceiveDetailItem.Value,'') = @mReplacementIMEI) 
      AND (ReceiveDetailItem.Version = 0)
      
   update #TempRD set Process = 1 where ReceiveDetailID in (Select ReceiveDetailID from #TempBin)
   Delete #TempRD where isnull(Process,0) != 1
   update #TempRD set Process = 0   
   
   End   
   
------------------------------------------------
if len(@mBinNumber) > 0
   Begin
   Delete #TempBin
   insert #TempBin
   SELECT ReceiveDetailItem.ReceiveDetailID
     FROM ReceiveDetailItem 
    INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
    INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
    WHERE (Question.Name = N'Bin') 
      AND (isnull(ReceiveDetailItem.Value,'') = @mBinNumber) 
      AND (ReceiveDetailItem.Version = 0)
   update #TempRD set Process = 1 where ReceiveDetailID in (Select ReceiveDetailID from #TempBin)
   Delete #TempRD where isnull(Process,0) != 1
   update #TempRD set Process = 0   
  
   End

if len(@mHobble) > 0
   Begin
   Delete #TempBin
   insert #TempBin
   SELECT ReceiveDetailItem.ReceiveDetailID
     FROM ReceiveDetailItem 
    INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
    INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
    WHERE (Question.Name = N'Hobble') 
      AND (isnull(ReceiveDetailItem.Value,'') = @mHobble) 
      AND (ReceiveDetailItem.Version = 0)
      
   update #TempRD set Process = 1 where ReceiveDetailID in (Select ReceiveDetailID from #TempBin)
   Delete #TempRD where isnull(Process,0) != 1
   update #TempRD set Process = 0   
   End
   
-- Select * from #TempRD
  
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
--	  INNER JOIN Process ON Process.ProcessID = dbo.GetReceivedDetailCurrentProcessID(ReceiveDetail.ReceiveDetailID)
	  INNER JOIN Project on ReceiveDetail.ProjectId = Project.ProjectID
	  Where (ReceiveDetail.ProjectID = @mProjectID or @mProjectID = -1) 
        and (ClientLocation.ClientLocationID = @mClientLocationID or @mClientLocationID = -1) 
	     --and ReceiveDetail.StatusID != @mGraveYardStatusID	
	  order by ClientLocation.Name, Project.Name, RMANumber, ProjectTag, Process, ESN

-- print 'Got the Detail:' + Convert(varchar(30),getdate(), 121)	

--Select * from #xTemp 
--return 



if len(rtrim(ltrim(@mProjectTag))) > 0
   Delete #xTemp where ProjectTag <> @mProjectTag or ProjectTag is null
   --print 'Deleted any Project Tags Not required:' + Convert(varchar(30),getdate(), 121)	

   
if len(rtrim(ltrim(@mRMANumber))) > 0
   Delete #xTemp where RMANumber <> @mRMANumber or RMANumber is null
   --print 'Deleted any RMANumbers Not required:' + Convert(varchar(30),getdate(), 121)	

-- // We want to get rid of any that may not be within our date range.
--   if year(@dReceiveBeginDate) > 1900
--      begin
--      Print 'we are there'
--      end


/*
-- Get the Item Data
insert #Temp123 (ReceiveDetailID
      ,QuestionID
      ,ReceiveDetailItemID
      ,OptionID
      ,QuestionType
      ,QuestionName,Variable
      ,VariableValue
      ,[Date_QC]  
      )
SELECT #xTemp.ReceiveDetailID
      ,Question.QuestionID
      ,ReceiveDetailItemID
      ,ReceiveDetailItem.OptionID
      ,QuestionType.Type
      ,Question.Name,Question.Name       
      ,ReceiveDetailItem.Value,
       convert(nvarchar(20), ReceiveDetailItem.CreateDate,101)         --mm/dd/yyyy
 From #xTemp
Inner join ReceiveDetailItem on ReceiveDetailItem.ReceiveDetailID = #xTemp.ReceiveDetailID
INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
INNER JOIN Question ON [Option].QuestionID = Question.QuestionID 
INNER JOIN QuestionType ON Question.QuestionTypeID = QuestionType.QuestionTypeID
Where ReceiveDetailItem.Version = 0 and QuestionType.Type != 'Checkbox'
ORDER BY ReceiveDetailItem.ReceiveDetailID, Question.Sequence, [Option].Sequence
-- print 'Done pulling the Raw Item Data:' + Convert(varchar(30),getdate(), 121)	

--Select * from #Temp123



-- We need to handle the CHeck box different because we can have multiple ones.
insert #Temp123 (ReceiveDetailID
      ,QuestionID
      ,ReceiveDetailItemID
      ,OptionID
      ,QuestionType
      ,QuestionName,Variable
      ,VariableValue
      ,[Date_QC]  
      )
SELECT ReceiveDetailItem.ReceiveDetailID
      ,Question.QuestionID
      ,-1                     --ReceiveDetailItemID
      ,-1                     -- ReceiveDetailItem.OptionID
      ,QuestionType.Type
      ,Question.Name,Question.Name       
      ,''                     -- ReceiveDetailItem.Value,
      ,''                     --mm/dd/yyyy
 From #xTemp
Inner join ReceiveDetailItem on ReceiveDetailItem.ReceiveDetailID = #xTemp.ReceiveDetailID
INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
INNER JOIN Question ON [Option].QuestionID = Question.QuestionID 
INNER JOIN QuestionType ON Question.QuestionTypeID = QuestionType.QuestionTypeID
Where ReceiveDetailItem.Version = 0 and QuestionType.Type = 'Checkbox'
Group By ReceiveDetailItem.ReceiveDetailID, Question.QuestionID, QuestionType.Type, Question.Name
ORDER BY ReceiveDetailItem.ReceiveDetailID

-- Select * from #Temp123

*/

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


--Update #xTemp set SwappedESN = IMEISwappedOut
--    From #xTemp
--   Inner join ReceiveDetailIMEISwappedLog on #xTemp.ReceiveDetailID = ReceiveDetailIMEISwappedLog.ReceiveDetailID;
   
   
Update #xTemp set SwappedESN = 
 (Select Top 1 IMEISwappedOut 
    From #xTemp z
   Inner join ReceiveDetailIMEISwappedLog on z.ReceiveDetailID = ReceiveDetailIMEISwappedLog.ReceiveDetailID
   where z.ReceiveDetailID = #xTemp.ReceiveDetailID order by CreateDate Desc);   



   
   
Select #xTemp.source,
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
       dbo.GetReceivedQuestionAnswerString_03(#xTemp.ReceiveDetailID, 'Storage Location') as IFSLocation,
       #TempRD.IFSCondition,
       #xTemp.LastUpdateDate,
       #xTemp.LastUpdateUser
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
