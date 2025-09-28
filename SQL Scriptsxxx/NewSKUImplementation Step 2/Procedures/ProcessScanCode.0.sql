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
