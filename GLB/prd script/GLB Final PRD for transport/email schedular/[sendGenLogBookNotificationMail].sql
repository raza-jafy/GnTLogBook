USE [GenerationQA]
GO
/****** Object:  StoredProcedure [dbo].[sendGenLogBookNotificationMail]    Script Date: 3/13/2020 3:52:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[sendGenLogBookNotificationMail]
--@SiteId as varchar(max), @UnitId as varchar(max)
as
begin
--
--select DATEDIFF(d, cast('09/03/2019 9:45:48' as datetime),getdate())
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
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)
union all 
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  
from UnitReadingRegister2 where 
isnull(L2Approval,'N') = 'N'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)
union all
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  
from UnitReadingRegister3 where 
isnull(L2Approval,'N') = 'N'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)) x
where DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) In (2,3,5)
)
begin

declare @numCount as bigint = 1
declare @RtnCode as bigint
declare @SiteId_ as varchar(max)
declare @UnitId_ as varchar(max)
declare @RdgDateTime_ as varchar(max)
declare @RdgDatediff_ as varchar(max)
--declare @EmailBody as varchar(max)
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


--revised logic problem identified by asmara

--select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  , 'UnitReadingRegister1' AS Register
--from UnitReadingRegister1 where 
--isnull(L2Approval,'N') = 'N' and CONVERT(VARCHAR(10), RdgDateTime, 103) NOT IN (select CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  
--from UnitReadingRegister1 where  (L2ApproveBy <> ''))
--group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)
--union all 
--select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  , 'UnitReadingRegister2' AS Register
--from UnitReadingRegister2 where 
--isnull(L2Approval,'N') = 'N'  and CONVERT(VARCHAR(10), RdgDateTime, 103) NOT IN (select CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  
--from UnitReadingRegister1 where  (L2ApproveBy <> ''))
--group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)
--union all
--select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  , 'UnitReadingRegister3' AS Register
--from UnitReadingRegister3 where 
--isnull(L2Approval,'N') = 'N'  and CONVERT(VARCHAR(10), RdgDateTime, 103) NOT IN (select CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  
--from UnitReadingRegister1 where  (L2ApproveBy <> ''))
--group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103

--revised logic problem identified by asmara


select SiteId,UnitId,RdgDateTime ,
 DATEDIFF(d,convert(datetime, RdgDateTime, 103),GETDATE()) Diff from (
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  
from UnitReadingRegister1 where 
isnull(L2Approval,'N') = 'N'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)
union all 
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  
from UnitReadingRegister2 where 
isnull(L2Approval,'N') = 'N'
group by SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)
union all
select SiteId,UnitId,CONVERT(VARCHAR(10), RdgDateTime, 103)  RdgDateTime  
from UnitReadingRegister3 where 
isnull(L2Approval,'N') = 'N'
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



		DECLARE @MAIL_BODYFirst VARCHAR(max)
		SET @MAIL_BODYFirst = '<p>Approval of following date is pending,Please approve the portal at the earliest</p><br/>'
		SELECT @MAIL_BODYFirst = 
			@MAIL_BODYFirst + '<table border="1" align="center" cellpadding="2" cellspacing="0" style="color:black;font-family:consolas;text-align:center;">' +
			'<tr>
						<th>Site ID</th>
						<th>Unit ID</th>
						<th>Registration Date</th>
						<th>TR</th>
						<th>CR</th>
			</tr>'
 
		/* ROWS */
		SELECT
			@MAIL_BODYFirst = @MAIL_BODYFirst +
				'<tr>' +
				'<td>' + CAST(@SiteId_ AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(@UnitId_ AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(@RdgDateTime_ AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(@TR AS VARCHAR(max)) + '</td>' +
				'<td>' + CAST(@CCR AS VARCHAR(max)) + '</td>' +
				'</tr>'
		SELECT @MAIL_BODYFirst = @MAIL_BODYFirst + '</table>'


		EXEC msdb.dbo.sp_send_dbmail @profile_name='LDCGEN', @body_format = 'HTML',
		@recipients='asmara.shahid@ke.com.pk',
		@copy_recipients = 'rabha.shoaib@ke.com.pk',
		@subject= 'Reminder 1: Scorecard ELR Portal', @body=@MAIL_BODYFirst

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
		SET @MAIL_BODYSecond = '<p>This is a final reminder, approval of following date is pending since 3 days</p><br/>'
		SELECT @MAIL_BODYSecond = 
			@MAIL_BODYSecond + '<table border="1" align="center" cellpadding="2" cellspacing="0" style="color:black;font-family:consolas;text-align:center;">' +
			'<tr>
						<th>Site ID</th>
						<th>Unit ID</th>
						<th>Registration Date</th>
						<th>TR</th>
						<th>CR</th>

			</tr>'
 
		/* ROWS */
		SELECT
			@MAIL_BODYSecond = @MAIL_BODYSecond +
				'<tr>' +
				'<td>' + CAST(@SiteId_ AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(@UnitId_ AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(@RdgDateTime_ AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(@TR AS VARCHAR(max)) + '</td>' +
				'<td>' + CAST(@CCR AS VARCHAR(max)) + '</td>' +
				'</tr>'
		SELECT @MAIL_BODYSecond = @MAIL_BODYSecond + '</table>'


		EXEC msdb.dbo.sp_send_dbmail @profile_name='LDCGEN', @body_format = 'HTML',
		@recipients='asmara.shahid@ke.com.pk',
		@copy_recipients = 'rabha.shoaib@ke.com.pk',
		@subject= 'Reminder 2: Scorecard ELR Portal', @body=@MAIL_BODYSecond

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
		SET @MAIL_BODYFinal = '<p>Approval is pending since 5 days, This is for your necessary action please.</p><br/>'
		SELECT @MAIL_BODYFinal = 
			@MAIL_BODYFinal + '<table border="1" align="center" cellpadding="2" cellspacing="0" style="color:black;font-family:consolas;text-align:center;">' +
			'<tr>
						<th>Site ID</th>
						<th>Unit ID</th>
						<th>Registration Date</th>
						<th>TR</th>
						<th>CR</th>

			</tr>'
 
		/* ROWS */
		SELECT
			@MAIL_BODYFinal = @MAIL_BODYFinal +
				'<tr>' +
				'<td>' + CAST(@SiteId_ AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(@UnitId_ AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(@RdgDateTime_ AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(@TR AS VARCHAR(max)) + '</td>' +
				'<td>' + CAST(@CCR AS VARCHAR(max)) + '</td>' +
				'</tr>'
		SELECT @MAIL_BODYFinal = @MAIL_BODYFinal + '</table>'


		EXEC msdb.dbo.sp_send_dbmail @profile_name='LDCGEN', @body_format = 'HTML',
		@recipients='asmara.shahid@ke.com.pk',
		@copy_recipients = 'rabha.shoaib@ke.com.pk',
		@subject= 'Reminder 3 Scorecard ELR Portal', @body=@MAIL_BODYFinal

		IF @RtnCode <> 0
		begin
			RAISERROR('Error.', 16, 1)
		end


	end


	

set @numCount = @numCount +1


FETCH NEXT FROM @bm INTO @SiteId_,@UnitId_,@RdgDateTime_,@RdgDatediff_
END
CLOSE @bm
DEALLOCATE @bm

end


















end