namespace RxUI.MauiToolkit.Bases;

using RxUI.MauiToolkit.Services.AppLog;
using RxUI.MauiToolkit.Utils;
using System.Reactive.Disposables;

public abstract class RxBasePageViewModel : RxBaseViewModel
{
	private readonly ILogService logService;

	public RxBasePageViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
	{
		Ensure.NotNull(serviceProvider);

		logService = serviceProvider.GetRequiredService<ILogService>();
	}


	public virtual CompositeDisposable OnActivated(CompositeDisposable disposables)
	{
		logService.Log($"{NameViewModel} activated.");
		return disposables;
	}

	public virtual Task OnAppearingAsync()
	{
		logService.Log($"{NameViewModel} appearing.");
		return Task.CompletedTask;
	}

	public virtual Task OnDisappearingAsync()
	{
		logService.Log($"{NameViewModel} disappearing.");
		return Task.CompletedTask;
	}

	protected virtual void Dispatch(Action action, bool secure = false)
	{
		if (secure)
		{
			try
			{
				if (Dispatcher is null)
					action?.Invoke();
				else
				{
					Dispatcher.Dispatch(() =>
					{
						try
						{
							action.Invoke();
						}
						catch (Exception ex)
						{
							logService.Error(ex);
						}
					});
				}
			}
			catch (Exception ex)
			{
				logService.Error(ex);
			}
		}
		else
		{
			if (Dispatcher is null)
				action?.Invoke();
			else
				Dispatcher.Dispatch(action);
		}
	}
}