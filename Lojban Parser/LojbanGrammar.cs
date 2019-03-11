using Irony.Interpreter;
using Irony.Parsing;

namespace Lojban_Parser
{
    [Language("Lojban")]
    class LojbanGrammar : InterpretedLanguageGrammar
    {
        public LojbanGrammar() : base(true)
        {
            // 1. Terminals
            NumberLiteral Number = new NumberLiteral("number");
            KeyTerm AddOperator = ToTerm("+");
            KeyTerm SubOperator = ToTerm("-");
            KeyTerm MulOperator = ToTerm("*");
            KeyTerm DivOperator = ToTerm("/");
            KeyTerm PowOperator = ToTerm("**");
            KeyTerm LeftParen = ToTerm("(");
            KeyTerm RightParen = ToTerm(")");
            KeyTerm EndOfSentence = ToTerm(";");
        }
    }
}
