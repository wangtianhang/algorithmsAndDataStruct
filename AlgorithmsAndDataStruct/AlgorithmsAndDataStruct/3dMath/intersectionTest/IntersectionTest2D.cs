using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 预计实现point2d ray2d line2d segment2d circle2d orientedRectangle2d convex2d sector2d 之间的相交测试
/// </summary>
public class IntersectionTest2D
{
    public static void Test()
    {
        Line2d line1 = new Line2d();
        line1.m_point1 = new Vector2L(0, 0);
        line1.m_point2 = new Vector2L(100, 100);

        Line2d line2 = new Line2d();
        line2.m_point1 = new Vector2L(0, 100);
        line2.m_point2 = new Vector2L(100, 0);

        Vector2L intersectionPoint = new Vector2L();
        if (Line2dWithLine2d(line1, line2, ref intersectionPoint))
        {
            Debug.Log("双线相交 交点 " + intersectionPoint);
        }
        else
        {
            Debug.Log("双线没有相交");
        }

        List<Vector2L> pointList = new List<Vector2L>{new Vector2L(-4.5f, -10f), 
            new Vector2L(-4.5f, 10f), 
            new Vector2L(4.5f, 10f), 
            new Vector2L(4.5f, -10f)};
        Convex2d convex1 = new Convex2d(new Vector2L(-5, 0), QuaternionL.Euler(Vector3L.zero), pointList);
        Convex2d convex2 = new Convex2d(new Vector2L(+5, 0), QuaternionL.Euler(Vector3L.zero), pointList);
        Debug.Log("凸多边形相交测试1 " + (Convex2dWithConvex2d(convex1, convex2, false, true) != null));
        Convex2d convex3 = new Convex2d(new Vector2L(+5, 0), QuaternionL.Euler(new Vector3L(0, -90, 0)), pointList);
        Debug.Log("凸多边形相交测试2 " + (Convex2dWithConvex2d(convex1, convex3, false, true) != null));

        Circle2d circle = new Circle2d(new Vector3L(-5, 0), 5);
        Debug.Log("圆与凸多边形相交测试1 " + (Circle2dWithConvex2d(circle, convex2, false, true) != null));
        Debug.Log("圆与凸多边形相交测试2 " + (Circle2dWithConvex2d(circle, convex3, false, true) != null));
    }

    /// <summary>
    /// 2d线和2d线相交
    /// </summary>
    /// <param name="line2"></param>
    /// <returns></returns>
//     public static bool Line2dWithLine2d(Line2d line1, Line2d line2)
//     {
//         return Math.Abs(line1.m_slope - line2.m_slope) > Line2d.epsilon
//             || Math.Abs(line1.m_y_interept - line2.m_y_interept) < Line2d.epsilon;
//     }
    public static bool Line2dWithLine2d(Line2d line1, Line2d line2, ref Vector2L intersectionPoint)
    {
        return linesCross(line1.m_point1, line1.m_point2, line2.m_point1, line2.m_point2, ref intersectionPoint);
    }

    /// Perform the cross product on a scalar and a vector. In 2D this produces
    /// a vector.
    static Vector2L b2Cross(FloatL s, Vector2L a)
    {
        return new Vector2L(-s * a.y, s * a.x);
    }

    static bool linesCross(Vector2L v0, Vector2L v1, Vector2L t0, Vector2L t1, ref Vector2L intersectionPoint)
    {
        if (v1 == t0 ||
            v0 == t0 ||
            v1 == t1 ||
            v0 == t1)
            return false;

        Vector2L vnormal = v1 - v0;
        vnormal = b2Cross(1.0f, vnormal);
        FloatL v0d = Vector2L.Dot(vnormal, v0);
        FloatL t0d = Vector2L.Dot(vnormal, t0);
        FloatL t1d = Vector2L.Dot(vnormal, t1);
        if (t0d > v0d && t1d > v0d)
            return false;
        if (t0d < v0d && t1d < v0d)
            return false;

        Vector2L tnormal = t1 - t0;
        tnormal = b2Cross(1.0f, tnormal);
        t0d = Vector2L.Dot(tnormal, t0);
        v0d = Vector2L.Dot(tnormal, v0);
        FloatL v1d = Vector2L.Dot(tnormal, v1);
        if (v0d > t0d && v1d > t0d)
            return false;
        if (v0d < t0d && v1d < t0d)
            return false;

        intersectionPoint = v0 + ((t0d - v0d) / (v1d - v0d)) * (v1 - v0);

        return true;
    }

