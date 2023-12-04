using Microsoft.EntityFrameworkCore;

namespace EvaluacionDavidLlopis.Models;

public partial class LigaContext : DbContext
{
    public LigaContext()
    {
    }

    public LigaContext(DbContextOptions<LigaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<Jugadore> Jugadores { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source=PC_CELIA_DAVID;Initial Catalog=Liga;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Equipo>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("PK__Equipos__3214EC073AAF70A4");

            _ = entity.Property(e => e.Ciudad)
                .HasMaxLength(100)
                .IsUnicode(false);
            _ = entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        _ = modelBuilder.Entity<Jugadore>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("PK__Jugadore__3214EC071B2A6039");

            _ = entity.Property(e => e.Nombre).HasMaxLength(150);
            _ = entity.Property(e => e.Sueldo).HasColumnType("decimal(9, 2)");

            _ = entity.HasOne(d => d.Equipo).WithMany(p => p.Jugadores)
                .HasForeignKey(d => d.EquipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Equipos_Jugadores");
        });

        _ = modelBuilder.Entity<Usuario>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC074D1A6B02");

            _ = entity.Property(e => e.Email).HasMaxLength(100);
            _ = entity.Property(e => e.Password).HasMaxLength(500);
            _ = entity.Property(e => e.Rol).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
