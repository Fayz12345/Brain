/****** Object:  UserDefinedFunction [dbo].[fn_Split]    Script Date: 05/17/2017 09:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER  FUNCTION [dbo].[fn_Split](@text varchar(8000), @delimiter varchar(20) = ' ')

RETURNS @Strings TABLE

(    

  position int IDENTITY PRIMARY KEY,

  value varchar(8000)   

)

AS

BEGIN

 

DECLARE @index int 

SET @index = -1 
Declare @rChar char(1)
Select @rChar = '`'
SELECT @text = REPLACE(@text, ' ', @rChar);

 

WHILE (LEN(@text) > 0) 

  BEGIN  

    SET @index = CHARINDEX(@delimiter , @text)  

    IF (@index = 0) AND (LEN(@text) > 0)  

      BEGIN   

        INSERT INTO @Strings VALUES (@text)

          BREAK  

      END  

    IF (@index > 1)  

      BEGIN   

        INSERT INTO @Strings VALUES (LEFT(@text, @index - 1))   

        SET @text = RIGHT(@text, (LEN(@text) - @index))  

      END  

    ELSE 

      SET @text = RIGHT(@text, (LEN(@text) - @index)) 

    END
    Update @Strings set value = REPLACE(value, @rChar, ' ');
    
    

  RETURN

END


