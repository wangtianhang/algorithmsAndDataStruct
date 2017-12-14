﻿using System;
using System.Collections.Generic;

using System.Text;



class MathCollection
{
    public static void Test()
    {
        string tmp = TenToTwo(10);
        Debug.Log(tmp);
        Debug.Log(TwoToTen(tmp).ToString());

        string tmp2 = TenToSixteen(10);
        Debug.Log(tmp2);
        Debug.Log(SixteenToTen(tmp2).ToString());

        Console.ReadLine();
    }

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
    /// 求导数
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public delegate float FunctionOfOneVariable(float x);
    public static float Derivative(FunctionOfOneVariable function, float x, float delta = 0.0001f)
    {
        return (function(x + delta) - function(x)) / delta;
    }

    /// <summary>
    /// sin（泰勒级数）
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static float Sin(float x)
    {
        float ret = x - x * x * x / (3 * 2 * 1) + x * x * x * x * x / (5 * 4 * 3 * 2 * 1);
        return ret;
    }

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

    /// <summary>
    /// 海伦公式
    /// </summary>
    /// <returns></returns>
    public float GetTriangleArea(float a, float b, float c)
    {
        float p = (a + b + c) / 2;
        return (float)Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    /// <summary>
    /// 一元二次方程组 ax2+bx+c=0;
    /// </summary>
    /// <returns></returns>
    public List<float> ResultOfQuadraticEquations(float a, float b, float c)
    {
        List<float> ret = new List<float>();
        float tmp = b * b - 4 * a * c;
        if(tmp > 0)
        {
            float x1 = -b + (float)Math.Sqrt(tmp);
            float x2 = -b - (float)Math.Sqrt(tmp);
            ret.Add(x1);
            ret.Add(x2);
        }
        else if (tmp == 0)
        {
            float x = -b / (2 * a);
            ret.Add(x);
        }
        return ret;
    }

    /// <summary>
    /// 十进制转二进制
    /// </summary>
    /// <returns></returns>
    public static string TenToTwo(int x)
    {
        return System.Convert.ToString(x, 2);
    }

    /// <summary>
    /// 二进制转十进制
    /// </summary>
    /// <returns></returns>
    public static int TwoToTen(string s)
    {
        return System.Convert.ToInt32(s, 2);
    }

    /// <summary>
    /// 十进制转十六进制
    /// </summary>
    /// <returns></returns>
    public static string TenToSixteen(int x)
    {
        return string.Format("{0:X}", x);
    }

    /// <summary>
    /// 十六进制转十进制
    /// </summary>
    /// <returns></returns>
    public static int SixteenToTen(string s)
    {
        return System.Convert.ToInt32(s, 16);
    }
}

