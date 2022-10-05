namespace RxUI.MauiToolkit.Tests.TestsForLogService;

using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using RxUI.MauiToolkit.Services.AppLog;
using System.Collections;

[TestCategory(nameof(LogService<MockGeneric>))]
public class GivenLogservice
{
	[TestClass]
	public class WhenCreated
	{
		[TestMethod]
		public void WithServiceProvider()
		{
			var mockServiceProvider = Substitute.For<IServiceProvider>();
			mockServiceProvider.GetService(typeof(ILogger<MockGeneric>)).Returns(Substitute.For<ILogger<MockGeneric>>());
			LogService<MockGeneric> logService = new LogService<MockGeneric>(mockServiceProvider);

			Assert.IsNotNull(logService);
		}


		[TestMethod]
		public void WithNull()
		{
			LogService<MockGeneric> logService = null;
			Assert.ThrowsException<NullReferenceException>(() => logService = new LogService<MockGeneric>(null));

			Assert.IsNull(logService);
		}
	}

	[TestClass]
	public class WhenLog
	{
	}
}
