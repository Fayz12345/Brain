





































































































/****** Object:  View [dbo].[vwSKUCalculated]    Script Date: 06/21/2017 17:14:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*
Select * from vwSKUCalculated where ReceiveDetailID = 2892
Select dbo.GetIFSSKUCarrierSegment(ReceiveDetailID) from ReceiveDetail  where ReceiveDetailID = 127912
Select dbo.GetIFSSKUKittingSegment(ReceiveDetailID) from ReceiveDetail  where ReceiveDetailID = 127912

Select * from [Option] where OptionID = 1823

Select * from REceiveDetail where ReceiveDetailID = 127912
Select ReceiveDetail.ReceiveDetailID
       ,Manufacturer.Name as Manufactuer,
                                   Model.Name as Model,
                                   dbo.GetIFSSKUCarrierSegment(ReceiveDetailID) as Carrier,
                                   Colour.Name as Colour
FROM         dbo.[Option] AS Colour RIGHT OUTER JOIN
                      dbo.ReceiveDetail ON Colour.OptionID = dbo.ReceiveDetail.ColourID LEFT OUTER JOIN
                      dbo.[Option] AS Model ON dbo.ReceiveDetail.ModelID = Model.OptionID LEFT OUTER JOIN
                      dbo.[Option] as Manufacturer ON dbo.ReceiveDetail.ManufacturerID = Manufacturer.OptionID 
 where ReceiveDetailID = 127912
*/


ALTER VIEW [dbo].[vwSKUCalculated]
AS
SELECT     dbo.ReceiveDetail.ReceiveDetailID
         , dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Manufacturer',3,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Carrier',3,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Model',11,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Memory',5,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Colour',3,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Unlocked Status',1,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Grade',1,' ') + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'IsKitted',2,' ') + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Refurb',1,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Country',2,' ') + '-' + 
           space(9) as  SKU_CALC
         --, CASE WHEN len(isnull(Manufacturer.Name + Model.Name + dbo.GetIFSSKUCarrierSegment(ReceiveDetailID) + Colour.Name, '')) > 25 
         --       THEN LEFT(isnull(isnull(Manufacturer.Name,'XXX') + isnull(Model.Name,'XXX') + dbo.GetIFSSKUCarrierSegment(ReceiveDetailID) + isnull(Colour.Name,'XXX'), ''), 25) 
         --       ELSE isnull(isnull(Manufacturer.Name,'XXX') + isnull(Model.Name,'XXX') + dbo.GetIFSSKUCarrierSegment(ReceiveDetailID) + isnull(Colour.Name,'XXX'), '') + dbo.GetIFSSKUKittingSegment(ReceiveDetailID) 
         --  END AS SKU_CALC1
FROM dbo.ReceiveDetail
--LEFT OUTER JOIN dbo.[Option] AS Colour ON dbo.ReceiveDetail.ColourID = Colour.OptionID
--LEFT OUTER JOIN dbo.[Option] AS Model ON dbo.ReceiveDetail.ModelID = Model.OptionID 
--LEFT OUTER JOIN dbo.[Option] as Manufacturer ON dbo.ReceiveDetail.ManufacturerID = Manufacturer.OptionID


/*


SELECT     ReceiveDetail.ReceiveDetailID, ReceiveDetail.ESN, ReceiveDetail.Version
         , Manufacturer.Name as Manufacturer
         , Model.Name as Model
         , Carrier.Name as Carrier
         , Colour.Name as Colour                           
FROM   ReceiveDetail
Inner join [Option] Manufacturer ON Manufacturer.OptionID = ReceiveDetail.ManufacturerID
INNER JOIN Question ON Question.QuestionID = Manufacturer.QuestionID AND Question.Name = 'Manufacturer'

Inner join [Option] Model ON Model.OptionID = ReceiveDetail.ModelID
INNER JOIN Question q1 ON q1.QuestionID = Model.QuestionID AND q1.Name = 'Model'

Inner join [Option] Carrier ON Carrier.OptionID = ReceiveDetail.CarrierID
INNER JOIN Question q2 ON q2.QuestionID = Carrier.QuestionID AND q2.Name = 'Carrier'

Inner join [Option] Colour ON Colour.OptionID = ReceiveDetail.ColourID
INNER JOIN Question q3 ON q3.QuestionID = Colour.QuestionID AND q3.Name = 'Colour'

Order by ESN, Version, ReceiveDetailID


*/



