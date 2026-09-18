# ---------- Build stage ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копіюємо спочатку тільки csproj — щоб dotnet restore кешувався
# і не перевиконувався при кожній зміні коду
COPY WatchesShop/WatchesShop.csproj WatchesShop/
RUN dotnet restore WatchesShop/WatchesShop.csproj

# Тепер копіюємо решту коду і публікуємо
COPY WatchesShop/ WatchesShop/
WORKDIR /src/WatchesShop
RUN dotnet publish -c Release -o /app/publish --no-restore

# ---------- Runtime stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

# Контейнер слухає лише HTTP (TLS термінується поза контейнером,
# або просто не потрібен для локальної демонстрації)
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Docker
EXPOSE 8080

ENTRYPOINT ["dotnet", "WatchesShop.dll"]
