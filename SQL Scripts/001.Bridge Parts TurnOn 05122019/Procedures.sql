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





















