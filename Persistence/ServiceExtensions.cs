using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class ServiceExtensions
    {
        public static void AddPersistenceInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
            //    configuration.GetConnectionString("DefaultConnection")
            //));

            #region Repositories
            //AddTransient
            //Transient lifetime services are created each time they are requested.This lifetime works best for lightweight, stateless services.
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(MyRepositoryAsync<>));
            //AddScoped
            //Scoped lifetime services are created once per request.

            //AddSingleton
            //Singleton lifetime services are created the first time they are requested(or when ConfigureServices is run if you specify an instance there) and then every subsequent request will use the same instance.

            //services.AddTransient(typeof(IRepositoryAsync<>), typeof(MyRepositoryAsync<>));
            #endregion



            // para correr migraciones
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
            //    configuration.GetConnectionString("DefaultConnection"),
            //    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
            //));
        }
    }
}
