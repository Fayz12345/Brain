/****** Object:  StoredProcedure [dbo].[GetMasterDetailInventoryList_TemplateRawData_01Version]    Script Date: 04/19/2018 10:49:19 ******/
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

GetMasterDetailInventoryList_TemplateRawData_01Version -1, '','','','','','','','','','','','','','','','','','','','','N','','000',''


GetMasterDetailInventoryList_TemplateRawData_01 30, '','','','','01/01/2013','02/01/2013','','','01/01/2013','02/01/2013','','','','','','','','','','','N',''

GetMasterDetailInventoryList_TemplateRawData_01Version -1, '','','','','01/01/2013','02/01/2013','','','01/01/2013','02/01/2013','','','','','','','','','','','N','000',''

GetMasterDetailInventoryList_TemplateRawData_01 -1, '','','','','','','','','','','','','','','','','','','','','N',''
GetMasterDetailInventoryList_TemplateRawData_01 -1, '','','','','','','','','','','','','','','','','','','','','N',''
GetMasterDetailInventoryList_TemplateRawData_01 -1, '','','','','','','','','','','','','','','','','','','','','N',''
                                                 1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21 22

12741


Select * from ClientLocation where ClientLocatioNID in (454,60,1290,2674)


Drop Table ##Temp321
Drop Table ##Temp3214
Select * from Client

Select ESN from ReceiveDetail
 

       Select ReceiveDetailID, ProcessID,ESN,Status,0
         From ReceiveDetail
        INNER JOIN ReceiveDetailStatus ON ReceiveDetail.StatusID = ReceiveDetailStatus.ReceiveDetailStatusID     
         
       Where ((@mShowGraveyard = 'N' and ReceiveDetail.StatusID != @mGraveYardStatusID)
          or  (@mShowGraveyard = 'Y' and ReceiveDetail.StatusID = @mGraveYardStatusID))

       Select ReceiveDetailID, ProcessID,ESN,Status,0
         From ReceiveDetail
        INNER JOIN ReceiveDetailStatus ON ReceiveDetail.StatusID = ReceiveDetailStatus.ReceiveDetailStatusID     
         
       Where (ReceiveDetail.ProjectID = @mProjectID or @mProjectID = -1) 
         and (ReceiveDetail.ClientLocationID = @mClientLocationID or @mClientLocationID = -1) 
         and ((@mShowGraveyard = 'N' and ReceiveDetail.StatusID != @mGraveYardStatusID)
          or  (@mShowGraveyard = 'Y' and ReceiveDetail.StatusID = @mGraveYardStatusID))
*/

Create PROCEDURE [dbo].[GetMasterDetailInventoryList_TemplateRawData_02Version]
      @mClientID numeric(18),
      @mProjectName nvarchar(50) = '',
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
      @mVersion char(3) = '',    
      @mProduct_Place nvarchar(50) = '',   
      @mOrderClientKey nvarchar(50) = ''            

AS
BEGIN
	SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	

declare @mShowLog int
Select @mShowLog = 1
	
Select @mShowGraveyard = case when @mShowGraveyard = 'Y' then 'Y' else 'N' end

if @mShowLog = 1	
   print 'Start:' + Convert(varchar(30),getdate(), 121)	

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

----------------------------------------------------------------------------------------------   

Declare @mGraveYardStatusID numeric(18)
Select @mGraveYardStatusID = ReceiveDetailStatusID FROM  ReceiveDetailStatus where Status = 'GraveYard'

Declare @mProjectID numeric(18)
Select @mProjectID = ProjectID from Project where Name = @mProjectName
Select @mProjectID = isnull(@mProjectID, -1)
if len(rtrim(ltrim(@mProjectName))) > 0 and @mProjectID = -1
   select @mProjectID = -2
   
   
if @mShowLog = 1	
   begin
   print '  @mProduct_Place = ' + Convert(varchar(30),@mProduct_Place) 
   print '  @mVersion = ' + Convert(varchar(30),@mVersion) 
   print '  @mClientLocationID = ' + Convert(varchar(30),@mClientLocationID)   
   print '  @mProjectID = ' + Convert(varchar(30),@mProjectID)   
   print '  @mReceiveBeginDate = ' + Convert(varchar(30),@mReceiveBeginDate) 
   print '  @mHobble = ' + Convert(varchar(30),@mHobble) 
   print '  isdate(@mReceiveBeginDate) = ' + Convert(varchar(30),isdate(@mReceiveBeginDate)) 
   print '  isdate(@mShippedBeginDate) = ' + Convert(varchar(30),isdate(@mShippedBeginDate)) 
   end
   
   
   
CREATE TABLE #xTemp(
	[source] [varchar](6) COLLATE Latin1_General_CI_AS NULL,
    [ReceiveHeaderID] [numeric] (18,0),
    [ReceiveDetailBulkID] [numeric] (18,0),
    [ReceiveDetailID] [numeric] (18,0),
    [ClientLocationID] [numeric] (18,0),
    [ProjectID] [numeric] (18,0),
	[ProjectName] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[Name] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[Location_Dealer_Code] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	
	[StoreNumber] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL,
	[StoreSuffix] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL,
	[Sequence] [int] NULL,
	
	[MasterClientName] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
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
	[ESN] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[Version] [nchar](3) COLLATE Latin1_General_CI_AS NULL,		
--	[ICB] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
--	[PIN] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[Date_QC] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL,
	
	[CarrierID] [numeric](18, 0) NULL,
	[ManufacturerID] [numeric](18, 0) NULL,
	[ModelID] [numeric](18, 0) NULL,
	[ColourID] [numeric](18, 0) NULL,	
	
	[CarrierAbbr] [nvarchar](20) NULL,
	[ManufacturerAbbr] [nvarchar](20) NULL,
	[ModelAbbr] [nvarchar](20) NULL,
	[ColourAbbr] [nvarchar](20) NULL,		
	[CarrierVerb] [nvarchar](50) NULL,
	[ManufacturerVerb] [nvarchar](50) NULL,
	[ModelVerb] [nvarchar](50) NULL,
	[ColourVerb] [nvarchar](50) NULL,
	
	[ML_Stock_Colour_Code] [nvarchar](20) NULL,	
	[ML_NickName] [nvarchar](50) NULL,	
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
	[ML_Retire] [nvarchar](20) NULL,
	[ML_Unit_OS] [nvarchar](100) NULL,	

	[SwappedESN] [nvarchar](50) NULL		
	)



