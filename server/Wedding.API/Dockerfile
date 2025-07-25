FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY "Wedding.API.csproj" .
RUN dotnet restore "Wedding.API.csproj"

COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+8080
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Wedding.API.dll"]
