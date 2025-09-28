



















/****** Object:  Table [dbo].[MasterCarrierManufacturerLookup]    Script Date: 04/27/2017 19:55:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MasterModelMemoryLookup](
	[MasterModelMemoryLookupID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[StatusID] [numeric](18, 0) NOT NULL,
	[ModelID] [numeric](18, 0) NOT NULL,
	[MemoryID] [numeric](18, 0) NOT NULL,
	[Retire] [nvarchar](20) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MasterModelMemoryLookup] PRIMARY KEY CLUSTERED 
(
	[MasterModelMemoryLookupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 75) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MasterModelMemoryLookup]  WITH CHECK ADD  CONSTRAINT [FK_MasterModelMemoryLookup_MasterCarrierManufacturerStatus] FOREIGN KEY([StatusID])
REFERENCES [dbo].[MasterCarrierManufacturerStatus] ([MasterCarrierManufacturerStatusID])
GO

ALTER TABLE [dbo].[MasterModelMemoryLookup]  WITH CHECK ADD  CONSTRAINT [FK_MasterModelMemoryLookup_OptionModel] FOREIGN KEY([ModelID])
REFERENCES [dbo].[Option] ([OptionID])
GO

ALTER TABLE [dbo].[MasterModelMemoryLookup]  WITH CHECK ADD  CONSTRAINT [FK_MasterModelMemoryLookup_OptionMemory] FOREIGN KEY([MemoryID])
REFERENCES [dbo].[Option] ([OptionID])
GO

ALTER TABLE [dbo].[MasterModelMemoryLookup] CHECK CONSTRAINT [FK_MasterModelMemoryLookup_MasterCarrierManufacturerStatus]
GO

ALTER TABLE [dbo].[MasterModelMemoryLookup] ADD  CONSTRAINT [DF_MasterModelMemoryLookup_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[MasterModelMemoryLookup] ADD  CONSTRAINT [DF_MasterModelMemoryLookup_CreateUser]  DEFAULT ('') FOR [CreateUser]
GO

ALTER TABLE [dbo].[MasterModelMemoryLookup] ADD  CONSTRAINT [DF_MasterModelMemoryLookup_LastUpdateDate]  DEFAULT (getdate()) FOR [LastUpdateDate]
GO

ALTER TABLE [dbo].[MasterModelMemoryLookup] ADD  CONSTRAINT [DF_MasterModelMemoryLookup_LastUpdateUser]  DEFAULT ('') FOR [LastUpdateUser]
GO

Create Index MasterModelMemory on [dbo].[MasterModelMemoryLookup](ModelID, MemoryID)
GO


Alter Table IFSPickListOrderDetail ALter Column SKUPART_NO nVarchar(50)

Alter Table Receivedetail ALter Column SKU nVarchar(50)









































/****** Object:  UserDefinedFunction [dbo].[GetIFSSKUCarrierSegment]    Script Date: 04/26/2017 11:26:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*

Print dbo.GetIFSCondtion(69510)


*/

Create FUNCTION [dbo].[GetIFSSKUCarrierSegment](@mReceiveDetailID numeric(18))
RETURNS nVarchar(3)
AS
BEGIN
Declare @mReturnValue nvarchar(3)
         

Select @mReturnValue = dbo.GetSKUSegment(@mReceiveDetailID,'Carrier',3,' ')
Return @mReturnValue

END

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










































