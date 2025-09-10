FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Blog_API/Blog_API.csproj Blog_API/
WORKDIR /src/Blog_API
RUN dotnet restore

COPY Blog_API/. .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Blog_API.dll"]
