
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

public class QueryPerfCounter
{
    public static void Test()
    {
        Console.WriteLine(GetCurNanoSecond());
        Console.ReadLine();
    }

    [DllImport("KERNEL32")]
    static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

    [DllImport("Kernel32.dll")]
    static extern bool QueryPerformanceFrequency(out long lpFrequency);

    static long s_frequency = 0;
    static Decimal s_multiplier = new Decimal(1.0e9);

    public static long GetCurNanoSecond()
    {
        if (s_frequency == 0)
        {
            if (QueryPerformanceFrequency(out s_frequency) == false)
            {
                // Frequency not supported
                throw new Win32Exception();
            }
        }

        long perfermanceCounter = 0;
        QueryPerformanceCounter(out perfermanceCounter);

        double ret = (double)perfermanceCounter * (double)s_multiplier / (double)s_frequency;
        return (long)ret;
    }
}
