/****** Object:  StoredProcedure [dbo].[GetDashboardInventoryQTY_01_GridValueFiltered]    Script Date: 10/16/2019 11:45:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*

Receiver
Supervisors



exec GetDashboardInventoryQTY_01_GridValueFiltered -1,'',''
exec GetDashboardInventoryQTY_01_GridValueFiltered -1,'Product Room',''
exec GetDashboardInventoryQTY_01_GridValueFiltered -1,'All',''

exec GetDashboardInventoryQTY_01_GridValueFiltered 2,'QC Shelf',''
exec GetDashboardInventoryQTY_01_GridValueFiltered 2,'Product Room',''
exec GetDashboardInventoryQTY_01_GridValueFiltered 3,'Product Room',''
exec GetDashboardInventoryQTY_01_GridValueFiltered 28,'Product Room',''
exec GetDashboardInventoryQTY_01_GridValueFiltered 23,'Product Room',''
exec GetDashboardInventoryQTY_01_GridValueFiltered 24,'Product Room',''
exec GetDashboardInventoryQTY_01_GridValueFiltered 27,'Product Room',''
exec GetDashboardInventoryQTY_01_GridValueFiltered 28,'Product Room',''
exec GetDashboardInventoryQTY_01_GridValueFiltered 3,'Apple ID Lock ',''


select * from Project
Select * from Question where Name = 'Product Place'
Select * from [Option] where QuestionID = 566


Insert into #Temp1
exec GetDashboardRepair_01 '10/06/2016',12

Select * from #Temp1


*/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GetDashboardInventoryQTY_01_GridValueFiltered]
    @ProjectID numeric(18),
    @Product_Place nvarchar(50),
    @RoleFilter nvarchar(max)
AS
BEGIN

Set NOCOUNT on
Declare @PrintIt int
Select @PrintIt = 0

if @PrintIt = 1
   print Convert(varchar(30),getdate(), 121)	 + ': Start (all times are at the start of the step, can also be understood as the end of the prior step):'
Declare @delimiter varchar(20)
Select @delimiter = ','
SELECT convert(numeric(18),0) as Processed, * into #TempRoles FROM fn_SplitDistinct(@RoleFilter, @delimiter)
Update #TempRoles set value = LTRIM(rtrim(Value))
--Select * from #TempRoles
--Select * from fn_SplitDistinct('aaa, aaab,ddddddd', ',')

if @Product_Place = 'All' or @Product_Place = 'ALL'
   Select @Product_Place = ''



SELECT Distinct aspnet_Users.UserName
  into #TempUsers
  FROM aspnet_Roles 
 INNER JOIN aspnet_UsersInRoles ON aspnet_Roles.RoleId = aspnet_UsersInRoles.RoleId 
 INNER JOIN aspnet_Users ON aspnet_UsersInRoles.UserId = aspnet_Users.UserId
 Inner join #TempRoles T on T.value = aspnet_Roles.RoleName
--where UserName in ('DCARELL','sandra clause')                      

Declare @ProductPlaceQID numeric(18)
Declare @ProductPlaceOptionID numeric(18)
Declare @GradeQID numeric(18)
Select @ProductPlaceQID = QuestionID from Question where Name = 'Product Place'
Select @GradeQID = QuestionID from Question where Name = 'Grade'
Select @ProductPlaceOptionID = [OptionID] from [Option] where OptionText = @Product_Place and QuestionID = @ProductPlaceQID
Select @ProductPlaceOptionID = isnull(@ProductPlaceOptionID, -1)
if LEN(@Product_Place) > 0 and @ProductPlaceOptionID = -1
   Select @ProductPlaceOptionID = -150 


if @PrintIt = 1
   print Convert(varchar(30),getdate(), 121)	 + ': Get the Devices:'
Select R.ReceiveDetailID,R.ProjectID, Manufacturer, model, CONVERT(nvarchar(50), '') as Product_Place, CONVERT(nvarchar(50), Grade) as Grade, CONVERT(numeric(18), 1) as LineCount
, CONVERT(numeric(18), 1) as TotalCount, I.OptionID as PPOptionID
into #Temp001
from ReceiveDetail R
inner join ReceiveDetailItem I on I.ReceiveDetailID = R.ReceiveDetailID
-- inner join [Option] O on O.OptionID = I.OptionID
 where R.Version = '000' AND (R.ProjectID = @ProjectID or @ProjectID = -1)
                         and (I.OptionID = @ProductPlaceOptionID or @ProductPlaceOptionID = -1)


if @PrintIt = 1
   print Convert(varchar(30),getdate(), 121)	 + ': Create Temp Index: ReceiveDetail'
Create Index Temp001 on #Temp001(ReceiveDetailID)
if @PrintIt = 1
   print Convert(varchar(30),getdate(), 121)	 + ': Create Temp Index: ReceiveDetail'
Create Index Temp002 on #Temp001(ProjectID, Product_Place, Manufacturer, Model, Grade)
--Print 'Product Place OptionID:' + convert(nvarchar(10),@ProductPlaceOptionID)
--Select * from #Temp001


--print Convert(varchar(30),getdate(), 121)	 + ': Update the Grade:'
--Update #Temp001 set Grade = O.OptionText
--From #Temp001 D
--Inner join ReceiveDetailItem I on D.ReceiveDetailID = I.ReceiveDetailID
--Inner join [Option] O on I.OptionID = O.OptionID
--where O.QuestionID = @GradeQID


if @PrintIt = 1
   print Convert(varchar(30),getdate(), 121)	 + ': Update the Product Place:'
Update #Temp001 set Product_Place = O.OptionText
From #Temp001 D
Inner join [Option] O on D.PPOptionID = O.OptionID
where O.QuestionID = @ProductPlaceQID



--Select * from #Temp001
--Order by Manufacturer, Model, Product_Place, Grade

if @PrintIt = 1
   print Convert(varchar(30),getdate(), 121)	 + ': Summarize the devices by project, product place, manufactuer, model and grade:'
Select ProjectID, CONVERT(nvarchar(20), '100') as line, Product_Place,Manufacturer, Model,  Grade, COUNT(*) as Frequency              --, SUM(LineCount) as LineCount, SUM(TotalCount) as TotalCount
into #Temp002
from #Temp001
Group by ProjectID, Product_Place, Manufacturer, Model, Grade
--With ROLLUP

/*
Update #Temp002 set line = Case when isnull(Manufacturer,'')  = '' and  isnull(Model,'') = '' and  isnull(Product_Place,'') = '' and  isnull(Grade,'') = '' then '200'
                                when isnull(Manufacturer,'') != '' and  isnull(Model,'') = '' and  isnull(Product_Place,'') = '' and  isnull(Grade,'') = '' then '300'
                                when isnull(Manufacturer,'') != '' and  isnull(Model,'') != '' and  isnull(Product_Place,'') != '' and  isnull(Grade,'') = '' then '400'
                                when isnull(Manufacturer,'') != '' and  isnull(Model,'') != '' and  isnull(Product_Place,'') != '' and  isnull(Grade,'') != '' then '500'
                                else line end
*/                                
                                
/*                                
Update #Temp002 set Product_Place = Case when line = '200' then ' Grand Total' else Product_Place end
                    ,Manufacturer =  Case when line = '300' then ' Total' else Manufacturer end
                    ,Model =  Case when line = '400' then ' Total' else Model end
                    ,Grade =  Case when line = '500' then ' Total' else Grade end
*/                                                            

/*
Select Line, Product_Place, Manufacturer, Model, Grade, Frequency from #Temp002
Order by Product_Place, Manufacturer, line desc, Model, Grade
*/

if @PrintIt = 1
   print Convert(varchar(30),getdate(), 121)	 + ': Start the Pivot Table:'
SELECT *, CONVERT(int, 0) as Total
into #Tempxxx
FROM (
    SELECT 
        ProjectID, Product_Place, Manufacturer, Model, Grade, Frequency
    FROM #Temp002
) as s
PIVOT
(
    SUM(Frequency)
    FOR Grade IN ([Not Graded], [Open Package], [NEW], [A],[B], [C])
    --FOR Grade IN ( [A],[B], [C], [Not Graded], [NEW], [CPO - Certified Pre-Owned],[A+])
)AS pvt


if @PrintIt = 1
   print Convert(varchar(30),getdate(), 121)	 + ': Doing some summary math cleanup, isnull and total=:'
Update #Tempxxx set [Not Graded] = ISNULL([Not Graded], 0)
                   ,[Open Package] = ISNULL([Open Package], 0)
                   ,[New] = ISNULL([New], 0)
                   ,[A] = ISNULL([A], 0)
                   ,[B] = ISNULL([B], 0)
                   ,[C] = ISNULL([C], 0)
                   

Update #Tempxxx set Total = ISNULL([Not Graded], 0) + ISNULL([Open Package], 0) + ISNULL([New], 0) + ISNULL([A], 0) + ISNULL([B], 0) + ISNULL([C], 0)                   
                   

if @PrintIt = 1
   print Convert(varchar(30),getdate(), 121)	 + ': Start the Rollup to output:'
Select ProjectID, Product_Place, Manufacturer, Model, sum([Not Graded]) as [Not Graded], sum([Open Package]) as [Open Package], sum([A]) as [A], sum([B]) as [B], sum([C]) as [C], SUM(Total) as Total
--Into DashboardInventoryQTY_01_Grid
from #Tempxxx
Group by ProjectID, Product_Place, Manufacturer, Model
With ROLLUP


--Drop table DashboardInventoryQTY_01_Grid

Drop Table #Tempxxx
Drop Table #Temp001
Drop Table #Temp002

if @PrintIt = 1
   print Convert(varchar(30),getdate(), 121)	 + ': Done:'

END



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


/****** Object:  StoredProcedure [dbo].[GetDashboardRepair_01_GridValueFiltered]    Script Date: 10/16/2019 11:52:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*

Receiver
Supervisors


exec GetDashboardRepair_01_GridValueFiltered 'MM/DD/YYYY',12, ''
exec GetDashboardRepair_01_GridValueFiltered '',16000, ''
exec GetDashboardRepair_01_GridValueFiltered '10/06/2016',1200, 'Receiver,Supervisors'
exec GetDashboardRepair_01_GridValue '10/06/2016',1200
exec GetDashboardRepair_01_GridValueFiltered '9/16/2016',12, ''
exec GetDashboardRepair_01_GridValueFiltered '',12, ''
exec GetDashboardRepair_01_GridValueFiltered '',12, ''






CREATE TABLE #Temp1(
	[Today] [nvarchar](10) NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[Createdate] [nvarchar](10) NULL,
	[daysago] [int] NULL,
	[Bridge Repair] [int] NULL,
	[MSC Repair Handling] [int] NULL,
	[Product Placement] [int] NULL,
	[Total] [numeric](10, 0) NULL
)

Insert into #Temp1
exec GetDashboardRepair_01 '10/06/2016',12

Select * from #Temp1


*/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GetDashboardRepair_01_GridValueFiltered]
    @Today nvarchar(10),
    @Days int,
    @RoleFilter nvarchar(max)
AS
BEGIN

Set nocount on

Declare @delimiter varchar(20)
Select @delimiter = ','
SELECT convert(numeric(18),0) as Processed, * into #TempRoles FROM fn_SplitDistinct(@RoleFilter, @delimiter)
Update #TempRoles set value = LTRIM(rtrim(Value))
--Select * from #TempRoles
--Select * from fn_SplitDistinct('aaa, aaab,ddddddd', ',')

SELECT Distinct aspnet_Users.UserName
  into #TempUsers
  FROM aspnet_Roles 
 INNER JOIN aspnet_UsersInRoles ON aspnet_Roles.RoleId = aspnet_UsersInRoles.RoleId 
 INNER JOIN aspnet_Users ON aspnet_UsersInRoles.UserId = aspnet_Users.UserId
 Inner join #TempRoles T on T.value = aspnet_Roles.RoleName
--where UserName in ('DCARELL','sandra clause')                      


/*   Get the raw data we will reformat  */
CREATE TABLE #Temp1(
	[Today] [nvarchar](10) NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[Createdate] [nvarchar](10) NULL,
	[daysago] [int] NULL,
	[Bridge Repair] [int] NULL,
	[MSC Repair Handling] [int] NULL,
	[Product Placement] [int] NULL,
	[MSC Return] [int] NULL,
	[Total] [numeric](10, 0) NULL,
	[Processed] [int] NULL
)
Insert into #Temp1
exec GetDashboardRepair_02 @Today,@Days
Update #Temp1 set [Processed] = 0
----------------------------------------------


  --and (ProcessText = 'Bridge Repair'
  --or ProcessText = 'MSC Repair Handling'
  --or ProcessText = 'ProductPlacement')



/*   Setup for reformating the data  */
CREATE TABLE #TempGridGT(
	[CreateUser] [nvarchar](50) NOT NULL,
	[C0_A]  [numeric](10, 0) NULL,
	[C0_B]  [numeric](10, 0) NULL,
	[C0_C]  [numeric](10, 0) NULL,
	[C0_D]  [numeric](10, 0) NULL,
	[C0_T]  [numeric](10, 0) NULL,
	
	[C1_A] [numeric](10, 0) NULL,
	[C1_B] [numeric](10, 0) NULL,
	[C1_C] [numeric](10, 0) NULL,
	[C1_D]  [numeric](10, 0) NULL,
	[C1_T] [numeric](10, 0) NULL,
	
    [C2_A] [numeric](10, 0) NULL,
	[C2_B] [numeric](10, 0) NULL,
	[C2_C] [numeric](10, 0) NULL,
	[C2_D]  [numeric](10, 0) NULL,
	[C2_T] [numeric](10, 0) NULL,
	[C_GT] [numeric](10, 0) NULL
)
CREATE TABLE #TempGrid(
	[CreateUser] [nvarchar](50) NOT NULL,
    [Seq] [int] NULL,
	[Row] [nvarchar](5) NULL,	
	
	[C0_Date] [nvarchar](10) NULL,
	[C0_A] [numeric](10, 0) NULL,
	[C0_B] [numeric](10, 0) NULL,
	[C0_C] [numeric](10, 0) NULL,
	[C0_D]  [numeric](10, 0) NULL,
	[C0_T] [numeric](10, 0) NULL,
	
	[C1_Date] [nvarchar](10) NULL,
	[C1_A] [numeric](10, 0) NULL,
	[C1_B] [numeric](10, 0) NULL,
	[C1_C] [numeric](10, 0) NULL,
	[C1_D]  [numeric](10, 0) NULL,
	[C1_T] [numeric](10, 0) NULL,
	
	[C2_Date] [nvarchar](10) NULL,
    [C2_A] [numeric](10, 0) NULL,
	[C2_B] [numeric](10, 0) NULL,
	[C2_C] [numeric](10, 0) NULL,
	[C2_D]  [numeric](10, 0) NULL,
	[C2_T] [numeric](10, 0) NULL,
	[C_GT] [numeric](10, 0) NULL
)

