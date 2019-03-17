using Lojban;
using System;

namespace LojbanTest
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var text = "la.alis.co'a tatpi lo nu zutse lo rirxe korbi re'o lo mensi gi'e zukte fi no da";
            var parser = new LojbanParser();
            var result = parser.Parse(text);
            Console.WriteLine(result);
        }
    }
}
