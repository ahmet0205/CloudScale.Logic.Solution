# Temel çalışma ortamı
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# İnşa (Build) aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# GitHub'daki gerçek dosya ismini kopyalıyoruz
COPY ["CloudScale.Logic.API.csproj", "."]
RUN dotnet restore "./CloudScale.Logic.API.csproj"

# Kalan tüm dosyaları kopyala ve inşa et
COPY . .
WORKDIR "/src/."
RUN dotnet build "./CloudScale.Logic.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Yayınlama (Publish) aşaması
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CloudScale.Logic.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final aşaması
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CloudScale.Logic.API.dll"]