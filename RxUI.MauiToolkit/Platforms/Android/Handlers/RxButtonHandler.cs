namespace RxUI.MauiToolkit.Platforms.Android.Handlers;

using Google.Android.Material.Button;
using Microsoft.Maui.Handlers;
using RxUI.MauiToolkit.Controls;

internal class RxButtonHandler : ButtonHandler
{
	protected override void ConnectHandler(MaterialButton platformView)
	{
		base.ConnectHandler(platformView);

		// Initialize properties
		OnTextAlignmentPropertyChanged();
	}

	public override void UpdateValue(string property)
	{
		base.UpdateValue(property);
		if (property == RxButton.HorizontalTextAlignmentProperty.PropertyName)
			OnTextAlignmentPropertyChanged();
	}

	private void OnTextAlignmentPropertyChanged()
	{
		if (VirtualView is RxButton virtualButton)
		{
			global::Android.Views.GravityFlags horizontalFlag = virtualButton.HorizontalTextAlignment switch
			{
				TextAlignment.Start => global::Android.Views.GravityFlags.Left,
				TextAlignment.Center => global::Android.Views.GravityFlags.CenterHorizontal,
				TextAlignment.End => global::Android.Views.GravityFlags.Right,
				_ => global::Android.Views.GravityFlags.Center
			};
			global::Android.Views.GravityFlags verticalFlag = virtualButton.VerticalTextAlignment switch
			{
				TextAlignment.Start => global::Android.Views.GravityFlags.Top,
				TextAlignment.Center => global::Android.Views.GravityFlags.CenterVertical,
				TextAlignment.End => global::Android.Views.GravityFlags.Bottom,
				_ => global::Android.Views.GravityFlags.Center
			};
			PlatformView.Gravity = horizontalFlag | verticalFlag;
		}
	}
}
