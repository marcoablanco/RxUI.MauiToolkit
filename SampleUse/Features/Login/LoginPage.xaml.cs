namespace SampleUse.Features.Login;

using RxUI.MauiToolkit.Services.Loading;

public partial class LoginPage
{
	private readonly ILoadingService loadingService;

	public LoginPage(IServiceProvider serviceProvider) : base(serviceProvider)
	{
		loadingService = serviceProvider.GetRequiredService<ILoadingService<LoginViewModel>>();

		InitializeComponent();
	}
}