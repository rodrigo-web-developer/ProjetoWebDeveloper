using System;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //int x;
            //x = int.Parse(Console.ReadLine());
            //var nome = Console.ReadLine();
            //Console.WriteLine("Seu nome é {1}\nVocê digitou o numero: {0}", x, nome);
            
            Func<int, int> dobrar = (a) => 
            {
                return a * 2;
            }; // uma função a recebe um valor inteiro e retorna o seu dobro
            var n = dobrar(10); // valor de n passa a ser 20

            string nome = "Rodrigo";
            int idade = 20;
            string texto = $"Meu nome é {nome} e tenho { idade} anos";
            Console.WriteLine(texto);
            Console.ReadLine();

        }

        public int Dobrar(int a)
        {
            return a * 2;
        }
    }
}
