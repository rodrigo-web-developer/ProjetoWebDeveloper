using LiteDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PrimeiraAPI.Autenticacao;
using PrimeiraAPI.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PrimeiraAPI.Controllers
{
    public class UsuarioController : ApiController<Usuario>
    {
        private readonly string ChaveCriptografia = "77834a37-6eaa-49d8-a243-589a5f143dd5";
        public ConfiguracaoAcesso Acesso { get; set; }
        public ConfiguracaoToken Token { get; set; }
        public UsuarioController(string connString, ConfiguracaoToken configToken, ConfiguracaoAcesso configAcesso) : base(connString)
        {
            Token = configToken;
            Acesso = configAcesso;
        }

        [HttpPost]
        public JsonResult Login([FromBody] Usuario u)
        {
            if (ModelState.IsValid)
            {
                using (var banco = new LiteDatabase(ConnectionString))
                {
                    var usuario = banco.GetCollection<Usuario>().FindOne(us => us.Email == u.Email);
                    if(usuario != null)
                    {
                        using (var cripto = new HMACSHA256(Encoding.UTF8.GetBytes(ChaveCriptografia)))
                        {
                            var encode = Encoding.UTF8.GetBytes(u.Senha);
                            var password = cripto.ComputeHash(encode);
                            u.Senha = BitConverter.ToString(password);
                        }
                        if (usuario.Senha == u.Senha)
                        {
                            var identidade = new ClaimsIdentity(
                                new[] {
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                                new Claim(ClaimTypes.Role, usuario.Tipo.ToString())
                            }, "Bearer", "Bearer", usuario.Tipo.ToString());

                            var handler = new JwtSecurityTokenHandler();

                            var token = handler.CreateToken(new SecurityTokenDescriptor {
                                Issuer = Token.Issuer,
                                Audience = Token.Audience,
                                SigningCredentials = Acesso.SigningCredentials,
                                Subject = identidade,
                                NotBefore = DateTime.Now,
                                Expires = DateTime.Now + TimeSpan.FromSeconds(Token.Seconds)
                            });

                            var tokenString = handler.WriteToken(token);

                            return new JsonResult(new
                            {
                                Valido = true,
                                CriadoEm = DateTime.Now,
                                AccessToken = tokenString
                            });

                        }
                    }
                }
            }
            Response.StatusCode = 422;
            return new JsonResult(new { Mensagem = "Usuário inválido!" });
        }

        public override JsonResult Criar([FromBody] Usuario c)
        {
            if (!string.IsNullOrEmpty(c.Senha))
            {
                using (var cripto = new HMACSHA256(Encoding.UTF8.GetBytes(ChaveCriptografia)))
                {
                    var encode = Encoding.UTF8.GetBytes(c.Senha);
                    var password = cripto.ComputeHash(encode);
                    c.Senha = BitConverter.ToString(password);
                }
            }
            return base.Criar(c);
        }
    }
}
