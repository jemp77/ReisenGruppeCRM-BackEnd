CREATE TABLE [dbo].[ContractBeneficiaries] (
    [id]             INT          IDENTITY (1, 1) NOT NULL,
    [Names]          VARCHAR (50) NOT NULL,
    [LastNames]      VARCHAR (50) NULL,
    [DocumentType]   VARCHAR (10) NULL,
    [DocumentNumber] VARCHAR (20) NULL,
    [BirthDate]      DATE         NULL,
    [ContractId]     INT          NOT NULL,
    CONSTRAINT [PK_ContractBeneficiaries] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_ContractBeneficiaries_Contract] FOREIGN KEY ([ContractId]) REFERENCES [dbo].[Contract] ([Id])
);

