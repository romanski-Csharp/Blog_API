FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копіюємо csproj та відновлюємо пакети
COPY Blog_API/Blog_API.csproj Blog_API/
WORKDIR /src/Blog_API
RUN dotnet restore

# Копіюємо решту файлів
COPY Blog_API/. .

# Публікуємо проєкт
RUN dotnet publish -c Release -o /app/publish

# Фінальний образ
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Blog_API.dll"]
