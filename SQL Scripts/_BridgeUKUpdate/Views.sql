
/****** Object:  View [dbo].[vwReceiveDetailCellbie]    Script Date: 10/16/2019 12:00:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*

Select * from vwReceiveDetailCellbie

*/


CREATE VIEW [dbo].[vwReceiveDetailCellbie]
AS


Select C.ReceiveDetailCellbieStatusID, C.Status
     , C.MiscText
     , c.LastUpdateDate as LastUpdateDate_Cellbie
     , R.* from ReceiveDetailCellbieStatus C
Inner join ReceiveDetail R on C.ReceiveDetailID = R.ReceiveDetailID






GO
























