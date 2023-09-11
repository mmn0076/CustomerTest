FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5090

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY CustomerTest.sln src/
COPY ["src/CustomerTest.Application/CustomerTest.Application.csproj", "src/CustomerTest.Application/"]
COPY ["src/CustomerTest.Domain/CustomerTest.Domain.csproj", "src/CustomerTest.Domain/"]
COPY ["src/CustomerTest.Infrastructure/CustomerTest.Infrastructure.csproj", "src/CustomerTest.Infrastructure/"]
COPY ["src/CustomerTest.Presentation/CustomerTest.Presentation.Api/CustomerTest.Presentation.Api.csproj", "src/CustomerTest.Presentation/CustomerTest.Presentation.Api/"]
COPY ["src/CustomerTest.Presentation/CustomerTest.Presentation.Contracts/CustomerTest.Presentation.Contracts.csproj", "src/CustomerTest.Presentation/CustomerTest.Presentation.Contracts/"]
COPY ["test/CustomerTest.EndToEndTests/CustomerTest.EndToEndTests.csproj", "src/CustomerTest.EndToEndTests/"]

RUN dotnet restore "src/CustomerTest.Presentation/CustomerTest.Presentation.Api/CustomerTest.Presentation.Api.csproj"
RUN dotnet restore "src/CustomerTest.EndToEndTests/CustomerTest.EndToEndTests.csproj"
COPY . .
WORKDIR src/CustomerTest.Presentation/CustomerTest.Presentation.Api
RUN dotnet build "CustomerTest.Presentation.Api.csproj" -c Release -o /app/build

WORKDIR ../../../src/CustomerTest.EndToEndTests
RUN dotnet build "CustomerTest.EndToEndTests.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR ../../src/CustomerTest.Presentation/CustomerTest.Presentation.Api
RUN dotnet publish "CustomerTest.Presentation.Api.csproj" -c Release -o /app/publish

WORKDIR ../../../src/CustomerTest.EndToEndTests
RUN dotnet publish "CustomerTest.EndToEndTests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerTest.Presentation.Api.dll"]
