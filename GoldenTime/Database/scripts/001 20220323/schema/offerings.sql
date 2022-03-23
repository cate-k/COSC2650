create table offerings
(
    idx         int identity
        constraint offerings_pk
            primary key,
    parentIdx   int
        constraint offerings_offerings_idx_fk
            references offerings,
    name        nvarchar(256) not null,
    description nvarchar(256),
    categoryIdx int
        constraint offerings_category_idx_fk
            references category
)
go

create unique index offerings_idx_uindex
    on offerings (idx)
go

