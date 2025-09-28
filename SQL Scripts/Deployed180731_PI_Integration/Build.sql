



















/****** Object:  Table [dbo].[OrderHeader]    Script Date: 07/11/2018 16:18:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO


Alter table CycleInventoryCountControlDetail Alter Column SKU nvarchar(50)
Go









































/****** Object:  UserDefinedFunction [dbo].[fn_GetDeviceValue]    Script Date: 07/17/2018 15:52:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
print dbo.fn_GetDeviceValue(7249,1437,1496, 7107 )

Select *
 from InvtPlanMfgModel T1 
where T1.StatusID = 1 
  and T1.InvtValuePlanID = 7249
  and T1.ManufacturerID = 1437 
  and T1.ModelID = 1496

Select *
  From InvtPlanCond TC
  where Tc.StatusID = 1
    and TC.InvtValuePlanID = 7249
    and TC.IFSConditionsID = 7107


*/


ALTER FUNCTION [dbo].[fn_GetDeviceValue](
                @ManufacturerID numeric(15),
                @ModelID numeric(15),
                @IFSConditionsID numeric(15)

)
RETURNS money
AS
BEGIN

----select * from Question where Name = 'Manufacturer'
----Select * from [Option] where QuestionID = 243

---- If we have an Apple or Samsung phone we need to use the CPA condition.
---- Otherwise we need to use the PTG Condtion
--Declare @Abbr nvarchar(10)
--Select @Abbr = O.Name from [Option] O inner join Question Q on Q.QuestionID = O.QuestionID where O.OptionID = @ManufacturerID and Q.Name = 'Manufacturer'

--if (@Abbr = 'SAM' or @Abbr = 'APL')
--   begin
--   Select @IFSConditionsID = O.OptionID 
--     from [Option] O 
--    inner join Question Q on Q.QuestionID = O.QuestionID 
--    where O.Name = 'CPA' and Q.Name = 'IFS Conditions'
--   end
--else
--   begin
--   Select @IFSConditionsID = O.OptionID 
--     from [Option] O 
--    inner join Question Q on Q.QuestionID = O.QuestionID 
--    where O.Name = 'PTG' and Q.Name = 'IFS Conditions'   
--   end   


--Declare @InvtValuePlanID numeric(15)
--Declare @value money
--Declare @Pct numeric(18,7)
--Select @value = 0

--Select @value = Value,
--       @InvtValuePlanID  = T1.InvtValuePlanID
--  from InvtPlanMfgModel T1 
-- where T1.StatusID = 1 
--   and T1.ManufacturerID = @ManufacturerID 
--   and T1.ModelID = @ModelID
   
--Select @value = ISNULL(@value,0);  

--Select @Pct = Pct
--  From InvtPlanCond TC
--  where Tc.StatusID = 1
--    and TC.InvtValuePlanID = @InvtValuePlanID
--    and TC.IFSConditionsID = @IFSConditionsID
--Select @Pct = ISNULL(@Pct,0);  

--Return @value * @Pct

Return 0


END
go

















































































USE [BW_Sandbox02]
GO
/****** Object:  StoredProcedure [dbo].[GetPhysicalInventoryCountList]    Script Date: 07/18/2018 14:02:12 ******/
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

exec GetPhysicalInventoryCountList '000000000003'
Select * from PhysicalInventoryCount

*/

ALTER PROCEDURE [dbo].[GetPhysicalInventoryCountList]
      @Batch nvarchar(50)

AS
BEGIN
	SET NOCOUNT ON;


