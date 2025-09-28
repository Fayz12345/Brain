/****** Object:  StoredProcedure [dbo].[Update_OrderEntry_Shipped_WithRerun_Go]    Script Date: 04/24/2020 14:46:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
*/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Update_OrderEntry_Shipped_WithRerun_Go]
    @mOrderHeaderID numeric(18),
    @mUserName nvarchar(50),
    @mRerun numeric(18) = 0                      -- If it is a rerun, there are some things we don't want to incorperate. 0 = no, 1 = yes   
AS
BEGIN
Set NOCOUNT ON 

Declare @mCarrier nvarchar(50),
        @mShipdate nvarchar(10),
        @mOrderClientCompanyName nvarchar(50),
        @mShippedProcessId numeric(18),
        @mOrderWayBillNumber nvarchar(50),        
        @mOptionID numeric(18),
        @mCustomerPO nvarchar(50),
        @mOrderNumber nvarchar(50),
        @mKitting_SKU nvarchar(50),
        @mShippedDate datetime
        
Create Table #mTempRD (ReceiveHeaderID numeric(18)
                      , ReceiveDetailID numeric(18)
                      , AttributeID numeric(18)
                      , process numeric(15)
                      , ProjectID numeric(18)
                      , ClientLocationID numeric(18)
                      , ClientID numeric(18)
                      , RateValue numeric(18)
                      , SKU nvarchar(20)
                      , Kitting_SKU nvarchar(20)
                      , ESN nvarchar(50)
                      , Version char(3)
                      ,UnitPrice numeric(18,2)
                      , ReplacementESN nvarchar(50)
                      , ReplacementRID numeric(18))                      
                      

Create index TempRDX on #mTempRD(ReceiveDetailID)
Create index TempRDX01 on #mTempRD(AttributeID)
Create index TempRDX02 on #mTempRD(Version)
Create index TempRDX03 on #mTempRD(ESN)
Create index TempRDX04 on #mTempRD(process)

print 'Start:' + Convert(varchar(30),getdate(), 121)	

Select Top 1 @mShippedProcessId = ProcessID
  From Process 
 where Name = 'Shipping'

print 'A:' + Convert(varchar(30),getdate(), 121)	

SELECT @mCarrier = Courier,
       @mShipdate = convert(nvarchar(10),ShippedDate,101),
       @mOrderWayBillNumber = WayBillNumber,
       @mCustomerPO = CustomerPO,
       @mOrderNumber = OrderNumber,
       @mShippedDate = ShippedDate
 FROM  OrderHeader
WHERE (OrderHeaderID = @mOrderHeaderID) 

print 'B:' + Convert(varchar(30),getdate(), 121)	

SELECT @mOrderClientCompanyName = Client.CompanyName
  FROM ClientLocation 
 INNER JOIN Client ON ClientLocation.ClientID = Client.ClientID 
 INNER JOIN OrderCompany ON ClientLocation.ClientLocationID = OrderCompany.ClientLocationID
WHERE (OrderCompany.CompanyType = N'Client') AND (OrderCompany.OrderHeaderID = @mOrderHeaderID)
print 'C:' + Convert(varchar(30),getdate(), 121)	        


-- Get all the REceive Detail ID's 
Insert #mTempRD (ReceiveHeaderID, ReceiveDetailID, AttributeID, process, ProjectID, ClientLocationID, ClientID, RateValue, SKU, Kitting_SKU, UnitPrice, ESN, Version)
SELECT -1, OrderDetailReceiveDetail.ReceiveDetailID, -1, 0, -1, -1, -1, 0, isnull(OrderDetailReceiveDetail.SKU,''), isnull(OrderDetail.SKU,''), isnull(OrderDetail.PurchaseUnitPrice,0), OrderDetailReceiveDetail.ESN, ''
  FROM OrderDetail 
 INNER JOIN OrderDetailReceiveDetail ON OrderDetail.OrderDetailID = OrderDetailReceiveDetail.OrderDetailID
WHERE (OrderDetail.OrderHeaderID = @mOrderHeaderID and not ReceiveDetailID is null)


print '1:' + Convert(varchar(30),getdate(), 121)	

Update #mTempRD set ReceiveHeaderID = ReceiveDetail.ReceiveHeaderID, ClientID = Clientlocation.ClientID, ClientLocationID = clientlocation.ClientLocationID, ProjectID = ReceiveDetail.ProjectID, Version = ReceiveDetail.Version
  from #mTempRD RD
 Inner join ReceiveDetail on RD.ReceiveDetailID = ReceiveDetail.ReceiveDetailID
 Inner join ClientLocation on ReceiveDetail.ClientLocationID = ClientLocation.ClientLocationID
----------------------------------------------------------------------------------------------
print '2:' + Convert(varchar(30),getdate(), 121)	

