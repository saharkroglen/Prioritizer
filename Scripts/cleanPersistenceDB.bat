#see reference here: http://blogs.msdn.com/b/carlos/archive/2013/01/10/workflow-foundation-sql-scripts.aspx


#create durable workflow schema and logic 
sqlcmd -S .\sqlexpress -d prioritizerPersistenceDB -i %WINDIR%\Microsoft.NET\Framework\v4.0.30319\SQL\en\SqlPersistenceProviderSchema.sql
sqlcmd -S .\sqlexpress -d prioritizerPersistenceDB -i %WINDIR%\Microsoft.NET\Framework\v4.0.30319\SQL\en\SqlPersistenceProviderLogic.sql

#clean schema
sqlcmd -S .\sqlexpress -d prioritizerPersistenceDB -i %WINDIR%\Microsoft.NET\Framework\v4.0.30319\SQL\en\SqlWorkflowInstanceStoreSchema.sql
sqlcmd -S .\sqlexpress -d prioritizerPersistenceDB -i %WINDIR%\Microsoft.NET\Framework\v4.0.30319\SQL\en\SqlWorkflowInstanceStoreLogic.sql
sqlcmd -S .\sqlexpress -d prioritizerPersistenceDB -i %WINDIR%\Microsoft.NET\Framework\v4.0.30319\SQL\en\SqlWorkflowInstanceStoreSchemaUpgrade.sql

pause

