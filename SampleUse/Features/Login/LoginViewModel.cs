namespace SampleUse.Features.Login;

using ReactiveUI;
using RxUI.MauiToolkit.Bases;
using RxUI.MauiToolkit.Services.AppLog;
using RxUI.MauiToolkit.Services.Loading;
using RxUI.MauiToolkit.Utils;
using SampleUse.Features.Main;
using SampleUse.Services.Authentication;
using SampleUse.Services.Persistance;
using System;
using System.Reactive;
using System.Reactive.Disposables;

public class LoginViewModel : RxBasePageViewModel
{
	private readonly IAuthenticationService authenticationService;
	private readonly ILoadingService loadingService;
	private readonly ILogService logService;
	private readonly IPersistanceService persistanceService;
	private readonly IServiceProvider serviceProvider;
	private string user;
	private string password;

	public LoginViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
	{
		authenticationService = serviceProvider.GetRequiredService<IAuthenticationService>();
		logService = serviceProvider.GetRequiredService<ILogService<LoginViewModel>>();
		loadingService = serviceProvider.GetRequiredService<ILoadingService<LoginViewModel>>();
		persistanceService = serviceProvider.GetRequiredService<IPersistanceService>();

		user = string.Empty;
		password = string.Empty;

		LoginCommand = ReactiveCommand.CreateFromTask(LoginCommandExecuteAsync);
		this.serviceProvider = serviceProvider;
	}

	public string User
	{
		get => user;
		set => this.RaiseAndSetIfChanged(ref user, value);
	}

	public string Password
	{
		get => password;
		set => this.RaiseAndSetIfChanged(ref password, value);
	}

	public ReactiveCommand<Unit, Unit> LoginCommand { get; }

	public override CompositeDisposable OnActivated(CompositeDisposable disposables)
	{
		base.OnActivated(disposables);

		disposables.Add(LoginCommand.ThrownExceptions.Subscribe(logService.Error));

		return disposables;
	}

	private async Task LoginCommandExecuteAsync()
	{
		try
		{
			loadingService.Add(Text.LoadingLogin);
			Ensure.NotNullOrEmpty(User, nameof(User));
			Ensure.NotNullOrEmpty(Password, nameof(Password));

			Token token = await authenticationService.GetTokenAsync(User, Password);
			if (token is null)
			{
				// TODO Show Alert with incorrect login.
			}
			else
			{
				await persistanceService.SaveTokenAsync(token);
				var mainPage = serviceProvider.GetRequiredService<MainShell>();
				Application.Current.MainPage = mainPage;
			} 

		}
		catch (Exception ex)
		{
			logService.Warning(ex);
			// TODO Show Alert with incorrect login.
		}
		finally
		{
			loadingService.Remove(Text.LoadingLogin);
		}

	}
}
