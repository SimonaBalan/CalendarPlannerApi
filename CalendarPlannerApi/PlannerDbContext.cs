using CalendarPlannerApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalendarPlannerApi
{
    public partial class PlannerDbContext : DbContext
    {
        public PlannerDbContext()
        {
        }

        public PlannerDbContext(DbContextOptions<PlannerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshToken>(entity =>
            {

                entity.Property(e => e.ExpiryDate).HasColumnType("smalldatetime");

                entity.Property(e => e.TokenHash)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.TokenSalt)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Ts)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("TS");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RefreshToken_User");

                entity.ToTable("RefreshToken");
            });

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PlannedDate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("PlannedDate");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activity_User");

                entity.ToTable("Activity");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Ts)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("TS");

                entity.ToTable("User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
