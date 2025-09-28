
/****** Object:  UserDefinedFunction [dbo].[GetIFSLocation_B]    Script Date: 01/27/2019 14:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*

Print dbo.GetSKU(6,5,9,3)

Select SKU, dbo.GetSKU(CarrierID, ManufacturerID, ModelID, ColourID) from ReceiveDetail

Select * from ClientLocation where ClientID = 57

Select * from Process order by Name

*/

ALTER FUNCTION [dbo].[GetIFSLocation_B](@mReceiveDetailID numeric(18), @mProcess nvarchar(25), @mClientLocationID numeric(18), @mCurrentIFSLocation nvarchar(50))
RETURNS nvarchar(50)
AS
BEGIN
Declare @mClientID numeric(18)
Declare @mLocation nvarchar(50)
Select @mProcess = upper(@mProcess)
Select @mLocation = isnull(@mCurrentIFSLocation,'')
Declare @IFSSite nvarchar(5)
Select @mClientID = ClientID, @IFSSite = IFSSite from ClientLocation where ClientLocationID = @mClientLocationID

if (left(@mProcess,7) = 'RECEIVE')
    begin
    -- We want to keep the Location that was set when the MSC was created/Received.
    if exists(Select * from ReceiveDetail where ReceiveDetailID = @mReceiveDetailID and ProjectName = 'xxx') and LEN(ltrim(rtrim(@mCurrentIFSLocation))) > 0
       return @mCurrentIFSLocation
    if (@IFSSite != 'xxx') Select @mLocation = 'QCS-001-001-001' else Select @mLocation = 'QCS-001-001-001' 
    end
    
    
if (left(@mProcess,7) = 'RECEIVE PO')
    begin
    -- We want to keep the Location that was set when the MSC was created/Received.
    if exists(Select * from ReceiveDetail where ReceiveDetailID = @mReceiveDetailID and ProjectName = 'xxx') and LEN(ltrim(rtrim(@mCurrentIFSLocation))) > 0
       return @mCurrentIFSLocation
    if (@IFSSite != 'xxx') Select @mLocation = 'QCS-001-001-001' else Select @mLocation = 'QCS-001-001-001' 
    end    
    
if (@mProcess = 'FIRST FT')
    begin
    if Exists (SELECT * FROM Question AS q INNER JOIN [Option] AS o ON q.QuestionID = o.QuestionID INNER JOIN ReceiveDetailItem ON o.OptionID = ReceiveDetailItem.OptionID
           WHERE  REceiveDetailItem.ReceiveDetailID = @mReceiveDetailID 
             and (q.Name = 'Conditions') 
             AND (o.OptionText = 'FNC') 
             AND (ReceiveDetailItem.Value = N'1'))
       begin    
       if (@IFSSite != 'xxx') Select @mLocation = 'PRD-001-001-001' else Select @mLocation = 'PRD-001-001-001' 
       end
    if Exists (SELECT * FROM Question AS q INNER JOIN [Option] AS o ON q.QuestionID = o.QuestionID INNER JOIN ReceiveDetailItem ON o.OptionID = ReceiveDetailItem.OptionID
           WHERE  REceiveDetailItem.ReceiveDetailID = @mReceiveDetailID 
             and (q.Name = 'Conditions') 
             AND (o.OptionText = 'DEF') 
             AND (ReceiveDetailItem.Value = N'1'))
       begin    
       if (@IFSSite != 'xxx') Select @mLocation = 'RPS-001-001-001' else Select @mLocation = 'RPS-001-001-001' 
       end
    if Exists (SELECT * FROM Question AS q INNER JOIN [Option] AS o ON q.QuestionID = o.QuestionID INNER JOIN ReceiveDetailItem ON o.OptionID = ReceiveDetailItem.OptionID
           WHERE  REceiveDetailItem.ReceiveDetailID = @mReceiveDetailID 
             and (q.Name = 'Conditions') 
             AND (o.OptionText = 'NYT') 
             AND (ReceiveDetailItem.Value = N'1'))
       begin    
       if (@IFSSite != 'xxx') Select @mLocation = 'QCS-001-001-001' else Select @mLocation = 'QCS-001-001-001' 
       end
    end    
if (@mProcess = 'LAB RECEIVE')
    begin
    if (@IFSSite != 'xxx') Select @mLocation = 'QCS-001-001-001' else Select @mLocation = 'QCS-001-001-001' 
    end  

--if (@mProcess = 'Tech Receive')
--    begin
--    if (@mClientID = 57) Select @mLocation = 'RP2-001-001-001' else Select @mLocation = 'RP1-001-001-001' 
--    end  
    
--Select * from Process order by Name

if (@mProcess = 'Lab Receive' or @mProcess = 'Tech Finished' or @mProcess = 'QC Assessment' or @mProcess = 'Error Reporting' or @mProcess = 'Hold Status' or @mProcess = 'Secondary Inspection' or @mProcess = 'Function CnC' or @mProcess = 'RF Test')
    begin
    if Exists (SELECT * FROM Question AS q INNER JOIN [Option] AS o ON q.QuestionID = o.QuestionID INNER JOIN ReceiveDetailItem ON o.OptionID = ReceiveDetailItem.OptionID
           WHERE  REceiveDetailItem.ReceiveDetailID = @mReceiveDetailID and (q.Name = 'Lab Destination') AND (o.OptionText = 'QC Only') AND (ReceiveDetailItem.Value = N'1'))
       begin    
       if (@IFSSite != 'xxx') Select @mLocation = 'QCS-001-001-001' else Select @mLocation = 'QCS-001-001-001' 
       end
    end

if (@mProcess = 'QC Assessment')
    begin
    if Exists (SELECT * FROM Question AS q INNER JOIN [Option] AS o ON q.QuestionID = o.QuestionID INNER JOIN ReceiveDetailItem ON o.OptionID = ReceiveDetailItem.OptionID
           WHERE  REceiveDetailItem.ReceiveDetailID = @mReceiveDetailID and (q.Name = 'Lab Destination') AND (o.OptionText = 'Repair') AND (ReceiveDetailItem.Value = N'1'))
       begin    
       if (@IFSSite != 'xxx') Select @mLocation = 'QCS-001-001-001' else Select @mLocation = 'QCS-001-001-001' 
       end
    end
    
if (left(@mProcess,8) = 'SHIPPING')
    begin
    if (@IFSSite != 'xxx') Select @mLocation = '' else Select @mLocation = '' 
    end  
    

Return @mLocation
END

