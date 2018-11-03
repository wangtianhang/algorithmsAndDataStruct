using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ProbabilityAndStatistics
{
    public static void Test()
    {
        List<Vector2Double> pointList = new List<Vector2Double>();
        pointList.Add(new Vector2Double(0, 6));
        pointList.Add(new Vector2Double(3, 8));
        pointList.Add(new Vector2Double(6, 10));
        pointList.Add(new Vector2Double(7, 13));
        pointList.Add(new Vector2Double(9, 15));
        pointList.Add(new Vector2Double(13, 20));
        pointList.Add(new Vector2Double(17, 22));
        pointList.Add(new Vector2Double(19, 25));
        pointList.Add(new Vector2Double(23, 29));
        pointList.Add(new Vector2Double(27, 32));
        pointList.Add(new Vector2Double(30, 33));
        double a = 0;
        double b = 0;
        LinearRegressionEquation(pointList, out a, out b);

        List<Vector2Double> pointList2 = new List<Vector2Double>();
        pointList2.Add(new Vector2Double(0.2, 1.8));
        pointList2.Add(new Vector2Double(0.65, 3.6));
        pointList2.Add(new Vector2Double(1.13, 5.4));
        pointList2.Add(new Vector2Double(2.55, 7.2));
        pointList2.Add(new Vector2Double(4.0, 9.0));
        pointList2.Add(new Vector2Double(5.75, 10.8));
        pointList2.Add(new Vector2Double(7.8, 12.6));
        pointList2.Add(new Vector2Double(10.2, 14.4));
        pointList2.Add(new Vector2Double(12.9, 16.2));
        pointList2.Add(new Vector2Double(16.0, 18.0));
        pointList2.Add(new Vector2Double(18.4, 19.8));
        PowerRegressionEquation(pointList2, out a, out b);

        List<Vector2Double> pointList3 = new List<Vector2Double>();
        pointList3.Add(new Vector2Double(20, 42));
        pointList3.Add(new Vector2Double(25, 56));
        pointList3.Add(new Vector2Double(30, 73.5));
        pointList3.Add(new Vector2Double(35, 91.5));
        pointList3.Add(new Vector2Double(40, 116));
        pointList3.Add(new Vector2Double(45, 142.5));
        pointList3.Add(new Vector2Double(50, 173));
        pointList3.Add(new Vector2Double(55, 209.5));
        pointList3.Add(new Vector2Double(60, 248));
        pointList3.Add(new Vector2Double(65, 292.5));
        pointList3.Add(new Vector2Double(70, 343));
        pointList3.Add(new Vector2Double(75, 401));
        pointList3.Add(new Vector2Double(80, 464));
        double c = 0;
        QuadraticPolynomialRegressionEquation(pointList3, out a, out b, out c);
        double x = 72;
        double length = a * x * x + b * x + c;
        Debug.Log("length 72 " + length);
        x = 85;
        length = a * x * x + b * x + c;
        Debug.Log("length 85 " + length);

        Debug.Log(CalculateCompoundInterest(100, 0.055, 20).ToString());

        List<Vector2Double> pointList4 = new List<Vector2Double>();
        pointList4.Add(new Vector2Double(1950 - 1900, 25.8));
        pointList4.Add(new Vector2Double(1960 - 1900, 34.9));
        pointList4.Add(new Vector2Double(1970 - 1900, 48.2));
        pointList4.Add(new Vector2Double(1980 - 1900, 66.8));
        pointList4.Add(new Vector2Double(1990 - 1900, 81.1));
        ExponentRegressionEquation(pointList4, out a, out b);

        List<Vector2Double> pointList5 = new List<Vector2Double>();
        pointList5.Add(new Vector2Double(1960 - 1900, 20.56));
        pointList5.Add(new Vector2Double(1970 - 1900, 42.10));
        pointList5.Add(new Vector2Double(1990 - 1900, 70.10));
        LogarithmRegressionEquation(pointList5, out a, out b);
    }

    /// <summary>
    /// 获得a概率10% 获得b概率20% 获得a和b需要进行多少次试验
    /// </summary>
    static void ProbabilityAB()
    {
        int totalCount = 0;
        System.Random random = new System.Random();
        for (int i = 0; i < 1000000; ++i)
        {
            int a = 0;
            int b = 0;
            while (a == 0 || b == 0)
            {
                int randomA = random.Next(0, 100);
                if (randomA < 10)
                {
                    a++;
                }
                //System.Random random2 = new System.Random();
                int randomB = random.Next(0, 100);
                if (randomB < 20)
                {
                    b++;
                }
                totalCount++;
            }
        }
        float perCount = (float)totalCount / 1000000;
        Debug.Log("ProbabilityAB 如果与11.4近似说明随机数发生器符合正态分布 " + perCount);
    }

    /// <summary>
    /// 线性回归
    /// 最小二乘法
    /// y = a + bx
    /// </summary>
    static void LinearRegressionEquation(List<Vector2Double> pointList, out double a, out double b)
    {
        double averageX = 0;
        double averageY = 0;
        double total1 = 0;
        double total2 = 0;
        for (int i = 0; i < pointList.Count; ++i)
        {
            averageX += pointList[i].x;
            averageY += pointList[i].y;

            total1 += pointList[i].x * pointList[i].y;
            total2 += pointList[i].x * pointList[i].x;
        }
        averageX /= pointList.Count;
        averageY /= pointList.Count;

        b = (total1 - pointList.Count * averageX * averageY) / (total2 - pointList.Count * averageX * averageX);
        a = averageY - b * averageX;
        Debug.Log("线性回归 y = " + a + " + " + b + " * x");
    }

    /// <summary>
    /// 幂函数回归
    /// y = a * x ^ b;
    /// lnY = lnA + b * lnX
    /// 令 y' = lnY    x' = lnX   a' = lna
    /// y' = a ' + b * x'
    /// </summary>
    /// <param name="pointList"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    static void PowerRegressionEquation(List<Vector2Double> pointList, out double a, out double b)
    {
        List<Vector2Double> lnPointList = new List<Vector2Double>();
        foreach(var iter in pointList)
        {
            lnPointList.Add(new Vector2Double(Math.Log(iter.x), Math.Log(iter.y)));
        }
        double lnA = 0;
        LinearRegressionEquation(lnPointList, out lnA, out b);
        a = Math.Pow(Math.E, lnA);
        Debug.Log("幂函数回归 y = " + a + " + x ^ " + b);
    }

    class TwoUnknonwnEquation
    {
        public TwoUnknonwnEquation(double x2, double x1, double y)
        {
            m_x2 = x2;
            m_x1 = x1;
            m_y = y;
        }
        public double m_x2 = 0;
        public double m_x1 = 0;
        public double m_y = 0;
    }

    /// <summary>
    /// 一元二次多项式回归
    /// 转为一次二元回归
    /// </summary>
    /// <param name="pointList"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    static void QuadraticPolynomialRegressionEquation(List<Vector2Double> pointList, out double a, out double b, out double c)
    {
        List<TwoUnknonwnEquation> twoUnknownEquationList = new List<TwoUnknonwnEquation>();
        foreach (var iter in pointList)
        {
            twoUnknownEquationList.Add(new TwoUnknonwnEquation(iter.x * iter.x, iter.x, iter.y));
        }
        TwoUnknonwnRegressionEquation(twoUnknownEquationList, out a, out b, out c);
        Debug.Log("一元二次多项式回归 y = " + a + " *  x ^ 2 + " + b + " *  x + " + c);
    }

    /// <summary>
    /// 二元一次回归方程
    /// y = beta0 + beta1 * x1 + beta2 * x2;
    /// </summary>
    /// <param name="twoUnknownEquationList"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    static void TwoUnknonwnRegressionEquation(List<TwoUnknonwnEquation> twoUnknownEquationList, out double beta2, out double beta1, out double beta0)
    {
        double sum_y_x2 = 0;
        double sum_x1_x1 = 0;
        double sum_y_x1 = 0;
        double sum_x1_x2 = 0;
        double sum_x2_x2 = 0;
        double averageY = 0;
        double averageX1 = 0;
        double avergaeX2 = 0;
        foreach(var iter in twoUnknownEquationList)
        {
            sum_y_x2 += iter.m_y * iter.m_x2;
            sum_x1_x1 += iter.m_x1 * iter.m_x1;
            sum_y_x1 += iter.m_y * iter.m_x1;
            sum_x1_x2 += iter.m_x1 * iter.m_x2;
            sum_x2_x2 += iter.m_x2 * iter.m_x2;

            averageY += iter.m_y;
            averageX1 += iter.m_x1;
            avergaeX2 += iter.m_x2;
        }
        averageY /= twoUnknownEquationList.Count;
        averageX1 /= twoUnknownEquationList.Count;
        avergaeX2 /= twoUnknownEquationList.Count;

        beta2 = (sum_y_x2 * sum_x1_x1 - sum_y_x1 * sum_x1_x2) / (sum_x1_x1 * sum_x2_x2 - sum_x1_x2 * sum_x1_x2);
        beta1 = (sum_y_x1 * sum_x2_x2 - sum_y_x2 * sum_x1_x2) / (sum_x1_x1 * sum_x2_x2 - sum_x1_x2 * sum_x1_x2);

        beta0 = averageY - beta1 * averageX1 - beta2 * avergaeX2;

        Debug.Log("二元一次回归方程 y = " + beta2 + " *  x2 + " + beta1 + " * x1 + " + beta0);
    }

    /// <summary>
    /// 指数回归方程
    /// y = a * b ^ x
    /// lnY = lnA + x * lnB
    /// y' = a' + b' x
    /// </summary>
    /// <param name="pointList"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    static void ExponentRegressionEquation(List<Vector2Double> pointList, out double a, out double b)
    {
        List<Vector2Double> linePointList = new List<Vector2Double>();
        foreach(var iter in pointList)
        {
            linePointList.Add(new Vector2Double(iter.x, Math.Log(iter.y)));
        }
        double lnB = 0;
        double lnA = 0;
        LinearRegressionEquation(linePointList, out lnA, out lnB);
        a = Math.Pow(Math.E, lnA);
        b = Math.Pow(Math.E, lnB);

        Debug.Log("指数回归方程 y = " + a + " * " + b + " ^ x");
    }

    /// <summary>
    /// 对数回归方程
    /// y = a + b * lnX
    /// y = a + b * x'
    /// </summary>
    /// <param name="pointList"></param>
    static void LogarithmRegressionEquation(List<Vector2Double> pointList, out double a, out double b)
    {
        List<Vector2Double> linePointList = new List<Vector2Double>();
        foreach (var iter in pointList)
        {
            linePointList.Add(new Vector2Double(Math.Log(iter.x), iter.y));
        }
        LinearRegressionEquation(linePointList, out a, out b);
        Debug.Log("对数回归方程 y = " + a + " + " + b + " * lnX");
    }

    /// <summary>
    /// 复利计算
    /// y = p * power(e, r * t)
    /// p 本金 r 利率 t 时间
    /// </summary>
    public static double CalculateCompoundInterest(double p, double r, double t)
    {
        return p * Math.Pow(Math.E, r * t);
    }
}

