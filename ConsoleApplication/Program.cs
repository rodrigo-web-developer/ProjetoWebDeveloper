using System;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            int x;
            x = int.Parse(Console.ReadLine());
            var nome = Console.ReadLine();
            Console.WriteLine("Seu nome é {1}\nVocê digitou o numero: {0}", x, nome);
            Console.ReadLine();
        }
    }
}
