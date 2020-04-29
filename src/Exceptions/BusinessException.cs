using System;
using System.Runtime.Serialization;

namespace ApiBase.Exceptions
{
    [Serializable()]
    public class BusinessException : Exception
    {       
        /// <summary>
        /// Método construtor básico
        /// </summary>
        /// <param name="message">Mensagem de retorno</param>
        public BusinessException(string message) : base(message){}

        /// <summary>
        /// Método construtor para serialização
        /// </summary>
        /// <param name="info">Informação da exception</param>
        /// <param name="context">Contexto </param>
        protected BusinessException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
