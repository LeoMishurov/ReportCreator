
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReportCreator.DataAccess.Models;

namespace ReportCreator.DataAccess
{

    public class MyContext : DbContext
    {

        public DbSet<Division> Divisions { get; set; }
        public DbSet<Equipment> Equipment { get; set; }


        /// <summary>
        /// настройки подключения к базе данных
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=123")
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}

