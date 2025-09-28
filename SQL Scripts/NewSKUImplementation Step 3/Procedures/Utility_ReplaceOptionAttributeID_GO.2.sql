/****** Object:  StoredProcedure [dbo].[Utility_ReplaceOptionAttributeID_GO]    Script Date: 07/31/2017 11:37:14 ******/
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

Select * from Question where name = 'Model'
Select * from [Option] o where -- questionID = 14 and 
         exists(select * from [option] b where b.optiontext = o.optiontext and b.QuestionID = o.questionID and b.OptionID != o.OptionID)
 order by optiontext


declare @mRecords int
exec Utility_ReplaceOptionAttributeID 149, 150, @mRecords Output
Print 'Rows Affected:' + convert(nvarchar(20), @mRecords)



exec Utility_ReplaceOptionAttributeID_GO 842,843

exec Utility_ReplaceOptionAttributeID_GO 2443,1872
exec Utility_ReplaceOptionAttributeID_GO 4569,4450
exec Utility_ReplaceOptionAttributeID_GO 2498,1722
exec Utility_ReplaceOptionAttributeID_GO 1757,1445
exec Utility_ReplaceOptionAttributeID_GO 2499,1759
exec Utility_ReplaceOptionAttributeID_GO 1768,1444
exec Utility_ReplaceOptionAttributeID_GO 2524,2520
exec Utility_ReplaceOptionAttributeID_GO 2568,2564
exec Utility_ReplaceOptionAttributeID_GO 2569,2565
exec Utility_ReplaceOptionAttributeID_GO 2566,1784
exec Utility_ReplaceOptionAttributeID_GO 2570,1784
exec Utility_ReplaceOptionAttributeID_GO 2571,2567
exec Utility_ReplaceOptionAttributeID_GO 5841,5840
exec Utility_ReplaceOptionAttributeID_GO 2383,1869
exec Utility_ReplaceOptionAttributeID_GO 2476,2475
exec Utility_ReplaceOptionAttributeID_GO 3272,2648
exec Utility_ReplaceOptionAttributeID_GO 6160,5995
exec Utility_ReplaceOptionAttributeID_GO 2421,1840
exec Utility_ReplaceOptionAttributeID_GO 2342,1891
exec Utility_ReplaceOptionAttributeID_GO 3359,3315
exec Utility_ReplaceOptionAttributeID_GO 2481,1899
exec Utility_ReplaceOptionAttributeID_GO 2795,2794
exec Utility_ReplaceOptionAttributeID_GO 2731,1771
exec Utility_ReplaceOptionAttributeID_GO 3235,2627

exec Utility_ReplaceOptionAttributeID_GO 2570, 2566

2566
2570



*/

ALTER PROCEDURE [dbo].[Utility_ReplaceOptionAttributeID_GO]
        @mSourceID numeric(18),
        @mTargetOptionID numeric(18),
        @mUserName nvarchar(20)

AS
BEGIN
SET NOCOUNT ON;


declare @mRecords int
declare @mTotalRecords int
Select @mTotalRecords = 0
exec Utility_ReplaceOptionAttributeID @mSourceID, @mTargetOptionID, @mUserName, @mRecords Output
Print 'Rows Affected:' + convert(nvarchar(20), @mRecords) + ' Source:' + convert(nvarchar(20), @mSourceID) + ' Target:' + convert(nvarchar(20), @mTargetOptionID)
Select @mTotalRecords = @mTotalRecords + isnull(@mRecords,0)


End
GO