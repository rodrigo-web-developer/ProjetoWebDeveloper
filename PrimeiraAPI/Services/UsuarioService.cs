using PrimeiraAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeiraAPI.Services
{
    public class UsuarioService : CrudService<Usuario>
    {
        public UsuarioService(string connString) : base(connString)
        {
        }
    }
}
