using Sprache;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


namespace Lojban.Calculator
{
    using OperatorCreator = System.Func<Expression, Expression, Expression>;

    static public class Calc
    {
        public static Parser<string> No => from digit in Parse.String("no").Text()
                                           select "0";
        public static Parser<string> Pa => from digit in Parse.String("pa").Text()
                                           select "1";
        public static Parser<string> Re => from digit in Parse.String("re").Text()
                                           select "2";
        public static Parser<string> Ci => from digit in Parse.String("ci").Text()
                                           select "3";
        public static Parser<string> Vo => from digit in Parse.String("vo").Text()
                                           select "4";
        public static Parser<string> Mu => from digit in Parse.String("mu").Text()
                                           select "5";
        public static Parser<string> Xa => from digit in Parse.String("xa").Text()
                                           select "6";
        public static Parser<string> Ze => from digit in Parse.String("ze").Text()
                                           select "7";
        public static Parser<string> Bi => from digit in Parse.String("bi").Text()
                                           select "8";
        public static Parser<string> So => from digit in Parse.String("so").Text()
                                           select "9";
        public static Parser<string> Dau => from digit in Parse.String("dau").Text()
                                            select "a";
        public static Parser<string> Fei => from digit in Parse.String("fei").Text()
                                            select "b";
        public static Parser<string> Gai => from digit in Parse.String("gai").Text()
                                            select "c";
        public static Parser<string> Jau => from digit in Parse.String("jau").Text()
                                            select "d";
        public static Parser<string> Rei => from digit in Parse.String("rei").Text()
                                            select "e";
        public static Parser<string> Vai => from digit in Parse.String("vai").Text()
                                            select "f";
        public static Parser<string> Pi => from digit in Parse.String("pi").Text()
                                           select ".";
        public static Parser<string> Digit => Parse.Digit.Select(c => c.ToString())
                                              .Or(No.Or(Pa.Or(Re.Or(Ci.Or(Vo.Or(Mu.Or(Xa.Or(Bi.Or(So)))))))));
        public static Parser<string> HexadecimalDigit => Parse.Chars("0123456789abcdef").Token().Select(c => c.ToString())
                                                        .Or(Digit.Or(Dau.Or(Fei.Or(Gai.Or(Jau.Or(Rei.Or(Vai)))))));
        public static Parser<Expression> LojbanNumber => from n in Digit.AtLeastOnce()
                                                         select new Integer(typeof(int), string.Join("", n));
        public static Parser<Expression> LojbanHexadecimal => from n in HexadecimalDigit.AtLeastOnce()
                                                              from space_1 in Parse.WhiteSpace.Many()
                                                              from op in Parse.String("ju'u").Text()
                                                              from space_2 in Parse.WhiteSpace.Many()
                                                              from one in Parse.String("pa").Text()
                                                              from space_3 in Parse.WhiteSpace.Many()
                                                              from six in Parse.String("ze").Text()
                                                              select new Integer(typeof(int), int.Parse(string.Join("", n), NumberStyles.HexNumber).ToString());
        public static Parser<Expression> Integer => LojbanNumber.Or(LojbanHexadecimal);
        public static Parser<Expression> MainParser => BinaryOperatorsParser;

        public static Parser<T> OrParser<T>(IEnumerable<Parser<T>> parsers) => parsers.Aggregate((a, b) => a.Or(b));
        public static Parser<T> OrParser<T>(params Parser<T>[] parsers) => OrParser((IEnumerable<Parser<T>>)parsers);

        public static Type UnitType(Type t1, Type t2)
        {
            if (t1 == typeof(double) || t2 == typeof(double))
                return typeof(double);
            else
                return typeof(int);
        }
        public static Parser<Expression> UnaryParser => throw new NotImplementedException();

        public static Parser<Expression> BinaryOperatorsParser =>
            BinaryOperatorParser(UnaryParser, new (string, OperatorCreator)[][] {
                new(string, OperatorCreator)[] {
                    ("su'i", Expression.Add)
                }});

        public static Parser<Expression> BinaryOperatorParser(
            Parser<Expression> elemParser,
            IEnumerable<(string, OperatorCreator)> operators)
        {
            Parser<Func<Expression, Expression>> restParser =
                operators
                .Select(x => from _ in Parse.String(x.Item1).Token()
                             from elem in elemParser
                             select new Func<Expression, Expression>(l => x.Item2(l, elem)))
                .Aggregate((x, y) => x.Or(y));
            return
                from elem in elemParser
                from rest in restParser.Many()
                select rest.Aggregate(elem, (acc, f) => f(acc));
        }

        public static Parser<Expression> BinaryOperatorParser(
            Parser<Expression> elemParser,
            IEnumerable<IEnumerable<(string, OperatorCreator)>> operators)
            => operators.Aggregate(elemParser, BinaryOperatorParser);
    }

    public abstract class Expression
    {
        public Type Type { get; set; }
        public virtual bool Debug { get; set; }
        public abstract string Evalution();

        public static Expression Add(Expression left, Expression right) => new Add(typeof(int), left, right);
    }

    public abstract class UnaryOperator : Expression
    {
        public string Operand { get; set; }
        public UnaryOperator(Type type, string operand) { Type = type; Operand = operand; }
    }

    public abstract class BinaryOperator : Expression
    {
        public Expression Left { get; set; }
        public Expression Right { get; set; }
        public bool _Debug;
        public override bool Debug { get => _Debug; set => _Debug = Left.Debug = Right.Debug = value; }
        public BinaryOperator(Type type, Expression left, Expression right) { Type = type; Left = left; Right = right; }
    }

    public class Integer : UnaryOperator
    {
        public Integer(Type type, string operand) : base(type, operand) { }
        public override string Evalution() { return Operand; }
    }

    public class Add : BinaryOperator
    {
        public Add(Type type, Expression left, Expression right) : base(type, left, right) { }
        public override string Evalution()
        {
            if (Type == typeof(int))
            {
                var left = int.Parse(Left.Evalution());
                var right = int.Parse(Right.Evalution());
                if (Debug) Console.WriteLine("Evaluate \"" + left + " + " + right + "\".");
                var result = left + right;
                if (Debug) Console.WriteLine(result + " is returned.");
                return result.ToString();
            }
            if (Type == typeof(double))
            {
                return (double.Parse(Left.Evalution()) + double.Parse(Right.Evalution())).ToString();
            }
            throw new Exception("Invaild type.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var text = "pa su'i re su'i re su'i re";
            Console.WriteLine(text);
            var result = Calc.MainParser.Parse(text);
            result.Debug = true;
            var r = result.Evalution();
            Console.WriteLine(r);
            Console.ReadKey();
        }
    }
}
