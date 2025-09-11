# JWTAuthManager

Uma API .NET robusta para autenticação e autorização de usuários utilizando JWT (JSON Web Tokens), gerenciamento de tokens de atualização (refresh tokens) e blacklist de tokens. O projeto foi desenvolvido com foco nos principais conceitos de Clean Architecture, Domain-Driven Design (DDD) e Command Query Responsibility Segregation (CQRS), garantindo uma estrutura escalável e organizada. 

---

## Funcionalidades
- Autenticação e autorização baseada em JWT
- Gerenciamento de refresh tokens
- Blacklist de tokens para logout seguro e revogação
- Hash seguro de senhas com BCrypt
- Arquitetura modular (Domain, Application, Infrastructure, API)
- Entity Framework Core com PostgreSQL
- MediatR para CQRS e manipulação de requisições
- FluentValidation para validação de dados
- Documentação Swagger/OpenAPI

---

## Estrutura do projeto

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

## Como Executar o Projeto

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/) (ou utilize o docker-compose)
- (Opcional) [Docker Compose](https://docs.docker.com/compose/)

### 1. Configuração do Banco de Dados

O projeto utiliza PostgreSQL. O arquivo `appsettings.Development.json` possui uma string de conexão padrão:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5433;Database=auth_manager_db;Username=postgres;Password=MyStrong!Passw0rd;"
}
```

Altere conforme necessário.

#### Usando Docker Compose

Se preferir, suba o banco com Docker:

```sh
docker-compose up -d
```

### 2. Aplicando as Migrations

No diretório da solução, execute:

```sh
dotnet ef database update --project JWTAuthManager.Infrastructure
```

### 3. Executando a API

No diretório da solução, rode:

```sh
dotnet run --project JWTAuthManager.Api
```

A API estará disponível em `https://localhost:7043` ou `http://localhost:5225`.

### 4. Documentação Swagger

Acesse `/swagger` na URL da API para visualizar e testar os endpoints.

---

## Estrutura do Projeto

## Tecnologias Utilizadas
- .NET Core 8
- Entity Framework Core (PostgreSQL)
- MediatR
- AutoMapper
- FluentValidation
- BCrypt.Net
- Swagger (Swashbuckle)