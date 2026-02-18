# Shorty

A small URL shortener API with redirect + click logging, built with ASP.NET Core and EF Core.

## Features (current)
- Create short links via API
- Redirect endpoint: `GET /{slug}` → 302 to target URL
- Click logging (timestamp + user-agent + referer)
- Health check: `GET /health`
- OpenAPI spec in Development
- SQLite database + EF Core migrations

## Tech stack
- .NET 10 (ASP.NET Core, Controllers)
- EF Core + SQLite
- OpenAPI

## Endpoints
Public:
- `GET /{slug}` → redirect + log click

API:
- `POST /api/links` → create link (custom slug or auto slug)
- `GET /api/links/{id}` → get link by id (plus metadata)

Ops:
- `GET /health`

## Running locally

```bash
dotnet restore
dotnet run --project src/Shorty.Api