--Declare @Batch nvarchar(50)
--Select @Batch = '000000000003'
    Declare @IDList AS ListOfIDs 
    
    



    SELECT PhysicalInventoryCountID, ReceiveDetailID
         --, MasterIFSLocationID as MasterLocationID
         --, MasterIFSCondtionID as MasterCondtionID
         , Status, IMEI, Batch, isBatchLocked                     --, isRequestKitted, isRequestUnlock, SetGrade
         , IFSSiteScan as SiteScan
		 , IFSProjectScan as ProjectScan
		 , IFSSite as Site
		 --, IFSProject as Project
		 --, POReceiptDate
		 --, SKU
		 , IFSLocation as ScanningLocation
		 --, IFSCondition as Condition
		 --, IFSConditionCode as ConditionCode
		 , StatusMessage
		 , DuplicateFoundBatches
		 , CreateDate
		 , CreateUser
		 --, ManufacturerID
		 --, ModelID
		 --, CarrierID
		 --, ColourID
     into #Temp1		 
     FROM  PhysicalInventoryCount where Batch = @Batch
     
     Insert @IDList
     Select Distinct ReceiveDetailID from #Temp1 where not ReceiveDetailID is null
     
     ------------------------------------------------------------------------------
     -- Create the Temp table required to house the pivot table data
     Declare @RValue nvarchar(max)
     exec Get_Pivot_RawData_AlterStatement @IDList, '#TempTable', @RValue output
     --Print @RValue
     Create Table #TempTable (KeyID numeric(18))
     EXEC sp_executesql @RValue
     -- Select * from #TempTable
     --Print 'Just Printed TempTable'
     ------------------------------------------------------------------
     -- Get the data we are interested in.
     Insert #TempTable
     --Select * from #TempTable
     exec GetData_Pivot_RawData @IDList

--Select * from #TempTable
--return 
Select ReceiveDetail.ReceiveDetailID,
       ReceiveDetail.ClientLocationID,
       Client.ClientID as cClientID,
       Client.Name as cName,
       Client.CompanyName as cCompanyName,
       Client.ContactName as cContactName,
       Client.BillingAddress as cBillingAddress,
       Client.AddressLine1 as cAddressLine1,
       Client.AddressLine2 as cAddressLine2,
       Client.City as cCity,
       Client.StateOrProvince as cStateOrProvince,
       Client.PostalCode as cPostalCode,
       Client.PhoneNumber as cPhoneNumber,
       Client.FaxNumber as cFaxNumber,
       Client.EmailAddress as cEmailAddress,

       Client.RMASuffix as cRMASuffix,
       Client.isVendorGroup as cisVendorGroup,
       Client.ProductTag as cProductTag,
       Client.UserName as cUserName,

       ClientLocation.ClientLocationID as lClientLocationID,
       ClientLocation.Name as lName,
       ClientLocation.StoreNumber as lStoreNumber,
       ClientLocation.StoreSuffix as lStoreSuffix,
       ClientLocation.ScanKey as lScanKey,
       ClientLocation.MacroKey as lMacroKey,
       ClientLocation.Sequence as lSequence,
       ClientLocation.CompanyName as lCompanyName,
       ClientLocation.ContactName as lContactName,
       ClientLocation.BillingAddress as lBillingAddress,
       ClientLocation.AddressLine1 as lAddressLine1,
       ClientLocation.AddressLine2 as lAddressLine2,
       ClientLocation.City as lCity,
       ClientLocation.StateOrProvince as lStateOrProvince,
       ClientLocation.PostalCode as lPostalCode,
       ClientLocation.PhoneNumber as lPhoneNumber,
       ClientLocation.FaxNumber as lFaxNumber,
       ClientLocation.EmailAddress as lEmailAddress,
       ClientLocation.UserName as lUserName

       , ReceiveDetail.ESN
       , ReceiveDetail.Version
       , ReceiveDetail.SKU as Skux
       , #TempTable.* 
       --into Template_DespatchNote
       into #Temp2
       from #TempTable
       Inner join REceiveDetail on ReceiveDetail.ReceiveDetailID = #TempTable.KeyID
       Inner Join ClientLocation on ClientLocation.ClientLocationID = ReceiveDetail.ClientLocationID
       inner join Client on Client.ClientID = ClientLocation.ClientID
       Order by Manufacturer, Model, Colour


