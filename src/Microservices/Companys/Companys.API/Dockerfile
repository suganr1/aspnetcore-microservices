#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Microservices/Companys/Companys.API/Companys.API.csproj", "Microservices/Companys/Companys.API/"]
COPY ["Microservices/Companys/Companys.Infrastructure/Companys.Infrastructure.csproj", "Microservices/Companys/Companys.Infrastructure/"]
COPY ["Microservices/Companys/Companys.Application/Companys.Application.csproj", "Microservices/Companys/Companys.Application/"]
COPY ["Microservices/Companys/Companys.Domain/Companys.Domain.csproj", "Microservices/Companys/Companys.Domain/"]
COPY ["CommonLogging/EventBus.Messages/EventBus.Messages.csproj", "CommonLogging/EventBus.Messages/"]
COPY ["CommonLogging/Common.Logging/Common.Logging.csproj", "CommonLogging/Common.Logging/"]
RUN dotnet restore "Microservices/Companys/Companys.API/Companys.API.csproj"
COPY . .
WORKDIR "/src/Microservices/Companys/Companys.API"
RUN dotnet build "Companys.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Companys.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Companys.API.dll"]