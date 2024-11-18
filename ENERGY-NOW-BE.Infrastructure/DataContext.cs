using ENERGY_NOW_BE.Core.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ENERGY_NOW_BE.Infrastructure
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<ClientConfiguration> ClientConfigurations { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
