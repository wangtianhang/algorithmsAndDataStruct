using System;
using System.Collections.Generic;
using System.Text;


public struct Mathf
{
    public const float Deg2Rad = 0.0174533f;
    public const float Epsilon = 1.4013e-045f;
    public const float Infinity = 1.0f / 0.0f;
    public const float NegativeInfinity = -1.0f / 0.0f;
    public const float PI = 3.14159f;
    public const float Rad2Deg = 57.2958f;

    public static float Clamp(float value, float min, float max)
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

    public static int Clamp(int value, int min, int max)
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

    public static float Clamp01(float value)
    {
        if (value < 0f)
        {
            return 0f;
        }
        if (value > 1f)
        {
            return 1f;
        }
        return value;
    }

    public static float Lerp(float from, float to, float t)
    {
        return from + (to - from) * Mathf.Clamp01(t);
    }

    public static float Abs(float f)
    {
        return Math.Abs(f);
    }
    public static int Abs(int value)
    {
        return Math.Abs(value);
    }

    public static float Acos(float f)
    {
        return (float)Math.Acos((double)f);
    }

    public static bool Approximately(float a, float b)
    {
        return Mathf.Abs(b - a) < Mathf.Max(1E-06f * Mathf.Max(Mathf.Abs(a), Mathf.Abs(b)), 1.121039E-44f);
    }

    public static float Max(float a, float b)
    {
        return (a <= b) ? b : a;
    }

    public static float Max(params float[] values)
    {
        int num = values.Length;
        if (num == 0)
        {
            return 0f;
        }
        float num2 = values[0];
        for (int i = 1; i < num; i++)
        {
            if (values[i] > num2)
            {
                num2 = values[i];
            }
        }
        return num2;
    }

    public static int Max(int a, int b)
    {
        return (a <= b) ? b : a;
    }

    public static int Max(params int[] values)
    {
        int num = values.Length;
        if (num == 0)
        {
            return 0;
        }
        int num2 = values[0];
        for (int i = 1; i < num; i++)
        {
            if (values[i] > num2)
            {
                num2 = values[i];
            }
        }
        return num2;
    }

    public static float Min(float a, float b)
    {
        return (a >= b) ? b : a;
    }

    public static float Min(params float[] values)
    {
        int num = values.Length;
        if (num == 0)
        {
            return 0f;
        }
        float num2 = values[0];
        for (int i = 1; i < num; i++)
        {
            if (values[i] < num2)
            {
                num2 = values[i];
            }
        }
        return num2;
    }

    public static int Min(int a, int b)
    {
        return (a >= b) ? b : a;
    }

    public static int Min(params int[] values)
    {
        int num = values.Length;
        if (num == 0)
        {
            return 0;
        }
        int num2 = values[0];
        for (int i = 1; i < num; i++)
        {
            if (values[i] < num2)
            {
                num2 = values[i];
            }
        }
        return num2;
    }
}

