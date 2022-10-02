namespace SampleUse.Configuration;

using SampleUse.Features.Login;
using SampleUse.Features.Main;

internal static class AppBootstrapper
{
	public static MauiAppBuilder AddDependencies(this MauiAppBuilder builder)
	{
		builder.Services
			// ViewModels
			.AddTransient(s => new LoginViewModel(s))
			// Pages
			.AddScoped(_ => new MainShell())
			.AddTransient(s => new LoginPage(s));

		return builder;
	}
}
