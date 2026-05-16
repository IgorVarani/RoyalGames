using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Royal_Games.Domains;

namespace Royal_Games.Contexts;

public partial class Royal_GamesContext : DbContext
{
    public Royal_GamesContext()
    {
    }

    public Royal_GamesContext(DbContextOptions<Royal_GamesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClassificacaoIndicativa> ClassificacaoIndicativa { get; set; }

    public virtual DbSet<EstadoConservacao> EstadoConservacao { get; set; }

    public virtual DbSet<Genero> Genero { get; set; }

    public virtual DbSet<Jogo> Jogo { get; set; }

    public virtual DbSet<JogoPromocao> JogoPromocao { get; set; }

    public virtual DbSet<Log_AlteracaoJogo> Log_AlteracaoJogo { get; set; }

    public virtual DbSet<Plataforma> Plataforma { get; set; }

    public virtual DbSet<Promocao> Promocao { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Royal_Games;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClassificacaoIndicativa>(entity =>
        {
            entity.HasKey(e => e.ClassificacaoIndicativaID).HasName("PK__Classifi__892DEC6F6902F9DB");

            entity.Property(e => e.Classificacao)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadoConservacao>(entity =>
        {
            entity.HasKey(e => e.EstadoConservacaoID).HasName("PK__EstadoCo__7DFB48536C7B58FD");

            entity.Property(e => e.Nome)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.GeneroID).HasName("PK__Genero__A99D026887164C22");

            entity.Property(e => e.Nome)
                .HasMaxLength(80)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Jogo>(entity =>
        {
            entity.HasKey(e => e.JogoID).HasName("PK__Jogo__5919685579D11715");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("trg_AlteracaoJogo");
                    tb.HasTrigger("trg_ExclusaoJogo");
                });

            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Preco).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StatusJogo).HasDefaultValue(true);

            entity.HasOne(d => d.ClassificacaoIndicativa).WithMany(p => p.Jogo)
                .HasForeignKey(d => d.ClassificacaoIndicativaID)
                .HasConstraintName("FK__Jogo__Classifica__6B24EA82");

            entity.HasOne(d => d.EstadoConservacao).WithMany(p => p.Jogo)
                .HasForeignKey(d => d.EstadoConservacaoID)
                .HasConstraintName("FK__Jogo__EstadoCons__693CA210");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Jogo)
                .HasForeignKey(d => d.UsuarioID)
                .HasConstraintName("FK__Jogo__UsuarioID__6A30C649");

            entity.HasMany(d => d.Genero).WithMany(p => p.Jogo)
                .UsingEntity<Dictionary<string, object>>(
                    "JogoGenero",
                    r => r.HasOne<Genero>().WithMany()
                        .HasForeignKey("GeneroID")
                        .HasConstraintName("Fk_JogoGenero_Genero"),
                    l => l.HasOne<Jogo>().WithMany()
                        .HasForeignKey("JogoID")
                        .HasConstraintName("Fk_JogoGenero_Jogo"),
                    j =>
                    {
                        j.HasKey("JogoID", "GeneroID").HasName("Pk_JogoGenero");
                    });

            entity.HasMany(d => d.Plataforma).WithMany(p => p.Jogo)
                .UsingEntity<Dictionary<string, object>>(
                    "JogoPlataforma",
                    r => r.HasOne<Plataforma>().WithMany()
                        .HasForeignKey("PlataformaID")
                        .HasConstraintName("Fk_JogoPlataforma_Plataforma"),
                    l => l.HasOne<Jogo>().WithMany()
                        .HasForeignKey("JogoID")
                        .HasConstraintName("Fk_JogoPlataforma_Jogo"),
                    j =>
                    {
                        j.HasKey("JogoID", "PlataformaID").HasName("Pk_JogoPlataforma");
                    });
        });

        modelBuilder.Entity<JogoPromocao>(entity =>
        {
            entity.HasKey(e => new { e.JogoID, e.PromocaoID }).HasName("Pk_JogoPromocao");

            entity.Property(e => e.Preco).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Jogo).WithMany(p => p.JogoPromocao)
                .HasForeignKey(d => d.JogoID)
                .HasConstraintName("Fk_JogoPromocao_Jogo");

            entity.HasOne(d => d.Promocao).WithMany(p => p.JogoPromocao)
                .HasForeignKey(d => d.PromocaoID)
                .HasConstraintName("Fk_JogoPromocao_Promocao");
        });

        modelBuilder.Entity<Log_AlteracaoJogo>(entity =>
        {
            entity.HasKey(e => e.Log_AlteracaoJogoID).HasName("PK__Log_Alte__BB9D2C4FEA5C4211");

            entity.Property(e => e.DataAlteracao).HasPrecision(0);
            entity.Property(e => e.NomeAnterior)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PrecoAnterior).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Jogo).WithMany(p => p.Log_AlteracaoJogo)
                .HasForeignKey(d => d.JogoID)
                .HasConstraintName("FK__Log_Alter__JogoI__6E01572D");
        });

        modelBuilder.Entity<Plataforma>(entity =>
        {
            entity.HasKey(e => e.PlataformaID).HasName("PK__Platafor__B835678DFE76FB13");

            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Promocao>(entity =>
        {
            entity.HasKey(e => e.PromocaoID).HasName("PK__Promocao__254B583D029D57AE");

            entity.Property(e => e.DataExpiracao).HasPrecision(0);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StatusPromocao).HasDefaultValue(true);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioID).HasName("PK__Usuario__2B3DE7982455CD86");

            entity.ToTable(tb => tb.HasTrigger("trg_ExclusaoUsuario"));

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D10534951E32C0").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(32);
            entity.Property(e => e.StatusUsuario).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
