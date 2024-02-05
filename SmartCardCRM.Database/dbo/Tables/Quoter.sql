CREATE TABLE [dbo].[Quoter] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (100) NOT NULL,
    [Email]         VARCHAR (100) NOT NULL,
    [Cellphone]     VARCHAR (15)  NOT NULL,
    [Destination]   VARCHAR(100)    NOT NULL,
    [ArrivalDate]   DATETIME      NOT NULL,
    [DepartureDate] DATETIME      NOT NULL,
    CONSTRAINT [PK__Quoter__3214EC07BDC8DB1D] PRIMARY KEY CLUSTERED ([Id] ASC)
);