CREATE TABLE #Temp123(
	[ReceiveDetailID] [numeric](18, 0) NULL,
	[ReceiveDetailItemID] [numeric](18, 0) NULL,	
	[Variable] [nvarchar](50) NOT NULL,
	[VariableValue] [nvarchar](500) NULL,
	[OptionID] [numeric](18, 0) NULL,		
	[QuestionID] [numeric](18, 0) NULL,
	[QuestionType] [nvarchar](20) NULL,
	[QuestionName] [nvarchar](50) NULL,
	[Date_QC] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL
    ) 

CREATE TABLE #Temp123_4(
	-- [ReceiveDetailID] [numeric](18, 0) NULL,
	[ID] [numeric](18, 0) NULL,	
	[Variable] [nvarchar](50) NOT NULL,
	[VariableValue] [nvarchar](500) NULL,
    ) 

CREATE TABLE #Temp123b(
	[ReceiveDetailID] [numeric](18, 0) NULL,
	[ReceiveDetailItemID] [numeric](18, 0) NULL,	
	[Variable] [nvarchar](50) NOT NULL,
	[VariableValue] [nvarchar](500) NULL,
	[OptionID] [numeric](18, 0) NULL,		
	[QuestionID] [numeric](18, 0) NULL,
	[QuestionType] [nvarchar](20) NULL,
	[QuestionName] [nvarchar](50) NULL,
	[Date_QC] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL
    ) 

CREATE TABLE #Temp123_4b(
	-- [ReceiveDetailID] [numeric](18, 0) NULL,
	[ID] [numeric](18, 0) NULL,	
	[Variable] [nvarchar](50) NOT NULL,
	[VariableValue] [nvarchar](500) NULL,
    ) 


CREATE TABLE #TempZX(
	[ReceiveDetailID] [numeric](18, 0) NULL,
	[CurrentLogID] [numeric](18, 0) NULL,
	[StartLogID] [numeric](18, 0) NULL,
    )
    
CREATE TABLE #TempRD(
	[ReceiveDetailID] [numeric](18, 0) NULL,
	[ProcessID] [numeric](18, 0) NULL,
	[CurrentProcessID] [numeric](18, 0) NULL,
	[StartProcessID] [numeric](18, 0) NULL,
	[CurrentProcessName] nVarchar(20) NULL,
	[StartProcessName] nVarchar(20) NULL,
	[ESN] nvarchar(50),
	[Status] nvarchar(20),	
	[Process] numeric(15) NULL
    )



--CREATE TABLE #TempPR(
--	[ReceiveDetailID] [numeric](18, 0) NULL,
--	[ProcessID] [numeric](18, 0) NULL,
--	[ProcessName] nVarchar(20) NULL,	
--    )
--
--Create Index Temp_05 on #TempPR (ReceiveDetailID)
--Create Index Temp_06 on #TempPR ([ProcessID])

CREATE TABLE #TempBin(
	[ReceiveDetailID] [numeric](18, 0) NULL
    )



if @mShowLog = 1	
   print 'Init done:' + Convert(varchar(30),getdate(), 121)	


if (LEN(RTRIM(ltrim(@mHobble))) > 0)
    begin
    Insert #TempRD (ReceiveDetailID, ProcessID,CurrentProcessID,StartProcessID,ESN,Status, Process)
   Select ReceiveDetail.ReceiveDetailID, ProcessID,@mSaveID,@mSaveID,ReceiveDetail.ESN,Status,0
     From ReceiveDetail
    INNER JOIN ReceiveDetailStatus ON ReceiveDetail.StatusID = ReceiveDetailStatus.ReceiveDetailStatusID    
    INNER JOIN OrderDetailReceiveDetail ON ReceiveDetail.ReceiveDetailID = OrderDetailReceiveDetail.ReceiveDetailID 
    INNER JOIN OrderDetail ON OrderDetailReceiveDetail.OrderDetailID = OrderDetail.OrderDetailID 
    INNER JOIN OrderHeader ON OrderDetail.OrderHeaderID = OrderHeader.OrderHeaderID
    WHERE     (OrderHeader.OrderNumber = @mHobble)
     and (ReceiveDetail.ProjectID = @mProjectID or @mProjectID = -1) 
     and (ReceiveDetail.ClientLocationID = @mClientLocationID or @mClientLocationID = -1) 
     
    if @mShowLog = 1	
       print 'OrderNumber Pulled:' + Convert(varchar(30),getdate(), 121)	
     
     
    end



    
if (LEN(RTRIM(ltrim(@mHobble))) = 0)
    begin
    if isdate(@mReceiveBeginDate) = 0
    
    
       Insert #TempRD (ReceiveDetailID, ProcessID,CurrentProcessID,StartProcessID,ESN,Status, Process)
       Select ReceiveDetailID, ProcessID,@mSaveID,@mSaveID,ESN,Status,0
         From ReceiveDetail
        INNER JOIN ReceiveDetailStatus ON ReceiveDetail.StatusID = ReceiveDetailStatus.ReceiveDetailStatusID     
         
       Where (ReceiveDetail.ProjectID = @mProjectID or @mProjectID = -1) 
         and (ReceiveDetail.ClientLocationID = @mClientLocationID or @mClientLocationID = -1) 
         and ((@mShowGraveyard = 'N' and ReceiveDetail.StatusID != @mGraveYardStatusID)
          or  (@mShowGraveyard = 'Y' and ReceiveDetail.StatusID = @mGraveYardStatusID))
          
          
         
    if isdate(@mReceiveBeginDate) = 1
       Insert #TempRD (ReceiveDetailID, ProcessID,CurrentProcessID,StartProcessID,ESN,Status, Process)
       Select ReceiveDetailID, ProcessID,@mSaveID,@mSaveID,ESN,Status,0
         From ReceiveDetail
        INNER JOIN ReceiveDetailStatus ON ReceiveDetail.StatusID = ReceiveDetailStatus.ReceiveDetailStatusID     
        
       Where (ReceiveDetail.ProjectID = @mProjectID or @mProjectID = -1) 
         and (ReceiveDetail.ClientLocationID = @mClientLocationID or @mClientLocationID = -1) 
         and ((@mShowGraveyard = 'N' and ReceiveDetail.StatusID != @mGraveYardStatusID)
          or  (@mShowGraveyard = 'Y' and ReceiveDetail.StatusID = @mGraveYardStatusID))
         and ReceiveDate >= @dReceiveBeginDate and ReceiveDate < @dReceiveEndDate

    if @mShowLog = 1	
       print 'Other Pulled:' + Convert(varchar(30),getdate(), 121)	
        
         
   END     

