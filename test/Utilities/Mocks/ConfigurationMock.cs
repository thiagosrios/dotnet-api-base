using Microsoft.Extensions.Configuration;
using Moq;

namespace ApiBaseTest.Utilities.Mocks
{
    public static class ConfigurationMock
    {
        public static IConfigurationSection GetNewSection(string returnValue)
        {
            Mock<IConfigurationSection> section = new Mock<IConfigurationSection>();
            section.Setup(a => a.Value).Returns(returnValue);

            return section.Object;
        }

        public static IConfiguration Setup()
        {
            Mock<IConfiguration> configuration = new Mock<IConfiguration>();

            configuration
                .Setup(x => x.GetSection("Host"))
                .Returns(GetNewSection("https://127.0.0.1"));

            return configuration.Object;
        }

        public static IConfiguration SetupForFailure()
        {
            Mock<IConfiguration> configuration = new Mock<IConfiguration>();
            Mock<IConfigurationSection> hostConfig = new Mock<IConfigurationSection>();

            hostConfig.Setup(a => a.Value).Returns("ftp://10.258.258.0");

            configuration
                .Setup(x => x.GetSection("Host"))
                .Returns(hostConfig.Object);

            return configuration.Object;
        }
    }
}
