using System;
using System.Collections.Generic;
using HypeHaven.Areas.Identity.Data;
using HypeHaven.models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HypeHaven.models;

public partial class HypeHavenContext : IdentityDbContext<HypeHavenUser>
{
    public HypeHavenContext()
    {
    }

    public HypeHavenContext(DbContextOptions<HypeHavenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
   // public virtual DbSet<AspNetRole> AspNetRoles { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__categori__D54EE9B45A129DE4");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__categorie__paren__4CA06362");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__brands__5E5A8E270E6B71A1");

            entity.ToTable("brands");

            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Facebook)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("facebook");
            entity.Property(e => e.Id).HasMaxLength(450);
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Instagram)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("instagram");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.Pinterest)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("pinterest");
            entity.Property(e => e.Tiktok)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("tiktok");
            entity.Property(e => e.Video)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("video");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__orders__46596229B0138C3B");

            entity.ToTable("orders");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.DeliveryDate)
                .HasColumnType("date")
                .HasColumnName("delivery_date");
            entity.Property(e => e.Id).HasMaxLength(450);
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("order_status");
            entity.Property(e => e.PaymentsId).HasColumnName("payments_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.PromoCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("promo_code");
            entity.Property(e => e.ShippingAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("shipping_address");
            entity.Property(e => e.ShippingMethod)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("shipping_method");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");
            entity.Property(e => e.TrackingNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("tracking_number");

            entity.HasOne(d => d.Payments).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__orders__payments__6B24EA82");

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__orders__product___6A30C649");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__order_it__3764B6BC0338597A");

            entity.ToTable("order_items");

            entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__order_ite__order__6E01572D");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__order_ite__produ__6EF57B66");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__payments__ED1FC9EAB83116EA");

            entity.ToTable("payments");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Id).HasMaxLength(450);
            entity.Property(e => e.PaidAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("paid_amount");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("payment_method");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__products__47027DF5C7801DC4");

            entity.ToTable("products");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Color)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Material)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("material");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Size)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("size");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__products__brand___6383C8BA");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__reviews__60883D90800C4BBA");

            entity.ToTable("reviews");

            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.Id).HasMaxLength(450);
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Text)
                .HasColumnType("text")
                .HasColumnName("text");

            entity.HasOne(d => d.Brand).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reviews__brand_i__72C60C4A");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reviews__product__73BA3083");
        });

/*        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });*/

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new HypeHavenUserEntityConfiguration());
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
public class HypeHavenUserEntityConfiguration : IEntityTypeConfiguration<HypeHavenUser>
{
    public void Configure(EntityTypeBuilder<HypeHavenUser> builder)
    {
        builder.Property(u => u.Name).HasMaxLength(255);
        builder.Property(u => u.Address).HasMaxLength(255);
    }
}