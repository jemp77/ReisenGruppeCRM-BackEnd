CREATE TABLE [dbo].[CustomerServiceObservationFiles] (
    [Id]                           INT           IDENTITY (1, 1) NOT NULL,
    [CustomerServiceObservationId] INT           NOT NULL,
    [FilePath]                     VARCHAR (MAX) NOT NULL,
    [FileName]                     VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_CustomerServiceObservationFiles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CustomerServiceObservationFiles_CustomerServiceObservations] FOREIGN KEY ([CustomerServiceObservationId]) REFERENCES [dbo].[CustomerServiceObservations] ([Id])
);

