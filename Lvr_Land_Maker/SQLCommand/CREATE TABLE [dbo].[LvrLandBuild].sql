USE [LvrLand]
GO

/****** Object:  Table [dbo].[LvrLandBuild]    Script Date: 05/31/2015 15:01:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LvrLandBuild](
	[Uid] [int] IDENTITY(1,1) NOT NULL,
	[Number] [varchar](50) NOT NULL,
	[Year] [int] NULL,
	[SquareMeter] [decimal](18, 2) NULL,
	[LevelGround] [decimal](18, 2) NULL,
	[BuildsType] [nchar](50) NULL,
	[Materials] [nchar](50) NULL,
	[CompletedDate] [date] NULL,
	[AllFloors] [int] NULL,
	[BuildPartitioned] [int] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


