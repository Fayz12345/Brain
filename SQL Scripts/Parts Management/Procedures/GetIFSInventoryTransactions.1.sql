
/****** Object:  StoredProcedure [dbo].[GetIFSInventoryTransactions]    Script Date: 05/27/2015 15:58:42 ******/
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

exec GetIFSInventoryTransactions 'ESN000001', 'Batch', StartDate, 'EndDate', 'IncludeDone'

exec GetIFSInventoryTransactions 'ESN000001', '', '', '','N'
exec GetIFSInventoryTransactions 'ESN000001', '', '', '','Y'
exec GetIFSInventoryTransactions '', '9', '', '','Y'


*/

ALTER PROCEDURE [dbo].[GetIFSInventoryTransactions]
      @ESN nvarchar(50),
      @Batch nvarchar(10),
      @StartDate nvarchar(10),
      @EndDate nvarchar(10),
      @IncludeDone nchar(1),
      @ShowDetail nchar(1)      

AS
BEGIN
	SET NOCOUNT ON;


if len(@Batch) > 0 
   begin
   if @ShowDetail = 'Y'
      select * from vwIFS_InvtTran where Retrievedbatch = @Batch
   else
       SELECT Directive, SUM(isnull(Quantity,1)) AS Quantity, POVendor, PONumber, POReceiptDate, POLine, POCost, IFSSite, IFSProject, FromSku, FromLocation, FromCondition, ToSku, 
              ToLocation, ToCondition, RetrievedBatch, RetrievedDate
         FROM vwIFS_InvtTran
        where Retrievedbatch = @Batch
        GROUP BY Directive, POVendor, PONumber, POReceiptDate, POLine, POCost, IFSSite, IFSProject, FromSku, FromLocation, FromCondition, ToSku, ToLocation, ToCondition, RetrievedBatch, RetrievedDate
   
   return
   end
--------------------------------------------------------------------------------   
if @IncludeDone = 'N'
    begin
    if len(@ESN) > 0
       begin
       if @ShowDetail = 'Y'
          select * from vwIFS_InvtTran where ESN = @ESN and RetrievedDate is null
       else
           SELECT Directive, SUM(isnull(Quantity,1)) AS Quantity, POVendor, PONumber, POReceiptDate, POLine, POCost, IFSSite, IFSProject, FromSku, FromLocation, FromCondition, ToSku, 
                  ToLocation, ToCondition, RetrievedBatch, RetrievedDate
             FROM vwIFS_InvtTran
            where ESN = @ESN and RetrievedDate is null         
            GROUP BY Directive, POVendor, PONumber, POReceiptDate, POLine, POCost, IFSSite, IFSProject, FromSku, FromLocation, FromCondition, ToSku, ToLocation, ToCondition, RetrievedBatch, RetrievedDate
       return
       end
    --------------------------------------------------------------------------------   
    if len(@ESN) > 0
       begin
       if @ShowDetail = 'Y'
          select * from vwIFS_InvtTran where ESN = @ESN and CreatedDate >= @StartDate and CreatedDate <= @EndDate and RetrievedDate is null
       else
           SELECT Directive, SUM(isnull(Quantity,1)) AS Quantity, POVendor, PONumber, POReceiptDate, POLine, POCost, IFSSite, IFSProject, FromSku, FromLocation, FromCondition, ToSku, 
                  ToLocation, ToCondition, RetrievedBatch, RetrievedDate
             FROM vwIFS_InvtTran
            where ESN = @ESN and CreatedDate >= @StartDate and CreatedDate <= @EndDate and RetrievedDate is null
            GROUP BY Directive, POVendor, PONumber, POReceiptDate, POLine, POCost, IFSSite, IFSProject, FromSku, FromLocation, FromCondition, ToSku, ToLocation, ToCondition, RetrievedBatch, RetrievedDate
       return
       end
    --------------------------------------------------------------------------------   
    if len(@ESN) = 0
       begin
          if @ShowDetail = 'Y'
             select * from vwIFS_InvtTran Where CreatedDate >= @StartDate and CreatedDate <= @EndDate and RetrievedDate is null
       else
           SELECT Directive, SUM(isnull(Quantity,1)) AS Quantity, POVendor, PONumber, POReceiptDate, POLine, POCost, IFSSite, IFSProject, FromSku, FromLocation, FromCondition, ToSku, 
                  ToLocation, ToCondition, RetrievedBatch, RetrievedDate
             FROM vwIFS_InvtTran
            Where CreatedDate >= @StartDate and CreatedDate <= @EndDate and RetrievedDate is null
            GROUP BY Directive, POVendor, PONumber, POReceiptDate, POLine, POCost, IFSSite, IFSProject, FromSku, FromLocation, FromCondition, ToSku, ToLocation, ToCondition, RetrievedBatch, RetrievedDate
       return
       end 
    --------------------------------------------------------------------------------   
    if @ShowDetail = 'Y'
       select * from vwIFS_InvtTran Where CreatedDate >= @StartDate and CreatedDate <= @EndDate and RetrievedDate is null
    else
       SELECT Directive, SUM(isnull(Quantity,1)) AS Quantity, POVendor, PONumber, POReceiptDate, POLine, POCost, IFSSite, IFSProject, FromSku, FromLocation, FromCondition, ToSku, 
              ToLocation, ToCondition, RetrievedBatch, RetrievedDate
         FROM vwIFS_InvtTran
        Where CreatedDate >= @StartDate and CreatedDate <= @EndDate and RetrievedDate is null
        GROUP BY Directive, POVendor, PONumber, POReceiptDate, POLine, POCost, IFSSite, IFSProject, FromSku, FromLocation, FromCondition, ToSku, ToLocation, ToCondition, RetrievedBatch, RetrievedDate
    return
   end

