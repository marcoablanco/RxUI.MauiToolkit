namespace RxUI.MauiToolkit.Tests.TestsForLoadingService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using RxUI.MauiToolkit.Services.Loading;


[TestCategory(nameof(LoadingService<MockGeneric>))]
public class GivenLoadingService
{
	[TestClass]
	public class WhenCreated
	{
		[TestMethod]
		public void ThenPropertiesAreNotNull()
		{
			var mockServiceProvider = Substitute.For<IServiceProvider>();
			mockServiceProvider.GetService(typeof(ILoadingService)).Returns(Substitute.For<ILoadingService>());
			LoadingService<MockGeneric> loadingservice = new LoadingService<MockGeneric>(mockServiceProvider);

			Assert.IsNotNull(loadingservice);
			Assert.IsNotNull(loadingservice.LastLoadingTask);
			Assert.IsNotNull(loadingservice.IsLoading);
			Assert.IsNotNull(loadingservice.IsNotLoading);
		}
	}

	[TestClass]
	public class WhenAdd
	{
		// TODO Fill
	}

	[TestClass]
	public class WhenRemove
	{
		// TODO Fill
	}

	[TestClass]
	public class WhenRemoveAll
	{
		// TODO Fill
	}
}
