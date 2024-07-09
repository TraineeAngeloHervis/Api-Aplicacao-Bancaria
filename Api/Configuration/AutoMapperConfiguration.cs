using Api.AutoMapper;
using AutoMapper;

namespace Api.Configuration;

public static class AutoMapperConfiguracao
{
    public static void AddConfiguracaoAutoMapper(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        
        services.AddAutoMapper(typeof(AutoMapperConfiguracao));

        ConfigurarProfiles(services);
    }

    private static void ConfigurarProfiles(IServiceCollection services)
    {
        var configuracao = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
        var mapper = configuracao.CreateMapper();
        services.AddSingleton(mapper);
    }
}