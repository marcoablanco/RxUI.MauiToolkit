namespace SampleUse.Configuration;
internal static class MauiConfiguration
{
	public static MauiAppBuilder ConfigureApp(this MauiAppBuilder builder)
	{
		builder.UseMauiApp<App>()
			   .ConfigureFonts(fonts =>
			   {
				   fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				   fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			   });
		return builder;
	}
}
