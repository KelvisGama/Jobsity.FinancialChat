using Jobsity.FinancialChat.Application;
using Jobsity.FinancialChat.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jobsity.FinancialChat.IoC
{
    public static class NativeInjectorBootstrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplication();
            services.AddPersistence(configuration);
        }
    }
}
