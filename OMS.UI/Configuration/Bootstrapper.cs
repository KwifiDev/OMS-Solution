using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OMS.BL.Mapping;
using OMS.DA.Context;
using System.IO;


namespace OMS.UI.Configuration
{
    public class Bootstrapper
    {
        public IServiceProvider ConfigureServices()
        {
            IConfigurationRoot? configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            ServiceCollection services = new ServiceCollection();

            string? connectionString = configuration.GetConnectionString("DbConnection");

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            // Register interfaces repositories 
            // ======================

            // ======================

            // Register services from Business Layer
            // ======================

            // ======================

            // Register AutoMapper
            MapperConfiguration? mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Register ViewModels
            // ======================

            // ======================

            return services.BuildServiceProvider();
        }
    }
}
