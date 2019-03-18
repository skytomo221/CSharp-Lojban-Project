using Lojban;
using Lojban.Jbovlaste;
using System;
using System.Linq;

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

            var jbovlaste = new Dictionary("banjupunu.xml");
            var result2 = (from valsi in jbovlaste
                          orderby valsi.Selmaho
                          select valsi.Selmaho).Distinct();
            foreach (var selmaho in result2)
            {
                System.Console.WriteLine(selmaho);
            }
        }
    }
}

