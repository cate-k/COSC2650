create table location
(
    idx           int identity
        constraint location_pk
            primary key,
    areaCode      nvarchar(64) not null,
    caption       nvarchar(256),
    description   nvarchar(max),
    country       nvarchar(256),
    state         nvarchar(256),
    weakLongitude decimal(10, 8),
    weakLatitude  decimal(10, 8)
)
go

create unique index location_idx_uindex
    on location (idx)
go

create index location_areaCode_index
    on location (areaCode)
go

