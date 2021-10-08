FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY . .
RUN ls -la
RUN dotnet restore "MT.Backend/MT.Backend.csproj"
RUN dotnet build "MT.Backend/MT.Backend.csproj" \
           --no-restore \
           --configuration Release \
           --output /app/build
RUN dotnet publish "MT.Backend/MT.Backend.csproj" \
           --no-restore \
           --configuration Release \
           --output /app/publish \
           -p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "MT.Backend.dll"]