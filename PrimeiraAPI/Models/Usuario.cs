using PrimeiraAPI.Validacoes;
using System.ComponentModel.DataAnnotations;

namespace PrimeiraAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        [Cpf]
        public string Cpf { get; set; }

        public TipoUsuario Tipo { get; set; }
    }

    public enum TipoUsuario
    {
        Admin, Aluno
    }
}
