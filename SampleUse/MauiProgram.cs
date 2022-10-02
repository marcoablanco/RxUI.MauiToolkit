﻿namespace SampleUse;

using RxUI.MauiToolkit.Configuration;
using SampleUse.Configuration;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder()
							 .InitRxToolkit()
							 .ConfigureApp()
							 .AddDependencies();
		

		return builder.Build();
	}
}
