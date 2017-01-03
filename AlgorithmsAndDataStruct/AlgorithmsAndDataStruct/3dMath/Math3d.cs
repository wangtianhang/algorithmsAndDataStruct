using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Math3d
{
    public static Vector3 GetIntersectionPoint(Vector3 rayOrigin, Vector3 rayDir, Vector3 planeNormal, Vector3 planeOnePoint)
    {
        float t = (Vector3.Dot(planeNormal, planeOnePoint) - Vector3.Dot(planeNormal, rayOrigin))
            / (Vector3.Dot(planeNormal, rayDir));
        Vector3 intersectionPoint = rayOrigin + rayDir * t;
        return intersectionPoint;
    }
}

