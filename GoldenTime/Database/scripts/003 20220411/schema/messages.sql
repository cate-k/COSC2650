create table messages
(
    idx         int identity
        constraint messages_pk
            primary key,
    senderIdx   int                             not null
        constraint messages_sender_idx_fk
            references users,
    receiverIdx int                             not null
        constraint messages_receiver_idx_fk
            references users,
    createdOn   datetime2 default sysdatetime() not null,
    modifiedOn  datetime2,
    subject     nvarchar(1024)                  not null,
    content     nvarchar(max),
    parentIdx   int
        constraint messages_messages_idx_fk
            references messages
)
go

create unique index messages_idx_uindex
    on messages (idx)
go

create index messages_receiverIdx_index
    on messages (receiverIdx)
go

create index messages_senderIdx_index
    on messages (senderIdx)
go

