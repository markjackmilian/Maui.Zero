using Microsoft.Extensions.Logging;

namespace Maui.Zero.Sample;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .AddZero(app =>
            {
                app.UseTransientPages = false;
            },provider => provider.GetService<AppShell>());

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}