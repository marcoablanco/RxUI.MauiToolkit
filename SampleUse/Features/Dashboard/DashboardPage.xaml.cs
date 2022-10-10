namespace SampleUse.Features.Dashboard;

using RxUI.MauiToolkit.Services.Loading;
using System.Reactive.Disposables;

public partial class DashboardPage
{
	private readonly ILoadingService loadingService;

	public DashboardPage(IServiceProvider serviceProvider) : base(serviceProvider)
	{
		loadingService = serviceProvider.GetRequiredService<ILoadingService>();

		InitializeComponent();
	}

	protected override CompositeDisposable OnActivated(CompositeDisposable disposables)
	{
		base.OnActivated(disposables);

		disposables.Add(LoadingControl.BindLoadingService(loadingService));

		return disposables;
	}
}