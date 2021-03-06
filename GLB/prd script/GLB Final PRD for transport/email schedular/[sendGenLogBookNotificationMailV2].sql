USE [GenerationQA]
GO
/****** Object:  StoredProcedure [dbo].[sendGenLogBookNotificationMailV2]    Script Date: 3/13/2020 3:52:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[sendGenLogBookNotificationMailV2]
--@SiteId as varchar(max), @UnitId as varchar(max)
as
begin
--
--select DATEDIFF(d, cast('09/03/2019 9:45:48' as datetime),getdate())
--BQ1

declare @TR as varchar(max)
declare @CCR as varchar(max)


--for records that are not created yet..after discussion with asmaara--------------------->>>>>>>>>>>>>>>>>>>>>>>>>>>>>NEED TO BE TEST BEFORE DEPLOYMENT
if exists(



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


--ORIGINAL LOGIC BEFORE FUNCTIONAL IMPLEMENTATION>>>>>
--select SiteId,UnitId,(CONVERT(VARCHAR(10), RdgDateTime, 103)) RdgDateTime ,
-- DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) Diff 
-- from (
--select SiteId,UnitId,max(RdgDateTime)  RdgDateTime  
--from UnitReadingRegister1 
--group by UnitId,SiteId
--union all 
--select SiteId,UnitId,max(RdgDateTime)  RdgDateTime  
--from UnitReadingRegister2 
--group by UnitId,SiteId
--union all
--select SiteId,UnitId,max(RdgDateTime)  RdgDateTime  
--from UnitReadingRegister3
--group by UnitId,SiteId
--) x
--where 
--DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) In (2,3,5)
--ORIGINAL LOGIC BEFORE FUNCTIONAL IMPLEMENTATION>>>>>

)
begin

declare @numCountNCR as bigint = 1
declare @RtnCodeNCR as bigint
declare @SiteId_NCR as varchar(max)
declare @UnitId_NCR as varchar(max)
declare @RdgDateTime_NCR as varchar(max)
declare @RdgDatediff_NCR as varchar(max)


DECLARE @bmNCR CURSOR
SET @bmNCR = CURSOR FOR


--ORIGINAL LOGIC BEFORE FUNCTIONAL IMPLEMENTATION>>>>>
--select SiteId,UnitId,(CONVERT(VARCHAR(10), RdgDateTime, 103)) RdgDateTime ,
-- DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) Diff 
-- from (
--select SiteId,UnitId,max(RdgDateTime)  RdgDateTime  
--from UnitReadingRegister1 
--group by UnitId,SiteId
--union all 
--select SiteId,UnitId,max(RdgDateTime)  RdgDateTime  
--from UnitReadingRegister2 
--group by UnitId,SiteId
--union all
--select SiteId,UnitId,max(RdgDateTime)  RdgDateTime  
--from UnitReadingRegister3
--group by UnitId,SiteId
--) x
--where 
--DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) In (2,3,5)
--ORIGINAL LOGIC BEFORE FUNCTIONAL IMPLEMENTATION>>>>>


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




OPEN @bmNCR
FETCH NEXT FROM @bmNCR INTO @SiteId_NCR,@UnitId_NCR,@RdgDateTime_NCR,@RdgDatediff_NCR
WHILE @@FETCH_STATUS = 0
BEGIN

--After 2 days of L2 entry not approved.
if ((DATEDIFF(d,convert(datetime, @RdgDateTime_NCR, 103),GETDATE())=2))
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
		SET @MAIL_BODYFirst_NCR = '<p>Registration is pending since a day,Please update the portal at the earliest</p><br/>'
		SELECT @MAIL_BODYFirst_NCR = 
			@MAIL_BODYFirst_NCR + '<table border="1" align="center" cellpadding="2" cellspacing="0" style="color:black;font-family:consolas;text-align:center;">' +
			'<tr>
						<th>Site ID</th>
						<th>Unit ID</th>
						<th>Last Registration Date</th>
						<th>TR</th>
						<th>CR</th>
			</tr>'
 
		/* ROWS */
		SELECT
			@MAIL_BODYFirst_NCR = @MAIL_BODYFirst_NCR +
				'<tr>' +
				'<td>' + CAST(@SiteId_NCR AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(@UnitId_NCR AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(@RdgDateTime_NCR AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(@TR AS VARCHAR(max)) + '</td>' +
				'<td>' + CAST(@CCR AS VARCHAR(max)) + '</td>' +
				'</tr>'
		SELECT @MAIL_BODYFirst_NCR = @MAIL_BODYFirst_NCR + '</table>'


		EXEC msdb.dbo.sp_send_dbmail @profile_name='LDCGEN', @body_format = 'HTML',
		@recipients='asmara.shahid@ke.com.pk',
		@copy_recipients = 'rabha.shoaib@ke.com.pk',
		@subject= 'Reminder 1: Scorecard ELR Portal', @body=@MAIL_BODYFirst_NCR

		IF @RtnCodeNCR <> 0
		begin
			RAISERROR('Error.', 16, 1)
		end


END



--After 3 days of L2 entry not approved.
else if (DATEDIFF(d,convert(datetime, @RdgDateTime_NCR, 103),GETDATE())=3)
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


		DECLARE @MAIL_BODYSecondNCR VARCHAR(max)
		SET @MAIL_BODYSecondNCR = '<p>This is a final reminder, registration is pending since 3 days</p><br/>'
		SELECT @MAIL_BODYSecondNCR = 
			@MAIL_BODYSecondNCR + '<table border="1" align="center" cellpadding="2" cellspacing="0" style="color:black;font-family:consolas;text-align:center;">' +
			'<tr>
						<th>Site ID</th>
						<th>Unit ID</th>
						<th>Last Registration Date</th>
						<th>TR</th>
						<th>CR</th>

			</tr>'
 
		/* ROWS */
		SELECT
			@MAIL_BODYSecondNCR = @MAIL_BODYSecondNCR +
				'<tr>' +
				'<td>' + CAST(@SiteId_NCR AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(@UnitId_NCR AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(@RdgDateTime_NCR AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(@TR AS VARCHAR(max)) + '</td>' +
				'<td>' + CAST(@CCR AS VARCHAR(max)) + '</td>' +
				'</tr>'
		SELECT @MAIL_BODYSecondNCR = @MAIL_BODYSecondNCR + '</table>'


		EXEC msdb.dbo.sp_send_dbmail @profile_name='LDCGEN', @body_format = 'HTML',
		@recipients='asmara.shahid@ke.com.pk',
		@copy_recipients = 'rabha.shoaib@ke.com.pk',
		@subject= 'Reminder 2: Scorecard ELR Portal', @body=@MAIL_BODYSecondNCR

		IF @RtnCodeNCR <> 0
		begin
			RAISERROR('Error.', 16, 1)
		end



END



else if (DATEDIFF(d,convert(datetime, @RdgDateTime_NCR, 103),GETDATE())=5)
BEGIN

	select @TR=''
	select @CCR=''


		SELECT @TR = COALESCE(@TR + ';','') + ISNULL(ToRecipients,'')  from xEmailReminderRecipientsForNotification
				where SiteID = @SiteId_NCR and ISNULL(ToRecipients,'')<>'' and  ReminderNo=2
				AND ACTIVE=1 AND  RType='TO'


		SELECT @CCR = COALESCE(@CCR + ';','') + ISNULL(ToRecipients,'')  from xEmailReminderRecipientsForNotification
				where SiteID = @SiteId_NCR and ISNULL(ToRecipients,'')<>'' and  ReminderNo=2
				AND ACTIVE=1 AND  RType='CC'



					SELECT @TR  =  substring(@TR,2, len(@TR))
				SELECT @CCR  =  substring(@CCR,2, len(@CCR))



		DECLARE @MAIL_BODYFinalNCR VARCHAR(max)
		SET @MAIL_BODYFinalNCR = '<p>Registration is pending since 5 days, This is for your necessary action please.</p><br/>'
		SELECT @MAIL_BODYFinalNCR = 
			@MAIL_BODYFinalNCR + '<table border="1" align="center" cellpadding="2" cellspacing="0" style="color:black;font-family:consolas;text-align:center;">' +
			'<tr>
						<th>Site ID</th>
						<th>Unit ID</th>
						<th>Last Registration Date</th>
						<th>TR</th>
						<th>CR</th>

			</tr>'
 
		/* ROWS */
		SELECT
			@MAIL_BODYFinalNCR = @MAIL_BODYFinalNCR +
				'<tr>' +
				'<td>' + CAST(@SiteId_NCR AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(@UnitId_NCR AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(@RdgDateTime_NCR AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(@TR AS VARCHAR(max)) + '</td>' +
				'<td>' + CAST(@CCR AS VARCHAR(max)) + '</td>' +
				'</tr>'
		SELECT @MAIL_BODYFinalNCR = @MAIL_BODYFinalNCR + '</table>'


		EXEC msdb.dbo.sp_send_dbmail @profile_name='LDCGEN', @body_format = 'HTML',
		@recipients='asmara.shahid@ke.com.pk',
		@copy_recipients = 'rabha.shoaib@ke.com.pk',
		@subject= 'Reminder 3 Scorecard ELR Portal', @body=@MAIL_BODYFinalNCR

		IF @RtnCodeNCR <> 0
		begin
			RAISERROR('Error.', 16, 1)
		end



END



FETCH NEXT FROM @bmNCR INTO @SiteId_NCR,@UnitId_NCR,@RdgDateTime_NCR,@RdgDatediff_NCR
END
CLOSE @bmNCR
DEALLOCATE @bmNCR
end
--Tacling 2nd scenario where records have not been pass through approval section..
--for records that are not created yet..after discussion with asmaara--------------------->>>>>>>>>>>>>>>>>>>>>>>>>>>>>NEED TO BE TEST BEFORE DEPLOYMENT










end