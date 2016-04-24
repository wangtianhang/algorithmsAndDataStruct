using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class MathCollection
{
    /// <summary>
    /// 牛顿法求平方根
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static double sqrt(double c)
    {
        if(c < 0)
        {
            return Double.NaN;
        }

        double err = 1e-15;
        double t = c;
        while(Math.Abs(t - c / t) > err * t)
        {
            t = (c / t + t) / 2.0;
        }
        return t;
    }
}

