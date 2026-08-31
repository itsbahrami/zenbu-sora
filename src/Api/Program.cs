using App.Api.Endpoints;
using App.Api.Endpoints.Jot;
using App.Api.Extensions;
using App.Application;
using App.Infrastructure;
using App.Infrastructure.Persistence;
using App.ServiceDefaults;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try {
    var builder = WebApplication.CreateBuilder(args);

    // Aspire service defaults (OpenTelemetry, health checks, service discovery)
    builder.AddServiceDefaults();

    // Serilog
    builder.Host.UseSerilog(
        (ctx, loggerConfig) => loggerConfig.ReadFrom.Configuration(ctx.Configuration)
    );

    builder.Services.AddLocalization(
        // options => options.ResourcesPath = "Resources"
    );

    builder.Services.AddCors(options => {
        options.AddPolicy("Frontend", policy => {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });

    builder.Services.Configure<RequestLocalizationOptions>(options => {
        string[] cultures = ["en", "fa", "ja"];

        options
            .SetDefaultCulture(cultures[0])
            .AddSupportedCultures(cultures)
            .AddSupportedUICultures(cultures);
    });

    // Aspire-managed SQLite
    builder.AddSqliteConnection("sqlite");

    // Application & Infrastructure
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    // Global exception handling
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

    // OpenAPI with JWT Bearer security scheme
    builder.Services.AddOpenApi(options => {
        options.AddDocumentTransformer((document, _, _) => {
            var info = document.Info ?? new OpenApiInfo();
            info.Title = "Zenbu API";
            info.Description = "The backend of the 'Zenbu' app.";
            info.Contact = new OpenApiContact {
                Name = "Mohammad Mahdi Bahrami",
                Url = new Uri("https://github.com/gikdev")
            };
            document.Info = info;

            var components = document.Components ?? new OpenApiComponents();
            components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
            components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "Enter your JWT token"
            };

            document.Components = components;

            var schemeReference = new OpenApiSecuritySchemeReference("Bearer");
            var securityRequirement = new OpenApiSecurityRequirement {
                [schemeReference] = []
            };

            document.Security ??= [];
            document.Security.Add(securityRequirement);
            return Task.CompletedTask;
        });
    });

    // ProblemDetails
    builder.Services.AddProblemDetails();

    var app = builder.Build();

    // Global exception handler
    app.UseExceptionHandler();
    app.UseStatusCodePages();

    app.UseRequestLocalization();

    app.UseCors("Frontend");

    // --------------------------------------------------------------
    // Serve static files from wwwroot (React Vite SPA build output)
    // --------------------------------------------------------------
    app.UseDefaultFiles();            // serves index.html for root path
    app.UseStaticFiles();             // serves all other static assets

    // Enable endpoint routing (explicit, though often implicit)
    app.UseRouting();

    // OpenAPI & Scalar (can be placed before or after auth)
    app.MapOpenApi();
    app.MapOpenApi("openapi.yml");
    app.MapScalarApiReference(options => {
        options.WithTitle("App API");
        options.WithDefaultHttpClient(ScalarTarget.JavaScript, ScalarClient.Fetch);
    });

    // Authentication & Authorization
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseSerilogRequestLogging();

    // Map API endpoints
    app.MapIdentityEndpoints();
    app.MapTodoEndpoints();
    app.MapWelcomeEndpoints();
    app.MapJottingEndpoints();

    // Aspire default endpoints (health, alive)
    app.MapDefaultEndpoints();

    // --------------------------------------------------------------
    // SPA fallback – any request not matching API or static file
    // returns index.html to support client-side routing
    // --------------------------------------------------------------
    app.MapFallbackToFile("index.html");

    // Seed database in development
    // if (app.Environment.IsDevelopment()) {
    await AppDbSeeder.SeedAsync(app.Services);
    // }

    await app.RunAsync();
} catch (Exception ex) when (ex is not HostAbortedException) {
    Log.Fatal(ex, "Application terminated unexpectedly");
} finally {
    await Log.CloseAndFlushAsync();
}
