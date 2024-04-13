using Agent.Services;
using Agent.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agent.DI
{
    public static class AgentDependencyInjection
    {
        public static IServiceCollection AddAgent(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<AgentOptions>().Bind(configuration.GetRequiredSection(AgentOptions.ConfigSectionName));

            services.AddTransient<ICsvDataReader, CsvDataReader>();
            services.AddScoped<IQueueService, QueueService>();
            services.AddHostedService<SensorService>();

            services.AddSingleton<ICustomLogger, ConsoleLogger>();

            return services;
        }
    }
}