--if @mShowLog = 1	
--   Select * from #TempRD
----------------------------------------------------------------------------------------
    if isdate(@mShippedBeginDate) = 1
       begin
       CREATE TABLE #TempShipped([ReceiveDetailID] [numeric](18, 0) NULL)
       Declare @dShippedBeginDate Datetime
       Declare @dShippedEndDate Datetime
       Select @dShippedBeginDate = convert(datetime,@mShippedBeginDate,101)
       Select @dShippedEndDate = convert(datetime,@mShippedEndDate,101)
       Select @dShippedEndDate = dateadd(d,1,@dShippedEndDate)
		----------------------------------------------------------------------------------------------       
       Insert #TempShipped (ReceiveDetailID)
      SELECT ReceiveDetailProcessLog.ReceiveDetailID       -- , Process.Name, min(ReceiveDetailProcessLog.CreateDate) as CreateDate
        FROM ReceiveDetailProcessLog 
       INNER JOIN Process ON ReceiveDetailProcessLog.ProcessID = Process.ProcessID  
       Where Name = 'Shipping'
       Group by ReceiveDetailID
      having MIN(ReceiveDetailProcessLog.CreateDate) >= @dShippedBeginDate and MIN(ReceiveDetailProcessLog.CreateDate) < @dShippedEndDate
       
       -- having MIN(ReceiveDetailProcessLog.CreateDate) >= '01/01/2013'and MIN(ReceiveDetailProcessLog.CreateDate) < '02/01/2013'
       
       Update #TempRD Set Process = 1 From #TempRD Inner join #TempShipped on #TempRD.ReceiveDetailID = #TempShipped.ReceiveDetailID
       Delete #TempRD Where Process <> 1
       Update #TempRD set Process = 0
       
       Drop Table #TempShipped
       
       end
       
       
 --if @mShowLog = 1	
 --  Select * from #TempRD      
----------------------------------------------------------------------------------------       

    if LEN(LTRIM(rtrim(@mOrderClientKey))) > 0
       begin
       
       Select COUNT(*) as Beforex from #TempRD
       
       Create Table #xTempOrderEntryKeyReceiveDetailID([ReceiveDetailID] [numeric] (18,0))   
       Create Index TempOrderEntryKeyReceiveDetailIndex on #xTempOrderEntryKeyReceiveDetailID(ReceiveDetailID)      
       
       Insert #xTempOrderEntryKeyReceiveDetailID (ReceiveDetailID)
       SELECT OrderDetailReceiveDetail.ReceiveDetailID                  -- , OrderStatus.Status, ClientLocation.ScanKey, OrderDetailReceiveDetail.ESN, OrderHeader.OrderNumber
         FROM OrderCompany INNER JOIN
              OrderHeader ON OrderCompany.OrderHeaderID = OrderHeader.OrderHeaderID INNER JOIN
              OrderDetail ON OrderHeader.OrderHeaderID = OrderDetail.OrderHeaderID INNER JOIN
              OrderDetailReceiveDetail ON OrderDetail.OrderDetailID = OrderDetailReceiveDetail.OrderDetailID INNER JOIN
              ClientLocation ON OrderCompany.ClientLocationID = ClientLocation.ClientLocationID INNER JOIN
              OrderStatus ON OrderHeader.StatusID = OrderStatus.OrderStatusID
       WHERE (OrderCompany.CompanyType = 'Client') 
         AND (NOT (OrderDetailReceiveDetail.ReceiveDetailID IS NULL)) 
         AND (OrderStatus.Status <> 'Trash') 
         and ClientLocation.ScanKey = @mOrderClientKey                       -- 'Goldiex'

       Update #TempRD Set Process = 1 From #TempRD Inner join #xTempOrderEntryKeyReceiveDetailID on #TempRD.ReceiveDetailID = #xTempOrderEntryKeyReceiveDetailID.ReceiveDetailID

       Delete #TempRD Where Process <> 1
       Update #TempRD set Process = 0
              
       drop table #xTempOrderEntryKeyReceiveDetailID
       end


---------------------------------------------------------------------------------------

Create Index Temp_03 on #TempRD (ReceiveDetailID)
Create Index Temp_04 on #TempRD (ProcessID)

if len(@mVersion) > 0
   begin
   Delete #TempBin
   insert #TempBin  
   Select T.ReceiveDetailID
   From #TempRD T
   Inner join ReceiveDetail R on T.ReceiveDetailID = R.ReceiveDetailID
   Where R.Version = @mVersion
   
   --if @mShowLog = 1	
   --Select * from #TempBin
   
   update #TempRD set Process = 1 where ReceiveDetailID in (Select ReceiveDetailID from #TempBin)
   Delete #TempRD where isnull(Process,0) != 1
   update #TempRD set Process = 0
   if @mShowLog = 1	
      print 'Version Isolated:' + Convert(varchar(30),getdate(), 121)  
      
   -- if @mShowLog = 1	
   --Select * from #TempRD     
   end
   
   
