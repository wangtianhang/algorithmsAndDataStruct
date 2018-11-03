using System;
using System.Collections.Generic;
using System.Text;


public partial class FixPointMath
{
//     public static UnityEngine.Vector3 Convert(Vector3L vec)
//     {
//         return new UnityEngine.Vector3(vec.x.ToFloat(), vec.y.ToFloat(), vec.z.ToFloat());
//     }

    public static FloatL Max(FloatL a, FloatL b, FloatL c)
    {
        return FixPointMath.Max(FixPointMath.Max(a, b), c);
    }

    public static long Divide(long a, long b)
    {
        long num = (long)((ulong)((a ^ b) & -9223372036854775808L) >> 63);
        long num2 = num * -2L + 1L;
        return (a + b / 2L * num2) / b;
    }
}


