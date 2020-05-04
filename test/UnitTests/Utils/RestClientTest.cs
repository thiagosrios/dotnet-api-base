using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ApiBase.Utils;
using ApiBaseTest.Utilities;
using ApiBase.Exceptions;
using ApiBaseTest.Utilities.Mocks;

namespace ApiBaseTest.UnitTests.Utils
{
    [TestClass]
    public class RestClientTest
    {
        private string BaseAddress { get; set; }

        [TestInitialize]
        public void Setup()
        {
            IConfiguration config = ConfigurationMock.Setup();
            this.BaseAddress = config.GetSection("Host").Value;
        }

        [TestMethod]
        public void TestCreateHttpClient()
        {
            RestClient client = new RestClient(this.BaseAddress);

            Assert.IsInstanceOfType(client.GetType(), typeof(RestClient).GetType());
            Assert.IsInstanceOfType(client.HttpClient.GetType(), typeof(HttpClient).GetType());
            Assert.That.IsNotNullAndIsString(client.BaseAddress);
            client.Dispose();
        }

        [TestMethod]
        public void TestCreateHttpClientExcecao()
        {
            IConfiguration config = ConfigurationMock.SetupForFailure();
            this.BaseAddress = config.GetSection("Host").Value;
            BusinessException exception = Assert.ThrowsException<BusinessException>(() => 
                {
                    RestClient client = new RestClient(this.BaseAddress);
                }
            );

            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception.GetType(), typeof(BusinessException).GetType());
        }

        [TestMethod]
        public void TestCreateHttpClientExcecaoConfiguracao()
        {
            // Reconfigura service para gerar exceção
            BusinessException exception = Assert.ThrowsException<BusinessException>(() =>
                {
                    RestClient client = new RestClient("");
                }
            );

            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception.GetType(), typeof(BusinessException).GetType());
        }

        [TestMethod]
        public async Task TestPost()
        {
            // Cria mock de HttpClient, para substituição na classe RestClient
            var retornoRequest = new { Cstat = 100 }; 
            string responseBody = JsonConvert.SerializeObject(retornoRequest);
            HttpClient http = HttpClientMock.Setup(this.BaseAddress, responseBody, HttpStatusCode.OK);

            // Cria nova instância da classe RestClient e substitui dependência da classe HttpClient pelo mock
            RestClient client = new RestClient(this.BaseAddress);
            client.HttpClient = http;

            var request = new { token = "45646546" };
            string requestBody = JsonConvert.SerializeObject(request);
            string response = await client.Post("ValidarToken", requestBody);

            Assert.That.IsNotNullAndIsString(response);
        }

        [TestMethod]
        public void TestPostExcecao()
        {
            RestClient client = new RestClient(this.BaseAddress);
            Task<string> response = client.Post(null, null);
            AggregateException exception = Assert.ThrowsException<AggregateException>(
                () => response.Result
            );

            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception.GetType(), typeof(AggregateException).GetType());
        }
    }
}
