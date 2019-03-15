using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace PegasusParser
{
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
                if (matches.Count == 1)
                {
                    var left = matches[0].Groups["left"].Value.Replace("-", "_");
                    var right = matches[0].Groups["right"].Value.Replace("-", "_");
                    if(Regex.IsMatch(right, "#.*$"))
                    {
                        right = Regex.Replace(right, "#(?<comment>.*)$", "/*${comment}*/");
                    }
                    peggcs += left + " <Node> = expr: (" + right + ") { new Node(\"" + left + "\", expr) }";
                }
                else
                {
                    peggcs += (Regex.IsMatch(sentence, "^#.*"))
                    ? Regex.Replace(sentence, "^#(?=.*)", "//")
                    : sentence;
                }
                peggcs += "\n";
            }
            Console.WriteLine(peggcs);
        }
    }
}
