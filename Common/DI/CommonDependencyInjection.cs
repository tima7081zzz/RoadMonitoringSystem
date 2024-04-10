using Microsoft.Extensions.DependencyInjection;

namespace Common.DI
{
    public static class CommonDependencyInjection
    {
        public static IServiceCollection AddCommon(this IServiceCollection services)
        {
            services.AddSingleton<ICustomLogger, ConsoleLogger>();
            services.AddSingleton<IAppConfig, AppConfig>();

            return services;
        }
    }
}