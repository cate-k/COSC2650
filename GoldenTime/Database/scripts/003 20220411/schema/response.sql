create table response
(
    idx             int identity
        constraint response_pk
            primary key,
    caption         nvarchar(128) not null,
    categoryIdx     int
        constraint response_category_idx_fk
            references category,
    logicalResponse int
)
go

exec sp_addextendedproperty 'MS_Description', 'Optional category specific response.
', 'SCHEMA', 'dbo', 'TABLE', 'response', 'COLUMN', 'categoryIdx'
go

exec sp_addextendedproperty 'MS_Description', 'Used for understanding logical response of the entity:
0 - false : no,never,...
1 - true : yes,always,...
2 - unknow: maybe, unknown...
.
.
. ', 'SCHEMA', 'dbo', 'TABLE', 'response', 'COLUMN', 'logicalResponse'
go

create unique index response_idx_uindex
    on response (idx)
go

