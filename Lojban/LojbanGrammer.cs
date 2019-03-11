namespace Lojban
{
    public static class LojbanGrammer
    {
        public static string Comma { get; } = @"[,]";

        public static string Space_Char { get; } = @"[.?! ]" + "|" + Space_Char1 + "|" + Space_Char2;
        public static string Space_Char1 { get; } = @"\t";
        public static string Space_Char2 { get; } = @"\n";
    }
}
