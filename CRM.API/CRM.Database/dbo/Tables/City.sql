﻿CREATE TABLE [dbo].[City] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (30) NOT NULL,
    CONSTRAINT [PK_CITY] PRIMARY KEY CLUSTERED ([Id] ASC)
);