SELECT     A.*, B.*
FROM         #Temp1 A LEFT OUTER JOIN
                      #Temp2 B ON A.ReceiveDetailID = B.ReceiveDetailID

 Drop Table #Temp1
 Drop Table #Temp2
 Drop Table #TempTable
END


USE [BW_Sandbox02]
GO
/****** Object:  StoredProcedure [dbo].[Update_ReceiveDetailPhysicalScan]    Script Date: 07/18/2018 13:58:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>

/*
Declare @ReturnMessage nvarchar(500)
exec Update_ReceiveDetailPhysicalScan -1, 0, 'ddddddddddddddd', '000000001365',''    ,''    ,'','fl1-001-001-001',''         ,'',0,0,0,'jmccomb',@ReturnMessage Output
-- Exec Update_ReceiveDetailPhysicalScan -1, 1179, 'ESN000001'   ,'Test01'       ,'C1NA','BRCE','','FL1-001-001-001','Brand New','',0,0,1,'jmccomb',@ReturnMessage Output
Print @ReturnMessage


Select * from PhysicalInventoryCount
Delete PhysicalInventoryCount

*/


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
	@Grade nvarchar(50),	
	@Kitted bit,
	@Unlocked bit,
	@UpdateIMEI bit,		
	@UserName nvarchar(50),
	@ReturnMessage nvarchar(500) Output
AS
BEGIN
Set NOCOUNT ON

Declare @Log bit
Select @Log = 0

Declare @ISFTransactionDirective smallint
Declare @Version nvarchar(3)
Declare @IFSSite nvarchar(5)
Declare	@IFSProject nvarchar(10)

Declare @Status nvarchar(10)
Declare @DupBatches nvarchar(500)

Declare @ReceiveDetailID numeric(18,0)
Declare @ClientLocationID numeric(18,0)
Declare @Message nvarchar(500)

Declare @IFSPOReceiptDate nvarchar(10)
Declare @IFSLocationActual nvarchar(20)
Declare @IFSConditionCode nvarchar(10)
Select @IFSLocation = UPPER(@IFSLocation)
Select @IFSLocationActual = ''
Select @Message = 'Scanned'
Select @IFSConditionCode = ''
Select @Status = ''
Select @DupBatches = ''

  if @Log = 1
     begin
     Insert JimErrorLog (CreateDate, CreateUser, Source, Message)
     Values (GETDATE(), @UserName, @Batch + ':' + @ESN,'Start')
     end

if Exists (Select * from PhysicalInventoryCount where Batch = @Batch and isBatchLocked = 1)
   begin
   Select @ReturnMessage = 'Error: Batch is locked'
   if @Log = 1
      begin
      Insert JimErrorLog (CreateDate, CreateUser, Source, Message)
      Values (GETDATE(), @UserName, @Batch + ':' + @ESN,@ReturnMessage)
      end

   return
   end
if Exists (Select * from PhysicalInventoryCount where Batch = @Batch and Status = 'Invalid')
   begin
   Select @ReturnMessage = 'Error: Batch is Invalid'
   if @Log = 1
      begin
      Insert JimErrorLog (CreateDate, CreateUser, Source, Message)
      Values (GETDATE(), @UserName, @Batch + ':' + @ESN,@ReturnMessage)
      end   
   return
   end

Select @MasterIFSLocationID = MasterIFSLocationID from MasterIFSLocation a
 Inner join MasterIFSLocationStatus b on a.StatusID = b.MasterIFSLocationStatusID
 where IFSLocation = @IFSLocation and b.Status = 'Active'
 
 
--if isnull(@MasterIFSLocationID, -1) < 1
--   begin
--   Select @ReturnMessage = 'Error: Invalid Location Given'
--   if @Log = 1
--      begin
--      Insert JimErrorLog (CreateDate, CreateUser, Source, Message)
--      Values (GETDATE(), @UserName, @Batch + ':' + @ESN,@ReturnMessage)
--      end   
--   return
--   end



