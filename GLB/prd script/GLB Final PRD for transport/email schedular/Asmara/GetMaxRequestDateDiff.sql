USE [GenerationQA]
GO
/****** Object:  UserDefinedFunction [dbo].[GetMaxRequestDateDiff]    Script Date: 3/13/2020 3:44:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER function [dbo].[GetMaxRequestDateDiff]
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