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

    public static bool Point2dWithRectangle2d(Vector2 point2d, OrientedRectangle2d rectangle2d)
    {
        Vector3 forward = RotateHelper.GetForward(rectangle2d.m_rotation);
        Vector3 right = Vector3.Cross(Vector3.up, forward);
        Vector3 pos = new Vector3(rectangle2d.m_pos.x, 0, rectangle2d.m_pos.y);
        Vector3 a = pos + forward * rectangle2d.m_length * 0.5f + -right * rectangle2d.m_width * 0.5f;
        Vector3 b = pos + forward * rectangle2d.m_length * 0.5f + right * rectangle2d.m_width * 0.5f;
        Vector3 c = pos + -forward * rectangle2d.m_length * 0.5f + right * rectangle2d.m_width * 0.5f;
        Vector3 d = pos + -forward * rectangle2d.m_length * 0.5f + -right * rectangle2d.m_width * 0.5f;
        return IsInRectangle2d(a, b, c, d, new Vector3(point2d.x, 0, point2d.y));
    }

    /// <summary>
    /// 注意abcd为绕序
    /// </summary>
    /// <param name="p"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="d"></param>
    /// <returns></returns>
    static bool IsInRectangle2d(Vector3 p, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        Vector3 v11 = p - a;
        Vector3 v12 = b - a;

        Vector3 v21 = p - b;
        Vector3 v22 = c - b;

        Vector3 v31 = p - c;
        Vector3 v32 = d - c;

        Vector3 v41 = p - d;
        Vector3 v42 = a - d;

        Vector3 cross1 = Vector3.Cross(v11, v12);
        Vector3 cross2 = Vector3.Cross(v21, v22);
        Vector3 cross3 = Vector3.Cross(v31, v32);
        Vector3 cross4 = Vector3.Cross(v41, v42);

        if (Vector3.Dot(cross1, cross2) > 0
            && Vector3.Dot(cross2, cross3) > 0
            && Vector3.Dot(cross3, cross4) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool Point2dWithPolygon2d(Vector2 pos, Polygon2d polygon2d)
    {
        var edgePoint = (polygon2d.m_pointList[1] + polygon2d.m_pointList[0]) * 0.5f;
        Vector2 outPoint = (edgePoint - pos).normalized * 10000;
        int count = 0;
        for (int i = 0; i < polygon2d.m_pointList.Count; i++)
        {
            var a = polygon2d.m_pointList[i % polygon2d.m_pointList.Count];
            var b = polygon2d.m_pointList[(i + 1) % polygon2d.m_pointList.Count];

            var r = IsTwoSegmentIntersection(a, b, pos, outPoint);

            if (r)
            {
                count += 1;
            }
        }
        return count % 2 == 1;
    }

    public static bool IsTwoSegmentIntersection(Vector2 a2d, Vector2 b2d, Vector2 c2d, Vector2 d2d)
    {
        Vector3 a = new Vector3(a2d.x, 0, a2d.y);
        Vector3 b = new Vector3(b2d.x, 0, b2d.y);
        Vector3 c = new Vector3(c2d.x, 0, c2d.y);
        Vector3 d = new Vector3(d2d.x, 0, d2d.y);

        var crossA = Mathf.Sign(Vector3.Cross(d - c, a - c).y);
        var crossB = Mathf.Sign(Vector3.Cross(d - c, b - c).y);

        if (Mathf.Approximately(crossA, crossB)) 
            return false;

        var crossC = Mathf.Sign(Vector3.Cross(b - a, c - a).y);
        var crossD = Mathf.Sign(Vector3.Cross(b - a, d - a).y);

        if (Mathf.Approximately(crossC, crossD)) 
            return false;

        return true;
    }
}

