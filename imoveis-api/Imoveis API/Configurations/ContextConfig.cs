using Imoveis_Infraestrutura.Entities;
using Microsoft.EntityFrameworkCore;

namespace Imoveis_API
{
    public static class ContextConfig
    {
        public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            //var configuracao = WebApplication.CreateBuilder();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<LOCADORA_IMOVEISContext>((options) =>
                             options.UseNpgsql(connectionString));
        }
    }
}
