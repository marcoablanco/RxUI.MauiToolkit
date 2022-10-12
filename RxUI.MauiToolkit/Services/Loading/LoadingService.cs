namespace RxUI.MauiToolkit.Services.Loading;

using DynamicData;
using ReactiveUI;
using RxUI.MauiToolkit.Utils;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

public sealed class LoadingService<TCategory> : ReactiveObject, ILoadingService<TCategory>
{
	private List<string> loadingTasks;
	private CompositeDisposable? disposables;
	private string lastLoadingTask;

	public LoadingService(IServiceProvider serviceProvider)
	{
		loadingTasks = new List<string>();
		disposables = new CompositeDisposable();

		lastLoadingTask = string.Empty;
		IsLoading = this.WhenAnyValue(s => s.LastLoadingTask).Select(t => !string.IsNullOrEmpty(t));
		IsNotLoading = IsLoading.Select(x => !x);

		if (typeof(TCategory) != typeof(Generic))
		{
			ILoadingService loadingService = serviceProvider.GetRequiredService<ILoadingService>();
			disposables.Add(loadingService.WhenAnyValue(s => s.LastLoadingTask).Subscribe(t => LastLoadingTask = t));
		}
	}

	public string LastLoadingTask
	{
		get => lastLoadingTask;
		set => this.RaiseAndSetIfChanged(ref lastLoadingTask, value);
	}

	public IObservable<bool> IsLoading { get; }

	public IObservable<bool> IsNotLoading { get; }

	public void Dispose()
	{
		disposables?.Dispose();
		disposables = null;
	}

	public void Add(string loadingTask)
	{
		loadingTasks.Add(loadingTask);
		LastLoadingTask = loadingTask;
	}


	public void Remove(string loadingTask)
	{
		loadingTasks.Remove(loadingTask);
		if (loadingTask == LastLoadingTask)
		{
			if (loadingTasks.Any())
				LastLoadingTask = loadingTasks.LastOrDefault(string.Empty);
			else
				LastLoadingTask = string.Empty;
		}
	}

	public void RemoveAll()
	{
		loadingTasks.Clear();
		LastLoadingTask = string.Empty;
	}
}
