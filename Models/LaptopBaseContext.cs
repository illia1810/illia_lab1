using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LaptopProject
{
    public partial class LaptopBaseContext : DbContext
    {
        public LaptopBaseContext()
        {
        }

        public LaptopBaseContext(DbContextOptions<LaptopBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Feature> Feature { get; set; }
        public virtual DbSet<Laptop> Laptop { get; set; }
        public virtual DbSet<LaptopFeature> LaptopFeature { get; set; }
        public virtual DbSet<Processor> Processor { get; set; }
        public virtual DbSet<Producer> Producer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=user-pc\\sqlexpress; Database=LaptopBase; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Color>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Color1)
                    .IsRequired()
                    .HasColumnName("Color")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Feature>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Feature1)
                    .IsRequired()
                    .HasColumnName("Feature")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Laptop>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ColorId).HasColumnName("Color_ID");

                entity.Property(e => e.CountryId).HasColumnName("Country_ID");

                entity.Property(e => e.CpuId).HasColumnName("CPU_ID");

                entity.Property(e => e.ModelId).HasColumnName("Model_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.Laptop)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Laptops_Color");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Laptop)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Laptops_Countries");

                entity.HasOne(d => d.Cpu)
                    .WithMany(p => p.Laptop)
                    .HasForeignKey(d => d.CpuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Laptops_CPU");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Laptop)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Laptops_Models");
            });

            modelBuilder.Entity<LaptopFeature>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FeatureId).HasColumnName("Feature_ID");

                entity.Property(e => e.LaptopId).HasColumnName("Laptop_ID");

                entity.HasOne(d => d.Feature)
                    .WithMany(p => p.LaptopFeature)
                    .HasForeignKey(d => d.FeatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LaptopFeatures_Features");

                entity.HasOne(d => d.Laptop)
                    .WithMany(p => p.LaptopFeature)
                    .HasForeignKey(d => d.LaptopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LaptopFeatures_Laptops");
            });

            modelBuilder.Entity<Processor>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Producer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Model).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
