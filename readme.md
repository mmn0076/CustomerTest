# How To Run

You Need SQLServer and .Net 7 to run this app.

## Step 1:
Change Database ConnectionString in CustomerTest.Infrastructure.DependencyInjection

## Step 2:
Create Your Database Using This Command (Initial Migration Has Been Created Before)

```
dotnet ef database update -p src/CustomerTest.Infrastructure -s src\CustomerTest.Presentation\CustomerTest.Presentation.Api\CustomerTest.Presentation.Api.csproj
```
Migrate Create Command (if needed)
```
dotnet ef migrations add InitialCreate -p src/CustomerTest.Infrastructure -s src\CustomerTest.Presentation\CustomerTest.Presentation.Api\CustomerTest.Presentation.Api.csproj -c CustomerTestDbContext -o Migrations
```

## Step 3:
build and run CustomerTest.Presentation.Api and CustomerTest.Presentation.Client using :
```
dotnet build
dotnet run
```

## Step 4:
for testing the app you can use :
```
dotnet test
```

## Notes:
Swagger UI is available on ```http://localhost:5090/swagger```

SWA UI is available on ```http://localhost:5091/```