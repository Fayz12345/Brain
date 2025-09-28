
/****** Object:  StoredProcedure [dbo].[RecordProjectDefinitionProcess]    Script Date: 05/01/2017 17:11:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>


/*


exec GetDeviceCatalogue

Select * from MasterModelMemoryLookup

*/


-- =============================================
Alter PROCEDURE [dbo].[GetDeviceCatalogue]

AS
BEGIN
SET NOCOUNT ON;

Select SKU, COUNT(*) as Qty, CONVERT(int, 0) as Allocated from ReceiveDetail r
Inner join ReceiveDetailStatus s on r.StatusID = s.ReceiveDetailStatusID
inner join ClientLocation CL on cl.ClientLocationID = r.ClientLocationID
inner join Client C on cl.ClientID = c.ClientID
Where Version = '000' and s.Status != 'GraveYard' 
Group By SKU
having count(*) > 0
Order by SKU


END


