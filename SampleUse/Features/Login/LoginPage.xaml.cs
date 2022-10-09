namespace SampleUse.Features.Login;

using ReactiveUI;
using RxUI.MauiToolkit.Services.Loading;
using System.Reactive.Disposables;

public partial class LoginPage
{
	private readonly ILoadingService<LoginViewModel> loadingService;

	public LoginPage(IServiceProvider serviceProvider) : base(serviceProvider)
	{
		loadingService = serviceProvider.GetRequiredService<ILoadingService<LoginViewModel>>();

		InitializeComponent();
	}

	protected override CompositeDisposable OnActivated(CompositeDisposable disposables)
	{
		base.OnActivated(disposables);

		disposables.Add(this.Bind(ViewModel, vm => vm.User, v => v.TxtUser.Text));
		disposables.Add(this.Bind(ViewModel, vm => vm.Password, v => v.TxtPassword.Text));

		disposables.Add(this.OneWayBind(ViewModel, vm => vm.LoginCommand, v => v.TxtPassword.ReturnCommand));

		disposables.Add(this.BindCommand(ViewModel, vm => vm.LoginCommand, v => v.BtnLogin));

		disposables.Add(LoadingControl.BindLoadingService(loadingService));

		return disposables;
	}
}