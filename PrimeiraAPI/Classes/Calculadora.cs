using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeiraAPI.Classes
{
    public class Calculadora
    {
        public Calculadora()
        {
            Console.WriteLine("Inicializando a calculadora");
        }

        public double Somar(double x, double y)
        {
            return x + y;
        }
        public void Alertar(double x)
        {
            Console.WriteLine("O valor de x é: "+x);
        }
    }

}
