# RxUI.MauiToolkit

Toolkit genérico usando ReactiveUI con clases base, helpers, servicios y controles típicos en proyectos.

- [RxUI.MauiToolkit](#rxuimauitoolkit)
	- [Inicialización](#inicialización)
	- [ViewModels](#viewmodels)
	- [Log](#log)
	- [LoadingService](#loadingservice)

 [![Build Status](https://marcoantonioblanco.visualstudio.com/RxUI.MauiToolkit/_apis/build/status/marcoablanco.RxUI.MauiToolkit?branchName=main)](https://marcoantonioblanco.visualstudio.com/RxUI.MauiToolkit/_build/latest?definitionId=2&branchName=main)

## Inicialización
Para inicializar el paquete usamos `.InitRxToolkit()`, que encontraremos en el namespace `RxUI.MauiToolkit.Configuration`, como método de extensión de `MauiAppBuilder` cuando creamos nuestra `MauiApp`:

```csharp
public static MauiApp CreateMauiApp()
{
	var builder = MauiApp.CreateBuilder()
						 .UseMauiApp<App>()
						 .InitRxToolkit();
	...

	return builder.Build();
}
```

Esto añadirá los target `Debug` y `Console` al servicio de Log, así como la inicialización del resto de servicios del Nuget.

## ViewModels

Este NuGet está a favor de evitar el uso de `Reflection` en la inyección de dependencias (siempre que sea posible). Para facilitar su construcción en el `Func<IServiceProvider, TService> implementationFactory`, los constructores de `RxBaseViewModel`, `RxBasePageViewModel` y `RxBaseContentPage<TViewModel>` no pide por inyección distintos servicios, sino el `IServiceProvider` para poder resolverlos él evitando la construcción por tipos.

Por ejemplo:
- Registro de nuestro `ViewModel`:
```csharp
builder.Services.AddTransient(s => new LoginViewModel(s));
```
- Constructor de nuestro `ViewModel`:
```csharp
public LoginViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
{
}
```

> El uso de `RxBaseContentPage<TViewModel>` exige el registro del `ViewModel` en el `IServiceProvider`.


## Log
Para que nos lleguen las analíticas a AppCenter, debemos indicarles las keys de la app en `AppCenter`. Este proceso se hace comunmente en el `OnStart()` de `App.xaml.cs`. Para ello, resolvemos `ILogService` en el constructor:

```csharp
public partial class App : Application
{
	private readonly ILogService logService;

	public App(ILogService logService)
	{
		this.logService = logService;
		...
	}
```

Y sobreescribimos el método `OnStart()` indicando nuestras claves:
```csharp
protected override void OnStart()
{
	base.OnStart();
	logService.SetAppCenterId("[iosId]", "[androidId]", "[uwpId]", "[macosId]"); ;
}
```


Como ya se ha comentado, el servicio de log ya configura los target `Debug` y `Console`, pero puede añadir otros mediante los NuGets de `Microsoft.Extensions.Logging`

## LoadingService

El servicio de loading está diseñado para gestionar las tareas de forma independiente en cada ViewModel. Se recomienda resolver el servicio bajo la categoría del ViewModel tanto en la View, como en el ViewModel. Constructores de ejemplo:

```csharp
public LoginViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
{
	loadingService = serviceProvider.GetRequiredService<ILoadingService<LoginViewModel>>();
}
```
```csharp
public LoginPage(IServiceProvider serviceProvider) : base(serviceProvider)
{
	loadingService = serviceProvider.GetRequiredService<ILoadingService<LoginViewModel>>();

	InitializeComponent();
}
```

De esta forma ambos estaría usando la misma instancia del servicio, pero se crearían nuevas para distintas pantallas o controles. Esta misma resolución puede hacerse con `ILoadingService<TCategory>`.
