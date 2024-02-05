CREATE TABLE [dbo].[CustomerServiceObservations] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [ContractId]      INT           NOT NULL,
    [Observations]    VARCHAR (MAX) NOT NULL,
    [ObservationDate] DATETIME      NOT NULL,
    CONSTRAINT [PK_CustomerServiceObservations] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CustomerServiceObservations_Contract] FOREIGN KEY ([ContractId]) REFERENCES [dbo].[Contract] ([Id])
);

