using System;
using System.Collections.Generic;
using System.Text;


class RandomAlgorithm
{
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
            int random = Random.Range(0, n - i);
            //int r = i + uniform(n - i);
            int r = i + random;
            T temp = a[i];
            a[i] = a[r];
            a[r] = temp;
        }
    }
}

