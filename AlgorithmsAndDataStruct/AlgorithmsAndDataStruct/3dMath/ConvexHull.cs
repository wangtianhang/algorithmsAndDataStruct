using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ConvexHull
{
    public static void Test()
    {
        List<Vector2> test = new List<Vector2>();
        test.Add(new Vector2(50, 50));
        test.Add(new Vector2(0, 0));
        test.Add(new Vector2(100, 100));
        test.Add(new Vector2(0, 100));
        test.Add(new Vector2(100, 0));
        Debug.Log("蛮力法求凸包");
        List<Vector2> ret = BruteForceGetConvexHull(test);
        foreach (var iter in ret)
        {
            Debug.Log(iter.ToString());
        }
        Debug.Log("扫描法求凸包");
        List<Vector2> ret2 = FastGetConvexHull(test);
        foreach(var iter in ret2)
        {
            Debug.Log(iter.ToString());
        }
        //Console.ReadLine();
    }

    #region 蛮力法求解凸包
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
    public static List<Vector2> BruteForceGetConvexHull(List<Vector2> allPoint)
    {
        Dictionary<string, int> noRepeatDic = new Dictionary<string, int>();
        List<Edge> allSegment = new List<Edge>();
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
                    if (!ret.Contains(allSegment[i].point2))
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
        foreach (var iter in allPoint)
        {
            if (iter != edge.point1
                && iter != edge.point2)
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

    #region 蛮力法求解凸包（尝试使用tuple替代edge求解凸包）
    public static List<Vector2> BruteForceGetConvexHull2(List<Vector2> allPoint)
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

    #region 凸包扫描（快包）来源自 算法技术手册
    /**
 * Represents either the top or the bottom of a Convex Hull.
 * 
 * @author George Heineman
 * @version 1.0, 6/15/08
 * @since 1.0
 */
    class PartialHull
    {
        /** Points that make up the hull. */
        List<Vector2> m_points = new List<Vector2>();

        /**
 * Construct the initial partial hull.
 * 
 * @param first     Left-most point (for upper) and right-most (for lower) 
 * @param second    Next one in sorted order, as the next assumed point in the hull.
 */
        public PartialHull(Vector2 first, Vector2 second)
        {
            m_points.Add(first);
            m_points.Add(second);
        }
        /** Add point to the Partial Hull. */
        public void add(Vector2 p)
        {
            m_points.Add(p);
        }
        /** Returns middle of last three. Returns true on success; false otherwise. */
        public bool removeMiddleOfLastThree()
        {
            if (!hasThree())
            {
                return false;
            }

            int pos = m_points.Count;
            m_points.RemoveAt(pos - 2);
            return true;
        }

        /** Determine if there are more than 2 points currently in the partial hull. */
        public bool hasThree()
        {
            return m_points.Count > 2;
        }

        /** Helper function to report number of points in the hull. */
        public int size()
        {
            return m_points.Count;
        }

        /** Return the points in this Partial Hull. */
        public List<Vector2> points()
        {
            return m_points;
        }

        /** 
 * Determines if last three points reflect a right turn.
 * 
 * If hasThree() is false, then this returns false.
 */
        public bool areLastThreeNonRight()
        {
            if (!hasThree()) return false;  // something to do

            double x1, y1, x2, y2, x3, y3;

            int pos = m_points.Count - 3;

            x1 = m_points[pos].x;
            y1 = m_points[pos].y;

            x2 = m_points[pos + 1].x;
            y2 = m_points[pos + 1].y;

            x3 = m_points[pos + 2].x;
            y3 = m_points[pos + 2].y;

            double val1 = (x2 - x1) * (y3 - y1);
            double val2 = (y2 - y1) * (x3 - x1);
            double diff = val1 - val2;
            if (diff >= 0) return true;

            return false;
        }
    }

    /**
 * Computes Convex Hull following Andrew's Algorithm. This algorithm is described
 * in the text.
 * 
 * @author George Heineman
 * @version 1.0, 6/15/08
 * @since 1.0
 */
    class ConverHullScan
    {
        /**
 * Use Andrew's algorithm to return the computed convex hull for 
 * the input set of points.
 * <p>
 * Points must have at least three points to do anything meaningful. If
 * it does not, then the sorted array is returned as the "hull".
 * <p>
 * This algorithm will still work if duplicate points are found in
 * the input set of points.
 *
 * @param points     a set of (n &ge; 3) two dimensional points.
 */
        public List<Vector2> compute(List<Vector2> points)
        {
            // sort by x-coordinate (and if ==, by y-coordinate). 
            int n = points.Count;
            points.Sort(new CompareVector2ForConvexHull());
            if (n < 3) { return points; }

            // Compute upper hull by starting with leftmost two points
            PartialHull upper = new PartialHull(points[0], points[1]);
            for (int i = 2; i < n; i++)
            {
                upper.add(points[i]);
                while (upper.hasThree() && upper.areLastThreeNonRight())
                {
                    upper.removeMiddleOfLastThree();
                }
            }

            // Compute lower hull by starting with rightmost two points
            PartialHull lower = new PartialHull(points[n - 1], points[n - 2]);
            for (int i = n - 3; i >= 0; i--)
            {
                lower.add(points[i]);
                while (lower.hasThree() && lower.areLastThreeNonRight())
                {
                    lower.removeMiddleOfLastThree();
                }
            }

            // remove duplicate end points when combining.
            List<Vector2> hull = new List<Vector2>();
            for (int i = 0; i < upper.size(); ++i )
            {
                hull.Add(upper.points()[i]);
            }
            for (int i = 1; i < lower.size() - 1; ++i )
            {
                hull.Add(lower.points()[i]);
            }

            return hull;
        }

        class CompareVector2ForConvexHull : IComparer<Vector2>
        {
            public int Compare(Vector2 v1, Vector2 v2)
            {
                if (v1.x > v2.x)
                {
                    return 1;
                }
                else if(v1.x < v2.x)
                {
                    return -1;
                }
                else
                {
                    if(v1.y > v2.y)
                    {
                        return 1;
                    }
                    else if(v1.y < v2.y)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }   
                }
            }
        }
    }

    public static List<Vector2> FastGetConvexHull(List<Vector2> allPoint)
    {
        ConverHullScan tmp = new ConverHullScan();
        return tmp.compute(allPoint);
    }
    #endregion
}

