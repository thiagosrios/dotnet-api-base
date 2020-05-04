using Moq;
using System;
using System.Net;
using System.Net.Http;

namespace ApiBaseTest.Utilities.Mocks
{
    public class HttpClientMock
    {
        /// <summary>
        /// Retorna mock de HttpClient para substituição nas classes a utilizam. 
        /// Utiliza um fakeHandler para permitir o override dos métodos de envio de requições
        /// </summary>
        /// <param name="baseAddress">Endereço de base para acesso do serviço</param>
        /// <param name="responseBody">String contendo o corpo da mensagem serializada que deve ser retornada</param>
        /// <param name="responseStatus">Status code que deve ser retornado</param>
        /// <returns>Objeto HttpClient</returns>
        public static HttpClient Setup(string baseAddress, string responseBody, HttpStatusCode responseStatus)
        {
            // Configura mock do HttpMessageHandler
            Mock<FakeHttpMessageHandler> fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };

            // Configura response message a partir dos parâmetros enviados
            HttpResponseMessage responseMessage = new HttpResponseMessage()
            {
                Content = new StringContent(responseBody),
                StatusCode = responseStatus
            };

            // Configura métodos que devem ser sobrescritos
            fakeHttpMessageHandler.Setup(x => x.Send(It.IsAny<HttpRequestMessage>())).Returns(responseMessage);

            // Cria nova instância do HttpClient usando o fakeHandler
            HttpClient fakeHttpClient = new HttpClient(fakeHttpMessageHandler.Object)
            {
                BaseAddress = new Uri(baseAddress)
            };

            return fakeHttpClient;
        }
    }
}