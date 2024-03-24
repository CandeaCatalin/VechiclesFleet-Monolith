FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["VehiclesFleet/VechiclesFleet.csproj", "VehiclesFleet/"]
COPY ["VehiclesFleet.Business/VechiclesFleet.Business.csproj", "VehiclesFleet.Business/"]
COPY ["VehiclesFleet.DataAccess/VechiclesFleet.DataAccess.csproj", "VehiclesFleet.DataAccess/"]
COPY ["VehiclesFleet.DI/VechiclesFleet.DI.csproj", "VehiclesFleet.DI/"]
COPY ["VehiclesFleet.Domain/VechiclesFleet.Domain.csproj", "VehiclesFleet.Domain/"]
COPY ["VehiclesFleet.Repository/VechiclesFleet.Repository.csproj", "VehiclesFleet.Repository/"]
COPY ["VehiclesFleet.Services/VechiclesFleet.Services.csproj", "VehiclesFleet.Services/"]
COPY ["VehiclesFleet.Settings/VechiclesFleet.Settings.csproj", "VehiclesFleet.Settings/"]
RUN dotnet restore "VehiclesFleet/VehiclesFleet.csproj"
COPY . .
WORKDIR "/src/VechiclesFleet"
RUN dotnet build "VehiclesFleet.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VehiclesFleet.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VehiclesFleet.dll"]
