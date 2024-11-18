using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ENERGY_NOW_BE.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<DataContext>();
            var connectionString = configuration.GetConnectionString("DevConnection");

            // Specify the migrations assembly
            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), 
                options => options.MigrationsAssembly("ENERGY-NOW-BE.Infrastructure"));

            return new DataContext(builder.Options);
        }
    }
}
