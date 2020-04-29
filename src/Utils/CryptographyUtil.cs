using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ApiBase.Utils
{
    public static class CryptographyUtil
    {
        /// <summary>
        /// Cria hash para o valor informado usando funções de derivação de chave criptográfica
        /// </summary>
        /// <inheritdoc cref="https://docs.microsoft.com/pt-br/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-3.1"/>
        /// <param name="value">Valor ou password a ser criptografado</param>
        /// <returns>Valor do hash</returns>
        public static string GenerateHash(string value)
        {
            try
            {
                string hash = string.Empty;

                // Cria um salt de 128-bit usando PRNG
                byte[] salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                hash = Convert.ToBase64String(
                    KeyDerivation.Pbkdf2(
                        password: value,
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8
                    )
                );

                return hash;
            }
            catch (Exception ex)
            { 
                throw new Exception("Erro ao gerar hash do valor informado: " + ex.Message);
            }
        }
    }
}
