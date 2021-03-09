

--generation QA

SELECT * FROM
(select SiteId,UnitId,RdgDateTime ,
 DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) Diff from (
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime , COUNT(*) NUM_COUNT  
from UnitReadingRegister1 where 
isnull(L2Approval,'N') = 'N'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103) 
union all 
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime , COUNT(*) NUM_COUNT  
from UnitReadingRegister2 where 
isnull(L2Approval,'N') = 'N'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)
union all
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  , COUNT(*) NUM_COUNT 
from UnitReadingRegister3 where 
isnull(L2Approval,'N') = 'N'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)) x
where DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) In (2,3,5) AND NUM_COUNT=1 ) AS A



select SiteId,UnitId,(CONVERT(VARCHAR(10), RdgDateTime, 103)) RdgDateTime ,
 DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) Diff 
 from (
select SiteId,UnitId,max(case  
dbo.GetMaxRequestDateDiff(SiteId,UnitId,1) 
when  '' then RdgDateTime 
else DATEADD(day, -1, CONVERT(datetime2, dbo.GetMaxRequestDateDiff(SiteId,UnitId,1),103)) end)  RdgDateTime  
from UnitReadingRegister1 
group by UnitId,SiteId
union all 
select SiteId,UnitId,max(case  
dbo.GetMaxRequestDateDiff(SiteId,UnitId,2) 
when  '' then RdgDateTime 
else DATEADD(day, -1, CONVERT(datetime2, dbo.GetMaxRequestDateDiff(SiteId,UnitId,2),103)) end)  RdgDateTime  
from UnitReadingRegister2 
group by UnitId,SiteId
union all
select SiteId,UnitId,max(case  
dbo.GetMaxRequestDateDiff(SiteId,UnitId,3) 
when  '' then RdgDateTime 
else DATEADD(day, -1, CONVERT(datetime2, dbo.GetMaxRequestDateDiff(SiteId,UnitId,3),103)) end)  RdgDateTime  
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
where 
DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) In (2,3,5)




select SiteId,UnitId,max(case  
dbo.GetMaxRequestDateDiff(SiteId,UnitId,1) 
when  '' then RdgDateTime 
else DATEADD(day, -1, CONVERT(datetime2, dbo.GetMaxRequestDateDiff(SiteId,UnitId,1))) end)  RdgDateTime  
from UnitReadingRegister2
group by UnitId,SiteId


select  CONVERT(datetime2,RdgDateTime,103),RdgDateTime ,dbo.GetMaxRequestDateDiff('KGT','KGT-SC1',2) func_res,
DATEADD(day, -1, CONVERT(datetime2, dbo.GetMaxRequestDateDiff('KGT','KGT-SC1',2),103)),

(case  
dbo.GetMaxRequestDateDiff(SiteId,UnitId,2) 
when  '' then RdgDateTime 
else DATEADD(day, -1, CONVERT(datetime2, dbo.GetMaxRequestDateDiff('KGT','KGT-SC1',2),103)) end)

 from (
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime , COUNT(*) NUM_COUNT  
from UnitReadingRegister2 where 
isnull(L2Approval,'N') = 'N'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)) x
where DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) In (2,3,5) AND NUM_COUNT=1 and SiteId='KGT' and UnitId='KGT-SC1'


select * from UnitReadingRegister1 order by RdgDateTime desc
select * from UnitReadingRegister2 order by RdgDateTime desc
select * from UnitReadingRegister3 order by RdgDateTime desc



select dbo.GetMaxRequestDateDiff('KGT','KGT-SC1',1)



select  * from (
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime , COUNT(*) NUM_COUNT  
from UnitReadingRegister3 where 
isnull(L2Approval,'N') = 'N'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)) x
where DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) In (2,3,5) AND NUM_COUNT=1 and SiteId='BQ2' and UnitId='BQ2-GT1'


SELECT   CONVERT(datetime2, dbo.GetMaxRequestDateDiff('BQ2','BQ2-GT1',3))

select DATEADD(day, -1, CONVERT(datetime2, dbo.GetMaxRequestDateDiff('BQ2','BQ2-GT1',3)))


alter function [dbo].[GetMaxRequestDateDiff]
(@SiteId varchar(max),@UnitId as varchar(max),@RegisterID as bigint)
RETURNS varchar(max)
as
begin

declare @RdgDateTime as varchar(max)
set @RdgDateTime=''

if @RegisterID=1
begin


select  @RdgDateTime = RdgDateTime  from (
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime , COUNT(*) NUM_COUNT  
from UnitReadingRegister1 where 
isnull(L2Approval,'XX') = 'N'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)) x
where DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) In (2,3,5) AND NUM_COUNT=1 and SiteId=@SiteId and UnitId=@UnitId


end
else if @RegisterID=2
begin

select  @RdgDateTime = RdgDateTime  from (
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime , COUNT(*) NUM_COUNT  
from UnitReadingRegister2 where 
isnull(L2Approval,'XX') = 'N'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)) x
where DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) In (2,3,5) AND NUM_COUNT=1 and SiteId=@SiteId and UnitId=@UnitId


end
else if @RegisterID=3
begin

select  @RdgDateTime = RdgDateTime  from (
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime , COUNT(*) NUM_COUNT  
from UnitReadingRegister3 where 
isnull(L2Approval,'XX') = 'N'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)) x
where 
DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) In (2,3,5) AND 
NUM_COUNT=1 and SiteId=@SiteId and UnitId=@UnitId

end


	RETURN @RdgDateTime
end
