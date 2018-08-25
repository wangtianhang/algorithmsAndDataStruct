using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 预计实现point2d ray2d line2d segment2d circle2d orientedRectangle2d convex2d sector2d 之间的相交测试
/// </summary>
class IntersectionTest2D
{
    /// <summary>
    /// 2d线和2d线相交
    /// </summary>
    /// <param name="line2"></param>
    /// <returns></returns>
    public static bool Line2dWithLine2d(Line2d line1, Line2d line2)
    {
        return Math.Abs(line1.m_slope - line2.m_slope) > Line2d.epsilon
            || Math.Abs(line1.m_y_interept - line2.m_y_interept) < Line2d.epsilon;
    }
}

