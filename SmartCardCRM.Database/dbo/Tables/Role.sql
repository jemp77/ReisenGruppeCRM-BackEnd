CREATE TABLE [dbo].[Role] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [RoleName] VARCHAR (50)  NOT NULL,
    [Scopes]   VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([Id] ASC)
);

