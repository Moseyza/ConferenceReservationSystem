using ConferenceReservationSystem.Entity.TotalSystem;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceReservationSystem.Entity
{
    public class SmsDbContext : DbContext
    {
        public virtual DbSet<SmsData> Smsdata { get; set; }
        public SmsDbContext(DbContextOptions<SmsDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=refahkart.com;Initial Catalog=TotalSystem;User Id=sa;Password=Sa14101410;TrustServerCertificate=True");
        }
    }
}
