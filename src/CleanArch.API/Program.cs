using CleanArch.API.Middleware;
using CleanArch.Application;
using CleanArch.Infrastructure;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// =========================================================
// SERVICES REGISTRATION
// =========================================================

// Controllers
builder.Services.AddControllers();

// Clean Architecture Layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title       = "CleanArch Training API",
        Version     = "v1",
        Description = "Hệ thống CRUD theo chuẩn Clean Architecture — .NET 8 + SQL Server",
        Contact = new OpenApiContact
        {
            Name  = "Training BE",
            Email = "training@cleanarch.dev"
        }
    });

    // Include XML comments cho Swagger UI
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        options.IncludeXmlComments(xmlPath);
});

// CORS (cho phép development)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// =========================================================
// MIDDLEWARE PIPELINE
// =========================================================

var app = builder.Build();

// Global exception handling — phải là middleware đầu tiên
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArch API v1");
        options.RoutePrefix = string.Empty; // Swagger tại root URL
        options.DocumentTitle = "CleanArch Training API";
        options.DisplayRequestDuration();
    });
}

app.UseHttpsRedirection();
app.UseCors();
app.MapControllers();

app.Run();