Select Top 1 @ReceiveDetailID = ReceiveDetailID
           , @ClientLocationID = Clientlocation.ClientLocationID
           , @IFSPOReceiptDate = convert(nvarchar(10),ReceiveDate, 101)
           , @Version = Version
           , @IFSLocationActual = IFSLocation 
           , @SKU = SKU
           , @IFSCondition = IFSCondition
           , @IFSConditionCode = IFSCondition
           , @IFSSite = IFSSite
           , @IFSProject = substring(ProjectName,0,10)
  from ReceiveDetail 
  Inner join ReceiveDetailStatus on ReceiveDetailStatus.ReceiveDetailStatusID = ReceiveDetail.StatusID
  inner join ClientLocation on ClientLocation.ClientLocationID = ReceiveDetail.ClientLocationID
 where ESN = @ESN and ReceiveDetailStatus.Status != 'GraveYard' and Version = '000'
 order by ReceiveDetail.CreateDate Desc


--if ISNULL(@ReceiveDetailID,-1) < 1
--   begin
--   Select Top 1 @ReceiveDetailID = ReceiveDetailID, @ClientLocationID = ClientLocationID, @IFSPOReceiptDate = convert(nvarchar(10),ReceiveDate, 101), @Version = Version 
--     from ReceiveDetail 
--    Inner join ReceiveDetailStatus on ReceiveDetailStatus.ReceiveDetailStatusID = ReceiveDetail.StatusID
--    where ESN = @ESN and ReceiveDetailStatus.Status != 'GraveYard' and Version = 'x00'
--    order by ReceiveDetail.CreateDate Desc   
--   end
   
--if ISNULL(@ReceiveDetailID,-1) < 1
--   begin
--   Select Top 1 @ReceiveDetailID = ReceiveDetailID, @ClientLocationID = ClientLocationID, @IFSPOReceiptDate = convert(nvarchar(10),ReceiveDate, 101), @Version = Version 
--    from ReceiveDetail 
--   Inner join ReceiveDetailStatus on ReceiveDetailStatus.ReceiveDetailStatusID = ReceiveDetail.StatusID
--   where ESN = @ESN and ReceiveDetailStatus.Status != 'GraveYard'
--   order by ReceiveDetail.CreateDate Desc
--   END 
   
 
   --if @Log = 1
   --   begin
   --   Insert JimErrorLog (CreateDate, CreateUser, Source, Message)
   --   Values (GETDATE(), @UserName, @Batch + ':' + @ESN,'GetRDID')
   --   end 
 
Select @ReceiveDetailID = ISNULL(@ReceiveDetailID,-1)
if (@ReceiveDetailID < 1)
    begin
	Select @Status = 'Error:'
    Select @Message =  'Error:IMEI not found!'
    --if @Log = 1
    --    begin
    --    Insert JimErrorLog (CreateDate, CreateUser, Source, Message)
    --    Values (GETDATE(), @UserName, @Batch + ':' + @ESN,@ReturnMessage)
    --    end    
    --return    
	end

---- We have an IMEI, Move forward.
--Select @IFSSite = isnull(IFSSite, ''),  @IFSProject = isnull(IFSProject,'') from ClientLocation where ClientLocationID = @ClientLocationID

---- Site and Project Missmatch
--if (@ReceiveDetailID > 0)
--    begin	
--	   if @IFSSite != @IFSSiteScan
--	      begin
--	      Select @Status = 'Error:'
--          Select @Message = @Message + case when len(@Message) > 0 then '/' else '' end + 'Error:Scanned Site(' + @IFSSiteScan + ') != Actual Site(' + @IFSSite + ')'             
--		  end
--	   if @IFSProject != @IFSProjectScan
--	      begin
--	      Select @Status = 'Error:'
--          Select @Message = @Message + case when len(@Message) > 0 then '/' else '' end + 'Error:Scanned Project(' + @IFSProjectScan + ') != Actual Project(' + @IFSProject + ')'
--	      --Select @Status = case when @Status = 'Error:' then @Status else 'Warning:' end
--		  end	
--	end  


