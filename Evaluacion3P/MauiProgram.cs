// MauiProgram.cs
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Hosting;
using Evaluacion3P.Data;
using Evaluacion3P.ViewModels;
using Evaluacion3P.Views;

namespace Evaluacion3P;

public static class MauiProgram
{
    public static Microsoft.Maui.Hosting.MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register services in the correct order: dependencies first.
        // ClienteDatabase and LogManager are singletons, so they are created once.
        builder.Services.AddSingleton<ClienteDatabase>();
        builder.Services.AddSingleton<LogManager>();

        // ViewModels depend on ClienteDatabase and LogManager, so they should be registered after.
        // ViewModels are usually Transient because they are tied to a specific page instance.
        builder.Services.AddTransient<ClienteListViewModel>();
        builder.Services.AddTransient<ClienteDetailViewModel>();
        builder.Services.AddTransient<LogViewModel>();

        // Pages are also usually Transient, and they receive their ViewModels via DI.
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<ClienteDetailPage>();
        builder.Services.AddTransient<LogPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}