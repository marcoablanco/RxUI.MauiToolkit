namespace RxUI.MauiToolkit.Controls;

using ReactiveUI;
using RxUI.MauiToolkit.Services.Loading;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public partial class AnimatedLoadingControl
{
	public static readonly BindableProperty BackgroundShapeBrushProperty = BindableProperty.Create(nameof(BackgroundShapeBrush), typeof(Brush), typeof(AnimatedLoadingControl), Brush.LightSlateGray);
	public static readonly BindableProperty BackgroundLabelBrushProperty = BindableProperty.Create(nameof(BackgroundLabelBrush), typeof(Brush), typeof(AnimatedLoadingControl), Brush.Transparent);
	public static readonly BindableProperty BackgroundShapeOpacityProperty = BindableProperty.Create(nameof(BackgroundShapeOpacity), typeof(double), typeof(AnimatedLoadingControl), 0.8);
	public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(AnimatedLoadingControl), string.Empty);

	public AnimatedLoadingControl()
	{
		InitializeComponent();
	}

	public bool IsLoading => !string.IsNullOrEmpty(Text);

	public Brush BackgroundLabelBrush
	{
		get => (Brush)GetValue(BackgroundLabelBrushProperty);
		set => SetValue(BackgroundLabelBrushProperty, value);
	}

	public Brush BackgroundShapeBrush
	{
		get => (Brush)GetValue(BackgroundShapeBrushProperty);
		set => SetValue(BackgroundShapeBrushProperty, value);
	}

	public double BackgroundShapeOpacity
	{
		get => (double)GetValue(BackgroundShapeOpacityProperty);
		set => SetValue(BackgroundShapeOpacityProperty, value);
	}

	public string Text
	{
		get => (string)GetValue(TextProperty);
		set => SetValue(TextProperty, value);
	}

	public IDisposable BindLoadingService(ILoadingService loadingService)
	{
		return loadingService.WhenAnyValue(s => s.LastLoadingTask).Subscribe(text => Text = text);
	}


	protected override async void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		base.OnPropertyChanged(propertyName);

		if (propertyName == TextProperty.PropertyName)
			OnTextPropertyChanged();
		else if (propertyName == BackgroundLabelBrushProperty.PropertyName)
			LblText.Background = BackgroundLabelBrush;
		else if (propertyName == BackgroundShapeBrushProperty.PropertyName)
			ShapeBackground.Fill = BackgroundShapeBrush;
		else if (propertyName == BackgroundShapeOpacityProperty.PropertyName)
			await OnBackgroundShapeOpacityPropertyChangedAsync();
	}

	private async Task OnBackgroundShapeOpacityPropertyChangedAsync()
	{
		if (IsLoading)
			await ShapeBackground.FadeTo(BackgroundShapeOpacity);
	}

	private void OnTextPropertyChanged()
	{
		if (!IsLoading)
		{
			double movement = Width;
			Animation animation = new Animation
			{
				{ 0, 1, new Animation(x => Opacity = x, Opacity, 0) },
				{ 0, 1, new Animation(x => LblText.TranslationX = x, 0, -movement) },
				{ 0, 1, new Animation(x => LblText.Opacity = x, 1, 0, finished: () => LblText.Text = Text) },
			};
			animation.Commit(this, nameof(AnimatedLoadingControl), finished: (d, b) => IsVisible = false);
		}
		else
		{
			IsVisible = true;
			double movement = Width;
			Animation animation = new Animation
			{
				{ 0, 0.2, new Animation(x => Opacity = x, Opacity, BackgroundShapeOpacity) },
				{ 0, 0.5, new Animation(x => LblText.TranslationX = x, 0, -movement) },
				{ 0, 0.5, new Animation(x => LblText.Opacity = x, 1, 0, finished: () => LblText.Text = Text) },
				{ 0.5, 1, new Animation(x => LblText.TranslationX = x, movement, 0) },
				{ 0, 0.5, new Animation(x => LblText.Opacity = x, 0, 1) },
			};
			animation.Commit(this, nameof(AnimatedLoadingControl), length: 500);
		}
	}
}