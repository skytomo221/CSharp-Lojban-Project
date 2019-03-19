using System;

namespace Lojban
{
    [Flags]
    public enum ParseMode
    {
        Indented = 0b0000001,
        KeepMorphology = 0b0010000,
        ShowSpaces = 0b0001000,
        ShowTerminators = 0b0000010,
        ShowWordClasses = 0b0000100,
        RawOutput = 0b0100000,
        ShowMainNodeLabels = 0b1000000,
    }
}