GO








































/****** Object:  StoredProcedure [dbo].[ProcessScanCode]    Script Date: 06/21/2017 14:52:41 ******/
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


*/

ALTER PROCEDURE [dbo].[ProcessScanCode]
       (@mUnitItemScanCode nvarchar(50),
        @mProcessSource nvarchar(50),
        @mScanCode nvarchar(50),
        @mUserName nvarchar(50),
        @mStepUpName nvarchar(50),
        @mTable nvarchar(20) OUTPUT,
        @mID nvarchar(20) OUTPUT,
        @mValue nvarchar(50) OUTPUT,
        @mMSG nvarchar(200) OUTPUT)


AS
SET NOCOUNT ON;

Declare @mReturnMessage nvarchar(200)
Select @mValue = ''
Select @mTable = ''
Select @mID = ''

Declare @mRMANumber nvarchar(20)
Select @mRMANumber = ''

Declare @mGraveYardID numeric(18)
Select @mGraveYardID = ReceiveDetailStatusID from ReceiveDetailStatus where Status = 'GraveYard'

Declare @mControlType nvarchar(2)
Select @mControlType = ''

Declare @mClientSubmitted nvarchar(1)
Select @mClientSubmitted = ''



    Select @mReturnMessage = Description + ':' + OptionText, @mTable = 'Option', @mID = convert(nvarchar(20), [Option].OptionID),
           @mControlType = Case when QuestionType.Type = 'Dropdown' then 'DD'
                                when QuestionType.Type = 'CheckBox' then 'CB'
                                when QuestionType.Type = 'RadialButton' then 'RD'   
                                when QuestionType.Type = 'Calendar' then 'CA'                   
                                else 'TX' END
      From [Option] 
     Inner Join Question on Question.QuestionID = [Option].QuestionID
     INNER JOIN QuestionType ON Question.QuestionTypeID = QuestionType.QuestionTypeID     
     where ScanKey = @mScanCode
     
