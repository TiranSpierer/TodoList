using ToDoList.Core.Interfaces;
using ToDoList.Infrastructure.Data;
using ToDoList.Core.Services;
using ToDoList.Core.Settings;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using ToDoList.Core.DataModels;

namespace ToDoList.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            return services;
        }

        public static IServiceCollection RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));

            services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                return new MongoClient(settings.ConnectionString);
            });

            services.AddSingleton(serviceProvider =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                var client = serviceProvider.GetRequiredService<IMongoClient>();
                return client.GetDatabase(settings.DatabaseName);
            });

            services.AddScoped<IRepository<ToDoTask>>(serviceProvider =>
            {
                var database = serviceProvider.GetRequiredService<IMongoDatabase>();
                string collectionName = configuration.GetValue<string>("MongoDbSettings:ToDoTaskCollection")
                                        ?? throw new ArgumentException("No collection name at: MongoDbSettings:ToDoTaskCollection");
                
                return new MongoRepository<ToDoTask>(database, collectionName);
            });

            services.AddScoped<IToDoTaskService, ToDoTaskService>();

            return services;
        }
    }
}
