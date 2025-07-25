FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Wedding.API.csproj", "Wedding.API/"]
RUN dotnet restore "Wedding.API/Wedding.API.csproj"]

COPY . .

WORKDIR /src/Wedding.API
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+8080
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Wedding.API.dll"]
