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
    public static int GCD(int a, int b)
    {
        int temp;          /*定义整型变量*/
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
        temp = GCD(a, b);  /*调用自定义函数，求出最大公约数*/
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
}


