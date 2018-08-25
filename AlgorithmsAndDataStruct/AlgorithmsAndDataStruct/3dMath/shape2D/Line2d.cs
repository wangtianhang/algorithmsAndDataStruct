using System;
using System.Collections.Generic;
using System.Text;


class Line2d
{
    public const double epsilon = 0.0000001d;
    public double m_slope; // 斜率
    public double m_y_interept; // y轴截距

    public Line2d(double s, double y)
    {
        m_slope = s;
        m_y_interept = y;
    }
}

