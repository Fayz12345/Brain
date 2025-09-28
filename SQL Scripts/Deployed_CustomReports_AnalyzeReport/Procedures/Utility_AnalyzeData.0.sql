

/****** Object:  StoredProcedure [dbo].[Utility_AnalyzeData]    Script Date: 02/14/2018 18:26:00 ******/
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

Exec Utility_AnalyzeData 'Jim'

*/

Alter PROCEDURE [dbo].[Utility_AnalyzeData]
                  @UserName nvarchar(50)
   
AS
BEGIN
SET NOCOUNT ON;


CREATE TABLE #Data (
	[ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[KeyID] [numeric](18, 0) NULL,
	[Type] [varchar](100) NOT NULL,	
	[Subject] [varchar](100) NOT NULL,
	[Note] [varchar](500) NOT NULL,	
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL)

Declare @Note varchar(500)

--------------------------------------------------------------------------
Select @Note = 'Database Name'
Insert #Data (KeyID, Type, Subject, Note, CreateDate, CreateUser)
Select -1, 'Database', @Note, db_name(), GETDATE(), @UserName
--------------------------------------------------------------------------
Select @Note = 'Database File Name and Size'
Insert #Data (KeyID, Type, Subject, Note, CreateDate, CreateUser)
Select -1, 'Physical File/SizeMB', Physical_Name, CONVERT(nvarchar(10), (size*8)/1024), GETDATE(), @UserName 
FROM sys.master_files
WHERE DB_NAME(database_id) = db_name()

--------------------------------------------------------------------------
Select @Note = '# of Client records'
Insert #Data (KeyID, Type, Subject, Note, CreateDate, CreateUser)
Select -1, 'Client Records', @Note, CONVERT(nvarchar(10), count(*)), GETDATE(), @UserName from Client
--------------------------------------------------------------------------
Select @Note = '# of Client Location records'
Insert #Data (KeyID, Type, Subject, Note, CreateDate, CreateUser)
Select -1, 'Client Location Records', @Note, CONVERT(nvarchar(10), count(*)), GETDATE(), @UserName from ClientLocation
--------------------------------------------------------------------------
Select @Note = 'Client/Location Freq'
Insert #Data (KeyID, Type, Subject, Note, CreateDate, CreateUser)
Select -1, 'client/Location Freq', '# of Locations for ' + Client.CompanyName , CONVERT(nvarchar(10), count(*)), GETDATE(), @UserName from Client
Inner join ClientLocation L on Client.ClientID = l.ClientID
Group by Client.CompanyName
Order by Client.CompanyName
--------------------------------------------------------------------------

