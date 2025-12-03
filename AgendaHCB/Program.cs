using AgendaHCB.Services;
using BusinessLogic.Implementation;
using BusinessLogic.Interfaces;
using CommonMethods;
using DataAccess.Implementation;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using NLog;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// -------------------- CONFIGURACIÓN NLOG --------------------
builder.Logging.ClearProviders();
builder.Host.UseNLog();

// -------------------- SERVICIOS --------------------

// Controllers
builder.Services.AddControllers();

// -------------------- CONFIGURACIÓN CORS --------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:8080") // <- frontend
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // si usas cookies/autenticación
    });
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Servicios propios
builder.Services.AddSingleton<OracleService>();
builder.Services.AddSingleton<AsyncExceptions>();
builder.Services.AddScoped<Exceptions>();

// DI para capa de datos y lógica
builder.Services.AddScoped<IPacienteBL, PacienteBL>();
builder.Services.AddScoped<IPacienteDA, PacienteDA>();
builder.Services.AddScoped<ICitaDA, CitaDA>();
builder.Services.AddScoped<ICitaBL, CitaBL>();

/*
builder.Services.AddScoped<IAgendaAD, AgendaAD>();
builder.Services.AddScoped<IAgendaLN, AgendaLN>();
builder.Services.AddScoped<IEspecialidadesDA, EspecialidadesDA>();
builder.Services.AddScoped<IEspecialidadesBL, EspecialidadesBL>();
builder.Services.AddScoped<ICitaCanceladaAD, CitaCanceladaAD>();
builder.Services.AddScoped<ICitaCanceladaLN, CitaCanceladaLN();*/

var app = builder.Build();

// -------------------- MIDDLEWARE --------------------

// Manejo global de errores (antes de MapControllers)
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            var logger = LogManager.GetCurrentClassLogger();
            logger.Error(contextFeature.Error, "Error no manejado en la aplicación");

            await context.Response.WriteAsJsonAsync(new
            {
                error = "Error interno del servidor",
                detail = contextFeature.Error.Message
            });
        }
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// -------------------- USO DE CORS --------------------
app.UseCors("AllowLocalhost");

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
