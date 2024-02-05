CREATE TABLE [dbo].[ExceptionLog] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [ComponentName]     VARCHAR (50)  NOT NULL,
    [ExceptionMessage]  VARCHAR (MAX) NOT NULL,
    [ExceptionTraceLog] VARCHAR (MAX) NOT NULL,
    [ExceptionDate]     DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_ExceptionLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);

