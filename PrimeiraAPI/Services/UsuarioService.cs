using PrimeiraAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PrimeiraAPI.Services
{
    public class UsuarioService : CrudService<Usuario>
    {
        private readonly string ChaveCriptografia = "77834a37-6eaa-49d8-a243-589a5f143dd5";
        public UsuarioService(string connString) : base(connString)
        {
        }

        public override Usuario Criar(Usuario model)
        {

            if (!string.IsNullOrEmpty(model.Senha))
            {
                using (var cripto = new HMACSHA256(Encoding.UTF8.GetBytes(ChaveCriptografia)))
                {
                    var encode = Encoding.UTF8.GetBytes(model.Senha);
                    var password = cripto.ComputeHash(encode);
                    model.Senha = BitConverter.ToString(password);
                }
            }
            return base.Criar(model);
        }
    }
}
