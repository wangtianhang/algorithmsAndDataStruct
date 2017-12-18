using System;
using System.Collections.Generic;
using System.Text;


class ConvexHull
{
    public static void Test()
    {
        List<Vector2> test = new List<Vector2>();
        test.Add(new Vector2(50, 50));
        test.Add(new Vector2(0, 0));
        test.Add(new Vector2(100, 100));
        test.Add(new Vector2(0, 100));
        test.Add(new Vector2(100, 0));
        List<Vector2> ret = BruteForceGetConvexHull(test);
        foreach (var iter in ret)
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
}

