
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UP_UserLogin]') AND type in (N'P'))
DROP Proc UP_UserLogin
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UP_UpdateUserPassword]') AND type in (N'P'))
DROP Proc UP_UpdateUserPassword
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UP_GetMenu]') AND type in (N'P'))
DROP Proc UP_GetMenu
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UP_FindUserList]') AND type in (N'P'))
DROP Proc UP_FindUserList
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UP_GetPermissions]') AND type in (N'P'))
DROP Proc UP_GetPermissions
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SYS_LoginLog]') AND type in (N'P'))
DROP Proc SYS_LoginLog
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UP_FindUserList]') AND type in (N'P'))
DROP Proc UP_FindUserList
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UP_AddUser]') AND type in (N'P'))
DROP Proc UP_AddUser
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UP_RecordPermission]') AND type in (N'P'))
DROP Proc UP_RecordPermission
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UP_FindExtList]') AND type in (N'P'))
DROP Proc UP_FindExtList
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UP_GetLeaveMessage]') AND type in (N'P'))
DROP Proc [UP_GetLeaveMessage]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UP_SelectRecord]') AND type in (N'P'))
DROP Proc UP_SelectRecord
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UP_DeleteUser]') AND type in (N'P'))
DROP Proc UP_DeleteUser
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UP_DeleteExt]') AND type in (N'P'))
DROP Proc UP_DeleteExt
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UP_AddOrUpdateExt]') AND type in (N'P'))
DROP Proc UP_AddOrUpdateExt
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UP_RecordPermission]') AND type in (N'P'))
DROP Proc UP_RecordPermission
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UP_ReadLeaveMessage]') AND type in (N'P'))
DROP Proc UP_ReadLeaveMessage
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[to_time]') AND type in (N'FN'))
DROP function to_time
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UP_Report_Total]') AND type in (N'P'))
DROP Proc UP_Report_Total
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UP_CallPort]') AND type in (N'P'))
DROP Proc UP_CallPort
go 
create proc UP_UserLogin
@userId varchar(16),
@password char(32),
@ip varchar(15)
with encryption as
set nocount on
declare @pwd char(32)
declare @id int 
select  @pwd = userPassword,@id = id from SYS_User where (userId=@userId or agentId =@userId) and isDelete = 0
if(@pwd is null)
	return -2
else if (@pwd<>@password)
	return -1
else begin
	update SYS_User set lastLoginTime = GETDATE(),lastLoginIp=@ip where id = @id
	insert into SYS_LoginLog(userId,ip)values(@id,@ip)
	select su.userId,agentId,su.permission,se.ext,text,sp.id
	--,urp.agent as agents,urp.ext as exts
	from SYS_User su 
	left join SYS_Ext se on su.lastLoginIp=se.userIp
	left join SYS_Permissions sp on sp.id = su.PermId
	--left join User_RecordPermission urp on su.id = urp.userId
	where su.id=@id
	set nocount off
	return 1
end
go
create proc UP_UpdateUserPassword
@userId varchar(16),
@oldpassword char(32),
@newpassword char(32)
with encryption as
update SYS_User set userPassword=@newpassword where userId = @userId and userPassword = @oldpassword
go

create proc UP_GetMenu
@id int,
@permission varchar(2000)=null
with encryption as
if(@id=1)
select * from SYS_Menu
else if(@id=2)
select * from SYS_Menu where adminlook = 1 and look = 1
else 
select * from SYS_Menu where userlook = 1 and look = 1 and @permission like '%,'+CAST(id as varchar(10))+',%'
go
select *from SYS_LoginLog
declare @r int
exec @r=UP_UserLogin 'admin','21232f297a57a5a743894a0e4a801fc3','127.0.0.1'
print @r

exec UP_GetMenu 2,',1,2,3,4,'
go

create proc UP_FindUserList
@begin int,
@end int,
@userId varchar(16)='%',
@userName varchar(20)='%',
@agentId varchar(10)='%',
@permId int = null,
@isDelete bit = null
with encryption as
select * from (
  select 
  ROW_NUMBER() over(order by lastLoginTime desc) as row,
  su.id,su.userId,agentId,userName,lastLoginTime,lastLoginIp,isDelete,su.allowDelete,text,su.permId--,urp.ext as exts, urp.agent as agents
  from SYS_User su
  left join SYS_Permissions sp on su.permId = sp.id
  --left join User_RecordPermission urp on urp.userId = su.id
  where sp.isShow = 1
  and su.userId like @userId
  and (userName like @userName or @userName='%')
  and (agentId like @agentId or @agentId='%')
  and (@permId is null or permId = @permId)
  and (@isDelete is null or isDelete = @isDelete)
)f where row between @begin and @end
return (
  select count(*)from SYS_User su
  left join SYS_Permissions sp on su.permId = sp.id
  where sp.isShow = 1
  and userId like @userId
  and (userName like @userName or @userName='%')
  and (agentId like @agentId or @agentId='%')
  and (@permId is null or permId = @permId)
  and (@isDelete is null or isDelete = @isDelete)
)
go
declare @r int
exec @r=UP_FindUserList 0,10
print @r
go
create proc UP_GetPermissions
with encryption as
select id,text from SYS_Permissions where isShow=1
go

