
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

