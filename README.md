# ETicaretCore - E-commerce Web App

A full-stack e-commerce web application built with ASP.NET Core MVC (.NET 6) on a layered (N-tier) architecture. Capstone project of the Bilge Adam software development programme (2022), built in a team of two.

## Features
- Product and category management (CRUD)
- User registration and login with cookie authentication and roles
- Country and city data for user profiles

## Architecture
- AppCore: reusable base layer (generic Entity Framework repository, result models)
- DataAccess: Entity Framework Core entities, DbContext, repositories and SQL Server migrations
- Business: services and view models
- MvcWebUI: ASP.NET Core MVC web UI (controllers and Razor views)

## Tech stack
C#, .NET 6, ASP.NET Core MVC, Entity Framework Core, SQL Server, Razor, JavaScript

## Run locally
1. Set the SQL Server connection string in MvcWebUI/appsettings.json
2. Run `dotnet ef database update` from the DataAccess project
3. Run `dotnet run` from the MvcWebUI project