create proc UP_AddUser
@id int=null, 
@userId varchar(16), 
@password char(32)=null,
@userName varchar(20), 
@agentId varchar(10)=null,
@permId int,
@permission varchar(2000)=null
with encryption as
if(@id is not null)
	update SYS_User set userName = @userName,agentId=@agentId,permId=@permId where id=@id
else
begin
	if(@permission is null)
		select @permission=permission from SYS_Permissions where id = @permId
	insert into SYS_User(userId,userPassword,userName,agentId,permission,permId)values(@userId,@password,@userName,@agentId,@permission,@permId)
end
go
select * from SYS_User
go
--insert into SYS_Area select * from WebIM.dbo.SYS_Area
create proc UP_DeleteUser
@id int 
with encryption as
update SYS_User set isDelete = 1 where id = @id
go
create proc UP_DeleteExt
@ext int 
with encryption as
delete TS_RECDEV where TS_DEVICENUMBER= @ext
go
exec UP_GetMenu 0,'1,2,3,4,5,6,7,8,9'

go
create proc UP_AddOrUpdateExt
@ext varchar(10),
@userIp varchar(15)='',
@ip varchar(15)='',
@mac char(17)='',
@userName varchar(20)=''
with encryption as
declare @has int 
select @has=count(*) from  SYS_Ext where ext = @ext
if(@has=0)
insert into SYS_Ext(ext,ip,mac,userName,userIp)values(@ext,@ip,@mac,@userName,@userIp)
else
update SYS_Ext set ip=@ip ,mac=@mac,userName=@userName,userIp=@userIp where ext = @ext
go
alter proc UP_AddOrUpdateExt
@ext varchar(10),
@userIp varchar(15)='',
@ip varchar(15)='',
@mac char(17)='',
@userName varchar(20)=''
with encryption as
declare @has int 
select @has=count(*) from  SYS_Ext where ext = @ext
if(@has=0)
insert into TS_RECDEV(
TS_DEVICENUMBER,
TS_IPADDRESS,
TS_MACADDRESS,
TS_AGENTID,
TS_ALIAS,TS_SERVERFLAG)values(@ext,@ip,@mac,@userName,@userIp,1)
else
update TS_RECDEV 
set TS_IPADDRESS=@ip ,
TS_MACADDRESS=@mac,
TS_AGENTID=@userName,
TS_ALIAS=@userIp 
where TS_DEVICENUMBER = @ext
go
--exec UP_FindExtList 1,30
create proc UP_FindExtList
@begin int,
@end int,
@ext varchar(10)='',
@userIp varchar(15)='',
@ip varchar(15)='',
@mac varchar(17)='',
@userName varchar(20)=''
with encryption as
select * from (
  select 
  ROW_NUMBER() over(order by ext desc) as row,
  *
  from SYS_Ext
  where ext like '%'+@ext+'%'
  and userIp like '%'+@userIp+'%'
  and ip like '%'+@ip+'%'
  and mac like '%'+@mac+'%'
  and userName like '%'+@userName+'%'
)f where row between @begin and @end
return (
  select count(*)from SYS_Ext
   where ext like '%'+@ext+'%'
  and userIp like '%'+@userIp+'%'
  and ip like '%'+@ip+'%'
  and mac like '%'+@mac+'%'
  and userName like '%'+@userName+'%'
)
go
alter proc UP_FindExtList
@begin int,
@end int,
@ext varchar(10)='',
@userIp varchar(15)='',
@ip varchar(15)='',
@mac varchar(17)='',
@userName varchar(20)=''
with encryption as
select 
TS_DEVICENUMBER as ext,
TS_ALIAS as userIp, 
TS_IPADDRESS as ip,
TS_MACADDRESS as mac,
TS_AGENTID as userName
from (
  select 
  ROW_NUMBER() over(order by TS_DEVICENUMBER desc) as row,
  *
  from TS_RECDEV
  where TS_DEVICENUMBER like '%'+@ext+'%'
  and TS_ALIAS like '%'+@userIp+'%'
  and TS_IPADDRESS like '%'+@ip+'%'
  and TS_MACADDRESS like '%'+@mac+'%'
  and TS_AGENTID like '%'+@userName+'%'
)f where row between @begin and @end
return (
  select count(*)from TS_RECDEV
   where TS_DEVICENUMBER like '%'+@ext+'%'
  and TS_ALIAS like '%'+@userIp+'%'
  and TS_IPADDRESS like '%'+@ip+'%'
  and TS_MACADDRESS like '%'+@mac+'%'
  and TS_AGENTID like '%'+@userName+'%'
)
go
exec UP_FindExtList 1,100
go

create proc UP_RecordPermission
@userId int,
@exts text,
@agents text
as
--if exists (select * from User_RecordPermission where userId=@userId)
--update  User_RecordPermission set ext=@exts,agent= @agents where  userId=@userId
--else
--insert into User_RecordPermission(userId,ext,agent) values(@userId,@exts,@agents)

go
create proc [dbo].[UP_GetLeaveMessage]
@begin int,
@end int,
@begindate datetime = null,
@enddate datetime = null,
@Status int = null,
@CallNum varchar(30) = null
as
select * from(
select ROW_NUMBER() over(order by RecDate desc) as num, * from RECORD 
where (@CallNum is null or CallingNum like '%'+@CallNum+'%')
and (@Status is null or Status =@Status)
and (@begindate is null or RecDate >= @begindate)
and (@enddate is null or RecDate <= @enddate)
)f where num between @begin and @end

