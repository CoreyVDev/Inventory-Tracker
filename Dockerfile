# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy everything
COPY . .

# Restore and build
RUN dotnet restore InventoryTracker.csproj
RUN dotnet publish InventoryTracker.csproj -c Release -o out

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "InventoryTracker.dll"]


