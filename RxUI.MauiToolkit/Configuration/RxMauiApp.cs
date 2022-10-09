namespace RxUI.MauiToolkit.Configuration;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RxUI.MauiToolkit.Services.AppLog;
using RxUI.MauiToolkit.Services.Loading;
using RxUI.MauiToolkit.Utils;

public static class RxMauiApp
{
	public static MauiAppBuilder InitRxToolkit(this MauiAppBuilder builder)
	{
		builder.Services.AddLogging(configure =>
		{
			configure.AddDebug()
					 .AddConsole();

		});

		return builder.AddServices();

	}

	private static MauiAppBuilder AddServices(this MauiAppBuilder builder)
	{
		// Can't avoid reflection with generic types.
		builder.Services.AddSingleton(typeof(ILogService<>), typeof(LogService<>))
						.AddSingleton(typeof(ILogService), s => new LogService<Generic>(s))
						.AddScoped(typeof(ILoadingService<>), typeof(LoadingService<>))
						.AddScoped(typeof(ILoadingService), s => new LoadingService<Generic>());
		return builder;
	}
}
