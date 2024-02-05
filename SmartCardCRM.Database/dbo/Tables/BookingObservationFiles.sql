CREATE TABLE [dbo].[BookingObservationFiles] (
    [Id]                   INT           IDENTITY (1, 1) NOT NULL,
    [BookingObservationId] INT           NOT NULL,
    [FilePath]             VARCHAR (MAX) NOT NULL,
    [FileName]             VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_BookingObservationFiles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BookingObservationFiles_BookingObservations] FOREIGN KEY ([BookingObservationId]) REFERENCES [dbo].[BookingObservations] ([Id])
);

