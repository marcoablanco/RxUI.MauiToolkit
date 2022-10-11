[assembly: System.Reflection.Metadata.MetadataUpdateHandlerAttribute(typeof(RxUI.MauiToolkit.Utils.HotReload))]
namespace RxUI.MauiToolkit.Utils;

using System;

public static class HotReload
{
	public static event Action<Type[]?>? UpdateApplicationEvent;

	internal static void ClearCache(Type[]? types) { }
	internal static void UpdateApplication(Type[]? types)
	{
		UpdateApplicationEvent?.Invoke(types);
	}
}