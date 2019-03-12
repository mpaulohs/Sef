using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Sfe.Domain.AggregatesModel.MessageSenderAggregate;
using Sfe.Domain.AggregatesModel.UserAggregate;
using Sfe.Infra.Data;
using Sfe.Infra.Data.Repositories;

namespace Sfe.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<UserManager<User>>();
            // Infra 
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmailSenderRepository, EmailSenderRepository>();
            services.AddScoped<SfeContext>();
        }
    }
}
