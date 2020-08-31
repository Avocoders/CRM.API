CREATE TABLE [dbo].[Lead] (
    [Id]               BIGINT        IDENTITY (1, 1) NOT NULL,
    [RoleId]           TINYINT       NOT NULL,
    [FirstName]        NVARCHAR (30) NOT NULL,
    [LastName]         NVARCHAR (30) NOT NULL,
    [Patronymic]       NVARCHAR (30) NULL,
    [Login]            NVARCHAR (30) NULL,
    [Password]         NVARCHAR (64) NOT NULL,
    [Phone]            NVARCHAR (20) NOT NULL,
    [Email]            NVARCHAR (40) NULL,
    [CityId]           INT           NULL,
    [Address]          NVARCHAR (90) NOT NULL,
    [BirthDate]        DATE          NOT NULL,
    [RegistrationDate] DATETIME2 (7) NOT NULL,
    [ChangeDate]       DATETIME2 (7) NOT NULL,
    [IsDeleted]        BIT           DEFAULT ('0') NOT NULL,
    CONSTRAINT [PK_LEAD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Lead_fk0] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id]),
    CONSTRAINT [Lead_fk1] FOREIGN KEY ([CityId]) REFERENCES [dbo].[City] ([Id]),
    UNIQUE NONCLUSTERED ([Email] ASC),
    UNIQUE NONCLUSTERED ([Login] ASC)
);

