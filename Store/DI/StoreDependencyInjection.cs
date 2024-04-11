using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Store.DAL;

namespace Store.DI
{
    public static class StoreDependencyInjection
    {
        public static IServiceCollection AddStore(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>((provider, builder) =>
            {
                builder.UseNpgsql("");
            });


            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}