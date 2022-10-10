namespace SampleUse.Features.Main;

using RxUI.MauiToolkit.Services.AppLog;
using RxUI.MauiToolkit.Services.Loading;
using SampleUse.Features.Login;
using System.Threading.Tasks;

public partial class MainShell : Shell
{
	private readonly ILoadingService loadingService;
	private readonly ILoginService loginService;
	private readonly ILogService logService;
	private readonly IServiceProvider serviceProvider;

	public MainShell(IServiceProvider serviceProvider)
	{
		this.serviceProvider = serviceProvider;
		loadingService = serviceProvider.GetRequiredService<ILoadingService>();
		loginService = serviceProvider.GetRequiredService<ILoginService>();
		logService = serviceProvider.GetRequiredService<ILogService<MainShell>>();

		InitializeComponent();
	}

	public async Task RefreshTokenAsync()
	{
		try
		{
			loadingService.Add(Text.LoadingLogin);
			await loginService.AutoLoginAsync();
		}
		catch (Exception ex)
		{
			logService.Error(ex);
			// TODO Alert showing error.
			var loginPage = serviceProvider.GetRequiredService<LoginPage>();
			Application.Current.MainPage = loginPage;
		}
		finally
		{
			loadingService.Remove(Text.LoadingLogin);
		}
	}
}
