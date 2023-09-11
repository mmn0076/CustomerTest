FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5090

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY CustomerTest.sln ./
COPY ["src/CustomerTest.Application/CustomerTest.Application.csproj", "src/CustomerTest.Application/"]
COPY ["src/CustomerTest.Domain/CustomerTest.Domain.csproj", "src/CustomerTest.Domain/"]
COPY ["src/CustomerTest.Infrastructure/CustomerTest.Infrastructure.csproj", "src/CustomerTest.Infrastructure/"]
COPY ["src/CustomerTest.Presentation/CustomerTest.Presentation.Api/CustomerTest.Presentation.Api.csproj", "src/CustomerTest.Presentation/CustomerTest.Presentation.Api/"]
COPY ["src/CustomerTest.Presentation/CustomerTest.Presentation.Contracts/CustomerTest.Presentation.Contracts.csproj", "src/CustomerTest.Presentation/CustomerTest.Presentation.Contracts/"]
COPY ["test/CustomerTest.EndToEndTests/CustomerTest.EndToEndTests.csproj", "src/CustomerTest.EndToEndTests/CustomerTest.EndToEndTests/"]

RUN dotnet restore "src/CustomerTest.Presentation/CustomerTest.Presentation.Api/CustomerTest.Presentation.Api.csproj"
COPY . .
WORKDIR "src/CustomerTest.Presentation/CustomerTest.Presentation.Api"
RUN dotnet build "CustomerTest.Presentation.Api.csproj" -c Release -o /app/build

#WORKDIR /src
#RUN dotnet test  CustomerTest.sln

FROM build AS publish
RUN dotnet publish "CustomerTest.Presentation.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerTest.Presentation.Api.dll"]
