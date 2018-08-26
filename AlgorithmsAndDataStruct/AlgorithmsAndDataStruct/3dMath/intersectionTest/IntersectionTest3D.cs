using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 预计实现point3d ray3d line3d plane3d sphere3d aabb3d obb3d triangle3d mesh3d 
/// Capsule3d(胶囊 delay) cylinder(圆柱 delay) cone3d(圆锥 delay) frustum3d(视锥 平截头体 delay) polyhedron3d(多面体 delay)
/// </summary>
public class IntersectionTest3D
{
    public static void Test()
    {
        Plane3d plane = new Plane3d(new Vector3(1, 1, 1), new Vector3(10, 10, 10));
        Vector3 closestPoint = Distance3d.ClosestPointOfPoint3dWithPlane3d(new Vector3(20, 20, 20), plane);
        Debug.Log("最近点 " + closestPoint);
        closestPoint = Distance3d.ClosestPointOfPoint3dWithPlane3d(new Vector3(-10, -10, -10), plane);
        Debug.Log("最近点 " + closestPoint);
    }

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

    public bool Point3dWithOBB3d(Vector3 point, OBB3d obb)
    {
        Vector3 objMin = obb.GetAABBMin();
        Vector3 objMax = obb.GetAABBMax();
        Matrix4x4 obj2World = obb.GetObjToWorld();
        Matrix4x4 worldToObj = obj2World.inverse;
        Vector3 objPoint = worldToObj * point;
        if (objPoint.x < objMin.x || objPoint.y < objMin.y || objPoint.z < objMin.z)
        {
            return false;
        }
        if (objPoint.x > objMax.x || objPoint.y > objMax.y || objPoint.z > objMax.z)
        {
            return false;
        }
        return true;
    }

    public bool Point3dWithPlane3d(Vector3 point, Plane3d plane)
    {
        // 根据3d平面定义
        float dot = Vector3.Dot(plane.m_planeOnePoint - point, plane.m_planeNormal);
        return Mathf.Approximately(dot, 0);
    }

    public bool Point3dWithLine3d(Vector3 point, Line3d line)
    {
        Vector3 closestPoint = Distance3d.ClosestPointOfPoint3dWithLine3d(point, line);
        float distanceSq = (closestPoint - point).sqrMagnitude;
        return Mathf.Approximately(distanceSq, 0);
    }

    public bool Point3dWithRay3d(Vector3 point, Ray3d ray)
    {
        if(point == ray.m_rayOrigin)
        {
            return true;
        }
        Vector3 norm = point - ray.m_rayOrigin;
        norm.Normalize();
        float diff = Vector3.Dot(norm, ray.m_rayDir);
        return Mathf.Approximately(diff, 1);
    }
}