select count(*) from RECORD 
where (@CallNum is null or CallingNum like '%'+@CallNum+'%')
and (@Status is null or Status =@Status)
and (@begindate is null or RecDate >= @begindate)
and (@enddate is null or RecDate <= @enddate)
GO
create proc UP_ReadLeaveMessage
@id int
as
update RECORD set Status=1 where id = @id
go
create function to_time( @seconds bigint )
returns varchar(12)
as
begin
-- declare @seconds bigint
-- set @seconds=3600
declare @hour int
decLARE @Minutes int
declare @second int
declare @mod int
if len(@seconds)>0
begin
set @hour = @seconds/3600
set @minutes = (@seconds%3600)/60
set @second =@seconds%60
end
return(cast(@hour as varchar(2)) +':' +cast( @minutes as varchar(2)) +':' + cast(@second as varchar(2)) )
end
go
create proc UP_SelectRecord
@begin int=1,
@end int=10,
@ext varchar(20)=null,
@caller varchar(20)=null,
@called varchar(20)=null,
@max int=null,
@min int=null,
@type int=null,
@beginDate datetime=null,
@endDate datetime=null,
@agentid varchar(10)=null,
@serverfalg varchar(10)=null--,
--@ext varchar(10)=null--,
--@checked int=0
as
select 
/*
TS_ID,
TS_NUMBER,
userName,
TS_CHANNEL,
case [TS_TYPE] when 0 then '呼入' when 1 then '转移' when 2 then '三方'  when 3 then '呼出' end as calltype,
TS_CALLER,
TS_CALLED,
CONVERT(varchar(100),TS_START_TIME, 120),
dbo.to_time(datediff(second,ts_start_time,ts_end_time)),
TS_AGENT_ID,
'',
@checked*/*
from (
select
ROW_NUMBER() over(order by ts_start_Time desc)as num,* from V_SelectRecord
where (@caller is null or TS_CALLER like '%'+@caller+'%')
and (@called is null or TS_CALLED like '%'+@called+'%')
and (@ext is null or TS_NUMBER like '%'+@ext+'%')
and (@called is null or TS_CALLED like '%'+@called+'%')
and (@type is null or TS_Type =@type)
and (@endDate is null or TS_START_TIME <=@endDate)
and (@beginDate is null or TS_START_TIME >=@beginDate)
and (@max is null or datediff(second,ts_start_time,ts_end_time) <=@max)
and (@min is null or datediff(second,ts_start_time,ts_end_time) >=@min)
and (@serverfalg is null or TS_SERVERFLAG = @serverfalg)
and (@ext is null or TS_NUMBER like '%'+@ext+'%')
)f 
where f.num between @begin and @end

select COUNT(*) from V_SelectRecord
where (@caller is null or TS_CALLER like '%'+@caller+'%')
and (@called is null or TS_CALLED like '%'+@called+'%')
and (@ext is null or TS_NUMBER like '%'+@ext+'%')
and (@type is null or TS_Type =@type)
and (@endDate is null or TS_START_TIME <=@endDate)
and (@beginDate is null or TS_START_TIME >=@beginDate)
and (@max is null or datediff(second,ts_start_time,ts_end_time) <=@max)
and (@min is null or datediff(second,ts_start_time,ts_end_time) >=@min)
and (@serverfalg is null or TS_SERVERFLAG = @serverfalg)
and (@ext is null or TS_NUMBER like '%'+@ext+'%')
/*
select '分机号,用户名,通道,状态,主叫号码,被叫号码,通话开始时间,时长,坐席编号,客户编号,选择,播放,下载' as Header,
'left,left,left,left,left,left,left,left,left,left,center,left,left' as ColAlign,
'ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ch,link,link' as ColTypes,
'str,str,str,str,str,str,str,int,str,str,int,str,str' as ColSorting*/

