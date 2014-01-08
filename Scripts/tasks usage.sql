select Users.userName, tasks.taskName,Tenant.TenantName 
from tasks  join 
Tenant on Tasks.TenantID=Tenant.ID join 
Users on Users.ID = Tasks.userID
order by Tenant.TenantName, users.userName
