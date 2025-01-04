# School Management
![GitHub repo size](https://img.shields.io/github/repo-size/ChicoFinels/SchoolManagement?style=for-the-badge)
![GitHub language count](https://img.shields.io/github/languages/count/ChicoFinels/SchoolManagement?style=for-the-badge)

<p>This project was developed with .NET 9 during the <a href="https://www.udemy.com/course/learn-aspnet-mvc-and-entity-framework/">ASP.NET Core - Cross-Platform Development</a> course by <a href="https://www.udemy.com/user/trevoirwilliams/">Trevoir Williams</a>.</p>

## 💻 Prerequisites

Before you start, I recommend:
- Install the latest version of Visual Studio;
- Install the latest version of SQL Server and SQL Server Management Studio (SSMS) or Azure Data Studio;

## ☕ Using School Management

<p>Since this project uses the Database First approach, you have to run the SQL script present in this repository to generate the database.</p>
<p>If you want to add something else to the script, you need to run a new database scaffold to update your models, here is an example of the command:</p>

```csharp
Scaffold-DbContext "Server=.\sqlexpress;Database=SchoolManagement;Trusted_Connection=true;MultipleActiveResultSets=true;Encrypt=false" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data -Force -NoOnConfiguring 
```

<p>The authentication of the application is done through a third-party service, Auth0, so in order to authenticate, you will need to have an Auth0 account and configure it with the help of the guides provided by Auth0.</p>
<p>Once the configuration is complete, you will need to replace the Auth0 data in this project's appsettings file with your own data and you will be able to authenticate to the application with the users you created in Auth0.</p>
