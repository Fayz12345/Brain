

/****** Object:  StoredProcedure [dbo].[Utility_MovePart]    Script Date: 05/01/2018 21:32:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
----------------------------------------------------------------------------

--------------------------------------------------------------------------------------
-- exec Utility_MoveReceiveDetailItem_Archive_02Offline
--------------------------------------------------------------------------------------
--
*/




CREATE PROCEDURE [dbo].[Utility_MoveReceiveDetailItem_Archive_02Offline]                     

 AS

Begin
Print 'Note these are hardcoded for different database roots.'
--INSERT INTO [Production_OffLine].[dbo].[ReceiveDetailItem_Archive_03]
--           ([ReceiveDetailItemID],[ReceiveHeaderID],[ReceiveDetailID],[Version],[OptionID]
--           ,[Value],[ReceiveDate],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser],[DateMoved])
--SELECT [ReceiveDetailItemID],[ReceiveHeaderID],[ReceiveDetailID],[Version],[OptionID]
--      ,[Value],[ReceiveDate],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser],[DateMoved]
--  FROM [BW_Production].[dbo].[ReceiveDetailItem_Archive_02]


--Delete [BW_Production].[dbo].[ReceiveDetailItem_Archive_02]

END

GO


