











































































































































/****** Object:  StoredProcedure [dbo].[GetMasterPartNumbersThisPart]    Script Date: 05/16/2019 10:48:38 ******/
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

Exec GetMasterPartNumbersThisPart 682, -1, -1, -1, -1
Exec GetMasterPartNumbersThisPart -1, -1, -1, 1443, 6121


Select * from MasterPartsLinkTable


*/

ALTER PROCEDURE [dbo].[GetMasterPartNumbersThisPart]
	  @mMasterPartID numeric(18, 0),
	  @mClientID numeric(18, 0),
	  @mClientLocationID numeric(18, 0),
      @mManufacturer nvarchar(50),
      @mModelID numeric(18, 0)          

AS
BEGIN
	SET NOCOUNT ON;
	
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	

Declare @Done Bit
Select @Done = 0


CREATE TABLE #Temp (
	[MasterPartsLinkTableID] [numeric](18, 0)  NOT NULL,
	[MasterPartsID] [numeric](18, 0) NOT NULL,
	[PartNumber] [nvarchar](30) NOT NULL,
	[ClientID] [numeric](18, 0) NULL,
	[Carrier] [varchar](500) NULL,
	[Manufacturer] [nvarchar](50) NULL,
	[Model] [varchar](500) NULL,
	[Quantity] [numeric](18, 0) NOT NULL,
	[MonthendQTY] [numeric](18, 0) NOT NULL,
	[MonthEndDate] [datetime] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,
	[UnitPrice] [numeric](18, 2) NULL,
	[MonthEndUnitPrice] [numeric](18, 2) NULL,
	[GMPPartNumber] [nvarchar](30) NULL,
	[GMPPartDescription] [nvarchar](50) NULL,
	[QTYMin] [numeric](18, 0) NULL,
	[QTYMax] [numeric](18, 0) NULL,
	[QTYReorder] [numeric](18, 0) NULL,
	[MasterPartsClassTypeID] [numeric](18, 0) NULL,
	[ClientLocationID] [numeric](18, 0) NULL,
	[InWarrentyWorkPrice] [numeric](18, 2) NULL,
	[MonthEndInWarrentyWorkPrice] [numeric](18, 2) NULL,
	[AveragePurchasePrice] [numeric](18, 2) NULL,
	[MonthEndAveragePurchasePrice] [numeric](18, 2) NULL)






if (@mModelID < 1)
    Begin
    if (@mMasterPartID < 1)
        begin
        Print 'Inside  1'
        Insert #Temp
        Select MasterPartsLinkTable.* 
          from MasterPartsLinkTable Inner join MasterParts on MasterPartsLinkTable.MasterPartsID = MasterParts.MasterPartsID
         where (Manufacturer = @mManufacturer or Manufacturer = -1) 
           and ClientLocationID = @mClientLocationID 
         order by PartNumber, MasterParts.Description
        Select @Done = 1
        
        
        --Select * from #Temp order by PartNumber
        --return
        END
    else
        begin
        Print 'Inside  2'
        Insert #Temp
        Select MasterPartsLinkTable.* 
        from MasterPartsLinkTable Inner join MasterParts on MasterPartsLinkTable.MasterPartsID = MasterParts.MasterPartsID
        where MasterParts.MasterPartsID = @mMasterPartID 
        and (Manufacturer = @mManufacturer or Manufacturer = -1) 
        and ClientLocationID = @mClientLocationID 
        order by PartNumber, MasterParts.Description
        Select @Done = 1
        END       
    END
