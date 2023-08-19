using AutoMapper;
using System.Security.Principal;
using Transaction.Framework.Types;

namespace Transaction.WebApi.Mappers
{
    public static class TransactionMapper
    {
        public static IServiceCollection AddTransactionMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new TransactionMapperImp());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}