-- Lay down the first layer.
/*
Insert #TempGrid ([Row], [CreateUser],[C0_A],[C0_B],[C0_C],[C0_T]
                              ,[C1_A],[C1_B],[C1_C],[C1_T]
                              ,[C2_A],[C2_B],[C2_C],[C2_T] 
                              ,[C_GT], Seq)
Values ('h0','','date','','',''        
               ,'date','','',''        
               ,'date','','',''          
               ,'', 1)        
Insert #TempGrid ([Row],[CreateUser],[C0_A],[C0_B],[C0_C],[C0_T]
                              ,[C1_A],[C1_B],[C1_C],[C1_T]
                              ,[C2_A],[C2_B],[C2_C],[C2_T] 
                              ,[C_GT], Seq)
Values ('h1','Rep','Bridge Repair','MSC Repair handline','Product Placement','total'   
,'Bridge Repair','MSC Repair handline','Product Placement','total'   
,'Bridge Repair','MSC Repair handline','Product Placement','total'     
,'grand total', 2)   
Insert #TempGridGT ([CreateUser],[C0_A],[C0_B],[C0_C],[C0_T]
                    ,[C1_A],[C1_B],[C1_C],[C1_T]
                    ,[C2_A],[C2_B],[C2_C],[C2_T] 
                    ,[C_GT])
Values ('Grand Total',0,0,0,0        
                   ,0,0,0,0
                   ,0,0,0,0       
                   ,0)  
*/

-- Our data is telling us the number of days since the "today" date.
--     depending on the work schedule, some days may not be present.
--     Our final grid carries three days of column data.
--     We would rather "squish out" those absend days.
--     This does it.
--     The Days since original number is lost.


Create Table #Renum(
   [NewNumb] [int] IDENTITY(1,1) NOT NULL,
   [daysago] [int])
Insert #Renum (daysago)
Select distinct daysago from #Temp1 order by daysago
Update #Temp1 set daysago = R.NewNumb from #Temp1 T inner join #Renum R on T.daysago = R.daysago
       -- Because we only want to show the users with transactions in the last 3 cycles.
       Delete #Temp1 where daysago > 3
Drop Table #Renum
---------------------------------------------------------------------------------

Declare @CreateUser [nvarchar](50),
	    @Createdate [nvarchar](10),
	    @daysago [int],
	    @BridgeRepair [int],
	    @MSCRepairHandling [int],
	    @ProductPlacement [int],
	    @MSCReturns [int],
	    @Total Numeric(10,0),
	    @C int
	    
Select @C = 0		    

while exists (Select * from #Temp1 where [Processed] = 0 and @C < 10000000)
      begin
      Select @C = @C + 1
      Select Top 1 @CreateUser = Createuser,
                   @Createdate = Createdate,
                   @daysago = daysago,
                   @BridgeRepair = [Bridge Repair],
                   @MSCRepairHandling = [MSC Repair Handling],
                   @ProductPlacement = [Product Placement],
                   @MSCReturns = [MSC Return],
                   @Total = [Total]
            from #Temp1 where Processed = 0
      update #Temp1 set processed = 1 where CreateUser = @Createuser and Createdate = @Createdate 
      
      Select @Total = ISNULL(@BridgeRepair, 0) + ISNULL(@MSCRepairHandling, 0) +  ISNULL(@ProductPlacement, 0) +  ISNULL(@MSCReturns, 0)
                   
      -- Add the record if not found already in Grid Section             
      if not exists(Select * from #TempGrid where CreateUser = @CreateUser)
         begin
         Insert #TempGrid ([Row], [CreateUser],[C0_A],[C0_B],[C0_C],[C0_D],[C0_T]
                             ,[C1_A],[C1_B],[C1_C],[C1_D],[C1_T]
                             ,[C2_A],[C2_B],[C2_C],[C2_D],[C2_T] 
                             ,[C_GT], Seq)
         Values ('d0',@CreateUser,0,0,0,0,0       
                                 ,0,0,0,0,0      
                                 ,0,0,0,0,0          
                                 ,0,3)      
         Insert #TempGridGT ([CreateUser],[C0_A],[C0_B],[C0_C],[C0_D],[C0_T]
                             ,[C1_A],[C1_B],[C1_C],[C1_D],[C1_T]
                             ,[C2_A],[C2_B],[C2_C],[C2_D],[C2_T] 
                             ,[C_GT])
         Values (@CreateUser,0,0,0,0,0           
                            ,0,0,0,0,0   
                            ,0,0,0,0,0     
                            ,0)                                  
         end
      
      if @daysago = 1
         begin
         --Update #TempGrid set C0_A = @Createdate where Row = 'h0'
         Update #TempGrid set C0_A = isnull(@BridgeRepair, 0)
                             ,C0_B = isnull(@MSCRepairHandling, 0)
                             ,C0_C = isnull(@ProductPlacement, 0)
                             ,C0_D = isnull(@MSCReturns, 0)
                             ,C0_T = isnull(@Total, 0)
                             ,C0_Date = @Createdate
                              where [Row] = 'd0' and CreateUser = @CreateUser
         Update #TempGridGT set C0_A = C0_A + isnull(@BridgeRepair, 0)
                             ,C0_B = C0_B + isnull(@MSCRepairHandling, 0)
                             ,C0_C = C0_C + isnull(@ProductPlacement, 0)
                             ,C0_D = C0_D + isnull(@MSCReturns, 0)
                             ,C0_T = C0_T + isnull(@Total, 0)
                             ,C_GT = C_GT + isnull(@Total, 0)  where CreateUser = @CreateUser
         Update #TempGridGT set C0_A = C0_A + isnull(@BridgeRepair, 0)
                             ,C0_B = C0_B + isnull(@MSCRepairHandling, 0)
                             ,C0_C = C0_C + isnull(@ProductPlacement, 0)
                             ,C0_D = C0_D + isnull(@MSCReturns, 0)
                             ,C0_T = C0_T + isnull(@Total, 0)
                             ,C_GT = C_GT + isnull(@Total, 0)  where CreateUser = 'Grand Total'                            
         end
      if @daysago = 2
         begin
         --Update #TempGrid set C1_A = @Createdate where Row = 'h0'
         Update #TempGrid set C1_A = case when isnull(@BridgeRepair, 0) = 0 then 0 else @BridgeRepair end
                             ,C1_B = case when isnull(@MSCRepairHandling, 0) = 0 then 0 else @MSCRepairHandling end
                             ,C1_C = case when isnull(@ProductPlacement, 0) = 0 then 0 else @ProductPlacement end
                             ,C1_D = case when isnull(@MSCReturns, 0) = 0 then 0 else @MSCReturns end
                             ,C1_T = case when isnull(@Total, 0) = 0 then 0 else @Total end 
                             ,C1_Date = @Createdate
                             where [Row] = 'd0' and CreateUser = @CreateUser
         Update #TempGridGT set C1_A = C1_A + isnull(@BridgeRepair, 0)
                             ,C1_B = C1_B + isnull(@MSCRepairHandling, 0)
                             ,C1_C = C1_C + isnull(@ProductPlacement, 0)
                             ,C1_D = C1_D + isnull(@MSCReturns, 0)
                             ,C0_T = C0_A + isnull(@Total, 0)
                             ,C_GT = C_GT + isnull(@Total, 0)  where CreateUser = @CreateUser
         Update #TempGridGT set C1_A = C1_A + isnull(@BridgeRepair, 0)
                             ,C1_B = C1_B + isnull(@MSCRepairHandling, 0)
                             ,C1_C = C1_C + isnull(@ProductPlacement, 0)
                             ,C1_D = C1_D + isnull(@MSCReturns, 0)
                             ,C1_T = C1_T + isnull(@Total, 0)
                             ,C_GT = C_GT + isnull(@Total, 0)  where CreateUser = 'Grand Total'   
         end
      if @daysago = 3
         begin
         --Update #TempGrid set C2_A = @Createdate where Row = 'h0'
         Update #TempGrid set C2_A = case when isnull(@BridgeRepair, 0) = 0 then 0 else @BridgeRepair end
                             ,C2_B = case when isnull(@MSCRepairHandling, 0) = 0 then 0 else @MSCRepairHandling end
                             ,C2_C = case when isnull(@ProductPlacement, 0) = 0 then 0 else @ProductPlacement end
                             ,C2_D = case when isnull(@MSCReturns, 0) = 0 then 0 else @MSCReturns end
                             ,C2_T = case when isnull(@Total, 0) = 0 then '0' else @Total end 
                             ,C2_Date = @Createdate
                             where [Row] = 'd0' and CreateUser = @CreateUser
         Update #TempGridGT set C2_A = C2_A + isnull(@BridgeRepair, 0)
                             ,C2_B = C2_B + isnull(@MSCRepairHandling, 0)
                             ,C2_C = C2_C + isnull(@ProductPlacement, 0)
                             ,C2_D = C2_D + isnull(@MSCReturns, 0)
                             ,C2_T = C2_T + isnull(@Total, 0)
                             ,C_GT = C_GT + isnull(@Total, 0)  where CreateUser = @CreateUser
         Update #TempGridGT set C2_A = C2_A + isnull(@BridgeRepair, 0)
                             ,C2_B = C2_B + isnull(@MSCRepairHandling, 0)
                             ,C2_C = C2_C + isnull(@ProductPlacement, 0)
                             ,C2_D = C2_D + isnull(@MSCReturns, 0)
                             ,C2_T = C2_T + isnull(@Total, 0)
                             ,C_GT = C_GT + isnull(@Total, 0)  where CreateUser = 'Grand Total'   
         end
      end
      /*  Now we need to add our grand total */
      Insert #TempGrid ([Row], [CreateUser],[C0_A],[C0_B],[C0_C],[C0_D],[C0_T]
                       ,[C1_A],[C1_B],[C1_C],[C1_D],[C1_T]
                       ,[C2_A],[C2_B],[C2_C],[C2_D],[C2_T] 
                       ,[C_GT], Seq)
      Select 't0', [CreateUser],[C0_A],[C0_B],[C0_C],[C0_D],[C0_T]
                       ,[C1_A],[C1_B],[C1_C],[C1_D],[C1_T]
                       ,[C2_A],[C2_B],[C2_C],[C2_D],[C2_T] 
                       ,[C_GT], 5
          From #TempGridGT where CreateUser = 'Grand Total'  
      -- We need to bring over our User Grand Totals.    
      update #TempGrid set C_GT = CONVERT(nvarchar(50), B.C_GT)
      From #TempGrid A
      Inner join #TempGridGT B on A.CreateUser = B.CreateUser
      ----------------------------------------------------------------

