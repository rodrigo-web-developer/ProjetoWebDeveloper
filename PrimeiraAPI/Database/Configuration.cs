using LiteDB;
using PrimeiraAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeiraAPI.Database
{
    public class Configuration
    {
        public static void Configure(string conString)
        {
            using (var banco = new LiteDatabase(conString))
            {
                var usuarios = banco.GetCollection<Usuario>();
                usuarios.EnsureIndex(u => u.Email, true);

                var professores = banco.GetCollection<Professor>();
                professores.EnsureIndex(u => u.Rp, true);
            }
        }
    }
}
