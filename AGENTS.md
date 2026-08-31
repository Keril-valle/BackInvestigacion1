# AGENTS.md

## Project overview

- Single .NET 10 minimal-API web project: `Investigacion1/` (solution: `Investigacion1.slnx`). No tests, linters, or CI in the repo.
- Architecture: **vertical slice + lightweight CQRS** (no MediatR). Shared domain under `Features/`, EF Core `DbContext` in `Persistence/`, per-entity Fluent API config in `Persistence/Configurations/`.
- Auth (JWT bearer + refresh) and **RBAC are implemented**: `Program.cs` adds an `"Admin"` policy via `RequireRole(Role.Admin)`; authenticated routes use `.RequireAuthorization()`; the Admin-only routes return 401 (no/unverified token) and 403 (non-Admin). Goal: JWT auth + RBAC backed by **PostgreSQL**.
- DB is **PostgreSQL via Supabase** (`UseNpgsql`, `Npgsql.EntityFrameworkCore.PostgreSQL` 10.0.3). Migrations live in `Migrations/`. **`Program.cs` runs `db.Database.Migrate()` on every startup** — the DB is auto-migrated when the app runs; you still must create migrations yourself after model changes.

## Commands (run from `Investigacion1/`)

- Build/verify: `dotnet build`
- Run dev server: `dotnet run` → http://localhost:5208 (see `Properties/launchSettings.json`). For manual API checks, edit and use `Investigacion1.http` (port hardcoded to `http://localhost:5208`).
- **Change schema → two steps:** `dotnet ef migrations add <Name> --output-dir Migrations` then `dotnet ef database update`. Build must pass first; a scaffold may warn about data loss (e.g. when dropping columns) — review the generated file.
- Add a package: `dotnet add package <Name>` — verify it actually landed in the `.csproj`; it occasionally isn't persisted and must be added by hand.

## Slice conventions (follow for any new feature)

- One folder per use case under `Features/<Feature>/<UseCase>/` containing exactly: `<UseCase>Endpoint.cs`, `<UseCase>Command.cs` (writes) or `<UseCase>Query.cs` (reads), and `<UseCase>Command|QueryHandler.cs`.
- Endpoints are `public static class` exposing `static void MapXxxEndpoint(this IEndpointRouteBuilder app)`; each **must be explicitly called in `Program.cs`** or the route won't exist.
- Handlers are `static async Task<IResult> HandleAsync(Request req, AppDbContext db, ...deps)`. The request class binds from the JSON body; other params resolve from DI.
- Validation is manual inside handlers, returning `Results.ValidationProblem(...)` for 400s. Match existing messages ("Usuario ya existe", etc.).
- Passwords: `BCrypt.Net.BCrypt.HashPassword(pwd, 15)` / `Verify`.

## Data model & the Usuario↔perfil relationship (high-signal)

- `Usuario` (`int Id`) is the auth table. **`Usuario` has NO `Nombre`** — display names live in the 1:1 clinical profile. `Usuario` has optional navs `Dermatologo?` / `Paciente?`.
- Clinical tables (`Pacientes`, `Dermatologos`, `Servicios`, `Citas`, `Tratamientos`, `CitaTratamientos`) use **`Guid Id`**, but their FK `UsuarioId` is **`int`** (matches `Usuario.Id`), 1:1 unique, `OnDelete(Cascade)`.
- **`Paciente` has no `Email`** (email is on `Usuario`); `Paciente.Nombre` holds the L1 user's name, `Dermatologo.Nombre` the Admin's name. `FechaNacimiento` on `Paciente` is nullable `DateOnly?`.
- When projecting a user's name in queries, follow the nav by role: `Role.Admin ? u.Dermatologo!.Nombre : u.Paciente!.Nombre` (see `GetUsersQueryHandler`, `/debug/users`). **Users created before the `RelacionUsuarioClinica` migration have no profile → `nombre` is null**; only newly registered users get a `Paciente`/`Dermatologo`.

## Gotchas

- **Real Supabase credentials are committed** in `appsettings.Development.json` (`ConnectionStrings:DefaultConnection`) and are **not gitignored** — do not leak/copy them into code or commits; prefer moving them to User Secrets.
- **JWT secret must be ≥ 256 bits** for HS256 or token generation throws `IDX10720` at runtime (login returns 500). The committed default secret in `appsettings.json` is long enough.
- **`Role` is a `static class`** of string constants in `Features/Usuarios/Role.cs`. Only **two roles exist and are used**: `Role.Admin = "Admin"` and `Role.Subscription_L1 = "Subscription_L1"` — do not add more. Because `Usuario` also has a `Role` property, property initializers must fully qualify it: `Usuarios.Role.Subscription_L1` (bare `Role.Subscription_L1` fails to compile with CS0236).
- **`Dermatologo.NumeroLicencia` is required and unique**; `Paciente`/`Dermatologo` reference their `Usuario` by `UsuarioId`. `/admin/register` now requires a `numeroLicencia` field and creates the `Dermatologo` for the Admin. `/auth/register` creates the `Paciente` for the L1 user.
- `authen/` (NestJS reference the auth was ported from) is not part of the repo — ignore leftovers.

## Style

- File-scoped namespaces; comments in Spanish; no XML docs required. Keep code consistent with the compact vertical-slice style already present.