go
create proc UP_Report_Total
@datetime datetime = null,
@type int = 1
as
if(@datetime IS null)
set @datetime = GETDATE()
if(@type=1)
begin
	declare @t1 table(
	[name] varchar(20),
	[id] varchar(20),
	[all] int,
	['0-1'] int,
	['1-2'] int,
	['2-3'] int,
	['3-4'] int,
	['4-5'] int,
	['5-6'] int,
	['6-7'] int,
	['7-8'] int,
	['8-9'] int,
	['9-10'] int,
	['10-11'] int,
	['11-12'] int,
	['12-13'] int,
	['13-14'] int,
	['14-15'] int,
	['15-16'] int,
	['16-17'] int,
	['17-18'] int,
	['18-19'] int,
	['19-20'] int,
	['20-21'] int,
	['21-22'] int,
	['22-23'] int,
	['23-24'] int
	)
	insert into @t1 select 
	anl.name,
	anl.id,
	SUM(case when CAST(TS_BEGIN_TIME as time) between '0:0:0' and '23:59:59:999' then 1 else 0 end)as 'all',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '0:0:0' and '1:0:0' then 1 else 0 end) as '0-1',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '1:0:0' and '2:0:0' then 1 else 0 end) as '1-2',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '2:0:0' and '3:0:0' then 1 else 0 end) as '2-3',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '3:0:0' and '4:0:0' then 1 else 0 end) as '3-4',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '4:0:0' and '5:0:0' then 1 else 0 end) as '4-5',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '5:0:0' and '6:0:0' then 1 else 0 end) as '5-6',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '6:0:0' and '7:0:0' then 1 else 0 end) as '6-7',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '7:0:0' and '8:0:0' then 1 else 0 end) as '7-8',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '8:0:0' and '9:0:0' then 1 else 0 end) as '8-9',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '9:0:0' and '10:0:0' then 1 else 0 end) as '9-10',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '10:0:0' and '11:0:0' then 1 else 0 end) as '10-11',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '11:0:0' and '12:0:0' then 1 else 0 end) as '11-12',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '12:0:0' and '13:0:0' then 1 else 0 end) as '12-13',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '13:0:0' and '14:0:0' then 1 else 0 end) as '13-14',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '14:0:0' and '15:0:0' then 1 else 0 end) as '14-15',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '15:0:0' and '16:0:0' then 1 else 0 end) as '15-16',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '16:0:0' and '17:0:0' then 1 else 0 end) as '16-17',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '17:0:0' and '18:0:0' then 1 else 0 end) as '17-18',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '18:0:0' and '19:0:0' then 1 else 0 end) as '18-19',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '19:0:0' and '20:0:0' then 1 else 0 end) as '19-20',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '20:0:0' and '21:0:0' then 1 else 0 end) as '20-21',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '21:0:0' and '22:0:0' then 1 else 0 end) as '21-22',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '22:0:0' and '23:0:0' then 1 else 0 end) as '22-23',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '23:0:0' and '23:59:59' then 1 else 0 end) as '23-0'
	from V_AgentNameList anl left join (select * from dbo.TS_AGENT_LOG where CAST(TS_BEGIN_TIME as DATE)= CAST(@datetime as DATE)) al on anl.id=al.TS_AGENT_ID
	--where (TS_AGENT_ID<>'' or TS_AGENT_ID<>null)
	group by anl.id,anl.name
	select * from @t1
	union 
	select 
	'',
	'',
	ISNULL(SUM([all]),0),
	ISNULL(SUM(['0-1']),0),
	ISNULL(SUM(['1-2']),0),
	ISNULL(SUM(['2-3']),0),
	ISNULL(SUM(['3-4']),0),
	ISNULL(SUM(['4-5']),0),
	ISNULL(SUM(['5-6']),0),
	ISNULL(SUM(['6-7']),0),
	ISNULL(SUM(['7-8']),0),
	ISNULL(SUM(['8-9']),0),
	ISNULL(SUM(['9-10']),0),
	ISNULL(SUM(['10-11']),0),
	ISNULL(SUM(['11-12']),0),
	ISNULL(SUM(['12-13']),0),
	ISNULL(SUM(['13-14']),0),
	ISNULL(SUM(['14-15']),0),
	ISNULL(SUM(['15-16']),0),
	ISNULL(SUM(['16-17']),0),
	ISNULL(SUM(['17-18']),0),
	ISNULL(SUM(['18-19']),0),
	ISNULL(SUM(['19-20']),0),
	ISNULL(SUM(['20-21']),0),
	ISNULL(SUM(['21-22']),0),
	ISNULL(SUM(['22-23']),0),
	ISNULL(SUM(['23-24']),0)
	from @t1
select 
'姓名,工号,所有,1点,2点,3点,4点,5点,6点,7点,8点,9点,10点,11点,12点,13点,14点,15点,16点,17点,18点,19点,20点,21点,22点,23点,24点' as Header,
'left,left,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center' as ColAlign,
'ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro' as ColTypes,
'str,str,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int' as ColSorting,
'50,50,50,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40' as Width
end
else if(@type=2)
begin
	declare @t2 table(
		[name] varchar(20),
		[id] varchar(20),
		[all] int,
		[1] int,
		[2] int,
		[3] int,
		[4] int,
		[5] int,
		[6] int,
		[7] int,
		[8] int,
		[9] int,
		[10] int,
		[11] int,
		[12] int,
		[13] int,
		[14] int,
		[15] int,
		[16] int,
		[17] int,
		[18] int,
		[19] int,
		[20] int,
		[21] int,
		[22] int,
		[23] int,
		[24] int,
		[25] int,
		[26] int,
		[27] int,
		[28] int,
		[29] int,
		[30] int,
		[31] int
	)
	insert into @t2  select 
	anl.name,
	anl.id,
	SUM(case when Day(TS_BEGIN_TIME) between 1 and 31 then 1 else 0 end)as 'all',
	SUM(case Day(TS_BEGIN_TIME) when 1 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 2 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 3 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 4 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 5 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 6 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 7 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 8 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 9 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 10 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 11 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 12 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 13 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 14 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 15 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 16 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 17 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 18 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 19 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 20 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 21 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 22 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 23 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 24 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 25 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 26 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 27 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 28 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 29 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 30 then 1 else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 31 then 1 else 0 end)
	from V_AgentNameList anl left join (select * from dbo.TS_AGENT_LOG where year(TS_BEGIN_TIME)= year(@datetime) and Month(TS_BEGIN_TIME) = Month(@datetime) ) al on anl.id=al.TS_AGENT_ID
	--where (TS_AGENT_ID<>'' or TS_AGENT_ID<>null)
	group by anl.id,anl.name
	select * from @t2
	union 
	select 
	'',
	'',
	ISNULL(SUM([all]),0),
	ISNULL(SUM([1]),0),
	ISNULL(SUM([2]),0),
	ISNULL(SUM([3]),0),
	ISNULL(SUM([4]),0),
	ISNULL(SUM([5]),0),
	ISNULL(SUM([6]),0),
	ISNULL(SUM([7]),0),
	ISNULL(SUM([8]),0),
	ISNULL(SUM([9]),0),
	ISNULL(SUM([10]),0),
	ISNULL(SUM([11]),0),
	ISNULL(SUM([12]),0),
	ISNULL(SUM([13]),0),
	ISNULL(SUM([14]),0),
	ISNULL(SUM([15]),0),
	ISNULL(SUM([16]),0),
	ISNULL(SUM([17]),0),
	ISNULL(SUM([18]),0),
	ISNULL(SUM([19]),0),
	ISNULL(SUM([20]),0),
	ISNULL(SUM([21]),0),
	ISNULL(SUM([22]),0),
	ISNULL(SUM([23]),0),
	ISNULL(SUM([24]),0),
	ISNULL(SUM([25]),0),
	ISNULL(SUM([26]),0),
	ISNULL(SUM([27]),0),
	ISNULL(SUM([28]),0),
	ISNULL(SUM([29]),0),
	ISNULL(SUM([30]),0),
	ISNULL(SUM([31]),0)
	from @t2
	