if len(ltrim(rtrim(@mProduct_Place))) > 0
   Begin
   Delete #TempBin
   insert #TempBin
   SELECT ReceiveDetailItem.ReceiveDetailID
     FROM ReceiveDetailItem 
    INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
    INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
    WHERE (Question.Name = N'Product Place') 
      AND (isnull([Option].OptionText,'') = @mProduct_Place)
      --AND (isnull(ReceiveDetailItem.Value,'') = @mBinNumber) 
      AND (ReceiveDetailItem.Version = 0)
   update #TempRD set Process = 1 where ReceiveDetailID in (Select ReceiveDetailID from #TempBin)
   Delete #TempRD where isnull(Process,0) != 1
   update #TempRD set Process = 0
   if @mShowLog = 1	
      print 'Process Place Deleted:' + Convert(varchar(30),getdate(), 121)	
   End   
   
   
if len(@mStatus) > 0
   Begin
   update #TempRD set Process = 1 where Status = @mStatus
   Delete #TempRD where isnull(Process,0) != 1
   update #TempRD set Process = 0
   if @mShowLog = 1	
      print 'Status Deleted:' + Convert(varchar(30),getdate(), 121)	
   
   End

if len(@mIMEI) > 0
   Begin
   update #TempRD set Process = 1 where ESN = @mIMEI
   Delete #TempRD where isnull(Process,0) != 1
   update #TempRD set Process = 0
   if @mShowLog = 1	
      print 'IMEI Deleted:' + Convert(varchar(30),getdate(), 121)	
   End
  
  
Create Index Temp_25 on #TempBin (ReceiveDetailID)
     
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
      AND (ReceiveDetailItem.Value = '1')      
      
      
   
   update #TempRD set Process = 1 where ReceiveDetailID in (Select ReceiveDetailID from #TempBin)
   Delete #TempRD where isnull(Process,0) != 1
   update #TempRD set Process = 0
   if @mShowLog = 1	
      print 'Carrier Deleted:' + Convert(varchar(30),getdate(), 121)	
   
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
      AND (ReceiveDetailItem.Value = '1')      
      
      
   update #TempRD set Process = 1 where ReceiveDetailID in (Select ReceiveDetailID from #TempBin)
   Delete #TempRD where isnull(Process,0) != 1
   update #TempRD set Process = 0
   if @mShowLog = 1	
      print 'Manufacturer Deleted:' + Convert(varchar(30),getdate(), 121)	
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
      AND (ReceiveDetailItem.Value = '1')      
      
      
   update #TempRD set Process = 1 where ReceiveDetailID in (Select ReceiveDetailID from #TempBin)
   Delete #TempRD where isnull(Process,0) != 1
   update #TempRD set Process = 0
   if @mShowLog = 1	
      print 'Model Deleted:' + Convert(varchar(30),getdate(), 121)	
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
      AND (ReceiveDetailItem.Value = '1')      
      
      
   update #TempRD set Process = 1 where ReceiveDetailID in (Select ReceiveDetailID from #TempBin)
   Delete #TempRD where isnull(Process,0) != 1
   update #TempRD set Process = 0
   
   if @mShowLog = 1	
      print 'Colour Deleted:' + Convert(varchar(30),getdate(), 121)	
      
   End         

if len(@mSKU) > 0
   Begin
   Delete #TempBin
   insert #TempBin
   SELECT ReceiveDetailItem.ReceiveDetailID
     FROM ReceiveDetailItem 
    INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
    INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
    WHERE (Question.Name = N'SKU') 
      AND (isnull(ReceiveDetailItem.Value,'') = @mSKU) 
      AND (ReceiveDetailItem.Version = 0)
      
   update #TempRD set Process = 1 where ReceiveDetailID in (Select ReceiveDetailID from #TempBin)
   Delete #TempRD where isnull(Process,0) != 1
   update #TempRD set Process = 0
   if @mShowLog = 1	
      print 'Sku Deleted:' + Convert(varchar(30),getdate(), 121)	

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
      AND (isnull(ReceiveDetailItem.Value,'') in (Select * from dbo.fn_SplitDistinct(@mBinNumber,',')))
      --AND (isnull(ReceiveDetailItem.Value,'') = @mBinNumber) 
      AND (ReceiveDetailItem.Version = 0)
   update #TempRD set Process = 1 where ReceiveDetailID in (Select ReceiveDetailID from #TempBin)
   Delete #TempRD where isnull(Process,0) != 1
   update #TempRD set Process = 0
   if @mShowLog = 1	
      print 'Bin Number Deleted:' + Convert(varchar(30),getdate(), 121)	
   End

--if len(@mHobble) > 0
--   Begin
--   Delete #TempBin
--   insert #TempBin
--   SELECT ReceiveDetailItem.ReceiveDetailID
--     FROM ReceiveDetailItem 
--    INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
--    INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
--    WHERE (Question.Name = N'Hobble') 
--      AND (isnull(ReceiveDetailItem.Value,'') = @mHobble) 
--      AND (ReceiveDetailItem.Version = 0)
--      
--   update #TempRD set Process = 1 where ReceiveDetailID in (Select ReceiveDetailID from #TempBin)
--   Delete #TempRD where isnull(Process,0) != 1
--   update #TempRD set Process = 0
--   End
   
   
if @mClientID > 0
   begin
   update #TempRD set Process = 0
   update #TempRD set Process = 1 
   from #TempRD 
   inner join ReceiveDetail on #TempRD.REceiveDetailID = ReceiveDetail.ReceiveDetailID
   inner join ClientLocation on ReceiveDetail.ClientLocationID = ClientLocation.ClientLocationID
   where ClientLocation.ClientID = @mClientID
   Delete #TempRD where isnull(Process,0) != 1
   if @mShowLog = 1	
      print 'ClientID Deleted:' + Convert(varchar(30),getdate(), 121)	
 
   end   
   

-- We want to get the start and Current process (Min/Max).

