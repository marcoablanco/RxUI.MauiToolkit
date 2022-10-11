namespace RxUI.MauiToolkit.Session;

using RxUI.MauiToolkit.Utils;

public sealed class AppSession : IDisposable
{
	private static AppSession? appSession;
	private IServiceScope serviceScope;
	private IServiceProvider serviceProvider;

	private AppSession(IServiceScope serviceScope, string user)
	{
		this.serviceScope = serviceScope;
		serviceProvider = serviceScope.ServiceProvider;
		User = user;
	}

	public static AppSession Current => appSession ?? throw new InvalidOperationException("Session not initialized");
	public string User { get; private set; }

	public static void CreateSession(IServiceProvider rootServiceProvider, string? user = null)
	{
		Ensure.NotNull(rootServiceProvider, "IServiceProvider can't be null");

		var serviceScope = rootServiceProvider.CreateScope();
		appSession = new AppSession(serviceScope, user ?? string.Empty);
	}

	public static void CloseSession()
	{
		appSession?.Dispose();
		appSession = null;
	}

	public T Get<T>() where T : notnull
	{
		return serviceProvider.GetRequiredService<T>();
	}

	public void Dispose()
	{
		// I don't care nullables when the object is disposed, bitch.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
		serviceScope?.Dispose();
		serviceScope = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
	}
}
