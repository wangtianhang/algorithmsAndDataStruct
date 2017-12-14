using System;
using System.Collections.Generic;

using System.Text;



class Math3d
{
    //public const float Deg2Rad = 0.0174533f;
    //public const float Rad2Deg = 57.2958f;

    /// <summary>
    /// 射线与平面相交
    /// </summary>
    /// <param name="rayOrigin"></param>
    /// <param name="rayDir"></param>
    /// <param name="planeNormal"></param>
    /// <param name="planeOnePoint"></param>
    /// <returns></returns>
    public static bool GetIntersectionPoint(Vector3 rayOrigin, Vector3 rayDir, Vector3 planeNormal, Vector3 planeOnePoint, out Vector3 result)
    {
        float t = (Vector3.Dot(planeNormal, planeOnePoint) - Vector3.Dot(planeNormal, rayOrigin))
            / (Vector3.Dot(planeNormal, rayDir));

        if(t < 0)
        {
            result = Vector3.zero;
            return false;
        }
        else
        {
            result = rayOrigin + rayDir * t;
            return true;
        }

    }

    /// <summary>
    /// 仅支持凸多边形
    /// </summary>
    /// <param name="p"></param>
    /// <param name="polygon"></param>
    /// <returns></returns>
    public static bool IsInConvexPolygon(Vector3 p, List<Vector3> polygon)
    {
        if (polygon.Count < 3)
        {
            return false;
        }

        p.y = 0;
        for (int i = 0; i < polygon.Count; ++i)
        {
            Vector3 tmp = polygon[i];
            tmp.y = 0;
            polygon[i] = tmp;
        }

        List<Vector3> crossResultList = new List<Vector3>();
        for (int i = 0; i < polygon.Count; ++i)
        {
            if (i == polygon.Count - 1)
            {
                Vector3 v1 = p - polygon[i];
                Vector3 v2 = polygon[0] - polygon[i];
                Vector3 v3 = Vector3.Cross(v1, v2);
                crossResultList.Add(v3);
            }
            else
            {
                Vector3 v1 = p - polygon[i];
                Vector3 v2 = polygon[i + 1] - polygon[i];
                Vector3 v3 = Vector3.Cross(v1, v2);
                crossResultList.Add(v3);
            }
        }

        for (int i = 0; i < crossResultList.Count; ++i)
        {
            if (i == crossResultList.Count - 1)
            {
                if (crossResultList[i].y * crossResultList[0].y < 0)
                {
                    return false;
                }
            }
            else
            {
                if (crossResultList[i].y * crossResultList[i + 1].y < 0)
                {
                    return false;
                }
            }
        }

        return true;
    }

    /// <summary>
    /// 来源自directx sdk dx9 “pick” demo 高性能三角形射线相交算法
    /// </summary>
    /// <param name="orig"></param>
    /// <param name="dir"></param>
    /// <param name="v0"></param>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="distance 距离"></param>
    /// <param name="u 质心坐标系"></param>
    /// <param name="v 质心坐标系"></param>
    /// <returns></returns>
    public bool IntersectTriangle(Vector3 orig, Vector3 dir, Vector3 v0, Vector3 v1, Vector3 v2, ref float t, ref float u, ref float v)
    {
        
        // Find vectors for two edges sharing vert0
        Vector3 edge1 = v1 - v0;
        Vector3 edge2 = v2 - v0;

        // Begin calculating determinant - also used to calculate U parameter
        Vector3 pvec = Vector3.Cross(dir, edge2);

        // If determinant is near zero, ray lies in plane of triangle
        float det = Vector3.Dot(edge1, pvec);

        Vector3 tvec;
        if(det > 0)
        {
            tvec = orig - v0;
        }
        else
        {
            tvec = v0 - orig;
            det = -det;
        }

        if (det < 0.0001f)
            return false;

        // Calculate U parameter and test bounds
        u = Vector3.Dot(tvec, pvec);
        if(u < 0.0f || u > det)
        {
            return false;
        }

        // Prepare to test V parameter
        Vector3 qvec = Vector3.Cross(tvec, edge1);

        // Calculate V parameter and test bounds
        v = Vector3.Dot(dir, qvec);
        if(v < 0.0f || u + v > det)
        {
            return false;
        }

        // Calculate t, scale parameters, ray intersects triangle
        t = Vector3.Dot(edge2, qvec);
        float fInvDet = 1.0f / det;
        t *= fInvDet;
        u *= fInvDet;
        v *= fInvDet;
        return true;
    }


    class Edge
    {
        public Vector2 point1;
        public Vector2 point2;
    }
    /// <summary>
    /// 蛮力法求解凸包
    /// 对于一个n个点集合中的两个点Pi和Pj，当且仅当该集合中的其他点都位于穿过这两点的直线的同一边时。
    /// 这两个点组成凸包的一个边
    /// ax + by = c 对两个点(x1,y1)(x2,y2)
    /// a = y2-y1 b = x1-x2 c = x1y2-y1x2
    /// 一个半平面中的点都满足ax+by>c或者ax+by<c
    /// </summary>
    /// <param name="allPoint"></param>
    /// <returns></returns>
    public static List<Vector2> GetConvexHull(List<Vector2> allPoint)
    {
        List<Edge> allSegment = new List<Edge>();
        for (int i = 0; i < allPoint.Count; ++i )
        {
            for (int j = 0; j < allPoint.Count; ++j )
            {
                Edge edge = new Edge();
                edge.point1 = allPoint[i];
                edge.point2 = allPoint[j];
                allSegment.Add(edge);
            }
        }

        for (int i = allSegment.Count - 1; i >= 0; --i)
        {
            if (!IsAllPointAtOneSide(allSegment[i], allPoint))
            {
                allSegment.RemoveAt(i);
            }
        }

        // 整理绕序
        List<Vector2> ret = new List<Vector2>();
        ret.Add(allSegment[0].point1);
        while (ret.Count != allSegment.Count)
        {
            Vector2 findPoint = ret[ret.Count - 1];
            for (int i = 0; i < allSegment.Count; ++i)
            {
                if (allSegment[i].point1 == findPoint)
                {
                    ret.Add(allSegment[i].point2);
                    break;
                }
            }
        }

        return ret;
    }

    static bool IsAllPointAtOneSide(Edge edge, List<Vector2> allPoint)
    {
        float a = edge.point2.y - edge.point1.y;
        float b = edge.point1.x - edge.point2.x;
        float c = edge.point1.x * edge.point2.y - edge.point1.y * edge.point2.x;
        int side1Count = 0;
        int side2Count = 0;
        foreach(var iter in allPoint)
        {
            if(iter != edge.point1
                && iter != edge.point2)
            {
                if(a * iter.x + b * iter.y > c)
                {
                    side1Count += 1;
                }
                else
                {
                    side2Count += 1;
                }
                if(side1Count != 0 && side2Count != 0)
                {
                    return false;
                }
            }
        }
        return true;
    }
}

