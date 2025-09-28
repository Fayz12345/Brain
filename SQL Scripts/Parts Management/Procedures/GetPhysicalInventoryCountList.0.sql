/****** Object:  StoredProcedure [dbo].[GetMasterProjectList]    Script Date: 05/15/2015 09:57:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

/*

exec GetPhysicalInventoryCountList 'batch'


*/

Create PROCEDURE [dbo].[GetPhysicalInventoryCountList]
      @Batch nvarchar(50)

AS
BEGIN
	SET NOCOUNT ON;


SELECT     PhysicalInventoryCount.*
FROM         PhysicalInventoryCount where Batch = @Batch

END





