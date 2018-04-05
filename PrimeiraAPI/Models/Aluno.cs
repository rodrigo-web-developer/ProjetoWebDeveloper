using System;
using System.ComponentModel.DataAnnotations;

namespace PrimeiraAPI.Models
{
    public class Aluno
    {
        private int idade; // campo
        [Required(ErrorMessage = "O campo RA é obrigatório")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "O RA deve ter 6 dígitos")]
        [RegularExpression(@"\d{6}")]
        public string Ra { get; set; } // propriedade
        [Required]
        public string Nome { get; set; } // propriedade

        public Graduacao TipoGraduacao { get; set; }

        public DateTime DataNascimento { get; set; } // propriedade
        [Range(18, 120)]
        public int Idade
        { // propriedade com apenas get
            get
            {
                idade = DateTime.Today.Year - DataNascimento.Year;
                var x = DataNascimento.AddYears(idade);
                if (DateTime.Today < x)
                    idade--;
                return idade;
            }
        }
    }

    public enum Graduacao
    {
        Licenciatura, 
        Tecnologo,
        Bacharelado
    }
}
