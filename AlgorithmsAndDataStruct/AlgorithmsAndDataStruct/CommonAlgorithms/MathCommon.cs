using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using UnityEngine;


public class MathCommon
{
    public static void Test()
    {


        //Debug.Log(CalculatePiSeries().ToString());
        //Debug.Log(CalculatePiFraction().ToString());

        //Debug.Log(CalculatePi_BBP_Fraction().ToString());

        //Console.ReadLine();


        //ProbabilityAB();



        //TestFuncGraphic();


        Debug.Log("Math.Exp " + Math.Exp(9));
        Debug.Log("SelfExp " + Exp(9));

        Debug.Log("Math.Exp " + Math.Exp(19));
        Debug.Log("SelfExp " + Exp(19));

        Debug.Log("Math.Ln " + Math.Log(1.1f));
        Debug.Log("SelfLn " + Ln(1.1f));

        Debug.Log("Math.Ln " + Math.Log(2f));
        Debug.Log("SelfLn " + Ln(2f));

        Debug.Log("Math.Ln " + Math.Log(10f));
        Debug.Log("SelfLn " + Ln(10f));

        Debug.Log("Math.Ln " + Math.Log(100));
        Debug.Log("SelfLn " + Ln(100));
    }

    /// <summary>
    /// 牛顿法求平方根
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static double Sqrt(double c)
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

    public static decimal Sqrt(decimal c)
    {
        if (c < 0)
        {
            return decimal.MinValue;
        }

        decimal err = 1e-28m;
        decimal t = c;
        while (Math.Abs(t - c / t) > err * t)
        {
            t = (c / t + t) / 2.0m;
        }
        return t;
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


    // exponential by  Taylor Series
    // Function returns approximate value of e^x  
    // using sum of first n terms of Taylor Series 
    public static double Exp(double x, int n = 10)
    {
        // initialize sum of series 
        double sum = 1;

        for (int i = n - 1; i > 0; --i)
            sum = 1 + x * sum / i;

        return sum; 
    }

    public static double LnOld(double x2, int n = 10)
    {
        double sum = 0;
        double x = x2 - 1;
        for (int i = 1; i <= n; ++i )
        {
            if(i % 2 == 1)
            {
                sum += _Pow(x, i) / i;
            }
            else
            {
                sum -= _Pow(x, i) / i;
            }
        }
        return sum;
    }

    public static double Ln(double x2, int n = 10)
    {
//         if(x2 < 2)
//         {
//             double sum = 0;
//             double x = x2 - 1;
//             for (int i = 1; i <= n; ++i)
//             {
//                 if (i % 2 == 1)
//                 {
//                     sum += _Pow(x, i) / i;
//                 }
//                 else
//                 {
//                     sum -= _Pow(x, i) / i;
//                 }
//             }
//             return sum;
//         }
        if(x2 < 10)
        {
            double sum = 0;
            double t = (x2 - 1) / (x2 + 1);
            for (int i = 0; i <= n; ++i)
            {
                sum += 2 * (_Pow(t, 2 * i + 1) / (2 * i + 1));
            }
            return sum;
        }
        else
        {
            double ln10 = 2.30258509299404568401;
            int count = 0;
            while(x2 > 10)
            {
                x2 /= 10;
                count += 1;
            }
            double sum = 0;
            double t = (x2 - 1) / (x2 + 1);
            for (int i = 0; i <= n; ++i)
            {
                sum += 2 * (_Pow(t, 2 * i + 1) / (2 * i + 1));
            }
            sum += count * ln10;
            return sum;
        }
    }

    // 换底公式
    public static double Log(double a, double b, int n = 10)
    {
        return Ln(b, n) / Ln(a, n);
    }

    static double _Pow(double x, int a)
    {
        double ret = 1;
        for (int i = 0; i < a; ++i )
        {
            ret *= x;
        }
        return ret;
    }

    static int _Factorial(int n)
    {
        int ret = 1;
        for (int i = 1; i <= n; ++i )
        {
            ret *= i;
        }
        return ret;
    }

    public static double Pow(double a, double x, int n = 10)
    {
        return Exp(x * Ln(a, n), n);
    }

//     public static float Log(float x2)
//     {
//         float x = x2 - 1;
//     }

//     public static double TestFunc(double x)
//     {
//         return 1 / Math.Sqrt(4 - x * x);
//     }
}

