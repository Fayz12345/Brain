/****** Object:  UserDefinedFunction [dbo].[IsOrderESNMatched]    Script Date: 05/19/2020 12:55:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*

("O1361", " O1438 O6525", "")'


Declare @KeyList nvarchar(500)
Declare @ReturnKeyList nvarchar(500)
Declare @AttributeToAdd nvarchar(20)

Select @AttributeToAdd = 'O1552'
Select @KeyList = 'O1361 O1438 O1664 O1446 O1935'


Select @AttributeToAdd = 'O1438'
Select @KeyList = 'O1361 O1438 O6525'

Select @ReturnKeyList = ''
Exec AddAttributeToDetailString @AttributeToAdd, @KeyList, 0, @ReturnKeyList Output

Print @AttributeToAdd
Print @KeyList
Print @ReturnKeyList

Select * from OrderDetail
Select * from Question where QUestionID in (210,243,244,214,226)
Select * from [Option] where QUestionID = 214 -- Colour
O1446
O1447
O1448
O1550
O1551
O1552
O1553

*/

Alter PROCEDURE [dbo].[AddAttributeToDetailString](@AttributeToAdd nvarchar(20),@KeyList nvarchar(500), @isDelete bit, @ReturnKeyList nVarchar(500) Output)

AS
BEGIN
Set NOCOUNT ON

declare @Delim varchar(20)
Select @ReturnKeyList = @KeyList;
Select @Delim = ' '
Select @KeyList = LTRIM(RTrim(@KeyList))

Declare @OptionID numeric(18)
Declare @QuestionID numeric(18)
Select @OptionID = OptionID, @QuestionID = QuestionID from [Option] where ScanKey = @AttributeToAdd
Select @OptionID = isnull(@OptionID, -1)
if @OptionID < 1
   begin
   return 0 
   end

Select CONVERT(numeric(18,0),0) as QuestionID, CONVERT(numeric(18,0),0) as OptionID, CONVERT(numeric(18,0),0) as processed, * into #Tempx from dbo.fn_SplitDistinct(@KeyList,@Delim) 
Update #Tempx set OptionID = o.OptionID, QuestionID = o.QuestionID from [Option] o inner join #Tempx X on X.value = o.ScanKey

--Select * from #Tempx

Update #Tempx set processed = 1 where QuestionID = @QuestionID
if @isDelete = 1          -- Delete = 0 = true
   begin
   Insert #Tempx (QuestionID, OptionID, Processed, value) Values (@QuestionID, @OptionID, 0, @AttributeToAdd)
   end

Select @ReturnKeyList = ''
Select @ReturnKeyList = @ReturnKeyList + convert(nvarchar(20), value) + ' ' from #Tempx
inner join Question Q on #Tempx.QuestionID = Q.QuestionID
where processed = 0
order by Q.Sequence, Q.Name

--Select * from #Tempx

Return 0

END

