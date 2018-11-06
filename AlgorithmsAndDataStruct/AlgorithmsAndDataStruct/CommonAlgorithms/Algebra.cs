using System;
using System.Collections.Generic;
using System.Text;

public delegate double FunctionOfOneVariableD(double x);
//public delegate float FunctionOfOneVariable(float x);

public class Algebra
{
    public static void Test()
    {
        UnityEngine.Debug.Log(InvertFunc((x) => x * x, 3, 1).ToString());
    }

    /// <summary>
    /// 求导数
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>

//     public static float Derivative(FunctionOfOneVariable function, float x, float delta = 0.0001f)
//     {
//         return (function(x + delta) - function(x)) / delta;
//     }

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

        } while (true);
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
    /// 一元二次方程组 ax2+bx+c=0;
    /// </summary>
    /// <returns></returns>
    public List<float> ResultOfQuadraticEquations(float a, float b, float c)
    {
        List<float> ret = new List<float>();
        float tmp = b * b - 4 * a * c;
        if (tmp > 0)
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

    // 二分法求根 需要单调连续区间
    // Prints root of func(x)  
    // with error of EPSILON 
    static void BisectionMethod(double left, double right, System.Func<double, double> func)
    {
        if (func(left) * func(right) >= 0)
        {
            Console.WriteLine("You have not assumed" +
                                    " right a and b");
            return;
        }

        double c = left;
        while ((right - left) >= float.Epsilon)
        {
            // Find middle point 
            c = (left + right) / 2;

            // Check if middle  
            // point is root 
            if (func(c) == 0.0)
                break;

            // Decide the side  
            // to repeat the steps 
            else if (func(c) * func(left) < 0)
                right = c;
            else
                left = c;
        }

        // prints value of c  
        // upto 4 decimal places 
        Console.WriteLine("The value of " +
                          "root is : " + c);
    } 
}