select 
'姓名,工号,所有,1号,2号,3号,4号,5号,6号,7号,8号,9号,10号,11号,12号,13号,14号,15号,16号,17号,18号,19号,20号,21号,22号,23号,24号,25号,26号,27号,28号,29号,30号,31号' as Header,
'left,left,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center' as ColAlign,
'ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro' as ColTypes,
'str,str,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int' as ColSorting,
'50,50,50,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40' as Width
end
else if(@type=3)
begin
	declare @t3 table(
		[name] varchar(20),
		[id] varchar(20),
		[all] int,
		[1] int,
		[2] int,
		[3] int,
		[4] int,
		[5] int,
		[6] int,
		[7] int,
		[8] int,
		[9] int,
		[10] int,
		[11] int,
		[12] int
	)
	insert into @t3  select 
	anl.name,
	anl.id,
	SUM(case when Month(TS_BEGIN_TIME) between 1 and 12 then 1 else 0 end)as 'all',
	SUM(case Month(TS_BEGIN_TIME) when 1 then 1 else 0 end),
	SUM(case Month(TS_BEGIN_TIME) when 2 then 1 else 0 end),
	SUM(case Month(TS_BEGIN_TIME) when 3 then 1 else 0 end),
	SUM(case Month(TS_BEGIN_TIME) when 4 then 1 else 0 end),
	SUM(case Month(TS_BEGIN_TIME) when 5 then 1 else 0 end),
	SUM(case Month(TS_BEGIN_TIME) when 6 then 1 else 0 end),
	SUM(case Month(TS_BEGIN_TIME) when 7 then 1 else 0 end),
	SUM(case Month(TS_BEGIN_TIME) when 8 then 1 else 0 end),
	SUM(case Month(TS_BEGIN_TIME) when 9 then 1 else 0 end),
	SUM(case Month(TS_BEGIN_TIME) when 10 then 1 else 0 end),
	SUM(case Month(TS_BEGIN_TIME) when 11 then 1 else 0 end),
	SUM(case Month(TS_BEGIN_TIME) when 12 then 1 else 0 end)
	from V_AgentNameList anl left join (select * from dbo.TS_AGENT_LOG where year(TS_BEGIN_TIME)= year(@datetime)) al on anl.id=al.TS_AGENT_ID
	--where (TS_AGENT_ID<>'' or TS_AGENT_ID<>null)
	group by anl.id,anl.name
	select * from @t3
	union 
	select 
	'',
	'',
	ISNULL(SUM([all]),0),
	ISNULL(SUM([1]),0),
	ISNULL(SUM([2]),0),
	ISNULL(SUM([3]),0),
	ISNULL(SUM([4]),0),
	ISNULL(SUM([5]),0),
	ISNULL(SUM([6]),0),
	ISNULL(SUM([7]),0),
	ISNULL(SUM([8]),0),
	ISNULL(SUM([9]),0),
	ISNULL(SUM([10]),0),
	ISNULL(SUM([11]),0),
	ISNULL(SUM([12]),0)
	from @t3
select 
'姓名,工号,所有,1月,2月,3月,4月,5月,6月,7月,8月,9月,10月,11月,12月' as Header,
'left,left,center,center,center,center,center,center,center,center,center,center,center,center,center' as ColAlign,
'ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro' as ColTypes,
'str,str,int,int,int,int,int,int,int,int,int,int,int,int,int' as ColSorting,
'50,50,50,40,40,40,40,40,40,40,40,40,40,40,40' as Width
end
go

