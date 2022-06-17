using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebsiteStockManagement.Models
{
    public partial class StockManagementWesbiteDBContext : DbContext
    {
        public StockManagementWesbiteDBContext()
        {
        }

        public StockManagementWesbiteDBContext(DbContextOptions<StockManagementWesbiteDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=StockManagementWesbiteDB;Trusted_Connection=True;");
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("PRODUCT");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id");

                entity.Property(e => e.ProductDaterange)
                    .HasColumnType("date")
                    .HasColumnName("product_daterange");

                entity.Property(e => e.ProductPrice).HasColumnName("product_price");

                entity.Property(e => e.ProductType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("product_type");

                entity.Property(e => e.UsersId).HasColumnName("users_id");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.UsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PRODUCT__users_i__267ABA7A");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UsersId)
                    .HasName("PK__USERS__EAA7D14BCC53172B");

                entity.ToTable("USERS");

                entity.Property(e => e.UsersId)
                    .HasColumnName("users_id");

                entity.Property(e => e.UsersFirstname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("users_firstname");

                entity.Property(e => e.UsersPassword)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("users_password");

                entity.Property(e => e.UsersRole)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("users_role");

                entity.Property(e => e.UsersSurname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("users_surname");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
