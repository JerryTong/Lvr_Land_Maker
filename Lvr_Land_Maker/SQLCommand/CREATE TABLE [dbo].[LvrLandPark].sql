USE [LvrLand]
GO

/****** Object:  Table [dbo].[LvrLandPark]    Script Date: 05/31/2015 15:02:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LvrLandPark](
	[Uid] [int] IDENTITY(1,1) NOT NULL,
	[Number] [varchar](50) NOT NULL,
	[Cost] [decimal](18, 2) NULL,
	[SquareMeter] [decimal](18, 2) NULL,
	[LevelGround] [decimal](18, 2) NULL,
	[ParkType] [varchar](50) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


