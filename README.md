# WatchesShop

A full-stack **ASP.NET Core MVC** e-commerce application for an online watch store — restored, containerized, and extended with a REST API as part of a portfolio project.

## Overview

WatchesShop is a watch catalog and ordering system originally built with ASP.NET Core MVC and Entity Framework Core. This repository documents the process of reviving an older codebase into a fully reproducible, containerized application with a REST API layer for external system integration.

**Live features:**
- Browse a filterable watch catalog (brand, gender, style, material, color, mechanism, price range)
- Shopping cart with session-based persistence
- Checkout flow that creates an order and notifies an external system
- Admin panel for adding/editing/removing watches and managing orders
- REST API for programmatic access (see below)

## Tech Stack

| Layer | Technology |
|---|---|
| Backend | ASP.NET Core 8 MVC |
| ORM / Database | Entity Framework Core 9, SQL Server |
| API Docs | Swagger / OpenAPI (Swashbuckle) |
| Auth | Cookie authentication (admin panel) |
| Containerization | Docker, Docker Compose |
| Frontend | Razor Views, vanilla CSS/JS |

## Getting Started (Docker)

The entire stack (web app + SQL Server) runs with a single command — no local .NET or SQL Server installation required:

```bash
git clone https://github.com/<your-username>/WatchesShop.git
cd WatchesShop
docker compose up --build
```

Once the containers are running:
- **Website:** http://localhost:8080
- **Swagger UI:** http://localhost:8080/swagger
- **Admin login:** `/Admin/Login` (demo credentials: `Admin` / `123admin`)

EF Core migrations are applied automatically on startup, seeding the database with brands, materials, colors, and a sample catalog of watches.

## REST API

Alongside the MVC interface, the project exposes a REST API for external clients:

| Resource | Endpoints |
|---|---|
| Watches | `GET /api/watches`, `GET /api/watches/{id}`, `GET /api/watches/brands`, `POST`, `PUT /{id}`, `DELETE /{id}` |
| Orders | `GET /api/orders`, `GET /api/orders/{id}`, `POST`, `PUT /{id}/ship` |

Creating an order via the API (or through checkout) triggers a **webhook notification** (`order.created` event) to a configurable external endpoint — a working example of system-to-system integration, with failure isolation so a downstream outage never blocks order creation.

Full API reference, request/response examples, and integration notes: see [`API_DOCUMENTATION.md`](./API_DOCUMENTATION.md).

## Project Structure

```
WatchesShop/
├── Controllers/
│   ├── Api/              # REST API controllers (Watches, Orders)
│   └── ...               # MVC controllers (Home, Watches, Cart, Admin, Orders)
├── Data/                 # EF Core DbContext
├── Migrations/           # EF Core migrations (schema + seed data)
├── Models/               # Entities and API DTOs
├── Services/             # Business logic + webhook notification service
├── Views/                # Razor views
└── wwwroot/              # Static assets, product images
```

## Notable Engineering Decisions

- **Seed data ordering**: lookup tables (brands, materials, colors, cases) are seeded in a dedicated migration *before* the watches that reference them, resolving a foreign-key dependency that wasn't caught until running against a genuinely empty database (as opposed to a pre-populated local dev DB).
- **Resilient integration**: the webhook call to the external system is wrapped so that a failure there is logged but never rolls back the underlying order transaction.
- **API/MVC parity**: both the traditional checkout form and the API `POST /api/orders` endpoint funnel through the same notification service — one integration point regardless of the channel.


