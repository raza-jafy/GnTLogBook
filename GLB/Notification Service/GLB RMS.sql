


select * from xEmailReminderRecipientsForNotification
where SiteID='KPC' and ReminderNo<=1


select * from xEmailReminderRecipientsForNotification
where CCRecipients='G&TPlanning@ke.com.pk' and ReminderNo<=1







insert into xEmailReminderRecipientsForNotification (SiteID,ReminderNo,ToRecipients,Active,RType)
values ('BQ1',1,'G&TPlanning@ke.com.pk',1,'CC')


insert into xEmailReminderRecipientsForNotification (SiteID,ReminderNo,ToRecipients,Active,RType)
values ('BQ2',1,'G&TPlanning@ke.com.pk',1,'CC')

insert into xEmailReminderRecipientsForNotification (SiteID,ReminderNo,ToRecipients,Active,RType)
values ('KGT',1,'G&TPlanning@ke.com.pk',1,'CC')

insert into xEmailReminderRecipientsForNotification (SiteID,ReminderNo,ToRecipients,Active,RType)
values ('SGT',1,'G&TPlanning@ke.com.pk',1,'CC')

insert into xEmailReminderRecipientsForNotification (SiteID,ReminderNo,ToRecipients,Active,RType)
values ('KPC',1,'G&TPlanning@ke.com.pk',1,'CC')



insert into xEmailReminderRecipientsForNotification (SiteID,ReminderNo,ToRecipients,Active,RType)
values ('KPC',1,'Syed.irfan@ke.com.pk',1,'CC')





insert into xEmailReminderRecipientsForNotification (SiteID,ReminderNo,ToRecipients,Active,RType)
values ('KGT',1,'shehzaib.amjad@ke.com.pk',1,'CC')

update xEmailReminderRecipientsForNotification set Active=0 where ID=51

update xEmailReminderRecipientsForNotification set ToRecipients='Syed.irfan@ke.com.pk' where ID=93


UPDATE xEmailReminderRecipientsForNotification
SET ToRecipients='G&TPlanning@ke.com.pk' WHERE ID IN (90,91,92,94,95)





---------------
select ID,SiteID,ToRecipients,RecipientType,ReminderNo,ACTIVE from (
select ID,SiteID,ToRecipients,'TO' RecipientType ,
case ReminderNo when 1 then 'First | 2nd Reminder Recipients' else 'Final Reminder Recipients' end ReminderNo,
CASE ACTIVE when 1 then 'Active' else 'In Active' end ACTIVE
--ReminderNo 
from xEmailReminderRecipientsForNotification 
where SiteID In (select distinct SiteId from SitesMaster) and ToRecipients is not null
AND ACTIVE=1 
AND  RType='TO'

union all

select ID,SiteID,ToRecipients,'CC' RecipientType,
case ReminderNo when 1 then 'First | 2nd Reminder Recipients' else 'Final Reminder Recipients' end ReminderNo,
CASE ACTIVE when 1 then 'Active' else 'In Active' end ACTIVE
--ReminderNo
from xEmailReminderRecipientsForNotification
where SiteID In (select distinct SiteId from SitesMaster) and  ToRecipients is not null
AND ACTIVE=1  AND  RType='CC') x 
order by x.SiteID


