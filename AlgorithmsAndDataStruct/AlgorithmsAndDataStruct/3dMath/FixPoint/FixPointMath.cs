//using UnityEngine;
using System.Collections;
using System;

namespace FixPoint
{
    public class FixPointMath
    {
        public const double Deg2Rad = 0.0174533d;
        public const double Epsilon = 1.4013e-045d;
        public const double Infinity = 1.0d / 0.0d;
        public const double NegativeInfinity = -1.0d / 0.0d;
        public const double PI = 3.14159d;
        public const double Rad2Deg = 57.2958d;

        public static FloatL Abs(FloatL f)
        {
            FloatL ret = f;
            if (ret.m_numerator < 0)
            {
                ret.m_numerator = -ret.m_numerator;
            }
            return ret;
        }

        public static FloatL Acos(FloatL f)
        {
            return Math.Acos(f.ToDouble());
        }

        public static FloatL Asin(FloatL f)
        {
            return Math.Asin(f.ToDouble());
        }

        public static FloatL Atan(FloatL f)
        {
            return Math.Atan(f.ToDouble());
        }

        public static FloatL Atan2(FloatL y, FloatL x)
        {
            return Math.Atan2(y.ToDouble(), x.ToDouble());
        }

        public static int CeilToInt(FloatL f)
        {
            return (int)((f.m_numerator + FloatL.m_denominator - 1) / FloatL.m_denominator);
        }

        public static FloatL Clamp(FloatL value, FloatL min, FloatL max)
        {
            if (value < min)
            {
                value = min;
            }
            else if (value > max)
            {
                value = max;
            }
            return value;
        }

        public static FloatL Clamp01(FloatL value)
        {
            if (value < 0)
            {
                return 0;
            }
            if (value > 1)
            {
                return 1;
            }
            return value;
        }

        public static FloatL Cos(FloatL f)
        {
            return Math.Cos(f.ToDouble());
        }

        public static FloatL DeltaAngle(FloatL current, FloatL target)
        {
            FloatL num = FixPointMath.Repeat(target - current, 360f);
            if (num > 180f)
            {
                num -= 360f;
            }
            return num;
        }

        public static FloatL Exp(FloatL power)
        {
            return Math.Exp(power.ToDouble());
        }

        public static FloatL Floor(FloatL f)
        {
            return Math.Floor(f.ToDouble());
        }

        public static int FloorToInt(FloatL f)
        {
            return f.ToInt();
        }

        public static FloatL InverseLerp(FloatL from, FloatL to, FloatL value)
        {
            if (from < to)
            {
                if (value < from)
                {
                    return 0f;
                }
                if (value > to)
                {
                    return 1f;
                }
                value -= from;
                value /= to - from;
                return value;
            }
            else
            {
                if (from <= to)
                {
                    return 0f;
                }
                if (value < to)
                {
                    return 1f;
                }
                if (value > from)
                {
                    return 0f;
                }
                return 1f - (value - to) / (from - to);
            }
        }

        public static FloatL Lerp(FloatL from, FloatL to, FloatL t)
        {
            return from + (to - from) * FixPointMath.Clamp01(t);
        }

        public static FloatL LerpAngle(FloatL a, FloatL b, FloatL t)
        {
            FloatL num = FixPointMath.Repeat(b - a, 360f);
            if (num > 180f)
            {
                num -= 360f;
            }
            return a + num * FixPointMath.Clamp01(t);
        }

        public static FloatL Log(FloatL f)
        {
            return Math.Log(f.ToDouble());
        }

        public static FloatL Log(FloatL f, FloatL p)
        {
            return Math.Log(f.ToDouble(), p.ToDouble());
        }

        public static FloatL Log10(FloatL f)
        {
            return Math.Log10(f.ToDouble());
        }

        public static FloatL Max(FloatL a, FloatL b)
        {
            return (a <= b) ? b : a;
        }

        public static FloatL Max(params FloatL[] values)
        {
            int num = values.Length;
            if (num == 0)
            {
                return 0f;
            }
            FloatL num2 = values[0];
            for (int i = 1; i < num; i++)
            {
                if (values[i] > num2)
                {
                    num2 = values[i];
                }
            }
            return num2;
        }

        public static FloatL Min(FloatL a, FloatL b)
        {
            return (a >= b) ? b : a;
        }

        public static FloatL Min(params FloatL[] values)
        {
            int num = values.Length;
            if (num == 0)
            {
                return 0f;
            }
            FloatL num2 = values[0];
            for (int i = 1; i < num; i++)
            {
                if (values[i] < num2)
                {
                    num2 = values[i];
                }
            }
            return num2;
        }

        public static FloatL MoveTowards(FloatL current, FloatL target, FloatL maxDelta)
        {
            if (FixPointMath.Abs(target - current) <= maxDelta)
            {
                return target;
            }
            return current + FixPointMath.Sign(target - current) * maxDelta;
        }

        public static FloatL MoveTowardsAngle(FloatL current, FloatL target, FloatL maxDelta)
        {
            target = current + FixPointMath.DeltaAngle(current, target);
            return FixPointMath.MoveTowards(current, target, maxDelta);
        }

        public static FloatL PingPong(FloatL t, FloatL length)
        {
            t = FixPointMath.Repeat(t, length * 2f);
            return length - FixPointMath.Abs(t - length);
        }

        public static FloatL Pow(FloatL f, FloatL p)
        {
            return Math.Pow(f.ToDouble(), p.ToDouble());
        }

        public static FloatL Repeat(FloatL t, FloatL length)
        {
            return t - FixPointMath.Floor(t / length) * length;
        }

        public static FloatL Round(FloatL f)
        {
            return Math.Round(f.ToDouble());
        }

        public static int RoundToInt(FloatL f)
        {
            return (int)Math.Round(f.ToDouble());
        }

        public static FloatL Sign(FloatL f)
        {
            return (f < 0f) ? -1f : 1f;
        }

        public static FloatL Sin(FloatL f)
        {
            return Math.Sin(f.ToDouble());
        }
        /// <summary>
        /// 牛顿法求平方根
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static FloatL Sqrt(FloatL c)
        {
            if(c == 0)
            {
                return 0;
            }

            if (c < new FloatL(0))
            {
                return new FloatL(-1);
            }

            FloatL err = new FloatL(0.01f);
            FloatL t = c;
            int count = 0;
            while (FixPointMath.Abs(t - c / t) > err * t)
            {
                count++;
                t = (c / t + t) / new FloatL(2.0f);
                if(count >= 100)
                {
                    Debug.LogError("FixPoint Sqrt " + c);
                    break;
                }
            }
            return t;
        }

        public static FloatL Tan(FloatL f)
        {
            return Math.Tan(f.ToDouble());
        }

        public static int ClosestPowerOfTwo(int n)
        {
            int v = n;
            v--;
            v |= v >> 1;
            v |= v >> 2;
            v |= v >> 4;
            v |= v >> 8;
            v |= v >> 16;
            v++;

            int x = v >> 1;
            return (v - n) > (n - x) ? x : v;
        }

        public static bool IsPowerOfTwo(int n)
        {
            if (n <= 0)
            {
                return false;
            }
            else
            {
                return (n & (n - 1)) == 0;
            }
        }
    }
}
