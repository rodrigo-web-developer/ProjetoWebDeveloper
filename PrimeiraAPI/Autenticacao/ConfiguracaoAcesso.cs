using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace PrimeiraAPI.Autenticacao
{
    public class ConfiguracaoAcesso
    {
        public SecurityKey Key { get; set; }
        public SigningCredentials SigningCredentials { get; set; }

        public ConfiguracaoAcesso()
        {
            using (var criptografia = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(criptografia.ExportParameters(true));
            }
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }

    public class ConfiguracaoToken
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }
}
