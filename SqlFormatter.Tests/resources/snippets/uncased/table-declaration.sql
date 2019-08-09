create table [dbo].[taco] (
    [a] int identity (1, 1) not null,
    [b]	varchar (1)  null,
    [c] datetime2(1)  not null,
    [d] int constraint [DF_taco_topping] default (1) not null,
    constraint [PK_taco] primary key clustered ([id] desc),
    constraint [FK_taco_a] foreign key ([a]) references [dbo].[burrito] ([a])
);

GO

create unique index [IX_taco_c] 
    on [dbo].[taco]([c] desc)
    include ([b], [d])

GO
