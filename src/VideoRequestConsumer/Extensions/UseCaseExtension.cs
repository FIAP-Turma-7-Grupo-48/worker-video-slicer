using UseCase.UseCase;
using UseCase.UseCase.Interfaces;

namespace VideoRequestConsumer.Extensions;

internal static class UseCaseExtension
{
    public static IServiceCollection AddUseCase(this IServiceCollection services)
    {
        return
            services
                .AddServices();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IFileUseCase, FileUseCase>()
            .AddSingleton<IVideoUseCase, VideoUseCase>();
    }
}
