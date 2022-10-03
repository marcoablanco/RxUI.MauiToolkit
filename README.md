# RxUI.MauiToolkit

Toolkit genérico usando ReactiveUI con clases base, helpers, servicios y controles típicos en proyectos.

- [RxUI.MauiToolkit](#rxuimauitoolkit)
  - [Inicialización](#inicialización)
  - [Log](#log)
  - [LoadingService](#loadingservice)
  - [Bases](#bases)

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

## Log
Para  que nos lleguen las analíticas a AppCenter, debemos indicarles las keys de la app en `AppCenter`. Este proceso se hace comunmente en el `OnStart()` de `App.xaml.cs`. Para ello, resolvemos `ILogService` en el constructor:

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

// TODO

## Bases

// TODO
