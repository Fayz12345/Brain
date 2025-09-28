
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
SELECT     ReceiveDetail.ESN, ReceiveDetail.Version, Process.Name as ProcessName
      , SystemTimeLog.[SystemTimeLogID]
      , SystemTimeLog.[RecordType]
      , SystemTimeLog.[ReceiveDetailID]
      , SystemTimeLog.[ProcessID]
      , SystemTimeLog.[MasterPartsRequestedLogID]
      , SystemTimeLog.[StartTimeDate]
      , SystemTimeLog.[EndTimeDate]
      , SystemTimeLog.[SaveTimeMS]
      , SystemTimeLog.[SaveTimeBrowserMS]
      , SystemTimeLog.[RecordDetailString]
      , SystemTimeLog.[CreateIPAddress]
      , SystemTimeLog.[CreateDate]
      , SystemTimeLog.[CreateUser]
      , SystemTimeLog.[LastUpdateDate]
      , SystemTimeLog.[LastUpdateUser]
FROM         SystemTimeLog INNER JOIN
                      ReceiveDetail ON SystemTimeLog.ReceiveDetailID = ReceiveDetail.ReceiveDetailID INNER JOIN
                      Process ON SystemTimeLog.ProcessID = Process.ProcessID
where RecordType = 'WorkScreenSave' 


GO




















