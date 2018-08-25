using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 预计实现point3d ray3d line3d plane3d sphere3d aabb3d obb3d triangle3d mesh3d 
/// Capsule3d(胶囊 delay) cylinder(圆柱 delay) cone3d(圆锥 delay) frustum3d(视锥 平截头体 delay) polyhedron3d(多面体 delay)
/// </summary>
class IntersectionTest3D
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