Insert #TempZX
Select #TempRD.ReceiveDetailID, Max(ReceiveDetailProcessLog.ReceiveDetailProcessLogID), Min(ReceiveDetailProcessLog.ReceiveDetailProcessLogID)
  From #TempRD
 Inner join ReceiveDetailProcessLog on ReceiveDetailProcessLog.ReceiveDetailID = #TempRD.ReceiveDetailID
 Inner Join Process on Process.ProcessID = ReceiveDetailProcessLog.ProcessID
 Where process.name != 'Save'
 Group By #TempRD.ReceiveDetailID
 
Create Index Temp_10 on #Tempzx([ReceiveDetailID])    
Create Index Temp_11 on #Tempzx([CurrentLogID])
Create Index Temp_12 on #Tempzx([StartLogID]) 

if @mShowLog = 1	
   print 'Processing Log Pulled:' + Convert(varchar(30),getdate(), 121)	


 
Update #TempRD set [StartProcessID] = Process.ProcessID, StartProcessName = Process.Name
  From #TempRD
 Inner join #TempZX on #TempZX.ReceiveDetailID = #TempRD.ReceiveDetailID
 Inner join ReceiveDetailProcessLog on ReceiveDetailProcessLog.ReceiveDetailProcessLogID = #TempZX.[StartLogID]
 Inner Join Process on Process.ProcessID = ReceiveDetailProcessLog.ProcessID

Update #TempRD set CurrentProcessID = Process.ProcessID, CurrentProcessName = Process.Name
  From #TempRD
 Inner join #TempZX on #TempZX.ReceiveDetailID = #TempRD.ReceiveDetailID
 Inner join ReceiveDetailProcessLog on ReceiveDetailProcessLog.ReceiveDetailProcessLogID = #TempZX.[CurrentLogID]
 Inner Join Process on Process.ProcessID = ReceiveDetailProcessLog.ProcessID

if @mShowLog = 1	
   print 'Processing Log Dated:' + Convert(varchar(30),getdate(), 121)	

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
			ClientLocation.ScanKey, 
			ClientLocation.StoreNumber, 
			ClientLocation.StoreSuffix, 
			ClientLocation.Sequence, 
            Client.CompanyName,

			ClientLocation.CompanyName, 
			convert(varchar(20),'') as Process,
			ReceiveDetailStatus.Status,
			convert(numeric(18),0) as QTYPaper, 
			ReceiveDetail.QTYIntegrated as QTYRecorded, 
			ReceiveDetail.QTYIntegrated, 
			ReceiveDetail.CreateDate, 
			-- convert(nvarchar(10),ReceiveDetail.CreateDate,111),
			convert(nvarchar(10),ReceiveDetail.CreateDate,101),			
			convert(nvarchar(10),ReceiveDetail.CreateDate,108), 

			'',
			Null,
			ReceiveDetail.RMANumber, 
			ReceiveDetail.ProjectTag, 
			ReceiveDetail.ESN, ReceiveDetail.Version,'',            --  ReceiveDetail.ICB, ReceiveDetail.PIN,
			
			ReceiveDetail.CarrierID,
			ReceiveDetail.ManufacturerID,
			ReceiveDetail.ModelID,
			ReceiveDetail.ColourID,
			'','','','',                                             -- ABBR
			'','','','',                                             -- Verbose
			'','','','','','','','','','','','','','',''
	   FROM #TempRD 
	  INNER JOIN ReceiveDetail ON #TempRD.ReceiveDetailID = ReceiveDetail.ReceiveDetailID 
      INNER JOIN ReceiveDetailStatus ON ReceiveDetail.StatusID = ReceiveDetailStatus.ReceiveDetailStatusID	  
	  INNER JOIN ClientLocation ON ReceiveDetail.ClientLocationID = ClientLocation.ClientLocationID
	  INNER JOIN Client ON ClientLocation.ClientID = Client.ClientID
  
--	  INNER JOIN Process ON Process.ProcessID = dbo.GetReceivedDetailCurrentProcessID(ReceiveDetail.ReceiveDetailID)
	  INNER JOIN Project on ReceiveDetail.ProjectId = Project.ProjectID
	  Where (ReceiveDetail.ProjectID = @mProjectID or @mProjectID = -1) 
        and (ClientLocation.ClientLocationID = @mClientLocationID or @mClientLocationID = -1) 
	     and ReceiveDetail.StatusID != @mGraveYardStatusID	
	  order by ClientLocation.Name, Project.Name, RMANumber, ProjectTag, Process, ESN

if @mShowLog = 1	
   print 'Detail Pulled:' + Convert(varchar(30),getdate(), 121)	

-- Create the indexs because now they are being used
Create Index Temp_01 on #xTemp (ReceiveDetailID)   --- 
Create Index Temp_02 on #xTemp (ProjectID)
Create Index Temp_C02 on #xTemp (CarrierID)
Create Index Temp_M02 on #xTemp (ManufacturerID)
Create Index Temp_Mo02 on #xTemp (ModelID)
Create Index Temp_Co02 on #xTemp (ColourID)


Update #xTemp set CarrierAbbr = O.Name, CarrierVerb = o.OptionText from #xTemp inner join [Option] O on #xTemp.CarrierID = O.optionID
Update #xTemp set ManufacturerAbbr = O.Name, ManufacturerVerb = o.OptionText from #xTemp inner join [Option] O on #xTemp.ManufacturerID = O.optionID
Update #xTemp set ModelAbbr = O.Name, ModelVerb = o.OptionText from #xTemp inner join [Option] O on #xTemp.ModelID = O.optionID
Update #xTemp set ColourAbbr = O.Name, ColourVerb = o.OptionText from #xTemp inner join [Option] O on #xTemp.ColourID = O.optionID

if @mShowLog = 1	
   print 'SKU ABBR updated:' + Convert(varchar(30),getdate(), 121)	


if len(rtrim(ltrim(@mProjectTag))) > 0
   Begin
   Delete #xTemp where ProjectTag <> @mProjectTag or ProjectTag is null
   if @mShowLog = 1	
      print 'Project Tag Deleted:' + Convert(varchar(30),getdate(), 121)	
   
   END
   --print 'Deleted any Project Tags Not required:' + Convert(varchar(30),getdate(), 121)	

   
