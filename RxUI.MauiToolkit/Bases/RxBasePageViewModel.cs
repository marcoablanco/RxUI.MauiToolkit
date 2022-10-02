namespace RxUI.MauiToolkit.Bases;

using RxUI.MauiToolkit.Services.AppLog;
using System.Reactive.Disposables;

public abstract class RxBasePageViewModel : RxBaseViewModel
{
	private readonly ILogService? logService;

	public RxBasePageViewModel(IServiceProvider? serviceProvider = null) : base(serviceProvider)
	{
		this.logService = serviceProvider?.GetService<ILogService>();
	}


	public virtual CompositeDisposable OnActivated(CompositeDisposable disposables)
	{
		logService?.Log($"{NameViewModel} activated.");
		return disposables;
	}

	public virtual Task OnAppearingAsync()
	{
		logService?.Log($"{NameViewModel} appearing.");
		return Task.CompletedTask;
	}

	public virtual Task OnDisappearingAsync()
	{
		logService?.Log($"{NameViewModel} disappearing.");
		return Task.CompletedTask;
	}
}