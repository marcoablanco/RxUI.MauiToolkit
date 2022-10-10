namespace SampleUse.Features.Login;

using Microsoft.Extensions.DependencyInjection;
using RxUI.MauiToolkit.Utils;
using SampleUse.Services.Authentication;
using SampleUse.Services.Persistance;
using SampleUse.Services.Preferences;
using System;
using System.Threading.Tasks;

internal class LoginService : ILoginService
{
	private readonly IAuthenticationService authenticationService;
	private readonly IPersistanceService persistanceService;
	private readonly IPreferencesService preferencesService;
	private readonly IServiceProvider serviceProvider;

	public LoginService(IServiceProvider serviceProvider)
	{
		this.serviceProvider = serviceProvider;
		authenticationService = serviceProvider.GetRequiredService<IAuthenticationService>();
		persistanceService = serviceProvider.GetRequiredService<IPersistanceService>();
		preferencesService = serviceProvider.GetRequiredService<IPreferencesService>();
	}

	public async Task AutoLoginAsync()
	{
		Token? token = await persistanceService.GetTokenAsync();
		Ensure.NotNull(token, nameof(token));

		token = await authenticationService.RefreshTokenAsync(token.RefreshToken);

		Ensure.NotNull(token, nameof(token));
		await persistanceService.SaveTokenAsync(token);
		preferencesService.SaveDateRefresh(token.RefreshValidAt);
	}

	public async Task LoginAsync(string user, string password)
	{
		Ensure.NotNullOrEmpty(user, nameof(user));
		Ensure.NotNullOrEmpty(password, nameof(password));

		Token token = await authenticationService.GetTokenAsync(user, password);

		Ensure.NotNull(token, nameof(token));
		await persistanceService.SaveTokenAsync(token);
		preferencesService.SaveDateRefresh(token.RefreshValidAt);
	}
}
