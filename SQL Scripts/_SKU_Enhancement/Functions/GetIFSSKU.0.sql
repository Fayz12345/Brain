

/****** Object:  UserDefinedFunction [dbo].[GetIFSSKU]    Script Date: 04/23/2020 13:21:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*

Print dbo.GetIFSCondtion(69510)

47981
47982
47983
47985
47987
47990


Select Top 100 ReceiveDetailID
Into #TempRD
 from ReceiveDetail
where Version = '000' -- and IFSCondition is null
Order by CreateDate Desc

Update ReceiveDetail set IFSCondition = dbo.GetIFSCondtion(ReceiveDetailID)
Where REceiveDetailID in (Select ReceiveDetailID from #TempRD)

Select * from ReceiveDetail
Where REceiveDetailID in (Select ReceiveDetailID from #TempRD)


Update ReceiveDetail set IFSCondition = NULL
Where REceiveDetailID in (Select ReceiveDetailID from #TempRD)

Drop table #TempRD
Select Top 1001 ReceiveDetailID, IFSCondition, dbo.GetIFSCondtion(ReceiveDetailID) from ReceiveDetail

Create Index Question_Condition2 on Question(IFS_Condition, IFS_Condition_Sequence)


*/

ALTER FUNCTION [dbo].[GetIFSSKU](@mReceiveDetailID numeric(18))
RETURNS nVarchar(50)
AS
BEGIN
Declare @mReturnValue nvarchar(50)
Select @mReturnValue = '';

Select @mReturnValue = SKU_Calc from vwSKUCalculated where ReceiveDetailID = @mReceiveDetailID
Select @mReturnValue = ISNULL(@mReturnValue,'')

Return @mReturnValue

END
GO
