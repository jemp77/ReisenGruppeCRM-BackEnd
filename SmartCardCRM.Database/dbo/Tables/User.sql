CREATE TABLE [dbo].[User] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [UserName] VARCHAR (50) NOT NULL,
    [Password] VARCHAR (50) NOT NULL,
    [IsActive] BIT          NOT NULL,
    [Name]     VARCHAR (50) NOT NULL,
    [LastName] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_User] UNIQUE NONCLUSTERED ([UserName] ASC)
);