/****** Object:  View [dbo].[vwSKUCalculated]    Script Date: 04/06/2017 15:02:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
Select * from vwSKUCalculated where ReceiveDetailID = 2892
Select dbo.GetIFSSKUCarrierSegment(ReceiveDetailID) from ReceiveDetail  where ReceiveDetailID = 127912
Select dbo.GetIFSSKUKittingSegment(ReceiveDetailID) from ReceiveDetail  where ReceiveDetailID = 127912

Select * from [Option] where OptionID = 1823

Select * from REceiveDetail where ReceiveDetailID = 127912
Select ReceiveDetail.ReceiveDetailID
       ,Manufacturer.Name as Manufactuer,
                                   Model.Name as Model,
                                   dbo.GetIFSSKUCarrierSegment(ReceiveDetailID) as Carrier,
                                   Colour.Name as Colour
FROM         dbo.[Option] AS Colour RIGHT OUTER JOIN
                      dbo.ReceiveDetail ON Colour.OptionID = dbo.ReceiveDetail.ColourID LEFT OUTER JOIN
                      dbo.[Option] AS Model ON dbo.ReceiveDetail.ModelID = Model.OptionID LEFT OUTER JOIN
                      dbo.[Option] as Manufacturer ON dbo.ReceiveDetail.ManufacturerID = Manufacturer.OptionID 
 where ReceiveDetailID = 127912

  Update ReceiveDetail set SKU = SKU_Calc
 From ReceiveDetail R
 Inner join vwSKUCalculated S on R.ReceiveDetailID = S.ReceiveDetailID
 
 Select SKU, SKU_Calc, len(SKU_Calc) as sl
 From ReceiveDetail R
 Inner join vwSKUCalculated S on R.ReceiveDetailID = S.ReceiveDetailID
 
 Alter Table ReceiveDetail Alter Column SKU nvarchar(50)

*/


CREATE VIEW [dbo].[vwSKUCalculated]
AS
SELECT     dbo.ReceiveDetail.ReceiveDetailID
         , dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Manufacturer',3,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Carrier',3,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Model',3,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Memory',11,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Colour',5,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Unlock',2,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Grade',1,' ') + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Refurb',1,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Country',2,' ') + '-' + 
           dbo.GetSKUSegment(dbo.ReceiveDetail.ReceiveDetailID,'Kit',1,' ') + 
           space(10) as  SKU_CALC
         --, CASE WHEN len(isnull(Manufacturer.Name + Model.Name + dbo.GetIFSSKUCarrierSegment(ReceiveDetailID) + Colour.Name, '')) > 25 
         --       THEN LEFT(isnull(isnull(Manufacturer.Name,'XXX') + isnull(Model.Name,'XXX') + dbo.GetIFSSKUCarrierSegment(ReceiveDetailID) + isnull(Colour.Name,'XXX'), ''), 25) 
         --       ELSE isnull(isnull(Manufacturer.Name,'XXX') + isnull(Model.Name,'XXX') + dbo.GetIFSSKUCarrierSegment(ReceiveDetailID) + isnull(Colour.Name,'XXX'), '') + dbo.GetIFSSKUKittingSegment(ReceiveDetailID) 
         --  END AS SKU_CALC1
FROM dbo.ReceiveDetail
--LEFT OUTER JOIN dbo.[Option] AS Colour ON dbo.ReceiveDetail.ColourID = Colour.OptionID
--LEFT OUTER JOIN dbo.[Option] AS Model ON dbo.ReceiveDetail.ModelID = Model.OptionID 
--LEFT OUTER JOIN dbo.[Option] as Manufacturer ON dbo.ReceiveDetail.ManufacturerID = Manufacturer.OptionID




GO









































/****** Object:  StoredProcedure [dbo].[RecordProjectDefinitionProcess]    Script Date: 05/01/2017 17:11:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>


/*


exec GetDeviceCatalogue

Select * from MasterModelMemoryLookup

*/


-- =============================================
Alter PROCEDURE [dbo].[GetDeviceCatalogue]

AS
BEGIN
SET NOCOUNT ON;

Select SKU, COUNT(*) as Qty, CONVERT(int, 0) as Allocated from ReceiveDetail r
Inner join ReceiveDetailStatus s on r.StatusID = s.ReceiveDetailStatusID
inner join ClientLocation CL on cl.ClientLocationID = r.ClientLocationID
inner join Client C on cl.ClientID = c.ClientID
Where Version = '000' and s.Status != 'GraveYard' 
Group By SKU
having count(*) > 0
Order by SKU


END


