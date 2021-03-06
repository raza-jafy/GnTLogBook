USE [GenerationQA]
GO
/****** Object:  UserDefinedFunction [dbo].[GetRecordCountByUnitID]    Script Date: 4/5/2020 9:23:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER function [dbo].[GetRecordCountByUnitID]
(@UnitId as varchar(max),@RegisterID as bigint)
RETURNS BIGINT
as
begin

declare @recordCount bigint =0

if @RegisterID=1
begin


 select @recordCount = isnull(COUNT(*),0)  
from UnitReadingRegister1 
where 
--isnull(L2Approval,'N') = 'N' and isnull(L1Approval,'N') = 'N' and
convert(datetime, RdgDateTime, 103) 
between  CONVERT(datetime,DATEADD(day, -7, GETDATE()),103) and CONVERT(datetime,GETDATE(),103)
and UnitId=@UnitId group by UnitId 



end
else if @RegisterID=2
begin


 select @recordCount = isnull(COUNT(*),0)  
from UnitReadingRegister2 
where 
--isnull(L2Approval,'N') = 'N' and isnull(L1Approval,'N') = 'N' and
convert(datetime, RdgDateTime, 103) 
between  CONVERT(datetime,DATEADD(day, -7, GETDATE()),103) and CONVERT(datetime,GETDATE(),103)
and UnitId=@UnitId group by UnitId 

end
else if @RegisterID=3
begin


 select @recordCount = isnull(COUNT(*),0)  
from UnitReadingRegister3
where 
--isnull(L2Approval,'N') = 'N' and isnull(L1Approval,'N') = 'N' and
convert(datetime, RdgDateTime, 103) 
between  CONVERT(datetime,DATEADD(day, -7, GETDATE()),103) and CONVERT(datetime,GETDATE(),103)
and UnitId=@UnitId group by UnitId 



end


	RETURN @recordCount
end