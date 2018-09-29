using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace FixPoint
{
    public struct FloatL
    {
        public const Int32 MaxValue = 2147483647;
        public const Int32 MinValue = -2147483648;

        public long m_numerator; // 分子
        public const long m_denominator = 100000L;
        public FloatL(float a)
        {
            m_numerator = (long)(a * m_denominator);
        }

        public FloatL(double a)
        {
            m_numerator = (long)(a * m_denominator);
        }

        public FloatL(int a)
        {
            m_numerator = a * m_denominator;
        }

        public static implicit operator FloatL(int n)
        {
            FloatL ret = new FloatL(n);
            return ret;
        }

        public static implicit operator FloatL(float n)
        {
            FloatL ret = new FloatL(n);
            return ret;
        }

        public static implicit operator FloatL(double n)
        {
            FloatL ret = new FloatL(n);
            return ret;
        }

        public static FloatL operator -(FloatL a)
        {
            FloatL ret = new FloatL();
            ret.m_numerator = -a.m_numerator;
            return ret;
        }

        public static bool operator !=(FloatL lhs, FloatL rhs)
        {
            return lhs.m_numerator != rhs.m_numerator;
        }

        public static bool operator ==(FloatL lhs, FloatL rhs)
        {
            return lhs.m_numerator == rhs.m_numerator;
        }

        public static bool operator > (FloatL lhs, FloatL rhs)
        {
            return lhs.m_numerator > rhs.m_numerator;
        }

        public static bool operator <(FloatL lhs, FloatL rhs)
        {
            return lhs.m_numerator < rhs.m_numerator;
        }

        public static bool operator >= (FloatL lhs, FloatL rhs)
        {
            return lhs.m_numerator >= rhs.m_numerator;
        }

        public static bool operator <=(FloatL lhs, FloatL rhs)
        {
            return lhs.m_numerator <= rhs.m_numerator;
        }

        public static FloatL operator +(FloatL a, FloatL b)
        {
            FloatL ret = new FloatL();
            ret.m_numerator = a.m_numerator + b.m_numerator;
            return ret;
        }

        public static FloatL operator -(FloatL a, FloatL b)
        {
            FloatL ret = new FloatL();
            ret.m_numerator = a.m_numerator - b.m_numerator;
            return ret;
        }

        public static FloatL operator *(FloatL a, FloatL b)
        {
            FloatL ret = new FloatL();
            ret.m_numerator = ((a.m_numerator * b.m_numerator) / m_denominator);
            return ret;
        }

        public static FloatL operator /(FloatL a, FloatL b)
        {
            FloatL ret = new FloatL();
            // 可以应对小数除大数时不为0
            ret.m_numerator = ((a.m_numerator * m_denominator) / b.m_numerator);
            return ret;
        }

        public float ToFloat()
        {
            // 据说各平台double降级为float会比较一致 有待实际测试
            return (float)((double)m_numerator / (double)m_denominator);
        }

        public double ToDouble()
        {
            return (double)m_numerator / (double)m_denominator;
        }

        public int ToInt()
        {
            return (int)(m_numerator / m_denominator);
        }

        public override string ToString()
        {
            double ret = (double)m_numerator / m_denominator;
            return ret.ToString("f6");
        }

        public override int GetHashCode()
        {
            return m_numerator.GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (!(other is FloatL))
            {
                return false;
            }
            FloatL otherFloatL = (FloatL)other;
            return this.m_numerator == otherFloatL.m_numerator;
        }
    }
}


