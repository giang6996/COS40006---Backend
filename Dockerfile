# Stage 1: Base image with ASP.NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use for heathy check
RUN apt-get update && apt-get install -y curl

# Stage 2: Build the app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the project files for each layer
COPY ["Server.Presentation/Server.Presentation.csproj", "Server.Presentation/"]
COPY ["Server.BusinessLogic/Server.BusinessLogic.csproj", "Server.BusinessLogic/"]
COPY ["Server.Common/Server.Common.csproj", "Server.Common/"]
COPY ["Server.DataAccess/Server.DataAccess.csproj", "Server.DataAccess/"]
COPY ["Server.Models/Server.Models.csproj", "Server.Models/"]

# Restore dependencies
RUN dotnet restore "Server.Presentation/Server.Presentation.csproj"

# Copy the entire source code for all layers
COPY . .

# Build the project
RUN dotnet build "Server.Presentation/Server.Presentation.csproj" -c Release -o /app/build

# Stage 3: Publish the app
FROM build AS publish
RUN dotnet publish "Server.Presentation/Server.Presentation.csproj" -c Release -o /app/publish

# Stage 4: Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy the self-signed certificate into the container
COPY aspnetapp.pfx /https/aspnetapp.pfx

# Specify the entry point to run the app
ENTRYPOINT ["dotnet", "Server.Presentation.dll"]