create proc UP_Report_DateTotal
@datetime datetime = null,
@type int = 1
as
if(@datetime IS null)
set @datetime = getdate()
if(@type=1)
begin
	declare @t1 table(
	[name] varchar(20),
	[id] varchar(20),
	[all] int,
	['0-1'] int,
	['1-2'] int,
	['2-3'] int,
	['3-4'] int,
	['4-5'] int,
	['5-6'] int,
	['6-7'] int,
	['7-8'] int,
	['8-9'] int,
	['9-10'] int,
	['10-11'] int,
	['11-12'] int,
	['12-13'] int,
	['13-14'] int,
	['14-15'] int,
	['15-16'] int,
	['16-17'] int,
	['17-18'] int,
	['18-19'] int,
	['19-20'] int,
	['20-21'] int,
	['21-22'] int,
	['22-23'] int,
	['23-24'] int
	)
	insert into @t1 select 
	anl.name,
	anl.id,
	SUM(case when CAST(TS_BEGIN_TIME as time) between '0:0:0' and '23:59:59:999' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end)as 'all',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '0:0:0' and '1:0:0' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '0-1',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '1:0:0' and '2:0:0' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '1-2',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '2:0:0' and '3:0:0' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '2-3',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '3:0:0' and '4:0:0' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '3-4',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '4:0:0' and '5:0:0' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '4-5',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '5:0:0' and '6:0:0' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '5-6',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '6:0:0' and '7:0:0' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '6-7',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '7:0:0' and '8:0:0' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '7-8',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '8:0:0' and '9:0:0' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '8-9',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '9:0:0' and '10:0:0' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '9-10',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '10:0:0' and '11:0:0' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '10-11',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '11:0:0' and '12:0:0' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '11-12',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '12:0:0' and '13:0:0' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '12-13',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '13:0:0' and '14:0:0' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '13-14',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '14:0:0' and '15:0:0' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '14-15',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '15:0:0' and '16:0:0' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '15-16',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '16:0:0' and '17:0:0' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '16-17',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '17:0:0' and '18:0:0' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '17-18',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '18:0:0' and '19:0:0' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '18-19',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '19:0:0' and '20:0:0' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '19-20',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '20:0:0' and '21:0:0' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '20-21',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '21:0:0' and '22:0:0' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '21-22',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '22:0:0' and '23:0:0' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '22-23',
	SUM(case when CAST(TS_BEGIN_TIME as time) between '23:0:0' and '23:59:59' then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end) as '23-0'
	from V_AgentNameList anl left join (select * from dbo.TS_AGENT_LOG where CAST(TS_BEGIN_TIME as DATE)= CAST(@datetime as DATE)) al on anl.id=al.TS_AGENT_ID
	--where (TS_AGENT_ID<>'' or TS_AGENT_ID<>null)
	group by anl.id,anl.name
	select * from @t1
	union 
	select 
	'',
	'',
	ISNULL(SUM([all]),0),
	ISNULL(SUM(['0-1']),0),
	ISNULL(SUM(['1-2']),0),
	ISNULL(SUM(['2-3']),0),
	ISNULL(SUM(['3-4']),0),
	ISNULL(SUM(['4-5']),0),
	ISNULL(SUM(['5-6']),0),
	ISNULL(SUM(['6-7']),0),
	ISNULL(SUM(['7-8']),0),
	ISNULL(SUM(['8-9']),0),
	ISNULL(SUM(['9-10']),0),
	ISNULL(SUM(['10-11']),0),
	ISNULL(SUM(['11-12']),0),
	ISNULL(SUM(['12-13']),0),
	ISNULL(SUM(['13-14']),0),
	ISNULL(SUM(['14-15']),0),
	ISNULL(SUM(['15-16']),0),
	ISNULL(SUM(['16-17']),0),
	ISNULL(SUM(['17-18']),0),
	ISNULL(SUM(['18-19']),0),
	ISNULL(SUM(['19-20']),0),
	ISNULL(SUM(['20-21']),0),
	ISNULL(SUM(['21-22']),0),
	ISNULL(SUM(['22-23']),0),
	ISNULL(SUM(['23-24']),0)
	from @t1
