using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace ConsoleApp4
{
    public static class Extension
    {
        [Flags]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        public static string Format(this IEnumerable<SlotDetail> obj, string formatter)
        {
            return string.Join(formatter,
                obj.Select(s => $"{s.GetLocationCode()}  ({s.Slots})  ({(s.StartDate == null ? "NA" : ((DateTime)s?.StartDate).ToString("yyyy MMM dd"))})"));
        }
    }
}