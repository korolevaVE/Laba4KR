using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ShopApp.Models;

namespace ShopApp.Data;

public partial class ShopDbContext : DbContext
{
    public ShopDbContext()
    {
    }

    public ShopDbContext(DbContextOptions<ShopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categories> Categories { get; set; }

    public virtual DbSet<Manufacturers> Manufacturers { get; set; }

    public virtual DbSet<OrderArticle> OrderArticle { get; set; }

    public virtual DbSet<Orders> Orders { get; set; }

    public virtual DbSet<PickupPoint> PickupPoint { get; set; }

    public virtual DbSet<ProductNames> ProductNames { get; set; }

    public virtual DbSet<Products> Products { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Statuses> Statuses { get; set; }

    public virtual DbSet<Suppliers> Suppliers { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=Lab3_Shop_Makarov;Integrated Security=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categories>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC0731103D3D");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Manufacturers>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Manufact__3214EC07C7E59C1A");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<OrderArticle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderArt__3214EC074D90BB28");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Order).WithMany(p => p.OrderArticle)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrdersArticle_Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderArticle)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_OrdersArticle_Products");
        });

        modelBuilder.Entity<Orders>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC07F51C0B72");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");

            entity.HasOne(d => d.PickupPoint).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PickupPointId)
                .HasConstraintName("FK_Orders_PickupPoints");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_Orders_Statuses");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Orders_Users");
        });

        modelBuilder.Entity<PickupPoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PickupPo__3214EC07FB2B42DC");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Adress).HasMaxLength(255);
        });

        modelBuilder.Entity<ProductNames>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductN__3214EC077102D859");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Products>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC07C5DD8FFC");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Article).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ImagePath).HasMaxLength(255);
            entity.Property(e => e.Unit).HasMaxLength(255);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Products_Categories");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products)
                .HasForeignKey(d => d.ManufacturerId)
                .HasConstraintName("FK_Products_Manufacturers");

            entity.HasOne(d => d.Name).WithMany(p => p.Products)
                .HasForeignKey(d => d.NameId)
                .HasConstraintName("FK_Products_ProductNames");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_Products_Suppliers");
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC0729712426");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Role).HasMaxLength(255);
        });

        modelBuilder.Entity<Statuses>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Statuses__3214EC077E4FCE2D");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Status).HasMaxLength(255);
        });

        modelBuilder.Entity<Suppliers>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3214EC0790FE139B");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07271DE9FC");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Login).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .HasConstraintName("FK_Users_UserRoles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
