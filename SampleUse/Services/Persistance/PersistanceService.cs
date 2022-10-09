namespace SampleUse.Services.Persistance;

using RxUI.MauiToolkit.Utils;
using SampleUse.Services.Authentication;
using System.Text.Json;
using System.Threading.Tasks;

internal class PersistanceService : IPersistanceService
{
	private readonly ISecureStorage secureStorage;

	public PersistanceService(IServiceProvider serviceProvider)
	{
		secureStorage = serviceProvider.GetRequiredService<ISecureStorage>();
	}

	public async Task<Token?> GetTokenAsync()
	{
		return await GetAsync<Token>(StorageKeys.Token);
	}

	public async Task SaveTokenAsync(Token token)
	{
		await SaveAsync(StorageKeys.Token, token);
	}


	private async Task SaveAsync<T>(string key, T value)
	{
		Ensure.NotNullOrEmpty(key);
		Ensure.NotNull(value, nameof(value));

		string result = JsonSerializer.Serialize(value);
		await secureStorage.SetAsync(key, result);
	}

	private async Task<T?> GetAsync<T>(string key)
	{
		Ensure.NotNullOrEmpty(key, nameof(key));

		string result = await secureStorage.GetAsync(key);
		if (string.IsNullOrEmpty(result))
			return default;
		return JsonSerializer.Deserialize<T>(result);
	}
}
