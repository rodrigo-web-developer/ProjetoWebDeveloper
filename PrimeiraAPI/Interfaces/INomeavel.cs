using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeiraAPI.Interfaces
{
    public interface INomeavel
    {
        string Nome { get; set; }

        string DizerNome();
    }
}
