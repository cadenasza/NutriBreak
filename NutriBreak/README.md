# NutriBreak API

Plataforma para promover saúde física e mental no trabalho recomendando pausas inteligentes e cardápios equilibrados.

## Objetivo
Apoiar profissionais (ODS 3 e 8) com registros de usuários, refeições e pausas, além de recomendações (v2) baseadas em humor, nível de energia e tempo de tela.

## Tecnologias
- .NET 8 / ASP.NET Core Web API
- Entity Framework Core (SQL Server / InMemory para testes)
- API Versioning (v1, v2)
- OpenTelemetry (tracing + metrics console exporter) – versões estáveis
- Health Checks (/health)
- Swagger com versionamento
- HATEOAS + Paginação
- Testes com xUnit (pasta `TestsNew` dentro do projeto)

## Estrutura de Versões
- v1: CRUD completo de `users`, `meals`, `break-records`.
- v2: Endpoint de recomendações por usuário: `/api/v2/users/{id}/recommendations`.

## Endpoints Principais (v1)
```
GET    /api/v1/users?pageNumber=1&pageSize=10
POST   /api/v1/users
GET    /api/v1/users/{id}
PUT    /api/v1/users/{id}
DELETE /api/v1/users/{id}

GET    /api/v1/meals
POST   /api/v1/meals
GET    /api/v1/meals/{id}
PUT    /api/v1/meals/{id}
DELETE /api/v1/meals/{id}

GET    /api/v1/break-records
POST   /api/v1/break-records
GET    /api/v1/break-records/{id}
PUT    /api/v1/break-records/{id}
DELETE /api/v1/break-records/{id}
```

## Endpoint Novo (v2)
```
GET /api/v2/users/{id}/recommendations
```
Retorna sugestões de próxima pausa, intervalo recomendado e tipo de refeição sugerida.

## Paginação & HATEOAS
Listagens retornam: `total`, `pageNumber`, `pageSize`, `items` e `links` (`self`, `next`, `prev`, `create`). Recursos isolados retornam `data` + `links` (`self`, `update`, `delete`).

## Status Codes
- 200 OK (consultas)
- 201 Created (criação)
- 204 NoContent (atualizações/remoções)
- 400 BadRequest (dados inválidos)
- 404 NotFound (não encontrado)
- 409 Conflict (duplicidade de e-mail)

## Health Check
```
GET /health
```
Retorna estado da conexão com o SQL Server.

## Observabilidade
OpenTelemetry configurado (AspNetCore + HttpClient) com exportador de console. Ajuste para Jaeger / OTLP em produção adicionando exportadores apropriados.

## Execução Local
```
dotnet restore
dotnet run --project NutriBreak
```
Swagger disponível (Development): `/swagger`.

## Variáveis de Ambiente (exemplo)
```
ConnectionStrings__DefaultConnection=Server=tcp:<host>,1433;Database=NutriBreak;User Id=<user>;Password=<password>;TrustServerCertificate=True;
ASPNETCORE_ENVIRONMENT=Development
OTEL_SERVICE_NAME=NutriBreak.API
OTEL_EXPORTER_OTLP_ENDPOINT=https://otel-collector.seudominio.com
```

## Migrations & Banco
Configurar `ConnectionStrings:DefaultConnection` em `appsettings.json`.
Criar/atualizar base:
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Exemplos curl
Criar usuário:
```
curl -X POST https://localhost:5001/api/v1/users \
 -H "Content-Type: application/json" \
 -d '{"name":"João","email":"joao@example.com","workMode":"remoto"}'
```
Listar usuários:
```
curl https://localhost:5001/api/v1/users?pageNumber=1&pageSize=10
```
Recomendações (v2):
```
curl https://localhost:5001/api/v2/users/<id-guid>/recommendations
```

## Testes
Arquivos de teste em `TestsNew` (xUnit + EF InMemory).
Executar todos:
```
dotnet test
```
Principais casos cobrem criação de usuários, refeições, pausas e recomendações.

## Melhorias Futuras
- Autenticação / API Key ou JWT
- Exportadores OTLP/Jaeger
- Camada de serviço / separação de responsabilidades
- Testes de integração via `WebApplicationFactory`
- Cache para recomendações

## Licença
Uso acadêmico / demonstração.