---- Manufacturer
--   if @Log = 1
--      begin
--      Insert JimErrorLog (CreateDate, CreateUser, Source, Message)
--      Values (GETDATE(), @UserName, @Batch + ':' + @ESN,'Start Manufacturer')
--      end
--if (@ReceiveDetailID > 0 and Not exists (Select * from ReceiveDetail r 
--                    inner join ReceiveDetailItem i on r.ReceiveDetailID = i.ReceiveDetailID
--                    Inner join [Option] o on o.OptionID = i.OptionID
--              where r.ReceiveDetailID = @ReceiveDetailID  and o.QuestionID = 243))
--    begin
--	Select @Status = 'Error:'
--    Select @Message = @Message + case when len(@Message) > 0 then '/' else '' end + 'Error:No Manufacturer Found!'
--    end
---- Model

--   if @Log = 1
--      begin
--      Insert JimErrorLog (CreateDate, CreateUser, Source, Message)
--      Values (GETDATE(), @UserName, @Batch + ':' + @ESN,'Start Model')
--      end
--if (@ReceiveDetailID > 0 and Not exists (Select * from ReceiveDetail r 
--                    inner join ReceiveDetailItem i on r.ReceiveDetailID = i.ReceiveDetailID
--                    Inner join [Option] o on o.OptionID = i.OptionID
--              where r.ReceiveDetailID = @ReceiveDetailID  and o.QuestionID = 244))
--    begin
--	Select @Status = 'Error:'
	
--    select  @Message = @Message + case when len(@Message) > 0 then '/' else  '' end + 'Error:No Model Found!'  
--    end
---- Carrier
--   if @Log = 1
--      begin
--      Insert JimErrorLog (CreateDate, CreateUser, Source, Message)
--      Values (GETDATE(), @UserName, @Batch + ':' + @ESN,'Start Carroer')
--      end
--if (@ReceiveDetailID > 0 and Not exists (Select * from ReceiveDetail r 
--                    inner join ReceiveDetailItem i on r.ReceiveDetailID = i.ReceiveDetailID
--                    Inner join [Option] o on o.OptionID = i.OptionID
--              where r.ReceiveDetailID = @ReceiveDetailID  and o.QuestionID = 210))
--    begin
--	Select @Status = 'Error:'
--    select  @Message = @Message + case when len(@Message) > 0 then '/' else  '' end + 'Error:No Carrier Found!'
--    end
---- Colour
--   if @Log = 1
--      begin
--      Insert JimErrorLog (CreateDate, CreateUser, Source, Message)
--      Values (GETDATE(), @UserName, @Batch + ':' + @ESN,'Start Colour')
--      end
--if (@ReceiveDetailID > 0 and Not exists (Select * from ReceiveDetail r 
--                    inner join ReceiveDetailItem i on r.ReceiveDetailID = i.ReceiveDetailID
--                    Inner join [Option] o on o.OptionID = i.OptionID
--              where r.ReceiveDetailID = @ReceiveDetailID  and o.QuestionID = 214))
--    begin
--	Select @Status = 'Error:'
--    select  @Message = @Message + case when len(@Message) > 0 then '/' else 'Error:No Colour Found!' end
--    end
	

--Create Index PhysicalInventoryCount_RDID on PhysicalInventoryCount(ReceiveDetailID)


if (@ReceiveDetailID > 0 and @Version != '000')
    begin
	Select @Status = case when @Status = 'Error:' then @Status else 'Warning:' end
    Select @Message = @Message + case when len(@Message) > 0 then '/' else '' end + 'Warning:IMEI, version was:' + @Version
	end
	
