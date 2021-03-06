USE [GenerationQA]
GO
/****** Object:  StoredProcedure [dbo].[PrepareDataForsendGenLogBookNotificationMailV2]    Script Date: 4/5/2020 9:22:12 PM ******/
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
	values ((rtrim(ltrim(SUBSTRING(@UnitId_,0,CHARINDEX('-',@UnitId_))))),@UnitId_,dbo.GetRecordCountByUnitID(@UnitId_,1),168,'UnitReadingRegister1')

 end
 else if exists(select * from UnitReadingRegister2 where UnitId=@UnitId_)
 begin

 --select @UnitId as UnitId , dbo.GetRecordCountByUnitID(@UnitId,2) as rcount

 insert into @tbl(SiteID,UnitId,rCount,tConut,RegisterID) 
 values ((rtrim(ltrim(SUBSTRING(@UnitId_,0,CHARINDEX('-',@UnitId_))))),@UnitId_,dbo.GetRecordCountByUnitID(@UnitId_,2),168,'UnitReadingRegister2')


end
else if exists(select * from UnitReadingRegister3 where UnitId=@UnitId_)
 begin


 --select @UnitId as UnitId , dbo.GetRecordCountByUnitID(@UnitId,3) as rcount

 insert into @tbl(SiteID,UnitId,rCount,tConut,RegisterID) values 
 ((rtrim(ltrim(SUBSTRING(@UnitId_,0,CHARINDEX('-',@UnitId_))))),@UnitId_,dbo.GetRecordCountByUnitID(@UnitId_,3),168,'UnitReadingRegister3')

 end


 set @numCount = @numCount +1


FETCH NEXT FROM @bm INTO @UnitId_
END
CLOSE @bm
DEALLOCATE @bm


--select SiteID,UnitId,rCount,tConut,RegisterID
-- from @tbl ORDER BY SiteID



 --snd data for notification-------------------
 if exists(select SiteID,UnitId,rCount,tConut,RegisterID from @tbl)
 begin

 declare @TR as varchar(max)
declare @CCR as varchar(max)

declare @SiteId_NCR as varchar(max)
declare @UnitId_NCR as varchar(max)
declare @rCount_NCR as bigint
declare @tCount_NCR as bigint
declare @RegisterID_NCR as varchar(max)



 DECLARE @cm CURSOR
SET @cm = CURSOR FOR
select SiteID,UnitId,rCount,tConut,RegisterID from @tbl 
--ORDER BY SiteID
OPEN @bm
FETCH NEXT FROM @cm INTO @SiteId_NCR,@UnitId_NCR,@rCount_NCR,@tCount_NCR,@RegisterID_NCR
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
		SET @MAIL_BODYFirst_NCR = '<p>Registration is pending,Please update the portal at the earliest</p><br/>'
		SELECT @MAIL_BODYFirst_NCR = 
			@MAIL_BODYFirst_NCR + '<table border="1" align="center" cellpadding="2" cellspacing="0" style="color:black;font-family:consolas;text-align:center;">' +
			'<tr>
						<th>Site ID</th>
						<th>Unit ID</th>
						<th>MSG</th>
						<th>TR</th>
						<th>CR</th>
			</tr>'
 
		/* ROWS */
		SELECT
			@MAIL_BODYFirst_NCR = @MAIL_BODYFirst_NCR +
				'<tr>' +
				'<td>' + CAST(@SiteId_NCR AS VARCHAR(11)) + '</td>' +
				'<td>' + CAST(@UnitId_NCR AS VARCHAR(11)) + '</td>' +
				--'<td>Registration is Pending, </td>' +
				'<td>' + CAST(@TR AS VARCHAR(max)) + '</td>' +
				'<td>' + CAST(@CCR AS VARCHAR(max)) + '</td>' +
				'</tr>'
		SELECT @MAIL_BODYFirst_NCR = @MAIL_BODYFirst_NCR + '</table>'


		EXEC msdb.dbo.sp_send_dbmail @profile_name='LDCGEN', @body_format = 'HTML',
		@recipients='asmara.shahid@ke.com.pk',
		@copy_recipients = 'rabha.shoaib@ke.com.pk',
		@subject= 'Reminder : Scorecard ELR Portal', @body=@MAIL_BODYFirst_NCR




FETCH NEXT FROM @cm INTO @SiteId_NCR,@UnitId_NCR,@rCount_NCR,@tCount_NCR,@RegisterID_NCR
END
CLOSE @cm
DEALLOCATE @cm




 end





end