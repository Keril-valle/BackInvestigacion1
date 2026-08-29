# AGENTS.md

## Project overview

- Single .NET 10 minimal-API web project: `Investigacion1/` (solution: `Investigacion1.slnx`). No tests, linters, or CI in the repo.
- Architecture: **vertical slice + lightweight CQRS** (no MediatR). Shared domain in `Features/Usuarios/`, EF Core `DbContext` in `Persistence/`.
- Auth exists: JWT bearer auth configured in `Program.cs`; register/login implemented. **Project goal: RBAC authorization backed by a PostgreSQL database** (not yet wired — see gotchas).

## Commands (run from `Investigacion1/`)

- Build/verify: `dotnet build`
- Run dev server: `dotnet run` → http://localhost:5208 (see `Properties/launchSettings.json`). For manual API checks, edit and use `Investigacion1.http` (port is hardcoded to `http://localhost:5208`).
- Add a package: `dotnet add package <Name>` — then verify it actually landed in the `.csproj`; it occasionally isn't persisted and must be added by hand.

## Slice conventions (follow for any new feature)

- One folder per use case under `Features/<Feature>/<UseCase>/` containing exactly: `<UseCase>Endpoint.cs`, `<UseCase>Command.cs` (writes) or `<UseCase>Query.cs` (reads), and `<UseCase>Command|QueryHandler.cs`.
- Endpoints are `public static class` exposing `static void MapXxxEndpoint(this IEndpointRouteBuilder app)`; each must be explicitly called in `Program.cs` or the route won't exist.
- Handlers are `static async Task<IResult> HandleAsync(Request req, AppDbContext db, ...deps)`. The request class binds from the JSON body; other params resolve from DI.
- Validation is manual inside handlers, returning `Results.ValidationProblem(...)` for 400s. Match existing messages ("Usuario ya existe", etc.).
- Passwords: `BCrypt.Net.BCrypt.HashPassword(pwd, 15)` / `Verify`.

## Gotchas

- **JWT secret must be ≥ 256 bits** for HS256 or token generation throws `IDX10720` at runtime (login returns 500). The committed default secret in `appsettings.json` is already long enough.
- **`Role` is a `static class`** of string constants in `Features/Usuarios/Role.cs`. Only **two roles exist and are used**: `Role.Admin = "Admin"` and `Role.Subscription_L1 = "Subscription_L1"` — do not add more. Because `Usuario` also has a `Role` property, property initializers must fully qualify it: `Usuarios.Role.Subscription_L1` (bare `Role.Subscription_L1` fails to compile with CS0236).
- **DB is EF Core InMemory** (`UseInMemoryDatabase("Investigacion1Db")`), not PostgreSQL. Adding Postgres means: add the Npgsql EF provider, swap `UseInMemoryDatabase`, add a connection string, and introduce migrations — none of this exists yet.
- `Role` is stored as a plain string on `Usuario` (nullable `Nombre`); there is no roles/permissions table yet — RBAC policy wiring in `AddAuthorization` still needs to be added.
- `authen/` (the NestJS reference the auth was ported from) is not part of the repo — ignore it if you see leftovers.

## Style

- File-scoped namespaces; comments in Spanish; no XML docs required. Keep code consistent with the compact vertical-slice style already present.