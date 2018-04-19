using LiteDB;

namespace PrimeiraAPI.Models
{
    public class Professor
    {
        [BsonId]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Rp { get; set; }

    }
}
