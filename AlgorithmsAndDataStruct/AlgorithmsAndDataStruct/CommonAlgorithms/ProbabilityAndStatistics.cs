using System;
using System.Collections.Generic;
using System.Text;


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
    /// 回归线性方程
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
        Debug.Log("y = " + a + " + " + b + " * x");
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
        Debug.Log("y = " + a + " + x ^ " + b);
    }
}

