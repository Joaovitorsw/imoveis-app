using Imoveis_Dominio.v1.Interfaces;
using Imoveis_Dominio.v1.Servicos;
using Imoveis_Infraestrutura.v1.Interfaces;
using Imoveis_Infraestrutura.v1.Repositorios;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Imoveis_API.Configurations
{
    public static class DependecyConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            //Work Interfaces
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            //Repositories
            services.AddScoped<IImoveisRepositorio, ImoveisRepositorio>();

            // Services
            services.AddScoped<IImoveisServico, ImoveisServico>();

            return services;
        }
    }
}