else
    Begin
    if (@mMasterPartID < 1)
        begin
        Print 'Inside  3'
        Insert #Temp
        Select MasterPartsLinkTable.* 
        from MasterPartsLinkTable Inner join MasterParts on MasterPartsLinkTable.MasterPartsID = MasterParts.MasterPartsID
        where (Manufacturer = @mManufacturer or Manufacturer = -1) 
          and ClientLocationID = @mClientLocationID 
          and exists(Select * from MasterPartsLinkTableModelList where MasterPartsLinkTableModelList.MasterPartsLinkTableID = MasterPartsLinkTable.MasterPartsLinkTableID 
                                                      and (MasterPartsLinkTableModelList.ModelID = @mModelID or MasterPartsLinkTableModelList.ModelID = -1))
        order by PartNumber, MasterParts.Description                   
        Select @Done = 1
        End
    else
        Begin
        Print 'Inside  4'
        Insert #Temp
        Select MasterPartsLinkTable.* 
        from MasterPartsLinkTable Inner join MasterParts on MasterPartsLinkTable.MasterPartsID = MasterParts.MasterPartsID
        where MasterParts.MasterPartsID = @mMasterPartID 
          and (Manufacturer = @mManufacturer or Manufacturer = -1)
          and ClientLocationID = @mClientLocationID 
        and exists(Select * from MasterPartsLinkTableModelList where MasterPartsLinkTableModelList.MasterPartsLinkTableID = MasterPartsLinkTable.MasterPartsLinkTableID
                                                      and (MasterPartsLinkTableModelList.ModelID = @mModelID or MasterPartsLinkTableModelList.ModelID = -1))
        order by PartNumber, MasterParts.Description                   
        Select @Done = 1
        End       
   end
   
 
-- Select * from #temp 
 
   
if @Done = 0
   begin               
   Insert #Temp
   Select MasterPartsLinkTable.* 
   from MasterPartsLinkTable where MasterPartsLinkTableID = -1             
   end
   
   
        
