using LiteDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PrimeiraAPI.Autenticacao;
using PrimeiraAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PrimeiraAPI.Controllers
{
    public class UsuarioController : ApiController<Usuario>
    {
        public ConfiguracaoAcesso Acesso { get; set; }
        public ConfiguracaoToken Token { get; set; }
        public UsuarioController(string connString, ConfiguracaoToken configToken, ConfiguracaoAcesso configAcesso) : base(connString)
        {
            Token = configToken;
            Acesso = configAcesso;
        }

        [HttpPost]
        public JsonResult Login(Usuario u)
        {
            if (ModelState.IsValid)
            {
                using (var banco = new LiteDatabase(ConnectionString))
                {
                    var usuario = banco.GetCollection<Usuario>().FindOne(us => us.Email == u.Email);
                    if(usuario != null)
                    {
                        if(usuario.Senha == u.Senha)
                        {
                            var identidade = new ClaimsIdentity(new[] {
                                new Claim(ClaimTypes.Role, u.Tipo.ToString())
                            }, "Bearer");

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
    }
}
