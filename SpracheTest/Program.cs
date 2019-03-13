using Sprache;
using System;

namespace SpracheTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var comma = Parse.Char(' ').Optional();
            var result = comma.Parse("");
            Console.WriteLine("\"" + result.GetOrDefault() + "\"");
        }
    }
}
