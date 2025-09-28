USE [BW_Sandbox02]
GO
/****** Object:  UserDefinedFunction [dbo].[GetSKU]    Script Date: 06/29/2017 16:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*

Declare @rValue nvarchar(50)
exec dbo.GetSKUWildcard 'LGE-null-null-null-BLK-null-null-null-null-null', @rValue OUTPUT
   exec dbo.GetSKUWildcard 'SAM-BEL-null-null-BLK-null-null-null-null-null', @rValue OUTPUT
-- exec dbo.GetSKUWildcard 'LGE-null-null-null-BLK-null-null-null-null-null', @rValue OUTPUT
-- exec dbo.GetSKUWildcard 'LGE-null-null-null-BLK-null-null-null-null-null', @rValue OUTPUT
-- exec dbo.GetSKUWildcard 'LGE-null-null-null-BLK-null-null-null-null-null', @rValue OUTPUT
-- exec dbo.GetSKUWildcard 'LGE-null-null-null-BLK-null-null-null-null-null', @rValue OUTPUT
-- exec dbo.GetSKUWildcard 'LGE-null-null-null-BLK-null-null-null-null-null', @rValue OUTPUT
-- exec dbo.GetSKUWildcard 'LGE-null-null-null-BLK-null-null-null-null-null', @rValue OUTPUT
-- exec dbo.GetSKUWildcard 'LGE-null-null-null-BLK-null-null-null-null-null', @rValue OUTPUT
-- exec dbo.GetSKUWildcard 'LGE-null-null-null-BLK-null-null-null-null-null', @rValue OUTPUT
Print @rValue
Select ReceiveDetailID, SKU from ReceiveDetail where Version= '000' and SKU Like @rValue
Select SKU from ReceiveDetail where Version= '000'

LGE-UNL-       D820-     -BLK- -C-  - -           
LGE-ROG-       D820-     -BLK- -C-  - -                        
LGE-___-___________-_____-BLK-_-_-__-_-__

Select * from ReceiveDetail where SKU like 'LGE-___-___________-_____-BLK-_-_-__-_-___________'

Select ReceiveDetailID, SKU from ReceiveDetail where Version = '000'

Update receiveDetail set SKU = SKU_Calc
From ReceiveDetail
Inner join vwSKUCalculated on ReceiveDetail.ReceiveDetailID = vwSKUCalculated.ReceiveDetailID
Where Version = '000'


ReceiveDetail.ReceiveDetailID in (
44,85,91,92,94,95,97,98,100,101,102,104,105,107,109,112,113,114,115,117,123,124,127,129,130,131,134,135,136,138,
139,144,145,148,150,151,153,154,155,157,161,165,169,176,178,179,180,181,183,184,186,188,202,206,207,208,210,216,
217,218,220,221,222,223,224,225,226,227,229,230,231,232,233,234,235,236,241,242,243,245,246,250,251)


Select * from vwSKUCalculated where ReceiveDetailID in (
44,85,91,92,94,95,97,98,100,101,102,104,105,107,109,112,113,114,115,117,123,124,127,129,130,131,134,135,136,138,
139,144,145,148,150,151,153,154,155,157,161,165,169,176,178,179,180,181,183,184,186,188,202,206,207,208,210,216,
217,218,220,221,222,223,224,225,226,227,229,230,231,232,233,234,235,236,241,242,243,245,246,250,251)


Version = '000'

*/

Create procedure [dbo].[GetSKUWildcard](@SKU nvarchar(100),
                 @rValue nvarchar(50) OUTPUT
                 )
AS
BEGIN
set Nocount on
Select * 
into #Tempx
from dbo.fn_splitSKU(@SKU,'-') 

Select @rValue = ''
Select @rValue = @rValue + Wildcard + '-'
From #Tempx
order by #Tempx.position

Print len(@rValue)

Select @rValue = SUBSTRING(@rValue, 0, len(@rValue))
-- We need to include the fillter at the end
Select @rValue = @rValue + '_________'


Print len(@rValue)


END
