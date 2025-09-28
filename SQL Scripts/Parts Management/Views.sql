


/****** Object:  View [dbo].[vwIFS_InvtTran]    Script Date: 05/11/2015 12:11:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*

Select * from vwIFS_InvtTran_B
Select * from InvtTran_IFS

ALTER TABLE InvtTran_IFS drop COLUMN FromValuePct
go
ALTER TABLE InvtTran_IFS drop COLUMN ToValuePct
go

*/




ALTER VIEW [dbo].[vwIFS_InvtTran]
AS
SELECT     ReceiveDetail.ReceiveDetailID, ReceiveDetail.ESN, ReceiveDetail.Version, InvtTran_IFS.InvtTranID
                    , InvtTran_IFS.IFSSite
					, InvtTran_IFS.IFSProject
					, InvtTran_IFS.POVendor
					, InvtTran_IFS.PONumber
					, InvtTran_IFS.POReceiptDate

					, InvtTran_IFS.POLine
					, InvtTran_IFS.POCost
                    , InvtTran_IFS.Quantity
                    , InvtTran_IFS.GMPPartNumber

                    , InvtTran_IFS.FromSku
					, InvtTran_IFS.FromLocation
					, InvtTran_IFS.FromCondition
					, InvtTran_IFS.ToSku
					, InvtTran_IFS.ToLocation
					, InvtTran_IFS.ToCondition, InvtTran_IFS.CreatedDate, InvtTran_IFS.CreateUser, 
                      InvtTran_IFS.Directive, InvtTran_IFS.CreateSource, InvtTran_IFS.RetrievedBatch, InvtTran_IFS.RetrievedDate, InvtTran_IFS.ProcessID, Process.Name AS Process, 
                      InvtTran_IFS.ToSKUID, InvtTran_IFS.ToLocationID, InvtTran_IFS.ToConditionID
                    , InvtTran_IFS.MiscNote, InvtTran_IFS.StatusID
FROM         InvtTran_IFS INNER JOIN
                      ReceiveDetail ON ReceiveDetail.ReceiveDetailID = InvtTran_IFS.ReceiveDetailID INNER JOIN
                      Process ON InvtTran_IFS.ProcessID = Process.ProcessID







GO




















