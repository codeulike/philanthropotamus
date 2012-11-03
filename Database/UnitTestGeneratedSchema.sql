
    if exists (select * from dbo.sysobjects where id = object_id(N'CharityInfos') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CharityInfos

    create table CharityInfos (
        CharityInfoId INT IDENTITY NOT NULL,
       CharityNumber INT null,
       CharityName NVARCHAR(255) null,
       Activities NVARCHAR(255) null,
       Website NVARCHAR(255) null,
       TwitterAccount NVARCHAR(255) null,
       GameScore INT null,
       primary key (CharityInfoId)
    )
