﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Store.csproj", "./"]
RUN dotnet restore "Store.csproj"
COPY . .

RUN dotnet build "Store.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "Store.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Store.dll"]
