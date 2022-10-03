namespace SampleUse;

using RxUI.MauiToolkit.Services.AppLog;
using SampleUse.Features.Login;

public partial class App : Application
{
	private readonly ILogService logService;

	public App(LoginPage loginPage, ILogService logService)
	{
		this.logService = logService;
		InitializeComponent();

		MainPage = loginPage;
	}

	protected override void OnStart()
	{
		base.OnStart();
		// logService.SetAppCenterId("iosId", "androidId", "uwpId", "macosId"); ;
	}
}
