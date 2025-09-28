/****** Object:  Table [dbo].[PricingSKURateTable]    Script Date: 06/29/2018 13:50:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Drop Table PricingSKURateTable
CREATE TABLE [dbo].[PricingSKURateTable](
	[PricingSKURateTableID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[PricingURLMapID] [numeric](18, 0) NOT NULL,
	[Catagory] [nvarchar](50) NULL,
	[StatusID] [numeric](18, 0) NOT NULL,
	[CarrierID] [numeric](18, 0) NULL,
	[ManufacturerID] [numeric](18, 0) NULL,
	[ModelID] [numeric](18, 0) NULL,
	[ColourID] [numeric](18, 0) NULL,
	[MemoryID] [numeric](18, 0) NULL,
	[ConditionID] [numeric](18, 0) NULL,
	[GradeID] [numeric](18, 0) NULL,
	[Value] [numeric](18, 5) NULL,
	[EffectiveDate] [datetime] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PricingSKURateTable] PRIMARY KEY CLUSTERED 
(
	[PricingSKURateTableID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PricingSKURateTable]  WITH CHECK ADD  CONSTRAINT [FK_PricingSKURateTable_MasterCarrierManufacturerStatus] FOREIGN KEY([StatusID])
REFERENCES [dbo].[MasterCarrierManufacturerStatus] ([MasterCarrierManufacturerStatusID])
GO

ALTER TABLE [dbo].[PricingSKURateTable] CHECK CONSTRAINT [FK_PricingSKURateTable_MasterCarrierManufacturerStatus]

GO

ALTER TABLE [dbo].[PricingSKURateTable] ADD  CONSTRAINT [DF_PricingSKURateTable_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[PricingSKURateTable] ADD  CONSTRAINT [DF_PricingSKURateTable_CreateUser]  DEFAULT ('') FOR [CreateUser]
GO

/*

Set NOCOUNT ON
Select MasterCarrierManufacturerLookupID as ID
     , CONVERT(numeric(10,0), -1) as process
     , OptionManufacturerID as ManufacturerID
     , OptionModelID as ModelID
     , OptionCarrierID as CarrierID
     , OptionColourID as ColourID
     , CONVERT(numeric(10,0), -1) as ConditionID
     , CONVERT(numeric(10,0), -1) as GradeID
     , CONVERT(numeric(10,0), -1) as MemoryID
into #Temp     
from MasterCarrierManufacturerLookup A
Inner join MasterCarrierManufacturerStatus B on A.StatusID = B.MasterCarrierManufacturerStatusID
where Status = 'Active'
Order by OptionManufacturerID, OptionModelID, OptionColourID

Set NOCOUNT ON
Declare @ID Numeric(10, 0)
Declare @ManufacturerID Numeric(10, 0)
Declare @ModelID Numeric(10, 0)
Declare @CarrierID Numeric(10, 0)
Declare @ColourID Numeric(10, 0)
Declare @ConditionID Numeric(10, 0)
Declare @GradeID Numeric(10, 0)
Declare @MemoryID Numeric(10, 0)

Declare @RNDOM numeric(10,2)
while exists(Select * from #Temp where Process < 1)
      begin
      Select Top 1 @ID = #Temp.ID
                  ,@ManufacturerID = ManufacturerID
                  ,@ModelID = ModelID
                  ,@CarrierID = CarrierID 
                  ,@ColourID = @ColourID 
                  ,@ConditionID = @ConditionID 
                  ,@GradeID = @GradeID 
                  ,@MemoryID = @MemoryID 
                  from #Temp where Process < 1
      Update #Temp set process = 1 where ID = @ID

      Select @RNDOM = FLOOR(RAND()* 10 + 1)
      -- Print @RNDOM
     
      if @RNDOM = 1
         begin
         --Select @ModelID = Null
         --Select @CarrierID = Null 
         --Select @ColourID = Null 
         Select @ConditionID = Null 
         Select @GradeID = Null 
         Select @MemoryID = Null       
         end
      if @RNDOM = 2
         begin
         --Select @ModelID = Null
         --Select @CarrierID = Null 
         --Select @ColourID = Null 
         Select @ConditionID = Null 
         Select @GradeID = Null 
         Select @MemoryID = Null       
         end
      if @RNDOM = 3
         begin
         Select @ModelID = Null
         --Select @CarrierID = Null 
         --Select @ColourID = Null 
         Select @ConditionID = Null 
         Select @GradeID = Null 
         Select @MemoryID = Null       
         end
      if @RNDOM = 4
         begin
         Select @ModelID = Null
         Select @CarrierID = Null 
         --Select @ColourID = Null 
         Select @ConditionID = Null 
         Select @GradeID = Null 
         Select @MemoryID = Null       
         end
      if @RNDOM = 5
         begin
         Select @ModelID = Null
         Select @CarrierID = Null 
         Select @ColourID = Null 
         Select @ConditionID = Null 
         Select @GradeID = Null 
         Select @MemoryID = Null       
         end
      if @RNDOM = 6
         begin
         Select @ModelID = Null
         --Select @CarrierID = Null 
         --Select @ColourID = Null 
         --Select @ConditionID = Null 
         --Select @GradeID = Null 
         --Select @MemoryID = Null       
         end
      if @RNDOM = 7
         begin
         Select @ModelID = Null
         Select @CarrierID = Null 
         --Select @ColourID = Null 
         --Select @ConditionID = Null 
         --Select @GradeID = Null 
         --Select @MemoryID = Null       
         end
      if @RNDOM = 8
         begin
         Select @ModelID = Null
         Select @CarrierID = Null 
         --Select @ColourID = Null 
         --Select @ConditionID = Null 
         --Select @GradeID = Null 
         --Select @MemoryID = Null       
         end
      if @RNDOM = 9
         begin
         Select @ModelID = Null
         --Select @CarrierID = Null 
         --Select @ColourID = Null 
         --Select @ConditionID = Null 
         --Select @GradeID = Null 
         --Select @MemoryID = Null       
         end
      if @RNDOM = 10
         begin
         Select @ModelID = Null
         --Select @CarrierID = Null 
         --Select @ColourID = Null 
         --Select @ConditionID = Null 
         --Select @GradeID = Null 
         --Select @MemoryID = Null       
         end
         
      Update #Temp set ModelID = @ModelID, CarrierID = @CarrierID, ColourID = @ColourID, ConditionID = @ConditionID, GradeID = @GradeID, MemoryID = @MemoryID where ID = @ID 
         
      
      end


Select * from #Temp
Select Distinct IDENTITY(numeric(18), 1, 1) as ID, Process, ManufacturerID, ModelID, CarrierID, ColourID, ConditionID, GradeID, MemoryID into #Temp2 from #temp
Select * from #Temp2



Set NOCOUNT ON
Declare @ID Numeric(10, 0)
Declare @ManufacturerID Numeric(10, 0)
Declare @ModelID Numeric(10, 0)
Declare @CarrierID Numeric(10, 0)
Declare @ColourID Numeric(10, 0)
Declare @ConditionID Numeric(10, 0)
Declare @GradeID Numeric(10, 0)
Declare @MemoryID Numeric(10, 0)
Declare @RNDOM numeric(10,2)


Update #Temp2 Set process = 0
Where Exists (Select * from #Temp2 where process < 1)
      begin
      Select Top 1 @ID = #Temp2.ID
                  ,@ManufacturerID = ManufacturerID
                  ,@ModelID = ModelID
                  ,@CarrierID = CarrierID 
                  ,@ColourID = @ColourID 
                  ,@ConditionID = @ConditionID 
                  ,@GradeID = @GradeID 
                  ,@MemoryID = @MemoryID 
                  from #Temp2 where Process < 1
                  
                  
      Update #Temp2 set process = 1 where ID = @ID

      Select @RNDOM = FLOOR(RAND()* 10 + 1)
      Print @Rndom
      INSERT INTO [PricingSKURateTable] ([PricingURLMapID],[Catagory],[StatusID],[CarrierID],[ManufacturerID],[ModelID],[ColourID],[MemoryID],[ConditionID],[GradeID],[Value],[EffectiveDate],[CreateDate],[CreateUser])
      VALUES  (1,'Cat A',1,@CarrierID,@ManufacturerID,@ModelID,@ColourID,@MemoryID,@ConditionID,@GradeID,@RNDOM,GETDATE(),GETDATE(),'Scrapper')
      end













Select * from PricingSKURateTable order by manufacturerID, CarrierID, ModelID

Delete PricingSKURateTable
Drop table #Temp
Drop table #Temp2







--Select FLOOR(RAND()* 10 + 1)
--SELECT FLOOR(RAND()*(10-5+1)+5); 


--INSERT INTO [BW_Sandbox02].[dbo].[PricingSKURateTable]
--           ([PricingURLMapID]
--           ,[Catagory]
--           ,[StatusID]
--           ,[CarrierID]
--           ,[ManufacturerID]
--           ,[ModelID]
--           ,[ColourID]
--           ,[MemoryID]
--           ,[ConditionID]
--           ,[GradeID]
--           ,[Value]
--           ,[EffectiveDate]
--           ,[CreateDate]
--           ,[CreateUser])
--     VALUES
--           (<PricingURLMapID, numeric(18,0),>
--           ,<Catagory, nvarchar(50),>
--           ,<StatusID, numeric(18,0),>
--           ,<CarrierID, numeric(18,0),>
--           ,<ManufacturerID, numeric(18,0),>
--           ,<ModelID, numeric(18,0),>
--           ,<ColourID, numeric(18,0),>
--           ,<MemoryID, numeric(18,0),>
--           ,<ConditionID, numeric(18,0),>
--           ,<GradeID, numeric(18,0),>
--           ,<Value, numeric(18,5),>
--           ,<EffectiveDate, datetime,>
--           ,<CreateDate, datetime,>
--           ,<CreateUser, nvarchar(50),>)
--GO



*/