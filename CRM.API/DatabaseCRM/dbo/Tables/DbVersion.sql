CREATE TABLE [dbo].[DbVersion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime2](0) NOT NULL,
	[DbVersion] [nvarchar](8) NOT NULL
) ON [PRIMARY]
GO

