using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jobsity.FinancialChat.Application.Common.Interfaces;
using Jobsity.FinancialChat.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jobsity.FinancialChat.StockQuoteBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddInfrastructure();
                    services.AddHostedService<Worker>();
                });

    }
}
