namespace SampleUse;

using RxUI.MauiToolkit.Services.AppLog;
using SampleUse.Features.Login;
using SampleUse.Features.Main;
using SampleUse.Services.Preferences;

public partial class App : Application
{
	private readonly ILogService logService;
	private readonly IPreferencesService preferencesService;

	public App(IServiceProvider serviceProvider)
	{
		logService = serviceProvider.GetRequiredService<ILogService<App>>();
		preferencesService = serviceProvider.GetRequiredService<IPreferencesService>();

		InitializeComponent();

		DateTimeOffset? dateRefreshToken = preferencesService.GetDateRefresh();

		if (dateRefreshToken.HasValue && dateRefreshToken > DateTimeOffset.UtcNow)
		{
			MainShell mainShell = serviceProvider.GetRequiredService<MainShell>();
			mainShell.RefreshTokenAsync().ConfigureAwait(false);
			MainPage = mainShell;
		}
		else
		{
			MainPage = serviceProvider.GetRequiredService<LoginPage>();
		}
	}

	protected override void OnStart()
	{
		logService.Log("OnStart");
		base.OnStart();
		// logService.SetAppCenterId("iosId", "androidId", "uwpId", "macosId");
	}

	protected override void OnResume()
	{
		base.OnResume();
		logService.Log("OnResume");
	}

	protected override void OnSleep()
	{
		base.OnSleep();
		logService.Log("OnSleep");
	}
}
