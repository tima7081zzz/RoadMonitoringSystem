using Agent.Services;
using Agent.Services.Interfaces;
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

            return services;
        }
    }
}