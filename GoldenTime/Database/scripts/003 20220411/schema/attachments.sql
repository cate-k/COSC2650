use cosc2650
go

create table attachments
(
    idx         int identity
        constraint attachments_pk
            primary key,
    postIdx     int            not null
        constraint attachments_posts_idx_fk
            references posts,
    description nvarchar(256)  not null,
    filename    nvarchar(1024),
    content     varbinary(max) not null,
    mime        nvarchar(64),
    createdOn   datetime2 default sysdatetime()
)
go

create unique index attachments_idx_uindex
    on attachments (idx)
go