if len(rtrim(ltrim(@mRMANumber))) > 0
   Begin
   Delete #xTemp where RMANumber <> @mRMANumber or RMANumber is null
   if @mShowLog = 1	
      print 'RMA Pulled:' + Convert(varchar(30),getdate(), 121)	
   
   End
   --print 'Deleted any RMANumbers Not required:' + Convert(varchar(30),getdate(), 121)	


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
      and (case when QuestionType.Type = 'Dropdown' and ReceiveDetailItem.Value <> '1' then 0
               when QuestionType.Type = 'Checkbox' and ReceiveDetailItem.Value <> '1' then 0 
               when QuestionType.Type = 'RadialButton' and ReceiveDetailItem.Value <> '1' then 0                
               else 1 end) = 1
ORDER BY ReceiveDetailItem.ReceiveDetailID, Question.Sequence, [Option].Sequence

if @mShowLog = 1	
   print 'Non Check Box Answers pulled:' + Convert(varchar(30),getdate(), 121)	


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
Where ReceiveDetailItem.Version = 0 and QuestionType.Type = 'Checkbox' and ReceiveDetailItem.Value = '1'
Group By ReceiveDetailItem.ReceiveDetailID, Question.QuestionID, QuestionType.Type, Question.Name
ORDER BY ReceiveDetailItem.ReceiveDetailID

if @mShowLog = 1	
   print 'Check box Answsers Pulled:' + Convert(varchar(30),getdate(), 121)	

Create Index Temp_02 on #Temp123 (ReceiveDetailID)
Create Index Temp_OPT_01 on #Temp123 (OptionID)
Create Index Temp_OPT_02 on #Temp123 (QuestionType)
Create Index Temp_OPT_04 on #Temp123 (QuestionName)

Update #xTemp set [Date_QC] = #Temp123.[Date_QC]
  from #xTemp 
Inner join #Temp123 on #Temp123.ReceiveDetailID = #xTemp.ReceiveDetailID
Where #Temp123.QuestionName = 'QC'

if @mShowLog = 1	
   print 'QC Pulled:' + Convert(varchar(30),getdate(), 121)	



UPdate #xTemp set WayBill = OrderHeader.WaybillNumber, ShipDate = OrderHeader.Shippeddate
  From #xTemp
 INNER JOIN OrderDetailReceiveDetail ON #xTemp.ReceiveDetailID = OrderDetailReceiveDetail.ReceiveDetailID 
 INNER JOIN OrderDetail ON OrderDetailReceiveDetail.OrderDetailID = OrderDetail.OrderDetailID 
 INNER JOIN OrderHeader ON OrderDetail.OrderHeaderID = OrderHeader.OrderHeaderID 

if @mShowLog = 1	
   print 'WayBill Pulled:' + Convert(varchar(30),getdate(), 121)	
   
------------------------------
if @mShowLog = 1	
   print 'Done Pivot for Create:' + Convert(varchar(30),getdate(), 121)	


Update #xTemp set 
       ML_Stock_Colour_Code = MasterCarrierManufacturerLookup.Condition,
       ML_SKU = MasterCarrierManufacturerLookup.SKU,
       ML_SKU_B = MasterCarrierManufacturerLookup.SKU_B,
       ML_SKU_C = MasterCarrierManufacturerLookup.SKU_C,       
       ML_UPC = MasterCarrierManufacturerLookup.UPC,
       ML_Description = MasterCarrierManufacturerLookup.Description,
       ML_WarrantyStickerPlacement = MasterCarrierManufacturerLookup.WarrantyStickerPlacement,
       ML_Device_Handset = MasterCarrierManufacturerLookup.Device_Handset,
       ML_Bar_Flip = MasterCarrierManufacturerLookup.Bar_Flip,
       ML_CDMA_HSPA = MasterCarrierManufacturerLookup.CDMA_HSPA,
       ML_Retire = MasterCarrierManufacturerLookup.Retire,
       ML_Unit_OS = MasterCarrierManufacturerLookup.Unit_OS,
       ML_NickName = MasterCarrierManufacturerLookup.NickName
  FROM MasterCarrierManufacturerLookup 
 INNER JOIN #xTemp ON MasterCarrierManufacturerLookup.OptionCarrierID = #xTemp.CarrierID
        AND MasterCarrierManufacturerLookup.OptionManufacturerID = #xTemp.ManufacturerID 
        AND MasterCarrierManufacturerLookup.OptionModelID = #xTemp.ModelID 
        AND MasterCarrierManufacturerLookup.OptionColourID = #xTemp.ColourID

-- print 'Piviot Done:' + Convert(varchar(20),getdate(), 120)	

Update #xTemp set SwappedESN = IMEISwappedOut          --  case when len(isnull(SwappedESN,'')) > 0 then SwappedESN + ',' + IMEISwappedOut else IMEISwappedOut end
From #xTemp
Inner join ReceiveDetailIMEISwappedLog on #xTemp.ReceiveDetailID = ReceiveDetailIMEISwappedLog.ReceiveDetailID

if @mShowLog = 1	
   print 'Master CMMC Lookup Done:' + Convert(varchar(30),getdate(), 121)	
------------------------------   
 --return  


-- Set up the variables and their values

Update #Temp123 Set VariableValue = OptionText 
From #Temp123
Inner join [Option] on #Temp123.OptionID  = [Option].OptionID
Where QuestionType = 'RadialButton' OR 
          QuestionType = 'DropDown'
          

Update #Temp123 set VariableValue = [dbo].[GetReceivedQuestionAnswerString_QID](ReceiveDetailID,QuestionID )
Where QuestionType = 'Checkbox'


Insert #Temp123_4
Select ReceiveDetailID,[Variable],[VariableValue] From #Temp123

if @mShowLog = 1	
   print 'Data Prep for Pivot A:' + Convert(varchar(30),getdate(), 121)	

 
 
 
 