    public static bool Segment2dWithSegment2d(Segment2d segment1, Segment2d segment2, ref Vector3L result)
    {
        FloatL resultX = 0;
        FloatL resultY = 0;
        bool ret = get_line_intersection(segment1.m_point1.x, segment1.m_point1.y, segment1.m_point2.x, segment1.m_point2.y,
            segment2.m_point1.x, segment2.m_point1.y, segment2.m_point2.x, segment2.m_point2.y,
            ref resultX, ref resultY);
        result.x = resultX;
        result.z = resultY;
        return ret;
    }

    static bool get_line_intersection(FloatL p0_x, FloatL p0_y, FloatL p1_x, FloatL p1_y,
        FloatL p2_x, FloatL p2_y, FloatL p3_x, FloatL p3_y, ref FloatL i_x, ref FloatL i_y)
    {
        FloatL s1_x, s1_y, s2_x, s2_y;
        s1_x = p1_x - p0_x; s1_y = p1_y - p0_y;
        s2_x = p3_x - p2_x; s2_y = p3_y - p2_y;

        FloatL s, t;
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

    public static bool Point2dWithRectangle2d(Vector2L point2d, OrientedRectangle2d rectangle2d)
    {
        Vector3L forward = RotateHelper.GetForward(rectangle2d.m_rotation);
        Vector3L right = Vector3L.Cross(Vector3L.up, forward);
        Vector3L pos = new Vector3L(rectangle2d.m_pos.x, 0, rectangle2d.m_pos.y);
        Vector3L a = pos + forward * rectangle2d.m_length * 0.5f + -right * rectangle2d.m_width * 0.5f;
        Vector3L b = pos + forward * rectangle2d.m_length * 0.5f + right * rectangle2d.m_width * 0.5f;
        Vector3L c = pos + -forward * rectangle2d.m_length * 0.5f + right * rectangle2d.m_width * 0.5f;
        Vector3L d = pos + -forward * rectangle2d.m_length * 0.5f + -right * rectangle2d.m_width * 0.5f;
        return IsInRectangle2d(a, b, c, d, new Vector3L(point2d.x, 0, point2d.y));
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
    static bool IsInRectangle2d(Vector3L p, Vector3L a, Vector3L b, Vector3L c, Vector3L d)
    {
        Vector3L v11 = p - a;
        Vector3L v12 = b - a;

        Vector3L v21 = p - b;
        Vector3L v22 = c - b;

        Vector3L v31 = p - c;
        Vector3L v32 = d - c;

        Vector3L v41 = p - d;
        Vector3L v42 = a - d;

        Vector3L cross1 = Vector3L.Cross(v11, v12);
        Vector3L cross2 = Vector3L.Cross(v21, v22);
        Vector3L cross3 = Vector3L.Cross(v31, v32);
        Vector3L cross4 = Vector3L.Cross(v41, v42);

        if (Vector3L.Dot(cross1, cross2) > 0
            && Vector3L.Dot(cross2, cross3) > 0
            && Vector3L.Dot(cross3, cross4) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool Point2dWithPolygon2d(Vector2L pos, Polygon2d polygon2d)
    {
        var edgePoint = (polygon2d.m_pointList[1] + polygon2d.m_pointList[0]) * 0.5f;
        Vector2L outPoint = (edgePoint - pos).normalized * 10000;
        int count = 0;
        List<Vector2L> pointList = polygon2d.GetWorldPosList();
        for (int i = 0; i < pointList.Count; i++)
        {
            var a = pointList[i % pointList.Count];
            var b = pointList[(i + 1) % pointList.Count];

            var r = IsTwoSegmentIntersection(a, b, pos, outPoint);

            if (r)
            {
                count += 1;
            }
        }
        return count % 2 == 1;
    }

    /// <summary>
    /// 判断两条线段是否相交
    /// 现在有线段AB和线段CB
    //用线段AB的方向和C，D两点分别做差乘比较。如果C,D在同侧则return跳出
    //用线段CD的方向和A，B两点分别做差乘比较。如果A,B在同侧则return跳出
    //最终返回相交
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="d"></param>
    /// <returns></returns>
    static bool IsTwoSegmentIntersection(Vector2L a2d, Vector2L b2d, Vector2L c2d, Vector2L d2d)
    {
        Vector3L a = new Vector3L(a2d.x, 0, a2d.y);
        Vector3L b = new Vector3L(b2d.x, 0, b2d.y);
        Vector3L c = new Vector3L(c2d.x, 0, c2d.y);
        Vector3L d = new Vector3L(d2d.x, 0, d2d.y);

        var crossA = FixPointMath.Sign(Vector3L.Cross(d - c, a - c).y);
        var crossB = FixPointMath.Sign(Vector3L.Cross(d - c, b - c).y);

        if (FixPointMath.Approximately(crossA, crossB)) 
            return false;

        var crossC = FixPointMath.Sign(Vector3L.Cross(b - a, c - a).y);
        var crossD = FixPointMath.Sign(Vector3L.Cross(b - a, d - a).y);

        if (FixPointMath.Approximately(crossC, crossD)) 
            return false;

        return true;
    }

    public static bool Point2dWithSector2d(Vector2L pos, Sector2d sector2d)
    {
        Vector2L distance = pos - sector2d.m_pos;
        if (distance.magnitude > sector2d.m_radius)
        {
            return false;
        }

        Vector3L sectorForward = RotateHelper.GetForward(sector2d.m_rotation);
        FloatL cosTarget = Vector3L.Dot(sectorForward, distance) / sectorForward.magnitude / distance.magnitude;
        FloatL cosHalfDegree = FixPointMath.Cos((FixPointMath.Deg2Rad * sector2d.m_theraDegree / 2));
        return cosTarget > cosHalfDegree;
    }

    public static bool Point2dWithLine2d(Vector2L point, Line2d line2d)
    {
        // Find the slope
        FloatL dy = (line2d.m_point2.y - line2d.m_point1.y);
        FloatL dx = (line2d.m_point2.x - line2d.m_point1.x);
        FloatL M = dy / dx;
        // Find the Y-Intercept
        FloatL B = line2d.m_point1.y - M * line2d.m_point1.x;
        // Check line equation
        return FixPointMath.Approximately(point.y, M * point.x + B);
    }

    public static bool Segment2dWithCircle2d(Segment2d segment, Circle2d circle)
    {
        Segment3d segment3d = new Segment3d(
            new Vector3L(segment.m_point1.x, 0, segment.m_point1.y),
            new Vector3L(segment.m_point2.x, 0, segment.m_point2.y));
        Vector3L closestPoint = Distance3d.ClosestPointOfPoint3dWithSegment3d(new Vector3L(circle.m_pos.x, 0, circle.m_pos.y), segment3d);
        Vector2L distance = circle.m_pos - new Vector2L(closestPoint.x, closestPoint.z);
        return distance.magnitude <= circle.m_radius;
    }

    /// <summary>
    /// 算法来自 game physics cookbook
    /// </summary>
    /// <param name="line"></param>
    /// <param name="circle"></param>
    /// <returns></returns>
    public static bool Line2dWithCircle2d(Line2d line, Circle2d circle)
    {
        Vector2L ab = line.m_point2 - line.m_point1;
        FloatL t = Vector2L.Dot(circle.m_pos - line.m_point1, ab) / Vector2L.Dot(ab, ab);
        Vector2L closestPoint = line.m_point1 + ab * t;
        return (circle.m_pos - closestPoint).sqrMagnitude < circle.m_radius * circle.m_radius;
    }

    public static bool Line2dWithOrientedRectangle2d(Line2d line, OrientedRectangle2d rectangle2d)
    {
        Vector3L forward = RotateHelper.GetForward(rectangle2d.m_rotation);
        Vector3L right = Vector3L.Cross(Vector3L.up, forward);
        Vector3L pos = new Vector3L(rectangle2d.m_pos.x, 0, rectangle2d.m_pos.y);
        Vector3L a = pos + forward * rectangle2d.m_length * 0.5f + -right * rectangle2d.m_width * 0.5f;
        Vector3L b = pos + forward * rectangle2d.m_length * 0.5f + right * rectangle2d.m_width * 0.5f;
        Vector3L c = pos + -forward * rectangle2d.m_length * 0.5f + right * rectangle2d.m_width * 0.5f;
        Vector3L d = pos + -forward * rectangle2d.m_length * 0.5f + -right * rectangle2d.m_width * 0.5f;
        List<Vector2L> lineList = new List<Vector2L>();
        lineList.Add(new Vector2L(a.x, a.z));
        lineList.Add(new Vector2L(b.x, b.z));
        lineList.Add(new Vector2L(c.x, c.z));
        lineList.Add(new Vector2L(d.x, d.z));
        for (int i = 0; i < lineList.Count; ++i )
        {
            if (IsTwoSegmentIntersection(lineList[i], lineList[(i + 1) % lineList.Count], line.m_point1, line.m_point2))
            {
                return true;
            }
        }

        return false;
    }

    public static bool Circle2dWithOrientedRectangle2d(Circle2d circle, OrientedRectangle2d rectangle)
    {
        Vector3L forward = RotateHelper.GetForward(rectangle.m_rotation);
        Vector3L right = Vector3L.Cross(Vector3L.up, forward);
        Vector3L pos = new Vector3L(rectangle.m_pos.x, 0, rectangle.m_pos.y);
        Vector3L a = pos + forward * rectangle.m_length * 0.5f + -right * rectangle.m_width * 0.5f;
        Vector3L b = pos + forward * rectangle.m_length * 0.5f + right * rectangle.m_width * 0.5f;
        Vector3L c = pos + -forward * rectangle.m_length * 0.5f + right * rectangle.m_width * 0.5f;
        Vector3L d = pos + -forward * rectangle.m_length * 0.5f + -right * rectangle.m_width * 0.5f;
        List<Vector2L> lineList = new List<Vector2L>();
        lineList.Add(new Vector2L(a.x, a.z));
        lineList.Add(new Vector2L(b.x, b.z));
        lineList.Add(new Vector2L(c.x, c.z));
        lineList.Add(new Vector2L(d.x, d.z));
        for (int i = 0; i < lineList.Count; ++i)
        {
            Segment2d segment = new Segment2d();
            segment.m_point1 = lineList[i];
            segment.m_point2 = lineList[(i + 1) % lineList.Count];
            if (Segment2dWithCircle2d(segment, circle))
            {
                return true;
            }
        }
        return false;
    }

    #region Separating Axis Theorem

    public class CollisionInfo
    {
        public System.Object shapeA;
        public System.Object shapeB;
        public bool shapeAContained;
        public bool shapeBContained;
        public FloatL distance;
        public Vector2L vector;
    }

    static Vector2L GetAxisNormal(List<Vector2L> vertexArray, int pointIndex)
    {
        Vector2L pt1 = vertexArray[pointIndex];
        Vector2L pt2 = pointIndex >= vertexArray.Count - 1 ? vertexArray[0] : vertexArray[pointIndex + 1];
        Vector2L p = new Vector2L(-(pt2.y - pt1.y), pt2.x - pt1.x);
        p.Normalize();
        return p;
    }

    /// <summary>
    /// 根据Separating Axis Theorem理论实现
    /// https://www.sevenson.com.au/actionscript/sat/
    /// https://github.com/sevdanski/SAT_AS3
    /// </summary>
    /// <param name="convex1"></param>
    /// <param name="convex2"></param>
    /// <returns></returns>
    public static CollisionInfo Convex2dWithConvex2d(Convex2d polygonA, Convex2d polygonB, bool flip, bool docalc)
    {
        CollisionInfo result = new CollisionInfo();
        result.shapeA = (flip) ? polygonB : polygonA;
        result.shapeB = (flip) ? polygonA : polygonB;
        result.shapeAContained = true;
        result.shapeBContained = true;

        // get the vertices
        List<Vector2L> p1 = polygonA.GetRotateList();
        List<Vector2L> p2 = polygonB.GetRotateList();

        // get the offset
        Vector2L vOffset = new Vector2L(polygonA.m_pos.x - polygonB.m_pos.x, polygonA.m_pos.y - polygonB.m_pos.y);

        FloatL shortestDist = FloatL.MaxValue;

        // loop through all of the axis on the first polygon
        for (int i = 0; i < p1.Count; ++i )
        {
            // find the axis that we will project onto
            Vector2L vAxis = GetAxisNormal(p1, i);

            // project polygon A
            FloatL min0 = Vector2L.Dot(vAxis, p1[0]);
            FloatL max0 = min0;

            for (int j = 1; j < p1.Count; ++j )
            {
                FloatL t = Vector2L.Dot(vAxis, p1[j]);
                if(t < min0)
                {
                    min0 = t;
                }
                if(t > max0)
                {
                    max0 = t;
                }
            }

            // project polygon B
            FloatL min1 = Vector2L.Dot(vAxis, p2[0]);
            FloatL max1 = min1;
            for (int j = 1; j < p2.Count; ++j )
            {
                FloatL t = Vector2L.Dot(vAxis, p2[j]);
                if(t < min1)
                {
                    min1 = t;
                }
                if(t > max1)
                {
                    max1 = t;
                }
            }

            // shift polygonA's projected points
            FloatL sOffset = Vector2L.Dot(vAxis, vOffset);
            min0 += sOffset;
            max0 += sOffset;

            // test for intersections
            FloatL d0 = min0 - max1;
            FloatL d1 = min1 - max0;
            if(d0 > 0 || d1 > 0)
            {
                // gap found
                return null;
            }

            if(docalc)
            {
                // check for containment
                if(!flip)
                {
                    if(max0 > max1 || min0 < min1)
                    {
                        result.shapeAContained = false;
                    }
                    if(max1 > max0 || min1 < min0)
                    {
                        result.shapeBContained = false;
                    }
                }
                else
                {
                    if(max0 < max1 || min0 > min1)
                    {
                        result.shapeAContained = false;
                    }
                    if(max1 < max0 || min1 > min0)
                    {
                        result.shapeBContained = false;
                    }
                }

                FloatL distmin = (max1 - min0) * -1;
                if(flip)
                {
                    distmin *= -1;
                }
                FloatL distminAbs = (distmin < 0) ? distmin * -1 : distmin;
                if (distminAbs < shortestDist)
                {
                    // this distance is shorter so use it...
                    result.distance = distmin;
                    result.vector = vAxis;

                    shortestDist = distminAbs;
                }

            }
        }

        // if you are here then no gap was found
        return result;
    }

    /// <summary>
    /// 根据Separating Axis Theorem理论实现
    /// https://www.sevenson.com.au/actionscript/sat/
    /// https://github.com/sevdanski/SAT_AS3
    /// </summary>
    /// <param name="convex1"></param>
    /// <param name="convex2"></param>
    /// <returns></returns>
    public static CollisionInfo Circle2dWithConvex2d(Circle2d circleA, Convex2d polygonA, bool flip, bool docalc)
    {
        CollisionInfo result = new CollisionInfo();
        if(flip)
        {
            result.shapeA = polygonA;
            result.shapeB = circleA;
        }
        else
        {
            result.shapeA = circleA;
            result.shapeB = polygonA;
        }
        result.shapeAContained = true;
        result.shapeBContained = true;

        // get the offset
        Vector2L vOffset = new Vector2L(polygonA.m_pos.x - circleA.m_pos.x, polygonA.m_pos.y - circleA.m_pos.y);

        // get the vertices
        List<Vector2L> p1 = polygonA.GetRotateList();

        // find the closest point
        Vector2L closestPoint = new Vector2L();
        FloatL minDist = FloatL.MaxValue;
        foreach(var iter in p1)
        {
            FloatL currentDist = (circleA.m_pos - (polygonA.m_pos + iter)).sqrMagnitude;
            if(currentDist < minDist)
            {
                minDist = currentDist;
                closestPoint = polygonA.m_pos + iter;
            }
        }

        // make a normal of this vector
        Vector2L vAxis = closestPoint - circleA.m_pos;
        vAxis.Normalize();

        // project polygon A
        FloatL min0 = Vector2L.Dot(vAxis, p1[0]);
        FloatL max0 = min0;

        for (int j = 1; j < p1.Count; ++j )
        {
            FloatL t = Vector2L.Dot(vAxis, p1[j]);
            if(t < min0)
            {
                min0 = t;
            }
            if(t > max0)
            {
                max0 = t;
            }
        }

        // project circle A
        FloatL min1 = Vector2L.Dot(vAxis, Vector2L.zero);
        FloatL max1 = min1 + circleA.m_radius;
        min1 -= circleA.m_radius;

        // shift polygonA's projected points
        FloatL sOffset = Vector2L.Dot(vAxis, vOffset);
        min0 += sOffset;
        max0 += sOffset;

        // test for intersections
        FloatL d0 = min0 - max1;
        FloatL d1 = min1 - max0;

        if (d0 > 0 || d1 > 0)
        {
            // gap found
            return null;
        }

        FloatL shortestDist = FloatL.MaxValue;
        if(docalc) {
			FloatL distmin = (max1 - min0) * -1;  //Math.min(dist0, dist1);
            if (flip) distmin *= -1;
			FloatL distminAbs = (distmin < 0) ? distmin * -1 : distmin;
							
			// check for containment
			if (!flip) {
				if (max0 > max1 || min0 < min1) result.shapeAContained = false;
				if (max1 > max0 || min1 < min0) result.shapeBContained = false;
			} else {
				if (max0 < max1 || min0 > min1) result.shapeAContained = false;				
				if (max1 < max0 || min1 > min0) result.shapeBContained = false;				
			}			
				
			// this distance is shorter so use it...
			result.distance = distmin;
			result.vector = vAxis;
			//
			shortestDist = distminAbs;
		}

        // loop through all of the axis on the first polygon
		for (int i = 0; i < p1.Count; i++) {
			// find the axis that we will project onto
			vAxis = GetAxisNormal(p1, i);
				
			// project polygon A
			min0 = Vector2L.Dot(vAxis, p1[0]);
			max0 = min0;
				
			//
			for (int j = 1; j < p1.Count; j++) {
				FloatL t = Vector2L.Dot(vAxis, p1[j]);
				if (t < min0) min0 = t;
				if (t > max0) max0 = t;
			}
				
			// project circle A
			min1 = Vector2L.Dot(vAxis, new Vector2L(0,0) );
			max1 = min1 + circleA.m_radius;
			min1 -= circleA.m_radius;				
				
			// shift polygonA's projected points
			sOffset = Vector2L.Dot(vAxis, vOffset);
			min0 += sOffset;
			max0 += sOffset;				
				
			// test for intersections
			d0 = min0 - max1;
			d1 = min1 - max0;
				
			if (d0 > 0 || d1 > 0) {
				// gap found
				return null;
			}
				
			if(docalc) {

				// check for containment
				if (!flip) {
					if (max0 > max1 || min0 < min1) result.shapeAContained = false;
					if (max1 > max0 || min1 < min0) result.shapeBContained = false;
				} else {
					if (max0 < max1 || min0 > min1) result.shapeAContained = false;				
					if (max1 < max0 || min1 > min0) result.shapeBContained = false;				
				}				
					
				FloatL distmin = (max1 - min0) * -1;
                if (flip) distmin *= -1;
				FloatL distminAbs = (distmin < 0) ? distmin * -1 : distmin;
				if (distminAbs < shortestDist) {
					// this distance is shorter so use it...
					result.distance = distmin;
					result.vector = vAxis;
					//
					shortestDist = distminAbs;
				}
			}
		}

        // if you are here then no gap was found
        return result;
    }

    #endregion
}



