using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication
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

        public double Somar<T1, T2>(T1 x, T2 y)
        {
            var n1 = double.Parse(x.ToString());
            var n2 = double.Parse(y.ToString());
            return n1 + n2;
        }

        public void Alertar(double x)
        {
            Console.WriteLine("O valor de x é: " + x);
        }
    }

}
