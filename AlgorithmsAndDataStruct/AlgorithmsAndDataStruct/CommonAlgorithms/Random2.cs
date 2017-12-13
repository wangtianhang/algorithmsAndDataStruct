using System;
using System.Collections.Generic;

using System.Text;



class Random2
{
    static System.Random inter = new Random();
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

