namespace SampleUse.Configuration;

using SampleUse.Features.Login;
using SampleUse.Features.Main;
using SampleUse.Services.Authentication;
using SampleUse.Services.Persistance;

internal static class AppBootstrapper
{
	public static MauiAppBuilder AddDependencies(this MauiAppBuilder builder)
	{
		builder.Services
			// Essentials
			.AddSingleton(SecureStorage.Default)
			// Services
			.AddSingleton<IAuthenticationService>(_ => new MockedAuthenticationService())
			.AddSingleton<IPersistanceService>(s => new PersistanceService(s))
			// ViewModels
			.AddTransient(s => new LoginViewModel(s))
			// Pages
			.AddScoped(_ => new MainShell())
			.AddTransient(s => new LoginPage(s));

		return builder;
	}
}
