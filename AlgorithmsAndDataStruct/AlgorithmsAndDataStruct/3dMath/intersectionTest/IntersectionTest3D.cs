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
    public static bool Ray3dWithPlane3d(Ray3d ray, Plane3d plane, out Vector3 result)
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

    /// <summary>
    /// 来自于微软pick.cpp demo
    /// </summary>
    /// <param name="ray3d"></param>
    /// <param name="triangle3d"></param>
    /// <param name="t"></param>
    /// <param name="u"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    public static bool Ray3dWithTriangle3d(Ray3d ray3d,
                    Triangle3d triangle3d,
                    ref float t, ref float u, ref float v)
    {
        // Find vectors for two edges sharing vert0
        Vector3 edge1 = triangle3d.m_point1 - triangle3d.m_point0;
        Vector3 edge2 = triangle3d.m_point2 - triangle3d.m_point0;

        // Begin calculating determinant - also used to calculate U parameter
        Vector3 pvec;
        pvec = Vector3.Cross(ray3d.m_rayDir, edge2);

        // If determinant is near zero, ray lies in plane of triangle
        float det = Vector3.Dot(edge1, pvec);

        Vector3 tvec;
        if (det > 0)
        {
            tvec = ray3d.m_rayOrigin - triangle3d.m_point0;
        }
        else
        {
            tvec = triangle3d.m_point0 - ray3d.m_rayOrigin;
            det = -det;
        }

        if (det < 0.0001f)
            return false;

        // Calculate U parameter and test bounds
        u = Vector3.Dot(tvec, pvec);
        if (u < 0.0f || u > det)
            return false;

        // Prepare to test V parameter
        Vector3 qvec;
        qvec = Vector3.Cross(tvec, edge1);

        // Calculate V parameter and test bounds
        v = Vector3.Dot(ray3d.m_rayDir, qvec);
        if (v < 0.0f || u + v > det)
            return false;

        // Calculate t, scale parameters, ray intersects triangle
        t = Vector3.Dot(edge2, qvec);
        float fInvDet = 1.0f / det;
        t *= fInvDet;
        u *= fInvDet;
        v *= fInvDet;

        return true;
    }
}

