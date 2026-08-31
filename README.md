# Zenbu Sora (空) ☁️

The API.

## Architecture

- Api Layer
  - Depends On 🔽
- Infrastructure Layer
  - Depends On 🔽
- Application Layer
  - Depends On 🔽
- Domain Layer

## Getting Started

```bash
task dev
```

### Explore the API

Navigate to `https://localhost:7200/scalar/v1` for the interactive Scalar API docs.

**Default admin credentials** (seeded automatically):
- Email: `admin@app.dev`
- Password: `Admin@123`

### Run Tests

```bash
task test
```

### Adding a New Feature

Follow the Todos pattern:

1. **Domain** — Add your entity in `Domain/Models/`
2. **Application** — Create a feature folder in `Application/Features/YourFeature/` with command/query records, handlers, and validators
3. **Infrastructure** — Add EF Core configuration in `Infrastructure/Persistence/Configurations/` and repository in `Infrastructure/Persistence/Repositories/`
4. **Api** — Add endpoints in `Api/Endpoints/` and register in `Program.cs`

## Sample: Todos Feature

The template includes a complete **Todos** CRUD feature as a reference implementation:

| Endpoint | Method | Auth | Description |
|----------|--------|------|-------------|
| `/api/todos` | GET | Yes | Get all todos (paginated) |
| `/api/todos/{id}` | GET | Yes | Get a todo by ID |
| `/api/todos` | POST | Yes | Create a new todo |
| `/api/todos/{id}` | PUT | Yes | Update a todo |
| `/api/todos/{id}/complete` | PATCH | Yes | Mark as completed |
| `/api/todos/{id}` | DELETE | Yes | Delete a todo |
