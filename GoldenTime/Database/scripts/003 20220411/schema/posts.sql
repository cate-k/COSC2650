create table posts
(
    idx         int identity
        constraint posts_pk
            primary key,
    userIdx     int                             not null
        constraint posts_users_idx_fk
            references users,
    createdOn   datetime2 default sysdatetime() not null,
    modifiedOn  datetime2,
    subject     nvarchar(1024)                  not null,
    content     nvarchar(max),
    parentIdx   int
        constraint posts_posts_idx_fk
            references posts,
    startingOn  datetime2,
    endingOn    datetime2,
    locationIdx int
        constraint posts_location_idx_fk
            references location
)
go

exec sp_addextendedproperty 'MS_Description', 'If this is <> null then the post is a reply.
', 'SCHEMA', 'dbo', 'TABLE', 'posts', 'COLUMN', 'parentIdx'
go

create unique index posts_idx_uindex
    on posts (idx)
go

create index posts_userIdx_index
    on posts (userIdx)
go

