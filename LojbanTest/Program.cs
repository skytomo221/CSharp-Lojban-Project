using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lojban;

namespace LojbanTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = "la.alis.co'a tatpi lo nu zutse lo rirxe korbi re'o lo mensi gi'e zukte fi no da";
            var parser = new LojbanParser();
            parser.Parse(text);
            Console.WriteLine(parser.Json);
        }
    }
}
