
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConferenceReservationSystem.Entity
{
    public class AppDbContext : IdentityDbContext<Person, Role, Guid>
    {
        #region Sets
        public virtual DbSet<Person> AspNetUsers { get; set; }
        public virtual DbSet<Conference> Conferences { get; set; }
        public virtual DbSet<ConferenceUser> ConferenceUsers { get; set; }
        #endregion

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=www.refahkart.com;initial catalog=Conference;persist security info=False;user id=sa;password=Sa14101410;packet size=4096;MultipleActiveResultSets=true;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Persian_100_CI_AI");

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Conference>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<ConferenceUser>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Conference)
                    .WithMany(p => p.ConferenceUsers)
                    .HasForeignKey(d => d.ConferenceId)
                    .HasConstraintName("FK_ConferenceUser_Conference");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ConferenceUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ConferenceUser_AspNetUsers");
            });

            base.OnModelCreating(modelBuilder);

        }

    }
}
