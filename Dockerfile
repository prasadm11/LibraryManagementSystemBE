FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LibraryManagementSystem.API/LibraryManagementSystem.API.csproj", "LibraryManagementSystem.API/"]
COPY ["LibraryManagementSystem.Application/LibraryManagementSystem.Application.csproj", "LibraryManagementSystem.Application/"]
COPY ["LibraryManagementSystem.Core/LibraryManagementSystem.Core.csproj", "LibraryManagementSystem.Core/"]
COPY ["LibraryManagementSystem.Infrastructure/LibraryManagementSystem.Infrastructure.csproj", "LibraryManagementSystem.Infrastructure/"]
RUN dotnet restore "LibraryManagementSystem.API/LibraryManagementSystem.API.csproj"
COPY . .
WORKDIR "/src/LibraryManagementSystem.API"
RUN dotnet build "./LibraryManagementSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LibraryManagementSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibraryManagementSystem.API.dll"]
