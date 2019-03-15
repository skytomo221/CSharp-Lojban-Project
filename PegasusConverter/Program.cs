using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace PegasusParser
{
    struct Expression
    {
        public string Left { get; set; }
        public string Right { get; set; }
        public Expression(string left, string right) { Left = left; Right = right; }
        public Expression(string comment) { Left = comment; Right = string.Empty; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string peg, peggcs = string.Empty;
            using (var sr = new StreamReader("camxes.peg", Encoding.UTF8))
            {
                peg = sr.ReadToEnd();
            }
            peg = peg.Replace("\r\n", "\n");
            foreach (var sentence in peg.Split('\n'))
            {
                var matches = Regex.Matches(sentence, @"(?<left>.*) <- (?<right>.*)");
                //Console.WriteLine(matches.Count);
                if (matches.Count == 1)
                {
                    var left = matches[0].Groups["left"].Value.Replace("-", "_");
                    var right = matches[0].Groups["right"].Value.Replace("-", "_");
                    peggcs += left + " <Node> <- expr: (" + right + ") { new Node(\"" + left + "\", expr) }";
                }
                else
                {
                    peggcs += sentence;
                }
                peggcs += "\n";
            }
            Console.WriteLine(peggcs);
        }
    }
}
