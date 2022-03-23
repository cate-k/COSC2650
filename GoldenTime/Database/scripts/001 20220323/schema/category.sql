create table category
(
    idx          int identity
        constraint category_pk
            primary key,
    name         nvarchar(256),
    parentIdx    int
        constraint category_category_idx_fk
            references category,
    categoryType nvarchar(256)
)
go

exec sp_addextendedproperty 'MS_Description', 'Optional
', 'SCHEMA', 'dbo', 'TABLE', 'category', 'COLUMN', 'parentIdx'
go

exec sp_addextendedproperty 'MS_Description', 'Category can be for preference, post, offering,...', 'SCHEMA', 'dbo',
     'TABLE', 'category', 'COLUMN', 'categoryType'
go

create unique index category_idx_uindex
    on category (idx)
go