--SELECT DISTINCT Question.Name, convert(numeric(15),0) as Included
--into #TempName   
--FROM  Question
--ORDER BY Question.Name    
       

--Create Index Temp_22 on #Temp123_4 (ID)
--Create Index Temp_VAR22 on #Temp123_4 (Variable)

-------------------------------------------------------
-------------------------------------------------------
--Declare @mSID numeric(18)
--Select @mSID = QuestionStatusID from QuestionStatus where [Status] = 'Active'

--SELECT Distinct ProcessQuestion.QuestionID
--  into #Temp01
--  FROM ProcessQuestion 
 
--SELECT DISTINCT Question.Name, convert(numeric(15),0) as Included
--into #TempName   
--FROM  Question
-- where not QuestionID in (Select QuestionID from #Temp01)  
--       and Question.QuestionStatusID = @mSID
--ORDER BY Question.Name    

--Drop Table #Temp01 

--Create Index Temp_22 on #Temp123_4 (ID)
--Create Index Temp_VAR22 on #Temp123_4 (Variable)


------------------------------------------------------
SELECT DISTINCT Question.Name, convert(numeric(15),0) as Included
into #TempName   
FROM  Question
ORDER BY Question.Name    

Create Index Temp_22 on #Temp123_4 (ID)
Create Index Temp_VAR22 on #Temp123_4 (Variable)
--------------------------------------------------------
--------------------------------------------------------


Update #TempName set Included = 1
from #TempName 
Inner join #Temp123_4 on #TempName.Name = #Temp123_4.Variable COLLATE DATABASE_DEFAULT



Insert #Temp123_4
Select -1,#TempName.Name,'' From #TempName
where Included = 0

---------------------------------------------------------
-- Get Rid of any two or more word values.
Update #Temp123_4 Set Variable = Replace(Variable, ' ', '_')              --,VariableValue = Replace([VariableValue], ' ', '_')
--print 'Moved over to pivot Temp 4:' + Convert(varchar(30),getdate(), 121)	

if @mShowLog = 1	
   print 'Data Prep for Pivot B:' + Convert(varchar(30),getdate(), 121)	


-- DECLARE @columns VARCHAR(8000)
DECLARE @columns nvarchar(max)


SELECT @columns = COALESCE(@columns + ',[' + cast(Variable as varchar) + ']', '[' + cast(Variable as varchar)+ ']')
FROM #Temp123_4
GROUP BY Variable

if len(rtrim(ltrim(isnull(@columns,'')))) = 0
   begin
   Select * from #xTemp
   return
   end
   
----------------------------------------------------
-------------------------------------------------------------
--Delete #Temp123
--Delete #Temp123_4


DECLARE @columnsb nvarchar(max)




Select ReceiveDetailProcessLog.ReceiveDetailID, Process.Name, Min(ReceiveDetailProcessLog.CreateDate) as CreateDate
 into #xTemp001
 From #xTemp
 INNER JOIN ReceiveDetailProcessLog ON #xTemp.ReceiveDetailID = ReceiveDetailProcessLog.ReceiveDetailID 
 INNER JOIN Process ON ReceiveDetailProcessLog.ProcessID = Process.ProcessID
Group by ReceiveDetailProcessLog.ReceiveDetailID,Process.Name


insert #Temp123b (ReceiveDetailID
      ,QuestionID
      ,ReceiveDetailItemID
      ,OptionID
      ,QuestionType
      ,QuestionName
      ,Variable
      ,VariableValue
      ,[Date_QC]  
      )
SELECT ReceiveDetailID
      ,-1
      ,-1                     --ReceiveDetailItemID
      ,-1                     -- ReceiveDetailItem.OptionID
      ,''
      ,Replace(isnull(Name,'NoName'), ' ', '_') + '_Created', Replace(isnull(Name,'NoName'), ' ', '_')  + '_Created'            
      ,convert(nvarchar(50),CreateDate,101)
      ,''                     --mm/dd/yyyy
 From #xTemp001



------insert #Temp123b (ReceiveDetailID
------      ,QuestionID
------      ,ReceiveDetailItemID
------      ,OptionID
------      ,QuestionType
------      ,QuestionName
------      ,Variable
------      ,VariableValue
------      ,[Date_QC]  
------      )
------SELECT #xTemp.ReceiveDetailID
------      ,-1
------      ,-1                     --ReceiveDetailItemID
------      ,-1                     -- ReceiveDetailItem.OptionID
------      ,''
------      ,Replace(isnull(Process.Name,'NoName'), ' ', '_') + '_Created', Replace(isnull(Process.Name,'NoName'), ' ', '_')  + '_Created'            
------      ,convert(nvarchar(50),ReceiveDetailProcessLog.CreateDate,101)
------      ,''                     --mm/dd/yyyy
------ From #xTemp
------INNER JOIN ReceiveDetailProcessLog ON #xTemp.ReceiveDetailID = ReceiveDetailProcessLog.ReceiveDetailID 
------INNER JOIN Process ON ReceiveDetailProcessLog.ProcessID = Process.ProcessID

-----------------------------------------
--Select * from ReceiveDetail where ESN = '270113181101164912'
--Select * from ReceiveDetailProcessLog where ReceiveDetailID in (531513,534609)
--Order by ReceiveDetailID, ProcessText




-----------------------------------------------------------







Insert #Temp123_4b
Select ReceiveDetailID,[Variable],[VariableValue] From #Temp123b

-- Second all the Process Create Dates from the log.
SELECT DISTINCT convert(varchar(50),Replace(isnull(Process.Name,'NoName'),' ', '_') + '_Created') as Name, CONVERT(numeric(15), 0) AS Included
into #TempProcessName
FROM Process             
               
               
Create Index xTemp_Name_01 on   #TempProcessName([Name])             
Create Index xTemp_Name_02 on   #TempProcessName(Included) 

Update #TempProcessName set Included = 1
from #TempProcessName 
Inner join #Temp123_4b on #TempProcessName.Name = #Temp123_4b.Variable  COLLATE DATABASE_DEFAULT

