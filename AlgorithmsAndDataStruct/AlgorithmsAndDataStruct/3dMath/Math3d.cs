﻿using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;
using UnityEngine;

public class Math3d
{
    public static void Test()
    {
        XAxisWeightMinDistance();

        XAxisWeightMinDistance2();

        XAxisWeightMinDistance3();

        XYPlaneMinDistance();


    }

    /// <summary>
    /// 从矩阵中提取position分量
    /// </summary>
    /// <param name="m"></param>
    /// <returns></returns>
    public static Vector3 GetPositionFromMatrix(Matrix4x4 m)
    {
        return m.GetColumn(3);
    }

    /// <summary>
    /// 从矩阵中提取scale分量
    /// </summary>
    /// <param name="m"></param>
    /// <returns></returns>
    public static Vector3 GetScaleFromMatrix(Matrix4x4 m)
    {
        return new Vector3
             (m.GetColumn(0).magnitude, m.GetColumn(1).magnitude, m.GetColumn(2).magnitude);
    }

    //public const float Deg2Rad = 0.0174533f;
    //public const float Rad2Deg = 57.2958f;



    /// <summary>
    /// 点在矩形内部
    /// </summary>
    /// <param name="p"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="d"></param>
    /// <returns></returns>
//     public static bool IsInRectangle(Vector3 p, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
//     {
//         Vector3 v11 = p - a;
//         Vector3 v12 = b - a;
//         Vector3 v21 = p - b;
//         Vector3 v22 = c - b;
//         Vector3 v31 = p - c;
//         Vector3 v32 = d - c;
//         Vector3 v41 = p - d;
//         Vector4 v42 = a - d;
// 
//         if (Vector3.Cross(v11, v12).normalized == Vector3.Cross(v21, v22).normalized
//             && Vector3.Cross(v21, v22).normalized == Vector3.Cross(v31, v32).normalized
//             && Vector3.Cross(v31, v32).normalized == Vector3.Cross(v41, v42).normalized)
//         {
//             return true;
//         }
//         else
//         {
//             return false;
//         }
//     }

    /// <summary>
    /// 仅支持凸多边形
    /// </summary>
    /// <param name="p"></param>
    /// <param name="polygon"></param>
    /// <returns></returns>
//     public static bool IsInConvexPolygon(Vector3 p, List<Vector3> polygon)
//     {
//         if (polygon.Count < 3)
//         {
//             return false;
//         }
// 
//         p.y = 0;
//         for (int i = 0; i < polygon.Count; ++i)
//         {
//             Vector3 tmp = polygon[i];
//             tmp.y = 0;
//             polygon[i] = tmp;
//         }
// 
//         List<Vector3> crossResultList = new List<Vector3>();
//         for (int i = 0; i < polygon.Count; ++i)
//         {
//             if (i == polygon.Count - 1)
//             {
//                 Vector3 v1 = p - polygon[i];
//                 Vector3 v2 = polygon[0] - polygon[i];
//                 Vector3 v3 = Vector3.Cross(v1, v2);
//                 crossResultList.Add(v3);
//             }
//             else
//             {
//                 Vector3 v1 = p - polygon[i];
//                 Vector3 v2 = polygon[i + 1] - polygon[i];
//                 Vector3 v3 = Vector3.Cross(v1, v2);
//                 crossResultList.Add(v3);
//             }
//         }
// 
//         for (int i = 0; i < crossResultList.Count; ++i)
//         {
//             if (i == crossResultList.Count - 1)
//             {
//                 if (crossResultList[i].y * crossResultList[0].y < 0)
//                 {
//                     return false;
//                 }
//             }
//             else
//             {
//                 if (crossResultList[i].y * crossResultList[i + 1].y < 0)
//                 {
//                     return false;
//                 }
//             }
//         }
// 
//         return true;
//     }

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
//     public bool IntersectTriangle(Vector3 orig, Vector3 dir, Vector3 v0, Vector3 v1, Vector3 v2, ref float t, ref float u, ref float v)
//     {
//         
//         // Find vectors for two edges sharing vert0
//         Vector3 edge1 = v1 - v0;
//         Vector3 edge2 = v2 - v0;
// 
//         // Begin calculating determinant - also used to calculate U parameter
//         Vector3 pvec = Vector3.Cross(dir, edge2);
// 
//         // If determinant is near zero, ray lies in plane of triangle
//         float det = Vector3.Dot(edge1, pvec);
// 
//         Vector3 tvec;
//         if(det > 0)
//         {
//             tvec = orig - v0;
//         }
//         else
//         {
//             tvec = v0 - orig;
//             det = -det;
//         }
// 
//         if (det < 0.0001f)
//             return false;
// 
//         // Calculate U parameter and test bounds
//         u = Vector3.Dot(tvec, pvec);
//         if(u < 0.0f || u > det)
//         {
//             return false;
//         }
// 
//         // Prepare to test V parameter
//         Vector3 qvec = Vector3.Cross(tvec, edge1);
// 
//         // Calculate V parameter and test bounds
//         v = Vector3.Dot(dir, qvec);
//         if(v < 0.0f || u + v > det)
//         {
//             return false;
//         }
// 
//         // Calculate t, scale parameters, ray intersects triangle
//         t = Vector3.Dot(edge2, qvec);
//         float fInvDet = 1.0f / det;
//         t *= fInvDet;
//         u *= fInvDet;
//         v *= fInvDet;
//         return true;
//     }

    