if exists(Select * from #TempUsers)
   begin 
   Select * from #TempGrid T
    inner join #TempUsers U on U.UserName = T.CreateUser COLLATE Database_Default 
    Order by Seq, CreateUser
   end
   
if not exists(Select * from #TempUsers)
   begin 
   Select * from #TempGrid T
    Order by Seq, CreateUser
   end  

Drop table #TempUsers
Drop table #TempRoles
Drop table #Temp1
Drop table #TempGrid


END

/****** Object:  StoredProcedure [dbo].[GetDashboardRepair_02]    Script Date: 10/16/2019 11:40:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*

exec GetDashboardRepair_01 'MM/DD/YYYY',12
exec GetDashboardRepair_02 '10/06/2016',12
exec GetDashboardRepair_01 '',12
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
Create PROCEDURE [dbo].[GetDashboardRepair_02]
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
  and (ProcessText = 'Bridge Repair'
  or ProcessText = 'MSC Repair Handling'
  or ProcessText = 'MSC Return'
  or ProcessText = 'ProductPlacement')
order by l.CreateDate Desc

SELECT Today, CreateUser, Createdate, workdaysdiff as daysago, [Bridge Repair], [MSC Repair Handling], [ProductPlacement], [MSC Return], convert(numeric(10),0) as Total, CONVERT(int, 0) as processed from  
             (
                select Today, CreateUser, Createdate, workdaysdiff, [ProcessText], [frequency]
                from #Temp1a
            ) x
            pivot 
            (
                sum([frequency])
                for ProcessText in ([Bridge Repair],[MSC Repair Handling],[ProductPlacement], [MSC Return])
            ) p order by Createuser, Createdate Desc




Drop Table #Temp1a

END


/****** Object:  StoredProcedure [dbo].[GetMasterDetailCellbieList_TemplateRawData_SearchGrid_03]    Script Date: 10/16/2019 11:48:20 ******/
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

Exec GetMasterDetailCellbieList_TemplateRawData_SearchGrid_03 'Error'
Exec GetMasterDetailCellbieList_TemplateRawData_SearchGrid_03 'Send'
Exec GetMasterDetailCellbieList_TemplateRawData_SearchGrid_03 'Success'

 Drop Procedure GetMasterDetailCellbieList_TemplateRawData_SearchGrid_03

*/

Create PROCEDURE [dbo].[GetMasterDetailCellbieList_TemplateRawData_SearchGrid_03]

      @mStatus nvarchar(50) = ''

AS
BEGIN
	SET NOCOUNT ON;
	
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED


-- Print @mIFSLocation

	
--Select @mShowGraveyard = case when @mShowGraveyard = 'Y' then 'Y' else 'N' end
	
--print 'Start:' + Convert(varchar(30),getdate(), 121)	
--      WAITFOR DELAY '00:00:30'
-- print 'Wait:' + Convert(varchar(20),getdate(), 120)	

Declare @mSaveID numeric(15)
Select Top 1 @mSaveID = ProcessID from Process where process.name = 'Save'
	
----if @mProjectName = 'All'
----   Select @mProjectName = ''
   		
--Declare @mClientLocationID numeric(18)
--Select 	@mClientLocationID = -1
--if len(ltrim(rtrim(@mClientCode))) > 0
--   Select @mClientLocationID = ClientLocationID from ClientLocation where scankey = @mClientCode

--Select @mClientLocationID = isnull(@mClientLocationID,-1)   
--if len(rtrim(ltrim(@mClientCode))) > 0 and @mClientLocationID = -1
--   select @mClientLocationID = -2

--Declare @dReceiveBeginDate Datetime
--Declare @dReceiveEndDate Datetime
--Select @dReceiveBeginDate = convert(datetime,@mReceiveBeginDate,101)
--Select @dReceiveEndDate = convert(datetime,@mReceiveEndDate,101)
--Select @dReceiveEndDate = dateadd(d,1,@dReceiveEndDate)

--Declare @mGraveYardStatusID numeric(18)
--Select @mGraveYardStatusID = ReceiveDetailStatusID FROM  ReceiveDetailStatus where Status = 'GraveYard'

--Declare @mProjectID numeric(18)
--Select @mProjectID = ProjectID from Project where Name = @mProjectName
--Select @mProjectID = isnull(@mProjectID, -1)
--if len(rtrim(ltrim(@mProjectName))) > 0 and @mProjectID = -1
--   select @mProjectID = -2
   
CREATE TABLE #xTemp(
	[source] [varchar](6) COLLATE Latin1_General_CI_AS NULL,
    [ReceiveHeaderID] [numeric] (18,0),
    [ReceiveDetailBulkID] [numeric] (18,0),
    [ReceiveDetailID] [numeric] (18,0),
    [ClientLocationID] [numeric] (18,0),
    [ProjectID] [numeric] (18,0),
	[ProjectName] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[Name] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[StoreNumber] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL,
	[StoreSuffix] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL,
	[Sequence] [int] NULL,
	[CompanyName] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[Process] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL,
	[Status] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL,
	[QTYPaper] [numeric](18, 0) NULL,
	[QTYRecorded] [numeric](18, 0) NULL,
	[QTYIntegrated] [numeric](18, 0) NULL,
	[ReceiveDate] [datetime] NOT NULL,
	[ReceiveDate_Date] nvarchar(10) NULL,
	[ReceiveDate_Time] nvarchar(10) NULL,	
	
	[WayBill] [nvarchar](500) COLLATE Latin1_General_CI_AS NULL,
	[ShipDate] [datetime] NULL,	
	[RMANumber] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[ProjectTag] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[MakeModelString] [nvarchar](200) COLLATE Latin1_General_CI_AS NULL,
	[ESN] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[Version] [nchar](3) COLLATE Latin1_General_CI_AS NULL,		
	[SwappedESN] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
--	[PIN] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[Date_QC] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL,

	[ML_Condition] [nvarchar](20) NULL,	
	[ML_NickName] [nvarchar](20) NULL,	
	[ML_Description] [nvarchar](200) NULL,
	[ML_UPC] [nvarchar](20) NULL,
	[ML_SKU] [nvarchar](20) NULL,
	[ML_SKU_B] [nvarchar](20) NULL,
	[ML_SKU_C] [nvarchar](20) NULL,		
	[ML_SKU_Loaner] [nvarchar](20) NULL,	
	[ML_WarrantyStickerPlacement] [nvarchar](200) NULL,
	[ML_Device_Handset] [nvarchar](50) NULL,
	[ML_Bar_Flip] [nvarchar](20) NULL,
	[ML_CDMA_HSPA] [nvarchar](50) NULL,
	[ML_Retire] [nvarchar](20) NULL	,
	[LastUpdateDate] [datetime] Not NULL,
	[LastUpdateUser] [nvarchar](50) Not NULL
	
	
	
	
	
	)

Create Index Temp_01 on #xTemp (ReceiveDetailID)
Create Index Temp_02 on #xTemp (ProjectID)



CREATE TABLE #TempZX(
	[ReceiveDetailID] [numeric](18, 0) NULL,
	[CurrentLogID] [numeric](18, 0) NULL,
	[StartLogID] [numeric](18, 0) NULL,
    )
    
Create Index Temp_10 on #Tempzx([ReceiveDetailID])    
Create Index Temp_11 on #Tempzx([CurrentLogID])
Create Index Temp_12 on #Tempzx([StartLogID])


CREATE TABLE #TempRD(
	[CellbieStatus] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL,
	[MiscText] [nvarchar](500) COLLATE Latin1_General_CI_AS NULL,
	[LastUpdateDate_Cellbie] [datetime] NOT NULL,
	
	
	[SendParamAgree] nVarchar(20) NULL,
	[SendParamMessage] nVarchar(200) NULL,
	[SendReturnMessage] nVarchar(200) NULL,
	

	[ReceiveDetailID] [numeric](18, 0) NULL,
	[ProcessID] [numeric](18, 0) NULL,
	[CurrentProcessID] [numeric](18, 0) NULL,
	[StartProcessID] [numeric](18, 0) NULL,
	[CurrentProcessName] nVarchar(20) NULL,
	[StartProcessName] nVarchar(20) NULL,
	[ESN] nvarchar(50),
	[Status] nvarchar(20),
	[Process] numeric(15) NULL,
	[SKU] nvarchar(50) NULL,
	[IFSLocation] nvarchar(20) NULL,
	[IFSCondition] nvarchar(50) NULL,
	[BIN] nvarchar(20) NULL,	
	[LastUpdateDate] [datetime] NULL,
	[LastUpdateUser] [nvarchar](50) NULL
    )

Create Index Temp_03 on #TempRD (ReceiveDetailID)
Create Index Temp_04 on #TempRD (ProcessID)

CREATE TABLE #TempBin(
	[ReceiveDetailID] [numeric](18, 0) NULL
    )

Create Index Temp_25 on #TempBin (ReceiveDetailID)
	

 Insert #TempRD (CellbieStatus, MiscText, LastUpdateDate_Cellbie,
        ReceiveDetailID, ProcessID,CurrentProcessID,StartProcessID,ESN,Status, Process, SKU, IFSLocation, IFSCondition,[SendParamAgree],[SendParamMessage],[SendReturnMessage])
 Select ReceiveDetail.Status, MiscText, LastUpdateDate_Cellbie,
        ReceiveDetail.ReceiveDetailID, ProcessID,@mSaveID,@mSaveID,ReceiveDetail.ESN,ReceiveDetailStatus.Status,0, ReceiveDetail.SKU, ReceiveDetail.IFSLocation, ReceiveDetail.IFSCondition,'','',''
   From vwReceiveDetailCellbie as ReceiveDetail
  INNER JOIN ReceiveDetailStatus ON ReceiveDetail.StatusID = ReceiveDetailStatus.ReceiveDetailStatusID    
  --INNER JOIN OrderDetailReceiveDetail ON ReceiveDetail.ReceiveDetailID = OrderDetailReceiveDetail.ReceiveDetailID 
  --INNER JOIN OrderDetail ON OrderDetailReceiveDetail.OrderDetailID = OrderDetail.OrderDetailID 
  --INNER JOIN OrderHeader ON OrderDetail.OrderHeaderID = OrderHeader.OrderHeaderID
  WHERE     (ReceiveDetail.Status=@mStatus)
      
--Select * from #TempRD
--return  
-- We want to get the start and Current process (Min/Max).

Insert #TempZX
Select #TempRD.ReceiveDetailID, Max(ReceiveDetailProcessLog.ReceiveDetailProcessLogID), Min(ReceiveDetailProcessLog.ReceiveDetailProcessLogID)
  From #TempRD
 Inner join ReceiveDetailProcessLog on ReceiveDetailProcessLog.ReceiveDetailID = #TempRD.ReceiveDetailID
 Inner Join Process on Process.ProcessID = ReceiveDetailProcessLog.ProcessID
 Where process.name != 'Save'
 Group By #TempRD.ReceiveDetailID
 
Update #TempRD set [StartProcessID] = Process.ProcessID, StartProcessName = Process.Name
  From #TempRD
 Inner join #TempZX on #TempZX.ReceiveDetailID = #TempRD.ReceiveDetailID
 Inner join ReceiveDetailProcessLog on ReceiveDetailProcessLog.ReceiveDetailProcessLogID = #TempZX.[StartLogID]
 Inner Join Process on Process.ProcessID = ReceiveDetailProcessLog.ProcessID

Update #TempRD set CurrentProcessID = Process.ProcessID, CurrentProcessName = Process.Name, LastUpdateDate = ReceiveDetailProcessLog.CreateDate, LastUpdateUser = ReceiveDetailProcessLog.CreateUser
  From #TempRD
 Inner join #TempZX on #TempZX.ReceiveDetailID = #TempRD.ReceiveDetailID
 Inner join ReceiveDetailProcessLog on ReceiveDetailProcessLog.ReceiveDetailProcessLogID = #TempZX.[CurrentLogID]
 Inner Join Process on Process.ProcessID = ReceiveDetailProcessLog.ProcessID

Delete #TempRD where LastUpdateDate is  null

--Select * from #TempRD
--return 
----------------------------------------------------------
 
