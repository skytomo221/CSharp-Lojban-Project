using Sprache;
using System;

namespace SpracheTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var k = from a in Parse.Char('t')
                       from b in Parse.Char('k')
                       select b;
            var test = from c in Parse.Char('t')
                       from d in k
                       select c.ToString() + d;
            var result = test.Parse("tk");
            Console.WriteLine("\"" + result + "\"");
        }
    }
}
