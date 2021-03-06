USE [GenerationQA]
GO
/****** Object:  StoredProcedure [dbo].[PrepareDataForsendGenLogBookNotificationMailV2]    Script Date: 4/8/2020 7:37:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[PrepareDataForsendGenLogBookNotificationMailV2]
--@UnitId varchar(max)
as
begin


DECLARE @tbl TABLE(SiteID VARCHAR(MAX) , UnitId	varchar(max),rCount	bigint,tConut  bigint, RegisterID varchar(max));  


declare @UnitId_ as varchar(max)
declare @SiteID_ as varchar(max)
declare @numCount as bigint = 1


DECLARE @bm CURSOR
SET @bm = CURSOR FOR

select distinct UnitId from UnitReadingRegister1 
union all 
select distinct UnitId from UnitReadingRegister2 
union all
select distinct UnitId from UnitReadingRegister3 

OPEN @bm
FETCH NEXT FROM @bm INTO @UnitId_
WHILE @@FETCH_STATUS = 0
BEGIN


--prepare data for notification


 if exists(select * from UnitReadingRegister1 where UnitId=@UnitId_)
 begin



--select @UnitId as UnitId , dbo.GetRecordCountByUnitID(@UnitId,1) as rcount
insert into @tbl(SiteID,UnitId,rCount,tConut,RegisterID) 
	values ((rtrim(ltrim(SUBSTRING(@UnitId_,0,CHARINDEX('-',@UnitId_))))),@UnitId_,dbo.GetRecordCountByUnitID(@UnitId_,1),175,'UnitReadingRegister1')

 end
 else if exists(select * from UnitReadingRegister2 where UnitId=@UnitId_)
 begin

 --select @UnitId as UnitId , dbo.GetRecordCountByUnitID(@UnitId,2) as rcount

 insert into @tbl(SiteID,UnitId,rCount,tConut,RegisterID) 
 values ((rtrim(ltrim(SUBSTRING(@UnitId_,0,CHARINDEX('-',@UnitId_))))),@UnitId_,dbo.GetRecordCountByUnitID(@UnitId_,2),175,'UnitReadingRegister2')


end
else if exists(select * from UnitReadingRegister3 where UnitId=@UnitId_)
 begin


 --select @UnitId as UnitId , dbo.GetRecordCountByUnitID(@UnitId,3) as rcount

 insert into @tbl(SiteID,UnitId,rCount,tConut,RegisterID) values 
 ((rtrim(ltrim(SUBSTRING(@UnitId_,0,CHARINDEX('-',@UnitId_))))),@UnitId_,dbo.GetRecordCountByUnitID(@UnitId_,3),175,'UnitReadingRegister3')

 end


 set @numCount = @numCount +1


FETCH NEXT FROM @bm INTO @UnitId_
END
CLOSE @bm
DEALLOCATE @bm


--select SiteID,UnitId,rCount,tConut,RegisterID,
--cast(CONVERT(date,DATEADD(day, -7, GETDATE()),103) as varchar(max)) FromDate, 
--cast(CONVERT(date,GETDATE(),103) as varchar(max)) ToDate
-- from @tbl ORDER BY SiteID


--select SiteID,UnitId,rCount,tConut,RegisterID,
--cast(CONVERT(date,DATEADD(day, -7, DATEADD(day, -1, GETDATE())),103)  as varchar(max)) FromDate, 
--cast(CONVERT(date,DATEADD(day, -1, GETDATE()),103) as varchar(max)) ToDate
-- from @tbl ORDER BY SiteID




 --snd data for notification-------------------
 if exists(select SiteID,UnitId,rCount,tConut,RegisterID,
 CONVERT(datetime,DATEADD(day, -7, DATEADD(day, -1, GETDATE())),103) FromDate, CONVERT(datetime,DATEADD(day, -1, GETDATE()),103) ToDate
  from @tbl where rCount<175)
 begin

 declare @TR as varchar(max)
declare @CCR as varchar(max)

declare @SiteId_NCR as varchar(max)
declare @UnitId_NCR as varchar(max)
declare @rCount_NCR as bigint
declare @tCount_NCR as bigint
declare @RegisterID_NCR as varchar(max)

declare @FromDate as varchar(max)
declare @ToDate as varchar(max)





 DECLARE @cm CURSOR
SET @cm = CURSOR FOR
select SiteID,UnitId,rCount,tConut,RegisterID,
--cast(CONVERT(date,DATEADD(day, -7, GETDATE()),103) as varchar(max)) FromDate,
--cast(CONVERT(date,GETDATE(),103) as varchar(max)) ToDate
 CONVERT(date,DATEADD(day, -7, DATEADD(day, -1, GETDATE())),103) FromDate,
 CONVERT(date,DATEADD(day, -1, GETDATE()),103) ToDate
 from @tbl where rCount<175
--ORDER BY SiteID
OPEN @cm
FETCH NEXT FROM @cm INTO @SiteId_NCR,@UnitId_NCR,@rCount_NCR,@tCount_NCR,@RegisterID_NCR,@FromDate,@ToDate
WHILE @@FETCH_STATUS = 0
BEGIN


	select @TR=''
	select @CCR=''


		SELECT @TR = COALESCE(@TR + ';','') + ISNULL(ToRecipients,'')  from xEmailReminderRecipientsForNotification
				where SiteID = @SiteId_NCR and ISNULL(ToRecipients,'')<>'' and ReminderNo<=1
				AND ACTIVE=1 AND  RType='TO'


		SELECT @CCR = COALESCE(@CCR + ';','') + ISNULL(ToRecipients,'')  from xEmailReminderRecipientsForNotification
				where SiteID = @SiteId_NCR and ISNULL(ToRecipients,'')<>'' and ReminderNo<=1
				AND ACTIVE=1 AND  RType='CC'


				SELECT @TR  =  substring(@TR,2, len(@TR))
				SELECT @CCR  =  substring(@CCR,2, len(@CCR))



					DECLARE @MAIL_BODYFirst_NCR VARCHAR(max)

					set @MAIL_BODYFirst_NCR=''

		SET @MAIL_BODYFirst_NCR = '<p>There are portal entries missing in last week date. Please update the portal at the earliest.</p><br/>'
		SELECT @MAIL_BODYFirst_NCR = 
			@MAIL_BODYFirst_NCR + '<table border="1" align="center" cellpadding="2" cellspacing="0" style="color:black;font-family:consolas;text-align:center;">' +
			'<tr>
						<th>Site ID</th>
						<th>Unit ID</th>

						<th>Record Count</th>
						<th>Total Count</th>

						<th>From Date</th>
						<th>To Date</th>
						<th>TR</th>
						<th>CR</th>
			</tr>'



 
		/* ROWS */
		SELECT
			@MAIL_BODYFirst_NCR = @MAIL_BODYFirst_NCR +
				'<tr>' +
				'<td>' + CAST(@SiteId_NCR AS VARCHAR(max)) + '</td>' +
				'<td>' + CAST(@UnitId_NCR AS VARCHAR(max)) + '</td>' +

				'<td>'+cast(@FromDate as varchar(max))+'</td>' +
				'<td>'+ cast(@ToDate as varchar(max))+'</td>' +

				'<td>' + CAST(@rCount_NCR AS VARCHAR(max)) + '</td>' +
				'<td>' + CAST(@tCount_NCR AS VARCHAR(max)) + '</td>' +


				'<td>' + CAST(@TR AS VARCHAR(max)) + '</td>' +
				'<td>' + CAST(@CCR AS VARCHAR(max)) + '</td>' +


				'</tr>'
		SELECT @MAIL_BODYFirst_NCR = @MAIL_BODYFirst_NCR + '</table>'

		--print @MAIL_BODYFirst_NCR


		--asmara.shahid@ke.com.pk
		EXEC msdb.dbo.sp_send_dbmail @profile_name='LDCGEN', @body_format = 'HTML',
		@recipients='asmara.shahid@ke.com.pk',
		@copy_recipients = 'rabha.shoaib@ke.com.pk',
		@subject= 'Reminder : Scorecard ELR Portal', @body=@MAIL_BODYFirst_NCR


		

FETCH NEXT FROM @cm INTO @SiteId_NCR,@UnitId_NCR,@rCount_NCR,@tCount_NCR,@RegisterID_NCR,@FromDate,@ToDate
END
CLOSE @cm
DEALLOCATE @cm




 end

 --snd data for notification-------------------



end