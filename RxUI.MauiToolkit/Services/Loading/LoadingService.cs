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
	private SourceList<string> sourceLoadingTasks;
	private ReadOnlyObservableCollection<string> loadingTasks;
	private CompositeDisposable? disposables;

	public LoadingService()
	{
		disposables = new CompositeDisposable();
		sourceLoadingTasks = new SourceList<string>();

		disposables.Add(sourceLoadingTasks.Connect()
										  .ObserveOn(RxApp.MainThreadScheduler)
										  .Bind(out loadingTasks)
										  .Subscribe(_ => this.RaisePropertyChanged(nameof(LoadingTasks))));

		LastLoadingTask = this.WhenAnyValue(s => s.LoadingTasks).Select(s => s.LastOrDefault());
		IsLoading = LastLoadingTask.Select(t => !string.IsNullOrEmpty(t));
		IsNotLoading = IsLoading.Select(x => !x);
	}

	public IObservable<string?> LastLoadingTask { get; }

	public IObservable<bool> IsLoading { get; }

	public IObservable<bool> IsNotLoading { get; }

	public ReadOnlyObservableCollection<string> LoadingTasks => loadingTasks;

	public void Dispose()
	{
		disposables?.Dispose();
		disposables = null;
	}

	public void Add(string loadingTask)
	{
		sourceLoadingTasks.Add(loadingTask);
	}


	public void Remove(string loadingTask)
	{
		sourceLoadingTasks.Remove(loadingTask);
	}

	public void RemoveAll()
	{
		sourceLoadingTasks.Clear();
	}
}
