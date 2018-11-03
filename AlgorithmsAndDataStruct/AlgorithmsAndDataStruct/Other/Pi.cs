using System;
using System.Collections.Generic;
using System.Text;


public class Pi
{
    public static void Test()
    {
        UnityEngine.Debug.Log(CalculatePi_BBP_double().ToString());
        UnityEngine.Debug.Log(CalculatePi_BBP_decemal().ToString());
        UnityEngine.Debug.Log(CalculatePi_SuperPi_double().ToString());
        UnityEngine.Debug.Log(CalculatePi_SuperPi_decimal().ToString());
    }

    /// <summary>
    /// 求Pi
    /// </summary>
    /// <returns></returns>
    public static double CalculatePi_Series_double()
    {
        //double x = 1;
        double ret = 0;
        int n = 10000 * 10000;
        for (int i = 0; i <= n; ++i)
        {
            if (i % 2 == 0)
            {
                //奇数项
                double tmp = 1 / (double)(i * 2 + 1);
                ret += tmp;
                //Debug.Log("CalculatePi " + tmp);
            }
            else
            {
                //偶数项
                double tmp = -1 / (double)(i * 2 + 1);
                ret += tmp;
                //Debug.Log("CalculatePi " + tmp);
            }
        }

        ret *= 4;
        return ret;
    }

    /// <summary>
    /// 高速求pi bbp公式
    /// </summary>
    /// <returns></returns>
    public static double CalculatePi_BBP_double()
    {
        double pi = 0;
        int maxN = 10;
        for (int k = 0; k < maxN; ++k)
        {
            double par1 = 1 / Math.Pow(16, k);
            pi += par1 * (4 / (double)(8 * k + 1) - 2 / (double)(8 * k + 4) - 1 / (double)(8 * k + 5) - 1 / (double)(8 * k + 6));
        }
        return pi;
    }

    /// <summary>
    /// 高速求pi bbp公式
    /// </summary>
    /// <returns></returns>
    public static decimal CalculatePi_BBP_decemal()
    {
        decimal pi = 0;
        int maxN = 20;
        for (int k = 0; k < maxN; ++k)
        {
            decimal par1 = 1;
            for (int i = 0; i < k; ++i)
            {
                par1 *= 16;
            }
            par1 = 1 / par1;
            pi += par1 * (4 / (decimal)(8 * k + 1) - 2 / (decimal)(8 * k + 4) - 1 / (decimal)(8 * k + 5) - 1 / (decimal)(8 * k + 6));
        }
        return pi;
    }

    /// <summary>
    /// 高斯-勒让德算法 super pi 使用
    /// </summary>
    /// <returns></returns>
    public static double CalculatePi_SuperPi_double()
    {
        double a = 1;
        double b = 1 / Math.Sqrt(2);
        double t = 1 / (double)4;
        double p = 1;

        int maxN = 3;
        for (int n = 0; n < maxN; ++n)
        {
            double nextA = (a + b) / 2;
            double nextB = Math.Sqrt(a * b);
            double nextT = t - p * (a - nextA) * (a - nextA);
            double nextP = 2 * p;

            a = nextA;
            b = nextB;
            t = nextT;
            p = nextP;
        }

        double pi = (a + b) * (a + b) / (4 * t);
        return pi;
    }

    /// <summary>
    /// 高斯-勒让德算法 super pi 使用
    /// </summary>
    /// <returns></returns>
    public static decimal CalculatePi_SuperPi_decimal()
    {
        decimal a = 1;
        decimal b = 1 / MathCommon.Sqrt(2m);
        decimal t = 1 / (decimal)4;
        decimal p = 1;

        int maxN = 10;
        for (int n = 0; n < maxN; ++n)
        {
            decimal nextA = (a + b) / 2;
            decimal nextB = MathCommon.Sqrt(a * b);
            decimal nextT = t - p * (a - nextA) * (a - nextA);
            decimal nextP = 2 * p;

            a = nextA;
            b = nextB;
            t = nextT;
            p = nextP;
        }

        decimal pi = (a + b) * (a + b) / (4 * t);
        return pi;
    }

    public static double CalculatePi_BBP_Fraction()
    {
        Fraction pi = Fraction.Zero;
        int maxN = 10;
        for (int k = 0; k < maxN; ++k)
        {
            Fraction par1 = new Fraction(1, (long)Math.Pow(16, k));
            pi += par1 * (new Fraction(4, 8 * k + 1) - new Fraction(2, 8 * k + 4) - new Fraction(1, 8 * k + 5) - new Fraction(1, 8 * k + 6));
            UnityEngine.Debug.Log(pi.ToString());
        }
        return pi.ToDouble();
    }
}

