using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeiraAPI.Models;
using PrimeiraAPI.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ProjetoTeste
{
    [TestClass]
    public class UsuarioTeste
    {
        [TestMethod]
        public void TestarAtribuicaoEmail()
        {
            var u = new Usuario();
            u.Email = "EMAIL@EMAIL.COM";
            Assert.AreEqual(u.Email, "email@email.com", "A atribuição de email para o usuário não está correta");
        }

        [TestMethod]
        public void TestarValidacaoUsuario()
        {
            var u = new Usuario();
            var validacao = new ValidationContext(u, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Validator.TryValidateObject(u, validacao, results);

            Assert.IsTrue(results.Any(), "Nenhum erro encontrado");
            
            Assert.AreEqual(results.Count, 2, "Erros de validacao não são 2");
        }

        [TestMethod]
        public void TestarCriacao()
        {
            var u = new Usuario()
            {
                Email = "email@email.com",
                Senha = "senha1"
            };
            var usuarioService = new UsuarioService(@"..\BancoDados.db");
            var usuario = usuarioService.Criar(u);
            Assert.IsNotNull(usuario, "Falhou na criacao: " +
                usuarioService.Mensagens?.Select(m => m.Mensagem).ToArray());

            Assert.AreNotEqual(u.Senha, "senha1", "Não criptografou a senha");
        }

        [TestMethod]
        public void ExcluirUsuarios()
        {
            TestarCriacao();
            var usuarioService = new UsuarioService(@"..\BancoDados.db");

            var usuarios = usuarioService.List().Where(u => u.Email == "email@email.com");

            Assert.AreNotEqual(usuarios.Count(), 0, "Nao retornou nenhum usuario");

            foreach (var u in usuarios)
            {
                var x = usuarioService.Excluir(u.Id);
                Assert.IsNotNull(x, "Não foi possível excluir o usuário");
            }
        }

        [TestMethod]
        public void TestarValidacaoCpf()
        {
            var u = new Usuario { Email = "BLA@EMAIL.COM", Senha = "123123", Cpf = "12345678910" };
            var validacao = new ValidationContext(u);
            var service = validacao.GetService(typeof(Usuario));
            try
            {
                Validator.ValidateObject(u, validacao, true);
                Assert.IsTrue(false, "Nenhum erro encontrado");
            }
            catch (System.Exception ex)
            {

                Assert.IsTrue(true, "Nenhum erro encontrado");
                u.Cpf = "96375852307";
                Validator.ValidateObject(u, validacao, true);
                Assert.IsFalse(false, "Erro de validacao encontrado");
            }
        }
    }
}
