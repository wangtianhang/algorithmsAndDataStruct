using System;
using System.Collections.Generic;

using System.Text;



public class Algorithms
{
    public static void Test()
    {
        Console.WriteLine(JosephRing(10, 2));
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
        Random.Shuffle<T>(a);
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
    public static int maxSubSum4(int[] nums)
    {
        int maxsum, maxhere;
        maxsum = maxhere = nums[0];   //初始化最大和为a【0】  
        for (int i = 1; i < nums.Length; i++)
        {
            if (maxhere <= 0)
                maxhere = nums[i];  //如果前面位置最大连续子序列和小于等于0，则以当前位置i结尾的最大连续子序列和为a[i]  
            else
                maxhere += nums[i]; //如果前面位置最大连续子序列和大于0，则以当前位置i结尾的最大连续子序列和为它们两者之和  
            if (maxhere > maxsum)
            {
                maxsum = maxhere;  //更新最大连续子序列和  
            }
        }
        return maxsum; 
    }

    /// <summary>
    /// 约瑟夫环问题
    /// </summary>
    /// <param name="n"></param>
    /// <param name="m"></param>
    /// <returns></returns>
    static int JosephRing(int n, int m)
    {
        int result = 0;
        for (int i = 2; i <= n; ++i )
        {
            result = (result + m) % i;
        }
        return result + 1;
    }
}

