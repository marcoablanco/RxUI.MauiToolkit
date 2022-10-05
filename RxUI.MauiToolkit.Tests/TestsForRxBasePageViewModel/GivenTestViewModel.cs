namespace RxUI.MauiToolkit.Tests.Bases;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using RxUI.MauiToolkit.Bases;
using RxUI.MauiToolkit.Services.AppLog;
using RxUI.MauiToolkit.Tests.TestsForRxBasePageViewModel;
using System.Reactive.Disposables;

public class GivenTestViewModel
{
	[TestClass]
	public class WhenCreated
	{
		[TestMethod]
		public void WithServiceProvider()
		{
			var mockServiceProvider = Substitute.For<IServiceProvider>();
			mockServiceProvider.GetService(typeof(ILogService)).Returns(Substitute.For<ILogService>());
			RxBasePageViewModel viewModel = new MockViewModel(mockServiceProvider);

			Assert.IsNotNull(viewModel);
		}


		[TestMethod]
		public void WithNull()
		{
			RxBasePageViewModel viewModel = null;
			Assert.ThrowsException<NullReferenceException>(() => viewModel = new MockViewModel(null));

			Assert.IsNull(viewModel);
		}
	}

	[TestClass]
	public class WhenAppearing
	{
		[TestMethod]
		public async Task ThenLogCalled()
		{
			IServiceProvider mockServiceProvider = Substitute.For<IServiceProvider>();
			ILogService mockLogService = Substitute.For<ILogService>();
			mockServiceProvider.GetService(typeof(ILogService)).Returns(mockLogService);
			RxBasePageViewModel viewModel = new MockViewModel(mockServiceProvider);

			await viewModel.OnAppearingAsync();

			mockLogService.Received(1).Log(Arg.Any<string>());
		}
	}

	[TestClass]
	public class WhenDisappearing
	{
		[TestMethod]
		public async Task ThenLogCalled()
		{
			IServiceProvider mockServiceProvider = Substitute.For<IServiceProvider>();
			ILogService mockLogService = Substitute.For<ILogService>();
			mockServiceProvider.GetService(typeof(ILogService)).Returns(mockLogService);
			RxBasePageViewModel viewModel = new MockViewModel(mockServiceProvider);

			await viewModel.OnDisappearingAsync();

			mockLogService.Received(1).Log(Arg.Any<string>());
		}
	}

	[TestClass]
	public class WhenActivated
	{
		[TestMethod]
		public void ThenLogCalled()
		{
			IServiceProvider mockServiceProvider = Substitute.For<IServiceProvider>();
			ILogService mockLogService = Substitute.For<ILogService>();
			mockServiceProvider.GetService(typeof(ILogService)).Returns(mockLogService);
			RxBasePageViewModel viewModel = new MockViewModel(mockServiceProvider);

			viewModel.OnActivated(new CompositeDisposable());

			mockLogService.Received(1).Log(Arg.Any<string>());
		}

		[TestMethod]
		public void ThenCompositeDisposablePreserveOwnDisposes()
		{
			IServiceProvider mockServiceProvider = Substitute.For<IServiceProvider>();
			ILogService mockLogService = Substitute.For<ILogService>();
			mockServiceProvider.GetService(typeof(ILogService)).Returns(mockLogService);
			RxBasePageViewModel viewModel = new MockViewModel(mockServiceProvider);

			var disposable = Disposable.Create(() => throw new Exception("TEST"));
			CompositeDisposable disposables = new CompositeDisposable();
			disposables.Add(disposables);

			var result = viewModel.OnActivated(disposables);

			Assert.AreEqual(disposables, result);
		}
	}
}