-- Look to see if the ReceiveDetailID shows up in any prior non 'Active' Batches.
if (@ReceiveDetailID > 0 and @Status != 'Error:')
    begin
       --if @Log = 1
       --   begin
       --   Insert JimErrorLog (CreateDate, CreateUser, Source, Message)
       --   Values (GETDATE(), @UserName, @Batch + ':' + @ESN,'Start Dups')
       --   end
    Select @DupBatches = @DupBatches + case when len(@DupBatches) > 0 then ',' else '' end + Batch from PhysicalInventoryCount 
                     where ReceiveDetailID = @ReceiveDetailID and Status != 'Invalid' and Substring(Statusmessage,1,6) != 'Error:'
	if len(@DupBatches) > 0
	   begin
	   Select @Status = 'Error:'
       select  @Message = @Message + case when len(@Message) > 0 then '/' else '' end + 'Error:Duplicate Scans:' + @DupBatches
	   end
	End
	
	
--if (@ReceiveDetailID > 0 and @Status != 'Error:')
--    begin
--    if @IFSLocationActual != @IFSLocation
--	   begin
--	   Select @Status = 'Warning:'
--       select  @Message = @Message + case when len(@Message) > 0 then '/' else '' end + 'Warning:Location (' + @IFSLocation + '/' + @IFSLocationActual + ')'
--	   end
--	End	
	
--if (@ReceiveDetailID > 0 and @Status != 'Error:')
--    begin
--    if @IFSSite != 'C1NA'
--	   begin
--	   Select @Status = 'Warning:'
--       select  @Message = @Message + case when len(@Message) > 0 then '/' else '' end + 'Warning:IFS Site (' + @IFSSite + '/' + 'C1NA' + ')'
--	   end
--	End	
	

--Select @IFSConditionCode = Name from [Option] where OptionID = @MasterIFSCondtionID
--if (@UpdateIMEI = 1 and @ReceiveDetailID > 0 and @Status != 'Error:')
--    begin
--       if @Log = 1
--          begin
--          Insert JimErrorLog (CreateDate, CreateUser, Source, Message)
--          Values (GETDATE(), @UserName, @Batch + ':' + @ESN,'Startu Update IMEI')
--          end
--    -- This needs to happen before the SKU is calculated.
--    if @UpdateIMEI = 1
--       begin
--       Select @ISFTransactionDirective = [dbo].[GetIFSDirective]('Ignore',-1)
--       Update ReceiveDetail set ISFTransactionDirective = @ISFTransactionDirective where ReceiveDetailID = @ReceiveDetailID
 
--       -- Un/locking
--       if @Unlocked = 1
--          begin
--          exec [dbo].[UpdateESNAttribute_NoProjectRestriction_BYID] @ReceiveDetailID, 'Unlocking Receive', 'Completed', @UserName  
--          end
--       if @Unlocked = 0
--          begin
--          if exists(SELECT ReceiveDetailItem.ReceiveDetailItemID
--                          FROM ReceiveDetailItem 
--                    INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID AND ReceiveDetailItem.OptionID = [Option].OptionID 
--                    INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
--                         WHERE (ReceiveDetailItem.ReceiveDetailID = @ReceiveDetailID) AND ([Option].OptionText = N'Completed') AND (Question.Name = N'Unlocking Receive') and ReceiveDetailItem.Value = '1')
--             begin
--             Update ReceiveDetailItem set Value = '0'
--             where ReceiveDetailItemID = (SELECT ReceiveDetailItem.ReceiveDetailItemID
--                                            FROM ReceiveDetailItem 
--                                      INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID AND ReceiveDetailItem.OptionID = [Option].OptionID 
--                                      INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
--                                           WHERE (ReceiveDetailItem.ReceiveDetailID = @ReceiveDetailID) AND ([Option].OptionText = N'Completed') AND (Question.Name = N'Unlocking Receive') and ReceiveDetailItem.Value = '1')
--             end
--          end
        
--       -- Un/Kitted
--       if @Kitted = 1
--          begin
--          exec [dbo].[UpdateESNAttribute_NoProjectRestriction_BYID] @ReceiveDetailID, 'IsKitted', 'Yes', @UserName  
--          end   
--       if @Kitted = 0
--          begin
--          exec [dbo].[UpdateESNAttribute_NoProjectRestriction_BYID] @ReceiveDetailID, 'IsKitted', 'No', @UserName  
--          end           
--       end
    
