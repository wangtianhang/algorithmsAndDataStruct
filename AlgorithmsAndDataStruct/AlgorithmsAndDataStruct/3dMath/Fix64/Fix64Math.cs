using System;
using System.Collections.Generic;
using System.Text;

namespace FixMath.NET
{
    class Fix64Math
    {
        public const double Deg2Rad = 0.0174533d;
        public const double Epsilon = 1.4013e-045d;
        public const double Infinity = 1.0d / 0.0d;
        public const double NegativeInfinity = -1.0d / 0.0d;
        public const double PI = 3.14159d;
        public const double Rad2Deg = 57.2958d;

        public static Fix64 Abs(Fix64 f)
        {
            //Fix64 ret = f;
            if (f < 0)
            {
                return -f;
            }
            return f;
        }

        public static Fix64 Acos(Fix64 f)
        {
            //return Math.Acos(f.ToDouble());
            return Fix64.Acos(f);
        }

        public static Fix64 Asin(Fix64 f)
        {
            //return Math.Asin(f.ToDouble());
            throw new NotImplementedException();
        }

        public static Fix64 Atan(Fix64 f)
        {
            //return Math.Atan(f.ToDouble());
            return Fix64.Atan(f);
        }

        public static Fix64 Atan2(Fix64 y, Fix64 x)
        {
            //return Math.Atan2(y.ToDouble(), x.ToDouble());
            return Fix64.Atan2(y, x);
        }

        public static int CeilToInt(Fix64 f)
        {
            //return (int)((f.m_numerator + FloatL.m_denominator - 1) / FloatL.m_denominator);
            //return (int)Math.Ceiling((double)f);
            return (int)Fix64.Ceiling(f);
        }

        public static Fix64 Clamp(Fix64 value, Fix64 min, Fix64 max)
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

        public static Fix64 Clamp01(Fix64 value)
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

        public static Fix64 Cos(Fix64 f)
        {
            //return Math.Cos((double)f);
            return Fix64.Cos(f);
        }

        public static Fix64 DeltaAngle(Fix64 current, Fix64 target)
        {
            Fix64 num = Repeat(target - current, 360f);
            if (num > 180f)
            {
                num -= 360f;
            }
            return num;
        }

        public static Fix64 Exp(Fix64 power)
        {
            //return Math.Exp(power.ToDouble());
            throw new NotImplementedException();
        }

        public static Fix64 Floor(Fix64 f)
        {
            //return Math.Floor(f.ToDouble());
            return Fix64.Floor(f);
        }

        public static int FloorToInt(Fix64 f)
        {
            //return f.ToInt();
            return (int)Fix64.Floor(f);
        }

        public static Fix64 InverseLerp(Fix64 from, Fix64 to, Fix64 value)
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

        public static Fix64 Lerp(Fix64 from, Fix64 to, Fix64 t)
        {
            return from + (to - from) * Clamp01(t);
        }

        public static Fix64 LerpAngle(Fix64 a, Fix64 b, Fix64 t)
        {
            Fix64 num = Repeat(b - a, 360f);
            if (num > 180f)
            {
                num -= 360f;
            }
            return a + num * Clamp01(t);
        }

        public static Fix64 Log(Fix64 f)
        {
            //return Math.Log(f.ToDouble());
            //return Fix64.Log2(f);
            throw new NotImplementedException();
        }

        public static Fix64 Log(Fix64 f, Fix64 p)
        {
            //return Math.Log(f.ToDouble(), p.ToDouble());
            throw new NotImplementedException();
        }

        public static Fix64 Log10(Fix64 f)
        {
            //return Math.Log10(f.ToDouble());
            throw new NotImplementedException();
        }

        public static Fix64 Max(Fix64 a, Fix64 b)
        {
            return (a <= b) ? b : a;
        }

        public static Fix64 Max(params Fix64[] values)
        {
            int num = values.Length;
            if (num == 0)
            {
                return 0f;
            }
            Fix64 num2 = values[0];
            for (int i = 1; i < num; i++)
            {
                if (values[i] > num2)
                {
                    num2 = values[i];
                }
            }
            return num2;
        }

        public static Fix64 Min(Fix64 a, Fix64 b)
        {
            return (a >= b) ? b : a;
        }

        public static Fix64 Min(params Fix64[] values)
        {
            int num = values.Length;
            if (num == 0)
            {
                return 0f;
            }
            Fix64 num2 = values[0];
            for (int i = 1; i < num; i++)
            {
                if (values[i] < num2)
                {
                    num2 = values[i];
                }
            }
            return num2;
        }

        public static Fix64 MoveTowards(Fix64 current, Fix64 target, Fix64 maxDelta)
        {
            if (Abs(target - current) <= maxDelta)
            {
                return target;
            }
            return current + Sign(target - current) * maxDelta;
        }

        public static Fix64 MoveTowardsAngle(Fix64 current, Fix64 target, Fix64 maxDelta)
        {
            target = current + DeltaAngle(current, target);
            return MoveTowards(current, target, maxDelta);
        }

        public static Fix64 PingPong(Fix64 t, Fix64 length)
        {
            t = Repeat(t, length * 2f);
            return length - Abs(t - length);
        }

        public static Fix64 Pow(Fix64 f, Fix64 p)
        {
            //return Math.Pow(f.ToDouble(), p.ToDouble());
            return Fix64.Pow(f, p);
        }

        public static Fix64 Repeat(Fix64 t, Fix64 length)
        {
            return t - Floor(t / length) * length;
        }

        public static Fix64 Round(Fix64 f)
        {
            //return Math.Round(f.ToDouble());
            return Fix64.Round(f);
        }

        public static int RoundToInt(Fix64 f)
        {
            //return (int)Math.Round(f.ToDouble());
            return (int)Fix64.Round(f);
        }

        public static Fix64 Sign(Fix64 f)
        {
            return (f < 0f) ? -1f : 1f;
        }

        public static Fix64 Sin(Fix64 f)
        {
            //return Math.Sin(f.ToDouble());
            return Fix64.Sin(f);
        }
        /// <summary>
        /// 牛顿法求平方根
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Fix64 Sqrt(Fix64 c)
        {
            if(c == 0)
            {
                return 0;
            }

            if (c < new Fix64(0))
            {
                return new Fix64(-1);
            }

            Fix64 err = (Fix64)(0.01f);
            Fix64 t = c;
            int count = 0;
            while (Abs(t - c / t) > err * t)
            {
                count++;
                t = (c / t + t) / (Fix64)(2.0f);
                if(count >= 100)
                {
                    UnityEngine.Debug.LogError("FixPoint Sqrt " + c);
                    break;
                }
            }
            return t;
        }

        public static Fix64 Tan(Fix64 f)
        {
            //return Math.Tan(f.ToDouble());
            return Fix64.Tan(f);
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
