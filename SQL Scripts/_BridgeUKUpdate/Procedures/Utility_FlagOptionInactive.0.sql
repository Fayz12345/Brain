/****** Object:  StoredProcedure [dbo].[Utility_FlagOptionInactive]    Script Date: 10/16/2019 11:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------

--------------------------------------------------------------------------------------
/*

exec Utility_FlagOptionInactive 6129

exec Utility_ModelSummary

*/
--------------------------------------------------------------------------------------
--
*/


-- Select * from MasterCarrierManufacturerLookup where OptionModelID = 6171

Create PROCEDURE [dbo].[Utility_FlagOptionInactive]
      @OptionID numeric(18,0)

 AS

Begin
SET NOCOUNT ON

Declare @optionInActiveID numeric(18)
Declare @LookupInActiveID numeric(18)


Declare @OptionCount numeric(18)
Declare @LookupCount numeric(18)

Select @optionInActiveID = OptionStatusID from OptionStatus where Status = 'Inactive'
Select @LookupInActiveID = MasterCarrierManufacturerStatusID from MasterCarrierManufacturerStatus where Status = 'Inactive'

if ISNULL(@optionInActiveID, -1) < 1
   begin
   Print 'Error: Option Status Inactive Not Found'
   return
   end

if ISNULL(@optionInActiveID, -1) < 1
   begin
   Print 'Error: MasterCarrierManufacturerlookup Status Inactive Not Found'
   return
   end

Update [Option] set OptionStatusID = @optionInActiveID where OptionID = @OptionID
Select @OptionCount = @@RowCount

Update MasterCarrierManufacturerLookup set StatusID = @LookupInActiveID where OptionModelID = @OptionID
Select @LookupCount = @@RowCount

Print '# Options Set Inactive:' + convert(nvarchar(20), @OptionCount) + '        # Lookups Set Inactive:' + convert(nvarchar(20), @LookupCount)

End