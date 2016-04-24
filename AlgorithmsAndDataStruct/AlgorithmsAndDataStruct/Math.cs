﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class MathCollection
{
    /// <summary>
    /// 牛顿法求平方根
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static double sqrt(double c)
    {
        if(c < 0)
        {
            return Double.NaN;
        }

        double err = 1e-15;
        double t = c;
        while(Math.Abs(t - c / t) > err * t)
        {
            t = (c / t + t) / 2.0;
        }
        return t;
    }

    /// <summary>
    /// 找到一组数中第k小元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static T Select<T>(T[] a, int k)
    {
        Random2.Shuffle<T>(a);
        int lo = 0, hi = a.Length - 1;
        while(hi > lo)
        {
            int j = QuickSort<T>.Partition(a, lo, hi);
            if(j == k)
            {
                return a[k];
            }
            else if(j > k)
            {
                hi = j - 1;
            }
            else if(j < k)
            {
                lo = j + 1;
            }
        }

        return a[k];
    }
}

