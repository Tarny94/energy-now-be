using ENERGY_NOW_BE.Core.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ENERGY_NOW_BE.Infrastructure
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Client> ClientConfigurations { get; set; }

        public DbSet<ClientSpecialization> ClientSpecializations { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasOne(c => c.User)
                .WithMany(u => u.Client)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "a4a4b5b7-8d73-4f55-8e91-1ed9e437e2f5",
                    Name = "USER",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Id = "b6b6c9d9-e862-4fb5-9c2c-5e9c7e9781c3",
                    Name = "CLIENT",
                    NormalizedName = "CLIENT"
                },
                new IdentityRole
                {
                    Id = "c8c8e1f1-f973-48d5-af3c-7e9c9e9781f4",
                    Name = "ADMIN",
                    NormalizedName = "ADMIN"
                }
            );
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClientSpecialization>()
                .HasKey(cs => new { cs.ClientId, cs.Specialization });

            modelBuilder.Entity<ClientSpecialization>()
                .HasOne(cs => cs.Client)
                .WithMany(c => c.ClientSpecializations)
                .HasForeignKey(cs => cs.ClientId);

            modelBuilder.Entity<ClientSpecialization>()
                .Property(cs => cs.Specialization)
                .HasConversion<string>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
