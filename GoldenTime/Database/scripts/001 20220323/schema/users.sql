create table users
(
    idx         int identity
        constraint users_pk
            primary key,
    email       nvarchar(512) not null,
    fullName    nvarchar(256),
    age         int,
    locationIdx int
        constraint users_location_idx_fk
            references location,
    mobile      nvarchar(16)
)
go

create unique index users_idx_uindex
    on users (idx)
go

create unique index users_email_uindex
    on users (email)
go