-- Scan NextProcessStep
   Declare @mP numeric(15)
   Select @mP = -1
   if @mReturnMessage is null
      Begin
      declare @rid numeric(18)
      
      -- We want to see if this is a MSC Return Device.
      if @mProcessSource = 'MSC Repair Handling'
	     begin
	     -- look to see we have it sitting in MSC right now...
	     if exists(select ReceiveDetailID 
	                 From ReceiveDetail 
                    where ReceiveDetail.ESN = @mScanCode and ReceiveDetail.Version like '8%' and ReceiveDetail.StatusID <> @mGraveYardID)
            begin
            
            -- Get the ID and bring it back.
            Declare @mMessage nvarchar(50)
            Select top 1 @rid = ReceiveDetailID From ReceiveDetail 
                    where ReceiveDetail.ESN = @mScanCode and ReceiveDetail.Version like '8%' and ReceiveDetail.StatusID <> @mGraveYardID
            
            exec UpdateESN_FromMSC_BYID @rid, @mUserName, @mMessage output
            end        
                    
	     end
	     -----------------------------------------------------------------
	     -- Now that it haa been returned to 000, move on down.
	     -----------------------------------------------------------------
      
      
      Select @mReturnMessage = 'ESN Found', @mTable = 'ReceiveDetail'
           , @mID = convert(nvarchar(20), ReceiveDetail.ReceiveDetailID)
           , @mP = ProjectID, @rid = ReceiveDetail.ReceiveDetailID
           , @mRMANumber = isnull(RMANumber,'')
           , @mClientSubmitted = case when (ISNULL(CarrierID, -1) < 1) then '8'
                                      when (ISNULL(ManufacturerID, -1) < 1) then '8'
                                      when (ISNULL(ModelID, -1) < 1) then '8'
                                      when (ISNULL(ColourID, -1) < 1) then '8' else '0' end
                                                                                                                  
        From ReceiveDetail 
       where ReceiveDetail.ESN = @mScanCode and ReceiveDetail.Version = '000' and ReceiveDetail.StatusID <> @mGraveYardID
       
       if not @mReturnMessage is null and isnull(@mP,0) > 0 and @mClientSubmitted != '8'
          Select @mClientSubmitted = case when AllowProjectPassThrough = 1 then '1' else '0' end From Project where ProjectID = @mP
          
          
       if not @mReturnMessage is null and @mClientSubmitted != '8'
          begin
			  declare @pid numeric(18)
			  Select @pid = -1
	          if @mProcessSource = 'RMA RECEIVE'
	             begin
	             if len(@mRMANumber) = 0
	                begin
                    Select @mClientSubmitted = '4'	                
	                end
	             else
	                begin  
				    Select @pid = ProcessID from Process where Name = 'QC Assessment'
				    if exists (Select * from ReceiveDetailProcessLog where ReceiveDetailID = @rid and ReceiveDetailProcessLog.ProcessID = @pid)
				    Select @mClientSubmitted = '3'
				    end
	             end
	          
			  if  @mProcessSource = 'Tech Receive'
			  begin
				  Select @pid = ProcessID from Process where Name = 'Lab Receive'
				  if exists (Select * from ReceiveDetailProcessLog where ReceiveDetailID = @rid and ReceiveDetailProcessLog.ProcessID = @pid)
				  Select @mClientSubmitted = '3'
			  end
			  
			  if  @mProcessSource = 'Tech Finished'
			  begin
				  Select @pid = ProcessID from Process where Name = 'GMP REPAIR'
				  if exists (Select * from ReceiveDetailProcessLog where ReceiveDetailID = @rid and ReceiveDetailProcessLog.ProcessID = @pid)
				  Select @mClientSubmitted = '3'
			  end				  
	          
			  if  @mProcessSource = 'GMP REPAIR' or @mProcessSource = 'LAB BILLING' or @mProcessSource = 'HOLD STATUS' or @mProcessSource = 'REQUEST PARTS'
			  begin     
			  
				  if (@mClientSubmitted != '3')   
				  begin
					  Select @pid = ProcessID from Process where Name = 'Tech Receive'
					  if exists (Select * from ReceiveDetailProcessLog where ReceiveDetailID = @rid and ReceiveDetailProcessLog.ProcessID = @pid)
						 Select @mClientSubmitted = '3'              
				  end   
				  
				  
				  			  
				  --if (@mClientSubmitted != '3')   
				  --begin
					 -- Select @pid = ProcessID from Process where Name = 'GMP REPAIR'
					 -- if exists (Select * from ReceiveDetailProcessLog where ReceiveDetailID = @rid and ReceiveDetailProcessLog.ProcessID = @pid)
						-- Select @mClientSubmitted = '3'              
				  --end   
				  --if (@mClientSubmitted != '3')   
				  --begin
					 -- Select @pid = ProcessID from Process where Name = 'LAB BILLING'
					 -- if exists (Select * from ReceiveDetailProcessLog where ReceiveDetailID = @rid and ReceiveDetailProcessLog.ProcessID = @pid)
						-- Select @mClientSubmitted = '3'              
				  --end 
				  --if (@mClientSubmitted != '3')   
				  --begin
					 -- Select @pid = ProcessID from Process where Name = 'HOLD STATUS'
					 -- if exists (Select * from ReceiveDetailProcessLog where ReceiveDetailID = @rid and ReceiveDetailProcessLog.ProcessID = @pid)
						-- Select @mClientSubmitted = '3'              
				  --end      
				  
				  
				  
				                 
			  end
          end


         
      -- ///////////    JODY, THIS is the piece of code I remmed out.
      if not @mReturnMessage is null and @mProcessSource = 'KITTING' and @mClientSubmitted != '8'
         begin
         declare @mDisposition nvarchar(50)
         select @mDisposition = dbo.GetReceivedQuestionAnswerString_03(@mID,'Disposition')
         if @mDisposition = 'Defective' 
            Select @mClientSubmitted = '4'
         end          

      -- We are only interested if nothing was found, we need to look to see if there is and non 000 versioned units.   
      if @mReturnMessage is null and len(@mProcessSource)>= 7 and substring(@mProcessSource,1,7) = 'RECEIVE' and @mClientSubmitted != '8'
         begin
         if [dbo].IsUnderGMP90DayWarranty(@mScanCode) = 1
            Select @mClientSubmitted = '5'
         end            
      -- ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
          
      END 


