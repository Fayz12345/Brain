--/****** Object:  StoredProcedure [dbo].[GetDeviceCatalogue]    Script Date: 06/25/2017 15:24:18 ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
---- =============================================
---- Author:		<Author,,Name>
---- Create date: <Create Date,,>
---- Description:	<Description,,>


--/*
--Delete BishopCatalogueSendLog

--exec GetDeviceCatalogueChanged

--*/


---- =============================================
--Alter PROCEDURE [dbo].[GetDeviceCatalogueChanged]

--AS
--BEGIN
--SET NOCOUNT ON;




--Declare @mThisBishipGroup numeric(18)
--Declare @mLastBishipGroup numeric(18)
--Declare @mThisSendDate DateTime
--Select @mThisSendDate = GETDATE()
--Select Top 1 @mThisBishipGroup = IDENT_CURRENT( 'BishopCatalogueSendLog' )
----Select Top 1 @mLastBishipGroup = [BishopGroupID] From BishopCatalogueSendLog order by [BishopGroupID] Desc

--Select @mLastBishipGroup = ISNULL(@mLastBishipGroup ,-1)             
----Print  @mThisBishipGroup
----print  @mLastBishipGroup

--Insert BishopCatalogueSendLog ([BishopGroupID],[BishopGroupLastID],[SKU],[Qty],[LastOnHandQTY],[DifferenceQty],[ThisSendDate],[LastSendDate],[Allocated])
--Select @mThisBishipGroup,-1, SKU, COUNT(*) as Qty, CONVERT(int, 0) as LastOnHandQTY, CONVERT(int, 0) as DifferenceQty, @mThisSendDate as ThisSendDate, @mThisSendDate as LastSendDate, CONVERT(int, 0) as Allocated
--from ReceiveDetail r
--Inner join ReceiveDetailStatus s on r.StatusID = s.ReceiveDetailStatusID
--inner join ClientLocation CL on cl.ClientLocationID = r.ClientLocationID
--inner join Client C on cl.ClientID = c.ClientID
--Where 1 = 1 
--  and CL.ScanKey = 'BW1'
--  and Version = '000' 
--  and s.Status != 'GraveYard' 
--Group By SKU
---- having count(*) > 0
--Order by SKU




--Update BishopCatalogueSendLog set BishopGroupLastID = (Select Top 1 BishopCatalogueSendLogID from BishopCatalogueSendLog b 
--                                                              where BishopCatalogueSendLog.SKU = b.SKU 
--                                                                and b.BishopGroupID < @mThisBishipGroup          --b.BishopCatalogueSendLogID
--                                                              Order by BishopGroupID Desc)
--Where BishopCatalogueSendLog.BishopGroupID = @mThisBishipGroup


--Update BishopCatalogueSendLog Set [LastOnHandQTY] = b.QTy, [LastSendDate] = b.[ThisSendDate]
--From BishopCatalogueSendLog 
--Inner join BishopCatalogueSendLog b on BishopCatalogueSendLog.[BishopGroupLastID] = b.BishopCatalogueSendLogID
--Where BishopCatalogueSendLog.BishopGroupID = @mThisBishipGroup -- and BishopCatalogueSendLog.SKU = b.SKU

--Update BishopCatalogueSendLog Set [DifferenceQty] = QTY - LastOnHandQTY
--From BishopCatalogueSendLog 
--Where BishopGroupID = @mThisBishipGroup

--Select * from BishopCatalogueSendLog 
--where [BishopGroupID] = @mThisBishipGroup 
--   -- and DifferenceQty != 0
--Order by SKU

----Select * from BishopCatalogueSendLog 
----Order by SKU



--END


