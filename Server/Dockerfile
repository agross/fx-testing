FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY . .
RUN ls -la
RUN dotnet restore "Server/Server.csproj"
RUN dotnet build "Server/Server.csproj" \
           --no-restore \
           --configuration Release \
           --output /app/build
RUN dotnet publish "Server/Server.csproj" \
           --no-restore \
           --configuration Release \
           --output /app/publish \
           -p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80/tcp
EXPOSE 443/tcp

ENTRYPOINT ["dotnet", "Server.dll"]