insert #xTemp
	SELECT 'Detail' as source, 
			ReceiveDetail.ReceiveHeaderID, 
			ReceiveDetail.ReceiveDetailBulkID, 
			ReceiveDetail.ReceiveDetailID,
			ClientLocation.ClientLocationID,
			ReceiveDetail.ProjectID, 	
			Project.Name,		
			ClientLocation.Name, 
			ClientLocation.StoreNumber, 
			ClientLocation.StoreSuffix, 
			ClientLocation.Sequence, 
			ClientLocation.CompanyName, 
			convert(varchar(20),'') as Process,
			ReceiveDetailStatus.Status,
			convert(numeric(18),0) as QTYPaper, 
			ReceiveDetail.QTYIntegrated as QTYRecorded, 
			ReceiveDetail.QTYIntegrated, 
			ReceiveDetail.CreateDate, 
			convert(nvarchar(10),ReceiveDetail.CreateDate,111),
			convert(nvarchar(10),ReceiveDetail.CreateDate,108), 

			'',
			Null,
			ReceiveDetail.RMANumber, 
			ReceiveDetail.ProjectTag, 
			dbo.GetCarrierMakeModelColourAnswerString(#TempRD.ReceiveDetailID),
			ReceiveDetail.ESN, ReceiveDetail.Version,'','',            --  ReceiveDetail.ICB, ReceiveDetail.PIN,
			'','','','','','','','','','','','','',
			#TempRD.LastUpdateDate,
			#TempRD.LastUpdateUser

	   FROM #TempRD 
	  INNER JOIN ReceiveDetail ON #TempRD.ReceiveDetailID = ReceiveDetail.ReceiveDetailID 
      INNER JOIN ReceiveDetailStatus ON ReceiveDetail.StatusID = ReceiveDetailStatus.ReceiveDetailStatusID	  
	  INNER JOIN ClientLocation ON ReceiveDetail.ClientLocationID = ClientLocation.ClientLocationID
	  INNER JOIN Project on ReceiveDetail.ProjectId = Project.ProjectID
	  order by ClientLocation.Name, Project.Name, RMANumber, ProjectTag, Process, ESN

--Select * from #xTemp 
--return 


Update #xTemp set [Date_QC] = dbo.GetReceivedQuestionAnswerString_03(#xTemp.ReceiveDetailID, 'Date QC')
  from #xTemp 


UPdate #xTemp set WayBill = OrderHeader.WaybillNumber, ShipDate = OrderHeader.Shippeddate
  From #xTemp
 INNER JOIN OrderDetailReceiveDetail ON #xTemp.ReceiveDetailID = OrderDetailReceiveDetail.ReceiveDetailID 
 INNER JOIN OrderDetail ON OrderDetailReceiveDetail.OrderDetailID = OrderDetail.OrderDetailID 
 INNER JOIN OrderHeader ON OrderDetail.OrderHeaderID = OrderHeader.OrderHeaderID 
 
 
 UPdate #xTemp set WayBill = 'Process Shipped', ShipDate = 
( SELECT TOP (1) ReceiveDetailProcessLog.CreateDate
    FROM ReceiveDetailProcessLog 
   INNER JOIN Process ON ReceiveDetailProcessLog.ProcessID = Process.ProcessID
   WHERE (Process.Name = 'Shipping') AND (ReceiveDetailProcessLog.ReceiveDetailID = #xTemp.ReceiveDetailID))
Where ShipDate is null   


----------------------------------------------------------------

   
Update #xTemp set SwappedESN = 
 (Select Top 1 IMEISwappedOut 
    From #xTemp z
   Inner join ReceiveDetailIMEISwappedLog on z.ReceiveDetailID = ReceiveDetailIMEISwappedLog.ReceiveDetailID
   where z.ReceiveDetailID = #xTemp.ReceiveDetailID order by CreateDate Desc);   



   
   
Select #TempRD.CellbieStatus,
       #TempRD.MiscText,
       #TempRD.LastUpdateDate_Cellbie,
       #TempRD.[SendParamAgree],#TempRD.[SendParamMessage],#TempRD.[SendReturnMessage],
       #xTemp.source,
       #xTemp.ReceiveHeaderID,
       #xTemp.ReceiveDetailBulkID,
       #xTemp.ReceiveDetailID,
       #xTemp.ClientLocationID,
       #xTemp.ProjectID,
       
       #xTemp.Name,
       #xTemp.StoreNumber,
       #xTemp.StoreSuffix,
       #xTemp.Sequence,
       #xTemp.CompanyName,
       #xTemp.ProjectName,
       #xTemp.Status,
       #TempRD.StartProcessName,
       #TempRD.CurrentProcessName,

       -- #xTemp.QTYPaper,
       -- #xTemp.QTYRecorded,
       #xTemp.QTYIntegrated,
       #xTemp.MakeModelString,
       #xTemp.ESN,
       #xTemp.Version,
       #xTemp.SwappedESN,
       #xTemp.RMANumber,
       #xTemp.ProjectTag,
       #xTemp.WayBill,
       #xTemp.ReceiveDate,
       #xTemp.ReceiveDate_Date,
       #xTemp.ReceiveDate_Time,

       #xTemp.ShipDate,
       #xTemp.Date_QC,

       #xTemp.ML_SKU,
       #xTemp.ML_UPC,
       #xTemp.ML_Description,
       #xTemp.ML_WarrantyStickerPlacement,
       #xTemp.ML_Device_Handset,
       #xTemp.ML_Bar_Flip,
       #xTemp.ML_CDMA_HSPA,
       #xTemp.ML_Retire,
       #TempRD.SKU,
       #TempRD.IFSLocation,       
       --dbo.GetReceivedQuestionAnswerString_03(#xTemp.ReceiveDetailID, 'Storage Location') as IFSLocation,
       #TempRD.IFSCondition,
       #xTemp.LastUpdateDate,
       #xTemp.LastUpdateUser
       
--into vwReceiveDetailCellbie_Grid
  from #xTemp
Inner join #TempRD on #xTemp.ReceiveDetailID = #TempRD.ReceiveDetailID;





-- print 'Data Out:' + Convert(varchar(20),getdate(), 120)	
Drop Table #TempRD
--Drop Table #temp123
--Drop Table #temp123_4
Drop Table #xTemp
Drop Table #TempZX
-- Drop Table #Temp321

END

/****** Object:  StoredProcedure [dbo].[GetMasterPartNumbersThisPart]    Script Date: 10/16/2019 11:36:04 ******/
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

Exec GetMasterPartNumbersThisPart 682, -1, -1, -1, -1
Exec GetMasterPartNumbersThisPart -1, -1, -1, 1443, 6121


Select * from MasterPartsTableIFSLocationStorage


*/

Create PROCEDURE [dbo].[GetMasterPartNumbersThisPart]
	  @mMasterPartID numeric(18, 0),
	  @mClientID numeric(18, 0),
	  @mClientLocationID numeric(18, 0),
      @mManufacturer nvarchar(50),
      @mModelID numeric(18, 0)          

AS
BEGIN
	SET NOCOUNT ON;
	
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	

Declare @Done Bit
Select @Done = 0


CREATE TABLE #Temp (
	[MasterPartsLinkTableID] [numeric](18, 0)  NOT NULL,
	[MasterPartsID] [numeric](18, 0) NOT NULL,
	[PartNumber] [nvarchar](30) NOT NULL,
	[ClientID] [numeric](18, 0) NULL,
	[Carrier] [varchar](500) NULL,
	[Manufacturer] [nvarchar](50) NULL,
	[Model] [varchar](500) NULL,
	[Quantity] [numeric](18, 0) NOT NULL,
	[MonthendQTY] [numeric](18, 0) NOT NULL,
	[MonthEndDate] [datetime] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,
	[UnitPrice] [numeric](18, 2) NULL,
	[MonthEndUnitPrice] [numeric](18, 2) NULL,
	[GMPPartNumber] [nvarchar](30) NULL,
	[GMPPartDescription] [nvarchar](50) NULL,
	[QTYMin] [numeric](18, 0) NULL,
	[QTYMax] [numeric](18, 0) NULL,
	[QTYReorder] [numeric](18, 0) NULL,
	[MasterPartsClassTypeID] [numeric](18, 0) NULL,
	[ClientLocationID] [numeric](18, 0) NULL,
	[InWarrentyWorkPrice] [numeric](18, 2) NULL,
	[MonthEndInWarrentyWorkPrice] [numeric](18, 2) NULL,
	[AveragePurchasePrice] [numeric](18, 2) NULL,
	[MonthEndAveragePurchasePrice] [numeric](18, 2) NULL)






if (@mModelID < 1)
    Begin
    if (@mMasterPartID < 1)
        begin
        Print 'Inside  1'
        Insert #Temp
        Select MasterPartsLinkTable.* 
          from MasterPartsLinkTable Inner join MasterParts on MasterPartsLinkTable.MasterPartsID = MasterParts.MasterPartsID
         where (Manufacturer = @mManufacturer or Manufacturer = -1) 
           and ClientLocationID = @mClientLocationID 
         order by PartNumber, MasterParts.Description
        Select @Done = 1
        
        
        --Select * from #Temp order by PartNumber
        --return
        END
    else
        begin
        Print 'Inside  2'
        Insert #Temp
        Select MasterPartsLinkTable.* 
        from MasterPartsLinkTable Inner join MasterParts on MasterPartsLinkTable.MasterPartsID = MasterParts.MasterPartsID
        where MasterParts.MasterPartsID = @mMasterPartID 
        and (Manufacturer = @mManufacturer or Manufacturer = -1) 
        and ClientLocationID = @mClientLocationID 
        order by PartNumber, MasterParts.Description
        Select @Done = 1
        END       
    END
else
    Begin
    if (@mMasterPartID < 1)
        begin
        Print 'Inside  3'
        Insert #Temp
        Select MasterPartsLinkTable.* 
        from MasterPartsLinkTable Inner join MasterParts on MasterPartsLinkTable.MasterPartsID = MasterParts.MasterPartsID
        where (Manufacturer = @mManufacturer or Manufacturer = -1) 
          and ClientLocationID = @mClientLocationID 
          and exists(Select * from MasterPartsLinkTableModelList where MasterPartsLinkTableModelList.MasterPartsLinkTableID = MasterPartsLinkTable.MasterPartsLinkTableID 
                                                      and (MasterPartsLinkTableModelList.ModelID = @mModelID or MasterPartsLinkTableModelList.ModelID = -1))
        order by PartNumber, MasterParts.Description                   
        Select @Done = 1
        End
    else
        Begin
        Print 'Inside  4'
        Insert #Temp
        Select MasterPartsLinkTable.* 
        from MasterPartsLinkTable Inner join MasterParts on MasterPartsLinkTable.MasterPartsID = MasterParts.MasterPartsID
        where MasterParts.MasterPartsID = @mMasterPartID 
          and (Manufacturer = @mManufacturer or Manufacturer = -1)
          and ClientLocationID = @mClientLocationID 
        and exists(Select * from MasterPartsLinkTableModelList where MasterPartsLinkTableModelList.MasterPartsLinkTableID = MasterPartsLinkTable.MasterPartsLinkTableID
                                                      and (MasterPartsLinkTableModelList.ModelID = @mModelID or MasterPartsLinkTableModelList.ModelID = -1))
        order by PartNumber, MasterParts.Description                   
        Select @Done = 1
        End       
   end
   
 
-- Select * from #temp 
 
   
if @Done = 0
   begin               
   Insert #Temp
   Select MasterPartsLinkTable.* 
   from MasterPartsLinkTable where MasterPartsLinkTableID = -1             
   end
   
   
--Select * from MasterPartsTableIFSLocationStorage   
   
Update #Temp set Quantity = 0 
        
Select SUM(QTY) as QTY, MasterPartsLinkTableID 
into #Temp2
from MasterPartsTableIFSLocationStorage 
where MasterPartsLinkTableID in (Select MasterPartsLinkTableID from #Temp)
group by MasterPartsLinkTableID

----Select * from #Temp2

Update A set A.Quantity = B.QTY
From #Temp A
Inner join #Temp2 B on A.MasterPartsLinkTableID = B.MasterPartsLinkTableID

Select * from #Temp order by PartNumber

Drop Table #Temp
Drop Table #Temp2

END
/****** Object:  StoredProcedure [dbo].[Record_CellbieTransaction]    Script Date: 10/16/2019 11:46:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>

/*
Declare @ReturnMessage nvarchar(500)
exec Record_CellbieTransaction 2961, 'Sent','CellbieStatus','api','paramjson','outputjson','info', 'error text', 'errortextinternal', 'Misc Text','jmccomb',@ReturnMessage Output
Print @ReturnMessage


// 2957	358040080423290     LG V30
// 2958	355458060568814     Google Nexus 6
// 2959	356160070900412     Samsung Galaxy S6 Edge
// 2960	990004600575546     BB10


Select * from ReceiveDetailCellbieStatus
Select * from ReceiveDetailCellbieStatusLog
Select * from ReceiveDetailCellbieCommLog
Update ReceiveDetail set ESN = '990004600575546' where ReceiveDetailID = 2960

2961
2960
2959
2958
2957
2956
2955
2954
2953

Select top 30 ReceiveDetailID from receiveDetail where Version = '000' order by ReceiveDetailID desc


*/


-- =============================================
Create PROCEDURE [dbo].[Record_CellbieTransaction]
	@ReceiveDetailID numeric(18, 0),
	@Status nvarchar(20),
	@CellbieStatus nvarchar(20),
	@API [nvarchar] (100),	
	@ParameterJSON [nvarchar](max),
	@OutputJSON [nvarchar](max),
	@TransactionResultJSON [nvarchar](max),
	@ErrorText [nvarchar](max),
	@ErrorInternalText [nvarchar](max),
	--@SendParamAgree nvarchar(20),
	--@SendParamMessage nvarchar(100),
	@MiscText nvarchar(500),
	@UserName nvarchar(50),
	@ReturnMessage nvarchar(500) Output
AS
BEGIN
Set NOCOUNT ON

Select @ReturnMessage = 'Error: Status not set!'
if not exists(Select * from ReceiveDetail where ReceiveDetailID = @ReceiveDetailID)
   begin
   Select @ReturnMessage = 'Error: Device Record not found!'   
   return
   end
   
exec Update_CellbieStatus @ReceiveDetailID, @Status,@MiscText,@UserName,@ReturnMessage Output

if substring(@ReturnMessage,1,5) = 'Error'
   begin
   return
   end


declare @ReceiveDetailCellbieStatusID [numeric](18, 0)
select @ReceiveDetailCellbieStatusID = ReceiveDetailCellbieStatusID from ReceiveDetailCellbieStatus where ReceiveDetailID = @ReceiveDetailID
if ISNULL(@ReceiveDetailCellbieStatusID, -1) < 1
   begin
   select @ReturnMessage = 'Error: Unable to find master Cellbie Status record.'
   return
   end
 
 
 -- Select * from  ReceiveDetailCellbieCommLog
 
 INSERT INTO [ReceiveDetailCellbieCommLog]
           ([ReceiveDetailCellbieStatusID]
           ,[ReceiveDetailID]
           ,[Status]
           ,[API]
           ,[ParameterJSON]
           ,[OutputJSON]
           ,[TransactionResultJSON]
           ,[ErrorText]
           ,[ErrorInternalText]
           ,[MiscText]
           ,[CreateDate]
           ,[CreateUser]
           ,[LastUpdateDate]
           ,[LastUpdateUser])
     VALUES
           (@ReceiveDetailCellbieStatusID
           ,@ReceiveDetailID
           ,@CellbieStatus
           ,@API
           ,@ParameterJSON
           ,@OutputJSON
           ,@TransactionResultJSON
           ,@ErrorText
           ,@ErrorInternalText
           ,@MiscText
           ,getdate()
           ,@UserName
           ,getdate()
           ,@UserName)


   select @ReturnMessage = 'Success: Transaction Added:' + CONVERT(nvarchar(20), @@Identity)
Return 0

END
/****** Object:  StoredProcedure [dbo].[Update_CellbieStatus]    Script Date: 10/16/2019 11:50:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>

/*
Declare @ReturnMessage nvarchar(500)
exec Update_CellbieStatus -1, 'Send', 'Agree','PramMessage'    ,'Misc Text'    ,'jmccomb',@ReturnMessage Output
Print @ReturnMessage
*/


-- =============================================
Create PROCEDURE [dbo].[Update_CellbieStatus]
	@ReceiveDetailID numeric(18, 0),
	@Status nvarchar(20),
	--@SendParamAgree nvarchar(20),
	--@SendParamMessage nvarchar(100),
	@MiscText nvarchar(500),
	@UserName nvarchar(50),
	@ReturnMessage nvarchar(500) Output
AS
BEGIN
Set NOCOUNT ON

 Select @ReturnMessage = 'Error: Status not set!'
 if not exists(Select * from ReceiveDetail where ReceiveDetailID = @ReceiveDetailID)
   begin
   Select @ReturnMessage = 'Error: Device Record not found!'   
   return
   end

 if LEN(@Status) < 1
   begin
   Select @Status = 'Send'
   end

 if @Status != 'Send'
and @Status != 'Sent'
and @Status != 'Success'
and @Status != 'Archive'
and @Status != 'Error'
   begin
   Select @ReturnMessage = 'Error: Invalid Status:' + @Status
   return
   end

 if not exists(Select * from ReceiveDetailCellbieStatus where ReceiveDetailID = @ReceiveDetailID)
    begin
    INSERT INTO [ReceiveDetailCellbieStatus]
               ([ReceiveDetailID],[Status],[MiscText],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser])
        VALUES (@ReceiveDetailID,@Status,@MiscText,GETDATE(),@UserName,GETDATE(),@UserName)
    Select @ReturnMessage = 'Success: Status Added!'
    end
 else
    begin
    Declare @ReceiveDetailCellbieStatusID numeric(18, 0)
     Select @ReceiveDetailCellbieStatusID = ReceiveDetailCellbieStatusID,
            @MiscText =  case when @MiscText = '..' then MiscText else @MiscText end
       From ReceiveDetailCellbieStatus where ReceiveDetailID = @ReceiveDetailID

     UPDATE [ReceiveDetailCellbieStatus]
        SET [Status] = @Status,[MiscText] = @MiscText,[LastUpdateDate] = GETDATE(),[LastUpdateUser] = @UserName
      WHERE ReceiveDetailCellbieStatusID = @ReceiveDetailCellbieStatusID 
    Select @ReturnMessage = 'Success: Status Updated!'
   end   

 
Return 0

END
/****** Object:  StoredProcedure [dbo].[Utility_FlagOptionInactive]    Script Date: 10/16/2019 11:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------

--------------------------------------------------------------------------------------
/*

exec Utility_FlagOptionInactive 6129

exec Utility_ModelSummary

*/
--------------------------------------------------------------------------------------
--
*/


-- Select * from MasterCarrierManufacturerLookup where OptionModelID = 6171

Create PROCEDURE [dbo].[Utility_FlagOptionInactive]
      @OptionID numeric(18,0)

 AS

Begin
SET NOCOUNT ON

Declare @optionInActiveID numeric(18)
Declare @LookupInActiveID numeric(18)


Declare @OptionCount numeric(18)
Declare @LookupCount numeric(18)

Select @optionInActiveID = OptionStatusID from OptionStatus where Status = 'Inactive'
Select @LookupInActiveID = MasterCarrierManufacturerStatusID from MasterCarrierManufacturerStatus where Status = 'Inactive'

if ISNULL(@optionInActiveID, -1) < 1
   begin
   Print 'Error: Option Status Inactive Not Found'
   return
   end

if ISNULL(@optionInActiveID, -1) < 1
   begin
   Print 'Error: MasterCarrierManufacturerlookup Status Inactive Not Found'
   return
   end

Update [Option] set OptionStatusID = @optionInActiveID where OptionID = @OptionID
Select @OptionCount = @@RowCount

Update MasterCarrierManufacturerLookup set StatusID = @LookupInActiveID where OptionModelID = @OptionID
Select @LookupCount = @@RowCount

Print '# Options Set Inactive:' + convert(nvarchar(20), @OptionCount) + '        # Lookups Set Inactive:' + convert(nvarchar(20), @LookupCount)

End
/****** Object:  StoredProcedure [dbo].[Utility_LoadAttributeValue_04]    Script Date: 10/16/2019 11:57:51 ******/
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

Exec Utility_LoadAttributeValue_03 'Discr Type','Buyers Remorse','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Type','DOA Accessories','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Type','DOA Hardware','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Type','Extended Warranty','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Type','Hardware','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Type','In Warranty','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Type','Loaners','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Type','Misc','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Type','Out of Warranty','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Type','Remix/Recall','jmccomb'

Exec Utility_LoadAttributeValue_03 'Discr OutCome','Information Received','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr OutCome','Moved to Non Sell','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr OutCome','Printed Paper Work','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr OutCome','Recycled','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr OutCome','Resolved by GMP','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr OutCome','Shipped Back to Store','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr OutCome','Shipped to Head Office','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr OutCome','Shipped/Redirected to Correct Location','jmccomb'

Exec Utility_LoadAttributeValue_03 'Discr Div','SG','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Div','TB','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Div','TM','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Div','WE','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Div','WW','jmccomb'

exec Utility_LoadAttributeValue_03 'Discr Desc','Apple ID Lock','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Box IMEI Transferred not Phone','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Customer Abuse','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Extra Item','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','IMEI Different than Paper Work','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Invalid/No Waybill','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Missing Item','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','MSC 1yr Warranty Period','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Name on POP Different','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','No POP/POR/PO','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','No Response to the Quote','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','No Service Request','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','No Stock Transfer','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Non Glentel Product','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Not in Non Sell','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Not Part of Remix','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Not picked up from Store','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Past Return Period','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Quote Accepted by the Customer','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Quote Rejected by the Customer','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Service Provider 1st year warranty period','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Service Request Incomplete','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Shipped to Wrong Location','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Wrong Item','jmccomb'
Exec Utility_LoadAttributeValue_03 'Discr Desc','Wrong/No Product in Case','jmccomb'

Select * from Question where name = 'Discr Type'
Select * from [option] where questionid = 78

Delete [option] where questionid = 78

*/

Create PROCEDURE [dbo].[Utility_LoadAttributeValue_04]
    
    @mAttributeName nVarchar(20),
    @mAttributeScankey nVarchar(50) = '',
    @mAttributeItemName nVarchar(20) = '',    
    @mAttributeValue nVarchar(50),    
    @mAttributeSeq nVarchar(10) = '',
    @mUserName nVarchar(50),
    @mMessage nvarchar(500) OUTPUT
   
AS
BEGIN
Set NOCOUNT ON
--Select Name from Question where Name = 'Colour'

Select @mMessage = ''
Declare @mStatusID numeric(18)
Declare @mTypeID numeric(18)
Declare @mQuestionID numeric(18)

Select Top 1 @mQuestionID = QuestionID from Question where ltrim(rtrim(Question.Name)) = @mAttributeName
Select Top 1 @mTypeID = OptionTypeID from OptionType where [Type] = 'Other'
Select Top 1 @mStatusID = OptionStatusID from OptionStatus where Status = 'Active'
Select @mQuestionID = isnull(@mQuestionID, -1)
Select @mTypeID = isnull(@mTypeID, -1)
Select @mStatusID = isnull(@mStatusID, -1)
if @mQuestionID < 1 
   begin
   Select @mMessage = 'Question Not found ' + @mAttributeName
   Print 'Question Not found ' + @mAttributeName
   Return 0
   end
if @mTypeID < 1
   begin
   Select @mMessage = 'Type Not found ' + 'Other'  
   Print 'Type Not found ' + 'Other'
   Return 0
   end
if @mStatusID < 1
   begin
   Select @mMessage = 'Status Not found ' + 'Active'  
   Print 'Status Not found ' + 'Active'
   Return 0
   end   


if @mAttributeName = 'Model' or
   @mAttributeName = 'Carrier' or
   @mAttributeName = 'Manufacturer' or
   @mAttributeName = 'Colour'
   begin
   if exists (Select * from [Option] where QuestionID = @mQuestionID and Name = @mAttributeItemName)
     begin
     Select @mMessage = @mAttributeName + ':' + @mAttributeItemName + ' already on file. (ABBR)'   
     Print @mAttributeName + ':' + @mAttributeItemName + ' already on file. (ABBR)'
     Return 0
     End
   end 

   if exists (Select OptionID from [Option] where QuestionID = @mQuestionID and OptionText =  @mAttributeValue)
     begin
     Select @mMessage = @mAttributeName + ':' + @mAttributeValue + ' already on file. (VALUE)'   
     Print @mAttributeName + ':' + @mAttributeValue + ' already on file. (VALUE)'
     Return 0
     End

   


   Select @mMessage = 'Success, Added:' + @mAttributeName + ':' + @mAttributeValue + ' - ABBR:' + @mAttributeItemName
   Print 'Success, Added:' + @mAttributeName + ':' + @mAttributeValue
   INSERT INTO [Option]
              ([ScanKey],[MacroKey]
              ,[OptionStatusID]
              ,[OptionTypeID]
              ,[OptionText]
              ,[HelpText]
              ,[QuestionID]
              ,[Name]
              ,[Sequence]
              ,[CreateDate]
              ,[CreateUser]
              ,[LastUpdateDate]
              ,[LastUpdateUser]
              ,[MicroKey])
     VALUES
           (@mAttributeScankey,''
           ,@mStatusID
           ,@mTypeID
           ,@mAttributeValue
           ,@mAttributeValue
           ,@mQuestionID
           ,@mAttributeItemName
           ,1
           ,getdate()
           ,'BulkAdd'
           ,getdate()
           ,'BulkAdd'
           ,'') 
             

declare @mID numeric(18)   
Select @mID  = @@IDENTITY
if len(@mAttributeScankey) < 1
   begin
   Select @mAttributeScankey = 'O' + CONVERT(nvarchar(10),@mID)
   Update [Option] set [ScanKey] = @mAttributeScankey where OptionID = @mID
   end

Return 1

END
/****** Object:  StoredProcedure [dbo].[Utility_LoadAttributeValue_WithDelete]    Script Date: 10/16/2019 11:56:48 ******/
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


Exec Utility_LoadAttributeValue 'Colour','Black'
Exec Utility_LoadAttributeValue 'Colour','Blue'
Exec Utility_LoadAttributeValue 'Colour','Brown'
Exec Utility_LoadAttributeValue 'Colour','Coral'
Exec Utility_LoadAttributeValue 'Colour','Fushia'
Exec Utility_LoadAttributeValue 'Colour','Gold'
Exec Utility_LoadAttributeValue 'Colour','Green'
Exec Utility_LoadAttributeValue 'Colour','Greg'
Exec Utility_LoadAttributeValue 'Colour','Grey'
Exec Utility_LoadAttributeValue 'Colour','Gun Metal'
Exec Utility_LoadAttributeValue 'Colour','Orange'
Exec Utility_LoadAttributeValue 'Colour','Pink'
Exec Utility_LoadAttributeValue 'Colour','Purple'
Exec Utility_LoadAttributeValue 'Colour','Red'
Exec Utility_LoadAttributeValue 'Colour','Red '
Exec Utility_LoadAttributeValue 'Colour','Red/Black'
Exec Utility_LoadAttributeValue 'Colour','Silver'
Exec Utility_LoadAttributeValue 'Colour','Silver/Grey'
Exec Utility_LoadAttributeValue 'Colour','violet'
Exec Utility_LoadAttributeValue 'Colour','White'
Exec Utility_LoadAttributeValue 'Colour','White/Black'
Exec Utility_LoadAttributeValue 'Colour','White/Purple'





*/

Create PROCEDURE [dbo].[Utility_LoadAttributeValue_WithDelete]
    @mAttributeName nVarchar(20),
    @mDelete int, 
    @mAttributeScankey nVarchar(50),
    @mAttributeItemName nVarchar(20),
    @mAttributeValue nVarchar(50),
    @mAttributeSeq nVarchar(10),
    @mUserName nVarchar(50),
    @mReturnMessage nvarchar(50) Output
   
AS
BEGIN
Set NOCOUNT ON
--Select Name from Question where Name = 'Colour'
Declare @Seq int
Declare @mStatusID numeric(18)
Declare @mStatusDeleteID numeric(18)
Declare @mTypeID numeric(18)
Declare @mQuestionID numeric(18)
Declare @mOptionID numeric(18)
Select @Seq = 100
if ISNUMERIC(@mAttributeSeq) = 1
   Select @Seq = CONVERT(int ,@mAttributeSeq)

Select Top 1 @mQuestionID = QuestionID from Question where ltrim(rtrim(Question.Name)) = @mAttributeName
Select Top 1 @mTypeID = OptionTypeID from OptionType where [Type] = 'Other'
Select Top 1 @mStatusID = OptionStatusID from OptionStatus where Status = 'Active'
Select Top 1 @mStatusDeleteID = OptionStatusID from OptionStatus where Status = 'Inactive'
Select @mQuestionID = isnull(@mQuestionID, -1)
Select @mTypeID = isnull(@mTypeID, -1)
Select @mStatusID = isnull(@mStatusID, -1)
if @mQuestionID < 1 
   begin
   Select @mReturnMessage = 'Error: Question Not found ' + @mAttributeName
   Print 'Question Not found ' + @mAttributeName
   Return 0
   end
if @mTypeID < 1
   begin
   Select @mReturnMessage = 'Error: Type Not found ' + 'Other'
   Print 'Type Not found ' + 'Other'
   Return 0
   end
if @mStatusID < 1
   begin
   Select @mReturnMessage = 'Error: Status Not found ' + 'Active'
   Print 'Status Not found ' + 'Active'
   Return 0
   end   
   

Select @mOptionID = OptionID from [Option] where 1 = 1
                                           and QuestionID = @mQuestionID
                                           and OptionStatusID = @mStatusID
                                           and (Name = @mAttributeItemName
                                            or ScanKey = @mAttributeScankey
                                            or OptionText = @mAttributeValue)


-- Do we delete?
if (@mDelete > 0 and isnull(@mOptionID,-1) > 0)
    begin
    Update [Option] set OptionStatusID = @mStatusDeleteID, [LastUpdateDate]= GETDATE(), [LastUpdateUser] = @mUserName 
     where OptionID = @mOptionID
    Select @mReturnMessage = 'Updated: Status Set to Inactive '
    Print  'Status Set to Inactive '
    Return 0    
    end

-- Do we update?
if (isnull(@mOptionID,-1) > 0)
    begin
    Update [Option] set OptionStatusID = @mStatusID
         , [ScanKey] = @mAttributeScankey
         , [OptionText] = @mAttributeValue
         , [Name] = @mAttributeItemName
         , [Sequence] =  @Seq                          
         , [LastUpdateDate]= GETDATE(), [LastUpdateUser] = @mUserName
     where OptionID = @mOptionID
    Select @mReturnMessage = 'Updated:'
    Print  'Attribute Updated'
    Return 0    
    end

-- Do we Add New?   
  
   
if Not Exists(Select OptionID from [Option] where QuestionID = @mQuestionID and OptionText =  @mAttributeValue )
   begin
   Print 'Insert:' + @mAttributeName + ':' + @mAttributeValue
   INSERT INTO [Option]
              ([ScanKey],[MacroKey]
              ,[OptionStatusID]
              ,[OptionTypeID]
              ,[OptionText]
              ,[HelpText]
              ,[QuestionID]
              ,[Name]
              ,[Sequence]
              ,[CreateDate]
              ,[CreateUser]
              ,[LastUpdateDate]
              ,[LastUpdateUser]
              ,[MicroKey])
     VALUES
           (@mAttributeScankey,''
           ,@mStatusID
           ,@mTypeID
           ,@mAttributeValue
           ,@mAttributeValue
           ,@mQuestionID
           ,@mAttributeItemName
           ,@Seq
           ,getdate()
           ,@mUserName
           ,getdate()
           ,@mUserName
           ,'')  
    Select @mReturnMessage = 'Inserted'
    if LEN(@mAttributeScankey) = 0
       begin
       Select @mOptionID = @@IDENTITY
       Select @mAttributeScankey = 'O' + ltrim(RTRIM(convert(nvarchar(20), @mOptionID)))
       Update [Option] set [ScanKey] = @mAttributeScankey where OptionID = @mOptionID
       end  
   end

Return 1

END
/****** Object:  StoredProcedure [dbo].[Utility_ModelSummary]    Script Date: 10/16/2019 11:55:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------

--------------------------------------------------------------------------------------
/*

exec Utility_ModelSummary

*/
--------------------------------------------------------------------------------------
--
*/




Create PROCEDURE [dbo].[Utility_ModelSummary]

 AS

Begin
SET NOCOUNT ON


Declare @QuestionID numeric(18,9)
Select @QuestionID = QuestionID from Question where Name = 'Model'
Select OptionID
     , [Option].Name as ABBR
     , [Option].OptionStatusID
	 , Convert(Datetime, null) as LastUseDate
     , CONVERT(numeric(18), 0) as DevicesAll
     , CONVERT(numeric(18), 0) as Devices000
     , CONVERT(numeric(18), 0) as Devices001
     , CONVERT(numeric(18), 0) as Deviceslookup
     , [Option].OptionStatusID as LookupStatusID
	 , Convert(Datetime, null) as LookupMinCreateDate
	 , Convert(Datetime, null) as LookupMaxCreateDate
  into #TempDevices   
  from [Option] where QuestionID = @QuestionID
  
  
 -- Select T.OptionID, Sum(1) as DevicesAll
 --      , Sum(case when R.Version = '000' then 1 else 0 end) as Devices000
 --      , Sum(case when R.Version = '000' then 0 else 1 end) as Devices001
 -- into #TempDevices01   
 --from #TempDevices T
 -- Inner join ReceiveDetailItem I on T.OptionID = I.OptionID  
 -- Inner join ReceiveDetail R on I.ReceiveDetailID = r.ReceiveDetailID 
 -- group by T.OptionID, R.Version
  
 -- Update #TempDevices set DevicesAll = I.DevicesAll
 --                        ,Devices000 = I.Devices000
 --                        ,Devices001 = I.Devices001
 -- from #TempDevices T
 -- Inner join #TempDevices01 I on T.OptionID = I.OptionID   



  Update #TempDevices set DevicesAll = (Select count(*) from  ReceiveDetailItem I
                                                        Inner join ReceiveDetail R on I.ReceiveDetailID = r.ReceiveDetailID 
														Where optionID = #TempDevices.optionID)
  Update #TempDevices set LastUseDate = (Select Max(I.Createdate) from  ReceiveDetailItem I
                                                        Inner join ReceiveDetail R on I.ReceiveDetailID = r.ReceiveDetailID 
														Where optionID = #TempDevices.optionID)

  Update #TempDevices set Devices000 = (Select count(*) from  ReceiveDetailItem I
                                                        Inner join ReceiveDetail R on I.ReceiveDetailID = r.ReceiveDetailID 
														Where optionID = #TempDevices.optionID and R.Version = '000')


  Update #TempDevices set Devices001 = (Select count(*) from  ReceiveDetailItem I
                                                        Inner join ReceiveDetail R on I.ReceiveDetailID = r.ReceiveDetailID 
														Where optionID = #TempDevices.optionID and R.Version != '000')

  -----------------------------------------------------------------------------------------------
  
  --Update #TempDevices set DevicesAll = Sum(1)
  --                       ,Devices000 = Sum(case when R.Version = '000' then 1 else null end)
  --                       ,Devices001 = Sum(case when R.Version = '000' then null else 1 end)
  --                       ,Deviceslookup = 0
  --from #TempDevices T
  --Inner join ReceiveDetailItem I on T.OptionID = I.OptionID  
  --Inner join ReceiveDetail R on I.ReceiveDetailID = r.ReceiveDetailID
  

  Select T.OptionID, Sum(1) as Deviceslookup
        ,StatusID as LookupStatusID, Min(CreateDate) as LookupMinCreateDate, Max(CreateDate) as LookupMaxCreateDate
  into #TempDevices02   
 from #TempDevices T
  Inner join MasterCarrierManufacturerLookup I on T.OptionID = I.OptionModelID
  group by T.OptionID, StatusID

  Update #TempDevices set Deviceslookup = I.Deviceslookup
                         ,LookupStatusID = I.LookupStatusID
						 ,LookupMinCreateDate = I.LookupMinCreateDate
						 ,LookupMaxCreateDate = I.LookupMaxCreateDate
  from #TempDevices T
  Inner join #TempDevices02 I on T.OptionID = I.OptionID 
  
  
  
 -- Select * from #TempDevices  
  
 SELECT     D.OptionID, O.Name AS ABBR, O.OptionText, O.ScanKey, S.Status AS OptionStatus, O.CreateDate, D.LastUseDate, DevicesAll, Devices000, Devices001, S1.Status AS LookUpStatus, Deviceslookup, D.LookupMinCreateDate, D.LookupMaxCreateDate
FROM         [#TempDevices] AS D INNER JOIN
                      [Option] AS O ON O.OptionID = D.OptionID INNER JOIN
                      OptionStatus AS S ON S.OptionStatusID = D.OptionStatusID LEFT OUTER JOIN
                      MasterCarrierManufacturerStatus AS S1 ON S1.MasterCarrierManufacturerStatusID = D.LookupStatusID
	--where Devices000 > 0 and Devices001 > 0
Order by OptionText, ABBR, OptionStatus, LookUpStatus    
    
    
    
    
  Drop table #TempDevices
  --Drop table #TempDevices01
  Drop table #TempDevices02
  
  

End
/****** Object:  StoredProcedure [dbo].[Utility_RemoveInactiveModels]    Script Date: 10/16/2019 11:53:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------

--------------------------------------------------------------------------------------
/*

exec Utility_RemoveInactiveModels

exec Utility_ModelSummary

*/
--------------------------------------------------------------------------------------
--
*/


Create PROCEDURE [dbo].[Utility_RemoveInactiveModels]

 AS

Begin
SET NOCOUNT ON

Declare @optionInActiveID numeric(18)
Declare @LookupInActiveID numeric(18)


Declare @OptionCount numeric(18)
Declare @LookupCount numeric(18)

Select @optionInActiveID = OptionStatusID from OptionStatus where Status = 'Inactive'
Select @LookupInActiveID = MasterCarrierManufacturerStatusID from MasterCarrierManufacturerStatus where Status = 'Inactive'

if ISNULL(@optionInActiveID, -1) < 1
   begin
   Print 'Error: Option Status Inactive Not Found'
   return
   end

if ISNULL(@optionInActiveID, -1) < 1
   begin
   Print 'Error: MasterCarrierManufacturerlookup Status Inactive Not Found'
   return
   end




Delete MasterModelMemoryLookup
Delete MasterCarrierManufacturerLookup where StatusID = @LookupInActiveID
Select @LookupCount = @@RowCount

Delete [Option] where QuestionID = 244 and OptionStatusID = @optionInActiveID
Select @OptionCount = @@RowCount

Print '# Options Removed:' + convert(nvarchar(20), @OptionCount) + '        # Lookups Removed:' + convert(nvarchar(20), @LookupCount)

End


/****** Object:  StoredProcedure [dbo].[GetDashboardQC_01_GridValueFiltered]    Script Date: 10/16/2019 11:42:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*

Receiver
Supervisors


exec GetDashboardQC_01_GridValueFiltered 'MM/DD/YYYY',12, ''
exec GetDashboardQC_01_GridValueFiltered '',16000, ''
exec GetDashboardQC_01_GridValueFiltered '10/06/2016',1200, 'Receiver,Supervisors'
exec GetDashboardQC_01_GridValueFiltered '10/06/2016',1200
exec GetDashboardQC_01_GridValueFiltered '9/16/2016',12, ''
exec GetDashboardQC_01_GridValueFiltered '',12, ''
exec GetDashboardQC_01_GridValueFiltered '',12, ''






CREATE TABLE #Temp1(
	[Today] [nvarchar](10) NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[Createdate] [nvarchar](10) NULL,
	[daysago] [int] NULL,
	[Bridge Repair] [int] NULL,
	[MSC Repair Handling] [int] NULL,
	[Product Placement] [int] NULL,
	[Total] [numeric](10, 0) NULL
)

Insert into #Temp1
exec GetDashboardRepair_01 '10/06/2016',12

Select * from #Temp1



Select * from Process where Name in ('Activation','Buffing','Function Test','Grade Improvement','Grading','Physical Damage','Unlocking')


*/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GetDashboardQC_01_GridValueFiltered]
    @Today nvarchar(10),
    @Days int,
    @RoleFilter nvarchar(max)
AS
BEGIN

Set nocount on

Declare @delimiter varchar(20)
Select @delimiter = ','
SELECT convert(numeric(18),0) as Processed, * into #TempRoles FROM fn_SplitDistinct(@RoleFilter, @delimiter)
Update #TempRoles set value = LTRIM(rtrim(Value))
--Select * from #TempRoles
--Select * from fn_SplitDistinct('aaa, aaab,ddddddd', ',')

SELECT Distinct aspnet_Users.UserName
  into #TempUsers
  FROM aspnet_Roles 
 INNER JOIN aspnet_UsersInRoles ON aspnet_Roles.RoleId = aspnet_UsersInRoles.RoleId 
 INNER JOIN aspnet_Users ON aspnet_UsersInRoles.UserId = aspnet_Users.UserId
 Inner join #TempRoles T on T.value = aspnet_Roles.RoleName
--where UserName in ('DCARELL','sandra clause')                      


/*   Get the raw data we will reformat  */
CREATE TABLE #Temp1(
	[Today] [nvarchar](10) NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[Createdate] [nvarchar](10) NULL,
	[daysago] [int] NULL,
	
	[Activation] [int] NULL,
	 [Buffing] [int] NULL,
	 [Function Test] [int] NULL,
	 [Grade Improvement] [int] NULL,
	 [Grading] [int] NULL,
	 [Physical Damage] [int] NULL,
	 [Unlocking] [int] NULL,
	
	[Total] [numeric](10, 0) NULL,
	[Processed] [int] NULL
)
Insert into #Temp1
exec GetDashboardQC_01 @Today,@Days
Update #Temp1 set [Processed] = 0
----------------------------------------------


  --and (ProcessText = 'Bridge Repair'
  --or ProcessText = 'MSC Repair Handling'
  --or ProcessText = 'ProductPlacement')



/*   Setup for reformating the data  */
CREATE TABLE #TempGridGT(
	[CreateUser] [nvarchar](50) NOT NULL,
	[C0_A]  [numeric](10, 0) NULL,
	[C0_B]  [numeric](10, 0) NULL,
	[C0_C]  [numeric](10, 0) NULL,
	[C0_D]  [numeric](10, 0) NULL,
	[C0_E]  [numeric](10, 0) NULL,
	[C0_F]  [numeric](10, 0) NULL,
	[C0_G]  [numeric](10, 0) NULL,
	[C0_T]  [numeric](10, 0) NULL,
	
	[C1_A] [numeric](10, 0) NULL,
	[C1_B] [numeric](10, 0) NULL,
	[C1_C] [numeric](10, 0) NULL,
	[C1_D]  [numeric](10, 0) NULL,
	[C1_E]  [numeric](10, 0) NULL,
	[C1_F]  [numeric](10, 0) NULL,
	[C1_G]  [numeric](10, 0) NULL,
	[C1_T] [numeric](10, 0) NULL,
	
    [C2_A] [numeric](10, 0) NULL,
	[C2_B] [numeric](10, 0) NULL,
	[C2_C] [numeric](10, 0) NULL,
	[C2_D]  [numeric](10, 0) NULL,
	[C2_E]  [numeric](10, 0) NULL,
	[C2_F]  [numeric](10, 0) NULL,
	[C2_G]  [numeric](10, 0) NULL,
	[C2_T] [numeric](10, 0) NULL,
	[C_GT] [numeric](10, 0) NULL
)
CREATE TABLE #TempGrid(
	[CreateUser] [nvarchar](50) NOT NULL,
    [Seq] [int] NULL,
	[Row] [nvarchar](5) NULL,	
	
	[C0_Date] [nvarchar](10) NULL,
	[C0_A] [numeric](10, 0) NULL,
	[C0_B] [numeric](10, 0) NULL,
	[C0_C] [numeric](10, 0) NULL,
	[C0_D]  [numeric](10, 0) NULL,
	[C0_E]  [numeric](10, 0) NULL,
	[C0_F]  [numeric](10, 0) NULL,
	[C0_G]  [numeric](10, 0) NULL,
	[C0_T] [numeric](10, 0) NULL,
	
	[C1_Date] [nvarchar](10) NULL,
	[C1_A] [numeric](10, 0) NULL,
	[C1_B] [numeric](10, 0) NULL,
	[C1_C] [numeric](10, 0) NULL,
	[C1_D]  [numeric](10, 0) NULL,
	[C1_E]  [numeric](10, 0) NULL,
	[C1_F]  [numeric](10, 0) NULL,
	[C1_G]  [numeric](10, 0) NULL,
	[C1_T] [numeric](10, 0) NULL,
	
	[C2_Date] [nvarchar](10) NULL,
    [C2_A] [numeric](10, 0) NULL,
	[C2_B] [numeric](10, 0) NULL,
	[C2_C] [numeric](10, 0) NULL,
	[C2_D]  [numeric](10, 0) NULL,
	[C2_E]  [numeric](10, 0) NULL,
	[C2_F]  [numeric](10, 0) NULL,
	[C2_G]  [numeric](10, 0) NULL,
	[C2_T] [numeric](10, 0) NULL,
	[C_GT] [numeric](10, 0) NULL
)

-- Lay down the first layer.
/*
Insert #TempGrid ([Row], [CreateUser],[C0_A],[C0_B],[C0_C],[C0_T]
                              ,[C1_A],[C1_B],[C1_C],[C1_T]
                              ,[C2_A],[C2_B],[C2_C],[C2_T] 
                              ,[C_GT], Seq)
Values ('h0','','date','','',''        
               ,'date','','',''        
               ,'date','','',''          
               ,'', 1)        
Insert #TempGrid ([Row],[CreateUser],[C0_A],[C0_B],[C0_C],[C0_T]
                              ,[C1_A],[C1_B],[C1_C],[C1_T]
                              ,[C2_A],[C2_B],[C2_C],[C2_T] 
                              ,[C_GT], Seq)
Values ('h1','Rep','Bridge Repair','MSC Repair handline','Product Placement','total'   
,'Bridge Repair','MSC Repair handline','Product Placement','total'   
,'Bridge Repair','MSC Repair handline','Product Placement','total'     
,'grand total', 2)   
Insert #TempGridGT ([CreateUser],[C0_A],[C0_B],[C0_C],[C0_T]
                    ,[C1_A],[C1_B],[C1_C],[C1_T]
                    ,[C2_A],[C2_B],[C2_C],[C2_T] 
                    ,[C_GT])
Values ('Grand Total',0,0,0,0        
                   ,0,0,0,0
                   ,0,0,0,0       
                   ,0)  
*/

-- Our data is telling us the number of days since the "today" date.
--     depending on the work schedule, some days may not be present.
--     Our final grid carries three days of column data.
--     We would rather "squish out" those absend days.
--     This does it.
--     The Days since original number is lost.


Create Table #Renum(
   [NewNumb] [int] IDENTITY(1,1) NOT NULL,
   [daysago] [int])
