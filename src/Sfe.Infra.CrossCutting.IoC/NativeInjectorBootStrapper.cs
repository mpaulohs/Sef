using Microsoft.Extensions.DependencyInjection;
using Sfe.Domain.AggregatesModel.UserAggregate;
using Sfe.Infra.Data;
using Sfe.Infra.Data.Repositories;

namespace Sfe.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Infra
            services.AddScoped<IUserRepository, UserRepository>();           
            services.AddScoped<SfeContext>();
        }
    }
}
