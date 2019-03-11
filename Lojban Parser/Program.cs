using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lojban_Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            var runner = new NPEG.GrammarInterpreter.PEGrammar();
            runner.Run();
        }
    }
}
