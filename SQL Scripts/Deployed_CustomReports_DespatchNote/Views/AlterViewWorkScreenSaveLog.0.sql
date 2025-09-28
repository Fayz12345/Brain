
/****** Object:  View [dbo].[ViewWorkScreenSaveLog]    Script Date: 02/13/2018 17:09:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*


Select * from ViewWorkScreenSaveLog


*/
Alter VIEW [dbo].[ViewWorkScreenSaveLog]
AS

SELECT  SystemTimeLog.[SystemTimeLogID]
      , SystemTimeLog.[ReceiveDetailID]
      , SystemTimeLog.[ProcessID]
      -- , SystemTimeLog.[MasterPartsRequestedLogID]
      , SystemTimeLog.[RecordType]
      , ReceiveDetail.ESN
      , ReceiveDetail.Version
      , Process.Name as ProcessName
      , SystemTimeLog.[StartTimeDate]
      , SystemTimeLog.[EndTimeDate]
      , isnull(SystemTimeLog.[SaveTimeMS], 0) as SaveTimeSQLMS
      , isnull(SystemTimeLog.[SaveTimeBrowserMS], 0) as SaveTimeBrowserMS
      , case when isnull(SystemTimeLog.[SaveTimeBrowserMS], 0) - isnull(SystemTimeLog.[SaveTimeMS], 0) < 0 then 0
             else isnull(SystemTimeLog.[SaveTimeBrowserMS], 0) - isnull(SystemTimeLog.[SaveTimeMS], 0) end as SaveTimeDifferenceMS            
      , SystemTimeLog.[RecordDetailString]
      , SystemTimeLog.[CreateIPAddress]
      , SystemTimeLog.[CreateDate]
      , SystemTimeLog.[CreateUser]
      , SystemTimeLog.[LastUpdateDate]
      , SystemTimeLog.[LastUpdateUser]
FROM SystemTimeLog 
INNER JOIN ReceiveDetail ON SystemTimeLog.ReceiveDetailID = ReceiveDetail.ReceiveDetailID 
INNER JOIN Process ON SystemTimeLog.ProcessID = Process.ProcessID
where RecordType = 'WorkScreenSave' 



GO
