USE [LvrLand]
GO

/****** Object:  Table [dbo].[LvrLandLand]    Script Date: 05/31/2015 15:02:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LvrLandLand](
	[Uid] [int] IDENTITY(1,1) NOT NULL,
	[Number] [varchar](50) NULL,
	[LandSection] [nchar](150) NULL,
	[SquareMeter] [decimal](18, 2) NULL,
	[LevelGround] [decimal](18, 2) NULL,
	[Subject] [nchar](150) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


