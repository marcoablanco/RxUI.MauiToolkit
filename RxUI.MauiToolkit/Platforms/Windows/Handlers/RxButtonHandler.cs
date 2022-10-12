namespace RxUI.MauiToolkit.Platforms.Windows.Handlers;

using Microsoft.Maui.Handlers;
using Microsoft.UI.Xaml.Controls;
using RxUI.MauiToolkit.Controls;

internal class RxButtonHandler : ButtonHandler
{
	protected override void ConnectHandler(Button platformView)
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
			PlatformView.HorizontalContentAlignment = virtualButton.HorizontalTextAlignment switch
			{
				TextAlignment.Start => Microsoft.UI.Xaml.HorizontalAlignment.Left,
				TextAlignment.Center => Microsoft.UI.Xaml.HorizontalAlignment.Center,
				TextAlignment.End => Microsoft.UI.Xaml.HorizontalAlignment.Right,
				_ => Microsoft.UI.Xaml.HorizontalAlignment.Center,
			};
			PlatformView.VerticalContentAlignment = virtualButton.HorizontalTextAlignment switch
			{
				TextAlignment.Start => Microsoft.UI.Xaml.VerticalAlignment.Top,
				TextAlignment.Center => Microsoft.UI.Xaml.VerticalAlignment.Center,
				TextAlignment.End => Microsoft.UI.Xaml.VerticalAlignment.Bottom,
				_ => Microsoft.UI.Xaml.VerticalAlignment.Center,
			};
		}
	}
}
