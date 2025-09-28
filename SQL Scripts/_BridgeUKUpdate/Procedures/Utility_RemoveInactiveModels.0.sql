/****** Object:  StoredProcedure [dbo].[Utility_RemoveInactiveModels]    Script Date: 10/16/2019 11:53:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------

--------------------------------------------------------------------------------------
/*

exec Utility_RemoveInactiveModels

exec Utility_ModelSummary

*/
--------------------------------------------------------------------------------------
--
*/


Create PROCEDURE [dbo].[Utility_RemoveInactiveModels]

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




Delete MasterModelMemoryLookup
Delete MasterCarrierManufacturerLookup where StatusID = @LookupInActiveID
Select @LookupCount = @@RowCount

Delete [Option] where QuestionID = 244 and OptionStatusID = @optionInActiveID
Select @OptionCount = @@RowCount

Print '# Options Removed:' + convert(nvarchar(20), @OptionCount) + '        # Lookups Removed:' + convert(nvarchar(20), @LookupCount)

End