# Stage 1: Base image with .NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
USER app
EXPOSE 8080

# Stage 2: Build environment with .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy all project files
COPY src/ .

# Restore dependencies
RUN dotnet restore "LocalFriendzApi.UI/LocalFriendzApi.UI.csproj"

# Build the project
WORKDIR "/src/LocalFriendzApi.UI"
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: Publish
FROM build AS publish
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 4: Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LocalFriendzApi.UI.dll"]
