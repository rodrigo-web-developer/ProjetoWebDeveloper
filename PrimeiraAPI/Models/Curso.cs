using LiteDB;
using System.ComponentModel.DataAnnotations;

namespace PrimeiraAPI.Models
{
    public class Curso
    {
        [Required]
        public string Nome { get; set; }
        [Range(4, 10)]
        public int Duracao { get; set; }
        [BsonId]
        public int Codigo { get; set; }
    }
}
