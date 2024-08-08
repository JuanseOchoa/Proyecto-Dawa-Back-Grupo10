using Microsoft.EntityFrameworkCore;
using ProgramaDAWA.Context;
using ProgramaDAWA.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Configurar el DbContext
builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configurar los servicios de CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin() // Permite solicitudes de cualquier origen
              .AllowAnyHeader() // Permite cualquier encabezado
              .AllowAnyMethod(); // Permite cualquier método (GET, POST, etc.)
    });
});

builder.Services.AddScoped<I_UsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<I_ProveedorRepository, ProveedorRepository>();
builder.Services.AddScoped<I_ClienteRepository, ClienteRepository>();
builder.Services.AddScoped<I_CategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<I_ProductoRepository, ProductoRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseCors();

app.Run();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
