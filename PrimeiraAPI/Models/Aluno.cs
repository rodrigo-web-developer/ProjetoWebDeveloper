using System;

namespace PrimeiraAPI.Models
{
    public class Aluno
    {
        private int idade; // campo
        public string Ra { get; set; } // propriedade
        public string Nome { get; set; } // propriedade

        public DateTime DataNascimento { get; set; } // propriedade
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
}
