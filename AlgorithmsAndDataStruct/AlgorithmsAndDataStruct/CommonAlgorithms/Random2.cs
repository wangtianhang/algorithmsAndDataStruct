//using System;
using System.Collections.Generic;

using System.Text;

class Random
{
    static System.Random s_ran = null;
    public static void Init()
    {
        if (s_ran == null)
        {
            long tick = System.DateTime.Now.Ticks;
            s_ran = new System.Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
        }
    }

    public static int Range(int min, int max)
    {
        Init();

        return s_ran.Next(min, max);
    }
}

class Random2
{
    static System.Random inter = new System.Random();
    public static int uniform(int n)
    {
        return inter.Next() % n;
    }

    public static void Shuffle<T>(T[] a)
    {
        int n = a.Length;
        for (int i = 0; i < n; ++i )
        {
            int r = i + uniform(n - i);
            T temp = a[i];
            a[i] = a[r];
            a[r] = temp;
        }
    }


}

