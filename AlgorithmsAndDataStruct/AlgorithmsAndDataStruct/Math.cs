using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class MathCollection
{
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
    /// 找到一组数中第k小元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static T Select<T>(T[] a, int k)
    {
        Random2.Shuffle<T>(a);
        int lo = 0, hi = a.Length - 1;
        while(hi > lo)
        {
            int j = QuickSort<T>.Partition(a, lo, hi);
            if(j == k)
            {
                return a[k];
            }
            else if(j > k)
            {
                hi = j - 1;
            }
            else if(j < k)
            {
                lo = j + 1;
            }
        }

        return a[k];
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

    public static Vector3 GetIntersectionPoint(Vector3 rayOrigin, Vector3 rayDir, Vector3 planeNormal, Vector3 planeOnePoint)
    {
        float t = (Vector3.Dot(planeNormal, planeOnePoint) - Vector3.Dot(planeNormal, rayOrigin))
            / (Vector3.Dot(planeNormal, rayDir));
        Vector3 intersectionPoint = rayOrigin + rayDir * t;
        return intersectionPoint;
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
}

