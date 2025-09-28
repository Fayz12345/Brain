/****** Object:  View [dbo].[vwGetCCRunHeader]    Script Date: 04/16/2018 12:08:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*

Select * from vwSystemTimeLog
Order by [Transfer Time Seconds] Desc
-- Create Index IDX_SystemTimeLog_ReceiveDetail on SystemTimeLog(ReceiveDetailID)
-- Create Index IDX_SystemTimeLog_SaveTimeBrowserMS on SystemTimeLog(SaveTimeBrowserMS)

Select * from InvtTran_IFS  Directive*/
Create VIEW [dbo].[vwSystemTimeLog]
AS

Select A.*
     , r.ESN
     , R.ProjectName
     , SaveTimeBrowserMS as [Browser Timer MS]
     , SaveTimeMS as [Server Timer MS]
     , SaveTimeBrowserMS/1000 as [Browser Timer Seconds]
     , SaveTimeMS/1000 as [Server Timer Seconds]
 
     , SaveTimeBrowserMS - (SaveTimeBrowserMS - SaveTimeMS) as [Server Process Time MS]
     , SaveTimeBrowserMS - SaveTimeMS as [Transfer Time MS]
     , (SaveTimeBrowserMS - (SaveTimeBrowserMS - SaveTimeMS))/1000 as [Server Process Time Seconds]
     , (SaveTimeBrowserMS - SaveTimeMS)/1000 as [Transfer Time Seconds]
 
     , (SaveTimeBrowserMS - (SaveTimeBrowserMS - SaveTimeMS))/SaveTimeBrowserMS  as [Server Process Time Percent]
     , (SaveTimeBrowserMS - SaveTimeMS)/SaveTimeBrowserMS  as [Transfer Time Percent]

 from SystemTimeLog A
Inner join ReceiveDetail R on R.ReceiveDetailID = A.ReceiveDetailID
where SaveTimeBrowserMS is not null 
-- order by A.SystemTimeLogID Desc



GO


