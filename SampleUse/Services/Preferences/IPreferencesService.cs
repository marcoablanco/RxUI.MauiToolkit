namespace SampleUse.Services.Preferences;

internal interface IPreferencesService
{
	DateTimeOffset? GetDateRefresh();
	void SaveDateRefresh(DateTimeOffset date);
}