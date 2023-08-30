using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ex2.Models;

namespace ex2.Data;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("departamentos");

            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Presupuesto).HasColumnName("presupuesto");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Dni).HasName("PRIMARY");

            entity.ToTable("empleados");

            entity.HasIndex(e => e.Departamento, "departamento");

            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .HasColumnName("dni");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(250)
                .HasColumnName("apellidos")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Departamento).HasColumnName("departamento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.DepartamentoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.Departamento)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("empleados_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
