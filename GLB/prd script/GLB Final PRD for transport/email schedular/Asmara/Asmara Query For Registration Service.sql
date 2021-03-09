
--Generation QA
select RdgDateTime  from (
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  , COUNT(*) num_records
from UnitReadingRegister1 where 
isnull(L2Approval,'N') = 'N'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)
union all 
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime   , COUNT(*) num_records
from UnitReadingRegister2 where 
isnull(L2Approval,'N') = 'N'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)
union all
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  , COUNT(*) num_records
from UnitReadingRegister3 where 
isnull(L2Approval,'N') = 'N'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)) X 
where x.num_records=1 and UnitId='BQ1-UT1' and SiteId='BQ1'


select SiteId,UnitId,max(RdgDateTime)  RdgDateTime  
from UnitReadingRegister1 where CONVERT(VARCHAR(10), RdgDateTime, 103) <> (
select RdgDateTime  from (
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  , COUNT(*) num_records
from UnitReadingRegister1 where 
isnull(L2Approval,'N') = 'N'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)
union all 
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime   , COUNT(*) num_records
from UnitReadingRegister2 where 
isnull(L2Approval,'N') = 'N'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)
union all
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  , COUNT(*) num_records
from UnitReadingRegister3 where 
isnull(L2Approval,'N') = 'N'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)) X 
where x.num_records=1 and UnitId='BQ1-UT1' and SiteId='BQ1'
)
group by UnitId,SiteId 





select SiteId,UnitId,(CONVERT(VARCHAR(10), RdgDateTime, 103)) RdgDateTime ,
 DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) Diff 
 from (
select SiteId,UnitId,max(RdgDateTime)  RdgDateTime  
from UnitReadingRegister1 
group by UnitId,SiteId 
union all 
select SiteId,UnitId,max(RdgDateTime)  RdgDateTime  
from UnitReadingRegister2 
group by UnitId,SiteId
union all
select SiteId,UnitId,max(RdgDateTime)  RdgDateTime
from UnitReadingRegister3
group by UnitId,SiteId
) x
where 
DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) In (2,3,5) 






select SiteId,UnitId,(CONVERT(VARCHAR(10), RdgDateTime, 103)) RdgDateTime ,
 DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) Diff 
 from (
select SiteId,UnitId,max(RdgDateTime)  RdgDateTime  
from UnitReadingRegister1 
group by UnitId,SiteId
union all 
select SiteId,UnitId,max(RdgDateTime)  RdgDateTime  
from UnitReadingRegister2 
group by UnitId,SiteId
union all
select SiteId,UnitId,max(RdgDateTime)  RdgDateTime
from UnitReadingRegister3
group by UnitId,SiteId
) x





where (NOT EXISTS
        (select SiteId,UnitId,RdgDateTime   from (
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  , COUNT(*) num_records
from UnitReadingRegister1 where 
isnull(L2Approval,'N') = 'N'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)
union all 
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime   , COUNT(*) num_records
from UnitReadingRegister2 where 
isnull(L2Approval,'N') = 'N'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)
union all
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  , COUNT(*) num_records
from UnitReadingRegister3 where 
isnull(L2Approval,'N') = 'N'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)) X 
where x.num_records=1 )) and DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) In (2,3,5) 