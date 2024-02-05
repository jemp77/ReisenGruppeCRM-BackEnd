CREATE TABLE [dbo].[ClientDebitCreditCards] (
    [Id]            INT          IDENTITY (1, 1) NOT NULL,
    [ClientId]      INT          NOT NULL,
    [IsClientCard]  BIT          NOT NULL,
    [CardType]      VARCHAR (20) NOT NULL,
    [FranchiseName] VARCHAR (30) NOT NULL,
    [BankName]      VARCHAR (30) NULL,
    CONSTRAINT [PK_ClientDebitCreditCards] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClientDebitCreditCards_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id])
);

