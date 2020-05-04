using Microsoft.AspNetCore.Hosting;
using Moq;

namespace ApiBaseTest.Utilities.Mocks
{
    public static class HostingEnviromentMock
    {
        public static IWebHostEnvironment Setup()
        {
            Mock<IWebHostEnvironment> hosting = new Mock<IWebHostEnvironment>();

            hosting.Setup(x => x.ApplicationName).Returns("ApiBase");
            hosting.Setup(x => x.ContentRootPath).Returns("../../../../api/");
            hosting.Setup(x => x.EnvironmentName).Returns("Test");

            return hosting.Object;
        }

        public static IWebHostEnvironment SetupForFailure()
        {
            Mock<IWebHostEnvironment> hosting = new Mock<IWebHostEnvironment>();

            hosting.Setup(x => x.ApplicationName).Returns("ApiBase");
            hosting.Setup(x => x.ContentRootPath).Returns("");
            hosting.Setup(x => x.EnvironmentName).Returns("Test");

            return hosting.Object;
        }

        public static IWebHostEnvironment SetupTest()
        {
            Mock<IWebHostEnvironment> hosting = new Mock<IWebHostEnvironment>();

            hosting.Setup(x => x.ApplicationName).Returns("ApiBase");
            hosting.Setup(x => x.ContentRootPath).Returns("../../../../test/");
            hosting.Setup(x => x.EnvironmentName).Returns("Test");

            return hosting.Object;
        }
    }
}
