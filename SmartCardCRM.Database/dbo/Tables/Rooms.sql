CREATE TABLE [dbo].[Rooms] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [IdQuoter] INT          NOT NULL,
    [Adults]   INT          NOT NULL,
    [Kids]     INT          NULL,
    [KidsAges] VARCHAR (20) NULL,
    CONSTRAINT [PK__Rooms__3214EC072EB77D73] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Rooms_Quoter] FOREIGN KEY ([IdQuoter]) REFERENCES [dbo].[Quoter] ([Id])
);

