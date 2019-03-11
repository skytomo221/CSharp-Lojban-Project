using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LojbanTest
{
    public abstract class LojbanGrammer
    {
    }



    public class Property
    {
        public string Key { get; private set; }
        public int Value { get; private set; }
        public Property(string key, int value)
        {
            Key = key;
            Value = value;
        }
    }

    public static class ConfigParser
    {
        public static readonly Parser<string> Key = Parse.Letter.AtLeastOnce().Text();
        public static readonly Parser<int> Value = Parse.Decimal.Select(n => Convert.ToInt32(n));
        public static readonly Parser<Property> Property = from key in Key.Token()
                                                           from asign in Parse.Char('=').Token()
                                                           from value in Value.Token()
                                                           select new Property(key, value);
        public static readonly Parser<IEnumerable<Property>> Properties = Property.Many().End();
    }

    class Program
    {

        static void Main(string[] args)
        {
            //var src = "speed = 300\ntest = 200";
            //var p = ConfigParser.Properties.Parse(src);
            //foreach (var item in p)
            //{
            //    Console.WriteLine(item.Key + "=" + item.Value);
            //}

            var input = "(cat))";

            // Sparache
            Parser<string> parser = from left in Parse.Char('(')
                                    from main in Parse.CharExcept(new char[] { '(', ')' }).Many().Text()
                                    from right in Parse.Char(')')
                                    select main;
            string result = parser.Parse(input);
            Console.WriteLine("Sprache: " + result + " (" + result + ")");

            // 正規表現
            var match = Regex.Match(input, @"(\(*).*(\)*)");
            result = match.Value;
            Console.WriteLine("Regex: " + result + " (" + result + ")");

        }
    }
}
