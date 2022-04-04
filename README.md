# TFGTemplateDemo
Welcome to your newly created solution! This document will provide some information on restoring the nuget packages, creating your first migration and setting up your pipelines.

# Getting Started
##	Solution Name
* Double check the name of the solution file (*.sln) created and rename it if required. 

##	Restore nuget packages
* Ensure that the TFG Azure Devops nuget server (`https://thefoschinigroup.pkgs.visualstudio.com/_packaging/TFG/nuget/v3/index.json`) is configured as a package source in Visual Studio.
* Right click on the solution and select `Restore NuGet Packages`.

## (Optional) Change default database schema
* The default schema for tables is set to 'Tfg'. This can be changed to align with your application's naming conventions.
* In the file 'Tfg.TFGTemplateDemo.Data' > Scripts > 1. Add Filegroups PRE.sql, change `SET @Schema = 'Tfg'` to your own schema
* In the file 'Tfg.TFGTemplateDemo.Data' > Scripts > 2. Permissions PRE.sql, change the line `SET @QuoteSchema = QUOTENAME('Tfg')` to your own schema
* In the file 'Tfg.TFGTemplateDemo.Data' > DatabaseSetup.cs (OnModelCreating method), change the `schemaName` parameter on the line `MapAssemblyToSchema(modelBuilder, "Tfg.TFGTemplateDemo.Data.Entities.dll", "Tfg");` to the same schema set in the previous step.
* In the file 'Tfg.TFGTemplateDemo.Data' > DatabaseSqlGenerator.cs (constructor), change the line 'defaultFileGroup = "Tfg"' to the same schema as set before

## Database authentication and permissions
### **SQL authentication**
The SQL authentication method should be used if the api will be running in a container. 
* In the file 'Tfg.TFGTemplateDemo.Data' > Scripts > 2. Permissions PRE.sql* a default SQL user has been scripted to be created. You can change the default username/password here.
* The connection string in all the presentation projects (apart from the Tfg.TFGTemplateDemo.Api project) has been defaulted to use the default SQL username/password.
* The connection string in Tfg.TFGTemplateDemo.Api defaults to integrated authentication when the project is first created, as the first time the project runs the SQL user needs to be created before it can be used. Once the Tfg.TFGTemplateDemo.Api has successfully be run, and the SQL user created, then the connection string can be changed to use SQL authentication (this is only an issue when running locally as when running in DEV, TST or PRD the CI/CD pipeline will execute the database updates before the service starts up)
<br>

### **Windows authentication/integrated Security**
The Windows authentication/integrated security method should be used if the api will be running in IIS on Windows. <br>
Have a new security group created for your application or use an existing security group used by your team.
* In the file 'Tfg.TFGTemplateDemo.Data' > Scripts > 2. Permissions PRE.sql, change the default security group to an application specific security group
* Change the 'SET @LoginName = ' for DEV, TST and PRD to a valid security group in active directory

## SQL Express
* Ensure that you have at least version 13.0.5026.0 of SQL Express installed.
* If you have a lower version, uninstall that version and install SQL Server Express 2016 SP 2 from: \\fs4\dsl$\Definitive Software Library\Microsoft\Visual Studio\2019\VS 2019 Dependencies

## (Optional) Create initial migration 
* The initial migration has been included. To rerun it or add additional migrations, delete the file 'Tfg.TFGTemplateDemo.Data' > Migrations > DatabaseSetupMigrations > *_InitialCreate.cs and follow the steps below.
* Set the project Tfg.TFGTemplateDemo.Data as the startup project as well as the default project in the Package Manager console
* Run the following in the Package Manager console: `Add-Migration NameOfMigration -context DatabaseContext`

If the following error is encountered `Add-Migration : A parameter cannot be found that matches parameter name 'context'`, run the following command instead of the above:
* `EntityFrameworkCore\Add-Migration NameOfMigration -context DatabaseContext`

The (currently) supported version of EF Core Tools required to perform this action is 3.1.*. To confirm and update if required, 
run the following commands in the `Package Manager Console` in Visual Studio:
* To confirm installed version: `dotnet ef --version`
* If not 3.1.*, uninstall existing version: `dotnet tool uninstall --global dotnet-ef`
* Install 3.1.* version: `dotnet tool install --global dotnet-ef --version 3.1.9`



## Generate migration sql script
A migration script is required to update the application's database in the test and production environments.
* Run the following commands in the Package Manager console:
    * cd '.\Solution Items'
    * `.\Migrate.ps1`

## Run the application
* Set the 'Tfg.TFGTemplateDemo.Api' project as the startup project to run the application.

## Using the WCF Endpoint Helper
When using the WCF endpoint helper to call WCF services, ensure that all System.ServiceModel.* dependencies are set to version 4.4.4. Upgrading these dependencies causes Kerberos issues when impersonating the user to the WCF service.

## Worker/Scheduler API
Ensure that the migration script for HangFire is included. Also ensure that the correct service accounts are configured in the migration script (in the "Custom TFG scripts" section)

# Setup Azure DevOps
## Add solution to source control
https://tfgonline.sharepoint.com/sites/EAT/SitePages/Visual%20Studio%20Developer%20Pack/Create-and-configure-a-new-Git-repository.aspx

## Create build and release pipelines
https://tfgonline.sharepoint.com/sites/EAT/SitePages/Visual%20Studio%20Developer%20Pack/Setup-Azure-DevOps-pipelines.aspx

## Troubleshooting
* For any assistance with toubleshooting your solution, go to the following link:
https://tfgonline.sharepoint.com/sites/EAT/SitePages/Visual%20Studio%20Developer%20Pack/Visual-Studio-2019-Developer-Pack-Troubleshooting.aspx