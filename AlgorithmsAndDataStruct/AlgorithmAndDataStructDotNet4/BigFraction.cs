using System;
using System.Collections.Generic;

using System.Text;
//using UnityEngine;
using Numerics;
using System.Numerics;

/// <summary>
/// 分式辅助类
/// </summary>
struct BigFraction
{
    public static void TestFraction()
    {
        BigFraction a = new BigFraction(2, 3);
        BigFraction b = new BigFraction(3, 4);

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
        Console.WriteLine(0.1f + (float)a);
        Console.WriteLine(0.1d + (double)b);
    }

    /// <summary>
    /// 最大公约数，辗转相除法
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static BigRational GCD(BigRational a, BigRational b)
    {
        BigRational temp;          /*定义整型变量*/
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
    public static BigFraction Zero
    {
        get { return new BigFraction(0, 1); }
    }

    public static BigFraction Error
    {
        get { return new BigFraction(0, 1); }
    }

    public BigFraction(BigRational numerator, BigRational denominator)
    {
        BigRational GreatestCommonDivisor = GCD(numerator, denominator);
        m_numerator = numerator / GreatestCommonDivisor;
        m_denominator = denominator / GreatestCommonDivisor;
    }
    BigRational m_numerator; // 分子
    BigRational m_denominator; // 分母

    public static bool operator !=(BigFraction lhs, BigFraction rhs)
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

    public static bool operator ==(BigFraction lhs, BigFraction rhs)
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

    public static BigFraction operator +(BigFraction a, BigFraction b)
    {
        try
        {
            checked
            {
                BigRational numerator = a.m_numerator * b.m_denominator + a.m_denominator * b.m_numerator;

                BigRational denominator = a.m_denominator * b.m_denominator;

                return new BigFraction(numerator, denominator);
            }
        }
        catch(System.OverflowException ex)
        {
            Debug.LogException(ex);
            return Error;
        }
    }

    public static BigFraction operator +(BigFraction a, int b)
    {
        BigRational numerator = a.m_numerator + a.m_denominator * b;

        BigRational denominator = a.m_denominator;

        return new BigFraction(numerator, denominator);
    }

    public static BigFraction operator +(int b, BigFraction a)
    {
        BigRational numerator = a.m_denominator * b + a.m_numerator;

        BigRational denominator = a.m_denominator;

        return new BigFraction(numerator, denominator);
    }

    public static BigFraction operator -(BigFraction a, BigFraction b)
    {
        try
        {
            checked
            {
                BigRational numerator = a.m_numerator * b.m_denominator - a.m_denominator * b.m_numerator;

                BigRational denominator = a.m_denominator * b.m_denominator;

                return new BigFraction(numerator, denominator);
            }
        }
        catch(System.OverflowException ex)
        {
            Debug.LogException(ex);
            return Error;
        }
    }

    public static BigFraction operator -(BigFraction a, int b)
    {
        BigRational numerator = a.m_numerator - a.m_denominator * b;

        BigRational denominator = a.m_denominator;

        return new BigFraction(numerator, denominator);
    }

    public static BigFraction operator -(int b, BigFraction a)
    {
        BigRational numerator = a.m_denominator * b - a.m_numerator;

        BigRational denominator = a.m_denominator;

        return new BigFraction(numerator, denominator);
    }

    public static BigFraction operator *(BigFraction a, BigFraction b)
    {
        try
        {
            checked
            {
                BigRational numerator = a.m_numerator * b.m_numerator;

                BigRational denominator = a.m_denominator * b.m_denominator;

                return new BigFraction(numerator, denominator);
            }
        }
        catch (System.OverflowException ex)
        {
            Debug.LogException(ex);
            return Error;
        }

    }

    public static BigFraction operator *(BigFraction a, int b)
    {
        BigRational numerator = a.m_numerator * b;

        BigRational denominator = a.m_denominator;

        return new BigFraction(numerator, denominator);
    }

    public static BigFraction operator *(int b, BigFraction a)
    {
        BigRational numerator = b * a.m_numerator;

        BigRational denominator = a.m_denominator;

        return new BigFraction(numerator, denominator);
    }

    public static BigFraction operator /(BigFraction a, BigFraction b)
    {
        try
        {
            checked
            {
                BigRational numerator = a.m_numerator * b.m_denominator;

                BigRational denominator = a.m_denominator * b.m_numerator;

                return new BigFraction(numerator, denominator);
            }
        }
        catch (System.OverflowException ex)
        {
            Debug.LogException(ex);
            return Error;
        }

    }

    public static BigFraction operator /(BigFraction a, int b)
    {
        BigRational numerator = a.m_numerator;

        BigRational denominator = a.m_denominator * b;

        return new BigFraction(numerator, denominator);
    }

    public static BigFraction operator /(int b, BigFraction a)
    {
        BigRational numerator = b * a.m_denominator;

        BigRational denominator = a.m_numerator;

        return new BigFraction(numerator, denominator);
    }

    public override string ToString()
    {
        return ((BigInteger)m_numerator).ToString() + "/" + ((BigInteger)m_denominator).ToString();
    }

    public decimal ToDecimal()
    {
        return (decimal)m_numerator / (decimal)m_denominator;
    }

    public static explicit operator decimal(BigFraction d)
    {
        return d.ToDecimal();
    }

    public double ToDouble()
    {
        return (double)m_numerator / (double)m_denominator;
    }

    public static explicit operator double(BigFraction d)
    {
        return d.ToDouble();
    }

    public float ToFloat()
    {
        return (float)m_numerator / (float)m_denominator;
    }

    public static explicit operator float(BigFraction d)
    {
        return d.ToFloat();
    }

    public BigRational ToBigRational()
    {
        return m_numerator / m_denominator;
    }
}

