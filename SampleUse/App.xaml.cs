namespace SampleUse;

using RxUI.MauiToolkit.Services.AppLog;
using SampleUse.Features.Login;

public partial class App : Application
{
	private readonly ILogService logService;

	public App(IServiceProvider serviceProvider)
	{
		this.logService = serviceProvider.GetRequiredService<ILogService>();
		InitializeComponent();

		MainPage = serviceProvider.GetRequiredService<LoginPage>();
	}

	protected override void OnStart()
	{
		base.OnStart();
		// logService.SetAppCenterId("iosId", "androidId", "uwpId", "macosId"); ;
	}
}
