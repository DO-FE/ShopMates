# Shop-Mates
This project is developed using C Sharp language with .Net ASP 6, Docker, MSSQL-JDBC

## Author
- Lâm Nhật Tiến - AxyRes
- Overview
- Shop-Mates is about Shopping Cart for Shop Online

## Features
- User authentication and authorization
- Shop
- Getting Started
- To run this application, you need to have C Sharp language with .Net ASP 6, Docker, MSSQL-JDBC installed on your computer.

## Install Tools
- .NET Core SDK 3.1
- Git client
- Visual Studio 2019
- SQL Server 2019

## How to configure and run
- Clone code from Github: git clone https://github.com/DO-FE/ShopMates
- Open solution eShopSolution.sln in Visual Studio 2022
- Set startup project is ShopMates.Data
- Change connection string in Appsetting.json in ShopMates.Data project
- Open Tools --> Nuget Package Manager -->  Package Manager Console in Visual Studio
- Run Update-database and Enter.
- After migrate database successful, set Startup Project is ShopMates.WebApp
- Change database connection in appsettings.Development.json in ShopMates.WebApp project.
- You need to change 3 projects to self-host profile.
- Set multiple run project: Right click to Solution and choose Properties and set Multiple Project, choose Start for 3 Projects: BackendApi, WebApp and AdminApp.
- Choose profile to run or press F5

------------------------------

## License
- This project is licensed under the AxyRes License. See the LICENSE file for details.

## Contact
- For any issues or suggestions related to this project, please contact me at nhattien.lam.545@gmail.com.