using System;
using System.Collections.Generic;
using System.Text;


class Distance3d
{
    /// <summary>
    /// 3d空间中点到直线距离
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    public static float DistanceOfPoint3dWithLine3d(Line3d line3d, Vector3 s)
    {
        float ab = Mathf.Sqrt(Mathf.Pow((line3d.m_point1.x - line3d.m_point2.x), 2.0f) + Mathf.Pow((line3d.m_point1.y - line3d.m_point2.y), 2.0f) + Mathf.Pow((line3d.m_point1.z - line3d.m_point2.z), 2.0f));
        float as2 = Mathf.Sqrt(Mathf.Pow((line3d.m_point1.x - s.x), 2.0f) + Mathf.Pow((line3d.m_point1.y - s.y), 2.0f) + Mathf.Pow((line3d.m_point1.z - s.z), 2.0f));
        float bs = Mathf.Sqrt(Mathf.Pow((s.x - line3d.m_point2.x), 2.0f) + Mathf.Pow((s.y - line3d.m_point2.y), 2.0f) + Mathf.Pow((s.z - line3d.m_point2.z), 2.0f));
        float cos_A = (Mathf.Pow(as2, 2.0f) + Mathf.Pow(ab, 2.0f) - Mathf.Pow(bs, 2.0f)) / (2 * ab * as2);
        float sin_A = Mathf.Sqrt(1 - Mathf.Pow(cos_A, 2.0f));
        return as2 * sin_A;
    }
}


