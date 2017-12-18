using System;
using System.Collections.Generic;

using System.Text;



class BezierCurve
{
    public Vector3 LinearBezier(Vector3 p0, Vector3 p1, float t/*[0~1]*/)
    {
        return (1 - t) * p0 + t * p1;
    }

    public Vector3 QuadraticBezier(Vector3 p0, Vector3 p1, Vector3 p2, float t/*[0~1]*/)
    {
        return (1 - t) * (1 - t) * p0 + 2 * (1 - t) * t * p1 + t * t * p2;
    }


}

//http://blog.csdn.net/kongbu0622/article/details/10123989
public class BezierCurveConstantMotion
{
    public static void Test()
    {
        BezierCurveConstantMotion test = new BezierCurveConstantMotion(new Vector2(50, 50), new Vector2(500, 500), new Vector2(800, 200));
        List<Vector2> posList = new List<Vector2>();
        for (int i = 0; i < 10; ++i )
        {
            float percent = 0.1f * i;
            Vector2 pos = test.CalculatePos(percent);
            float distancef = 0;
            if (i - 1 >= 0)
            {
                Vector2 distance = pos - posList[i - 1];
                distancef = distance.magnitude;
            }
            posList.Add(pos);
            Debug.Log(pos.ToString() + " " + distancef);
        }
    }

    Vector2 m_P0;
    Vector2 m_P1;
    Vector2 m_P2;

    double m_A = 0;
    double m_B = 0;
    double m_C = 0;

    public double m_length = 0;

    public BezierCurveConstantMotion(Vector2 P0, Vector2 P1, Vector2 P2)
    {
        m_P0 = P0;
        m_P1 = P1;
        m_P2 = P2;

        float ax = m_P0.x - 2 * m_P1.x + m_P2.x;
        float ay = m_P0.y - 2 * m_P1.y + m_P2.y;
        float bx = 2 * m_P1.x - 2 * m_P0.x;
        float by = 2 * m_P1.y - 2 * m_P0.y;

        m_A = 4 * (ax * ax + ay * ay);
        m_B = 4 * (ax * bx + ay * by);
        m_C = bx * bx + by * by;

        m_length = Length();
    }

    /// <summary>
    /// 号称求贝塞尔曲线长度（积分）
    /// </summary>
    /// <param name="P0"></param>
    /// <param name="P1"></param>
    /// <param name="P2"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    double Length(double t = 1)
    {
        double temp1 = Math.Sqrt(m_C + t * (m_B + m_A * t));
        double temp2 = (2 * m_A * t * temp1 + m_B * (temp1 - Math.Sqrt(m_C)));
        double temp3 = Math.Log(m_B + 2 * Math.Sqrt(m_A) * Math.Sqrt(m_C));
        double temp4 = Math.Log(m_B + 2 * m_A * t + 2 * Math.Sqrt(m_A) * temp1);
        double temp5 = 2 * Math.Sqrt(m_A) * temp2;
        double temp6 = (m_B * m_B - 4 * m_A * m_C) * (temp3 - temp4);
        return (temp5 + temp6) / (8 * Math.Pow(m_A, 1.5));
    }

    double V(double t)
    {
        return Math.Sqrt(m_A * t * t + m_B * t + m_C);
    }

    /// <summary>
    /// 根据长度百分比逆推t
    /// </summary>
    /// <param name="l"></param>
    /// <param name="percent"></param>
    /// <returns></returns>
    double InvertLength(double percent)
    {
        double t1 = percent, t2;

        // 牛顿切线法求解L(t1) = L(1.0) * percent;
        // Xn+1 = Xn - (L(xn) - L(1.0) * percent / L'(xn)) 
        do
        {
            t2 = t1 - (Length(t1) - m_length * percent) / V(t1);

            if (Math.Abs(t1 - t2) < 0.000001) 
                break;

            t1 = t2;

        } while (true);

        return t2;
    }

    public Vector2 CalculatePos(float percent/*[0~1]*/)
    {
        //Debug.Log("length " + Length(percent));
        double t = InvertLength(percent);
        return QuadraticBezier(m_P0, m_P1, m_P2, (float)t);
    }

    Vector2 QuadraticBezier(Vector2 p0, Vector2 p1, Vector2 p2, float t/*[0~1]*/)
    {
        return (1 - t) * (1 - t) * p0 + 2 * (1 - t) * t * p1 + t * t * p2;
    }
}

