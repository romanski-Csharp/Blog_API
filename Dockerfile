# Використовуємо офіційний образ .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Встановлюємо робочу директорію
WORKDIR /src

# Копіюємо файли проєкту
COPY ["Blog_API/Blog_API.csproj", "Blog_API/"]

# Відновлюємо залежності
RUN dotnet restore "BlogApi/BlogApi.csproj"

# Копіюємо решту файлів
COPY . .

# Публікуємо проєкт
WORKDIR "/src/Blog_API"
RUN dotnet publish "Blog_API.csproj" -c Release -o /app/publish

# Створюємо фінальний образ
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Копіюємо публікацію з попереднього етапу
COPY --from=build /app/publish .

# Вказуємо команду для запуску
ENTRYPOINT ["dotnet", "Blog_API.dll"]
