# MAEVEN Backend

ASP.NET Core Web API backend for MAEVEN, using PostgreSQL, Entity Framework Core, JWT auth, and role-protected admin product APIs.

## Local Development

Prerequisites:

- .NET 9 SDK
- PostgreSQL
- EF Core CLI: `dotnet tool install --global dotnet-ef`

Set local secrets:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=maeven_db;Username=postgres;Password=your_password_here"
dotnet user-secrets set "JwtSettings:Secret" "replace_with_a_long_random_secret_at_least_32_chars"
```

Run migrations and start the API:

```bash
dotnet restore
dotnet ef database update
dotnet run
```

Swagger is available in Development at:

- `http://localhost:5080/swagger`
- `http://localhost:5080/openapi/v1.json`

## Deploy To Azure App Service

Push this folder as its own GitHub repository, then connect it to an Azure App Service.

Recommended Azure settings:

- Runtime stack: `.NET 9`
- Platform: `64 Bit`
- Startup command: leave empty for App Service GitHub deployment

Add these App Service application settings:

```env
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__DefaultConnection=Host=...;Port=5432;Database=...;Username=...;Password=...;SSL Mode=Require;Trust Server Certificate=true
JwtSettings__Secret=replace_with_a_long_random_secret_at_least_32_chars
JwtSettings__Issuer=MAEVEN.Backend
JwtSettings__Audience=MAEVEN.Frontend
Cors__AllowedOrigins__0=https://your-vercel-app.vercel.app
```

Apply database migrations after deployment:

```bash
dotnet ef database update
```

For production, run migrations from your local machine against the Azure PostgreSQL connection string or use a controlled CI/CD migration step.
