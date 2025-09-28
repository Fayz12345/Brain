/****** Object:  UserDefinedFunction [dbo].[fn_GetDeviceValue]    Script Date: 07/17/2018 15:52:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
print dbo.fn_GetDeviceValue(7249,1437,1496, 7107 )

Select *
 from InvtPlanMfgModel T1 
where T1.StatusID = 1 
  and T1.InvtValuePlanID = 7249
  and T1.ManufacturerID = 1437 
  and T1.ModelID = 1496

Select *
  From InvtPlanCond TC
  where Tc.StatusID = 1
    and TC.InvtValuePlanID = 7249
    and TC.IFSConditionsID = 7107


*/


ALTER FUNCTION [dbo].[fn_GetDeviceValue](
                @ManufacturerID numeric(15),
                @ModelID numeric(15),
                @IFSConditionsID numeric(15)

)
RETURNS money
AS
BEGIN

----select * from Question where Name = 'Manufacturer'
----Select * from [Option] where QuestionID = 243

---- If we have an Apple or Samsung phone we need to use the CPA condition.
---- Otherwise we need to use the PTG Condtion
--Declare @Abbr nvarchar(10)
--Select @Abbr = O.Name from [Option] O inner join Question Q on Q.QuestionID = O.QuestionID where O.OptionID = @ManufacturerID and Q.Name = 'Manufacturer'

--if (@Abbr = 'SAM' or @Abbr = 'APL')
--   begin
--   Select @IFSConditionsID = O.OptionID 
--     from [Option] O 
--    inner join Question Q on Q.QuestionID = O.QuestionID 
--    where O.Name = 'CPA' and Q.Name = 'IFS Conditions'
--   end
--else
--   begin
--   Select @IFSConditionsID = O.OptionID 
--     from [Option] O 
--    inner join Question Q on Q.QuestionID = O.QuestionID 
--    where O.Name = 'PTG' and Q.Name = 'IFS Conditions'   
--   end   


--Declare @InvtValuePlanID numeric(15)
--Declare @value money
--Declare @Pct numeric(18,7)
--Select @value = 0

--Select @value = Value,
--       @InvtValuePlanID  = T1.InvtValuePlanID
--  from InvtPlanMfgModel T1 
-- where T1.StatusID = 1 
--   and T1.ManufacturerID = @ManufacturerID 
--   and T1.ModelID = @ModelID
   
--Select @value = ISNULL(@value,0);  

--Select @Pct = Pct
--  From InvtPlanCond TC
--  where Tc.StatusID = 1
--    and TC.InvtValuePlanID = @InvtValuePlanID
--    and TC.IFSConditionsID = @IFSConditionsID
--Select @Pct = ISNULL(@Pct,0);  

--Return @value * @Pct

Return 0


END
go

