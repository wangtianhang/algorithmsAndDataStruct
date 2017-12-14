using System;
using System.Collections.Generic;

using System.Text;
//using UnityEngine;

/// <summary>
/// 分式辅助类
/// </summary>
struct Fraction
{
    public static void TestFraction()
    {
        Fraction a = new Fraction(2, 3);
        Fraction b = new Fraction(3, 4);

        // 测试加减乘除
        Console.WriteLine(a + b);
        Console.WriteLine(a - b);
        Console.WriteLine(a * b);
        Console.WriteLine(a / b);

        Console.WriteLine(a + 1);
        Console.WriteLine(a - 1);
        Console.WriteLine(a * 1);
        Console.WriteLine(a / 1);

        Console.WriteLine(1 + a);
        Console.WriteLine(1 - a);
        Console.WriteLine(1 * a);
        Console.WriteLine(1 / a);

        // 测试隐式转换
        Console.WriteLine(0.1f + a);
        Console.WriteLine(0.1d + b);
    }

    /// <summary>
    /// 最大公约数，辗转相除法
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static long GCD(long a, long b)
    {
        long temp;          /*定义整型变量*/
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

//     public Fraction()
//     {
//         m_numerator = 0;
//         m_denominator = 1;
//     }
    public static Fraction Zero
    {
        get { return new Fraction(0, 1); }
    }

    public static Fraction Error
    {
        get { return new Fraction(long.MinValue, 1); }
    }

    public Fraction(long numerator, long denominator)
    {
        long GreatestCommonDivisor = GCD(numerator, denominator);
        m_numerator = numerator / GreatestCommonDivisor;
        m_denominator = denominator / GreatestCommonDivisor;
    }
    long m_numerator; // 分子
    long m_denominator; // 分母

    public static bool operator != (Fraction lhs, Fraction rhs)
    {
        if (lhs.m_numerator != rhs.m_numerator
            || lhs.m_denominator != rhs.m_denominator)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator == (Fraction lhs, Fraction rhs)
    {
        if(lhs.m_numerator == rhs.m_numerator
            && lhs.m_denominator == rhs.m_denominator)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static Fraction operator + (Fraction a, Fraction b)
    {
        try
        {
            checked
            {
                long numerator = a.m_numerator * b.m_denominator + a.m_denominator * b.m_numerator;

                long denominator = a.m_denominator * b.m_denominator;

                return new Fraction(numerator, denominator);
            }
        }
        catch(System.OverflowException ex)
        {
            Debug.LogException(ex);
            return Error;
        }
    }

    public static Fraction operator +(Fraction a, int b)
    {
        long numerator = a.m_numerator + a.m_denominator * b;

        long denominator = a.m_denominator;

        return new Fraction(numerator, denominator);
    }

    public static Fraction operator +(int b, Fraction a)
    {
        long numerator = a.m_denominator * b + a.m_numerator;

        long denominator = a.m_denominator;

        return new Fraction(numerator, denominator);
    }

    public static Fraction operator - (Fraction a, Fraction b)
    {
        try
        {
            checked
            {
                long numerator = a.m_numerator * b.m_denominator - a.m_denominator * b.m_numerator;

                long denominator = a.m_denominator * b.m_denominator;

                return new Fraction(numerator, denominator);
            }
        }
        catch(System.OverflowException ex)
        {
            Debug.LogException(ex);
            return Error;
        }
    }

    public static Fraction operator -(Fraction a, int b)
    {
        long numerator = a.m_numerator - a.m_denominator * b;

        long denominator = a.m_denominator;

        return new Fraction(numerator, denominator);
    }

    public static Fraction operator -(int b, Fraction a)
    {
        long numerator = a.m_denominator * b - a.m_numerator;

        long denominator = a.m_denominator;

        return new Fraction(numerator, denominator);
    }

    public static Fraction operator * (Fraction a, Fraction b)
    {
        try
        {
            checked
            {
                long numerator = a.m_numerator * b.m_numerator;

                long denominator = a.m_denominator * b.m_denominator;

                return new Fraction(numerator, denominator);
            }
        }
        catch (System.OverflowException ex)
        {
            Debug.LogException(ex);
            return Error;
        }

    }

    public static Fraction operator *(Fraction a, int b)
    {
        long numerator = a.m_numerator * b;

        long denominator = a.m_denominator;

        return new Fraction(numerator, denominator);
    }

    public static Fraction operator *(int b, Fraction a)
    {
        long numerator = b * a.m_numerator;

        long denominator = a.m_denominator;

        return new Fraction(numerator, denominator);
    }

    public static Fraction operator / (Fraction a, Fraction b)
    {
        try
        {
            checked
            {
                long numerator = a.m_numerator * b.m_denominator;

                long denominator = a.m_denominator * b.m_numerator;

                return new Fraction(numerator, denominator);
            }
        }
        catch (System.OverflowException ex)
        {
            Debug.LogException(ex);
            return Error;
        }

    }

    public static Fraction operator /(Fraction a, int b)
    {
        long numerator = a.m_numerator;

        long denominator = a.m_denominator * b;

        return new Fraction(numerator, denominator);
    }

    public static Fraction operator /(int b, Fraction a)
    {
        long numerator = b * a.m_denominator;

        long denominator = a.m_numerator;

        return new Fraction(numerator, denominator);
    }

    public override string ToString()
    {
        return m_numerator.ToString() + "/" + m_denominator.ToString();
    }

    public double ToDouble()
    {
        return (double)m_numerator / (double)m_denominator;
    }

    public static implicit operator double(Fraction d)
    {
        return d.ToDouble();
    }

    public float ToFloat()
    {
        return (float)m_numerator / (float)m_denominator;
    }

    public static implicit operator float(Fraction d)
    {
        return d.ToFloat();
    }
}

