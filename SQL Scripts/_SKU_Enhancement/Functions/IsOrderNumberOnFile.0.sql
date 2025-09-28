

/****** Object:  UserDefinedFunction [dbo].[IsOrderESNMatched]    Script Date: 04/23/2020 14:55:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

/*

Print dbo.IsOrderNumberOnFile('BW0000000005')
Select * from OrderHeader O
Inner join OrderStatus S on O.StatusID = S.OrderStatusID where OrderNumber = 'GMP000000002' and Status != 'Trash'


Select * from OrderHeader
GMP000000002
BW0000000005
BW0000000006
BW0000000008
BW0000000009
BW0000000010
BW0000000011
BW0000000012
BW0000000013
BW0000000014
*/

Create FUNCTION [dbo].[IsOrderNumberOnFile](@mOrderNumber nvarchar(20))
RETURNS Bit
AS
BEGIN
Declare @Valid Bit

Select @Valid = 0 -- false

if Exists (Select * from OrderHeader O
Inner join OrderStatus S on O.StatusID = S.OrderStatusID where OrderNumber = @mOrderNumber and Status != 'Trash')
   begin
   Select @Valid = 1 -- TRUE
   end
   
Return @Valid

END

GO


