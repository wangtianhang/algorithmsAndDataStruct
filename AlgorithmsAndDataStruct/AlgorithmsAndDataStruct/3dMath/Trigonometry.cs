using System;
using System.Collections.Generic;
using System.Text;


class Trigonometry
{
    /// <summary>
    /// 海伦公式
    /// </summary>
    /// <returns></returns>
    public float GetTriangleArea(float a, float b, float c)
    {
        float p = (a + b + c) / 2;
        return (float)Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    /// <summary>
    /// 余弦定理 根据三边求夹角
    /// c ^ 2 = a ^ 2 + b ^ 2 - 2 * a * b * cosTheta
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public float GetTheta(float a, float b, float c)
    {
        float cosTheta = (a * a + b * b - c * c) / (2 * a * b);
        return (float)Math.Acos(cosTheta);
    }
}

