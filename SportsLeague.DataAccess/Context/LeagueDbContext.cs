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
    public DbSet<Referee> Referees => Set<Referee>();              // NUEVO
    public DbSet<Tournament> Tournaments => Set<Tournament>();    // NUEVO
    public DbSet<TournamentTeam> TournamentTeams => Set<TournamentTeam>(); // NUEVO

    public DbSet<Sponsor> Sponsors => Set<Sponsor>();// Nuevo para entrega evento evaluativo
    public DbSet<SponsorTournament> SponsorTournaments => Set<SponsorTournament>();// Nuevo para entrega evento evaluativo


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

        // ── Referee Configuration ──
        modelBuilder.Entity<Referee>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.FirstName)
                  .IsRequired()
                  .HasMaxLength(80);
            entity.Property(r => r.LastName)
                  .IsRequired()
                  .HasMaxLength(80);
            entity.Property(r => r.Nationality)
                  .IsRequired()
                  .HasMaxLength(80);
            entity.Property(r => r.CreatedAt)
                  .IsRequired();
            entity.Property(r => r.UpdatedAt)
                  .IsRequired(false);
        });

        // ── Tournament Configuration ──
        modelBuilder.Entity<Tournament>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Name)
                  .IsRequired()
                  .HasMaxLength(150);
            entity.Property(t => t.Season)
                  .IsRequired()
                  .HasMaxLength(20);
            entity.Property(t => t.StartDate)
                  .IsRequired();
            entity.Property(t => t.EndDate)
                  .IsRequired();
            entity.Property(t => t.Status)
                  .IsRequired();
            entity.Property(t => t.CreatedAt)
                  .IsRequired();
            entity.Property(t => t.UpdatedAt)
                  .IsRequired(false);
        });

        // ── TournamentTeam Configuration ──
        modelBuilder.Entity<TournamentTeam>(entity =>
        {
            entity.HasKey(tt => tt.Id);
            entity.Property(tt => tt.RegisteredAt)
                  .IsRequired();
            entity.Property(tt => tt.CreatedAt)
                  .IsRequired();
            entity.Property(tt => tt.UpdatedAt)
                  .IsRequired(false);

            // Relación con Tournament
            entity.HasOne(tt => tt.Tournament)
                  .WithMany(t => t.TournamentTeams)
                  .HasForeignKey(tt => tt.TournamentId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Relación con Team
            entity.HasOne(tt => tt.Team)
                  .WithMany(t => t.TournamentTeams)
                  .HasForeignKey(tt => tt.TeamId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Índice único compuesto: un equipo solo una vez por torneo
            entity.HasIndex(tt => new { tt.TournamentId, tt.TeamId })
                  .IsUnique();
        });

        // Sponsor configuration
        modelBuilder.Entity<Sponsor>(entity =>
        {
            entity.HasKey(s => s.Id);

            entity.Property(s => s.Name)
                  .IsRequired()
                  .HasMaxLength(150);

            entity.Property(s => s.ContactEmail)
                  .IsRequired()
                  .HasMaxLength(150);

            entity.Property(s => s.Phone)
                  .HasMaxLength(50);

            entity.Property(s => s.WebsiteUrl)
                  .HasMaxLength(200);

            entity.Property(s => s.Category)
                  .IsRequired();

            entity.Property(s => s.CreatedAt)
                  .IsRequired();

            entity.Property(s => s.UpdatedAt)
                  .IsRequired(false);

            // índice único; NO PUEDEN EXISTIR 2 SPONSORS CON EL MISMO NOMBRE
            entity.HasIndex(s => s.Name)
                  .IsUnique();
        });

        //SponsorTournament Configuration
        modelBuilder.Entity<SponsorTournament>(entity =>
        {
            entity.HasKey(st => st.Id);//PK

            entity.Property(st => st.ContractAmount)
                  .IsRequired();

            entity.Property(st => st.JoinedAt)
                  .IsRequired();

            entity.Property(st => st.CreatedAt)
                  .IsRequired();

            entity.Property(st => st.UpdatedAt)
                  .IsRequired(false);

            // RELACIÓN CON SPONSOR
            entity.HasOne(st => st.Sponsor)
                  .WithMany(s => s.SponsorTournaments)
                  .HasForeignKey(st => st.SponsorId)
                  .OnDelete(DeleteBehavior.Cascade);

            // RELACIÓN CON TOURNAMENT
            entity.HasOne(st => st.Tournament)
                  .WithMany(t => t.SponsorTournaments)
                  .HasForeignKey(st => st.TournamentId)
                  .OnDelete(DeleteBehavior.Cascade);

            // ÍNDICE ÚNICO COMPUESTO; UN SPONSOR NO PUEDE REPETIRSE EN EL MISMO TORNEO
            entity.HasIndex(st => new { st.SponsorId, st.TournamentId })
                  .IsUnique();
        });



    }
}
