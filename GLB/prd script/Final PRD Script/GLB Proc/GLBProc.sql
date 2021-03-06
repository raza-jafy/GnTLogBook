USE [GenerationQA]
GO
/****** Object:  StoredProcedure [dbo].[GetGenLogBookPredefinedReasons]    Script Date: 1/23/2020 9:29:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetGenLogBookPredefinedReasons]
@SiteId varchar(max), @UnitId varchar(max)
as
begin

select distinct Reason from UnitReadingRegister1
where ISNULL(Reason,'')<>''
union all 
select distinct Reason from UnitReadingRegister2
where ISNULL(Reason,'')<>''
union all 
select distinct Reason from UnitReadingRegister3
where ISNULL(Reason,'')<>''
--and SiteId = @SiteId and UnitId = @UnitId


end
GO
/****** Object:  StoredProcedure [dbo].[GetGenLogBookPredefinedReasonsBySite]    Script Date: 1/23/2020 9:29:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GetGenLogBookPredefinedReasonsBySite]
@SiteId varchar(max)
as
begin

select distinct Reason from UnitReadingRegister1
where ISNULL(Reason,'')<>'' and SiteId = @SiteId
union all 
select distinct Reason from UnitReadingRegister2
where ISNULL(Reason,'')<>'' and SiteId = @SiteId
union all 
select distinct Reason from UnitReadingRegister3
where ISNULL(Reason,'')<>'' and SiteId = @SiteId
--and SiteId = @SiteId and UnitId = @UnitId


end
GO
/****** Object:  StoredProcedure [dbo].[getGenLogBookRecipients]    Script Date: 1/23/2020 9:29:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[getGenLogBookRecipients]
as
begin


DECLARE @SiteID as varchar(max)
DECLARE @ToRecipients as varchar(max)
DECLARE @CCRecipients as varchar(max)
declare @ReminderNo as varchar(max)


declare  @xTable TABLE (SiteID varchar(max) , Recipients varchar(max), KR varchar(max),ReminderNo varchar(max))  


DECLARE @bm CURSOR
SET @bm = CURSOR FOR
--select SiteID,ToRecipients,CCRecipients from xEmailReminderRecipients where SiteID In (
--select SiteId from SitesMaster
--)

select SiteID,ToRecipients,CCRecipients,
case ReminderNo when 1 then '1st & 2nd Reminder Recipients' else 'Final Reminder Recipients' end ReminderNo from xEmailReminderRecipients where SiteID In (
select SiteId from SitesMaster
)
OPEN @bm
FETCH NEXT FROM @bm INTO  @SiteID, @ToRecipients , @CCRecipients,@ReminderNo
WHILE @@FETCH_STATUS = 0
BEGIN

				DECLARE @Recipients as varchar(max)
				DECLARE @RType as varchar(max)

				DECLARE @bx CURSOR
				SET @bx = CURSOR FOR
				SELECT items,KeyR FROM DBO.fnSplit(@ToRecipients,';','TR')
				union all
				SELECT items,KeyR FROM DBO.fnSplit(@CCRecipients,';','TC')
				OPEN @bx
				FETCH NEXT FROM @bx INTO  @Recipients ,@RType
				WHILE @@FETCH_STATUS = 0
				BEGIN

				insert into @xTable(SiteID,Recipients,KR,ReminderNo) values(@SiteID,@Recipients,@RType,@ReminderNo) 

				FETCH NEXT FROM @bx INTO  @Recipients ,@RType
				END
				CLOSE @bx
				DEALLOCATE @bx


FETCH NEXT FROM @bm INTO  @SiteID, @ToRecipients , @CCRecipients,@ReminderNo
END
CLOSE @bm
DEALLOCATE @bm

select * from @xTable




end
GO
/****** Object:  StoredProcedure [dbo].[getGenLogBookRecipientsForNotification]    Script Date: 1/23/2020 9:29:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[getGenLogBookRecipientsForNotification]
@inputSiteID  varchar(max)
as
begin

--select ID,SiteID,ToRecipients,'TO' RecipientType ,
--case ReminderNo when 1 then 'First | 2nd Reminder Recipients' else 'Final Reminder Recipients' end ReminderNo
--from xEmailReminderRecipientsForNotification
--where SiteID In (@inputSiteID) and ToRecipients is not null
--union all
--select ID,SiteID,CCRecipients,'CC' RecipientType,
--case ReminderNo when 1 then 'First | 2nd Reminder Recipients' else 'Final Reminder Recipients' end ReminderNo
--from xEmailReminderRecipientsForNotification
--where SiteID In (@inputSiteID) and CCRecipients is not null



select ID,SiteID,ToRecipients,'TO' RecipientType ,
case ReminderNo when 1 then 'First | 2nd Reminder Recipients' else 'Final Reminder Recipients' end ReminderNo,
CASE ACTIVE when 1 then 'Active' else 'In Active' end ACTIVE
--ReminderNo 
from xEmailReminderRecipientsForNotification 
where SiteID In (@inputSiteID) and ToRecipients is not null
--AND ACTIVE=1 
AND  RType='TO'

union all

select ID,SiteID,ToRecipients,'CC' RecipientType,
case ReminderNo when 1 then 'First | 2nd Reminder Recipients' else 'Final Reminder Recipients' end ReminderNo,
CASE ACTIVE when 1 then 'Active' else 'In Active' end ACTIVE
--ReminderNo
from xEmailReminderRecipientsForNotification
where SiteID In (@inputSiteID) and  ToRecipients is not null
--AND ACTIVE=1 
AND  RType='CC'





end
GO
/****** Object:  StoredProcedure [dbo].[sendGenLogBookNotificationMail]    Script Date: 1/23/2020 9:29:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sendGenLogBookNotificationMail]
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



		DECLARE @MAIL_BODYFirst VARCHAR(max)
		SET @MAIL_BODYFirst = '<p>Approval is pending since a day,Please update the portal at the earliest</p><br/>'
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
		@recipients='rabha.shoaib@ke.com.pk',
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


		SELECT @TR = COALESCE(@TR + ';','') + ISNULL(ToRecipients,'')  from xEmailReminderRecipientsForNotification
				where SiteID = @SiteId_ and ISNULL(ToRecipients,'')<>'' and ReminderNo<=1
				AND ACTIVE=1 AND  RType='TO'



		SELECT @CCR = COALESCE(@CCR + ';','') + ISNULL(ToRecipients,'')  from xEmailReminderRecipientsForNotification
				where SiteID = @SiteId_ and ISNULL(ToRecipients,'')<>'' and ReminderNo<=1
				AND ACTIVE=1 AND  RType='CC'


		DECLARE @MAIL_BODYSecond VARCHAR(max)
		SET @MAIL_BODYSecond = '<p>Approval is pending since 5 days, This is for your necessary action please.</p><br/>'
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
		@recipients='rabha.shoaib@ke.com.pk',
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


			SELECT @TR = COALESCE(@TR + ';','') + ISNULL(ToRecipients,'')  from xEmailReminderRecipientsForNotification
				where SiteID = @SiteId_ and ISNULL(ToRecipients,'')<>'' and  ReminderNo=2
				AND ACTIVE=1 AND  RType='TO'


		SELECT @CCR = COALESCE(@CCR + ';','') + ISNULL(ToRecipients,'')  from xEmailReminderRecipientsForNotification
				where SiteID = @SiteId_ and ISNULL(ToRecipients,'')<>'' and  ReminderNo=2
				AND ACTIVE=1 AND  RType='CC'



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
		@recipients='rabha.shoaib@ke.com.pk',
		@copy_recipients = 'rabha.shoaib@ke.com.pk',
		@subject= 'Reminder 3 Scorecard ELR Portal', @body=@MAIL_BODYFinal

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
GO
/****** Object:  UserDefinedFunction [dbo].[fnSplit]    Script Date: 1/23/2020 9:29:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fnSplit](@String varchar(max), @Delimiter char(1),@TC varchar(max))     
returns @temptable TABLE (items varchar(max) , KeyR varchar(max))     
as     
begin     
    declare @idx int     
    declare @slice varchar(max)     
    
    select @idx = 1     
        if len(@String)<1 or @String is null  return     
    
    while @idx!= 0     
    begin     
        set @idx = charindex(@Delimiter,@String)     
        if @idx!=0     
            set @slice = left(@String,@idx - 1)     
        else     
            set @slice = @String     
        
        if(len(@slice)>0)
            insert into @temptable(Items,KeyR) values(@slice,@TC)     

        set @String = right(@String,len(@String) - @idx)     
        if len(@String) = 0 break     
    end 
return     
end
GO
/****** Object:  UserDefinedFunction [dbo].[Split]    Script Date: 1/23/2020 9:29:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[Split](@String varchar(max), @Delimiter char(1))     
returns @temptable TABLE (items varchar(8000))     
as     
begin     
    declare @idx int     
    declare @slice varchar(max)     
    
    select @idx = 1     
        if len(@String)<1 or @String is null  return     
    
    while @idx!= 0     
    begin     
        set @idx = charindex(@Delimiter,@String)     
        if @idx!=0     
            set @slice = left(@String,@idx - 1)     
        else     
            set @slice = @String     
        
        if(len(@slice)>0)
            insert into @temptable(Items) values(@slice)     

        set @String = right(@String,len(@String) - @idx)     
        if len(@String) = 0 break     
    end 
return     
end
GO