select 
'姓名,工号,所有,1点,2点,3点,4点,5点,6点,7点,8点,9点,10点,11点,12点,13点,14点,15点,16点,17点,18点,19点,20点,21点,22点,23点,24点' as Header,
'left,left,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center' as ColAlign,
'ro,ro,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt' as ColTypes,
'str,str,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int' as ColSorting,
'50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50' as Width
end
else if(@type=2)
begin
	declare @t2 table(
		[name] varchar(20),
		[id] varchar(20),
		[all] int,
		[1] int,
		[2] int,
		[3] int,
		[4] int,
		[5] int,
		[6] int,
		[7] int,
		[8] int,
		[9] int,
		[10] int,
		[11] int,
		[12] int,
		[13] int,
		[14] int,
		[15] int,
		[16] int,
		[17] int,
		[18] int,
		[19] int,
		[20] int,
		[21] int,
		[22] int,
		[23] int,
		[24] int,
		[25] int,
		[26] int,
		[27] int,
		[28] int,
		[29] int,
		[30] int,
		[31] int
	)
	insert into @t2  select 
	anl.name,
	anl.id,
	SUM(case when Day(TS_BEGIN_TIME) between 1 and 31 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end)as 'all',
	SUM(case Day(TS_BEGIN_TIME) when 1 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 2 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 3 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 4 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 5 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 6 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 7 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 8 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 9 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 10 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 11 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 12 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 13 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 14 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 15 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 16 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 17 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 18 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 19 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 20 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 21 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 22 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 23 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 24 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 25 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 26 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 27 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 28 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 29 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 30 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Day(TS_BEGIN_TIME) when 31 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end)
	from V_AgentNameList anl left join (select * from dbo.TS_AGENT_LOG where year(TS_BEGIN_TIME)= year(@datetime) and Month(TS_BEGIN_TIME) = Month(@datetime) ) al on anl.id=al.TS_AGENT_ID
	--where (TS_AGENT_ID<>'' or TS_AGENT_ID<>null)
	group by anl.id,anl.name
	select * from @t2
	union 
	select 
	'',
	'',
	ISNULL(SUM([all]),0),
	ISNULL(SUM([1]),0),
	ISNULL(SUM([2]),0),
	ISNULL(SUM([3]),0),
	ISNULL(SUM([4]),0),
	ISNULL(SUM([5]),0),
	ISNULL(SUM([6]),0),
	ISNULL(SUM([7]),0),
	ISNULL(SUM([8]),0),
	ISNULL(SUM([9]),0),
	ISNULL(SUM([10]),0),
	ISNULL(SUM([11]),0),
	ISNULL(SUM([12]),0),
	ISNULL(SUM([13]),0),
	ISNULL(SUM([14]),0),
	ISNULL(SUM([15]),0),
	ISNULL(SUM([16]),0),
	ISNULL(SUM([17]),0),
	ISNULL(SUM([18]),0),
	ISNULL(SUM([19]),0),
	ISNULL(SUM([20]),0),
	ISNULL(SUM([21]),0),
	ISNULL(SUM([22]),0),
	ISNULL(SUM([23]),0),
	ISNULL(SUM([24]),0),
	ISNULL(SUM([25]),0),
	ISNULL(SUM([26]),0),
	ISNULL(SUM([27]),0),
	ISNULL(SUM([28]),0),
	ISNULL(SUM([29]),0),
	ISNULL(SUM([30]),0),
	ISNULL(SUM([31]),0)
	from @t2
	
select 
'姓名,工号,所有,1号,2号,3号,4号,5号,6号,7号,8号,9号,10号,11号,12号,13号,14号,15号,16号,17号,18号,19号,20号,21号,22号,23号,24号,25号,26号,27号,28号,29号,30号,31号' as Header,
'left,left,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center,center' as ColAlign,
'ro,ro,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt' as ColTypes,
'str,str,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int' as ColSorting,
'50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50' as Width
end
else if(@type=3)
begin
	declare @t3 table(
		[name] varchar(20),
		[id] varchar(20),
		[all] int,
		[1] int,
		[2] int,
		[3] int,
		[4] int,
		[5] int,
		[6] int,
		[7] int,
		[8] int,
		[9] int,
		[10] int,
		[11] int,
		[12] int
	)
	insert into @t3  select 
	anl.name,
	anl.id,
	SUM(case when Month(TS_BEGIN_TIME) between 1 and 12 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end)as 'all',
	SUM(case Month(TS_BEGIN_TIME) when 1 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Month(TS_BEGIN_TIME) when 2 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Month(TS_BEGIN_TIME) when 3 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Month(TS_BEGIN_TIME) when 4 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Month(TS_BEGIN_TIME) when 5 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Month(TS_BEGIN_TIME) when 6 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Month(TS_BEGIN_TIME) when 7 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Month(TS_BEGIN_TIME) when 8 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Month(TS_BEGIN_TIME) when 9 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Month(TS_BEGIN_TIME) when 10 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Month(TS_BEGIN_TIME) when 11 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end),
	SUM(case Month(TS_BEGIN_TIME) when 12 then Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) else 0 end)
	from V_AgentNameList anl left join (select * from dbo.TS_AGENT_LOG where year(TS_BEGIN_TIME)= year(@datetime)) al on anl.id=al.TS_AGENT_ID
	--where (TS_AGENT_ID<>'' or TS_AGENT_ID<>null)
	group by anl.id,anl.name
	select * from @t3
	union 
	select 
	'',
	'',
	ISNULL(SUM([all]),0),
	ISNULL(SUM([1]),0),
	ISNULL(SUM([2]),0),
	ISNULL(SUM([3]),0),
	ISNULL(SUM([4]),0),
	ISNULL(SUM([5]),0),
	ISNULL(SUM([6]),0),
	ISNULL(SUM([7]),0),
	ISNULL(SUM([8]),0),
	ISNULL(SUM([9]),0),
	ISNULL(SUM([10]),0),
	ISNULL(SUM([11]),0),
	ISNULL(SUM([12]),0)
	from @t3
select 
'姓名,工号,所有,1月,2月,3月,4月,5月,6月,7月,8月,9月,10月,11月,12月' as Header,
'left,left,center,center,center,center,center,center,center,center,center,center,center,center,center' as ColAlign,
'ro,ro,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt,dt' as ColTypes,
'str,str,int,int,int,int,int,int,int,int,int,int,int,int,int' as ColSorting,
'50,50,50,50,50,50,50,50,50,50,50,50,50,50,50' as Width
end
GO

