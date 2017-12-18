//using System;
using System.Collections.Generic;

using System.Text;

public class Random
{
    static System.Random s_ran = null;
    static void Init()
    {
        if (s_ran == null)
        {
            long tick = System.DateTime.Now.Ticks;
            s_ran = new System.Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
        }
    }

    public static void SetSeed(int seed)
    {
        s_ran = new System.Random(0);
    }

    public static int Range(int min, int max)
    {
        Init();

        return s_ran.Next(min, max);
    }

    static int uniform(int n)
    {
        Init();

        return s_ran.Next() % n;
    }

    /// <summary>
    /// 完美洗牌算法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    public static void Shuffle<T>(T[] a)
    {
        int n = a.Length;
        for (int i = 0; i < n; ++i)
        {
            int r = i + uniform(n - i);
            T temp = a[i];
            a[i] = a[r];
            a[r] = temp;
        }
    }
}

