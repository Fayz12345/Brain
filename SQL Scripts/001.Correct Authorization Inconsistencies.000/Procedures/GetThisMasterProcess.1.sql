/****** Object:  StoredProcedure [dbo].[GetThisMasterProcessx]    Script Date: 02/22/2019 14:25:23 ******/
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

Exec GetThisMasterProcessx 'jmccomb', 125






Select * from UserTable where UserTableID = 2
select * from 

select userID from aspnet_users where userName = 'jmccomb'


*/

ALTER PROCEDURE [dbo].[GetThisMasterProcess]
      @mUserName varchar(50),
      @mID numeric(18)

AS
BEGIN
	SET NOCOUNT ON;
Declare @mUserTableID numeric(18,0)
Declare @mRole nvarchar(500) = '' 


SELECT @mUserTableID = UserTable.UserTableID
FROM  UserTable 
INNER JOIN UserStatus ON UserTable.StatusID = UserStatus.UserStatusID
WHERE (UserStatus.Status = 'Active') and 
       UserTable.UserName = @mUserName

--print 'UserTableID:' + convert(nvarchar(20), @mUserTableID)

SELECT ProcessID, AllowSelect, AllowAdd, AllowUpdate, AllowDelete, AllowScan
  into #TempQU
  FROM Process
Where ProcessID = @mID



SELECT @mRole = @mRole + aspnet_Roles.RoleName + ','
  FROM aspnet_Users 
 INNER JOIN aspnet_UsersInRoles ON aspnet_Users.UserId = aspnet_UsersInRoles.UserId 
 INNER JOIN aspnet_Roles ON aspnet_UsersInRoles.RoleId = aspnet_Roles.RoleId
 where aspnet_Users.UserName = @mUserName

--Select @mRole as r


update #TempQU set AllowSelect = case when #TempQU.AllowSelect = 1 then 1 else UserAccessTable.AllowSelect end,
                   -- AllowAdd = UserAccessTable.AllowAdd,
                   AllowUpdate = case when #TempQU.AllowUpdate = 1 then 1 else UserAccessTable.AllowUpdate end,
                   AllowDelete = case when #TempQU.AllowDelete = 1 then 1 else UserAccessTable.AllowDelete end,
                   AllowScan = case when #TempQU.AllowScan = 1 then 1 else UserAccessTable.AllowScan end
from #TempQU 
inner join UserAccessTable on UserAccessTable.AllowTableRecordID = -1 and 
           UserAccessTable.TableName = 'Process' and
           UserTableID = @mUserTableID

update #TempQU set AllowSelect = case when #TempQU.AllowSelect = 1 then 1 else UserAccessTable.AllowSelect end,
                   -- AllowAdd = UserAccessTable.AllowAdd,
                   AllowUpdate = case when #TempQU.AllowUpdate = 1 then 1 else UserAccessTable.AllowUpdate end,
                   AllowDelete = case when #TempQU.AllowDelete = 1 then 1 else UserAccessTable.AllowDelete end,
                   AllowScan = case when #TempQU.AllowScan = 1 then 1 else UserAccessTable.AllowScan end
from #TempQU 
inner join UserAccessTable on #TempQU.ProcessID = UserAccessTable.AllowTableRecordID and 
           UserAccessTable.TableName = 'Process' and
           UserTableID = @mUserTableID



---------------------------------
update #TempQU set AllowSelect = case when #TempQU.AllowSelect = 1 then 1 else RoleAccessTable.AllowSelect end,
                   -- AllowAdd = UserAccessTable.AllowAdd,
                   AllowUpdate = case when #TempQU.AllowUpdate = 1 then 1 else RoleAccessTable.AllowUpdate end,
                   AllowDelete = case when #TempQU.AllowDelete = 1 then 1 else RoleAccessTable.AllowDelete end,
                   AllowScan = case when #TempQU.AllowScan = 1 then 1 else RoleAccessTable.AllowScan end
from #TempQU 
inner join RoleAccessTable on RoleAccessTable.AllowTableRecordID = -1 and 
           RoleAccessTable.TableName = 'Process' and
           RoleAccessTable.Role in (Select * from dbo.fn_SplitDistinct( @mRole,','))
        
--Select * from #TempQU

           
update #TempQU set AllowSelect = case when #TempQU.AllowSelect = 1 then 1 else RoleAccessTable.AllowSelect end,
                   -- AllowAdd = UserAccessTable.AllowAdd,
                   AllowUpdate = case when #TempQU.AllowUpdate = 1 then 1 else RoleAccessTable.AllowUpdate end,
                   AllowDelete = case when #TempQU.AllowDelete = 1 then 1 else RoleAccessTable.AllowDelete end,
                   AllowScan = case when #TempQU.AllowScan = 1 then 1 else RoleAccessTable.AllowScan end
from #TempQU 
inner join RoleAccessTable on #TempQU.ProcessID = RoleAccessTable.AllowTableRecordID and 
           RoleAccessTable.TableName = 'Process' and
           RoleAccessTable.Role in (Select * from dbo.fn_SplitDistinct( @mRole,','))
                      

--Select * from #TempQU




Delete #TempQU where isnull(AllowSelect,0) = 0

--Select * from #TempQU



SELECT #TempQU.[ProcessID]
      ,[ScanKey]
      ,[MacroKey]
      ,[Name]
      ,[Description]
      ,[Description_Client]
      ,[StatusID]
      ,[Sequence]
      ,#TempQU.[AllowSelect]
      ,#TempQU.[AllowAdd]
      ,#TempQU.[AllowUpdate]
      ,#TempQU.[AllowDelete]
      ,#TempQU.[AllowScan]
      ,[CreateDate]
      ,[CreateUser]
      ,[LastUpdateDate]
      ,[LastUpdateUser]
      ,[ShowCompletedStatus]
      ,[ButtonText]
      ,[NextRMANumber]
      ,[RMASuffix]
      ,[isReadOnly]
      ,[BucketCount]
      ,[BucketCountOffset]
      ,[ShowTat],CanJumpProject,TurnStickyOn,MinutesToYellow,MinutesToRed,DisablePrint, ForcePrintOnSave, Process.AllowXBINX
 from #TempQU      
Inner join [Process] on #TempQU.ProcessID = Process.ProcessID



END