go
create proc UP_CallPort
@begin datetime = null,
@end datetime = null,
@type int = null
as 
declare @t table(id int identity(1,1),title varchar(20),[all] int,[callin]int,[callout] int,[callother] int,[ask] int,[askcallin]int,[askcallout]int,[noask]int,[noaskcallin]int, [noaskcallout]int,[timeout] int)
insert into @t select '总数',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
if(@type = 3)
begin
insert into @t select '一月',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where month([time])=1 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '二月',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where month([time])=2 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '三月',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where month([time])=3 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '四月',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where month([time])=4 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '五月',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where month([time])=5 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '六月',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where month([time])=6 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '七月',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where month([time])=7 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '八月',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where month([time])=8 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '九月',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where month([time])=9 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '十月',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where month([time])=10 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '十一月',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where month([time])=11 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '十二月',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where month([time])=12 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
end
else if(@type = 2)
begin
insert into @t select '1号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=1 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '2号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=2 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '3号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=3 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '4号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=4 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '5号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=5 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '6号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=6 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '7号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=7 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '8号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=8 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '9号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=9 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '10号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=10 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '11号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=11 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '12号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=12 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '13号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=13 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '14号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=14 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '15号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=15 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '16号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=16 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '17号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=17 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '18号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=18 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '19号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=19 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '20号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=20 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '21号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=21 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '22号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=22 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '23号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=23 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '24号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=24 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '25号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=25 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '26号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=26 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '27号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=27 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '28号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=28 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '29号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=29 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '30号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=30 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '31号',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where day([time])=31 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
end
else if(@type = 1)
begin
insert into @t select '1点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=1 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '2点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=2 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '3点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=3 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '4点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=4 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '5点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=5 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '6点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=6 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '7点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=7 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '8点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=8 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '9点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=9 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '10点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=10 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '11点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=11 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '12点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=12 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '13点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=13 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '14点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=14 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '15点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=15 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '16点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=16 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '17点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=17 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '18点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=18 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '19点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=19 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '20点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=20 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '21点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=21 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '22点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=22 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '23点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=23 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
insert into @t select '24点',SUM([all]) as 'all',SUM([callin]) as 'callin',SUM([callout]) as 'callout',SUM([callother]) as 'callother',SUM([ask]) as 'ask',SUM([askcallin]) as 'askcallin',SUM([askcallout]) as 'askcallout',SUM([noask]) as 'noask',SUM([noaskcallin]) as 'noaskcallin',SUM([noaskcallout]) as 'noaskcallout',SUM([timeout]) as 'timeout'  from V_CallPort where Datepart(HOUR,[time])=24 and (@begin is null or [time] > @begin) and (@end is null or [time] > @end)
end
select title,ISNULL([all],0) as [all],ISNULL([callin],0) as [callin],ISNULL([callout],0) as [callout],ISNULL([callother],0) as [callother],ISNULL([ask],0) as [ask],ISNULL([askcallin],0) as [askcallin],ISNULL([askcallout],0) as [askcallout],ISNULL([noask],0) as [noask],ISNULL([noaskcallin],0) as [noaskcallin],ISNULL([noaskcallout],0) as [noaskcallout],ISNULL([timeout],0) as [timeout] from @t order by id

select 
'分类,所有,呼入,呼出,其它,应答,应答呼入,应答呼出,无应答,无应答呼入,无应答呼出,呼入无应答超时' as Header,
'left,left,center,center,center,center,center,center,center,center,center,center' as ColAlign,
'ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro' as ColTypes,
'str,str,int,int,int,int,int,int,int,int,int,int' as ColSorting,
'50,50,50,50,50,50,50,100,100,100,100,100' as Width
go

create proc UP_CallTotal
@begin datetime = null,
@end datetime = null,
@type int =null
as
select top 200 ROW_NUMBER() over(order by TS_BEGIN_TIME desc) id,
case TS_CATEGORY when 3 then '呼出' else '呼入' end as '呼叫状态',
isnull(anl.name,'') as '坐席',
TS_EXTION as '分机号码',
case TS_CATEGORY when 3 then TS_OTHER_PARTY else TS_THIS_PARTY end as '被叫号码',
case TS_CATEGORY when 3 then TS_THIS_PARTY else TS_OTHER_PARTY end as '主叫号码',
TS_BEGIN_TIME as '电话进入时间',
TS_PICKUP_TIME as '电话接听时间',
TS_END_TIME as '挂机时间',
Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME) as '通话时常'
from TS_AGENT_LOG al left join V_AgentNameList anl on  al.TS_AGENT_ID = anl.id
where Datediff(SECOND,TS_PICKUP_TIME,TS_END_TIME)>=0
and (TS_AGENT_ID<>'' or TS_AGENT_ID is  not null)
and (TS_OTHER_PARTY<>'' or TS_THIS_PARTY<>'')
and (@begin is null or TS_BEGIN_TIME>@begin)
and (@end is null or TS_BEGIN_TIME<@end)
--and TS_BEGIN_TIME between '{0:yyyy-MM-dd}'and '{1:yyyy-MM-dd}'
--and TS_CATEGORY=3

select 
'行号,呼叫状态,坐席,分机号码,被叫号码,主叫号码,电话进入时间,电话接听时间,挂机时间,通话时常' as Header,
'left,left,left,center,center,center,center,center,center,center' as ColAlign,
'ro,ro,ro,ro,ro,ro,ro,ro,ro,dt' as ColTypes,
'int,str,str,int,int,int,int,int,int,int' as ColSorting,
'50,100,100,100,100,100,130,130,130,100' as Width
go