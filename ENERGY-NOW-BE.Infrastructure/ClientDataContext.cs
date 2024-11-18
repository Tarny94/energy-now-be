using ENERGY_NOW_BE.Core.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENERGY_NOW_BE.Infrastructure
{
    public class ClientDataContext : IdentityDbContext
    {
        public ClientDataContext(DbContextOptions<ClientDataContext> options) : base(options)
        {
        }

        public DbSet<ClientConfiguration> ClientDetails { get; set; }
    }
}
