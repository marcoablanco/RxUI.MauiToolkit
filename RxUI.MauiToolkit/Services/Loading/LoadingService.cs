namespace RxUI.MauiToolkit.Services.Loading;

using DynamicData;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

public class LoadingService<TCategory> : ReactiveObject, ILoadingService<TCategory>
{
	private List<string> loadingTasks;
	private CompositeDisposable? disposables;
	private string lastLoadingTask;

	public LoadingService()
	{
		loadingTasks = new List<string>();

		lastLoadingTask = string.Empty;
		IsLoading = this.WhenAnyValue(s => s.LastLoadingTask).Select(t => !string.IsNullOrEmpty(t));
		IsNotLoading = IsLoading.Select(x => !x);
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
