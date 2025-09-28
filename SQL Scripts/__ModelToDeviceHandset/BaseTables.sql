
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*

Select * from [Option] where QuestionID in (Select QuestionID from Question where Name = 'DeviceHandset')
Select * from [Option] where QuestionID in (Select QuestionID from Question where Name = 'Model')
Select * from QuestionStatus

INSERT INTO [MasterModelToDeviceHandset] ([StatusID], [ModelID],[DeviceHandsetID],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser]) VALUES (1, 1444,7370,GetDate(),'System',GetDate(),'System')
INSERT INTO [MasterModelToDeviceHandset] ([StatusID],[ModelID],[DeviceHandsetID],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser]) VALUES (1, 1445,7372,GetDate(),'System',GetDate(),'System')
INSERT INTO [MasterModelToDeviceHandset] ([StatusID],[ModelID],[DeviceHandsetID],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser]) VALUES (1, 1492,7371,GetDate(),'System',GetDate(),'System')
INSERT INTO [MasterModelToDeviceHandset] ([StatusID],[ModelID],[DeviceHandsetID],[CreateDate],[CreateUser],[LastUpdateDate],[LastUpdateUser]) VALUES (1, 1493,7373,GetDate(),'System',GetDate(),'System')

Select * from MasterModelToDeviceHandset

Select MasterModelToDeviceHandsetID, 
       Mster.StatusID, Status.Status, 
       Mster.ModelID, Model.OptionText, Model.Name as ModelName,
       Mster.DeviceHandsetID, Device.OptionText, Device.Name as DeviceName,
       Mster.CreateDate, Mster.CreateUser,
       Mster.LastUpdateDate, Mster.LastUpdateUser
 from MasterModelToDeviceHandset Mster
Inner join [Option] model on Model.OptionID = ModelID
Inner join [Option] Device on Device.OptionID = DeviceHandsetID
Inner join QuestionStatus Status on Status.QuestionStatusID = Mster.StatusID

-- Drop Table MasterModelToDeviceHandset

*/


-- Drop Table PricingSKURateTable
CREATE TABLE [dbo].[MasterModelToDeviceHandset](
	[MasterModelToDeviceHandsetID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[StatusID] [numeric](18, 0) NOT NULL,	-- This status pulls from the QuestionStatus Table.
	[ModelID] [numeric](18, 0) NOT NULL,
	[DeviceHandsetID] [numeric](18, 0) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](50) NOT NULL,	
	
 CONSTRAINT [PK_MasterModelToDeviceHandset] PRIMARY KEY CLUSTERED 
(
	[MasterModelToDeviceHandsetID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MasterModelToDeviceHandset] ADD  CONSTRAINT [DF_MasterModelToDeviceHandset_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[MasterModelToDeviceHandset] ADD  CONSTRAINT [DF_MasterModelToDeviceHandset_CreateUser]  DEFAULT ('') FOR [CreateUser]
GO




















