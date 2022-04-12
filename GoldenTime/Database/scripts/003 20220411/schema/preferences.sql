create table preferences
(
    idx             int identity
        constraint preferences_pk
            primary key,
    userIdx         int                             not null
        constraint preferences_users_idx_fk
            references users,
    preferenceIdx   int                             not null
        constraint preferences_preference_idx_fk
            references preference,
    preferenceValue nvarchar(256)                   not null,
    createdOn       datetime2 default sysdatetime() not null,
    updatedOn       datetime2
)
go

exec sp_addextendedproperty 'MS_Description', 'Fk to user table
', 'SCHEMA', 'dbo', 'TABLE', 'preferences', 'COLUMN', 'userIdx'
go

exec sp_addextendedproperty 'MS_Description', 'Preference index for type.
', 'SCHEMA', 'dbo', 'TABLE', 'preferences', 'COLUMN', 'preferenceIdx'
go

exec sp_addextendedproperty 'MS_Description',
     'All preferences are stored as nvarchar. If we need a preference that needs to be highly indexable, we can create a separate field.',
     'SCHEMA', 'dbo', 'TABLE', 'preferences', 'COLUMN', 'preferenceValue'
go

create unique index preferences_prefIdx_uindex
    on preferences (idx)
go