Insert #Temp123_4b
Select -1,#TempProcessName.Name,'' From #TempProcessName
where Included = 0



SELECT @columnsb = COALESCE(@columnsb + ',[' + cast(Variable as varchar) + ']', '[' + cast(Variable as varchar)+ ']')
FROM #Temp123_4b
GROUP BY Variable
   
----------------------------------------------------   
   
   
   
   
   
   
   
   
   
   
   
   
   
   
   
   
   
   

DECLARE @query NVARCHAR(MAX)

SET @query = '
SELECT *
into #Temp321
FROM #Temp123_4
PIVOT
(
MAX(VariableValue)
FOR [Variable]
IN (' + @columns + ')
)
AS p;

SELECT *
into #Temp3214
FROM #Temp123_4b
PIVOT
(
MAX(VariableValue)
FOR [Variable]
IN (' + @columnsb + ')
)
AS p;


Create Index Temp321_01 on #Temp321(ID);

Create Index Temp3214_01 on #Temp3214(ID);

Select #xTemp.source,
       #xTemp.ReceiveHeaderID,
       #xTemp.ReceiveDetailBulkID,
       #xTemp.ReceiveDetailID,
       #xTemp.ClientLocationID,
       #xTemp.ProjectID,
       #xTemp.MasterClientName,
      
       #xTemp.Name,
       #xTemp.Location_Dealer_Code,
       #xTemp.StoreNumber,
       #xTemp.StoreSuffix,
       #xTemp.Sequence,
       #xTemp.CompanyName,
       #xTemp.ProjectName,
       #xTemp.Status,
       #TempRD.StartProcessName,
       #TempRD.CurrentProcessName,
       #xTemp.QTYIntegrated,

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

       #xTemp.CarrierAbbr,
       #xTemp.ManufacturerAbbr,
       #xTemp.ModelAbbr,
       #xTemp.ColourAbbr,
       #xTemp.CarrierVerb,
       #xTemp.ManufacturerVerb,
       #xTemp.ModelVerb,
       #xTemp.ColourVerb,

       #xTemp.ML_Stock_Colour_Code,
       #xTemp.ML_SKU,
       #xTemp.ML_SKU_B,
       #xTemp.ML_SKU_C,              
       #xTemp.ML_UPC,
       #xTemp.ML_Description,
       #xTemp.ML_WarrantyStickerPlacement,
       #xTemp.ML_Device_Handset,
       #xTemp.ML_Bar_Flip,
       #xTemp.ML_CDMA_HSPA,
       #xTemp.ML_Retire,
       #xTemp.ML_Unit_OS,
       #xTemp.ML_NickName,       
       #Temp321.*,
       #Temp3214.*
  from #xTemp
Inner join #TempRD on #xTemp.ReceiveDetailID = #TempRD.ReceiveDetailID
Inner join #Temp321 on #xTemp.ReceiveDetailID = #Temp321.ID
Inner join #Temp3214 on #xTemp.ReceiveDetailID = #Temp3214.ID
'

EXECUTE(@query)


--Create Index Temp321_01 on ##Temp321(ID)

--SET @query = '
--
--SELECT *
--into ##Temp3214
--FROM #Temp123_4b
--PIVOT
--(
--MAX(VariableValue)
--FOR [Variable]
--IN (' + @columnsb + ')
--)
--AS p
--'
--
--EXECUTE(@query)

if @mShowLog = 1	
   print 'Pivot Done:' + Convert(varchar(30),getdate(), 121)	


--Create Index Temp3214_01 on ##Temp3214(ID)
--
--Select #xTemp.source,
--       #xTemp.ReceiveHeaderID,
--       #xTemp.ReceiveDetailBulkID,
--       #xTemp.ReceiveDetailID,
--       #xTemp.ClientLocationID,
--       #xTemp.ProjectID,
--       #xTemp.MasterClientName,
--      
--       #xTemp.Name,
--       #xTemp.Location_Dealer_Code,
--       #xTemp.StoreNumber,
--       #xTemp.StoreSuffix,
--       #xTemp.Sequence,
--       #xTemp.CompanyName,
--       #xTemp.ProjectName,
--       #xTemp.Status,
--       #TempRD.StartProcessName,
--       #TempRD.CurrentProcessName,
--       #xTemp.QTYIntegrated,
--
--       #xTemp.ESN,
--       #xTemp.Version,
--       #xTemp.SwappedESN,
--       #xTemp.RMANumber,
--       #xTemp.ProjectTag,
--       #xTemp.WayBill,
--       #xTemp.ReceiveDate,
--       #xTemp.ReceiveDate_Date,
--       #xTemp.ReceiveDate_Time,
--
--       #xTemp.ShipDate,
--       #xTemp.Date_QC,
--
--       #xTemp.ML_SKU,
--       #xTemp.ML_UPC,
--       #xTemp.ML_Description,
--       #xTemp.ML_WarrantyStickerPlacement,
--       #xTemp.ML_Device_Handset,
--       #xTemp.ML_Bar_Flip,
--       #xTemp.ML_CDMA_HSPA,
--       #xTemp.ML_Retire,
--       
--       ##Temp321.*,
--       ##Temp3214.*
--  from #xTemp
--Inner join #TempRD on #xTemp.ReceiveDetailID = #TempRD.ReceiveDetailID
--Inner join ##Temp321 on #xTemp.ReceiveDetailID = ##Temp321.ID
--Inner join ##Temp3214 on #xTemp.ReceiveDetailID = ##Temp3214.ID


-- print 'Data Out:' + Convert(varchar(20),getdate(), 120)	
Drop Table #TempRD
Drop Table #temp123
Drop Table #temp123_4

Drop Table #temp123b
Drop Table #temp123_4b


Drop Table #TempName
Drop Table #xTemp
Drop Table #TempZX

--Drop Table ##Temp321
--Drop Table ##Temp3214

if @mShowLog = 1	
   print 'All Done:' + Convert(varchar(30),getdate(), 121)	


END