--    --print dbo.GetIFSSKU(1159607)
--       if @Log = 1
--          begin
--          Insert JimErrorLog (CreateDate, CreateUser, Source, Message)
--          Values (GETDATE(), @UserName, @Batch + ':' + @ESN,'Get SKU')
--          end
--    Select @SKU = dbo.GetIFSSKU(@ReceiveDetailID)
--    if @UpdateIMEI = 1  -- true
--       begin
--          if @Log = 1
--             begin
--             Insert JimErrorLog (CreateDate, CreateUser, Source, Message)
--             Values (GETDATE(), @UserName, @Batch + ':' + @ESN,'Update RD, Condtion, SKU,LOC,COND logs')
--             end
--       Update ReceiveDetail set IFSLocation = @IFSLocation, SKU = @SKU, IFSCondition = @IFSConditionCode, Version = '000', ISFTransactionDirective = @ISFTransactionDirective, isIFSLocked = 0  where ReceiveDetailID = @ReceiveDetailID
--       exec [dbo].[UpdateESNAttribute_NoProjectRestriction_BYID] @ReceiveDetailID, 'IFS Conditions', @IFSCondition, @UserName
--       --exec [dbo].[UpdateESNAttribute_NoProjectRestriction_BYID] @ReceiveDetailID, 'Grade', @Grade, @UserName       
       
--       ---------------------------------------------------------------------------------       
--       -- REMOVE THIS ONCE THE TRIGGER IS ADDED FOR RECEIVEDETAIL
--       Insert into ReceiveDetailSKUChangeLog (ReceiveDetailID, SKU, CreateDate,CreateUser) values (@ReceiveDetailID, @SKU, GETDATE(), @UserName)
--       Insert into ReceiveDetailIFSLocationLog (ReceiveDetailID, IFSLocation, MiscText, CreateDate,CreateUser) values (@ReceiveDetailID, @IFSLocation,'', GETDATE(), @UserName )
--       Insert into ReceiveDetailConditionChangeLog (ReceiveDetailID, IFS_Condition, CreateDate,CreateUser) values (@ReceiveDetailID,@IFSConditionCode, GETDATE(), @UserName )
--       end
--    end
--    -------------------------------------------------------------------------------- 
    
--   if @Log = 1
--      begin
--      Insert JimErrorLog (CreateDate, CreateUser, Source, Message)
--      Values (GETDATE(), @UserName, @Batch + ':' + @ESN,'Write out PI Record')
--      end
--Select * from [PhysicalInventoryCount]

Insert [PhysicalInventoryCount](
       [ReceiveDetailID]
      ,[MasterIFSLocationID]
      ,[MasterIFSCondtionID]
      ,[isBatchLocked]
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
	  ,[isRequestKitted]
      ,[isRequestUnlock]
      ,[StatusMessage]
      ,[CreateDate]
      ,[CreateUser])
values (@ReceiveDetailID
       ,@MasterIFSLocationID
       ,@MasterIFSCondtionID
       ,0
	   ,'Active'                 -- case when @Status = 'Error:' then 'Error' else 'Active' end
	   ,@DupBatches
       ,@ESN
       ,@Batch
       ,''                  --@IFSSiteScan
       ,@IFSProjectScan
	   ,''                  --@IFSSite
	   ,@IFSProject
       ,@SKU
       ,@IFSLocation
       ,@IFSCondition
       ,@IFSConditionCode
       ,@IFSPOReceiptDate
       ,@Kitted
       ,@Unlocked
       ,@Status +  @Message
       ,getdate()
       ,@UserName)

      
Select @ReturnMessage = @Status +  @ESN + ' - ' +  @Message

   --if @Log = 1
   --   begin
   --   Insert JimErrorLog (CreateDate, CreateUser, Source, Message)
   --   Values (GETDATE(), @UserName, @Batch + ':' + @ESN,'Done-' + @ReturnMessage)
   --   end 
 
Return 0

END
















































































