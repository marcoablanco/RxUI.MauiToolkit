[assembly: System.Reflection.Metadata.MetadataUpdateHandler(typeof(SampleUse.Utils.HotReload))]
namespace SampleUse.Utils;

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