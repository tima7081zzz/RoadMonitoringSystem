using Hub.Services;
using Hub.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hub.DI
{
    public static class HubDependencyInjection
    {
        public static IServiceCollection AddHub(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<HubOptions>().Bind(configuration.GetRequiredSection(HubOptions.ConfigSectionName));

            services.AddHttpClient<IStoreClient, StoreClient>();
            services.AddHostedService<AgentDataSaverService>();
            services.AddSingleton<IAgentDataSaverService, AgentDataSaverService>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}