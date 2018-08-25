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

    public static bool Segment2dWithSegment2d(Segment2d segment1, Segment2d segment2, ref Vector3 result)
    {
        float resultX = 0;
        float resultY = 0;
        bool ret = get_line_intersection(segment1.pos1.x, segment1.pos1.y, segment1.pos2.x, segment1.pos2.y,
            segment2.pos1.x, segment2.pos1.y, segment2.pos2.x, segment2.pos2.y,
            ref resultX, ref resultY);
        result.x = resultX;
        result.z = resultY;
        return ret;
    }

    static bool get_line_intersection(float p0_x, float p0_y, float p1_x, float p1_y,
        float p2_x, float p2_y, float p3_x, float p3_y, ref float i_x, ref float i_y)
    {
        float s1_x, s1_y, s2_x, s2_y;
        s1_x = p1_x - p0_x; s1_y = p1_y - p0_y;
        s2_x = p3_x - p2_x; s2_y = p3_y - p2_y;

        float s, t;
        s = (-s1_y * (p0_x - p2_x) + s1_x * (p0_y - p2_y)) / (-s2_x * s1_y + s1_x * s2_y);
        t = (s2_x * (p0_y - p2_y) - s2_y * (p0_x - p2_x)) / (-s2_x * s1_y + s1_x * s2_y);

        if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
        {
            // Collision detected
            i_x = p0_x + (t * s1_x);
            i_y = p0_y + (t * s1_y);
            return true;
        }

        return false; // No collision
    }
}

