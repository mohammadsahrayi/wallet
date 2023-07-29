namespace Transaction.Framework.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Transaction.Framework.Data;
    using Transaction.Framework.Data.Interface;
    using Transaction.Framework.Data.Repositories;
    using Transaction.Framework.Services;
    using Transaction.Framework.Services.Interface;
    using Transaction.Framework.Mappers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Microsoft.OpenApi.Models;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using Swashbuckle.AspNetCore.Swagger;
    using Microsoft.Extensions.Logging;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddTransactionFramework(this IServiceCollection services, IConfiguration configuration)
        {
            // Connection String
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("SqlServerConnection")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
            services.Configure<MvcOptions>(c =>
             c.Conventions.Add(new SwaggerApplicationConvention()));

            services.AddTransient<ISwaggerProvider, SwaggerGenerator>();



            // Repository
            services.AddScoped<IAccountSummaryRepository, AccountSummaryRepository>();
            services.AddScoped<IAccountTransactionRepository, AccountTransactionRepository>();
            // Mappers

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            //loger
   

            // Service
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddControllers();

            return services;
        }
    }
}
