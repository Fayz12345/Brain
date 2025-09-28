/****** Object:  StoredProcedure [dbo].[Update_ReceiveDetailPhysicalScan]    Script Date: 05/19/2015 23:20:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Update_ReceiveDetailPhysicalScan]
	@MasterIFSLocationID numeric(18, 0),
	@MasterIFSCondtionID numeric(18, 0), 
	@ESN nvarchar(50),
	@Batch nvarchar(25),	
	@IFSSiteScan nvarchar(5),
	@IFSProjectScan nvarchar(10),
	--@IFSPOReceiptDate nvarchar(10),
	@SKU nvarchar(25),
	@IFSLocation nvarchar(20),
	@IFSCondition nvarchar(50),
	@UpdateIMEI bit,
	@UserName nvarchar(50),
	@ReturnMessage nvarchar(500) Output
AS
BEGIN
Declare @Version nvarchar(3)
Declare @IFSSite nvarchar(5)
Declare	@IFSProject nvarchar(10)

Declare @Status nvarchar(10)
Declare @DupBatches nvarchar(500)



Declare @ReceiveDetailID numeric(18,0)
Declare @ClientLocationID numeric(18,0)
Declare @Message nvarchar(100)

Declare @IFSPOReceiptDate nvarchar(10)

Declare @IFSConditionCode nvarchar(10)
Select @Message = 'Scanned'
Select @IFSConditionCode = ''
Select @Status = ''
Select @DupBatches = ''

if Exists (Select * from PhysicalInventoryCount where Batch = @Batch and isBatchLocked = 1)
   begin
   Select @ReturnMessage = 'Error: Batch is locked'
   return
   end

Select @MasterIFSLocationID = MasterIFSLocationID from MasterIFSLocation where IFSLocation = @IFSLocation
if isnull(@MasterIFSLocationID, -1) < 1
   begin
   Select @ReturnMessage = 'Error: Invalid Location Given'
   return
   end


Select Top 1 @ReceiveDetailID = ReceiveDetailID, @ClientLocationID = ClientLocationID, @IFSPOReceiptDate = convert(nvarchar(10),ReceiveDate, 101), @Version = Version from ReceiveDetail where ESN = @ESN order by CreateDate Desc
Select @ReceiveDetailID = ISNULL(@ReceiveDetailID,-1)
if (@ReceiveDetailID < 1)
    begin
	Select @Status = 'Error:'
    select @Message = 'IMEI found!'
	end

if (@ReceiveDetailID > 0 and @Version != '000')
    begin
	Select @Status = 'Warning:'
    select @Message = 'IMEI, version is:' + @Version
	end


-- Look to see if the ReceiveDetailID shows up in any prior non 'Active' Batches.
if (@ReceiveDetailID < 1)
    begin
    Select @DupBatches = @DupBatches + case when len(@DupBatches) > 0 then ',' else '' end + Batch from PhysicalInventoryCount where ReceiveDetailID = @ReceiveDetailID
	if len(@DupBatches) > 0
	   begin
       Select @Status = 'Warning:'
       select  @Message = @Message + case when len(@Message) > 0 then '/' else '' end + 'Duplicate Scans:' + @DupBatches

	   end
	End



Select @IFSConditionCode = Name from [Option] where OptionID = @MasterIFSCondtionID
if (@ReceiveDetailID > 1)
    begin
    Select @SKU = dbo.GetIFSSKU(@ReceiveDetailID)
    if @UpdateIMEI = 1         -- true
       begin
       Select @IFSSite = isnull(IFSSite, ''),  @IFSProject = isnull(IFSProject,'') from ClientLocation where ClientLocationID = @ClientLocationID
	   --if (len(@IFSSite) > 0 or len(@IFSProject) > 0)
	      --begin
          --Update ClientLocation set IFSSite = case when len(@IFSSite) = 0 then IFSSite else @IFSSite end
	         --                      ,IFSProject = case when len(@IFSProject) = 0 then IFSProject else @IFSProject end
          --where ClientLocationID = @ClientLocationID
		  --end
       Update ReceiveDetail set IFSLocation = @IFSLocation, SKU = @SKU, IFSCondition = @IFSConditionCode where ReceiveDetailID = @ReceiveDetailID
       exec [dbo].[UpdateESNAttribute_BYID] @ReceiveDetailID, 'IFS Conditions', @IFSCondition, @UserName
       Select @Message = 'Updated'
	   if @IFSSite != @IFSSiteScan
	      begin
          Select @Message = @Message + case when len(@Message) > 0 then '/' else '' end + 'Scanned Site(' + @IFSSiteScan + ') != Actual Site(' + @IFSSite + ')'
		  	Select @Status = 'Warning:'
		  end
	   if @IFSProject != @IFSProjectScan
	      begin
          Select @Message = @Message + case when len(@Message) > 0 then '/' else '' end + 'Scanned Project(' + @IFSProjectScan + ') != Actual Project(' + @IFSProject + ')'
		  	Select @Status = 'Warning:'
		  end
       end
	   -- REMOVE THIS ONCE THE TRIGGER IS ADDED FOR RECEIVEDETAIL
	    exec IFS_GenerateInvtTran @ReceiveDetailID, 'S', 'L', 'C', @UserName
    end
    

--Select * from [PhysicalInventoryCount]

Insert [PhysicalInventoryCount](
       [ReceiveDetailID]
      ,[MasterIFSLocationID]
      ,[MasterIFSCondtionID]
	  ,[Status]
	  ,[DuplicateFoundBatches]
      ,[IMEI]
      ,[Batch]
      ,[IFSSiteScan]
      ,[IFSProjectScan]
      ,[IFSSite]
      ,[IFSProject]
      ,[SKU]
      ,[IFSLocation]
      ,[IFSCondition]
      ,[IFSConditionCode]
	  ,[POReceiptDate]
      ,[StatusMessage]
      ,[CreateDate]
      ,[CreateUser])
values (@ReceiveDetailID
       ,@MasterIFSLocationID
       ,@MasterIFSCondtionID
	   ,'Active'
	   ,@DupBatches
       ,@ESN
       ,@Batch
       ,@IFSSiteScan
       ,@IFSProjectScan
	   ,@IFSSite
	   ,@IFSProject
       ,@SKU
       ,@IFSLocation
       ,@IFSCondition
       ,@IFSConditionCode
       ,@IFSPOReceiptDate
       ,@Status +  @Message
       ,getdate()
       ,@UserName)
       
Select @ReturnMessage = @Status +  @ESN + ' - ' +  @Message
 
Return 0

END





