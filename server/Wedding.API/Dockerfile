FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Restaurar dependências
COPY *.csproj ./
RUN dotnet restore

# Build da aplicação
COPY . ./
RUN dotnet publish -c Release -o out

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Wedding.API.dll"]
