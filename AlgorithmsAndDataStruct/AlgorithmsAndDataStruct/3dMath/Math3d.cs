using System;
using System.Collections.Generic;

using System.Text;



class Math3d
{
    public static void Test()
    {
        List<Vector2> test = new List<Vector2>();
        test.Add(new Vector2(50, 50));
        test.Add(new Vector2(0, 0));
        test.Add(new Vector2(100, 100));
        test.Add(new Vector2(0, 100));
        test.Add(new Vector2(100, 0));
        List<Vector2> ret = GetConvexHull(test);
        foreach(var iter in ret)
        {
            Debug.Log(iter.ToString());
        }

        Console.ReadLine();
    }

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

    #region 暴力求解凸包
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
        Dictionary<string, int> noRepeatDic = new Dictionary<string, int>();
        List<Edge> allSegment = new List<Edge>();
        for (int i = 0; i < allPoint.Count; ++i )
        {
            for (int j = 0; j < allPoint.Count; ++j )
            {
                if (i != j)
                {
                    int min = Mathf.Min(i, j);
                    int max = Mathf.Max(i, j);
                    string key = min.ToString() + "_" + max;
                    if (!noRepeatDic.ContainsKey(key))
                    {
                        noRepeatDic.Add(key, 1);
                        Edge edge = new Edge();
                        edge.point1 = allPoint[i];
                        edge.point2 = allPoint[j];
                        allSegment.Add(edge);
                    }
                }
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
                    if(!ret.Contains(allSegment[i].point2))
                    {
                        ret.Add(allSegment[i].point2);
                        break;
                    }
                }
                else if (allSegment[i].point2 == findPoint)
                {
                    if (!ret.Contains(allSegment[i].point1))
                    {
                        ret.Add(allSegment[i].point1);
                        break;
                    }
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
    #endregion

    #region 尝试使用tuple替代edge求解凸包
    public static List<Vector2> GetConvexHull2(List<Vector2> allPoint)
    {
        Dictionary<string, int> noRepeatDic = new Dictionary<string, int>();
        List<Tuple<Vector2, Vector2>> allSegment = new List<Tuple<Vector2, Vector2>>();
        for (int i = 0; i < allPoint.Count; ++i)
        {
            for (int j = 0; j < allPoint.Count; ++j)
            {
                if (i != j)
                {
                    int min = Mathf.Min(i, j);
                    int max = Mathf.Max(i, j);
                    string key = min.ToString() + "_" + max;
                    if (!noRepeatDic.ContainsKey(key))
                    {
                        noRepeatDic.Add(key, 1);
                        Tuple<Vector2, Vector2> edge = new Tuple<Vector2, Vector2>(allPoint[i], allPoint[j]);
                        allSegment.Add(edge);
                    }
                }
            }
        }

        for (int i = allSegment.Count - 1; i >= 0; --i)
        {
            if (!IsAllPointAtOneSide2(allSegment[i], allPoint))
            {
                allSegment.RemoveAt(i);
            }
        }

        // 整理绕序
        List<Vector2> ret = new List<Vector2>();
        ret.Add(allSegment[0].Item1);
        while (ret.Count != allSegment.Count)
        {
            Vector2 findPoint = ret[ret.Count - 1];
            for (int i = 0; i < allSegment.Count; ++i)
            {
                if (allSegment[i].Item1 == findPoint)
                {
                    if (!ret.Contains(allSegment[i].Item2))
                    {
                        ret.Add(allSegment[i].Item2);
                        break;
                    }
                }
                else if (allSegment[i].Item2 == findPoint)
                {
                    if (!ret.Contains(allSegment[i].Item1))
                    {
                        ret.Add(allSegment[i].Item1);
                        break;
                    }
                }
            }
        }

        return ret;
    }

    static bool IsAllPointAtOneSide2(Tuple<Vector2, Vector2> edge, List<Vector2> allPoint)
    {
        float a = edge.Item2.y - edge.Item1.y;
        float b = edge.Item1.x - edge.Item2.x;
        float c = edge.Item1.x * edge.Item2.y - edge.Item1.y * edge.Item2.x;
        int side1Count = 0;
        int side2Count = 0;
        foreach (var iter in allPoint)
        {
            if (iter != edge.Item1
                && iter != edge.Item2)
            {
                if (a * iter.x + b * iter.y > c)
                {
                    side1Count += 1;
                }
                else
                {
                    side2Count += 1;
                }
                if (side1Count != 0 && side2Count != 0)
                {
                    return false;
                }
            }
        }
        return true;
    }
    #endregion
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
    /// 三点确定一个圆
    /// </summary>
    /// <returns></returns>
    public Tuple<Vector2, double> ThreePointCalculateCircle(Vector2 pt1, Vector2 pt2, Vector2 pt3)
    {
        //Tuple<Vector2, double> ret = new Tuple<Vector2, double>();

        double x1 = pt1.x;
        double x2 = pt2.x;
        double x3 = pt3.x;
        double y1 = pt1.y;
        double y2 = pt2.y;
        double y3 = pt3.y;

        double a = x1 - x2;
        double b = y1 - y2;
        double c = x1 - x3;
        double d = y1 - y3;
        double e = ((x1 * x1 - x2 * x2) + (y1 * y1 - y2 * y2)) / 2.0;
        double f = ((x1 * x1 - x3 * x3) + (y1 * y1 - y3 * y3)) / 2.0;
        double det = b * c - a * d;
        if(Math.Abs(det) < 1e-5)
        {
            //ret.Item2 = -1f;
            return new Tuple<Vector2,double>(Vector2.zero, -1);
        }

        double x0 = -(d * e - b * f) / det;
        double y0 = -(a * f - c * e) / det;
        double radius = Math.Sqrt((x1 - x0) * (x1 - x0) + (y1 - y0) * (y1 - y0));
        return new Tuple<Vector2, double>(new Vector2((float)x0, (float)y0), radius);
    }

    #region 高速线段相交判定
    //http://dec3.jlu.edu.cn/webcourse/t000096/graphics/chapter5/01_1.html
    double determinant(double v1, double v2, double v3, double v4)  // 行列式  
    {
        return (v1 * v3 - v2 * v4);
    }
    bool intersect3(Vector2 aa, Vector2 bb, Vector2 cc, Vector2 dd)
    {
        double delta = determinant(bb.x - aa.x, cc.x - dd.x, bb.y - aa.y, cc.y - dd.y);
        if (delta <= (1e-6) && delta >= -(1e-6))  // delta=0，表示两线段重合或平行  
        {
            return false;
        }
        double namenda = determinant(cc.x - aa.x, cc.x - dd.x, cc.y - aa.y, cc.y - dd.y) / delta;
        if (namenda > 1 || namenda < 0)
        {
            return false;
        }
        double miu = determinant(bb.x - aa.x, cc.x - aa.x, bb.y - aa.y, cc.y - aa.y) / delta;
        if (miu > 1 || miu < 0)
        {
            return false;
        }
        return true;
    }  
    #endregion
}

