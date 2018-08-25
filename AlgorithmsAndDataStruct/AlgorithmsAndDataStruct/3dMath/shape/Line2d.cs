using System;
using System.Collections.Generic;
using System.Text;


class Line2d
{
    const double epsilon = 0.0000001d;
    public double m_slope; // 斜率
    public double m_y_interept; // y轴截距

    public Line2d(double s, double y)
    {
        m_slope = s;
        m_y_interept = y;
    }

    public bool intersect(Line2d line2)
    {
        return Math.Abs(m_slope - line2.m_slope) > epsilon
            || Math.Abs(m_y_interept - line2.m_y_interept) < epsilon;
    }
}

