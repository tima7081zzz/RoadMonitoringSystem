using Agent.Services;
using Agent.Services.Interfaces;
using Common;
using Microsoft.Extensions.DependencyInjection;

namespace Agent.DI
{
    public static class AgentDependencyInjection
    {
        public static IServiceCollection AddAgent(this IServiceCollection services)
        {
            services.AddTransient<ICsvDataReader, CsvDataReader>();
            services.AddScoped<IQueueService, QueueService>();
            services.AddHostedService<SensorService>();

            services.AddSingleton<ICustomLogger, ConsoleLogger>();
            services.AddSingleton<IAppConfig, AppConfig>();

            return services;
        }
    }
}