
/****** Object:  StoredProcedure [dbo].[GetDashboardQC_01]    Script Date: 10/16/2019 11:42:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*

exec GetDashboardQC_01 'MM/DD/YYYY',12
exec GetDashboardQC_01 '05/06/2019',12
exec GetDashboardQC_01 '',12
exec GetDashboardRepair_AllProcesses '',12

Drop Table Repair_01

Create Table Repair_01
{

}





*/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GetDashboardQC_01]
    @Today nvarchar(10),
    @Days int
AS
BEGIN

Set nocount on

Declare @StartDate Datetime
Declare @EndDate Datetime


if @Days = 0
   Select @Days = 6

if @Days > 0
   Select @Days = @Days * -1
   
   
Select @StartDate = GETDATE()
   
if LEN(@Today) > 0
   begin
   Select @StartDate = convert(datetime,@Today,101)
   end
  
Select @EndDate = DATEADD(d, @Days, @StartDate)
Print @Startdate
Print @EndDate

Select convert(nvarchar(10), @StartDate, 126) as Today
      , replace(ProcessText, ' ', ' ') as ProcessText
      , convert(nvarchar(10), L.CreateDate, 126) + replace(ProcessText, ' ', '_') as ProcessTextb
      , L.CreateUser
      , convert(nvarchar(10), L.CreateDate, 126) as CreateDate
      , (DATEDIFF(dd, CONVERT(DateTime, convert(nvarchar(10), L.CreateDate, 126)), @StartDate) + 0)
       -(DATEDIFF(wk, CONVERT(DateTime, convert(nvarchar(10), L.CreateDate, 126)), @StartDate) * 2)
       -(CASE WHEN DATENAME(dw, @StartDate) = 'Sunday' THEN 1 ELSE 0 END)
       -(CASE WHEN DATENAME(dw, CONVERT(DateTime, convert(nvarchar(10), L.CreateDate, 126))) = 'Saturday' THEN 1 ELSE 0 END) as workdaysdiff      
      ,(DATEDIFF(dd, L.CreateDate, @StartDate) + 1) as daysdiff
      ,(DATEDIFF(wk, L.CreateDate, @StartDate) * 2) as weeksdiff
      , CONVERT(int, 1) as frequency
into #Temp1a  
from ReceiveDetailProcessLog L
Inner join ReceiveDetail R on L.ReceiveDetailID = r.ReceiveDetailID
inner join ClientLocation cl on cl.ClientLocationID = r.ClientLocationID
inner join Process P on P.ProcessID = L.ProcessID
Where L.CreateDate < DateAdd(dd, 1, @StartDate) and L.CreateDate >  @EndDate
  and ProcessText <> 'SAVE'
  and (ProcessText = 'Activation'
  or ProcessText = 'Buffing'
  or ProcessText = 'Function Test'
  or ProcessText = 'Grade Improvement'
  or ProcessText = 'Grading'
  or ProcessText = 'Physical Damage'
  or ProcessText = 'Unlocking')
order by l.CreateDate Desc

--Select * from #Temp1a

SELECT Today, CreateUser, Createdate, workdaysdiff as daysago, [Activation], [Buffing], [Function Test], [Grade Improvement], [Grading], [Physical Damage], [Unlocking], convert(numeric(10),0) as Total, CONVERT(int, 0) as processed from  
             (
                select Today, CreateUser, Createdate, workdaysdiff, [ProcessText], [frequency]
                from #Temp1a
            ) x
            pivot 
            (
                sum([frequency])
                for ProcessText in ([Activation], [Buffing], [Function Test], [Grade Improvement], [Grading], [Physical Damage], [Unlocking])
            ) p order by Createuser, Createdate Desc




Drop Table #Temp1a

END

