USE [MbanqDevTest]
GO

/****** Object:  Table [dbo].[Person]    Script Date: 21/04/2021 22:36:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Person](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TIN] [varchar](50) NOT NULL,
	[Name] [varchar](50) NULL,
	[Surname] [varchar](50) NULL,
	[Place] [varchar](50) NULL,
	[Address] [varchar](100) NULL,
	[Phone] [varchar](50) NULL,
	[Email] [varchar](100) NULL,
 CONSTRAINT [PK_Person2] PRIMARY KEY CLUSTERED 
(
	[TIN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


