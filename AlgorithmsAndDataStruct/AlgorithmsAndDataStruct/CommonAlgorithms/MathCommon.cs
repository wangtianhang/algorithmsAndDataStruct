using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;



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
    public static float Exp(float x, int n = 10)
    {
        // initialize sum of series 
        float sum = 1;

        for (int i = n - 1; i > 0; --i)
            sum = 1 + x * sum / i;

        return sum; 
    }

    public static float Ln(float x2, int n = 10)
    {
        float sum = 0;
        float x = x2 - 1;
        for (int i = 1; i <= n; ++i )
        {
            if(i % 2 == 0)
            {
                sum += _Pow(x, i) / _Factorial(i);
            }
            else
            {
                sum -= _Pow(x, i) / _Factorial(i);
            }
        }
        return sum;
    }

    // 换底公式
    public static float Log(float a, float b, int n = 10)
    {
        return Ln(b, n) / Ln(a, n);
    }

    static float _Pow(float x, int a)
    {
        float ret = 1;
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

    public static float Pow(float a, float x, int n = 10)
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

