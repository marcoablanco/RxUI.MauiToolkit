namespace RxUI.MauiToolkit.Bases;

using ReactiveUI;
using RxUI.MauiToolkit.Services.AppLog;
using RxUI.MauiToolkit.Utils;
using System.Diagnostics;

public abstract class RxBaseViewModel : ReactiveObject
{
	public RxBaseViewModel(IServiceProvider serviceProvider)
	{
		Ensure.NotNull(serviceProvider);

		NameViewModel = GetType().Name;
	}

	public string NameViewModel { get; }


}
