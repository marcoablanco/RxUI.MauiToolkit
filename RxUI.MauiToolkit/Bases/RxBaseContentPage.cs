namespace RxUI.MauiToolkit.Bases;

using RxUI.MauiToolkit.Services.AppLog;
using RxUI.MauiToolkit.Services.Loading;
using ReactiveUI;
using ReactiveUI.Maui;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

public abstract class RxBaseContentPage<TViewModel> : ReactiveContentPage<TViewModel> where TViewModel : RxBasePageViewModel
{
	private static readonly BindableProperty ActivatedProperty = BindableProperty.Create(nameof(Activated), typeof(bool), typeof(RxBaseContentPage<TViewModel>));
	private static readonly BindableProperty AppearedProperty = BindableProperty.Create(nameof(Appeared), typeof(bool), typeof(RxBaseContentPage<TViewModel>));

	private readonly ILogService logService;
	private readonly ILoadingService loadingService;

	public RxBaseContentPage(IServiceProvider serviceProvider)
	{
		logService = serviceProvider.GetRequiredService<ILogService<TViewModel>>();
		loadingService = serviceProvider.GetRequiredService<ILoadingService<TViewModel>>();

		ViewModel = serviceProvider.GetRequiredService<TViewModel>();
		ViewModel.Dispatcher = Dispatcher;

		this.WhenActivated(d => OnActivated(d));
	}



	public bool Activated
	{
		get => (bool)GetValue(ActivatedProperty);
		set => SetValue(ActivatedProperty, value);
	}

	public bool Appeared
	{
		get => (bool)GetValue(AppearedProperty);
		set => SetValue(AppearedProperty, value);
	}


	protected override void OnAppearing()
	{
		base.OnAppearing();

		ViewModel?.OnAppearingAsync();
		Appeared = true;
	}

	protected override void OnDisappearing()
	{
		Appeared = false;
		ViewModel?.OnDisappearingAsync();

		base.OnDisappearing();
	}

	protected virtual CompositeDisposable OnActivated(CompositeDisposable disposables)
	{
		if (Activated || ViewModel is null)
			return disposables;
		Activated = true;

		if (loadingService is not null)
			SetBindingIsBusy(loadingService);

		ViewModel.OnActivated(disposables);

		return disposables;
	}

	protected IDisposable SetBindingIsBusy(ILoadingService loadingService)
	{
		return loadingService.IsLoading
							 .Do(isLoading => Dispatcher.Dispatch(() => IsBusy = isLoading), ex=> logService?.Error(ex))
							 .Catch<bool, Exception>(ex =>
							 {
								 if (logService is not null)
									 logService.Error(ex);
								 else
									 Debug.WriteLine(ex.Message);
								 return loadingService.IsLoading;
							 })
							 .Subscribe();
	}
}
