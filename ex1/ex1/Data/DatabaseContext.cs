using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ex1.Models;

namespace ex1.Data;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Articulo> Articulos { get; set; }

    public virtual DbSet<Fabricante> Fabricantes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("articulos");

            entity.HasIndex(e => e.Fabricante, "fabricante");

            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Fabricante).HasColumnName("fabricante");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Precio).HasColumnName("precio");

            entity.HasOne(d => d.FabricanteNavigation).WithMany(p => p.Articulos)
                .HasForeignKey(d => d.Fabricante)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("articulos_ibfk_1");
        });

        modelBuilder.Entity<Fabricante>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("fabricantes");

            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
