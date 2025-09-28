

/****** Object:  UserDefinedFunction [dbo].[GetIFSSKUCarrierSegment]    Script Date: 04/26/2017 11:26:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*

Print dbo.GetIFSCondtion(69510)


*/

Create FUNCTION [dbo].[GetIFSSKUCarrierSegment](@mReceiveDetailID numeric(18))
RETURNS nVarchar(3)
AS
BEGIN
Declare @mReturnValue nvarchar(3)
         

Select @mReturnValue = dbo.GetSKUSegment(@mReceiveDetailID,'Carrier',3,' ')
Return @mReturnValue

END