-- Record the Statistical Records for "Shipped".
--if @mRerun = 0
--   BEGIN
--	INSERT INTO [StatisticalRawData]
--			   ([Processed],[ReceiveDetailID],[Action]
--			   ,[ClientID],[ClientLocationID],[ProjectID],[ProcessID]
--			   ,[Count],[CreateDate],[CreateUser])
--	Select 0, RD.ReceiveDetailID, 'Shipped'
--		 , RD.ClientLocationID, RD.ClientLocationID, RD.ProjectID, @mShippedProcessId
--		 , 1, @mShippedDate,@mUserName
--	  from #mTempRD RD
--   END
----------------------------------------------------------------------------------------------
print '3:' + Convert(varchar(30),getdate(), 121)	
-- Record the Items as Shipped - Process Log

if @mRerun = 0
   BEGIN
   INSERT INTO [ReceiveDetailProcessLog]
              ([ReceiveDetailID],[ProcessID],[ProcessText],[MiscText],[CreateDate],[CreateUser])
   Select RD.ReceiveDetailID, @mShippedProcessId, 'Shipping','',@mShippedDate, @mUserName
     from #mTempRD RD
  END
----------------------------------------------------------------------------------------------
print '4:' + Convert(varchar(30),getdate(), 121)	

           
-- Add the billing points ----------------------------------------------------------
if @mRerun = 0
   BEGIN
   update #mTempRD set process = 0
   -- We are not interested in any that already have the billing point
   Update #mTempRD set process = 2
   from #mTempRD RD
   inner join ReceiveDetailBillingPoints BP on BP.ReceiveDetailID = RD.ReceiveDetailID 
                                          and BP.ClientID = RD.ClientID  
                                          and BP.ProjectID = RD.ProjectID  
                                          and BP.ProcessID = @mShippedProcessId      
   END                                       
print '5:' + Convert(varchar(30),getdate(), 121)	                                          
   -- Any that are not there (process = 0) we need to get any client or generic billing points
   -- Get the client billing points for those with clients
   Update #mTempRD set process = 1, RateValue = BP.RateValue
   from #mTempRD RD
   inner join ClientBillingPoints BP on BP.ClientID = RD.ClientID  
                                    and BP.ProjectID = RD.ProjectID  
                                    and BP.ProcessID = @mShippedProcessId 
   where Process = 0                                                                                 

print '6:' + Convert(varchar(30),getdate(), 121)	
   -- Get the client billing points for generic billing points
   Update #mTempRD set process = 1, RateValue = BP.RateValue
   from #mTempRD RD
   inner join ClientBillingPoints BP on BP.ClientID = -1  
                                    and BP.ProjectID = RD.ProjectID  
                                    and BP.ProcessID = @mShippedProcessId 
   where Process = 0     
   
print '7:' + Convert(varchar(30),getdate(), 121)	                                                                               
   -- We are now ready to add the appropriate records for those that are process = 1
if @mRerun = 0
   BEGIN   
   insert ReceiveDetailBillingPoints (ReceiveDetailID, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser, RateValue, ProcessID, ClientID, ProjectID)
   Select RD.ReceiveDetailID, @mShippedDate, @mUserName, @mShippedDate, @mUserName, RD.RateValue, @mShippedProcessId, RD.ClientID, RD.ProjectID
     From #mTempRD  RD    
    where Process = 1 
   END

print '8:' + Convert(varchar(30),getdate(), 121)	    
   -- Record the Statistical Records for "BillPointA". // Billing point added
--if @mRerun = 0
--   BEGIN   
--   INSERT INTO [StatisticalRawData]
--              ([Processed],[ReceiveDetailID],[Action]
--              ,[ClientID],[ClientLocationID],[ProjectID],[ProcessID]
--              ,[Count],[CreateDate],[CreateUser])
--   Select 0, RD.ReceiveDetailID, 'BillPointA'
--         ,-1, RD.ClientLocationID, RD.ProjectID, @mShippedProcessId
--         ,1, @mShippedDate,@mUserName
--     from #mTempRD RD    
--    where Process = 1    
--   END
   
print '9:' + Convert(varchar(30),getdate(), 121)	    


Create Table #mNewTempAttribute (
	[ReceiveHeaderID] [numeric](18, 0) NOT NULL,
	[ReceiveDetailID] [numeric](18, 0) NOT NULL,
	[Version] [numeric](18, 0) NOT NULL,
	[OptionID] [numeric](18, 0) NOT NULL,
	[Value] [nvarchar](200) NULL,
	[ReceiveDate] [datetime] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,
	[LastUpdate_IP] [nvarchar](20) NULL
)                      
   
