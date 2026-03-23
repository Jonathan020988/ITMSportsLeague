using Microsoft.EntityFrameworkCore;
using SportsLeague.Domain.Entities;

namespace SportsLeague.DataAccess.Context;

public class LeagueDbContext : DbContext// hereda de dbcontext que es una clase de EF
{
    public LeagueDbContext(DbContextOptions<LeagueDbContext> options)
        : base(options)
    {
    }

    public DbSet<Team> Teams => Set<Team>();//entidad(Team), tabla(Teams) , entidad (Team); por cada tabla nueva o cada entridad es un nuevo DbSet
    public DbSet<Player> Players => Set<Player>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)// validaciones, que tipos de datos,longitud, si es o no obligatorio
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Name)
                  .IsRequired()
                  .HasMaxLength(100);
            entity.Property(t => t.City)
                  .IsRequired()// es obligatorio
                  .HasMaxLength(100);
            entity.Property(t => t.Stadium)
                  .HasMaxLength(150);
            entity.Property(t => t.LogoUrl)
                  .HasMaxLength(500);
            entity.Property(t => t.CreatedAt)
                  .IsRequired();
            entity.Property(t => t.UpdatedAt)
                  .IsRequired(false);// es obligatorio pero en el auditbase tiene ? lo que la vuelve nuleable por eso aca tiene el false 
            entity.HasIndex(t => t.Name)
                  .IsUnique();//columna es unica, no pueden haber nombres repetidos de equipo
        });

        // ── Player Configuration ──
        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.FirstName)
                  .IsRequired()
                  .HasMaxLength(80);
            entity.Property(p => p.LastName)
                  .IsRequired()
                  .HasMaxLength(80);
            entity.Property(p => p.BirthDate)
                  .IsRequired();
            entity.Property(p => p.Number)
                  .IsRequired();
            entity.Property(p => p.Position)
                  .IsRequired();
            entity.Property(p => p.CreatedAt)
                  .IsRequired();
            entity.Property(p => p.UpdatedAt)
                  .IsRequired(false);

            // Relación 1:N con Team// RELACION CON EF
            entity.HasOne(p => p.Team)
                  .WithMany(t => t.Players)
                  .HasForeignKey(p => p.TeamId)
                  .OnDelete(DeleteBehavior.Cascade);//borrado en cascada. ejemplo bayer lo borro, el borrado en cascada elimina todos lo jugadores que estan ligados a ese club

            // Índice único compuesto: número de camiseta único por equipo
            entity.HasIndex(p => new { p.TeamId, p.Number })
                  .IsUnique();
        });

    }
}
