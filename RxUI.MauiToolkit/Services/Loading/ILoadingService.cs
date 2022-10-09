namespace RxUI.MauiToolkit.Services.Loading;


public interface ILoadingService : IDisposable
{
	string LastLoadingTask { get; }
	IObservable<bool> IsLoading { get; }
	IObservable<bool> IsNotLoading { get; }

	void Add(string loadingTask);
	void Remove(string loadingTask);
	void RemoveAll();
}
public interface ILoadingService<TCategory> : ILoadingService { }