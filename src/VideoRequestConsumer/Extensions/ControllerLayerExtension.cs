using Controller.Application;
using Controller.Application.Interfaces;
using Controller.Utils;
using Controller.Utils.Interfaces;

namespace VideoRequestConsumer.Extensions;

internal static class ControllerLayerExtension
{
    public static IServiceCollection AddControllerLayer(this IServiceCollection services)
    {
        return
            services
                .AddApplication()
                .AddUtils();
    }

    private static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddSingleton<IVideoRequestApplication, VideoRequestApplication>();
    }

    private static IServiceCollection AddUtils(this IServiceCollection services)
    {
        return services
            .AddSingleton<IControllerVariables, ControllerVariables>();
    }
}
