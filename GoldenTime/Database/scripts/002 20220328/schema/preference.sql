create table preference
(
    idx         int identity
        constraint preference_pk
            primary key,
    name        nvarchar(256),
    type        nvarchar(256) not null,
    dataType    nvarchar(256),
    categoryIdx int
        constraint preference_category_idx_fk
            references category
)
go

exec sp_addextendedproperty 'MS_Description', 'Helper, if we need to convert data types in code.
', 'SCHEMA', 'dbo', 'TABLE', 'preference', 'COLUMN', 'dataType'
go

exec sp_addextendedproperty 'MS_Description', 'optional, if we want to categorise', 'SCHEMA', 'dbo', 'TABLE',
     'preference', 'COLUMN', 'categoryIdx'
go

create unique index preference_idx_uindex
    on preference (idx)
go

