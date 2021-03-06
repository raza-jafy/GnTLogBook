USE [GenerationQA2]
GO
/****** Object:  StoredProcedure [dbo].[GetGenLogBookPredefinedReasons]    Script Date: 1/22/2020 12:10:28 PM ******/
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
/****** Object:  StoredProcedure [dbo].[GetGenLogBookPredefinedReasonsBySite]    Script Date: 1/22/2020 12:10:28 PM ******/
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
/****** Object:  StoredProcedure [dbo].[getGenLogBookRecipients]    Script Date: 1/22/2020 12:10:28 PM ******/
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
/****** Object:  StoredProcedure [dbo].[getGenLogBookRecipientsForNotification]    Script Date: 1/22/2020 12:10:28 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sendGenLogBookNotificationMail]    Script Date: 1/22/2020 12:10:28 PM ******/
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
/****** Object:  UserDefinedFunction [dbo].[fnSplit]    Script Date: 1/22/2020 12:10:28 PM ******/
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
/****** Object:  UserDefinedFunction [dbo].[Split]    Script Date: 1/22/2020 12:10:28 PM ******/
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
/****** Object:  Table [dbo].[xEmailReminderRecipients]    Script Date: 1/22/2020 12:10:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[xEmailReminderRecipients](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SiteID] [varchar](10) NULL,
	[ReminderNo] [int] NULL,
	[ToRecipients] [varchar](max) NULL,
	[CCRecipients] [varchar](max) NULL,
	[RedgDateTime] [datetime] NULL DEFAULT (getdate())
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[xEmailReminderRecipientsForNotification]    Script Date: 1/22/2020 12:10:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[xEmailReminderRecipientsForNotification](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SiteID] [varchar](10) NULL,
	[ReminderNo] [int] NULL,
	[ToRecipients] [varchar](max) NULL,
	[CCRecipients] [varchar](max) NULL,
	[RedgDateTime] [datetime] NULL DEFAULT (getdate()),
	[RType] [varchar](max) NULL,
	[Active] [bigint] NULL,
 CONSTRAINT [PK_xEmailReminderRecipientsForNotification] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedFunction [dbo].[fDaysInMonth]    Script Date: 1/22/2020 12:10:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fDaysInMonth] (
    @month varchar(2),
	@year varchar(4)
)
RETURNS TABLE
AS
RETURN
WITH n AS
(
    SELECT cast(@month+'/1/'+@year as date) as theDate
    UNION ALL
    SELECT dateadd(day, 1, thedate) FROM n 
	WHERE dateadd(day, 1, thedate) < dateadd(month, 1,cast(@month+'/1/'+@year as date))
)
SELECT theDate FROM n;

GO
SET IDENTITY_INSERT [dbo].[xEmailReminderRecipients] ON 

INSERT [dbo].[xEmailReminderRecipients] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime]) VALUES (1, N'BQ1', 1, N'tahirmalik.muhammad@ke.com.pk;arshad.farooqui@ke.com.pk', N'shahbaz.naser@ke.com.pk;naseem.raza@ke.com.pk;waqas.ansari@ke.com.pk;muneer.ul.haq@ke.com.pk;faizan.ahmed@ke.com.pk', CAST(N'2019-12-24 13:12:24.913' AS DateTime))
INSERT [dbo].[xEmailReminderRecipients] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime]) VALUES (2, N'BQ2', 1, N'mehdi.ali@ke.com.pk;aamir.fayyaz@ke.com.pk', N'rehman.sheikh@ke.com.pk;aslam.warsi@ke.com.pk;imranur.rahim@ke.com.pk;afzal.sher@ke.com.pk;mazhar.haider@ke.com.pk;muhammad.ansar@ke.com.pk;umair.abid@ke.com.pk;ikhtiar.ahmed@ke.com.pk;hassan.sajjad@ke.com.pk;umer.khalid@ke.com.pk', CAST(N'2019-12-24 13:13:55.703' AS DateTime))
INSERT [dbo].[xEmailReminderRecipients] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime]) VALUES (3, N'KPC', 1, N'imtiaz.alam@ke.com.pk;javed.mateen@ke.com.pk', N'asad.rahman@ke.com.pk;kaleemullah.khan@ke.com.pk;khurram.khan@ke.com.pk;gotam.ram@ke.com.pk;imran.mukhtar@ke.com.pk;muhammad.anas@ke.com.pk;sajjad.raza@ke.com.pk;abbas.zulfiqar@ke.com.pk;waseem.sabir@ke.com.pk;talha.khubaib@ke.com.pk', CAST(N'2019-12-24 13:16:06.580' AS DateTime))
INSERT [dbo].[xEmailReminderRecipients] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime]) VALUES (4, N'SGT', 1, N'saqib.shaheen@ke.com.pk;mahsan.qureshi@ke.com.pk', N'toqeer.alam@ke.com.pk;javed.mumtaz@ke.com.pk;syed.zeeshan@ke.com.pk;mfarhan.alam@ke.com.pk;raza.jamal@ke.com.pk;junaid.waseem@ke.com.pk;sohaib.khan@ke.com.pk;nida.nawab@ke.com.pk', CAST(N'2019-12-24 13:18:42.520' AS DateTime))
INSERT [dbo].[xEmailReminderRecipients] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime]) VALUES (5, N'KGT', 1, N'adnan.ahmad@ke.com.pk;Mirza.saad@ke.com.pk', N'amir.alisyed@ke.com.pk;shahrukh.Abbas@ke.com.pk;wajid.shaikh@ke.com.pk;zarak.Noveed@ke.com.pk;mustafa.Khanzada@ke.com.pk;mustafa.Suleman@ke.com.pk;Syed.irfan@ke.com.pk', CAST(N'2019-12-24 13:20:33.853' AS DateTime))
INSERT [dbo].[xEmailReminderRecipients] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime]) VALUES (6, N'BQ1', 3, N'razzaq.ahmad@ke.com.pk', N'tahirmalik.muhammad@ke.com.pk;arshad.farooqui@ke.com.pk;mustafa.husain@ke.com.pk;sm.adeel@ke.com.pk;mzafar.iqbal@ke.com.pk;asmara.shahid@ke.com.pk', CAST(N'2019-12-24 13:23:22.993' AS DateTime))
INSERT [dbo].[xEmailReminderRecipients] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime]) VALUES (7, N'BQ2', 3, N'tahir.ali@ke.com.pk', N'mehdi.ali@ke.com.pk;aamir.fayyaz@ke.com.pk;mustafa.husain@ke.com.pk;sm.adeel@ke.com.pk;mzafar.iqbal@ke.com.pk;asmara.shahid@ke.com.pk', CAST(N'2019-12-24 13:24:52.417' AS DateTime))
INSERT [dbo].[xEmailReminderRecipients] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime]) VALUES (8, N'KPC', 3, N'nowshad.alam@ke.com.pk', N'imtiaz.alam@ke.com.pk;javed.mateen@ke.com.pk;mustafa.husain@ke.com.pk;sm.adeel@ke.com.pk;mzafar.iqbal@ke.com.pk;asmara.shahid@ke.com.pk', CAST(N'2019-12-24 13:26:00.413' AS DateTime))
INSERT [dbo].[xEmailReminderRecipients] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime]) VALUES (9, N'SGT', 3, N'muhammad.s@ke.com.pk', N'saqib.shaheen@ke.com.pk;mahsan.qureshi@ke.com.pk;mustafa.husain@ke.com.pk;sm.adeel@ke.com.pk;mzafar.iqbal@ke.com.pk;asmara.shahid@ke.com.pk', CAST(N'2019-12-24 14:31:37.373' AS DateTime))
INSERT [dbo].[xEmailReminderRecipients] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime]) VALUES (10, N'KGT', 3, N'nowshad.alam@ke.com.pk', N'adnan.ahmad@ke.com.pk;Mirza.saad@ke.com.pk;mustafa.husain@ke.com.pk;sm.adeel@ke.com.pk;mzafar.iqbal@ke.com.pk;asmara.shahid@ke.com.pk', CAST(N'2019-12-24 14:33:21.287' AS DateTime))
SET IDENTITY_INSERT [dbo].[xEmailReminderRecipients] OFF
SET IDENTITY_INSERT [dbo].[xEmailReminderRecipientsForNotification] ON 

INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (1, N'BQ1', 1, N'tahirmalik.muhammad@ke.com.pk', N'shahbaz.naser@ke.com.pk', CAST(N'2020-01-08 12:59:26.040' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (2, N'BQ1', 1, N'arshad.farooqui@ke.com.pk', N'naseem.raza@ke.com.pk', CAST(N'2020-01-08 13:01:18.033' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (3, N'BQ1', 1, N'shahbaz.naser@ke.com.pk', N'waqas.ansari@ke.com.pk', CAST(N'2020-01-08 13:02:21.210' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (4, N'BQ1', 1, N'naseem.raza@ke.com.pk', N'muneer.ul.haq@ke.com.pk', CAST(N'2020-01-08 13:02:40.920' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (5, N'BQ1', 1, N'waqas.ansari@ke.com.pk', N'faizan.ahmed@ke.com.pk', CAST(N'2020-01-08 13:03:06.130' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (7, N'BQ1', 1, N'muneer.ul.haq@ke.com.pk', N'rehman.sheikh@ke.com.pk', CAST(N'2020-01-08 13:05:00.413' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (8, N'BQ1', 1, N'faizan.ahmed@ke.com.pk', N'aslam.warsi@ke.com.pk', CAST(N'2020-01-08 13:05:35.690' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (9, N'BQ2', 1, N'mehdi.ali@ke.com.pk', N'imranur.rahim@ke.com.pk', CAST(N'2020-01-08 13:06:06.460' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (10, N'BQ2', 1, N'aamir.fayyaz@ke.com.pk', N'afzal.sher@ke.com.pk', CAST(N'2020-01-08 13:06:16.277' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (11, N'BQ2', 1, N'rehman.sheikh@ke.com.pk', N'mazhar.haider@ke.com.pk', CAST(N'2020-01-08 13:06:24.983' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (12, N'BQ2', 1, N'aslam.warsi@ke.com.pk', N'muhammad.ansar@ke.com.pk', CAST(N'2020-01-08 13:06:33.613' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (13, N'BQ2', 1, N'imranur.rahim@ke.com.pk', N'umair.abid@ke.com.pk', CAST(N'2020-01-08 13:06:41.327' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (14, N'BQ2', 1, N'afzal.sher@ke.com.pk', N'ikhtiar.ahmed@ke.com.pk', CAST(N'2020-01-08 13:06:49.407' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (15, N'BQ2', 1, N'mazhar.haider@ke.com.pk', N'hassan.sajjad@ke.com.pk', CAST(N'2020-01-08 13:07:02.907' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (16, N'BQ2', 1, N'muhammad.ansar@ke.com.pk', N'umer.khalid@ke.com.pk', CAST(N'2020-01-08 13:07:11.863' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (17, N'BQ2', 1, N'umair.abid@ke.com.pk', N'asad.rahman@ke.com.pk', CAST(N'2020-01-08 13:07:50.343' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (18, N'BQ2', 1, N'ikhtiar.ahmed@ke.com.pk', N'kaleemullah.khan@ke.com.pk', CAST(N'2020-01-08 13:08:39.987' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (19, N'BQ2', 1, N'hassan.sajjad@ke.com.pk', N'khurram.khan@ke.com.pk', CAST(N'2020-01-08 13:09:19.997' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (20, N'BQ2', 1, N'umer.khalid@ke.com.pk', N'gotam.ram@ke.com.pk', CAST(N'2020-01-08 13:09:26.250' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (21, N'KPC', 1, N'imtiaz.alam@ke.com.pk', N'imran.mukhtar@ke.com.pk', CAST(N'2020-01-08 13:09:38.860' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (22, N'KPC', 1, N'javed.mateen@ke.com.pk', N'muhammad.anas@ke.com.pk', CAST(N'2020-01-08 13:09:45.360' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (23, N'KPC', 1, N'asad.rahman@ke.com.pk', N'sajjad.raza@ke.com.pk', CAST(N'2020-01-08 13:09:51.437' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (24, N'KPC', 1, N'kaleemullah.khan@ke.com.pk', N'abbas.zulfiqar@ke.com.pk', CAST(N'2020-01-08 13:09:58.090' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (25, N'KPC', 1, N'khurram.khan@ke.com.pk', N'waseem.sabir@ke.com.pk', CAST(N'2020-01-08 13:10:04.817' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (26, N'KPC', 1, N'gotam.ram@ke.com.pk', N'talha.khubaib@ke.com.pk', CAST(N'2020-01-08 13:10:06.583' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (27, N'KPC', 1, N'imran.mukhtar@ke.com.pk', N'toqeer.alam@ke.com.pk', CAST(N'2020-01-08 13:10:24.307' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (28, N'KPC', 1, N'muhammad.anas@ke.com.pk', N'javed.mumtaz@ke.com.pk', CAST(N'2020-01-08 13:11:25.040' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (29, N'KPC', 1, N'sajjad.raza@ke.com.pk', N'syed.zeeshan@ke.com.pk', CAST(N'2020-01-08 13:11:58.340' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (30, N'KPC', 1, N'abbas.zulfiqar@ke.com.pk', N'mfarhan.alam@ke.com.pk', CAST(N'2020-01-08 13:12:04.557' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (31, N'KPC', 1, N'waseem.sabir@ke.com.pk', N'raza.jamal@ke.com.pk', CAST(N'2020-01-08 13:12:12.077' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (32, N'KPC', 1, N'talha.khubaib@ke.com.pk', N'junaid.waseem@ke.com.pk', CAST(N'2020-01-08 13:12:18.713' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (33, N'SGT', 1, N'saqib.shaheen@ke.com.pk', N'sohaib.khan@ke.com.pk', CAST(N'2020-01-08 13:12:24.627' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (34, N'SGT', 1, N'mahsan.qureshi@ke.com.pk', N'nida.nawab@ke.com.pk', CAST(N'2020-01-08 13:12:31.333' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (35, N'SGT', 1, N'toqeer.alam@ke.com.pk', N'amir.alisyed@ke.com.pk', CAST(N'2020-01-08 14:50:40.563' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (36, N'SGT', 1, N'javed.mumtaz@ke.com.pk', N'shahrukh.Abbas@ke.com.pk', CAST(N'2020-01-08 14:51:14.690' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (37, N'SGT', 1, N'syed.zeeshan@ke.com.pk', N'wajid.shaikh@ke.com.pk', CAST(N'2020-01-08 14:51:46.840' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (38, N'SGT', 1, N'mfarhan.alam@ke.com.pk', N'zarak.Noveed@ke.com.pk', CAST(N'2020-01-08 14:51:53.760' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (39, N'SGT', 1, N'raza.jamal@ke.com.pk', N'mustafa.Khanzada@ke.com.pk', CAST(N'2020-01-08 14:52:02.640' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (40, N'SGT', 1, N'junaid.waseem@ke.com.pk', N'mustafa.Suleman@ke.com.pk', CAST(N'2020-01-08 14:52:08.867' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (41, N'SGT', 1, N'sohaib.khan@ke.com.pk', N'Syed.irfan@ke.com.pk', CAST(N'2020-01-08 14:52:17.440' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (42, N'SGT', 1, N'nida.nawab@ke.com.pk', N'tahirmalik.muhammad@ke.com.pk', CAST(N'2020-01-08 14:53:01.800' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (43, N'KGT', 1, N'adnan.ahmad@ke.com.pk', N'arshad.farooqui@ke.com.pk', CAST(N'2020-01-08 14:54:27.960' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (44, N'KGT', 1, N'Mirza.saad@ke.com.pk', N'mustafa.husain@ke.com.pk', CAST(N'2020-01-08 14:54:35.260' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (45, N'KGT', 1, N'amir.alisyed@ke.com.pk', N'sm.adeel@ke.com.pk', CAST(N'2020-01-08 14:55:10.350' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (46, N'KGT', 1, N'shahrukh.Abbas@ke.com.pk', N'mzafar.iqbal@ke.com.pk', CAST(N'2020-01-08 14:55:16.763' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (47, N'KGT', 1, N'wajid.shaikh@ke.com.pk', N'asmara.shahid@ke.com.pk', CAST(N'2020-01-08 14:55:23.193' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (48, N'KGT', 1, N'zarak.Noveed@ke.com.pk', N'mehdi.ali@ke.com.pk', CAST(N'2020-01-08 14:55:55.157' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (49, N'KGT', 1, N'mustafa.Khanzada@ke.com.pk', N'aamir.fayyaz@ke.com.pk', CAST(N'2020-01-08 14:56:28.247' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (50, N'KGT', 1, N'mustafa.Suleman@ke.com.pk', N'mustafa.husain@ke.com.pk', CAST(N'2020-01-08 14:56:34.827' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (51, N'KGT', 1, N'Syed.irfan@ke.com.pk', N'sm.adeel@ke.com.pk', CAST(N'2020-01-08 14:56:40.983' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (52, N'BQ1', 2, N'razzaq.ahmad@ke.com.pk', N'mzafar.iqbal@ke.com.pk', CAST(N'2020-01-08 14:56:47.157' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (53, N'BQ1', 2, N'tahirmalik.muhammad@ke.com.pk', N'asmara.shahid@ke.com.pk', CAST(N'2020-01-08 14:56:54.330' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (54, N'BQ1', 2, N'arshad.farooqui@ke.com.pk', N'imtiaz.alam@ke.com.pk', CAST(N'2020-01-08 14:57:31.487' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (55, N'BQ1', 2, N'mustafa.husain@ke.com.pk', N'javed.mateen@ke.com.pk', CAST(N'2020-01-08 14:57:59.490' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (56, N'BQ1', 2, N'sm.adeel@ke.com.pk', N'mustafa.husain@ke.com.pk', CAST(N'2020-01-08 14:58:06.570' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (57, N'BQ1', 2, N'mzafar.iqbal@ke.com.pk', N'sm.adeel@ke.com.pk', CAST(N'2020-01-08 14:58:12.680' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (58, N'BQ1', 2, N'asmara.shahid@ke.com.pk', N'mzafar.iqbal@ke.com.pk', CAST(N'2020-01-08 14:58:19.290' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (59, N'BQ2', 2, N'tahir.ali@ke.com.pk', N'asmara.shahid@ke.com.pk', CAST(N'2020-01-08 14:58:26.130' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (60, N'BQ2', 2, N'mehdi.ali@ke.com.pk', N'saqib.shaheen@ke.com.pk', CAST(N'2020-01-08 14:59:08.300' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (61, N'BQ2', 2, N'aamir.fayyaz@ke.com.pk', N'mahsan.qureshi@ke.com.pk', CAST(N'2020-01-08 14:59:43.680' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (62, N'BQ2', 2, N'mustafa.husain@ke.com.pk', N'mustafa.husain@ke.com.pk', CAST(N'2020-01-08 14:59:50.407' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (63, N'BQ2', 2, N'sm.adeel@ke.com.pk', N'sm.adeel@ke.com.pk', CAST(N'2020-01-08 14:59:56.663' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (64, N'BQ2', 2, N'mzafar.iqbal@ke.com.pk', N'mzafar.iqbal@ke.com.pk', CAST(N'2020-01-08 15:00:02.933' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (65, N'BQ2', 2, N'asmara.shahid@ke.com.pk', N'asmara.shahid@ke.com.pk', CAST(N'2020-01-08 15:00:12.060' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (66, N'KPC', 2, N'nowshad.alam@ke.com.pk', N'adnan.ahmad@ke.com.pk', CAST(N'2020-01-08 15:00:39.693' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (67, N'KPC', 2, N'imtiaz.alam@ke.com.pk', N'Mirza.saad@ke.com.pk', CAST(N'2020-01-08 15:01:08.783' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (68, N'KPC', 2, N'javed.mateen@ke.com.pk', N'mustafa.husain@ke.com.pk', CAST(N'2020-01-08 15:01:15.083' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (69, N'KPC', 2, N'mustafa.husain@ke.com.pk', N'sm.adeel@ke.com.pk', CAST(N'2020-01-08 15:01:22.120' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (70, N'KPC', 2, N'sm.adeel@ke.com.pk', N'mzafar.iqbal@ke.com.pk', CAST(N'2020-01-08 15:01:28.800' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (71, N'KPC', 2, N'mzafar.iqbal@ke.com.pk', N'asmara.shahid@ke.com.pk', CAST(N'2020-01-08 15:01:36.097' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (72, N'KPC', 2, N'asmara.shahid@ke.com.pk', NULL, CAST(N'2020-01-14 14:43:04.183' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (73, N'SGT', 2, N'muhammad.s@ke.com.pk', NULL, CAST(N'2020-01-14 14:45:20.050' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (74, N'SGT', 2, N'saqib.shaheen@ke.com.pk', NULL, CAST(N'2020-01-14 14:46:02.383' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (75, N'SGT', 2, N'mahsan.qureshi@ke.com.pk', NULL, CAST(N'2020-01-14 14:46:09.230' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (76, N'SGT', 2, N'mustafa.husain@ke.com.pk', NULL, CAST(N'2020-01-14 14:46:15.977' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (77, N'SGT', 2, N'sm.adeel@ke.com.pk', NULL, CAST(N'2020-01-14 14:46:21.893' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (78, N'SGT', 2, N'mzafar.iqbal@ke.com.pk', NULL, CAST(N'2020-01-14 14:46:29.593' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (79, N'SGT', 2, N'asmara.shahid@ke.com.pk', NULL, CAST(N'2020-01-14 14:46:37.973' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (80, N'KGT', 2, N'nowshad.alam@ke.com.pk', NULL, CAST(N'2020-01-14 14:47:33.617' AS DateTime), N'TO', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (81, N'KGT', 2, N'adnan.ahmad@ke.com.pk', NULL, CAST(N'2020-01-14 14:48:06.880' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (82, N'KGT', 2, N'Mirza.saad@ke.com.pk', NULL, CAST(N'2020-01-14 14:48:13.230' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (83, N'KGT', 2, N'mustafa.husain@ke.com.pk', NULL, CAST(N'2020-01-14 14:48:20.150' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (84, N'KGT', 2, N'sm.adeel@ke.com.pk', NULL, CAST(N'2020-01-14 14:48:26.417' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (85, N'KGT', 2, N'mzafar.iqbal@ke.com.pk', NULL, CAST(N'2020-01-14 14:48:32.820' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (86, N'KGT', 2, N'asmara.shahid@ke.com.pk', NULL, CAST(N'2020-01-14 14:48:41.023' AS DateTime), N'CC', 1)
INSERT [dbo].[xEmailReminderRecipientsForNotification] ([ID], [SiteID], [ReminderNo], [ToRecipients], [CCRecipients], [RedgDateTime], [RType], [Active]) VALUES (87, N'KPC', 2, NULL, N'rabha.shoaib@ke.com.pk', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[xEmailReminderRecipientsForNotification] OFF
ALTER TABLE [dbo].[xEmailReminderRecipients]  WITH CHECK ADD FOREIGN KEY([SiteID])
REFERENCES [dbo].[SitesMaster] ([SiteId])
GO
ALTER TABLE [dbo].[xEmailReminderRecipientsForNotification]  WITH CHECK ADD FOREIGN KEY([SiteID])
REFERENCES [dbo].[SitesMaster] ([SiteId])
GO