Select @Note = '# of Project records'
Insert #Data (KeyID, Type, Subject, Note, CreateDate, CreateUser)
Select -1, 'Project Records', @Note, CONVERT(nvarchar(10), count(*)), GETDATE(), @UserName from Project
--------------------------------------------------------------------------
Select @Note = '# of Process records'
Insert #Data (KeyID, Type, Subject, Note, CreateDate, CreateUser)
Select -1, 'Process Records', @Note, CONVERT(nvarchar(10), count(*)), GETDATE(), @UserName from Process
--------------------------------------------------------------------------
Select @Note = '# of Question records'
Insert #Data (KeyID, Type, Subject, Note, CreateDate, CreateUser)
Select -1, 'Question Records', @Note, CONVERT(nvarchar(10), count(*)), GETDATE(), @UserName from Question
--------------------------------------------------------------------------
Select @Note = '# of Question Option records'
Insert #Data (KeyID, Type, Subject, Note, CreateDate, CreateUser)
Select -1, 'Option Records', @Note, CONVERT(nvarchar(10), count(*)), GETDATE(), @UserName from [Option]
--------------------------------------------------------------------------
Select @Note = '# of Device records'
Insert #Data (KeyID, Type, Subject, Note, CreateDate, CreateUser)
Select -1, 'Device Freq', @Note, CONVERT(nvarchar(10), count(*)), GETDATE(), @UserName from ReceiveDetail
--------------------------------------------------------------------------
Select @Note = '# Devices in Status Freq'
Insert #Data (KeyID, Type, Subject, Note, CreateDate, CreateUser)
Select -1, 'Device Status Freq', '# of Devices ' + S.Status , CONVERT(nvarchar(10), count(*)), GETDATE(), @UserName from ReceiveDetail
Inner join ReceiveDetailStatus S on s.ReceiveDetailStatusID = ReceiveDetail.StatusID
Group by Status
Order by Status
--Select Distinct ReceiveDetail.StatusID from ReceiveDetail
--------------------------------------------------------------------------
Select @Note = '# Devices in Version Freq'
Insert #Data (KeyID, Type, Subject, Note, CreateDate, CreateUser)
Select -1, 'Device Version Freq', '# of Devices ' + ReceiveDetail.Version , CONVERT(nvarchar(10), count(*)), GETDATE(), @UserName from ReceiveDetail
Group by Version
Order by Version
--Select Distinct ReceiveDetail.StatusID from ReceiveDetail
--------------------------------------------------------------------------
Select @Note = '# of Work Screen Answer records'
Insert #Data (KeyID, Type, Subject, Note, CreateDate, CreateUser)
Select -1, 'Record Freq', @Note, CONVERT(nvarchar(10), count(*)), GETDATE(), @UserName from ReceiveDetailItem
--------------------------------------------------------------------------
--------------------------------------------------------------------------
Select @Note = '# of Work Screen Answer Change log records'
Insert #Data (KeyID, Type, Subject, Note, CreateDate, CreateUser)
Select -1, 'Record Freq', @Note, CONVERT(nvarchar(10), count(*)), GETDATE(), @UserName from ReceiveDetailItem

Select @Note = '# of Work Screen Time Log records'
Insert #Data (KeyID, Type, Subject, Note, CreateDate, CreateUser)
Select -1, 'Record Freq', @Note, CONVERT(nvarchar(10), count(*)), GETDATE(), @UserName from SystemTimeLog
--------------------------------------------------------------------------
Select @Note = '# of ReceiveDetailItem_Archive_02 records'
Insert #Data (KeyID, Type, Subject, Note, CreateDate, CreateUser)
Select -1, 'Record Freq', @Note, CONVERT(nvarchar(10), count(*)), GETDATE(), @UserName from ReceiveDetailItem_Archive_02
--------------------------------------------------------------------------

Select @Note = 'Unused Process'
Insert #Data (KeyID, Type, Subject, Note, CreateDate, CreateUser)
Select ProcessID, 'Process', @Note, Name, GETDATE(), @UserName
 from Process where not ProcessID in (Select distinct ProcessID from ProjectProcess)
 Order by Name
--------------------------------------------------------------------------
Select @Note = 'Unused Question'
Insert #Data (KeyID, Type, Subject, Note, CreateDate, CreateUser)
Select QuestionID, 'Question', @Note, Name, GETDATE(), @UserName
 from Question where not QuestionID in (Select distinct QuestionID from ProcessQuestion)
 Order by Name
--------------------------------------------------------------------------
Select @Note = 'Question Never Answered on work screen'
Insert #Data (KeyID, Type, Subject, Note, CreateDate, CreateUser)
Select QuestionID, 'Question', @Note, Name, GETDATE(), @UserName
 from Question where not QuestionID in (Select distinct QuestionID from [Option] O inner join ReceiveDetailItem I on O.optionID = I.OptionID)
 Order by Name
--------------------------------------------------------------------------
Select @Note = 'Process Never work screen saved'
-- Exclude these from the search -- Those processes not used.
Select ProcessID
  into #Temp
  from Process where not ProcessID in (Select distinct ProcessID from ProjectProcess)
    -- Report any that are attached to a project, but never answered on a device.
Insert #Data (KeyID, Type, Subject, Note, CreateDate, CreateUser)
Select ProcessID, 'Process', @Note, Name, GETDATE(), @UserName
  from Process where not ProcessID in (Select distinct ProcessID from ReceiveDetailProcessLog)
              and not ProcessID in (Select ProcessID from #Temp)
 Order by Name              
--------------------------------------------------------------------------

Select * from #Data Order by ID

return 0

END
Go
