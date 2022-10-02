namespace RxUI.MauiToolkit.Bases;

using ReactiveUI;
using RxUI.MauiToolkit.Services.AppLog;
using System.Diagnostics;

public abstract class RxBaseViewModel : ReactiveObject
{
	private readonly ILogService? logService;

	public RxBaseViewModel(IServiceProvider? serviceProvider = null)
	{
		this.logService = serviceProvider?.GetService<ILogService>();

		NameViewModel = GetType().Name;

		logService?.Log($"{NameViewModel} created.");
	}

	public IDispatcher? Dispatcher { get; set; }

	protected string NameViewModel { get; }



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
							if (logService is not null)
								logService.Error(ex);
							else
								Debug.WriteLine(ex.Message);
						}
					});
				}
			}
			catch (Exception ex)
			{
				if (logService is not null)
					logService.Error(ex);
				else
					Debug.WriteLine(ex.Message);
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
