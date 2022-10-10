namespace SampleUse.Services.Preferences;
using System.Text.Json;

internal class PreferencesService : IPreferencesService
{
	private readonly IPreferences preferences;
	private const string preferencesKeyDateRefresh = "{F5372202-6DA8-4598-941B-6FD35A23A374}";

	public PreferencesService(IServiceProvider serviceProvider)
	{
		preferences = serviceProvider.GetRequiredService<IPreferences>();
	}

	public DateTimeOffset? GetDateRefresh()
	{
		string result = preferences.Get<string>(preferencesKeyDateRefresh, null, null);
		if (result is null)
			return null;
		return JsonSerializer.Deserialize<DateTimeOffset>(result);
	}

	public void SaveDateRefresh(DateTimeOffset date)
	{
		string result = JsonSerializer.Serialize(date);
		preferences.Set(preferencesKeyDateRefresh, result, null);
	}
}
