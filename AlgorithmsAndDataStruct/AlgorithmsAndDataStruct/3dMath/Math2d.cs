using System;
using System.Collections.Generic;
using System.Text;


class Math2d
{
    /// <summary>
    /// 三点确定一个圆
    /// </summary>
    /// <returns></returns>
    public bool ThreePointCalculateCircle(Vector2 pt1, Vector2 pt2, Vector2 pt3, ref Circle2d result)
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
        if (Math.Abs(det) < 1e-5)
        {
            //ret.Item2 = -1f;
            return false;
        }

        double x0 = -(d * e - b * f) / det;
        double y0 = -(a * f - c * e) / det;
        double radius = Math.Sqrt((x1 - x0) * (x1 - x0) + (y1 - y0) * (y1 - y0));
        result = new Circle2d(new Vector2((float)x0, (float)y0), (float)radius);
        return true;
    }

    /// <summary>
    /// 根据点集合求包围圆
    /// </summary>
    /// <returns></returns>
    public Circle2d GetCircle2dByPointSet(List<Vector2> pointList)
    {
        Vector2 center = new Vector2();
        foreach(var iter in pointList)
        {
            center += iter;
        }
        center /= pointList.Count;
        float radius = float.MinValue;
        foreach(var iter in pointList)
        {
            Vector2 distance = iter - center;
            if (distance.magnitude > radius)
            {
                radius = distance.magnitude;
            }
        }
        return new Circle2d(center, radius);
    }

    /// <summary>
    /// 根据点集合求包围aa矩形
    /// </summary>
    /// <returns></returns>
    public AARectangle GetAARectangleByPointSet(List<Vector2> pointList)
    {
        Vector2 min = pointList[0];
        Vector2 max = pointList[0];
        for (int i = 1; i < pointList.Count; ++i)
        {
            min.x = pointList[i].x < min.x ?
            pointList[i].x : min.x;
            min.y = pointList[i].y < min.y ?
            pointList[i].y : min.y;
            max.x = pointList[i].x > max.x ?
            pointList[i].x : max.x;
            max.y = pointList[i].y > max.y ?
            pointList[i].y : max.y;
        }

        Vector3 pos = (max - min) / 2;
        float length = max.y - min.y;
        float width = max.x - min.x;
        return new AARectangle(pos, width, length);
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

