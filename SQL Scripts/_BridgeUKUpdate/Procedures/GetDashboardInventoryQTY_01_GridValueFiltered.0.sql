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


