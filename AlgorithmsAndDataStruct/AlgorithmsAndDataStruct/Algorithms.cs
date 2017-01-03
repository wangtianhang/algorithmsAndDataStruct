using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Algorithms
{
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
        while (hi > lo)
        {
            int j = QuickSort<T>.Partition(a, lo, hi);
            if (j == k)
            {
                return a[k];
            }
            else if (j > k)
            {
                hi = j - 1;
            }
            else if (j < k)
            {
                lo = j + 1;
            }
        }

        return a[k];
    }

    /// <summary>
    /// 最大子序列和
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static int maxSubSum4(int[] a)
    {
        int maxSum = 0;
        int thisSum = 0;
        for (int j = 0; j < a.Length; ++j )
        {
            thisSum += a[j];

            if(thisSum > maxSum)
            {
                maxSum = thisSum;
            }
            else if(thisSum < 0)
            {
                thisSum = 0;
            }
        }

        return maxSum;
    }
}

