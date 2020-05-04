using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ApiBaseTest.Utilities.Mocks
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        public virtual HttpResponseMessage Send(HttpRequestMessage request)
        {
            return new HttpResponseMessage();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Send(request));
        }
    }
}
