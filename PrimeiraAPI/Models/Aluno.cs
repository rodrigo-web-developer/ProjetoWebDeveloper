using LiteDB;
using PrimeiraAPI.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace PrimeiraAPI.Models
{
    public class Aluno : INomeavel
    {
        private int idade; // campo
        [Required(ErrorMessage = "O campo RA é obrigatório")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "O RA deve ter 6 dígitos")]
        [RegularExpression(@"\d{6}")]
        [BsonId]
        public string Ra { get; set; } // propriedade
        [Required]
        public string Nome { get; set; } // propriedade

        public Graduacao TipoGraduacao { get; set; }

        public DateTime DataNascimento { get; set; } // propriedade
        [Range(18, 120)]
        [BsonIgnore]
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

        public string DizerNome()
        {
            return $"Meu nome é {Nome}";
        }
    }

    public enum Graduacao
    {
        Licenciatura, 
        Tecnologo,
        Bacharelado
    }
}
