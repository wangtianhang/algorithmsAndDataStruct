using System;
using System.Collections.Generic;
using System.Text;


class IntersectionTest
{
    /// <summary>
    /// 射线与平面相交
    /// </summary>
    /// <param name="rayOrigin"></param>
    /// <param name="rayDir"></param>
    /// <param name="planeNormal"></param>
    /// <param name="planeOnePoint"></param>
    /// <returns></returns>
    public static bool RayToPlane(Ray3d ray, Plane3d plane, out Vector3 result)
    {
        float t = (Vector3.Dot(plane.m_planeNormal, plane.m_planeOnePoint) - Vector3.Dot(plane.m_planeNormal, ray.m_rayOrigin))
            / (Vector3.Dot(plane.m_planeNormal, ray.m_rayDir));

        if (t < 0)
        {
            result = Vector3.zero;
            return false;
        }
        else
        {
            result = ray.m_rayOrigin + ray.m_rayDir * t;
            return true;
        }

    }
}