/****** Object:  StoredProcedure [dbo].[GetDeviceCataloguePartial]    Script Date: 05/17/2017 09:05:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>


/*


exec GetDeviceCataloguePartial 'ALC-BEL-40A-          -  BLK-  -B -  -           ,ALC-BEL-40A-          -  GRY-  -d -  -           ,ALC-BEL-50A-          -  GRY-  -d -  -           ,ALC-BEL-50A-          -  GRY-  -W -  -           ,ALC-KOO-170-          -  BLK-  -A -  -           ,ALC-KOO-170-          -  BLK-  -B -  -           ,ALC-KOO-170-          -  BLK-  -C -  -           ,ALC-KOO-170-          -  BLK-  -d -  -           '


Declare @List nvarchar(max)
Select @List = 'ALC-BEL-40A-          -  BLK-  -B -  -           ,ALC-BEL-40A-          -  GRY-  -d -  -           ,ALC-BEL-50A-          -  GRY-  -d -  -           ,ALC-BEL-50A-          -  GRY-  -W -  -           ,ALC-KOO-170-          -  BLK-  -A -  -           ,ALC-KOO-170-          -  BLK-  -B -  -           ,ALC-KOO-170-          -  BLK-  -C -  -           ,ALC-KOO-170-          -  BLK-  -d -  -           '
SELECT * into #Tempxxx FROM fn_Split(@List, ',')
Select * from #Tempxxx
Drop table #Tempxxx

*/


-- =============================================
Create PROCEDURE [dbo].[GetDeviceCataloguePartial]
     @List nvarchar(max)

AS
BEGIN
SET NOCOUNT ON;


-- SELECT * into #Tempxxx FROM fn_Split(@List, '/')


Declare @text nvarchar(max)
Select @text = @List
DECLARE @index int 
Declare @delimiter varchar(20)
Select @delimiter = ','



SELECT convert(numeric(18),0) as Processed, * into #Tempxxx FROM fn_Split(@List, @delimiter)
Select SKU, COUNT(*) as Qty, CONVERT(int, 0) as Allocated from ReceiveDetail r
Inner join ReceiveDetailStatus s on r.StatusID = s.ReceiveDetailStatusID
inner join ClientLocation CL on cl.ClientLocationID = r.ClientLocationID
inner join Client C on cl.ClientID = c.ClientID
inner join #Tempxxx L on L.value = r.SKU
Where Version = '000' and s.Status != 'GraveYard' -- and SKU in (Select value from @List)
Group By SKU
having count(*) > 0
Order by SKU

Drop Table #Tempxxx
END


/****** Object:  StoredProcedure [dbo].[RecordProjectDefinitionProcess]    Script Date: 05/01/2017 17:11:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>


/*


exec RecordModelDefinitionMemory 6171,'7465,7467','','jmccomb'

Select * from MasterModelMemoryLookup

*/


-- =============================================
Alter PROCEDURE [dbo].[RecordModelDefinitionMemory]
    @mModelID numeric(18),
    @mMemoryKeyIDList varchar(8000),
	@mModelName varchar(20) = '',
	@mUserName varchar(50)
AS
BEGIN
SET NOCOUNT ON;

Declare @mStatusID numeric(18,0)
Select @mStatusID = 1
Select ValueID as TargetMemoryID, 0 as processed into #MemoryKeyList from dbo.fn_SplitDistinctNumeric(@mMemoryKeyIDList,',')

-- Remove any from NextProcessStep that are not in #NextStepList
Delete MasterModelMemoryLookup  
 Where MasterModelMemoryLookup.ModelID = @mModelID and MemoryID not in (Select TargetMemoryID from #MemoryKeyList)

---- Remove any from #NextStepList that is in NextProcessStep
Update #MemoryKeyList set Processed = 1 
 where TargetMemoryID in (select MemoryID from MasterModelMemoryLookup where MasterModelMemoryLookup.ModelID = @mModelID)

-- Add the rest to NextProcessStep
Insert MasterModelMemoryLookup 
           ([ModelID] ,[MemoryID], StatusID,[CreateDate] ,[CreateUser],[LastUpdateDate],[LastUpdateUser])
Select @mModelID, TargetMemoryID, (SELECT MasterCarrierManufacturerStatusID
  FROM [MasterCarrierManufacturerStatus] where Status = 'Active'), getdate(), @mUserName, getdate(), @mUserName
  from #MemoryKeyList 
 where processed = 0


Return 0

END


















































































