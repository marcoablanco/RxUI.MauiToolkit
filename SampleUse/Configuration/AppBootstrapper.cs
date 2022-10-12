namespace SampleUse.Configuration;

using SampleUse.Features.Dashboard;
using SampleUse.Features.Login;
using SampleUse.Features.Main;
using SampleUse.Services.Authentication;
using SampleUse.Services.Persistance;
using SampleUse.Services.Preferences;

internal static class AppBootstrapper
{
	public static MauiAppBuilder AddDependencies(this MauiAppBuilder builder)
	{
		builder.Services
			// Essentials
			.AddSingleton(Preferences.Default)
			.AddSingleton(SecureStorage.Default)
			// Services
			.AddSingleton<IPreferencesService>(s => new PreferencesService(s))
			.AddSingleton<IAuthenticationService>(_ => new MockedAuthenticationService())
			.AddSingleton<IPersistanceService>(s => new PersistanceService(s))
			.AddSingleton<ILoginService>(s => new LoginService(s))
			// ViewModels
			.AddTransient(s => new LoginViewModel(s))
			.AddScoped(s => new DashboardPage(s))
			// Pages
			.AddTransient(s => new MainShell(s))
			.AddTransient(s => new LoginPage(s))
			.AddScoped(s => new DashboardViewModel(s));

		return builder;
	}
}
