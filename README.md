# 🌱 Beat Core Service

> The heart of sustainable living - A robust microservice powering eco-conscious communities through profile management, store operations, and sustainability tracking.

Part of the **Beat EcoProve** ecosystem - where sustainability meets technology.

## 📋 Project Overview

The Beat Core Service is the central microservice of the Beat EcoProve platform, managing user profiles, store operations, clothing items, and sustainability metrics. It serves as the backbone for tracking eco-friendly behaviors, managing circular economy transactions, and fostering sustainable communities.

Built with Clean Architecture principles and Domain-Driven Design (DDD), this service integrates seamlessly with event-driven systems using Kafka for asynchronous operations, Redis for distributed caching and real-time features, and PostgreSQL with PostGIS for persistent storage with geospatial capabilities.

### ✨ Key Features

- 👤 **User Profile Management** - Complete profile system with sustainability tracking
- 🏪 **Store Operations** - Manage stores with geospatial search capabilities
- 👕 **Clothing Item Management** - Track clothing items in the circular economy
- 📊 **Sustainability Metrics** - XP and level system to gamify eco-friendly behavior
- 📡 **Event-Driven Architecture** - Kafka integration for microservice communication
- 🗺️ **Geospatial Features** - PostGIS support for location-based services
- 📚 **Interactive Swagger API** - Comprehensive API documentation
- ⚡ **High-Performance Caching** - Redis for optimized data access
- 🔐 **JWT Authentication** - Secure token-based authentication

### 🛠️ Technologies

- **.NET Version**: 9.0
- **Framework**: ASP.NET Core with Carter for minimal APIs
- **Database**: PostgreSQL 17+ with PostGIS for geospatial data
- **Cache**: Redis for session and data caching
- **Message Broker**: Kafka for event streaming
- **Security**: JWT Bearer authentication with Identity Server integration
- **API Documentation**: Swagger/OpenAPI with interactive UI
- **Migrations**: Entity Framework Core migrations
- **Object Mapping**: Mapster for high-performance mapping
- **Telemetry**: OpenTelemetry with Prometheus exporter
- **Password Hashing**: BCrypt.Net for secure password storage

## 🚀 Setup for Development

### 📦 Prerequisites

Before you can start developing with this project, ensure you have the following tools installed:

#### Option 1: Using Nix (Recommended)

