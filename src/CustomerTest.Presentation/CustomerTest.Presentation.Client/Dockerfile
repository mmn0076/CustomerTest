FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5091


FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/CustomerTest.Presentation/CustomerTest.Presentation.Contracts/CustomerTest.Presentation.Contracts.csproj", "src/CustomerTest.Presentation/CustomerTest.Presentation.Contracts/"]
COPY ["src/CustomerTest.Presentation/CustomerTest.Presentation.Client/CustomerTest.Presentation.Client.csproj", "src/CustomerTest.Presentation/CustomerTest.Presentation.Client/"]
RUN dotnet restore "src/CustomerTest.Presentation/CustomerTest.Presentation.Client/CustomerTest.Presentation.Client.csproj"
COPY . .
WORKDIR "src/CustomerTest.Presentation/CustomerTest.Presentation.Client"
RUN dotnet build "CustomerTest.Presentation.Client.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CustomerTest.Presentation.Client.csproj" -c Release -o /app

FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/wwwroot .
COPY --from=publish /app/nginx.conf /etc/nginx/nginx.conf

