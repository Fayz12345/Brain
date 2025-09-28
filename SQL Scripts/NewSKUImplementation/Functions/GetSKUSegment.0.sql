
/****** Object:  UserDefinedFunction [dbo].[GetSKUSegment]    Script Date: 04/26/2017 11:26:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

/*

Print dbo.GetSKUSegment(2892,'Unlocking Receive',3)
Print dbo.GetSKUSegment(2892,'Carrier',3,'y')
Print dbo.GetSKUSegment(2892,'Manufacturer',9,'y')
Print dbo.GetSKUSegment(2892,'Carrier',3)
Print dbo.GetSKUSegment(2892,'Carrier',3)
Select * from ReceiveDetail 
2892
2893
2894
2895
2896
2897
2898
2899
2900
2901
2902
2903
2904
2905
2906
2907
2908
2909

*/

Create FUNCTION [dbo].[GetSKUSegment](@mReceiveDetailID numeric(18), @mQuestionName nvarchar(20), @PadLength int, @Default nvarchar(10))
RETURNS nvarchar(10)
AS
BEGIN
Declare @mReturnValue nvarchar(50)
--Select @mQuestionName = 'Unlocking Receive'
--Declare @mCarrierSegment nvarchar(20)
--Declare @mUnlockSegment nvarchar(20)

Select @mReturnValue = [Option].Name
       FROM ReceiveDetailItem 
               INNER JOIN [Option] ON ReceiveDetailItem.OptionID = [Option].OptionID AND ReceiveDetailItem.OptionID = [Option].OptionID 
               INNER JOIN Question ON [Option].QuestionID = Question.QuestionID
               WHERE (ReceiveDetailItem.ReceiveDetailID = @mReceiveDetailID) AND ((Question.Name = @mQuestionName))

Select @mReturnValue = ISNULL(@mReturnValue,replicate(@Default,@PadLength))
if (LEN(@mReturnValue) != @PadLength)
    begin
    select @mReturnValue = RIGHT(replicate(@Default,@PadLength) + @mReturnValue, @PadLength)
    end

Return @mReturnValue

END

