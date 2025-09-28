/****** Object:  StoredProcedure [dbo].[DoesAttrMatch_ScanKey]    Script Date: 06/11/2018 15:59:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
 
Declare @NumberQuestions  numeric(18)
Declare @NumberOptions  numeric(18)
Declare @NumberMatches  numeric(18)
Declare @PassIsZero  numeric(18)
Declare @Matches nvarchar(500)
Declare @Misses nvarchar(500) 
exec dbo.DoesAttrMatch_ScanKey 'O1361 O1437 O2565 O1446 O1403', 599, @NumberQuestions OUTPUT, @NumberOptions OUTPUT, @NumberMatches OUTPUT, @PassIsZero OUTPUT, @Matches OUTPUT, @Misses OUTPUT
Print 'Pass is Zero:' + convert(nvarchar(10),@PassIsZero)
Print '#Questions:' + convert(nvarchar(10),@NumberQuestions)
Print '#Options:' + convert(nvarchar(10),@NumberOptions)
Print '#Matches:' + convert(nvarchar(10),@NumberMatches)
Print 'Matches:' + @Matches
Print 'Misses:' + @Misses



Declare @NumberQuestions  numeric(18)
Declare @NumberOptions  numeric(18)
Declare @NumberMatches  numeric(18)
Declare @PassIsZero  numeric(18)
Declare @Matches nvarchar(500)
Declare @Misses nvarchar(500) 
exec dbo.DoesAttrMatch_ScanKey 'O1437 O1403', 599, @NumberQuestions OUTPUT, @NumberOptions OUTPUT, @NumberMatches OUTPUT, @PassIsZero OUTPUT, @Matches OUTPUT, @Misses OUTPUT
Print 'Pass is Zero:' + convert(nvarchar(10),@PassIsZero)
Print '#Questions:' + convert(nvarchar(10),@NumberQuestions)
Print '#Options:' + convert(nvarchar(10),@NumberOptions)
Print '#Matches:' + convert(nvarchar(10),@NumberMatches)
Print 'Matches:' + @Matches
Print 'Misses:' + @Misses



Select * from ReceiveDetail where ReceiveDetailID = '599'


Declare @PassIsOne  numeric(18)
exec dbo.DoesAttrMatch_ScanKey 'O1437 O1403', 599, @PassIsOne OUTPUT
Print @PassIsOne


*/

Create procedure [dbo].[DoesAttrMatch_ScanKey](@KeyList nvarchar(500),
                 @ReceiveDetailID numeric(18),
                 @NumberQuestions numeric(18) OUTPUT,
                 @NumberOptions numeric(18) OUTPUT,
                 @NumberMatches numeric(18) OUTPUT,
                 @PassIsZero numeric(18) OUTPUT,
                 @Matches nvarchar(500) OUTPUT,
                 @Misses nvarchar(500) OUTPUT
                 )
AS
BEGIN
set Nocount on

declare @Delim varchar(20)
Select @Delim = ' '


Select @PassIsZero = 0
Select @Matches = ''
Select @Misses = ''

Select @KeyList = LTRIM(RTrim(@KeyList))

Select CONVERT(numeric(18,0),0) as QuestionID, CONVERT(numeric(18,0),0) as OptionID, CONVERT(numeric(18,0),0) as processed, * into #Tempx from dbo.fn_SplitDistinct(@KeyList,@Delim) 


Update #Tempx set OptionID = o.OptionID, QuestionID = o.QuestionID from [Option] o inner join #Tempx X on X.value = o.ScanKey

Select @NumberQuestions = COUNT(distinct QuestionID) from #Tempx where OptionID > 0
Select @NumberOptions = COUNT(distinct OptionID) from #Tempx where OptionID > 0
--print 'Attribute Count: number of questions that need to be answered' + Convert(nvarchar(20), @Attributes)

Update #Tempx set processed = 1 
  From #Tempx T
 Inner join ReceiveDetailItem I on T.OptionID = I.OptionID
 Where I.ReceiveDetailID = @ReceiveDetailID
 
Select @NumberMatches = Count(OptionID) From #Tempx where processed = 1
Select @Matches = @Matches + case when LEN(@Matches) = 0 then '' else ' ' end + #Tempx.value From #Tempx where processed = 1
Select @Misses = @Misses + case when LEN(@Misses) = 0 then '' else ' ' end + #Tempx.value From #Tempx where processed != 1

Select @PassIsZero = abs(round(@NumberQuestions - @NumberMatches,0))


Drop Table #Tempx
return

END
Go
