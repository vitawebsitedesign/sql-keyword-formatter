CREATE TABLE [dbo].[taco] (
    [a] INT IDENTITY (1, 1) NOT NULL,
    [b]	VARCHAR (1)  NULL,
    [c] DATETIME2(1)  NOT NULL,
    [d] INT CONSTRAINT [DF_taco_topping] DEFAULT (1) NOT NULL,
    CONSTRAINT [PK_taco] PRIMARY KEY CLUSTERED ([id] DESC),
    CONSTRAINT [FK_taco_a] FOREIGN KEY ([a]) REFERENCES [dbo].[burrito] ([a])
);

GO

CREATE UNIQUE INDEX [IX_taco_c] 
    ON [dbo].[taco]([c] DESC)
    INCLUDE ([b], [d])

GO
