using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LojbanTest
{

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

    public abstract class MetaNode
    {
    }

    // text <- intro-null NAI-clause* text-part-2 (!text-1 joik-jek)? text-1? faho-clause EOF?
    public class Text : MetaNode
    {
        public Intro_null Intro_null { get; set; }
        public IEnumerable<NAI_clause> NAI_clause { get; set; }
        public Text_part_2 Text_part_2 { get; set; }
        public Joik_jek Joik_jek { get; set; }
        public Text_1 Text_1 { get; set; }
        public Faho_clause Faho_clause { get; set; }
        public EOF EOF { get; set; }
    }

    // intro-null <- spaces? su-clause* intro-si-clause
    public class Intro_null : MetaNode
    {
        public Spaces Spaces { get; set; }
        public IEnumerable<SuClause> SuClause { get; set; }
        public IntroSiClause IntroSiClause { get; set; }
    }

    // text-part-2 <- (CMENE-clause+ / indicators?) free*
    public class Text_part_2 : MetaNode
    {
        public IEnumerable<CmeneClause> CmeneClause { get; set; }
        public Indicators Indicators { get; set; }
        public IEnumerable<Free> Free { get; set; }
    }

    // ; intro-sa-clause <- SA-clause+ / any-word-SA-handling !(ZEI-clause SA-clause) intro-sa-clause
    // intro-si-clause <- si-clause? SI-clause*
    public class IntroSiClause : MetaNode {
        public SiClause SiClause { get; set; }
        public IEnumerable<SIClause> SIClause { get; set; }

    }
    // faho-clause<- (FAhO-clause dot-star)?
    public class Faho_clause : MetaNode
    {
        public FAhOClause FAhOClause { get; set; }
        public DotStar<SIClause> DotStar { get; set; }

    }

    public class NAI_clause : MetaNode { }
    public class Text_1 : MetaNode { }
    public class Joik_jek : MetaNode { }
    public class EOF : MetaNode { }
    public class Spaces : MetaNode { }
    public class SuClause : MetaNode { }
    public class CmeneClause : MetaNode { }
    public class Indicators : MetaNode { }
    public class Free : MetaNode { }
    public class SiClause : MetaNode { }
    public class SIClause : MetaNode { }

    public class SpaceChar1 : MetaNode
    {
    }

    public static class Lojban
    {
        public static readonly Parser<char> Comma = Parse.Char(',');
        public static readonly Parser<char> EOF = from main in Parse.Char(',')
                                                  from negative_look_behind in Parse.Not(Parse.AnyChar)
                                                  select main;
        public static readonly Parser<char> SpaceChar1 = Parse.Char('\t');
        public static readonly Parser<char> SpaceChar2 = Parse.Char('\n');
        public static readonly Parser<char> SpaceChar = Parse.Chars(new char[] { '.', '!', '?', ' ', }).Or(SpaceChar1.Or(SpaceChar2));
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
            Parser<IEnumerable<char>> test = Parse.AnyChar.Many();
            //var src = "speed = 300\ntest = 200";
            //var p = ConfigParser.Properties.Parse(src);
            //foreach (var item in p)
            //{
            //    Console.WriteLine(item.Key + "=" + item.Value);
            //}

            var input = "^cat";

            // Sparache
            Parser<string> parser = from look_ahead in Parse.String("^")
                                    from main in Parse.AnyChar.Many().Text()
                                    select main;
            string result = parser.Parse(input);

            Console.WriteLine("Sprache: " + result + " (" + result + ")");

            // 正規表現
            var match = Regex.Match(input, @"(?<!\^).*");
            result = match.Value;
            Console.WriteLine("Regex: " + result + " (" + result + ")");

        }
    }
}