Insert #Renum (daysago)
Select distinct daysago from #Temp1 order by daysago
Update #Temp1 set daysago = R.NewNumb from #Temp1 T inner join #Renum R on T.daysago = R.daysago
       -- Because we only want to show the users with transactions in the last 3 cycles.
       Delete #Temp1 where daysago > 3
Drop Table #Renum
---------------------------------------------------------------------------------

Declare @CreateUser [nvarchar](50),
	    @Createdate [nvarchar](10),
	    @daysago [int],
	    @Activation [int],
	    @Buffing [int],
	    @FunctionTest [int],
	    @GradeImprovement [int],	    
	    @Grading [int],
	    @PhysicalDamage [int],
	    @Unlocking [int],
	    @Total Numeric(10,0),
	    @C int
	    
Select @C = 0		    

while exists (Select * from #Temp1 where [Processed] = 0 and @C < 10000000)
      begin
      Select @C = @C + 1
      Select Top 1 @CreateUser = Createuser,
                   @Createdate = Createdate,
                   @daysago = daysago,
        
                   @Activation = [Activation],
                   @Buffing = [Buffing],
                   @FunctionTest = [Function Test],
                   @GradeImprovement = [Grade Improvement],           
                   
                   @Grading = [Grading],
                   @PhysicalDamage = [Physical Damage],
                   @Unlocking = [Unlocking],
                   @Total = [Total]
            from #Temp1 where Processed = 0
      update #Temp1 set processed = 1 where CreateUser = @Createuser and Createdate = @Createdate 
      
      Select @Total = ISNULL(@Activation, 0) + ISNULL(@Buffing, 0) +  ISNULL(@FunctionTest, 0) + ISNULL(@GradeImprovement, 0) + ISNULL(@Grading, 0) +  ISNULL(@PhysicalDamage, 0)+  ISNULL(@Unlocking, 0)
                   
      -- Add the record if not found already in Grid Section             
      if not exists(Select * from #TempGrid where CreateUser = @CreateUser)
         begin
         Insert #TempGrid ([Row], [CreateUser],[C0_A],[C0_B],[C0_C],[C0_D],[C0_E],[C0_F],[C0_G],[C0_T]
                             ,[C1_A],[C1_B],[C1_C],[C1_D],[C1_E],[C1_F],[C1_G],[C1_T]
                             ,[C2_A],[C2_B],[C2_C],[C2_D],[C2_E],[C2_F],[C2_G],[C2_T] 
                             ,[C_GT], Seq)
         Values ('d0',@CreateUser,0,0,0,0,0,0,0,0       
                                 ,0,0,0,0,0,0,0,0  
                                 ,0,0,0,0,0,0,0,0           
                                 ,0,3)      
         Insert #TempGridGT ([CreateUser],[C0_A],[C0_B],[C0_C],[C0_D],[C0_E],[C0_F],[C0_G],[C0_T]
                             ,[C1_A],[C1_B],[C1_C],[C1_D],[C1_E],[C1_F],[C1_G],[C1_T]
                             ,[C2_A],[C2_B],[C2_C],[C2_D],[C2_E],[C2_F],[C2_G],[C2_T] 
                             ,[C_GT])
         Values (@CreateUser,0,0,0,0,0,0,0,0         
                            ,0,0,0,0,0,0,0,0 
                            ,0,0,0,0,0,0,0,0   
                            ,0)                                  
         end
      
      if @daysago = 1
         begin
         --Update #TempGrid set C0_A = @Createdate where Row = 'h0'
         Update #TempGrid set C0_A = isnull(@Activation, 0)
                             ,C0_B = isnull(@Buffing, 0)
                             ,C0_C = isnull(@FunctionTest, 0)
                             ,C0_D = isnull(@GradeImprovement, 0)
                             ,C0_E = isnull(@Grading, 0)
                             ,C0_F = isnull(@PhysicalDamage, 0)
                             ,C0_G = isnull(@Unlocking, 0)
                             ,C0_T = isnull(@Total, 0)
                             ,C0_Date = @Createdate
                              where [Row] = 'd0' and CreateUser = @CreateUser
         Update #TempGridGT set  C0_A = isnull(@Activation, 0)
                             ,C0_B = isnull(@Buffing, 0)
                             ,C0_C = isnull(@FunctionTest, 0)
                             ,C0_D = isnull(@GradeImprovement, 0)
                             ,C0_E = isnull(@Grading, 0)
                             ,C0_F = isnull(@PhysicalDamage, 0)
                             ,C0_G = isnull(@Unlocking, 0)
                             ,C0_T = C0_T + isnull(@Total, 0)
                             ,C_GT = C_GT + isnull(@Total, 0)  where CreateUser = @CreateUser
         Update #TempGridGT set C0_A = isnull(@Activation, 0)
                             ,C0_B = isnull(@Buffing, 0)
                             ,C0_C = isnull(@FunctionTest, 0)
                             ,C0_D = isnull(@GradeImprovement, 0)
                             ,C0_E = isnull(@Grading, 0)
                             ,C0_F = isnull(@PhysicalDamage, 0)
                             ,C0_G = isnull(@Unlocking, 0)
                             ,C0_T = C0_T + isnull(@Total, 0)
                             ,C_GT = C_GT + isnull(@Total, 0)  where CreateUser = 'Grand Total'                            
         end
      if @daysago = 2
         begin
         --Update #TempGrid set C1_A = @Createdate where Row = 'h0'
         Update #TempGrid set C1_A = case when isnull(@Activation, 0) = 0 then 0 else @Activation end
                             ,C1_B = case when isnull(@Buffing, 0) = 0 then 0 else @Buffing end
                             ,C1_C = case when isnull(@FunctionTest, 0) = 0 then 0 else @FunctionTest end
                             ,C1_D = case when isnull(@GradeImprovement, 0) = 0 then 0 else @GradeImprovement end
                             ,C1_E = case when isnull(@Grading, 0) = 0 then 0 else @Grading end
                             ,C1_F = case when isnull(@PhysicalDamage, 0) = 0 then 0 else @PhysicalDamage end
                             ,C1_G = case when isnull(@Unlocking, 0) = 0 then 0 else @Unlocking end
                             ,C1_T = case when isnull(@Total, 0) = 0 then 0 else @Total end 
                             ,C1_Date = @Createdate
                             where [Row] = 'd0' and CreateUser = @CreateUser
         Update #TempGridGT set C1_A = case when isnull(@Activation, 0) = 0 then 0 else @Activation end
                             ,C1_B = case when isnull(@Buffing, 0) = 0 then 0 else @Buffing end
                             ,C1_C = case when isnull(@FunctionTest, 0) = 0 then 0 else @FunctionTest end
                             ,C1_D = case when isnull(@GradeImprovement, 0) = 0 then 0 else @GradeImprovement end
                             ,C1_E = case when isnull(@Grading, 0) = 0 then 0 else @Grading end
                             ,C1_F = case when isnull(@PhysicalDamage, 0) = 0 then 0 else @PhysicalDamage end
                             ,C1_G = case when isnull(@Unlocking, 0) = 0 then 0 else @Unlocking end
                             ,C1_T = C1_A + isnull(@Total, 0)
                             ,C_GT = C_GT + isnull(@Total, 0)  where CreateUser = @CreateUser
         Update #TempGridGT set C1_A = case when isnull(@Activation, 0) = 0 then 0 else @Activation end
                             ,C1_B = case when isnull(@Buffing, 0) = 0 then 0 else @Buffing end
                             ,C1_C = case when isnull(@FunctionTest, 0) = 0 then 0 else @FunctionTest end
                             ,C1_D = case when isnull(@GradeImprovement, 0) = 0 then 0 else @GradeImprovement end
                             ,C1_E = case when isnull(@Grading, 0) = 0 then 0 else @Grading end
                             ,C1_F = case when isnull(@PhysicalDamage, 0) = 0 then 0 else @PhysicalDamage end
                             ,C1_G = case when isnull(@Unlocking, 0) = 0 then 0 else @Unlocking end
                             ,C1_T = C1_T + isnull(@Total, 0)
                             ,C_GT = C_GT + isnull(@Total, 0)  where CreateUser = 'Grand Total'   
         end
      if @daysago = 3
         begin
         --Update #TempGrid set C2_A = @Createdate where Row = 'h0'
         Update #TempGrid set C2_A = case when isnull(@Activation, 0) = 0 then 0 else @Activation end
                             ,C2_B = case when isnull(@Buffing, 0) = 0 then 0 else @Buffing end
                             ,C2_C = case when isnull(@FunctionTest, 0) = 0 then 0 else @FunctionTest end
                             ,C2_D = case when isnull(@GradeImprovement, 0) = 0 then 0 else @GradeImprovement end
                             ,C2_E = case when isnull(@Grading, 0) = 0 then 0 else @Grading end
                             ,C2_F = case when isnull(@PhysicalDamage, 0) = 0 then 0 else @PhysicalDamage end
                             ,C2_G = case when isnull(@Unlocking, 0) = 0 then 0 else @Unlocking end
                             ,C2_T = case when isnull(@Total, 0) = 0 then '0' else @Total end 
                             ,C2_Date = @Createdate
                             where [Row] = 'd0' and CreateUser = @CreateUser
         Update #TempGridGT set C2_A = case when isnull(@Activation, 0) = 0 then 0 else @Activation end
                             ,C2_B = case when isnull(@Buffing, 0) = 0 then 0 else @Buffing end
                             ,C2_C = case when isnull(@FunctionTest, 0) = 0 then 0 else @FunctionTest end
                             ,C2_D = case when isnull(@GradeImprovement, 0) = 0 then 0 else @GradeImprovement end
                             ,C2_E = case when isnull(@Grading, 0) = 0 then 0 else @Grading end
                             ,C2_F = case when isnull(@PhysicalDamage, 0) = 0 then 0 else @PhysicalDamage end
                             ,C2_G = case when isnull(@Unlocking, 0) = 0 then 0 else @Unlocking end
                             ,C2_T = C2_T + isnull(@Total, 0)
                             ,C_GT = C_GT + isnull(@Total, 0)  where CreateUser = @CreateUser
         Update #TempGridGT set C2_A = case when isnull(@Activation, 0) = 0 then 0 else @Activation end
                             ,C2_B = case when isnull(@Buffing, 0) = 0 then 0 else @Buffing end
                             ,C2_C = case when isnull(@FunctionTest, 0) = 0 then 0 else @FunctionTest end
                             ,C2_D = case when isnull(@GradeImprovement, 0) = 0 then 0 else @GradeImprovement end
                             ,C2_E = case when isnull(@Grading, 0) = 0 then 0 else @Grading end
                             ,C2_F = case when isnull(@PhysicalDamage, 0) = 0 then 0 else @PhysicalDamage end
                             ,C2_G = case when isnull(@Unlocking, 0) = 0 then 0 else @Unlocking end
                             ,C2_T = C2_T + isnull(@Total, 0)
                             ,C_GT = C_GT + isnull(@Total, 0)  where CreateUser = 'Grand Total'   
         end
      end
      /*  Now we need to add our grand total */
      Insert #TempGrid ([Row], [CreateUser],[C0_A],[C0_B],[C0_C],[C0_T]
                       ,[C1_A],[C1_B],[C1_C],[C1_T]
                       ,[C2_A],[C2_B],[C2_C],[C2_T] 
                       ,[C_GT], Seq)
      Select 't0', [CreateUser],[C0_A],[C0_B],[C0_C],[C0_T]
                       ,[C1_A],[C1_B],[C1_C],[C1_T]
                       ,[C2_A],[C2_B],[C2_C],[C2_T] 
                       ,[C_GT], 5
          From #TempGridGT where CreateUser = 'Grand Total'  
      -- We need to bring over our User Grand Totals.    
      update #TempGrid set C_GT = CONVERT(nvarchar(50), B.C_GT)
      From #TempGrid A
      Inner join #TempGridGT B on A.CreateUser = B.CreateUser
      ----------------------------------------------------------------

