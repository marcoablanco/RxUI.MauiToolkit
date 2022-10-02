namespace RxUI.MauiToolkit.Configuration;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RxUI.MauiToolkit.Services.AppLog;
using RxUI.MauiToolkit.Services.Loading;

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
		builder.Services.AddSingleton(typeof(ILogService<>), typeof(LogService<>))
						.AddScoped(typeof(ILoadingService<>), typeof(LoadingService<>));
		return builder;
	}
}
