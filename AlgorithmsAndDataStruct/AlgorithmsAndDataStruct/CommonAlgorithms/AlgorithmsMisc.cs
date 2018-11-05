using System;
using System.Collections.Generic;

using System.Text;



public class AlgorithmsMisc
{
    public static void Test()
    {
        Console.WriteLine(JosephRing(10, 2));
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

    /// <summary>
    /// 不用多余变量交换两个int型。。又是无聊的trick题目
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    public static void Swap(ref int a, ref int b)
    {
        a = a + b;
        b = a - b;
        a = a - b;
    }

    //埃拉托色尼筛选法
    //求一定范围内的质数
    public static void SieveOfEratosthenes(int n)
    {

        // Create a boolean array "prime[0..n]" and initialize 
        // all entries it as true. A value in prime[i] will 
        // finally be false if i is Not a prime, else true. 

        bool[] prime = new bool[n + 1];

        for (int i = 0; i < n; i++)
            prime[i] = true;

        for (int p = 2; p * p <= n; p++)
        {
            // If prime[p] is not changed, 
            // then it is a prime 
            if (prime[p] == true)
            {
                // Update all multiples of p 
                for (int i = p * 2; i <= n; i += p)
                    prime[i] = false;
            }
        }

        // Print all prime numbers 
        for (int i = 2; i <= n; i++)
        {
            if (prime[i] == true)
                Console.Write(i + " ");
        }

    }


}

