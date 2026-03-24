using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.DataAccess.Repositories;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;
using SportsLeague.Domain.Services;

var builder = WebApplication.CreateBuilder(args);// contenedores de dependencias, y se van agregando a medida que se creen nuevas, en este momentos olo esta team

// ── Entity Framework Core ──
builder.Services.AddDbContext<LeagueDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// ── Repositories ──
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();   // NUEVO player

// ── Services ──
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();         // NUEVO player

// ── AutoMapper ──
builder.Services.AddAutoMapper(typeof(Program).Assembly);// solo una vez

// ── Controllers ──
builder.Services.AddControllers();

// ── Swagger ── para pruebas
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ── Middleware Pipeline ──
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Results.Redirect("/swagger"));// es para que me pueda redirigir a swagger

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
