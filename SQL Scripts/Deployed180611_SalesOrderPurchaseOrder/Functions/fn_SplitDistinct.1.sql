/****** Object:  UserDefinedFunction [dbo].[fn_SplitDistinct]    Script Date: 06/11/2018 16:01:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
SELECT value as Address into #TempBranchAddress FROM fn_Split(@mBranchAddressString, ';')
*/

ALTER  FUNCTION [dbo].[fn_SplitDistinct](@text varchar(8000), @delimiter varchar(20) = ' ')
RETURNS @Strings TABLE (value varchar(100))

AS

BEGIN

DECLARE @index int 
SET @index = -1 
Select @text = LTRIM(rtrim(@text))
WHILE (LEN(@text) > 0) 
BEGIN  
    SET @index = CHARINDEX(@delimiter , @text)  
    --
    --print 'Index:' + convert(nvarchar(10), @index)
    IF (@index = 0) AND (LEN(@text) > 0)  
      BEGIN   
      if not exists (Select Value from @Strings where Value = @Text)
         INSERT INTO @Strings VALUES (@text)
      BREAK  
      END  

    IF (@index > 1)  
      BEGIN   
      if not exists (Select Value from @Strings where Value = LEFT(@text, @index - 1))
         INSERT INTO @Strings VALUES (LEFT(@text, @index - 1))   
      SET @text = RIGHT(@text, (LEN(@text) - @index))  
      END  
    ELSE 
      SET @text = RIGHT(@text, (LEN(@text) - @index)) 
END
RETURN

END
Go
