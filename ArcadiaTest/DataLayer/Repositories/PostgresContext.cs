using ArcadiaTest.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArcadiaTest.DataLayer.Repositories
{
    public class PostgresContext : DbContext
    {
        
        public virtual DbSet<Qualification> Qualifications { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<CookQualifications> CookQualifications { get; set; }
        public virtual DbSet<Cook> Cooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<Cook>(entity =>
            {
                entity.ToTable("cook");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");

                entity.Property(e => e.SecondName).HasColumnName("second_name");

                entity.Property(e => e.Shift)
                    .HasColumnName("shift")
                    .HasMaxLength(1);

                entity.Property(e => e.Workday).HasColumnName("workday");

                entity.Property(e => e.Workdays).HasColumnName("workdays");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Cook)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cook_restaurant_id_fkey");
            });

            modelBuilder.Entity<CookQualifications>(entity =>
            {
                entity.HasKey(e => new { e.CookId, e.QualificationId })
                    .HasName("cook_qualifications_pkey");

                entity.ToTable("cook_qualifications");

                entity.Property(e => e.CookId).HasColumnName("cook_id");

                entity.Property(e => e.QualificationId).HasColumnName("qualification_id");

                entity.HasOne(d => d.Cook)
                    .WithMany(p => p.CookQualifications)
                    .HasForeignKey(d => d.CookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cook_qualifications_cook_id_fkey");

                entity.HasOne(d => d.Qualification)
                    .WithMany(p => p.CookQualifications)
                    .HasForeignKey(d => d.QualificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cook_qualifications_qualification_id_fkey");
            });

            modelBuilder.Entity<Qualification>(entity =>
            {
                entity.ToTable("qualification");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("restaurant");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title");
            });
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=arcadia_test;Username=postgres;Password=my_pw");
            }
        }
    }
}