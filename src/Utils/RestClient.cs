using ApiBase.Exceptions;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace ApiBase.Utils
{
    public class RestClient : IDisposable
    {
        // Instância de HTTPClient para comunicação com o serviço
        public HttpClient HttpClient { get; set; }

        // Endereço base usado para consumo do serviço REST
        public string BaseAddress { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="baseAddress">Endereço base para requisição do serviço</param>
        public RestClient(string baseAddress)
        {
            this.BaseAddress = baseAddress;
            this.HttpClient = this.CreateHttpClient(this.BaseAddress);
        }

        /// <summary>
        /// Configura o handler para o HttpClient, usando certificado e protocolos SSL
        /// </summary>
        /// <returns>Objeto HttpClientHandler para configurar conexão http</returns>
        private HttpClientHandler GetHttpClientHandler()
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    MaxConnectionsPerServer = 9999,
                    SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12
                };

                return handler;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        /// <summary>
        /// Cria novo objeto HttpClient através do baseAddress e do handler padrão para conexão
        /// </summary>
        /// <param name="baseAddress">URL do enedereço base para conexão com o serviço REST</param>
        /// <returns>Objeto HttpClient</returns>
        public HttpClient CreateHttpClient(string baseAddress)
        {
            try
            {
                HttpClientHandler handler = this.GetHttpClientHandler();
                HttpClient client = new HttpClient(handler)
                {
                    Timeout = TimeSpan.FromMinutes(20),
                    BaseAddress = new Uri(baseAddress),
                };

                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                return client;
            }
            catch (Exception ex)
            {
                string message = string.Concat("Erro ao estabelecer comunicação com o serviço: ", ex.Message);
                throw new BusinessException(message);
            }
        }

        /// <summary>
        /// Envia uma requisição POST para o endpoint formado por baseAddress e url, usando 
        /// como request body o parâmetro jsonData
        /// </summary>
        /// <param name="url">Action usada para criar resource no serviço</param>
        /// <param name="jsonData">Objeto usado para requisição</param>
        /// <returns>Resultado da requisição</returns>
        public async Task<string> Post(string url, string jsonData)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(jsonData, Encoding.UTF8, "application/json"),
                };

                HttpResponseMessage response = await this.HttpClient.SendAsync(request);

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                string message = string.Concat("Erro ao enviar requisição para serviço: ", ex.Message);
                throw new BusinessException(message);
            }
        }

        /// <summary>
        /// Libera os recursos de uso do client Http
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Override do método Dispose
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            this.HttpClient.Dispose();
        }
    }
}
