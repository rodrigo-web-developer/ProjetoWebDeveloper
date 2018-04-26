using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeiraAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        public TipoUsuario Tipo { get; set; }
    }

    public enum TipoUsuario
    {
        Admin, Aluno
    }
}
