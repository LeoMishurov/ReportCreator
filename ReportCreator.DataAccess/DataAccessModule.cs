using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReportCreator.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportCreator.DataAccess
{
    public class DataAccessModule
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Получение строки подключения к бд из конфигурации 
            var connection = configuration.GetConnectionString("MyCon");
            // Настройка зависимости, добавляет при необходимости обьект MyContext
            services.AddDbContext<MyContext>(options => options.UseNpgsql(connection));

            // Настройка зависимости DependencyInjection, добавляет обьект Repository
            services.AddScoped<RepositoryEquipment>();
            services.AddScoped<RepositoryDivisions>();
        }
    }
}
