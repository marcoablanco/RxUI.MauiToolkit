# RxUI.MauiToolkit

Toolkit genérico usando ReactiveUI con clases base, helpers, servicios y controles típicos en proyectos.


 - Main: [![Build Status](https://marcoantonioblanco.visualstudio.com/RxUI.MauiToolkit/_apis/build/status/marcoablanco.RxUI.MauiToolkit?branchName=main)](https://marcoantonioblanco.visualstudio.com/RxUI.MauiToolkit/_build/latest?definitionId=2&branchName=main)
 - Develop: [![Build Status](https://marcoantonioblanco.visualstudio.com/RxUI.MauiToolkit/_apis/build/status/Develop%20CI?branchName=develop)](https://marcoantonioblanco.visualstudio.com/RxUI.MauiToolkit/_build/latest?definitionId=3&branchName=develop)

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
