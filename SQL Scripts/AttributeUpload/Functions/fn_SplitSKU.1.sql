/****** Object:  UserDefinedFunction [dbo].[fn_Split]    Script Date: 06/29/2017 16:03:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*

Select * from dbo.fn_splitSKU('mmm-ccc-mmmmmmmmmmm-mmmmm-ccc-u-g-k-r-c','-') 
Select * from dbo.fn_splitSKU('mmm-null-null-mmmmm-ccc-u-g-k-r-c','-') 

*/

Alter  FUNCTION [dbo].[fn_SplitSKU](@text varchar(8000), @delimiter varchar(20) = '-')

RETURNS @Strings TABLE

(    
  position int IDENTITY PRIMARY KEY,
  KeyID numeric(18),
  KeyString nvarchar(50),
  size int,
  value varchar(25),
  WildCard varchar(25)       
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

        INSERT INTO @Strings VALUES ( -1,'',0,@text, @text)

          BREAK  

      END  

    IF (@index > 1)  

      BEGIN   

        INSERT INTO @Strings VALUES ( -1,'',0,LEFT(@text, @index - 1),LEFT(@text, @index - 1))   

        SET @text = RIGHT(@text, (LEN(@text) - @index))  

      END  

    ELSE 

      SET @text = RIGHT(@text, (LEN(@text) - @index)) 

    END
    Update @Strings set value = REPLACE(value, @rChar, ' ');
    
    
    
Update @Strings set KeyString = 'Manufacturer', size = 3 where position = 1  
Update @Strings set KeyString = 'Carrier', size = 3 where position = 2  
Update @Strings set KeyString = 'Model', size = 11 where position = 3  
Update @Strings set KeyString = 'Memory', size = 5 where position = 4  
Update @Strings set KeyString = 'Colour', size = 3 where position = 5  
Update @Strings set KeyString = 'Unlocked Status', size = 1 where position = 6  
Update @Strings set KeyString = 'Grade', size = 1 where position = 7  
Update @Strings set KeyString = 'IsKitted', size = 2 where position = 8  
Update @Strings set KeyString = 'Refurb', size = 1 where position = 9  
Update @Strings set KeyString = 'Country', size = 2 where position = 10  

Update @Strings set WildCard = REPLICATE('_', size) where WildCard = 'null'



  RETURN

END
