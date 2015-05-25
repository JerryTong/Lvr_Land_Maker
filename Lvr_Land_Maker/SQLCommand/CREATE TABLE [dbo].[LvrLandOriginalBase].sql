USE [LvrLand]
GO

/****** Object:  Table [dbo].[LvrLandOriginalBase]    Script Date: 05/25/2015 22:28:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LvrLandOriginalBase](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ObjectNumber] [varchar](30) NULL,
	[SaleType] [varchar](20) NULL,
	[City] [nvarchar](50) NULL,
	[CityCode] [char](2) NULL,
	[CityName] [nvarchar](50) NULL,
	[ZipCode] [int] NULL,
	[Townships] [nvarchar](50) NULL,
	[Address] [nvarchar](255) NULL,
	[Subject] [nvarchar](255) NULL,
	[SubjectCode] [int] NULL,
	[Partition] [nvarchar](50) NULL,
	[PartitionCode] [int] NULL,
	[Non_Partition] [nvarchar](50) NULL,
	[Non_Scheduled] [nvarchar](50) NULL,
	[LandSquareMeter] [decimal](18, 2) NULL,
	[LandLevelGround] [decimal](18, 2) NULL,
	[TradeDate] [date] NULL,
	[TradeYear] [nvarchar](50) NULL,
	[AllFloors] [int] NULL,
	[TradeFloors] [nvarchar](100) NULL,
	[BuildsType] [nvarchar](100) NULL,
	[BuildsTypeCode] [int] NULL,
	[Using] [nvarchar](50) NULL,
	[UsingCode] [int] NULL,
	[Materials] [nvarchar](50) NULL,
	[MaterialsCode] [int] NULL,
	[CompletedDate] [date] NULL,
	[BuildsSquareMeter] [decimal](18, 2) NULL,
	[BuildsLevelGround] [decimal](18, 2) NULL,
	[Room] [int] NULL,
	[Livingroom] [int] NULL,
	[Bathroom] [int] NULL,
	[Compartment] [int] NULL,
	[Management] [int] NULL,
	[Cost] [decimal](18, 2) NULL,
	[SquareMeterCost] [decimal](18, 2) NULL,
	[LevelGroundCost] [decimal](18, 2) NULL,
	[CarType] [varchar](20) NULL,
	[CarTypeCode] [int] NULL,
	[CarSquareMeter] [decimal](18, 2) NULL,
	[CarLevelGround] [decimal](18, 2) NULL,
	[CarCost] [decimal](18, 2) NULL,
	[Remark] [nvarchar](255) NULL,
	[EditTime] [date] NULL,
 CONSTRAINT [PK_LvrLandOriginalBase] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