if @IncludeDone != 'N'
    begin
    if len(@ESN) > 0
       begin
       if @ShowDetail = 'Y'
          select * from vwIFS_InvtTran where ESN = @ESN
       else
           SELECT Directive, SUM(isnull(Quantity,1)) AS Quantity, POVendor, PONumber, POReceiptDate, POLine, POCost, IFSSite, IFSProject, FromSku, FromLocation, FromCondition, ToSku, 
                  ToLocation, ToCondition, RetrievedBatch, RetrievedDate
             FROM vwIFS_InvtTran
            where ESN = @ESN      
            GROUP BY Directive, POVendor, PONumber, POReceiptDate, POLine, POCost, IFSSite, IFSProject, FromSku, FromLocation, FromCondition, ToSku, ToLocation, ToCondition, RetrievedBatch, RetrievedDate
       return
       end
    --------------------------------------------------------------------------------   
    if len(@ESN) > 0
       begin
       if @ShowDetail = 'Y'
          select * from vwIFS_InvtTran where ESN = @ESN and CreatedDate >= @StartDate and CreatedDate <= @EndDate
       else
           SELECT Directive, SUM(isnull(Quantity,1)) AS Quantity, POVendor, PONumber, POReceiptDate, POLine, POCost, IFSSite, IFSProject, FromSku, FromLocation, FromCondition, ToSku, 
                  ToLocation, ToCondition, RetrievedBatch, RetrievedDate
             FROM vwIFS_InvtTran
            where ESN = @ESN and CreatedDate >= @StartDate and CreatedDate <= @EndDate
            GROUP BY Directive, POVendor, PONumber, POReceiptDate, POLine, POCost, IFSSite, IFSProject, FromSku, FromLocation, FromCondition, ToSku, ToLocation, ToCondition, RetrievedBatch, RetrievedDate
       return
       end
    --------------------------------------------------------------------------------   
    if len(@ESN) = 0
       begin
          if @ShowDetail = 'Y'
             select * from vwIFS_InvtTran Where CreatedDate >= @StartDate and CreatedDate <= @EndDate
       else
           SELECT Directive, SUM(isnull(Quantity,1)) AS Quantity, POVendor, PONumber, POReceiptDate, POLine, POCost, IFSSite, IFSProject, FromSku, FromLocation, FromCondition, ToSku, 
                  ToLocation, ToCondition, RetrievedBatch, RetrievedDate
             FROM vwIFS_InvtTran
            Where CreatedDate >= @StartDate and CreatedDate <= @EndDate
            GROUP BY Directive, POVendor, PONumber, POReceiptDate, POLine, POCost, IFSSite, IFSProject, FromSku, FromLocation, FromCondition, ToSku, ToLocation, ToCondition, RetrievedBatch, RetrievedDate
       return
       end 
    --------------------------------------------------------------------------------   
    if @ShowDetail = 'Y'
       select * from vwIFS_InvtTran Where CreatedDate >= @StartDate and CreatedDate <= @EndDate
    else
       SELECT Directive, SUM(isnull(Quantity,1)) AS Quantity, POVendor, PONumber, POReceiptDate, POLine, POCost, IFSSite, IFSProject, FromSku, FromLocation, FromCondition, ToSku, 
              ToLocation, ToCondition, RetrievedBatch, RetrievedDate
         FROM vwIFS_InvtTran
        Where CreatedDate >= @StartDate and CreatedDate <= @EndDate
        GROUP BY Directive, POVendor, PONumber, POReceiptDate, POLine, POCost, IFSSite, IFSProject, FromSku, FromLocation, FromCondition, ToSku, ToLocation, ToCondition, RetrievedBatch, RetrievedDate
    return
   end

   
END

