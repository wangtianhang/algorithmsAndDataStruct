using System;
using System.Collections.Generic;
using System.Text;


class NumberTheory
{
    /// <summary>
    /// 最大公约数（递归），辗转相除法
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int GCDRecursive(int a, int b)
    {
        if (b == 0)
        {
            return a;
        }
        else
        {
            return GCDRecursive(b, a % b);
        }
    }

    /// <summary>
    /// 最大公约数，辗转相除法
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
//     public static int GCD(int a, int b)
//     {
//         int temp;          /*定义整型变量*/
//         if (a < b)             /*通过比较求出两个数中的最大值和最小值*/
//         {
//             temp = a;
//             a = b;
//             b = temp;
//         } /*设置中间变量进行两数交换*/
//         while (b != 0)           /*通过循环求两数的余数，直到余数为0*/
//         {
//             temp = a % b;
//             a = b;              /*变量数值交换*/
//             b = temp;
//         }
//         return a;            /*返回最大公约数到调用函数处*/
//     }

    /// <summary>
    /// 最大公约数，辗转相除法
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static long GCD(long a, long b)
    {
        long temp;          /*定义整型变量*/
        if (a < b)             /*通过比较求出两个数中的最大值和最小值*/
        {
            temp = a;
            a = b;
            b = temp;
        } /*设置中间变量进行两数交换*/
        while (b != 0)           /*通过循环求两数的余数，直到余数为0*/
        {
            temp = a % b;
            a = b;              /*变量数值交换*/
            b = temp;
        }
        return a;            /*返回最大公约数到调用函数处*/
    }

    /// <summary>
    /// 最小公倍数
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int LCM(int a, int b)
    {
        int temp;
        temp = (int)GCD(a, b);  /*调用自定义函数，求出最大公约数*/
        return (a * b / temp); /*返回最小公倍数到主调函数处进行输出*/
    }

    /// <summary>
    /// 下一个素数
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    int nextPrime(int n)
    {
        if (n % 2 == 0)
            n++;

        for (; !isPrime(n); n += 2)
            ;

        return n;
    }

    /// <summary>
    /// 判断是否为素数
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    bool isPrime(int n)
    {
        if (n == 2 || n == 3)
            return true;

        if (n == 1 || n % 2 == 0)
            return false;

        for (int i = 3; i * i <= n; i += 2)
            if (n % i == 0)
                return false;

        return true;
    }

    //厄拉多塞筛法,小于n的质数的个数
    public int CountPrimes(int n)
    {
        if (n <= 2)
        {
            return 0;
        }
        if (n == 3)
        {
            // 小于3只有2一个质数
            return 1;
        }
        bool[] del = new bool[n];
        del[2] = false;
        for (int i = 3; i < n; ++i)
        {
            if (i % 2 == 0)
            {
                del[i] = true;
            }
            else
            {
                del[i] = false;
            }
        }

        for (int i = 3; i < n; i += 2)
        {
            if (!del[i])// 之后第一个未被划去
            {
                if (i * i > n)
                {
                    // 当前素数的平方大于n，跳出循环
                    break;
                }
                for (int j = 2; i * j < n; ++j)
                {
                    del[i * j] = true;
                }
            }
        }

        int count = 0;
        for (int i = 2; i < n; ++i)
        {
            if (!del[i])
            {
                ++count;
            }
        }

        return count;
    }
}


