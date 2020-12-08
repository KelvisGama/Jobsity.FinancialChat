using Jobsity.FinancialChat.Application.Common.Interfaces;
using Jobsity.FinancialChat.Application.Common.Interfaces.ExternalApis;
using Jobsity.FinancialChat.Infrastructure.ExternalApis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jobsity.FinancialChat.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddHttpClient<IStooqApi, StooqApi>();
            return services;
        }
    }
}