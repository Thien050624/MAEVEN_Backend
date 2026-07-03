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

Health check:

- `http://localhost:5080/api/health`

Swagger is available in Development at:

- `http://localhost:5080/swagger`
- `http://localhost:5080/openapi/v1.json`

## Deploy To Render

Render does not provide a native .NET runtime, so this project deploys as a Docker web service.

1. Push this folder as its own GitHub repository.
2. In Render, create a new Web Service from that repository.
3. Select Docker as the runtime.
4. Use `/api/health` as the health check path.
5. Add the environment variables below.

Required Render environment variables:

```env
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__DefaultConnection=Host=...;Port=5432;Database=...;Username=...;Password=...;SSL Mode=Require;Trust Server Certificate=true
JwtSettings__Secret=replace_with_a_long_random_secret_at_least_32_chars
JwtSettings__Issuer=MAEVEN.Backend
JwtSettings__Audience=MAEVEN.Frontend
Cors__AllowedOrigins__0=https://your-vercel-app.vercel.app
```

If you use Render Postgres and the database is in the same Render account, prefer the internal connection string for backend-to-database traffic. If you run migrations from your local machine, use the external connection string.

Apply database migrations after creating the production database:

```bash
dotnet ef database update
```

For production, run migrations from your local machine against the production PostgreSQL connection string or use a controlled CI/CD migration step.

## Connect Vercel Frontend

In Vercel, set:

```env
VITE_API_BASE_URL=https://your-render-service.onrender.com/api
```

Redeploy the Vercel project after changing this variable.