if exists(Select * from #TempUsers)
   begin 
   Select * 
   --INTO QC_01GridValue
   from #TempGrid T
    inner join #TempUsers U on U.UserName = T.CreateUser COLLATE Database_Default 
    Order by Seq, CreateUser
   end
   
if not exists(Select * from #TempUsers)
   begin 
   Select * from #TempGrid T
    Order by Seq, CreateUser
   end  

Drop table #TempUsers
Drop table #TempRoles
Drop table #Temp1
Drop table #TempGrid


END


/****** Object:  StoredProcedure [dbo].[GetDashboardRepair_02_GridValueFiltered]    Script Date: 10/16/2019 11:38:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*

Receiver
Supervisors


exec GetDashboardRepair_01_GridValueFiltered 'MM/DD/YYYY',12, ''
exec GetDashboardRepair_01_GridValueFiltered '',16000, ''
exec GetDashboardRepair_01_GridValueFiltered '10/06/2016',1200, 'Receiver,Supervisors'
exec GetDashboardRepair_01_GridValue '10/06/2016',1200
exec GetDashboardRepair_01_GridValueFiltered '9/16/2016',12, ''
exec GetDashboardRepair_01_GridValueFiltered '',12, ''
exec GetDashboardRepair_01_GridValueFiltered '',12, ''






CREATE TABLE #Temp1(
	[Today] [nvarchar](10) NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[Createdate] [nvarchar](10) NULL,
	[daysago] [int] NULL,
	[Bridge Repair] [int] NULL,
	[MSC Repair Handling] [int] NULL,
	[Product Placement] [int] NULL,
	[Total] [numeric](10, 0) NULL
)

Insert into #Temp1
exec GetDashboardRepair_01 '10/06/2016',12

Select * from #Temp1


*/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GetDashboardRepair_02_GridValueFiltered]
    @Today nvarchar(10),
    @Days int,
    @RoleFilter nvarchar(max)
AS
BEGIN

Set nocount on

Declare @delimiter varchar(20)
Select @delimiter = ','
SELECT convert(numeric(18),0) as Processed, * into #TempRoles FROM fn_SplitDistinct(@RoleFilter, @delimiter)
Update #TempRoles set value = LTRIM(rtrim(Value))
--Select * from #TempRoles
--Select * from fn_SplitDistinct('aaa, aaab,ddddddd', ',')

SELECT Distinct aspnet_Users.UserName
  into #TempUsers
  FROM aspnet_Roles 
 INNER JOIN aspnet_UsersInRoles ON aspnet_Roles.RoleId = aspnet_UsersInRoles.RoleId 
 INNER JOIN aspnet_Users ON aspnet_UsersInRoles.UserId = aspnet_Users.UserId
 Inner join #TempRoles T on T.value = aspnet_Roles.RoleName
--where UserName in ('DCARELL','sandra clause')                      


/*   Get the raw data we will reformat  */
CREATE TABLE #Temp1(
	[Today] [nvarchar](10) NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[Createdate] [nvarchar](10) NULL,
	[daysago] [int] NULL,
	[Bridge Repair] [int] NULL,
	[MSC Repair Handling] [int] NULL,
	[Product Placement] [int] NULL,
	[MSC Return] [int] NULL,
	[Total] [numeric](10, 0) NULL,
	[Processed] [int] NULL
)
Insert into #Temp1
exec GetDashboardRepair_02 @Today,@Days
Update #Temp1 set [Processed] = 0
----------------------------------------------


  --and (ProcessText = 'Bridge Repair'
  --or ProcessText = 'MSC Repair Handling'
  --or ProcessText = 'ProductPlacement')



