# JWT Authentication Manager

An open-source authentication solution for .NET applications that handles the complexity of JWT tokens, user management, and security best practices. Drop it into your project and get enterprise-grade authentication without the headache.

## Why This Exists

Most authentication systems are either too simple for real-world use or overly complex to understand and maintain. This project strikes the right balance - powerful enough for production but simple enough to extend.

## What Makes It Different

**🏗️ Built with Clean Architecture & DDD**  
Organized in clear layers that make the code easy to understand, test, and modify. Domain logic stays pure while infrastructure concerns are isolated.

**⚡ CQRS Pattern for Scalability**  
Commands and queries are separated, making the system more maintainable and allowing for different optimization strategies for reads vs writes.

**🔐 Security That Actually Works**  
JWT tokens with refresh capabilities, secure logout via token blacklisting, and proper password handling. No shortcuts, no "we'll fix it later" - just solid security from day one.

**📬 Smart Email Handling**  
Email sending never blocks user operations. Registration works even if email servers are down, with comprehensive logging for troubleshooting.

**🔍 Production-Ready Logging**  
Every important action is logged with correlation IDs, making debugging and auditing straightforward in production environments.

## Architecture & Design Patterns

This project showcases how to properly implement **Clean Architecture** in .NET applications:

**🏛️ Clean Architecture Layers**
- **Domain**: Pure business logic and entities with no external dependencies
- **Application**: Use cases and business workflows using CQRS pattern  
- **Infrastructure**: Database, email services, and external integrations
- **API**: Controllers, middleware, and presentation concerns

**📋 CQRS (Command Query Responsibility Segregation)**
- Commands handle write operations (Create, Update, Delete)
- Queries handle read operations (Get, List, Search)
- Each has dedicated handlers and can be optimized independently
- MediatR orchestrates the request/response flow cleanly

**🎯 Domain-Driven Design (DDD)**
- Rich domain entities with business logic encapsulation
- Repository patterns for data access abstraction
- Value objects and aggregates for complex business rules
- Clear separation between domain and infrastructure concerns

**Why This Matters**: These patterns make the codebase maintainable as it grows, easier to test, and simpler to understand for new team members.

## What's Inside

**Authentication Flow**
- User registration with email verification
- Secure login with JWT + refresh tokens  
- Password reset via email
- Token blacklisting for security

**User Management**
- Profile management endpoints
- Account deletion with cleanup
- Secure data handling

**Developer Experience**
- Swagger documentation
- MailHog for email testing
- Structured logging with correlation IDs
- Docker setup included

## Learn the Patterns

This project demonstrates several important architectural patterns:

- **Clean Architecture**: Clear separation of concerns across layers
- **Domain-Driven Design**: Rich domain models with business logic
- **CQRS with MediatR**: Separate read/write operations
- **Repository Pattern**: Abstracted data access
- **Middleware Pipeline**: Cross-cutting concerns handling

**Benefits:**
- **Testability**: Each layer can be independently tested
- **Maintainability**: Clear separation of concerns and dependencies
- **Scalability**: Easy to extend with new features and integrations
- **Flexibility**: Infrastructure can be swapped without affecting business logic

## Project Structure

```
JWTAuthManager/
├── src/
    ├── JWTAuthManager.API/                # Camada de apresentação
    │   ├── Controllers/                   # Controllers REST
    │   ├── Middleware/                    # Middlewares globais
    │   ├── appsettings*.json              # Configurações de ambiente
    │   ├── Program.cs                     # Configurações da aplicação
    │
    ├── JWTAuthManager.Application/        # Camada de aplicação
    │   ├── Common/                        # Utilitários, interfaces, modelos, validações
    │   ├── Modules/                       # Módulos de negócio
    │   │   ├── Commands/                  # Comandos (Write)
    │   │   ├── Queries/                   # Consultas (Read)
    │   │   ├── Handlers/                  # Manipuladores de comandos/consultas
    │   │   └── DTOs/                      # Data Transfer Objects
    │   └── DependencyInjection.cs         # Injeção de dependências da aplicação
    │
    ├── JWTAuthManager.Domain/             # Camada de domínio
    │   ├── Entities/                      # Entidades de domínio
    │   │   ├── Common/                    # BaseEntity e entidades compartilhadas
    │   │   └── Token/                     # Entidades relacionadas a tokens
    │   ├── Interfaces/                    # Interfaces de repositórios e serviços
    │
    ├── JWTAuthManager.Infrastructure/     # Camada de infraestrutura
    │   ├── Data/                          # DbContext, configurações de entidades
    │   ├── Migrations/                    # Migrations do banco de dados
    │   ├── Repositories/                  # Implementações dos repositórios
    │   ├── Security/                      # Implementações de segurança (ex: BCryptPasswordHasher)
    │   ├── Services/                      # Serviços de infraestrutura
    │   └── DependencyInjection.cs         # Injeção de dependências da infraestrutura
├── JWTAuthManager.sln # Solução do Visual Studio
├── docker-compose.yml # (Opcional) PostgreSQL em Container
```
---

## Quick Start

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker & Docker Compose](https://docs.docker.com/compose/) (recommended)
- [PostgreSQL 13+](https://postgresql.org) (if not using Docker)

### 1. Clone and Setup

```bash
git clone https://github.com/gabrielcarvallho/JWTAuthManager.git
cd JWTAuthManager

# Start PostgreSQL and MailHog with Docker
docker-compose up -d

# Apply database migrations
dotnet ef database update --project src/JWTAuthManager.Infrastructure
```

### 2. Run the Application

```bash
# Start the API
dotnet run --project src/JWTAuthManager.API

# API will be available at:
# - HTTP: http://localhost:5274
# - HTTPS: https://localhost:7258
# - Swagger: https://localhost:7258/swagger
```

### 3. Test Email Functionality

- **MailHog UI**: http://localhost:8025 (captures all emails in development)
- **Test Registration**: POST to `/api/User` to trigger welcome email
- **Test Password Reset**: POST to `/api/Authentication/forgot-password`

### Configuration (Optional)

The project works out-of-the-box with Docker, but you can customize:

- **Database**: Update connection string in `appsettings.Development.json`
- **Email**: Configure SMTP settings for real email sending (development uses MailHog)
- **JWT Settings**: Modify token expiration and secrets as needed

## Contributing

Want to help make this better? Here are some ways to contribute:

- **Add Features**: Email templates, role-based auth, API versioning
- **Improve Architecture**: Add integration tests, implement event sourcing
- **Enhance Security**: Rate limiting, account lockouts, audit logging
- **Developer Experience**: Better error handling, documentation, examples

Fork it, make it better, send a PR! Every contribution makes this project stronger. 💪 

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.