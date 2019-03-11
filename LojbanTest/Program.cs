using System;
using System.Text.RegularExpressions;


namespace LojbanTest
{
    public static class LojbanGrammer
    {

        //--------------------------------------------------------------------------------

        public static NotImplementedException LojbanWord => new NotImplementedException();
        public static NotImplementedException Nucleus => new NotImplementedException();
        public static NotImplementedException Affricate => new NotImplementedException();

        //--------------------------------------------------------------------------------

        public static string Glide => @"(" + i + u + ")(?!" + Nucleus + ")(?!" + Glide + ")";
        public static string Diphtong => @"(" + a + e + "|" + a + u + "|" + e + i + "|" + o + i + ")(?!" + Nucleus + ")(?!" + Glide + ")";
        public static string Vowel => @"(" + a + "|" + e + "|" + i + "|" + o + "|" + u + ")(?!" + Nucleus + ")";
        public static string a => Comma + @"*[aA]";
        public static string e => Comma + @"*[eE]";
        public static string i => Comma + @"*[iI]";
        public static string o => Comma + @"*[oO]";
        public static string u => Comma + @"*[uU]";
        public static string y => Comma + @"*[yY]";

        //--------------------------------------------------------------------------------

        public static string Syllabic => l + "|" + m + "|" + n + "|" + r;
        public static string Voiced => b + "|" + d + "|" + g + "|" + j + "|" + v + "|" + z;
        public static string Unvoiced => c + "|" + f + "|" + k + "|" + p + "|" + s + "|" + t + "|" + x;
        public static string l => Comma + @"*[lL](?!" + h + ")(?![lL])";
        public static string m => Comma + @"*[mM](?!" + h + ")(?![mM])(?!" + z + ")";
        public static string n => Comma + @"*[nN](?!" + h + ")(?![nN])(?!" + Affricate + ")";
        public static string r => Comma + @"*[rR](?!" + h + ")(?![rR])";
        public static string b => Comma + @"*[bB](?!" + h + ")(?![bB])(?!" + Unvoiced + ")";
        public static string d => Comma + @"*[dD](?!" + h + ")(?![dD])(?!" + Unvoiced + ")";
        public static string g => Comma + @"*[gG](?!" + h + ")(?![gG])(?!" + Unvoiced + ")";
        public static string v => Comma + @"*[vV](?!" + h + ")(?![vV])(?!" + Unvoiced + ")";
        public static string j => Comma + @"*[jJ](?!" + h + ")(?![jJ])(?!" + z + ")(?!" + Unvoiced + ")";
        public static string z => Comma + @"*[zZ](?!" + h + ")(?![zZ])(?!" + j + ")(?!" + Voiced + ")";
        public static string s => Comma + @"*[sS](?!" + h + ")(?![sS])(?!" + c + ")(?!" + Voiced + ")";
        public static string c => Comma + @"*[cC](?!" + h + ")(?![cC])(?!" + s + ")(?!" + x + ")(?!" + Voiced + ")";
        public static string x => Comma + @"*[xX](?!" + h + ")(?![xX])(?!" + c + ")(?!" + k + ")(?!" + Voiced + ")";
        public static string k => Comma + @"*[kK](?!" + h + ")(?![kK])(?!" + x + ")(?!" + Voiced + ")";
        public static string f => Comma + @"*[fF](?!" + h + ")(?![fF])(?!" + Voiced + ")";
        public static string p => Comma + @"*[pP](?!" + h + ")(?![pP])(?!" + Voiced + ")";
        public static string t => Comma + @"*[tT](?!" + h + ")(?![tT])(?!" + Voiced + ")";
        public static string h => Comma + @"*['h](?" + Nucleus + ")";

        //--------------------------------------------------------------------------------

        public static string Digit => Comma + @"*%d(?!" + h + ")(?!" + Nucleus + ")";
        public static string PostWord => Pause + @"|(?!" + Nucleus + ")" + LojbanWord;
        public static string Pause => Comma + @"*" + SpaceChar + "|" + EOF;
        public static string EOF => Comma + @"*(?!.)";
        public static string Comma => @"[,]";
        public static string NonLojbanWord => @"[,]";
        public static string NonSpace => @"[,]";

        // Unicode-style and escaped chars not compatible with cl-peg
        // space-char <- [.\t\n\r?!\u0020]
        public static string SpaceChar => @"[.?! ]" + "|" + SpaceChar1 + "|" + SpaceChar2;
        public static string SpaceChar1 => @"\t";
        public static string SpaceChar2 => @"\n";

    }

    class Program
    {
        static void Main(string[] args)
        {
            var result = Regex.Match("ki'mo", LojbanGrammer.h);
            Console.WriteLine("Success : " + result.Success);
            Console.WriteLine("Value   : " + result.Value);
        }
    }
}
