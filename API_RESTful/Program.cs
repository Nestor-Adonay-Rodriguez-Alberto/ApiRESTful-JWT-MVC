using API_RESTful.Modelos;
using API_RESTful.Servicios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// AGREGA TODO LOS CONTROLADORES:
builder.Services.AddControllers();

// DOCUMENTACION DE Swagger PARA TESTEAR:
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// INYECCION DE LA DB:
builder.Services.AddDbContext<MyDBcontext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Cadena_Conexion")));

// INYECCION DE LOS SERVICIOS PARA INTERACTUAR CON LA DB:
builder.Services.AddScoped<Servicios_Rol>();
builder.Services.AddScoped<Servicios_Empleado>();
builder.Services.AddScoped<Servicios_Producto>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
