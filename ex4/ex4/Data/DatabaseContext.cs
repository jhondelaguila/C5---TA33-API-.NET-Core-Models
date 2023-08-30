using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ex4.Models;

namespace ex4.Data;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pelicula> Peliculas { get; set; }

    public virtual DbSet<Sala> Salas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("peliculas");

            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.CalificacionEdad).HasColumnName("calificacion_edad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Sala>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("salas");

            entity.HasIndex(e => e.Pelicula, "pelicula");

            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Pelicula).HasColumnName("pelicula");

            entity.HasOne(d => d.PeliculaNavigation).WithMany(p => p.Salas)
                .HasForeignKey(d => d.Pelicula)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("salas_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