-- Scan NextProcessStep
   if @mReturnMessage is null
      Select @mReturnMessage = 'Moved To:' + Process.Description, @mTable = 'NextStep', @mID = convert(nvarchar(20), NextProcessStep.NextProcessStepID)
        From NextProcessStep 
       Inner Join Process on NextProcessStep.TargetStepProcessID = Process.ProcessID
       where NextProcessStep.ScanKey = @mScanCode

-- Scan Process
   if @mReturnMessage is null
      Select @mReturnMessage = 'Current Process Identified as :' + Process.Description, @mTable = 'Process', @mID = convert(nvarchar(20), Process.ProcessID)
        From Process 
       where ScanKey = @mScanCode

-- Scan BinLocation
   if @mReturnMessage is null
      Select @mReturnMessage = 'BinLocation Identified as :' + BinLocation.Name, @mTable = 'BinLocation', @mID = convert(nvarchar(20), BinLocation.BinLocationID)
        From BinLocation 
       where ScanKey = @mScanCode

-- Scan Client Location
   if @mReturnMessage is null
      Select @mReturnMessage = 'ClientLocation:' + convert(nvarchar(20), ClientLocation.ClientLocationID), @mTable = 'ClientLocation', @mID = convert(nvarchar(20), ClientLocation.ClientLocationID)
        From ClientLocation
       Inner join Client on ClientLocation.ClientID = Client.ClientID 
       where ClientLocation.ScanKey = @mScanCode


/*-- Scan Client
   if @mReturnMessage is null
      Select @mReturnMessage = 'Client Identified as :' + Client.Name
        From Client 
       where ScanKey = @mScanCode
*/

-- Scan Command Codes
   if @mReturnMessage is null
      Select @mReturnMessage = 'Command Code Executed :' + CommandCode.Description, @mTable = 'CommandCode', @mID = convert(nvarchar(20), CommandCode.CommandCodeID)
        From CommandCode 
       where ScanKey = @mScanCode

-- Scan Nothing Found
     Select @mReturnMessage = isnull(@mReturnMessage, 'Unknown Scancode')
     Select @mMSG = @mTable + ':' + @mID + ':' + @mReturnMessage + ':' + @mScanCode + ':' + @mControlType + ':' + @mClientSubmitted


--INSERT INTO ScanCodeLog
--           ([UnitItemKey]     ,[UnderProcess]  ,[ScanCode] ,[ParsedResult],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser],[StepUpUser])
--    VALUES (@mUnitItemScanCode, @mProcessSource, @mScanCode, @mMSG , getdate()  , @mUserName , getdate()      ,@mUserName     ,@mStepUpName)

Return

/****** Object:  StoredProcedure [dbo].[UpdateESN_UnShip_BYID]    Script Date: 06/21/2017 12:44:23 ******/
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


*/

Create PROCEDURE [dbo].[UpdateESN_FromMSC_BYID]
    @mReceiveDetailID numeric(18),
    @mUserName nvarchar(50) ='XXXX',
	@mMessage nvarchar(50) output
    
   
AS
BEGIN
Set NOCOUNT on

