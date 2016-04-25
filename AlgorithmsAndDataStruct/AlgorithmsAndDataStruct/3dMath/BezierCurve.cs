using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

