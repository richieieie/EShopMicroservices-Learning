FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /App
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EShop.Services/Catalog/Catalog.API/Catalog.API.csproj", "EShop.Services/Catalog/Catalog.API/"]
RUN --mount=type=cache,target=/root/.nuget/packages \ 
    dotnet restore "./EShop.Services/Catalog/Catalog.API/Catalog.API.csproj"
COPY . .
WORKDIR /src/EShop.Services/Catalog/Catalog.API
RUN dotnet build -c ${BUILD_CONFIGURATION} -o /App/Build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Catalog.API.csproj" -c ${BUILD_CONFIGURATION} -o /App/Publish /p:UserAppHost=false

FROM base AS final
WORKDIR /App
COPY --from=publish /App/Publish .
CMD [ "dotnet", "Catalog.API.dll" ]