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
            text = "coi";
            var parser = new LojbanParser();
            parser.ParserFile = "camxes";
            var result = parser.Parse(text);
            Console.WriteLine(result);
            parser.ParserFile = "camxes-beta";
            result = parser.Parse(text);
            Console.WriteLine(result);
        }
    }
}

