namespace SampleUse.Features.Login;

using RxUI.MauiToolkit.Bases;
using RxUI.MauiToolkit.Services.Loading;

public class LoginViewModel : RxBasePageViewModel
{
	private readonly ILoadingService loadingService;

	public LoginViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
	{
		loadingService = serviceProvider.GetRequiredService<ILoadingService<LoginViewModel>>();
	}
}