Select @mMessage = 'Error: Version 8xx not found.'
if exists ( Select * from ReceiveDetail where ReceiveDetailID = @mReceiveDetailID and Version like '8%')
   begin
   Select @mMessage = 'Error: Version 000 already exists.'
   if not exists (Select * from ReceiveDetail where ESN = (Select ESN from ReceiveDetail where ReceiveDetailID = @mReceiveDetailID) and Version = '000')
      begin
      Update ReceiveDetail set Version = '000'
                          , LastUpdateDate = GETDATE()
                          , LastUpdateUser = @mUserName 
      where ReceiveDetailID = @mReceiveDetailID
      Select @mMessage = 'Device moved back from MSC 800.'      
      end
   end

---- List of Attributes that need to be reset.
--exec UpdateESNAttribute_NoProjectRestriction_BYID @mReceiveDetailID, 'Question', 'answer', @mUserName
--exec UpdateESNAttribute_NoProjectRestriction_BYID @mReceiveDetailID, 'Question', 'answer', @mUserName
--exec UpdateESNAttribute_NoProjectRestriction_BYID @mReceiveDetailID, 'Question', 'answer', @mUserName
--exec UpdateESNAttribute_NoProjectRestriction_BYID @mReceiveDetailID, 'Question', 'answer', @mUserName

Return 0

END

/****** Object:  StoredProcedure [dbo].[UpdateESN_UnShip_BYID]    Script Date: 06/21/2017 12:44:23 ******/
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


*/

Create PROCEDURE [dbo].[UpdateESN_ToMSC_BYID]
    @mReceiveDetailID numeric(18),
    @mUserName nvarchar(50) ='XXXX',
	@mMessage nvarchar(50) output
    
   
AS
BEGIN
Set NOCOUNT on

Select @mMessage = 'Error: Version 000 not found.'
if exists ( Select * from ReceiveDetail where ReceiveDetailID = @mReceiveDetailID and Version = '000')
   begin
   Select @mMessage = 'Error: Version 8xx already exists.'
   if not exists (Select * from ReceiveDetail where ESN = (Select ESN from ReceiveDetail where ReceiveDetailID = @mReceiveDetailID) and Version like '8%')
      begin
      Update ReceiveDetail set Version = '800'
                          , LastUpdateDate = GETDATE()
                          , LastUpdateUser = @mUserName 
      where ReceiveDetailID = @mReceiveDetailID
      Select @mMessage = 'Device moved to MSC 800.'      
      end
   end

---- List of Attributes that need to be reset.
--exec UpdateESNAttribute_NoProjectRestriction_BYID @mReceiveDetailID, 'Question', 'answer', @mUserName
--exec UpdateESNAttribute_NoProjectRestriction_BYID @mReceiveDetailID, 'Question', 'answer', @mUserName
--exec UpdateESNAttribute_NoProjectRestriction_BYID @mReceiveDetailID, 'Question', 'answer', @mUserName
--exec UpdateESNAttribute_NoProjectRestriction_BYID @mReceiveDetailID, 'Question', 'answer', @mUserName

Return 0

END
/****** Object:  StoredProcedure [dbo].[UpdateESNAttribute_NoProjectRestriction_BYID]    Script Date: 06/20/2017 14:28:16 ******/
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


*/

Create PROCEDURE [dbo].[UpdateESN_UnShip_BYID]
    @mReceiveDetailID numeric(18),
    @mUserName nvarchar(50) ='XXXX'
    
   
AS
BEGIN
Set NOCOUNT on


Shipto,PSlip,Out-Bound Waybill-S

---- List of Attributes that need to be reset.
exec UpdateESNAttribute_NoProjectRestriction_BYID @mReceiveDetailID, 'Shipto', '', @mUserName
exec UpdateESNAttribute_NoProjectRestriction_BYID @mReceiveDetailID, 'PSlip', '', @mUserName
exec UpdateESNAttribute_NoProjectRestriction_BYID @mReceiveDetailID, 'Out-Bound Waybill-S', '', @mUserName


Update ReceiveDetail set Version = '000'
                       , LastUpdateDate = GETDATE()
                       , LastUpdateUser = @mUserName 
where ReceiveDetailID = @mReceiveDetailID



Return 0

END
















































































