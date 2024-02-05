CREATE TABLE [dbo].[BookingObservations] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [ContractId]      INT           NOT NULL,
    [Observations]    VARCHAR (MAX) NOT NULL,
    [ObservationDate] DATETIME      NOT NULL,
    CONSTRAINT [PK_BookingObservations] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BookingObservations_Contract] FOREIGN KEY ([ContractId]) REFERENCES [dbo].[Contract] ([Id])
);

