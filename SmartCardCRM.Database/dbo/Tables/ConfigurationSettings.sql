CREATE TABLE [dbo].[ConfigurationSettings] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Key]       VARCHAR (50)  NOT NULL,
    [Value]     VARCHAR (MAX) NOT NULL,
    [IsEnabled] BIT           NOT NULL,
    [IsHidden]  BIT           DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ConfigurationSettings] PRIMARY KEY CLUSTERED ([Id] ASC)
);