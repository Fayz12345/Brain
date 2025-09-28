/****** Object:  StoredProcedure [dbo].[Utility_ModelSummary]    Script Date: 10/16/2019 11:55:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------

--------------------------------------------------------------------------------------
/*

exec Utility_ModelSummary

*/
--------------------------------------------------------------------------------------
--
*/




Create PROCEDURE [dbo].[Utility_ModelSummary]

 AS

Begin
SET NOCOUNT ON


Declare @QuestionID numeric(18,9)
Select @QuestionID = QuestionID from Question where Name = 'Model'
Select OptionID
     , [Option].Name as ABBR
     , [Option].OptionStatusID
	 , Convert(Datetime, null) as LastUseDate
     , CONVERT(numeric(18), 0) as DevicesAll
     , CONVERT(numeric(18), 0) as Devices000
     , CONVERT(numeric(18), 0) as Devices001
     , CONVERT(numeric(18), 0) as Deviceslookup
     , [Option].OptionStatusID as LookupStatusID
	 , Convert(Datetime, null) as LookupMinCreateDate
	 , Convert(Datetime, null) as LookupMaxCreateDate
  into #TempDevices   
  from [Option] where QuestionID = @QuestionID
  
  
 -- Select T.OptionID, Sum(1) as DevicesAll
 --      , Sum(case when R.Version = '000' then 1 else 0 end) as Devices000
 --      , Sum(case when R.Version = '000' then 0 else 1 end) as Devices001
 -- into #TempDevices01   
 --from #TempDevices T
 -- Inner join ReceiveDetailItem I on T.OptionID = I.OptionID  
 -- Inner join ReceiveDetail R on I.ReceiveDetailID = r.ReceiveDetailID 
 -- group by T.OptionID, R.Version
  
 -- Update #TempDevices set DevicesAll = I.DevicesAll
 --                        ,Devices000 = I.Devices000
 --                        ,Devices001 = I.Devices001
 -- from #TempDevices T
 -- Inner join #TempDevices01 I on T.OptionID = I.OptionID   



  Update #TempDevices set DevicesAll = (Select count(*) from  ReceiveDetailItem I
                                                        Inner join ReceiveDetail R on I.ReceiveDetailID = r.ReceiveDetailID 
														Where optionID = #TempDevices.optionID)
  Update #TempDevices set LastUseDate = (Select Max(I.Createdate) from  ReceiveDetailItem I
                                                        Inner join ReceiveDetail R on I.ReceiveDetailID = r.ReceiveDetailID 
														Where optionID = #TempDevices.optionID)

  Update #TempDevices set Devices000 = (Select count(*) from  ReceiveDetailItem I
                                                        Inner join ReceiveDetail R on I.ReceiveDetailID = r.ReceiveDetailID 
														Where optionID = #TempDevices.optionID and R.Version = '000')


  Update #TempDevices set Devices001 = (Select count(*) from  ReceiveDetailItem I
                                                        Inner join ReceiveDetail R on I.ReceiveDetailID = r.ReceiveDetailID 
														Where optionID = #TempDevices.optionID and R.Version != '000')

  -----------------------------------------------------------------------------------------------
  
  --Update #TempDevices set DevicesAll = Sum(1)
  --                       ,Devices000 = Sum(case when R.Version = '000' then 1 else null end)
  --                       ,Devices001 = Sum(case when R.Version = '000' then null else 1 end)
  --                       ,Deviceslookup = 0
  --from #TempDevices T
  --Inner join ReceiveDetailItem I on T.OptionID = I.OptionID  
  --Inner join ReceiveDetail R on I.ReceiveDetailID = r.ReceiveDetailID
  

  Select T.OptionID, Sum(1) as Deviceslookup
        ,StatusID as LookupStatusID, Min(CreateDate) as LookupMinCreateDate, Max(CreateDate) as LookupMaxCreateDate
  into #TempDevices02   
 from #TempDevices T
  Inner join MasterCarrierManufacturerLookup I on T.OptionID = I.OptionModelID
  group by T.OptionID, StatusID

  Update #TempDevices set Deviceslookup = I.Deviceslookup
                         ,LookupStatusID = I.LookupStatusID
						 ,LookupMinCreateDate = I.LookupMinCreateDate
						 ,LookupMaxCreateDate = I.LookupMaxCreateDate
  from #TempDevices T
  Inner join #TempDevices02 I on T.OptionID = I.OptionID 
  
  
  
 -- Select * from #TempDevices  
  
 SELECT     D.OptionID, O.Name AS ABBR, O.OptionText, O.ScanKey, S.Status AS OptionStatus, O.CreateDate, D.LastUseDate, DevicesAll, Devices000, Devices001, S1.Status AS LookUpStatus, Deviceslookup, D.LookupMinCreateDate, D.LookupMaxCreateDate
FROM         [#TempDevices] AS D INNER JOIN
                      [Option] AS O ON O.OptionID = D.OptionID INNER JOIN
                      OptionStatus AS S ON S.OptionStatusID = D.OptionStatusID LEFT OUTER JOIN
                      MasterCarrierManufacturerStatus AS S1 ON S1.MasterCarrierManufacturerStatusID = D.LookupStatusID
	--where Devices000 > 0 and Devices001 > 0
Order by OptionText, ABBR, OptionStatus, LookUpStatus    
    
    
    
    
  Drop table #TempDevices
  --Drop table #TempDevices01
  Drop table #TempDevices02
  
  

End
