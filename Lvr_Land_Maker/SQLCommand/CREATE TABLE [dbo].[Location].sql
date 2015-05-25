USE [LvrLand]
GO

/****** Object:  Table [dbo].[Location]    Script Date: 11/08/2014 15:53:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Location](
	[City] [nvarchar](50) NOT NULL,
	[CityCode] [nchar](10) NOT NULL,
	[Township] [nvarchar](50) NOT NULL,
	[TownshipCode] [int] NOT NULL,
	[ZipCode] [int] NOT NULL,
	[UId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[UId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


