USE [GenerationQA]
GO
/****** Object:  StoredProcedure [dbo].[sendGenLogBookNotificationMail]    Script Date: 4/29/2020 11:36:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[sendGenLogBookNotificationMail]
--@SiteId as varchar(max), @UnitId as varchar(max)
as
begin

--BQ1

declare @TR as varchar(max)
declare @CCR as varchar(max)



--select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  
--from UnitReadingRegister1 where 
--isnull(L2Approval,'N') = 'N'
--group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)
--union all 
--select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  
--from UnitReadingRegister2 where 
--isnull(L2Approval,'N') = 'N'
--group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)
--union all
--select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  
--from UnitReadingRegister3 where 
--isnull(L2Approval,'N') = 'N'
--group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)


--if exists(select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  
--from UnitReadingRegister1 where 
----SiteId = 'BQ1' and UnitId = 'BQ1-UT3' and 
--isnull(L2Approval,'N') = 'N' AND SiteId='BQ1'
--group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)
----order by CONVERT(VARCHAR(10), RdgDateTime, 103)
--)


if exists(
select SiteId,UnitId,RdgDateTime ,
 DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) Diff from (
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  
from UnitReadingRegister1 where 
isnull(L2Approval,'N') = 'N' 
--and SiteId='KGT'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)
union all 
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  
from UnitReadingRegister2 where 
isnull(L2Approval,'N') = 'N' 
--and SiteId='KGT'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)
union all
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  
from UnitReadingRegister3 where 
isnull(L2Approval,'N') = 'N'
 --and SiteId='KGT'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)) x
where DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) In (2,3,5)
)

declare @numCount as bigint = 1
declare @RtnCode as bigint
declare @SiteId_ as varchar(max)
declare @UnitId_ as varchar(max)
declare @RdgDateTime_ as varchar(max)
declare @RdgDatediff_ as varchar(max)
--declare @EmailBody as varchar(max)



--select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103) RdgDateTime,DATEDIFF(D,RdgDateTime,getdate())
--from UnitReadingRegister1 
--where 

--isnull(L1Approval,'N') = 'N' and DATEDIFF(D, RdgDateTime,getdate())>1
--group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103),CONVERT(VARCHAR(10), DATEADD(d,1,RdgDateTime), 103) ,DATEDIFF(D,RdgDateTime,getdate())





DECLARE @bm CURSOR
SET @bm = CURSOR FOR



--Original logic
--select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  
--from UnitReadingRegister1 
--where 
--isnull(L2Approval,'N') = 'N'  AND SiteId='BQ1'
--group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)


--revised logic

--select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  
--from UnitReadingRegister1 where 
--isnull(L2Approval,'N') = 'N'
--group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)
----union all 
----select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  
----from UnitReadingRegister2 where 
----isnull(L2Approval,'N') = 'N'
----group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)
----union all
----select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  
----from UnitReadingRegister3 where 
----isnull(L2Approval,'N') = 'N'
----group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)
--order by CONVERT(VARCHAR(10), RdgDateTime, 103)
--Original logic


select SiteId,UnitId,RdgDateTime ,
 DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) Diff from (
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  
from UnitReadingRegister1 where 
isnull(L2Approval,'N') = 'N' 
--and SiteId='KGT'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)
union all 
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  
from UnitReadingRegister2 where 
isnull(L2Approval,'N') = 'N' 
--and SiteId='KGT'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)
union all
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  
from UnitReadingRegister3 where 
isnull(L2Approval,'N') = 'N' 
--and SiteId='KGT'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)) x
where DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) In (2,3,5)
order by CONVERT(VARCHAR(10), RdgDateTime, 103)


OPEN @bm
FETCH NEXT FROM @bm INTO @SiteId_,@UnitId_,@RdgDateTime_,@RdgDatediff_
WHILE @@FETCH_STATUS = 0
BEGIN

	--After 2 days of L2 entry not approved.
	if ((DATEDIFF(d,convert(datetime, @RdgDateTime_, 103),GETDATE())=2))
	begin

	select @TR=''
	select @CCR=''

				--http://tomaslind.net/2015/06/26/sending-html-tables-in-mail-from-sql-server/
		--DECLARE @MailSubject as varchar(max)
		--set @MailSubject  = CONCAT('LDC-GEN-LOG BOOK - Pending Requests Unit  ( ', CONCAT(@SiteId_ , ' -> ' , @UnitId_ )  ,' )')
		--set @MailSubject  = 'Reminder Scorecard ELR Portal'

		
		--select @TR =ToRecipients,@CCR = CCRecipients from xEmailReminderRecipients where siteId = @SiteId_ and ReminderNo<=2

		SELECT @TR = COALESCE(@tr + ';','') + ISNULL(ToRecipients,'')  from xEmailReminderRecipientsForNotification
				where SiteID = @SiteId_ and ISNULL(ToRecipients,'')<>'' and ReminderNo<=1
				AND ACTIVE=1 AND  RType='TO'


		SELECT @CCR = COALESCE(@CCR + ';','') + ISNULL(ToRecipients,'')  from xEmailReminderRecipientsForNotification
				where SiteID = @SiteId_ and ISNULL(ToRecipients,'')<>'' and ReminderNo<=1
				AND ACTIVE=1 AND  RType='CC'


				SELECT @TR  =  substring(@TR,2, len(@TR))
				SELECT @CCR  =  substring(@CCR,2, len(@CCR))



		--DECLARE @MAIL_BODYFirst VARCHAR(max)
		--SET @MAIL_BODYFirst = '<p>Approval is pending since a day.Please update the portal at the earliest.</p><br/>'
		--SELECT @MAIL_BODYFirst = 
		--	@MAIL_BODYFirst + '<table border="1" align="center" cellpadding="2" cellspacing="0" style="color:black;font-family:consolas;text-align:center;">' +
		--	'<tr>
		--				<th>Site ID</th>
		--				<th>Unit ID</th>
		--				<th>Registration Date</th>
		--				<th>TR</th>
		--				<th>CR</th>
		--	</tr>'



		DECLARE @MAIL_BODYFirst VARCHAR(max)
		SET @MAIL_BODYFirst = '<p>Approval is pending since a day.Please update the portal at the earliest.</p><br/>'
		SELECT @MAIL_BODYFirst = 
			@MAIL_BODYFirst + '<table border="1" align="left" cellpadding="2" cellspacing="0" style="color:black;font-family:consolas;text-align:center;">' +
			'<tr>
						<th>Site ID</th>
						<th>Unit ID</th>
						<th>Registration Date</th>
			</tr>'

 
		/* ROWS */
		SELECT
			@MAIL_BODYFirst = @MAIL_BODYFirst +
				'<tr>' +
				'<td>' + CAST(@SiteId_ AS VARCHAR(11)) + '</td>' +
				--'<td>' + CAST(@UnitId_ AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(((rtrim(ltrim(SUBSTRING(@UnitId_,(CHARINDEX('-',@UnitId_)+1),len(@UnitId_)))))) AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(@RdgDateTime_ AS VARCHAR(11)) + '</td>' +
				'</tr>'
		SELECT @MAIL_BODYFirst = @MAIL_BODYFirst + '</table>'


		--EXEC msdb.dbo.sp_send_dbmail @profile_name='LDCGEN', @body_format = 'HTML',
		--@recipients='asmara.shahid@ke.com.pk',
		--@copy_recipients = 'rabha.shoaib@ke.com.pk',
		--@subject= 'Reminder 1: Scorecard ELR Portal', @body=@MAIL_BODYFirst



		
		EXEC msdb.dbo.sp_send_dbmail @profile_name='LDCGEN', @body_format = 'HTML',
		@recipients=@TR,
		@copy_recipients = @CCR,
		@subject= 'Approval Reminder 1: Scorecard ELR Portal', @body=@MAIL_BODYFirst



		IF @RtnCode <> 0
		begin
			RAISERROR('Error.', 16, 1)
		end


	end


	--After 3 days of L2 entry not approved.
	else if (DATEDIFF(d,convert(datetime, @RdgDateTime_, 103),GETDATE())=3)
	begin

		--http://tomaslind.net/2015/06/26/sending-html-tables-in-mail-from-sql-server/
		--DECLARE @MailSubject_ as varchar(max)
		--set @MailSubject  = CONCAT('LDC-GEN-LOG BOOK - Pending Requests Unit  ( ', CONCAT(@SiteId_ , ' -> ' , @UnitId_ )  ,' )')
		--set @MailSubject_  = 'Reminder Scorecard ELR Portal'

		--select @TR =ToRecipients,@CCR = CCRecipients from xEmailReminderRecipients where siteId = @SiteId_ and ReminderNo<=2
		select @TR=''
	select @CCR=''

		SELECT @TR = COALESCE(@TR + ';','') + ISNULL(ToRecipients,'')  from xEmailReminderRecipientsForNotification
				where SiteID = @SiteId_ and ISNULL(ToRecipients,'')<>'' and ReminderNo<=1
				AND ACTIVE=1 AND  RType='TO'



		SELECT @CCR = COALESCE(@CCR + ';','') + ISNULL(ToRecipients,'')  from xEmailReminderRecipientsForNotification
				where SiteID = @SiteId_ and ISNULL(ToRecipients,'')<>'' and ReminderNo<=1
				AND ACTIVE=1 AND  RType='CC'


				SELECT @TR  =  substring(@TR,2, len(@TR))
				SELECT @CCR  =  substring(@CCR,2, len(@CCR))



		DECLARE @MAIL_BODYSecond VARCHAR(max)
		SET @MAIL_BODYSecond = '<p>This is a final reminder. Approval of following date is pending since 3 days.Please update the portal at the earliest.</p><br/>'
		SELECT @MAIL_BODYSecond = 
			@MAIL_BODYSecond + '<table border="1" align="left" cellpadding="2" cellspacing="0" style="color:black;font-family:consolas;text-align:center;">' +
			'<tr>
						<th>Site ID</th>
						<th>Unit ID</th>
						<th>Registration Date</th>

			</tr>'
 
		/* ROWS */
		SELECT
			@MAIL_BODYSecond = @MAIL_BODYSecond +
				'<tr>' +
				'<td>' + CAST(@SiteId_ AS VARCHAR(11)) + '</td>' +
				--'<td>' + CAST(@UnitId_ AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(((rtrim(ltrim(SUBSTRING(@UnitId_,(CHARINDEX('-',@UnitId_)+1),len(@UnitId_)))))) AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(@RdgDateTime_ AS VARCHAR(11)) + '</td>' +
				'</tr>'
		SELECT @MAIL_BODYSecond = @MAIL_BODYSecond + '</table>'


		--EXEC msdb.dbo.sp_send_dbmail @profile_name='LDCGEN', @body_format = 'HTML',
		--@recipients='asmara.shahid@ke.com.pk',
		--@copy_recipients = 'rabha.shoaib@ke.com.pk',
		--@subject= 'Reminder 2: Scorecard ELR Portal', @body=@MAIL_BODYSecond


			EXEC msdb.dbo.sp_send_dbmail @profile_name='LDCGEN', @body_format = 'HTML',
		@recipients=@TR,
		@copy_recipients = @CCR,
		@subject= 'Approval Reminder 2: Scorecard ELR Portal', @body=@MAIL_BODYSecond


		IF @RtnCode <> 0
		begin
			RAISERROR('Error.', 16, 1)
		end


	end



	--After 5 days of L2 entry not approved.
	else if (DATEDIFF(d,convert(datetime, @RdgDateTime_, 103),GETDATE())=5)
	begin

		--http://tomaslind.net/2015/06/26/sending-html-tables-in-mail-from-sql-server/
		--DECLARE @MailSubject_ as varchar(max)
		--set @MailSubject  = CONCAT('LDC-GEN-LOG BOOK - Pending Requests Unit  ( ', CONCAT(@SiteId_ , ' -> ' , @UnitId_ )  ,' )')
		--set @MailSubject_  = 'Reminder Scorecard ELR Portal'

		--select @TR =ToRecipients,@CCR = CCRecipients from xEmailReminderRecipients where siteId = @SiteId_ and ReminderNo=3

		select @TR=''
	select @CCR=''


			SELECT @TR = COALESCE(@TR + ';','') + ISNULL(ToRecipients,'')  from xEmailReminderRecipientsForNotification
				where SiteID = @SiteId_ and ISNULL(ToRecipients,'')<>'' and  ReminderNo=2
				AND ACTIVE=1 AND  RType='TO'


		SELECT @CCR = COALESCE(@CCR + ';','') + ISNULL(ToRecipients,'')  from xEmailReminderRecipientsForNotification
				where SiteID = @SiteId_ and ISNULL(ToRecipients,'')<>'' and  ReminderNo=2
				AND ACTIVE=1 AND  RType='CC'



						SELECT @TR  =  substring(@TR,2, len(@TR))
				SELECT @CCR  =  substring(@CCR,2, len(@CCR))



		DECLARE @MAIL_BODYFinal VARCHAR(max)
		SET @MAIL_BODYFinal = '<p>Dear Sir, Entries in ELR portal are pending approval since 5 days. Please ask the concerned to take necessary action in order to reflect stations true KPIs numbers in Generation Scorecard Dashboard.</p><br/>'
		SELECT @MAIL_BODYFinal = 
			@MAIL_BODYFinal + '<table border="1" align="left" cellpadding="2" cellspacing="0" style="color:black;font-family:consolas;text-align:center;">' +
			'<tr>
						<th>Site ID</th>
						<th>Unit ID</th>
						<th>Registration Date</th>

			</tr>'
 
		/* ROWS */
		SELECT
			@MAIL_BODYFinal = @MAIL_BODYFinal +
				'<tr>' +
				'<td>' + CAST(@SiteId_ AS VARCHAR(11)) + '</td>' +
				--'<td>' + CAST(@UnitId_ AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(((rtrim(ltrim(SUBSTRING(@UnitId_,(CHARINDEX('-',@UnitId_)+1),len(@UnitId_)))))) AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(@RdgDateTime_ AS VARCHAR(11)) + '</td>' +
				'</tr>'
		SELECT @MAIL_BODYFinal = @MAIL_BODYFinal + '</table>'


		--EXEC msdb.dbo.sp_send_dbmail @profile_name='LDCGEN', @body_format = 'HTML',
		--@recipients='asmara.shahid@ke.com.pk',
		--@copy_recipients = 'rabha.shoaib@ke.com.pk',
		--@subject= 'Reminder 3 Scorecard ELR Portal', @body=@MAIL_BODYFinal



			EXEC msdb.dbo.sp_send_dbmail @profile_name='LDCGEN', @body_format = 'HTML',
		@recipients=@TR,
		@copy_recipients = @CCR,
		@subject= 'Approval Reminder 3: Scorecard ELR Portal', @body=@MAIL_BODYFinal



		IF @RtnCode <> 0
		begin
			RAISERROR('Error.', 16, 1)
		end


	end


	--ORIGINAL LOGIC BEFORE CHANGES

		--http://tomaslind.net/2015/06/26/sending-html-tables-in-mail-from-sql-server/
		--DECLARE @MailSubject as varchar(max)
		--set @MailSubject  = CONCAT('LDC-GEN-LOG BOOK - Pending Requests Unit  ( ', CONCAT(@SiteId_ , ' -> ' , @UnitId_ )  ,' )')


		--DECLARE @MAIL_BODY VARCHAR(max)
		--SET @MAIL_BODY = '<p>System Generated Report</p><br/>'
		--SELECT @MAIL_BODY = 
		--	@MAIL_BODY + '<table border="1" align="center" cellpadding="2" cellspacing="0" style="color:black;font-family:consolas;text-align:center;">' +
		--	'<tr>
		--				<th>Site ID</th>
		--				<th>Unit ID</th>
		--				<th>Registration Date</th>
		--	</tr>'
 
		--/* ROWS */
		--SELECT
		--	@MAIL_BODY = @MAIL_BODY +
		--		'<tr>' +
		--		'<td>' + CAST(@SiteId_ AS VARCHAR(11)) + '</td>' +
		--		'<td>' + CAST(@UnitId_ AS VARCHAR(11)) + '</td>' +
		--		'<td>' + CAST(@RdgDateTime_ AS VARCHAR(11)) + '</td>' +
		--		'</tr>'
		--SELECT @MAIL_BODY = @MAIL_BODY + '</table>'


		--EXEC msdb.dbo.sp_send_dbmail @profile_name='LDCGEN', @body_format = 'HTML',
		--@recipients='rabha.shoaib@ke.com.pk',
		--@copy_recipients = 'rabha.shoaib@ke.com.pk',
		--@subject= @MailSubject, @body=@MAIL_BODY

		--IF @RtnCode <> 0
		--begin
		--	RAISERROR('Error.', 16, 1)
		--end

set @numCount = @numCount +1


FETCH NEXT FROM @bm INTO @SiteId_,@UnitId_,@RdgDateTime_,@RdgDatediff_
END
CLOSE @bm
DEALLOCATE @bm













--for records that are not created yet..after discussion with asmaara--------------------->>>>>>>>>>>>>>>>>>>>>>>>>>>>>NEED TO BE TEST BEFORE DEPLOYMENT
--if exists(select SiteId,UnitId,(CONVERT(VARCHAR(10), RdgDateTime, 103)) RdgDateTime ,
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
--DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) >1)
--begin

--declare @numCountNCR as bigint = 1
--declare @RtnCodeNCR as bigint
--declare @SiteId_NCR as varchar(max)
--declare @UnitId_NCR as varchar(max)
--declare @RdgDateTime_NCR as varchar(max)
--declare @RdgDatediff_NCR as varchar(max)


--DECLARE @bmNCR CURSOR
--SET @bmNCR = CURSOR FOR

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
--DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) >1


--OPEN @bmNCR
--FETCH NEXT FROM @bmNCR INTO @SiteId_NCR,@UnitId_NCR,@RdgDateTime_NCR,@RdgDatediff_NCR
--WHILE @@FETCH_STATUS = 0
--BEGIN

----After 2 days of L2 entry not approved.
--if ((DATEDIFF(d,convert(datetime, @RdgDateTime_NCR, 103),GETDATE())=2))
--BEGIN

--	SELECT @TR = COALESCE(@TR + ';','') + ISNULL(ToRecipients,'')  from xEmailReminderRecipientsForNotification
--				where SiteID = @SiteId_NCR and ISNULL(ToRecipients,'')<>'' and ReminderNo<=1
--				AND ACTIVE=1 AND  RType='TO'


--		SELECT @CCR = COALESCE(@CCR + ';','') + ISNULL(ToRecipients,'')  from xEmailReminderRecipientsForNotification
--				where SiteID = @SiteId_NCR and ISNULL(ToRecipients,'')<>'' and ReminderNo<=1
--				AND ACTIVE=1 AND  RType='CC'



--		DECLARE @MAIL_BODYFirst_NCR VARCHAR(max)
--		SET @MAIL_BODYFirst_NCR = '<p>Registration is pending since a day,Please update the portal at the earliest</p><br/>'
--		SELECT @MAIL_BODYFirst_NCR = 
--			@MAIL_BODYFirst_NCR + '<table border="1" align="center" cellpadding="2" cellspacing="0" style="color:black;font-family:consolas;text-align:center;">' +
--			'<tr>
--						<th>Site ID</th>
--						<th>Unit ID</th>
--						<th>Registration Date</th>
--						<th>TR</th>
--						<th>CR</th>
--			</tr>'
 
--		/* ROWS */
--		SELECT
--			@MAIL_BODYFirst_NCR = @MAIL_BODYFirst_NCR +
--				'<tr>' +
--				'<td>' + CAST(@SiteId_NCR AS VARCHAR(11)) + '</td>' +
--				'<td>' + CAST(@UnitId_NCR AS VARCHAR(11)) + '</td>' +
--				'<td>' + CAST(@RdgDateTime_NCR AS VARCHAR(11)) + '</td>' +
--				'<td>' + CAST(@TR AS VARCHAR(max)) + '</td>' +
--				'<td>' + CAST(@CCR AS VARCHAR(max)) + '</td>' +
--				'</tr>'
--		SELECT @MAIL_BODYFirst_NCR = @MAIL_BODYFirst_NCR + '</table>'


--		EXEC msdb.dbo.sp_send_dbmail @profile_name='LDCGEN', @body_format = 'HTML',
--		@recipients='rabha.shoaib@ke.com.pk',
--		@copy_recipients = 'rabha.shoaib@ke.com.pk',
--		@subject= 'Reminder 1: Scorecard ELR Portal', @body=@MAIL_BODYFirst_NCR

--		IF @RtnCodeNCR <> 0
--		begin
--			RAISERROR('Error.', 16, 1)
--		end


--END



----After 3 days of L2 entry not approved.
--else if (DATEDIFF(d,convert(datetime, @RdgDateTime_NCR, 103),GETDATE())=3)
--BEGIN



--		SELECT @TR = COALESCE(@TR + ';','') + ISNULL(ToRecipients,'')  from xEmailReminderRecipientsForNotification
--				where SiteID = @SiteId_ and ISNULL(ToRecipients,'')<>'' and ReminderNo<=1
--				AND ACTIVE=1 AND  RType='TO'



--		SELECT @CCR = COALESCE(@CCR + ';','') + ISNULL(ToRecipients,'')  from xEmailReminderRecipientsForNotification
--				where SiteID = @SiteId_ and ISNULL(ToRecipients,'')<>'' and ReminderNo<=1
--				AND ACTIVE=1 AND  RType='CC'


--		DECLARE @MAIL_BODYSecondNCR VARCHAR(max)
--		SET @MAIL_BODYSecondNCR = '<p>Registration is pending since 3 days, This is for your necessary action please.</p><br/>'
--		SELECT @MAIL_BODYSecondNCR = 
--			@MAIL_BODYSecond + '<table border="1" align="center" cellpadding="2" cellspacing="0" style="color:black;font-family:consolas;text-align:center;">' +
--			'<tr>
--						<th>Site ID</th>
--						<th>Unit ID</th>
--						<th>Registration Date</th>
--						<th>TR</th>
--						<th>CR</th>

--			</tr>'
 
--		/* ROWS */
--		SELECT
--			@MAIL_BODYSecondNCR = @MAIL_BODYSecondNCR +
--				'<tr>' +
--				'<td>' + CAST(@SiteId_NCR AS VARCHAR(11)) + '</td>' +
--				'<td>' + CAST(@UnitId_NCR AS VARCHAR(11)) + '</td>' +
--				'<td>' + CAST(@RdgDateTime_NCR AS VARCHAR(11)) + '</td>' +
--				'<td>' + CAST(@TR AS VARCHAR(max)) + '</td>' +
--				'<td>' + CAST(@CCR AS VARCHAR(max)) + '</td>' +
--				'</tr>'
--		SELECT @MAIL_BODYSecondNCR = @MAIL_BODYSecondNCR + '</table>'


--		EXEC msdb.dbo.sp_send_dbmail @profile_name='LDCGEN', @body_format = 'HTML',
--		@recipients='rabha.shoaib@ke.com.pk',
--		@copy_recipients = 'rabha.shoaib@ke.com.pk',
--		@subject= 'Reminder 2: Scorecard ELR Portal', @body=@MAIL_BODYSecondNCR

--		IF @RtnCodeNCR <> 0
--		begin
--			RAISERROR('Error.', 16, 1)
--		end



--END



--else if (DATEDIFF(d,convert(datetime, @RdgDateTime_NCR, 103),GETDATE())=5)
--BEGIN


--		SELECT @TR = COALESCE(@TR + ';','') + ISNULL(ToRecipients,'')  from xEmailReminderRecipientsForNotification
--				where SiteID = @SiteId_NCR and ISNULL(ToRecipients,'')<>'' and  ReminderNo=2
--				AND ACTIVE=1 AND  RType='TO'


--		SELECT @CCR = COALESCE(@CCR + ';','') + ISNULL(ToRecipients,'')  from xEmailReminderRecipientsForNotification
--				where SiteID = @SiteId_NCR and ISNULL(ToRecipients,'')<>'' and  ReminderNo=2
--				AND ACTIVE=1 AND  RType='CC'



--		DECLARE @MAIL_BODYFinalNCR VARCHAR(max)
--		SET @MAIL_BODYFinalNCR = '<p>Approval is pending since 5 days, This is for your necessary action please.</p><br/>'
--		SELECT @MAIL_BODYFinalNCR = 
--			@MAIL_BODYFinalNCR + '<table border="1" align="center" cellpadding="2" cellspacing="0" style="color:black;font-family:consolas;text-align:center;">' +
--			'<tr>
--						<th>Site ID</th>
--						<th>Unit ID</th>
--						<th>Registration Date</th>
--						<th>TR</th>
--						<th>CR</th>

--			</tr>'
 
--		/* ROWS */
--		SELECT
--			@MAIL_BODYFinalNCR = @MAIL_BODYFinalNCR +
--				'<tr>' +
--				'<td>' + CAST(@SiteId_NCR AS VARCHAR(11)) + '</td>' +
--				'<td>' + CAST(@UnitId_NCR AS VARCHAR(11)) + '</td>' +
--				'<td>' + CAST(@RdgDateTime_NCR AS VARCHAR(11)) + '</td>' +
--				'<td>' + CAST(@TR AS VARCHAR(max)) + '</td>' +
--				'<td>' + CAST(@CCR AS VARCHAR(max)) + '</td>' +
--				'</tr>'
--		SELECT @MAIL_BODYFinalNCR = @MAIL_BODYFinalNCR + '</table>'


--		EXEC msdb.dbo.sp_send_dbmail @profile_name='LDCGEN', @body_format = 'HTML',
--		@recipients='rabha.shoaib@ke.com.pk',
--		@copy_recipients = 'rabha.shoaib@ke.com.pk',
--		@subject= 'Reminder 3 Scorecard ELR Portal', @body=@MAIL_BODYFinal

--		IF @RtnCodeNCR <> 0
--		begin
--			RAISERROR('Error.', 16, 1)
--		end



--END



--FETCH NEXT FROM @bmNCR INTO @SiteId_NCR,@UnitId_NCR,@RdgDateTime_NCR,@RdgDatediff_NCR
--END
--CLOSE @bmNCR
--DEALLOCATE @bmNCR




--end

--for records that are not created yet..after discussion with asmaara--------------------->>>>>>>>>>>>>>>>>>>>>>>>>>>>>










end
