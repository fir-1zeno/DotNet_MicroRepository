# Simple Web App

A simple, modern web application built with ASP.NET Core.

## Features

- **Modern Design**: Clean, responsive interface with Bootstrap styling
- **Interactive Elements**: JavaScript-powered interactions
- **Hero Section**: Eye-catching gradient header
- **Feature Cards**: Highlighting key capabilities
- **Interactive Demo**: User input demonstration
- **Docker Support**: Fully containerized for easy deployment

## How to Run

### Option 1: Using .NET CLI (Recommended)
```bash
cd SimpleWebApp
dotnet run
```
**Note**: The application will start and show URLs like:
- `http://localhost:5245`
- `https://localhost:7245` (if HTTPS is configured)

### Option 2: Using the batch file
```bash
cd SimpleWebApp
run.bat
```

### Option 3: Using Visual Studio
1. Open `SimpleWebApp.csproj` in Visual Studio
2. Press F5 or click "Start Debugging"

### Option 4: Using Docker (Recommended for Production)
```bash
# Build and run with Docker Compose
cd SimpleWebApp
docker-compose up --build

# Or build and run manually
docker build -t simplewebapp .
docker run -p 8080:80 -p 8443:443 simplewebapp
```

## Docker Configuration

The application includes full Docker support with:

- **Dockerfile**: Multi-stage build for optimized production images
- **docker-compose.yml**: Easy deployment with proper networking
- **.dockerignore**: Excludes unnecessary files from build context

### Docker Ports
- **HTTP**: Port 8080 (mapped to container port 80)
- **HTTPS**: Port 8443 (mapped to container port 443)

### Docker Commands
```bash
# Build the image
docker build -t simplewebapp .

# Run the container
docker run -d -p 8080:80 --name simplewebapp-container simplewebapp

# Stop the container
docker stop simplewebapp-container

# Remove the container
docker rm simplewebapp-container

# View logs
docker logs simplewebapp-container
```

## What You'll See

- A beautiful hero section with gradient background
- Three feature cards highlighting the app's capabilities
- An interactive demo where you can enter your name and get a personalized greeting
- Responsive navigation bar
- Clean, modern design throughout

## Technologies Used

- **ASP.NET Core**: Web framework
- **Razor Pages**: Server-side pages
- **Bootstrap**: CSS framework for styling
- **JavaScript**: Client-side interactivity
- **Docker**: Containerization platform

## Default Ports

- **Local Development**: `http://localhost:5000` or `https://localhost:5001`
- **Docker**: `http://localhost:8080` or `https://localhost:8443`
