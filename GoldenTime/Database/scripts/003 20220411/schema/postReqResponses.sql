create table postReqResponses
(
    idx             int identity
        constraint postReqResponses_pk
            primary key,
    postIdx         int                             not null
        constraint postReqResponses_posts_idx_fk
            references posts,
    responderIdx    int                             not null
        constraint postReqResponses_users_idx_fk
            references users,
    respondedOn     datetime2 default sysdatetime() not null,
    responseText    nvarchar(512),
    responseTypeIdx int                             not null
        constraint postReqResponses_response_idx_fk
            references response
)
go

create unique index postReqResponses_idx_uindex
    on postReqResponses (idx)
go

