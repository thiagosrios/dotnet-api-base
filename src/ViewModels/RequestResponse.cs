using System.Net;

namespace ApiBase.ViewModels
{
    public static class RequestResponse
    {
        public static Response RequestSucessful(string message, object data = null)
        {
            return new Response()
            {
                Message = message,
                Data = data,
                Result = true,
                Status = (int)HttpStatusCode.OK
            };
        }

        public static Response ResourceNotFound(string message)
        {
            return new Response()
            {
                Message = message,
                Result = false,
                Status = (int)HttpStatusCode.NotFound
            };
        }

        public static Response NoContent()
        {
            return new Response()
            {
                Message = "Nenhum conteúdo disponível",
                Result = false,
                Status = (int)HttpStatusCode.NoContent
            };
        }

        
        public static Response RequestFailed(string message)
        {
            return new Response()
            {
                Message = message,
                Result = false,
                Status = (int)HttpStatusCode.BadRequest
            };
        }
    }
}
