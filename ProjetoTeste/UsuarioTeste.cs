using LiteDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeiraAPI.Database;
using PrimeiraAPI.Models;
using PrimeiraAPI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ProjetoTeste
{
    [TestClass]
    public class UsuarioTeste
    {
        private readonly string ConnString = @"..\BancoDados.db";
        public DateTime DataCriacaoTestes = new DateTime(3000, 4, 1);
        private UsuarioService Service;
        [TestInitialize]
        public void Setup()
        {
            Service = new UsuarioService(ConnString);
        }

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
            Configuration.Configure(ConnString);
            var u = new Usuario()
            {
                Email = "teste1@teste.com",
                Senha = "senha1",
                CreatedAt = DataCriacaoTestes
            };
            var usuario = Service.Criar(u);
            Assert.IsNotNull(usuario, "Falhou na criacao: " +
                string.Join("\n", Service.Mensagens.Select(m => m.Mensagem).ToArray()));

            Assert.AreNotEqual(u.Senha, "senha1", "Não criptografou a senha");
        }

        [TestMethod]
        public void TesteCarga()
        {
            for (int i = 0; i < 10000; i++)
            {
                var u = new Usuario()
                {
                    Email = $"teste_{i}teste.com",
                    Senha = "senha1",
                    CreatedAt = DataCriacaoTestes
                };
                var usuario = Service.Criar(u);
                Assert.IsNotNull(usuario, "Falhou na criacao: " +
                    string.Join("\n", Service.Mensagens.Select(m => m.Mensagem).ToArray()));

                Assert.AreNotEqual(u.Senha, "senha1", "Não criptografou a senha");
            }
        }


        [TestMethod]
        public void ExcluirUsuarios()
        {
            TestarCriacao();

            var usuarios = Service.List().Where(u => u.Email == "teste1@teste.com");
            Assert.AreNotEqual(usuarios.Count(), 0, "Nao retornou nenhum usuario");

            foreach (var u in usuarios)
            {
                var x = Service.Excluir(u.Id);
                Assert.IsNotNull(x, "Não foi possível excluir o usuário");
            }
        }

        [TestMethod]
        public void TestarValidacaoCpf()
        {
            var u = new Usuario { Cpf = "11111111111" };
            var validacao = new ValidationContext(u);
            validacao.MemberName = "Cpf";
            var service = validacao.GetService(typeof(Usuario));
            try
            {
                Validator.ValidateProperty(u.Cpf, validacao);
                Assert.IsTrue(false, "Nenhum erro encontrado");
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(true, "Nenhum erro encontrado");
                u.Cpf = "96375852307";
                Validator.ValidateProperty(u.Cpf, validacao);
                Assert.IsFalse(false, "Erro de validacao encontrado");
            }
        }

        [TestCleanup]
        public void TearDown()
        {
            using (var banco = new LiteDatabase(ConnString))
            {
                var usuarios = banco.GetCollection<Usuario>()
                    .Delete(u => u.CreatedAt == DataCriacaoTestes);
            }
        }
    }

}
