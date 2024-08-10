# Use the official .NET runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["media-player-backend.csproj", "./"]
RUN dotnet restore "media-player-backend.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "media-player-backend.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "media-player-backend.csproj" -c Release -o /app/publish

# Create the final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "media-player-backend.dll"]