    #region 邮局测试代码 不要用
    class XAxisWeightMinDistanceData
    {
        public List<double> m_posList = new List<double>();

        public XAxisWeightMinDistanceData()
        {
            // 先人肉排序了..
            m_posList.Add(5d);
            m_posList.Add(5d);
            m_posList.Add(10d);
            m_posList.Add(10d);
            m_posList.Add(15d);
            m_posList.Add(20d);
            m_posList.Add(35d);
        }

        public double GetDistance(double curPos)
        {
            double sum = 0;
            foreach (var iter in m_posList)
            {
                //double weight = iter.Item2;
                sum += Math.Abs(curPos - iter);
            }
            return sum;
        }

        public double GetDistanceDerivative(double curPos, double delta = 0.001d)
        {
            return Algebra.DerivativeD(GetDistance, curPos, delta);
        }

        public double GetMinWeightDistanceX()
        {
//             double total = 0;
//             foreach (var iter in m_posList)
//             {
//                 total += iter;
//             }

//             double left = 0;
//             double right = 0;
// 
//             double sum = 0;
//             for (int i = 0; i < m_posList.Count; ++i )
//             {
//                 sum += m_posList[i];
//                 if (sum >= total / 2)
//                 {
//                     right = m_posList[i];
//                     break;
//                 }
//             }
// 
//             sum = 0;
//             for (int i = m_posList.Count - 1; i >= 0; --i)
//             {
//                 sum += m_posList[i];
//                 if (sum >= total / 2)
//                 {
//                     left = m_posList[i];
//                     break;
//                 }
//             }

            double minX = Algebra.GetMinByThree(GetDistance, m_posList[0], m_posList[m_posList.Count - 1]);
            return minX;
        }
    }

    public static void XAxisWeightMinDistance()
    {
        XAxisWeightMinDistanceData test = new XAxisWeightMinDistanceData();

        double total = 0;
        foreach (var iter in test.m_posList)
        {
            total += iter;
        }

        double minDistance = double.MaxValue;
        double minPos = double.MaxValue;
        double step = 0.1d;
        for (int i = 0; i < 1000; ++i )
        {
            double curPos = step * i;
            double sum = 0;
            foreach (var iter in test.m_posList)
            {
                //double weight = iter.Item2;
                sum += Math.Abs(curPos - iter);
            }
            if(sum < minDistance)
            {
                minDistance = sum;
                minPos = curPos;
            }
        }
        Debug.Log("total " + total + " XAxisWeightMinDistance " + minDistance + " minPos " + minPos);

        List<FunctionDraw> drawList = new List<FunctionDraw>();
        drawList.Add(new FunctionDraw(test.GetDistance, 0.001d));
        Bitmap bitmap = DrawFunctionHelper.DrawFunction(drawList,
            test.m_posList[0] - 10, test.m_posList[test.m_posList.Count - 1], -10, +100, "XAxisWeightMinDistance");
        string fileName = "XAxisWeightMinDistance" + Debug.GetTime() + ".bmp";
        bitmap.Save(fileName);
    }

    public static void XAxisWeightMinDistance2()
    {
        XAxisWeightMinDistanceData test = new XAxisWeightMinDistanceData();
        double total = 0;
        foreach (var iter in test.m_posList)
        {
            total += iter;
        }

        // f(x) = Abs(x - x1) * weight1 + ...Abs(x - xN) * weightN - ( total / 2)
        double t1 = 20d, t2 = 0;
        do
        {
            double sum = test.GetDistance(t1);
            t2 = t1 - (sum - total / 2) / test.GetDistanceDerivative(t1);

            if (Math.Abs(t1 - t2) < 0.001d) 
                break;

            t1 = t2;

        } while (true);

        double minSum = test.GetDistance(t2);
        Debug.Log("XAxisWeightMinDistance2 " + minSum + " minPos " + t2);
    }

    public static void XAxisWeightMinDistance3()
    {
        XAxisWeightMinDistanceData test = new XAxisWeightMinDistanceData();
        double minX = test.GetMinWeightDistanceX();
        double minSum = test.GetDistance(minX);
        Debug.Log("XAxisWeightMinDistance3 " + minSum + " minX " + minX);
    }

    public static void XYPlaneMinDistance()
    {

        List<Vector2> posList = new List<Vector2>();
        posList.Add(new Vector2(0.1f, 0.1f));
        posList.Add(new Vector2(0.35f, 0.35f));
        posList.Add(new Vector2(0.05f, 0.05f));
        posList.Add(new Vector2(0.1f, 0.1f));
        posList.Add(new Vector2(0.15f, 0.15f));
        posList.Add(new Vector2(0.05f, 0.05f));
        posList.Add(new Vector2(0.2f, 0.2f));
        long start = QueryPerfCounter.GetMs();
        float minDistance = float.MaxValue;
        Vector2 minPos = Vector2.zero;
        float step = 0.001f;
        for (int i = 0; i < 1000; ++i)
        {
            for (int j = 0; j < 1000; ++j )
            {
                Vector2 curPos = new Vector2(step * i, step * j);
                float sum = 0;
                foreach (var iter in posList)
                {
                    sum += (curPos - iter).magnitude;
                }

                if(sum < minDistance)
                {
                    minDistance = sum;
                    minPos = curPos;
                }
            }

        }
        long end = QueryPerfCounter.GetMs();
        Debug.Log("XYPlaneMinDistance " + minDistance + " minPos " + minPos + " time " + (end - start));
    }
    #endregion
}

