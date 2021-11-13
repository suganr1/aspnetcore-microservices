#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Gateway/Aggregator.API/Aggregator.API.csproj", "Gateway/Aggregator.API/"]
COPY ["CommonLogging/Common.Logging/Common.Logging.csproj", "CommonLogging/Common.Logging/"]
RUN dotnet restore "Gateway/Aggregator.API/Aggregator.API.csproj"
COPY . .
WORKDIR "/src/Gateway/Aggregator.API"
RUN dotnet build "Aggregator.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aggregator.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aggregator.API.dll"]
