# Stage 1: Base Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

ENV ASPNETCORE_URLS=http://+:8000

# Stage 2: Build Image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution file
COPY SmartTextSummarizerAgent.sln . 

# Create project folder and copy the project file
RUN mkdir SmartTextSummarizerAgent
COPY SmartTextSummarizerAgent/SmartTextSummarizerAgent.csproj SmartTextSummarizerAgent/
RUN dotnet restore SmartTextSummarizerAgent.sln

# Copy everything else
COPY . .

# Set the working directory to the project folder
WORKDIR /src/SmartTextSummarizerAgent

# Build the application
RUN dotnet build SmartTextSummarizerAgent.csproj -c Release -o /app/build

# Stage 3: Publish Image
FROM build AS publish
RUN dotnet publish SmartTextSummarizerAgent.csproj -c Release -o /app/publish /p:UseAppHost=false

# Stage 4: Final Image
FROM base AS final
WORKDIR /app

# Copy the published output from the publish stage
COPY --from=publish /app/publish .

# Command to run the application
ENTRYPOINT ["dotnet", "SmartTextSummarizerAgent.dll"]
