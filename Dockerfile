FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["MAEVEN.Backend.csproj", "./"]
RUN dotnet restore "MAEVEN.Backend.csproj"

COPY . .
RUN dotnet publish "MAEVEN.Backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 10000

ENTRYPOINT ["dotnet", "MAEVEN.Backend.dll"]
