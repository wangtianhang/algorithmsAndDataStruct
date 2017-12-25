using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

public delegate double FunctionOfOneVariableD(double x);
public delegate float FunctionOfOneVariable(float x);

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
        Debug.Log(InvertFunc((x) => x * x, 3, 1).ToString());
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
    /// 求导数
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    
    public static float Derivative(FunctionOfOneVariable function, float x, float delta = 0.0001f)
    {
        return (function(x + delta) - function(x)) / delta;
    }

    public static double DerivativeD(FunctionOfOneVariableD function, double x, double delta = 0.0001d)
    {
        return (function(x + delta) - function(x)) / delta;
    }

    /// <summary>
    /// 反函数
    /// </summary>
    /// <param name="function"></param>
    /// <param name="endY"></param>
    /// <param name="beginX"></param>
    /// <param name="delta"></param>
    /// <returns></returns>
    public static double InvertFunc(FunctionOfOneVariableD function, double endY, double beginX, double delta = 0.0001d)
    {
        double t1 = beginX; 
        double t2;

        // 牛顿切线法求解L(t1) = L(1.0) * percent;
        // Xn+1 = Xn - (L(xn) - L(1.0) * percent / L'(xn)) 
        do
        {
            t2 = t1 - (function(t1) - endY) / DerivativeD(function, t1);

            if (Math.Abs(t1 - t2) < 0.000001)
                break;

            t1 = t2;

        } while (true);

        return t2;
    }

    /// <summary>
    /// 三分法计算极值
    /// </summary>
    /// <param name="function"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static double GetMinByThree(FunctionOfOneVariableD function, double left, double right)
    {
        do
        {
            double yLeft = function(left);
            double yRight = function(right);
            double mid = (left + right) / 2;
            double yMid = function(mid);
            double midMid = (left + mid) / 2;
            double yMidMid = function(midMid);
            if (yMidMid > yMid)
            {
                left = midMid;
            }
            else
            {
                right = mid;
            }

            if (Math.Abs(left - right) < 0.001d)
            {
                return left;
            }

        }while(true);
    }

    public static double GetMaxByThree(FunctionOfOneVariableD function, double left, double right)
    {
        do
        {
            double yLeft = function(left);
            double yRight = function(right);
            double mid = (left + right) / 2;
            double yMid = function(mid);
            double midMid = (left + mid) / 2;
            double yMidMid = function(midMid);
            if (yMidMid < yMid)
            {
                left = midMid;
            }
            else
            {
                right = mid;
            }

            if (Math.Abs(left - right) < 0.001d)
            {
                return left;
            }

        } while (true);
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



//     public static double TestFunc(double x)
//     {
//         return 1 / Math.Sqrt(4 - x * x);
//     }
}