- **[Nix](https://nixos.org/download.html)**: Package manager for reproducible builds
- **[direnv](https://direnv.net/)** (optional): Automatic environment loading

With Nix and direnv installed, simply navigate to the project directory and the development environment will be set up automatically!

```bash
cd /path/to/beat/core
# If using direnv, run: direnv allow
# Otherwise, run: nix develop
```

#### Option 2: Manual Setup

- **[.NET SDK](https://dotnet.microsoft.com/download)**: Version 9.0 or higher
- **[Docker](https://www.docker.com/get-started)**: For infrastructure services (PostgreSQL, Redis, Kafka)
- **[Just](https://github.com/casey/just)** (optional): Command runner for common tasks

Verify your installation:

```bash
dotnet --version  # Should output 9.0.x or higher
docker --version
```

### 🔧 Environment Configuration

1. **Clone the repository**

```bash
git clone https://github.com/BeatEcoprove/beat-ecoprove-core.git  core
cd core
```

2. **Set up environment variables**

Copy the example environment file and configure it:

```bash
cp .env.example .env
```

Edit `.env` with your configuration:

```env
# API Configuration
BEAT_API_REST_PORT=5182
ASPNETCORE_HTTP_PORTS=5182
ASPNETCORE_URLS=http://*:5182

# Identity Server
JWKS_URL=http://localhost:5001

# Redis Configuration
REDIS_HOST=localhost
REDIS_PORT=6379
REDIS_PORT_INTERFACE=8001

# PostgreSQL Configuration
POSTGRES_DB=ecoprove
POSTGRES_USER=beat
POSTGRES_PASSWORD=verysecurepassword
POSTGRES_PORT=5432
POSTGRES_HOST=localhost

# Kafka Configuration
KAFKA_HOST=localhost
KAFKA_PORT=9092
```

### 🏗️ Build Options

#### Option 1: Using Nix (Recommended)

Build the entire project using Nix flakes:

```bash
# Build the application
nix build

# Build Docker image
nix build .#docker

# Load Docker image
docker load < result
```

#### Option 2: Using Docker

Build and run with Docker:

```bash
# Build the Docker image
docker build -t beat-core:latest .

# Run the container
docker run -p 5182:5182 --env-file .env beat-core:latest
```

#### Option 3: Using .NET CLI

For local development with .NET:

```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build BeatEcoprove.sln

# Build in Release mode
dotnet build BeatEcoprove.sln -c Release

# Run the API
dotnet run --project src/BeatEcoprove.Api
```

#### Option 4: Using Just (Command Runner)

If you have [just](https://github.com/casey/just) installed, you can use these convenient commands:

```bash
# List all available commands
just

# Run the API locally
just serve

# Build the solution
just build

# Build in Release mode
just build-release

# Clean build artifacts
just clean

# Format code
just format
```

### 🗄️ Database Setup

This project uses Entity Framework Core for database migrations.

#### Using Just

```bash
# Apply migrations to database
just migration-push

# Create a new migration
just migration-create AddNewFeature

# List all migrations
just migration-list

# Rollback all migrations
just migration-reset

# Remove last migration
just migration-remove
```

#### Using .NET CLI

```bash
# Apply migrations
dotnet ef database update --startup-project src/BeatEcoprove.Api --project src/BeatEcoprove.Infrastructure

# Create a new migration
dotnet ef migrations add MigrationName --startup-project src/BeatEcoprove.Api --project src/BeatEcoprove.Infrastructure

# List migrations
dotnet ef migrations list --startup-project src/BeatEcoprove.Api --project src/BeatEcoprove.Infrastructure
```

### 🐳 Infrastructure with Docker Compose

If you have a Docker Compose setup for infrastructure services (PostgreSQL, Redis, Kafka), start them with:

```bash
docker-compose up -d
```

## 🎯 Running the Application

Once your environment is configured and the database is set up:

1. **Start infrastructure services** (if using Docker Compose)
2. **Run the application** using one of the methods above
3. **Access Swagger UI** at `http://localhost:5182/swagger`

The API will be available at `http://localhost:5182`

## 📚 API Documentation

Interactive API documentation is available via Swagger UI:

- **Swagger UI**: `http://localhost:5182/swagger`
- **OpenAPI Specification**: `http://localhost:5182/swagger/v1/swagger.json`

The API supports:
- JWT Bearer authentication
- API versioning
- Comprehensive endpoint documentation
- Try-it-out functionality for testing endpoints

## 🏛️ Architecture

This service follows **Clean Architecture** principles with clear separation of concerns:

```
src/
├── BeatEcoprove.Api/              # Presentation layer (Controllers, Middleware, API configuration)
├── BeatEcoprove.Application/      # Application layer (Use cases, business logic orchestration)
├── BeatEcoprove.Domain/           # Domain layer (Entities, domain logic, interfaces)
├── BeatEcoprove.Infrastructure/   # Infrastructure layer (Database, external services, Kafka)
└── BeatEcoprove.Contracts/        # Data contracts (DTOs, request/response models)
```

### Key Design Patterns

- **Domain-Driven Design (DDD)**: Rich domain models with business logic
- **CQRS**: Separation of read and write operations
- **Repository Pattern**: Data access abstraction
- **Dependency Injection**: Loose coupling and testability
- **Event-Driven Architecture**: Async communication via Kafka

## 🔑 Key Dependencies

- **ASP.NET Core 9.0**: Web framework
- **Entity Framework Core 9.0**: ORM with PostgreSQL provider
- **Npgsql.EntityFrameworkCore.PostgreSQL**: PostgreSQL database provider
- **NetTopologySuite**: Geospatial data support
- **Carter**: Minimal API endpoints
- **MassTransit**: Message bus abstraction for Kafka
- **NRedisStack**: Redis client with modern features
- **BCrypt.Net**: Password hashing
- **Mapster**: High-performance object mapping
- **OpenTelemetry**: Observability and metrics
- **Swashbuckle**: Swagger/OpenAPI documentation

## 🧪 Testing

```bash
# Run all tests
dotnet test

# Run tests with coverage
dotnet test /p:CollectCoverage=true
```

## 📝 Development Workflow

1. **Create a feature branch** from `main`
2. **Make your changes** following the project's coding standards
3. **Run migrations** if you modified database models
4. **Format your code** with `just format` or `dotnet format`
5. **Test your changes** thoroughly
6. **Commit and push** your changes
7. **Create a pull request** to `main`

## 🔍 Code Quality

This project maintains high code quality standards:

- **Nullable reference types** enabled
- **Implicit usings** for cleaner code
- **.editorconfig** for consistent formatting
- **Code analysis** with Roslyn analyzers

Format your code before committing:

```bash
dotnet format
# or
just format
```

## 🌍 Environment Variables Reference

| Variable | Description | Default | Required |
|----------|-------------|---------|----------|
| `BEAT_API_REST_PORT` | API HTTP port | 5182 | Yes |
| `ASPNETCORE_HTTP_PORTS` | ASP.NET Core HTTP ports | 5182 | Yes |
| `JWKS_URL` | Identity server URL | - | Yes |
| `POSTGRES_HOST` | PostgreSQL host | localhost | Yes |
| `POSTGRES_PORT` | PostgreSQL port | 5432 | Yes |
| `POSTGRES_DB` | Database name | ecoprove | Yes |
| `POSTGRES_USER` | Database user | - | Yes |
| `POSTGRES_PASSWORD` | Database password | - | Yes |
| `REDIS_HOST` | Redis host | localhost | Yes |
| `REDIS_PORT` | Redis port | 6379 | Yes |
| `KAFKA_HOST` | Kafka broker host | localhost | Yes |
| `KAFKA_PORT` | Kafka broker port | 9092 | Yes |

## 🤝 Contributing

We welcome contributions! Please follow these guidelines:

1. Fork the repository
2. Create a feature branch
3. Make your changes with clear commit messages
4. Ensure all tests pass
5. Submit a pull request

## 📄 License

This project is part of the Beat EcoProve ecosystem.

## 🙏 Acknowledgments

Built with passion for sustainability and powered by the Beat EcoProve team.

---

**Ready to make the world more sustainable?** 🌱 Start developing and join us in building a circular economy for clothing!
