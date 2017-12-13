using System;
using System.Collections.Generic;

using System.Runtime.InteropServices;
using System.Text;



class CpuUsage
{
    [DllImport("kernel32.dll")]
    static extern uint GetTickCount();

    [DllImport("kernel32.dll")]
    static extern UIntPtr SetThreadAffinityMask(IntPtr hThread,
    UIntPtr dwThreadAffinityMask);

    [DllImport("kernel32.dll")]
    static extern IntPtr GetCurrentThread();

    static ulong SetCpuID(int id)
    {
        ulong cpuid = 0;
        if (id < 0 || id >= System.Environment.ProcessorCount)
        {
            id = 0;
        }
        cpuid |= 1UL << id;

        return cpuid;
    } 
 
    public static void Test()
    {
        SetThreadAffinityMask(GetCurrentThread(), new UIntPtr(SetCpuID(0)));

        MakeUsage3();
    }

    static void MakeUsage3()
    {
        int COUNT = 200;
        List<float> busySpan = new List<float>();
        List<float> idleSpan = new List<float>();
        float interval = 300;
        float half = interval / 2;
        float radian = 0;
        float split = 0.01f;

        for (int i = 0; i < COUNT; ++i)
        {
            busySpan.Add(half + (float)Math.Sin(Math.PI * radian) * half);
            idleSpan.Add(interval - busySpan[i]);
            radian += split;
        }

        int startTime = 0;
        int j = 0;
        while (true)
        {
            j = j % COUNT;
            startTime = (int)GetTickCount();
            while (GetTickCount() - startTime <= busySpan[j])
            {
                int test = 0;
            }
            System.Threading.Thread.Sleep((int)idleSpan[j]);
            Console.WriteLine("sleep " + idleSpan[j] + " ms");
            j++;
        }
    }
}