/*   Setup for reformating the data  */
CREATE TABLE #TempGridGT(
	[CreateUser] [nvarchar](50) NOT NULL,
	[C0_A]  [numeric](10, 0) NULL,
	[C0_B]  [numeric](10, 0) NULL,
	[C0_C]  [numeric](10, 0) NULL,
	[C0_D]  [numeric](10, 0) NULL,
	[C0_T]  [numeric](10, 0) NULL,
	
	[C1_A] [numeric](10, 0) NULL,
	[C1_B] [numeric](10, 0) NULL,
	[C1_C] [numeric](10, 0) NULL,
	[C1_D]  [numeric](10, 0) NULL,
	[C1_T] [numeric](10, 0) NULL,
	
    [C2_A] [numeric](10, 0) NULL,
	[C2_B] [numeric](10, 0) NULL,
	[C2_C] [numeric](10, 0) NULL,
	[C2_D]  [numeric](10, 0) NULL,
	[C2_T] [numeric](10, 0) NULL,
	[C_GT] [numeric](10, 0) NULL
)
CREATE TABLE #TempGrid(
	[CreateUser] [nvarchar](50) NOT NULL,
    [Seq] [int] NULL,
	[Row] [nvarchar](5) NULL,	
	
	[C0_Date] [nvarchar](10) NULL,
	[C0_A] [numeric](10, 0) NULL,
	[C0_B] [numeric](10, 0) NULL,
	[C0_C] [numeric](10, 0) NULL,
	[C0_D]  [numeric](10, 0) NULL,
	[C0_T] [numeric](10, 0) NULL,
	
	[C1_Date] [nvarchar](10) NULL,
	[C1_A] [numeric](10, 0) NULL,
	[C1_B] [numeric](10, 0) NULL,
	[C1_C] [numeric](10, 0) NULL,
	[C1_D]  [numeric](10, 0) NULL,
	[C1_T] [numeric](10, 0) NULL,
	
	[C2_Date] [nvarchar](10) NULL,
    [C2_A] [numeric](10, 0) NULL,
	[C2_B] [numeric](10, 0) NULL,
	[C2_C] [numeric](10, 0) NULL,
	[C2_D]  [numeric](10, 0) NULL,
	[C2_T] [numeric](10, 0) NULL,
	[C_GT] [numeric](10, 0) NULL
)

