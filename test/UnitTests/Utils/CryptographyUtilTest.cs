using ApiBase.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiBaseTest.UnitTests.Utils
{
    [TestClass]
    public class CryptographyUtilTest
    {
        [TestMethod]
        public void TestGenerateHash()
        {
            string result = CryptographyUtil.GenerateHash("123456");

            Assert.IsNotNull(result);
        }
    }
}
