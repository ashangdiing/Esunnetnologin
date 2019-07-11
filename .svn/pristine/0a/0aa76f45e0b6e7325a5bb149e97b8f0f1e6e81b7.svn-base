IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[V_SelectRecord]') AND type in (N'V'))
DROP view V_SelectRecord
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[V_AgentNameList]') AND type in (N'V'))
DROP view V_AgentNameList
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[V_CallPort]') AND type in (N'V'))
DROP view V_CallPort
go
create view V_SelectRecord
as
select 
*,
'' as userName,
'' as agentid,
datediff(second,ts_start_time,ts_end_time) as duration ,
case [TS_TYPE] when 0 then '呼入' when 1 then '转移' when 2 then '三方'  when 3 then '呼出' end as calltype 
from TS_REC_LOG
go
create view V_AgentNameList
as
select TS_AGENT_NAME as name,TS_AGENT_ID as id from dbo.TS_AGENT
go
create view V_CallPort
as
select 
TS_BEGIN_TIME as time,
COUNT(al.TS_LOG_ID) as 'all',
SUM(case al.TS_CATEGORY when 1 then 1 else 0 end) as 'callin',
SUM(case TS_CATEGORY when 3 then 1 else 0 end) as 'callout',
SUM(case  when TS_CATEGORY<>3 and TS_CATEGORY<>1 then 1 else 0 end) as 'callother',
SUM(case when Datediff(second,al.TS_PICKUP_TIME,al.TS_END_TIME)>0 then 1 else 0 end) as 'ask',
SUM(case when TS_CATEGORY= 1 and Datediff(second,al.TS_PICKUP_TIME,al.TS_END_TIME)>0 then 1 else 0 end) as 'askcallin',
SUM(case when TS_CATEGORY= 3 and Datediff(second,al.TS_PICKUP_TIME,al.TS_END_TIME)>0 then 1 else 0 end) as 'askcallout',
SUM(case when Datediff(second,al.TS_PICKUP_TIME,al.TS_END_TIME)=0 then 1 else 0 end) as 'noask',
SUM(case when TS_CATEGORY= 1 and Datediff(second,al.TS_PICKUP_TIME,al.TS_END_TIME)=0 then 1 else 0 end) as 'noaskcallin',
SUM(case when TS_CATEGORY= 3 and Datediff(second,al.TS_PICKUP_TIME,al.TS_END_TIME)=0 then 1 else 0 end) as 'noaskcallout',
SUM(case when TS_CATEGORY= 1 and Datediff(second,al.TS_PICKUP_TIME,al.TS_END_TIME)=0 and Datediff(second,al.TS_BEGIN_TIME,al.TS_END_TIME)>10 then 1 else 0 end) as 'timeout'
from TS_AGENT_LOG al group by al.TS_BEGIN_TIME 