-- Lay down the first layer.
/*
Insert #TempGrid ([Row], [CreateUser],[C0_A],[C0_B],[C0_C],[C0_T]
                              ,[C1_A],[C1_B],[C1_C],[C1_T]
                              ,[C2_A],[C2_B],[C2_C],[C2_T] 
                              ,[C_GT], Seq)
Values ('h0','','date','','',''        
               ,'date','','',''        
               ,'date','','',''          
               ,'', 1)        
Insert #TempGrid ([Row],[CreateUser],[C0_A],[C0_B],[C0_C],[C0_T]
                              ,[C1_A],[C1_B],[C1_C],[C1_T]
                              ,[C2_A],[C2_B],[C2_C],[C2_T] 
                              ,[C_GT], Seq)
Values ('h1','Rep','Bridge Repair','MSC Repair handline','Product Placement','total'   
,'Bridge Repair','MSC Repair handline','Product Placement','total'   
,'Bridge Repair','MSC Repair handline','Product Placement','total'     
,'grand total', 2)   
Insert #TempGridGT ([CreateUser],[C0_A],[C0_B],[C0_C],[C0_T]
                    ,[C1_A],[C1_B],[C1_C],[C1_T]
                    ,[C2_A],[C2_B],[C2_C],[C2_T] 
                    ,[C_GT])
Values ('Grand Total',0,0,0,0        
                   ,0,0,0,0
                   ,0,0,0,0       
                   ,0)  
*/

-- Our data is telling us the number of days since the "today" date.
--     depending on the work schedule, some days may not be present.
--     Our final grid carries three days of column data.
--     We would rather "squish out" those absend days.
--     This does it.
--     The Days since original number is lost.


Create Table #Renum(
   [NewNumb] [int] IDENTITY(1,1) NOT NULL,
   [daysago] [int])
Insert #Renum (daysago)
Select distinct daysago from #Temp1 order by daysago
Update #Temp1 set daysago = R.NewNumb from #Temp1 T inner join #Renum R on T.daysago = R.daysago
       -- Because we only want to show the users with transactions in the last 3 cycles.
       Delete #Temp1 where daysago > 3
Drop Table #Renum
---------------------------------------------------------------------------------

Declare @CreateUser [nvarchar](50),
	    @Createdate [nvarchar](10),
	    @daysago [int],
	    @BridgeRepair [int],
	    @MSCRepairHandling [int],
	    @ProductPlacement [int],
	    @MSCReturns [int],
	    @Total Numeric(10,0),
	    @C int
	    
Select @C = 0		    

while exists (Select * from #Temp1 where [Processed] = 0 and @C < 10000000)
      begin
      Select @C = @C + 1
      Select Top 1 @CreateUser = Createuser,
                   @Createdate = Createdate,
                   @daysago = daysago,
                   @BridgeRepair = [Bridge Repair],
                   @MSCRepairHandling = [MSC Repair Handling],
                   @ProductPlacement = [Product Placement],
                   @MSCReturns = [MSC Return],
                   @Total = [Total]
            from #Temp1 where Processed = 0
      update #Temp1 set processed = 1 where CreateUser = @Createuser and Createdate = @Createdate 
      
      Select @Total = ISNULL(@BridgeRepair, 0) + ISNULL(@MSCRepairHandling, 0) +  ISNULL(@ProductPlacement, 0) +  ISNULL(@MSCReturns, 0)
                   
      -- Add the record if not found already in Grid Section             
      if not exists(Select * from #TempGrid where CreateUser = @CreateUser)
         begin
         Insert #TempGrid ([Row], [CreateUser],[C0_A],[C0_B],[C0_C],[C0_D],[C0_T]
                             ,[C1_A],[C1_B],[C1_C],[C1_D],[C1_T]
                             ,[C2_A],[C2_B],[C2_C],[C2_D],[C2_T] 
                             ,[C_GT], Seq)
         Values ('d0',@CreateUser,0,0,0,0,0       
                                 ,0,0,0,0,0      
                                 ,0,0,0,0,0          
                                 ,0,3)      
         Insert #TempGridGT ([CreateUser],[C0_A],[C0_B],[C0_C],[C0_D],[C0_T]
                             ,[C1_A],[C1_B],[C1_C],[C1_D],[C1_T]
                             ,[C2_A],[C2_B],[C2_C],[C2_D],[C2_T] 
                             ,[C_GT])
         Values (@CreateUser,0,0,0,0,0           
                            ,0,0,0,0,0   
                            ,0,0,0,0,0     
                            ,0)                                  
         end
      
      if @daysago = 1
         begin
         --Update #TempGrid set C0_A = @Createdate where Row = 'h0'
         Update #TempGrid set C0_A = isnull(@BridgeRepair, 0)
                             ,C0_B = isnull(@MSCRepairHandling, 0)
                             ,C0_C = isnull(@ProductPlacement, 0)
                             ,C0_D = isnull(@MSCReturns, 0)
                             ,C0_T = isnull(@Total, 0)
                             ,C0_Date = @Createdate
                              where [Row] = 'd0' and CreateUser = @CreateUser
         Update #TempGridGT set C0_A = C0_A + isnull(@BridgeRepair, 0)
                             ,C0_B = C0_B + isnull(@MSCRepairHandling, 0)
                             ,C0_C = C0_C + isnull(@ProductPlacement, 0)
                             ,C0_D = C0_D + isnull(@MSCReturns, 0)
                             ,C0_T = C0_T + isnull(@Total, 0)
                             ,C_GT = C_GT + isnull(@Total, 0)  where CreateUser = @CreateUser
         Update #TempGridGT set C0_A = C0_A + isnull(@BridgeRepair, 0)
                             ,C0_B = C0_B + isnull(@MSCRepairHandling, 0)
                             ,C0_C = C0_C + isnull(@ProductPlacement, 0)
                             ,C0_D = C0_D + isnull(@MSCReturns, 0)
                             ,C0_T = C0_T + isnull(@Total, 0)
                             ,C_GT = C_GT + isnull(@Total, 0)  where CreateUser = 'Grand Total'                            
         end
      if @daysago = 2
         begin
         --Update #TempGrid set C1_A = @Createdate where Row = 'h0'
         Update #TempGrid set C1_A = case when isnull(@BridgeRepair, 0) = 0 then 0 else @BridgeRepair end
                             ,C1_B = case when isnull(@MSCRepairHandling, 0) = 0 then 0 else @MSCRepairHandling end
                             ,C1_C = case when isnull(@ProductPlacement, 0) = 0 then 0 else @ProductPlacement end
                             ,C1_D = case when isnull(@MSCReturns, 0) = 0 then 0 else @MSCReturns end
                             ,C1_T = case when isnull(@Total, 0) = 0 then 0 else @Total end 
                             ,C1_Date = @Createdate
                             where [Row] = 'd0' and CreateUser = @CreateUser
         Update #TempGridGT set C1_A = C1_A + isnull(@BridgeRepair, 0)
                             ,C1_B = C1_B + isnull(@MSCRepairHandling, 0)
                             ,C1_C = C1_C + isnull(@ProductPlacement, 0)
                             ,C1_D = C1_D + isnull(@MSCReturns, 0)
                             ,C0_T = C0_A + isnull(@Total, 0)
                             ,C_GT = C_GT + isnull(@Total, 0)  where CreateUser = @CreateUser
         Update #TempGridGT set C1_A = C1_A + isnull(@BridgeRepair, 0)
                             ,C1_B = C1_B + isnull(@MSCRepairHandling, 0)
                             ,C1_C = C1_C + isnull(@ProductPlacement, 0)
                             ,C1_D = C1_D + isnull(@MSCReturns, 0)
                             ,C1_T = C1_T + isnull(@Total, 0)
                             ,C_GT = C_GT + isnull(@Total, 0)  where CreateUser = 'Grand Total'   
         end
      if @daysago = 3
         begin
         --Update #TempGrid set C2_A = @Createdate where Row = 'h0'
         Update #TempGrid set C2_A = case when isnull(@BridgeRepair, 0) = 0 then 0 else @BridgeRepair end
                             ,C2_B = case when isnull(@MSCRepairHandling, 0) = 0 then 0 else @MSCRepairHandling end
                             ,C2_C = case when isnull(@ProductPlacement, 0) = 0 then 0 else @ProductPlacement end
                             ,C2_D = case when isnull(@MSCReturns, 0) = 0 then 0 else @MSCReturns end
                             ,C2_T = case when isnull(@Total, 0) = 0 then '0' else @Total end 
                             ,C2_Date = @Createdate
                             where [Row] = 'd0' and CreateUser = @CreateUser
         Update #TempGridGT set C2_A = C2_A + isnull(@BridgeRepair, 0)
                             ,C2_B = C2_B + isnull(@MSCRepairHandling, 0)
                             ,C2_C = C2_C + isnull(@ProductPlacement, 0)
                             ,C2_D = C2_D + isnull(@MSCReturns, 0)
                             ,C2_T = C2_T + isnull(@Total, 0)
                             ,C_GT = C_GT + isnull(@Total, 0)  where CreateUser = @CreateUser
         Update #TempGridGT set C2_A = C2_A + isnull(@BridgeRepair, 0)
                             ,C2_B = C2_B + isnull(@MSCRepairHandling, 0)
                             ,C2_C = C2_C + isnull(@ProductPlacement, 0)
                             ,C2_D = C2_D + isnull(@MSCReturns, 0)
                             ,C2_T = C2_T + isnull(@Total, 0)
                             ,C_GT = C_GT + isnull(@Total, 0)  where CreateUser = 'Grand Total'   
         end
      end
      /*  Now we need to add our grand total */
      Insert #TempGrid ([Row], [CreateUser],[C0_A],[C0_B],[C0_C],[C0_D],[C0_T]
                       ,[C1_A],[C1_B],[C1_C],[C1_D],[C1_T]
                       ,[C2_A],[C2_B],[C2_C],[C2_D],[C2_T] 
                       ,[C_GT], Seq)
      Select 't0', [CreateUser],[C0_A],[C0_B],[C0_C],[C0_D],[C0_T]
                       ,[C1_A],[C1_B],[C1_C],[C1_D],[C1_T]
                       ,[C2_A],[C2_B],[C2_C],[C2_D],[C2_T] 
                       ,[C_GT], 5
          From #TempGridGT where CreateUser = 'Grand Total'  
      -- We need to bring over our User Grand Totals.    
      update #TempGrid set C_GT = CONVERT(nvarchar(50), B.C_GT)
      From #TempGrid A
      Inner join #TempGridGT B on A.CreateUser = B.CreateUser
      ----------------------------------------------------------------

if exists(Select * from #TempUsers)
   begin 
   Select * from #TempGrid T
    inner join #TempUsers U on U.UserName = T.CreateUser COLLATE Database_Default 
    Order by Seq, CreateUser
   end
   
if not exists(Select * from #TempUsers)
   begin 
   Select * from #TempGrid T
    Order by Seq, CreateUser
   end  

Drop table #TempUsers
Drop table #TempRoles
Drop table #Temp1
Drop table #TempGrid


END



