Select SUM(QTY) as QTY, MasterPartsLinkTableID 
into #Temp2
from MasterPartsTableIFSLocationStorage 
where MasterPartsLinkTableID in (Select MasterPartsLinkTableID from #Temp)
group by MasterPartsLinkTableID


Update #Temp set Quantity = 0 
Update A set A.Quantity = B.QTY
From #Temp A
Inner join #Temp2 B on A.MasterPartsLinkTableID = B.MasterPartsLinkTableID

Select * from #Temp order by PartNumber

Drop Table #Temp
Drop Table #Temp2

END
GO





















/****** Object:  Trigger [dbo].[PartNumberBucketInventoryPlacementAdd]    Script Date: 05/16/2019 12:22:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

Create TRIGGER [dbo].[PartNumberBucketInventoryPlacementAdd]
   ON  [dbo].[PartNumberBucketInventoryPlacement]
  AFTER UPDATE, INSERT
AS 
BEGIN

--Select * from PartNumberBucketInventoryPlacement

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;
  
   
Declare @mPartNumberBucketInventoryPlacementID  numeric(18)
Declare @mUserName nvarchar(50)
Declare @List Table (PartNumberBucketInventoryPlacementID numeric(18))
Declare @ISFTransactionDirective numeric(18)

    
IF (SELECT COUNT(*) FROM inserted) > 0 and (SELECT COUNT(*) FROM Deleted) < 1
 BEGIN
    
    While exists (Select PartNumberBucketInventoryPlacementID from inserted where not PartNumberBucketInventoryPlacementID in (Select PartNumberBucketInventoryPlacementID from @List))
    begin
          Select Top 1 @mPartNumberBucketInventoryPlacementID = PartNumberBucketInventoryPlacementID, @mUserName = CreateUser, @ISFTransactionDirective = ISNULL(IFSDirective, -1) from inserted where not PartNumberBucketInventoryPlacementID in (Select PartNumberBucketInventoryPlacementID from @List)
          insert @List (PartNumberBucketInventoryPlacementID) values (@mPartNumberBucketInventoryPlacementID)
          
          if @ISFTransactionDirective = -1
             Select @ISFTransactionDirective = [dbo].[GetIFSDirective]('Parts_Movement',-1)          
          
          exec IFS_GeneratePartPlacementInvtTran @mPartNumberBucketInventoryPlacementID, @ISFTransactionDirective, @mUserName
    end 
  
   RETURN
 END

    -- Insert statements for trigger here

END


Go



/****** Object:  Trigger [dbo].[PartNumberBucketInventorySourceAdd]    Script Date: 05/16/2019 12:25:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

Create TRIGGER [dbo].[PartNumberBucketInventorySourceAdd]
   ON  [dbo].[PartNumberBucketInventorySource]
  AFTER UPDATE, INSERT
AS 
BEGIN


	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;
	

Declare @mPartNumberBucketInventorySourceID  numeric(18)
Declare @mUserName nvarchar(50)
Declare @List Table (PartNumberBucketInventorySourceID numeric(18))
Declare @ISFTransactionDirective numeric(18)

--Declare   @mMasterPartsLinkTableID numeric(18, 0),
--      @mReceiveDetailID [numeric](18, 0),
--      @mMasterIFSLocationID [numeric](18, 0),            
--      @ToSKUID numeric(18),
--      @QTY smallint,
--      @POCost [numeric](18, 5),
--      @POVendor [nvarchar](50),
--      @PONumber [nvarchar](12),
--      @POReceiptDate Datetime,
--      @POLine [nvarchar](4)

    
IF (SELECT COUNT(*) FROM inserted) > 0 and (SELECT COUNT(*) FROM Deleted) < 1
 BEGIN
    
    While exists (Select PartNumberBucketInventorySourceID from inserted where not PartNumberBucketInventorySourceID in (Select PartNumberBucketInventorySourceID from @List))
    begin
          Select Top 1 @mPartNumberBucketInventorySourceID = PartNumberBucketInventorySourceID, @mUserName = CreateUser, @ISFTransactionDirective = ISNULL(IFSDirective, -1) from inserted where not PartNumberBucketInventorySourceID in (Select PartNumberBucketInventorySourceID from @List)
          insert @List (PartNumberBucketInventorySourceID) values (@mPartNumberBucketInventorySourceID)
          if @ISFTransactionDirective = -1
             Select @ISFTransactionDirective = [dbo].[GetIFSDirective]('Parts_Movement',-1)          
          -- exec IFS_GeneratePartSourceInvtTran @mPartNumberBucketInventorySourceID, @ISFTransactionDirective, @mUserName
          exec IFS_GeneratePartSourceInvtTran @mPartNumberBucketInventorySourceID, @ISFTransactionDirective, @mUserName          
    end 
  
   RETURN
 END

    -- Insert statements for trigger here

END
Go


/****** Object:  Trigger [dbo].[RecordStorageChange]    Script Date: 05/12/2019 10:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

Create TRIGGER [dbo].[RecordStorageChange]
   ON  [dbo].[PartNumberBucketInventorySource]
  AFTER UPDATE, INSERT
AS 
BEGIN

	
/*

Select * from MasterPartsTableIFSLocationStorage


*/	
	
	
SET NOCOUNT ON;
    
IF (SELECT COUNT(*) FROM inserted) > 0 and (SELECT COUNT(*) FROM Deleted) < 1
 BEGIN

       INSERT INTO [dbo].[MasterPartsTableIFSLocationStorage]
           ([MasterPartsLinkTableID]
           ,[MasterIFSLocationID]
           ,[QTY]
           ,[CreateDate]
           ,[CreateUser]
           ,[LastUpdateDate]
           ,[LastUpdateUser])
       Select MasterPartsLinkTableID
             ,MasterIFSLocationID
             ,0
             , LastUpdateDate
             , LastUpdateUser
             , LastUpdateDate
             , LastUpdateUser
      from inserted where MasterIFSLocationID not in (select MasterIFSLocationID from [MasterPartsTableIFSLocationStorage] where MasterPartsTableIFSLocationStorage.MasterIFSLocationID = inserted.MasterIFSLocationID and MasterPartsTableIFSLocationStorage.MasterPartsLinkTableID = inserted.MasterPartsLinkTableID)
      
      Insert into [MasterPartsTableIFSLocationStorageMoveLog]  (FromMasterPartsTableIFSLocationStorageID
                                                             ,ToMasterPartsTableIFSLocationStorageID
                                                             ,PartNumberBucketInventorySourceID
                                                             ,PartNumberBucketInventoryPlacementID
                                                             ,MasterPartsTechAssignedLogID
                                                             ,QTY
                                                             ,CreateDate
                                                             ,CreateUser)
    Select MasterPartsTableIFSLocationStorageID
          ,MasterPartsTableIFSLocationStorageID
          ,PartNumberBucketInventorySourceID
          ,null
          ,null
          ,inserted.Quantity
          ,GETDATE()
          ,inserted.LastUpdateUser
          
     from inserted   
     inner join MasterPartsTableIFSLocationStorage on MasterPartsTableIFSLocationStorage.MasterIFSLocationID = inserted.MasterIFSLocationID
     and MasterPartsTableIFSLocationStorage.MasterPartsLinkTableID = inserted.MasterPartsLinkTableID
     
     
     Update MasterPartsTableIFSLocationStorage set QTY = QTY + inserted.quantity
     from inserted   
     inner join MasterPartsTableIFSLocationStorage on MasterPartsTableIFSLocationStorage.MasterIFSLocationID = inserted.MasterIFSLocationID 
     and MasterPartsTableIFSLocationStorage.MasterPartsLinkTableID = inserted.MasterPartsLinkTableID
  
   RETURN
 END

    -- Insert statements for trigger here

END
Go


/****** Object:  Trigger [dbo].[RecordStorageChangePlacement]    Script Date: 05/16/2019 12:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

Create TRIGGER [dbo].[RecordStorageChangePlacement]
   ON  [dbo].[PartNumberBucketInventoryPlacement]
  AFTER UPDATE, INSERT
AS 
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;



IF (SELECT COUNT(*) FROM inserted) > 0 and (SELECT COUNT(*) FROM Deleted) < 1
 BEGIN

       INSERT INTO [dbo].[MasterPartsTableIFSLocationStorage]
           ([MasterPartsLinkTableID]
           ,[MasterIFSLocationID]
           ,[QTY]
           ,[CreateDate]
           ,[CreateUser]
           ,[LastUpdateDate]
           ,[LastUpdateUser])
       Select MasterPartsLinkTableID
             ,MasterIFSLocationID
             ,0
             , LastUpdateDate
             , LastUpdateUser
             , LastUpdateDate
             , LastUpdateUser
      from inserted where MasterIFSLocationID not in (select MasterIFSLocationID from [MasterPartsTableIFSLocationStorage] where MasterPartsTableIFSLocationStorage.MasterIFSLocationID = inserted.MasterIFSLocationID and MasterPartsTableIFSLocationStorage.MasterPartsLinkTableID = inserted.MasterPartsLinkTableID)
      
      Insert into [MasterPartsTableIFSLocationStorageMoveLog]  (FromMasterPartsTableIFSLocationStorageID
                                                             ,ToMasterPartsTableIFSLocationStorageID
                                                             ,PartNumberBucketInventorySourceID
                                                             ,PartNumberBucketInventoryPlacementID
                                                             ,MasterPartsTechAssignedLogID
                                                             ,QTY
                                                             ,CreateDate
                                                             ,CreateUser)
    Select MasterPartsTableIFSLocationStorageID
          ,MasterPartsTableIFSLocationStorageID
          ,null
          ,PartNumberBucketInventoryPlacementID
          ,MasterPartsTechAssignedLogID
          ,inserted.Quantity
          ,GETDATE()
          ,inserted.LastUpdateUser
     from inserted   
     inner join MasterPartsTableIFSLocationStorage on MasterPartsTableIFSLocationStorage.MasterIFSLocationID = inserted.MasterIFSLocationID
       and MasterPartsTableIFSLocationStorage.MasterPartsLinkTableID = inserted.MasterPartsLinkTableID     
     
     
     
     Update MasterPartsTableIFSLocationStorage set QTY = QTY + inserted.quantity
     from inserted   
     inner join MasterPartsTableIFSLocationStorage on MasterPartsTableIFSLocationStorage.MasterIFSLocationID = inserted.MasterIFSLocationID 
       and MasterPartsTableIFSLocationStorage.MasterPartsLinkTableID = inserted.MasterPartsLinkTableID
  
   RETURN
 END

    -- Insert statements for trigger here

END


Go






























































