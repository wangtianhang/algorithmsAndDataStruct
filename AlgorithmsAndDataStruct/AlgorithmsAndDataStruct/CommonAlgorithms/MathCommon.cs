﻿using System;
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

//     public static double TestFunc(double x)
//     {
//         return 1 / Math.Sqrt(4 - x * x);
//     }
}

