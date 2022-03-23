create table postCategories
(
    idx         int identity
        constraint postCategories_pk
            primary key,
    postIdx     int not null
        constraint postCategories_posts_idx_fk
            references posts,
    categoryIdx int not null
        constraint postCategories_category_idx_fk
            references category
)
go

create unique index postCategories_idx_uindex
    on postCategories (idx)
go

create index postCategories_postIdx_index
    on postCategories (postIdx desc)
go