----------------------------------------------------------------------------------------------
-- Add Keyboard attributes  
-- 'Kitting SKU'
-- Reset the temprd back to normal.
   update #mTempRD set process = 0, AttributeID = -1

   Select @mOptionID = null
   Select Top 1 @mOptionID = OptionID
     From [Option]
    INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
    WHERE (Question.Name = N'Kitting SKU')
    print '90:Kitting SKU:' + Convert(varchar(30),getdate(), 121)

   if not @mOptionID is null
      BEGIN
       -- Identify those that have the attribute already 
       Update #mTempRD set Process = 1, AttributeID = ReceiveDetailItem.ReceiveDetailItemID
         From #mTempRD RD
        inner join ReceiveDetailItem on RD.ReceiveDetailID = ReceiveDetailItem.ReceiveDetailID
        --INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
        WHERE (ReceiveDetailItem.Version = 0) AND (ReceiveDetailItem.OptionID = @mOptionID)
     print '91:' + Convert(varchar(30),getdate(), 121)      

       -- Add the attribut to those that don't have it
     INSERT INTO [#mNewTempAttribute]
                ([ReceiveHeaderID],[ReceiveDetailID],[Version],[OptionID],[Value]
                ,[ReceiveDate],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
         Select  [ReceiveHeaderID],[ReceiveDetailID], 0,  @mOptionID, Kitting_SKU, @mShippedDate, @mShippedDate, @mUserName, @mShippedDate, @mUserName
          From #mTempRD 
          where Process = 0 and LEN(Kitting_SKU) > 0
       
     print '92:' + Convert(varchar(30),getdate(), 121)      

        Update [ReceiveDetailItem] Set Value = RD.Kitting_SKU, [LastUpdateDate] = @mShippedDate,[LastUpdateUser] = @mUserName
          From #mTempRD RD
         inner join ReceiveDetailItem on RD.AttributeID = ReceiveDetailItem.ReceiveDetailItemID
         Where Process = 1
    print '94:' + Convert(varchar(30),getdate(), 121)         
   END     
   
----------------------------------------------------------------------------------------------
-- 'Out-Bound Waybill-S'
-- Reset the temprd back to normal.
   update #mTempRD set process = 0, AttributeID = -1

   Select @mOptionID = null
   Select Top 1 @mOptionID = OptionID
     From [Option]
    INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
    WHERE (Question.Name = N'Out-Bound Waybill-S')
    print '95:Out-Bound Waybill-S:' + Convert(varchar(30),getdate(), 121)

   if not @mOptionID is null
      BEGIN
       -- Identify those that have the attribute already 
       Update #mTempRD set Process = 1, AttributeID = ReceiveDetailItem.ReceiveDetailItemID
         From #mTempRD RD
        inner join ReceiveDetailItem on RD.ReceiveDetailID = ReceiveDetailItem.ReceiveDetailID
        --INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
        WHERE (ReceiveDetailItem.Version = 0) AND (ReceiveDetailItem.OptionID = @mOptionID)
       print '96:' + Convert(varchar(30),getdate(), 121)
       
       	
       -- Add the attribut to those that don't have it
     INSERT INTO [#mNewTempAttribute]
                ([ReceiveHeaderID],[ReceiveDetailID],[Version],[OptionID],[Value]
                ,[ReceiveDate],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
         Select  [ReceiveHeaderID],[ReceiveDetailID], 0,  @mOptionID, @mOrderWayBillNumber, @mShippedDate, @mShippedDate, @mUserName, @mShippedDate, @mUserName
          From #mTempRD 
          where Process = 0  and LEN(@mOrderWayBillNumber) > 0
       print '97:' + Convert(varchar(30),getdate(), 121)	
       
       ---- Get the attributes for those just added
       --Update #mTempRD set Process = 1, AttributeID = ReceiveDetailItem.ReceiveDetailItemID
       --  From #mTempRD RD
       -- inner join ReceiveDetailItem on RD.ReceiveDetailID = ReceiveDetailItem.ReceiveDetailID
       -- --INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
       -- WHERE (ReceiveDetailItem.Version = 0) AND (ReceiveDetailItem.OptionID = @mOptionID AND RD.Process = 0)         
       -- print '98:' + Convert(varchar(30),getdate(), 121)	
        
                
         -- update the attributes with the proper value.
        Update [ReceiveDetailItem] Set Value = @mOrderWayBillNumber, [LastUpdateDate] = @mShippedDate,[LastUpdateUser] = @mUserName
          From #mTempRD RD
         inner join ReceiveDetailItem on RD.AttributeID = ReceiveDetailItem.ReceiveDetailItemID
         Where Process = 1
   END     

----------------------------------------------------------------------------------------------
-- 'Shipment Date'
-- Reset the temprd back to normal.
   update #mTempRD set process = 0, AttributeID = -1

   Select @mOptionID = null
   Select Top 1 @mOptionID = OptionID
     From [Option]
    INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
    WHERE (Question.Name = N'Shipment Date')
    print '15:Shipment Date:' + Convert(varchar(30),getdate(), 121)    
    
   if not @mOptionID is null
      BEGIN
      -- Identify those that have the attribute already 
       Update #mTempRD set Process = 1, AttributeID = ReceiveDetailItem.ReceiveDetailItemID
         From #mTempRD RD
        inner join ReceiveDetailItem on RD.ReceiveDetailID = ReceiveDetailItem.ReceiveDetailID
        --INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
        WHERE (ReceiveDetailItem.Version = 0) AND (ReceiveDetailItem.OptionID = @mOptionID)
       print '16:' + Convert(varchar(30),getdate(), 121)
       
       
       -- Add the attribut to those that don't have it
     INSERT INTO [#mNewTempAttribute]
                ([ReceiveHeaderID],[ReceiveDetailID],[Version],[OptionID],[Value]
                ,[ReceiveDate],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
         Select  [ReceiveHeaderID],[ReceiveDetailID], 0,  @mOptionID, @mShipdate, @mShippedDate, @mShippedDate, @mUserName, @mShippedDate, @mUserName
          From #mTempRD 
          where Process = 0  and LEN(@mShipdate) > 0   
       print '17:' + Convert(varchar(30),getdate(), 121)       

       ---- Get the attributes for those just added
       --Update #mTempRD set Process = 1, AttributeID = ReceiveDetailItem.ReceiveDetailItemID
       --  From #mTempRD RD
       -- inner join ReceiveDetailItem on RD.ReceiveDetailID = ReceiveDetailItem.ReceiveDetailID
       -- --INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
       -- WHERE (ReceiveDetailItem.Version = 0) AND (ReceiveDetailItem.OptionID = @mOptionID AND RD.Process = 0)         

         -- update the attributes with the proper value.
        Update [ReceiveDetailItem] Set Value = @mShipdate, [LastUpdateDate] = @mShippedDate,[LastUpdateUser] = @mUserName
          From #mTempRD RD
         inner join ReceiveDetailItem on RD.AttributeID = ReceiveDetailItem.ReceiveDetailItemID
         Where Process = 1           
       print '19:' + Convert(varchar(30),getdate(), 121)
         
   END

----------------------------------------------------------------------------------------------
-- 'Carton No'       -- Text box
-- Reset the temprd back to normal.
   update #mTempRD set process = 0, AttributeID = -1

   Select @mOptionID = null
   Select Top 1 @mOptionID = OptionID
     From [Option]
    INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
    WHERE (Question.Name = N'Carton No')
    print '20:Carton No:' + Convert(varchar(30),getdate(), 121)

   if not @mOptionID is null
      BEGIN
       -- Identify those that have the attribute already 
       Update #mTempRD set Process = 1, AttributeID = ReceiveDetailItem.ReceiveDetailItemID
         From #mTempRD RD
        inner join ReceiveDetailItem on RD.ReceiveDetailID = ReceiveDetailItem.ReceiveDetailID
        --INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
        WHERE (ReceiveDetailItem.Version = 0) AND (ReceiveDetailItem.OptionID = @mOptionID)
       print '21:' + Convert(varchar(30),getdate(), 121)        
       
       -- Add the attribut to those that don't have it
     INSERT INTO [#mNewTempAttribute]
                ([ReceiveHeaderID],[ReceiveDetailID],[Version],[OptionID],[Value]
                ,[ReceiveDate],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
         Select  [ReceiveHeaderID],[ReceiveDetailID], 0,  @mOptionID, SKU, @mShippedDate, @mShippedDate, @mUserName, @mShippedDate, @mUserName
          From #mTempRD 
          where Process = 0    and LEN(SKU) > 0   
       print '22:' + Convert(varchar(30),getdate(), 121)       
       -- Get the attributes for those just added

       --Update #mTempRD set Process = 1, AttributeID = ReceiveDetailItem.ReceiveDetailItemID
       --  From #mTempRD RD
       -- inner join ReceiveDetailItem on RD.ReceiveDetailID = ReceiveDetailItem.ReceiveDetailID
       -- --INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
       -- WHERE (ReceiveDetailItem.Version = 0) AND (ReceiveDetailItem.OptionID = @mOptionID AND RD.Process = 0)         
       --print '23:' + Convert(varchar(30),getdate(), 121)        
       
       
       
         -- update the attributes with the proper value.
        Update [ReceiveDetailItem] Set Value = RD.SKU, [LastUpdateDate] = @mShippedDate,[LastUpdateUser] = @mUserName
          From #mTempRD RD
         inner join ReceiveDetailItem on RD.AttributeID = ReceiveDetailItem.ReceiveDetailItemID
         Where Process = 1 
       print '24:' + Convert(varchar(30),getdate(), 121)         
   END   
   
----------------------------------------------------------------------------------------------   
 -- 'ShipTo OE'       -- Text box
-- Reset the temprd back to normal.
   update #mTempRD set process = 0, AttributeID = -1
   
   Select @mOptionID = null
   Select Top 1 @mOptionID = OptionID
     From [Option]
    INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
    WHERE (Question.Name = N'ShipTo OE')
    print '40:ShipTo OE:' + Convert(varchar(30),getdate(), 121)

   if not @mOptionID is null
      BEGIN
       -- Identify those that have the attribute already 
       Update #mTempRD set Process = 1, AttributeID = ReceiveDetailItem.ReceiveDetailItemID
         From #mTempRD RD
        inner join ReceiveDetailItem on RD.ReceiveDetailID = ReceiveDetailItem.ReceiveDetailID
        --INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
        WHERE (ReceiveDetailItem.Version = 0) AND (ReceiveDetailItem.OptionID = @mOptionID)
   print '41:' + Convert(varchar(30),getdate(), 121)
   
          
       -- Add the attribut to those that don't have it
     INSERT INTO [#mNewTempAttribute]
                ([ReceiveHeaderID],[ReceiveDetailID],[Version],[OptionID],[Value]
                ,[ReceiveDate],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
         Select  [ReceiveHeaderID],[ReceiveDetailID], 0,  @mOptionID, @mOrderClientCompanyName, @mShippedDate, @mShippedDate, @mUserName, @mShippedDate, @mUserName
          From #mTempRD 
          where Process = 0   and LEN(@mOrderClientCompanyName) > 0   
         print '42:' + Convert(varchar(30),getdate(), 121)
          
   --    -- Get the attributes for those just added
   --    Update #mTempRD set Process = 1, AttributeID = ReceiveDetailItem.ReceiveDetailItemID
   --      From #mTempRD RD
   --     inner join ReceiveDetailItem on RD.ReceiveDetailID = ReceiveDetailItem.ReceiveDetailID
   --     --INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
   --     WHERE (ReceiveDetailItem.Version = 0) AND (ReceiveDetailItem.OptionID = @mOptionID AND RD.Process = 0)         
   --print '43:' + Convert(varchar(30),getdate(), 121)
           
         -- update the attributes with the proper value.
        Update [ReceiveDetailItem] Set Value = @mOrderClientCompanyName, [LastUpdateDate] = @mShippedDate,[LastUpdateUser] = @mUserName
          From #mTempRD RD
         inner join ReceiveDetailItem on RD.AttributeID = ReceiveDetailItem.ReceiveDetailItemID
         Where Process = 1   
   print '44:' + Convert(varchar(30),getdate(), 121)
            
    END     

----------------------------------------------------------------------------------------------
 -- 'Courier Out'       -- Text box
-- Reset the temprd back to normal.
   update #mTempRD set process = 0, AttributeID = -1

   Select @mOptionID = null
   Select Top 1 @mOptionID = OptionID
     From [Option]
    INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
    WHERE (Question.Name = N'Courier Out')
   print '50:Courier Out:' + Convert(varchar(30),getdate(), 121)    
   if not @mOptionID is null
      BEGIN
       -- Identify those that have the attribute already 
       Update #mTempRD set Process = 1, AttributeID = ReceiveDetailItem.ReceiveDetailItemID
         From #mTempRD RD
        inner join ReceiveDetailItem on RD.ReceiveDetailID = ReceiveDetailItem.ReceiveDetailID
        --INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
        WHERE (ReceiveDetailItem.Version = 0) AND (ReceiveDetailItem.OptionID = @mOptionID)
   print '51:' + Convert(varchar(30),getdate(), 121)
   
          
       -- Add the attribut to those that don't have it
     INSERT INTO [#mNewTempAttribute]
                ([ReceiveHeaderID],[ReceiveDetailID],[Version],[OptionID],[Value]
                ,[ReceiveDate],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
         Select  [ReceiveHeaderID],[ReceiveDetailID], 0,  @mOptionID, @mCarrier, @mShippedDate, @mShippedDate, @mUserName, @mShippedDate, @mUserName
          From #mTempRD 
          where Process = 0   and LEN(@mCarrier) > 0   
   print '52:' + Convert(varchar(30),getdate(), 121)
   
   
   --    -- Get the attributes for those just added
   --    Update #mTempRD set Process = 1, AttributeID = ReceiveDetailItem.ReceiveDetailItemID
   --      From #mTempRD RD
   --     inner join ReceiveDetailItem on RD.ReceiveDetailID = ReceiveDetailItem.ReceiveDetailID
   --     --INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
   --     WHERE (ReceiveDetailItem.Version = 0) AND (ReceiveDetailItem.OptionID = @mOptionID AND RD.Process = 0)         
   --print '53:' + Convert(varchar(30),getdate(), 121) 
   
          
         -- update the attributes with the proper value.
        Update [ReceiveDetailItem] Set Value = @mCarrier, [LastUpdateDate] = @mShippedDate,[LastUpdateUser] = @mUserName
          From #mTempRD RD
         inner join ReceiveDetailItem on RD.AttributeID = ReceiveDetailItem.ReceiveDetailItemID
         Where Process = 1 
   print '54:' + Convert(varchar(30),getdate(), 121)
   
            
   END
   
----------------------------------------------------------------------------------------------   
-- 'PO No'       -- Text box
-- Reset the temprd back to normal.
   update #mTempRD set process = 0, AttributeID = -1

   Select @mOptionID = null
   Select Top 1 @mOptionID = OptionID
     From [Option]
    INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
    WHERE (Question.Name = N'PO No')
    print '60:PO No:' + Convert(varchar(30),getdate(), 121)

   if not @mOptionID is null
      BEGIN
       -- Identify those that have the attribute already 
       Update #mTempRD set Process = 1, AttributeID = ReceiveDetailItem.ReceiveDetailItemID
         From #mTempRD RD
        inner join ReceiveDetailItem on RD.ReceiveDetailID = ReceiveDetailItem.ReceiveDetailID
        --INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
        WHERE (ReceiveDetailItem.Version = 0) AND (ReceiveDetailItem.OptionID = @mOptionID)
print '61:' + Convert(varchar(30),getdate(), 121)      

       -- Add the attribut to those that don't have it
     INSERT INTO [#mNewTempAttribute]
                ([ReceiveHeaderID],[ReceiveDetailID],[Version],[OptionID],[Value]
                ,[ReceiveDate],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
         Select  [ReceiveHeaderID],[ReceiveDetailID], 0,  @mOptionID, @mCustomerPO, @mShippedDate, @mShippedDate, @mUserName, @mShippedDate, @mUserName
          From #mTempRD 
          where Process = 0 and LEN(@mCustomerPO) > 0   
print '62:' + Convert(varchar(30),getdate(), 121)  

     
--       -- Get the attributes for those just added
--       Update #mTempRD set Process = 1, AttributeID = ReceiveDetailItem.ReceiveDetailItemID
--         From #mTempRD RD
--        inner join ReceiveDetailItem on RD.ReceiveDetailID = ReceiveDetailItem.ReceiveDetailID
--        --INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
--        WHERE (ReceiveDetailItem.Version = 0) AND (ReceiveDetailItem.OptionID = @mOptionID AND RD.Process = 0)         
--print '63:' + Convert(varchar(30),getdate(), 121)        


         -- update the attributes with the proper value.
        Update [ReceiveDetailItem] Set Value = @mCustomerPO, [LastUpdateDate] = @mShippedDate,[LastUpdateUser] = @mUserName
          From #mTempRD RD
         inner join ReceiveDetailItem on RD.AttributeID = ReceiveDetailItem.ReceiveDetailItemID
         Where Process = 1 
print '64:' + Convert(varchar(30),getdate(), 121)         
   END

----------------------------------------------------------------------------------------------   
-- 'GMP Order Number'       -- Text box
-- Reset the temprd back to normal.
   update #mTempRD set process = 0, AttributeID = -1

   Select @mOptionID = null
   Select Top 1 @mOptionID = OptionID
     From [Option]
    INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
    WHERE (Question.Name = N'BW Order Number')
    print '70:GMP Order Number:' + Convert(varchar(30),getdate(), 121)
   if not @mOptionID is null
      BEGIN
       -- Identify those that have the attribute already 
       Update #mTempRD set Process = 1, AttributeID = ReceiveDetailItem.ReceiveDetailItemID
         From #mTempRD RD
        inner join ReceiveDetailItem on RD.ReceiveDetailID = ReceiveDetailItem.ReceiveDetailID
        --INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
        WHERE (ReceiveDetailItem.Version = 0) AND (ReceiveDetailItem.OptionID = @mOptionID)
print '71:' + Convert(varchar(30),getdate(), 121)       
      
       
       -- Add the attribut to those that don't have it
     INSERT INTO [#mNewTempAttribute]
                ([ReceiveHeaderID],[ReceiveDetailID],[Version],[OptionID],[Value]
                ,[ReceiveDate],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
         Select  [ReceiveHeaderID],[ReceiveDetailID], 0,  @mOptionID, @mOrderNumber, @mShippedDate, @mShippedDate, @mUserName, @mShippedDate, @mUserName
          From #mTempRD 
          where process = 0  and LEN(@mOrderNumber) > 0   
print '72:' + Convert(varchar(30),getdate(), 121)       

       
--       -- Get the attributes for those just added
--       Update #mTempRD set Process = 1, AttributeID = ReceiveDetailItem.ReceiveDetailItemID
--         From #mTempRD RD
--        inner join ReceiveDetailItem on RD.ReceiveDetailID = ReceiveDetailItem.ReceiveDetailID
--        --INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
--        WHERE (ReceiveDetailItem.Version = 0) AND (ReceiveDetailItem.OptionID = @mOptionID AND RD.Process = 0)    
--print '73:' + Convert(varchar(30),getdate(), 121)             
        
         -- update the attributes with the proper value.
        Update [ReceiveDetailItem] Set Value = @mOrderNumber, [LastUpdateDate] = @mShippedDate,[LastUpdateUser] = @mUserName
          From #mTempRD RD
         inner join ReceiveDetailItem on RD.AttributeID = ReceiveDetailItem.ReceiveDetailItemID
         Where Process = 1 
print '74:' + Convert(varchar(30),getdate(), 121)         
   END   


----------------------------------------------------------------------------------------------
-- 'Bin'       -- Text box
-- Reset the temprd back to normal.
   update #mTempRD set process = 0, AttributeID = -1

   Select @mOptionID = null
   Select Top 1 @mOptionID = OptionID
     From [Option]
    INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
    WHERE (Question.Name = N'Bin')
    print '100:Bin:' + Convert(varchar(30),getdate(), 121)	   
   
   if not @mOptionID is null
      BEGIN
       -- Identify those that have the attribute already 
       Update #mTempRD set Process = 1, AttributeID = ReceiveDetailItem.ReceiveDetailItemID
         From #mTempRD RD
        inner join ReceiveDetailItem on RD.ReceiveDetailID = ReceiveDetailItem.ReceiveDetailID
        --INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
        WHERE (ReceiveDetailItem.Version = 0) AND (ReceiveDetailItem.OptionID = @mOptionID)

       ---- We don't need to add a bin if there is not one there now.
       ---- Add the attribut to those that don't have it
       --INSERT INTO [ReceiveDetailItem]
       --        ([ReceiveHeaderID],[ReceiveDetailID],[Version],[OptionID],[Value]
       --        ,[ReceiveDate],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
       --Select  [ReceiveHeaderID],[ReceiveDetailID], 0,  @mOptionID, '', @mShippedDate, @mShippedDate, @mUserName, @mShippedDate, @mUserName
       --From #mTempRD 
       --where Process = 0
       
       ---- Get the attributes for those just added
       --Update #mTempRD set Process = 1, AttributeID = ReceiveDetailItem.ReceiveDetailItemID
       --  From #mTempRD RD
       -- inner join ReceiveDetailItem on RD.ReceiveDetailID = ReceiveDetailItem.ReceiveDetailID
       -- --INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
       -- WHERE (ReceiveDetailItem.Version = 0) AND (ReceiveDetailItem.OptionID = @mOptionID AND RD.Process = 0)         
        
         -- update the attributes with the proper value.
        Update [ReceiveDetailItem] Set Value = '', [LastUpdateDate] = @mShippedDate,[LastUpdateUser] = @mUserName
          From #mTempRD RD
         inner join ReceiveDetailItem on RD.AttributeID = ReceiveDetailItem.ReceiveDetailItemID
         Where Process = 1 
   END
   
-------------------------------------------------------------------------------------------------------------------
-- 'Location'     -- Dropdown
-- Reset the temprd back to normal.
   update #mTempRD set process = 0, AttributeID = -1

   -- Get the 'None' OptionID
   Select @mOptionID = null
   Select Top 1 @mOptionID = OptionID
     From [Option]
    INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
    WHERE (Question.Name = N'Location' and [Option].OptionText = N'None')
   print '200:Location:' + Convert(varchar(30),getdate(), 121)	
   
   if not @mOptionID is null
      BEGIN
       -- Identify those that have the attribute for NONE already 
       Update #mTempRD set Process = 1, AttributeID = ReceiveDetailItem.ReceiveDetailItemID
         From #mTempRD RD
        inner join ReceiveDetailItem on RD.ReceiveDetailID = ReceiveDetailItem.ReceiveDetailID
        --INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
        WHERE (ReceiveDetailItem.Version = 0 AND ReceiveDetailItem.OptionID = @mOptionID)
print '211:' + Convert(varchar(30),getdate(), 121)	       
       
--       -- Add the attribute to those that don't have it
--       INSERT INTO [ReceiveDetailItem]
--               ([ReceiveHeaderID],[ReceiveDetailID],[Version],[OptionID],[Value]
--               ,[ReceiveDate],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
--       Select  [ReceiveHeaderID],[ReceiveDetailID], 0,  @mOptionID, '1', @mShippedDate, @mShippedDate, @mUserName, @mShippedDate, @mUserName
--       From #mTempRD 
--       where Process = 0
--print '212:' + Convert(varchar(30),getdate(), 121)	       
       
--       -- Get the attributes for those just added
--       Update #mTempRD set Process = 1, AttributeID = ReceiveDetailItem.ReceiveDetailItemID
--         From #mTempRD RD
--        inner join ReceiveDetailItem on RD.ReceiveDetailID = ReceiveDetailItem.ReceiveDetailID
--        --INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID 
--        WHERE (ReceiveDetailItem.Version = 0) AND (ReceiveDetailItem.OptionID = @mOptionID AND RD.Process = 0)         
--print '213:' + Convert(varchar(30),getdate(), 121)	        
        
       -- update the attributes with the proper value.
       Update [ReceiveDetailItem] Set Value = '1', [LastUpdateDate] = @mShippedDate,[LastUpdateUser] = @mUserName
         From #mTempRD RD
        inner join ReceiveDetailItem on RD.AttributeID = ReceiveDetailItem.ReceiveDetailItemID
        Where Process = 1    
print '214:' + Convert(varchar(30),getdate(), 121)	        



       
       -- Disable any of the other Location Attributes for these units
       Update [ReceiveDetailItem] Set Value = '0', [LastUpdateDate] = @mShippedDate,[LastUpdateUser] = @mUserName
         From #mTempRD RD
        inner join ReceiveDetailItem on RD.ReceiveDetailID = ReceiveDetailItem.ReceiveDetailID
        INNER JOIN [Option] ON [Option].OptionID = ReceiveDetailItem.OptionID    
        INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
        WHERE (Question.Name = N'Location' and [Option].OptionText != N'None')
print '215:' + Convert(varchar(30),getdate(), 121)	        
   END
 
 
 
-- Now we need to add any of the new attributes.
INSERT INTO [ReceiveDetailItem]
       ([ReceiveHeaderID],[ReceiveDetailID],[Version],[OptionID],[Value]
       ,[ReceiveDate],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
Select [ReceiveHeaderID],[ReceiveDetailID],[Version],[OptionID],[Value]
       ,[ReceiveDate],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser]
From [#mNewTempAttribute]    



print '250:' + Convert(varchar(30),getdate(), 121)	        
 
----------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------
-- Deal with Replacement IMEI records.
update #mTempRD set process = 1
-- Find any IMEI records that have a Replacement IMEI

                      --, ReplacementESN nvarchar(50)
                      --, ReplacementRID numeric(18)
Update #mTempRD set ReplacementESN = ReceiveDetailItem.Value, process = 0
  From #mTempRD
 Inner join ReceiveDetailItem on ReceiveDetailItem.ReceiveDetailID = #mTempRD.ReceiveDetailID
 Inner Join [Option] on ReceiveDetailItem.OptionID = [Option].OptionID
 Inner Join [Question] on [Option].QuestionID = Question.QuestionID
 Where Question.Name = 'Replacement IMEI'

-- get rid of any blank replacement IMEI records.
Update #mTempRD set process = 1 
where process = 0 and LEN(ISNULL(ReplaceMentESN,'')) = 0

-- If there has already been a swap, we do not want to do it again.
Update #mTempRD set process = 2
  From #mTempRD rd
 Inner join ReceiveDetailIMEISwappedLog on ReceiveDetailIMEISwappedLog.receiveDetailID = rd.ReceiveDetailID
 Where process = 0
 
-- Get the replacement IMEI ReceiveDetailId
Update #mTempRD set ReplacementRID = ReceiveDetail.ReceiveDetailID
  From #mTempRD
 Inner join ReceiveDetail on ReceiveDetail.ESN = #mTempRD.ReplacementESN and ReceiveDetail.Version = '000'
 where process = 0
 
 -- Get Rid of replacement records where the replacement ReceiveDetailID is not found.
 Update #mTempRD set process = 3 
 where process = 0 and (ReplacementRID is null or ReplacementRID < 1)

------------ We have our records (process = 0). 
print '300:' + Convert(varchar(30),getdate(), 121)	   

Insert ReceiveDetailIMEISwappedLog (CreateDate, CreateUser, IMEISwappedIn, IMEISwappedOut, ReceiveDetailID)
Select GETDATE(), @mUserName, #mTempRD.ReplacementESN, #mTempRD.ESN, #mTempRD.ReceiveDetailID
From #mTempRD where process = 0

UPDATE [ReceiveDetail] Set ESN = #mTempRD.ReplacementESN, LastUpdateDate = getdate(), LastUpdateUser = @mUserName
  From ReceiveDetail
  Inner join #mTempRD on #mTempRD.ReceiveDetailID  = ReceiveDetail.ReceiveDetailID
where #mTempRD.process = 0  
  

Declare @mReplacementESN nvarchar(50)
Declare @mReplacementRID numeric(18)
Declare @mOriginalRID numeric(18)
Declare @mOriginalESN nvarchar(50)
declare @mText nvarchar(200)
while exists(Select * from #mTempRD where process = 0)
begin
   Select Top 1 @mOriginalRID = #mTempRD.ReceiveDetailID
              , @mOriginalESN = #mTempRD.ESN
              , @mReplacementESN = #mTempRD.ReplacementESN
              , @mReplacementRID = #mTempRD.ReplacementRID
              
   from #mTempRD where process = 0
   Update #mTempRD set process = 4 where #mTempRD.ReceiveDetailID = @mOriginalRID
   Select @mText = 'Auto Advanced because of swap:' + @mOriginalESN
   exec AdvanceESNVersion @mReplacementESN
   exec UpdateESNAttribute_BYID @mReplacementRID,'ShipTo',@mText,@mUserName
end



--------------------------------------------------------------------------
print '400:' + Convert(varchar(30),getdate(), 121)	  

Update ReceiveDetail set Version = right('000' + ltrim(rtrim(Convert(char(3),COnvert(numeric(18),Version) + 1))),3)
                        ,isIFSLocked = 1
 Where ESN in (Select ESN from #mTempRD where Version = '000')
---------------------------------------------------------------------------------------------- 

print '500:' + Convert(varchar(30),getdate(), 121)	   
if @mRerun = 0
   BEGIN
	Update ReservedAvailableStock Set AssignedDate = @mShippedDate, 
									  AssignedUser = @mUserName,
									  isOpen = 0
	From ReservedAvailableStock b
	where AvailableStock_OrderNumber = @mOrderNumber
   END
   


Drop Table #mTempRD
print '600:' + Convert(varchar(30),getdate(), 121)	+ ':DONE!!!!!!!'
Return 0

END

Go