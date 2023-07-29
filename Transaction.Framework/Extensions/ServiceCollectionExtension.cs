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

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddTransactionFramework(this IServiceCollection services, IConfiguration configuration)
        {
            // Service
            services.AddScoped<ITransactionService, TransactionService>();

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

            // Connection String
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("SqlServerConnection")));
        
            return services;
        }
    }
}
