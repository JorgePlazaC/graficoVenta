using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Proyecto1;

public partial class VentasContext : DbContext
{
    public VentasContext()
    {
    }

    public VentasContext(DbContextOptions<VentasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<VentaProducto> VentaProductos { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost; database=ventas; user=root; password=");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__efmigrationshistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("producto");

            entity.HasIndex(e => e.Codigo, "Codigo").IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(10)");
            entity.Property(e => e.Codigo).HasColumnType("int(10)");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(255);
            entity.Property(e => e.Precio).HasColumnType("int(10)");
        });

        modelBuilder.Entity<VentaProducto>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.ProductoId, e.VentaId }).HasName("PRIMARY");

            entity.ToTable("venta_producto");

            entity.HasIndex(e => e.ProductoId, "FKVenta_Prod199609");

            entity.HasIndex(e => e.VentaId, "FKVenta_Prod441897");

            entity.Property(e => e.Id).HasColumnType("int(10)");
            entity.Property(e => e.ProductoId).HasColumnType("int(10)");
            entity.Property(e => e.VentaId).HasColumnType("int(10)");
            entity.Property(e => e.Cantidad).HasColumnType("int(10)");
            entity.Property(e => e.Precio).HasColumnType("int(10)");

            entity.HasOne(d => d.Producto).WithMany(p => p.VentaProductos)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FKVenta_Prod199609");

            entity.HasOne(d => d.Venta).WithMany(p => p.VentaProductos)
                .HasForeignKey(d => d.VentaId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FKVenta_Prod441897");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("venta");

            entity.HasIndex(e => e.Correlativo, "Correlativo").IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(10)");
            entity.Property(e => e.Correlativo).HasColumnType("int(10)");
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.Hora).HasMaxLength(6);
            entity.Property(e => e.RutCliente).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
