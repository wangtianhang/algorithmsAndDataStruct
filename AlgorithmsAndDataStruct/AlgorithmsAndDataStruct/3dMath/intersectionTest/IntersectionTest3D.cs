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

        OBB3d obb1 = new OBB3d(new Vector3(-5, 0, 0), Quaternion.Euler(Vector3.zero), 9, 20, 10);
        OBB3d obb2 = new OBB3d(new Vector3(5, 0, 0), Quaternion.Euler(Vector3.zero), 9, 20, 10);
        Debug.Log("obb相交测试1 " + OBB3dWithOBB3d(obb1, obb2));
        OBB3d obb3 = new OBB3d(new Vector3(5, 0, 0), Quaternion.Euler(new Vector3(0, 0, 90)), 9, 20, 10);
        Debug.Log("obb相交测试2 " + OBB3dWithOBB3d(obb1, obb3));
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

    public bool Sphere3dWithAABB3d(Sphere3d sphere, AABB3d aabb)
    {
        Vector3 closestPoint = Distance3d.ClosestPointOfPoint3dWithAABB3d(sphere.m_pos, aabb);
        return (sphere.m_pos - closestPoint).magnitude <= sphere.m_radius;
    }

    public bool Sphere3dWithObb3d(Sphere3d sphere, OBB3d obb)
    {
        Vector3 closestPoint = Distance3d.ClosestPointOfPoint3dWithOBB3d(sphere.m_pos, obb);
        return (sphere.m_pos - closestPoint).magnitude <= sphere.m_radius;
    }

    public bool Sphere3dWithPlane3d(Sphere3d sphere, Plane3d plane)
    {
        Vector3 closestPoint = Distance3d.ClosestPointOfPoint3dWithPlane3d(sphere.m_pos, plane);
        return (sphere.m_pos - closestPoint).magnitude <= sphere.m_radius;
    }

    public bool AABB3dWithAABB3d(AABB3d aabb1, AABB3d aabb2)
    {
        Vector3 aMin = aabb1.GetMin();
        Vector3 aMax = aabb1.GetMax();

        Vector3 bMin = aabb2.GetMin();
        Vector3 bMax = aabb2.GetMax();

        return (aMin.x <= bMax.x && aMax.x >= bMin.x) &&
            (aMin.y <= bMax.y && aMax.y >= bMin.y) &&
            (aMin.z <= bMax.z && aMax.z >= bMin.z);
    }



    #region Separating Axis Theorem
    /// <summary>
    /// 来自game physics cookbook
    /// https://github.com/gszauer/GamePhysicsCookbook
    /// </summary>
    class Interval
    {
        public float min;
        public float max;
    }

    static Interval GetInterval(OBB3d obb, Vector3 axis)
    {
        Vector3[] vertex = new Vector3[8];

	    Vector3 C = obb.m_pos;	// OBB Center
	    Vector3 E = obb.GetHalfSize();		// OBB Extents
	    float[] o = obb.GetOrientationMatrixArray();
	    Vector3[] A = {			// OBB Axis
		    new Vector3(o[0], o[1], o[2]),
		    new Vector3(o[3], o[4], o[5]),
		    new Vector3(o[6], o[7], o[8]),
	    };

	    vertex[0] = C + A[0] * E[0] + A[1] * E[1] + A[2] * E[2];
	    vertex[1] = C - A[0] * E[0] + A[1] * E[1] + A[2] * E[2];
	    vertex[2] = C + A[0] * E[0] - A[1] * E[1] + A[2] * E[2];
	    vertex[3] = C + A[0] * E[0] + A[1] * E[1] - A[2] * E[2];
	    vertex[4] = C - A[0] * E[0] - A[1] * E[1] - A[2] * E[2];
	    vertex[5] = C + A[0] * E[0] - A[1] * E[1] - A[2] * E[2];
	    vertex[6] = C - A[0] * E[0] + A[1] * E[1] - A[2] * E[2];
	    vertex[7] = C - A[0] * E[0] - A[1] * E[1] + A[2] * E[2];

	    Interval result = new Interval();
	    result.min = result.max = Vector3.Dot(axis, vertex[0]);

	    for (int i = 1; i < 8; ++i) {
            float projection = Vector3.Dot(axis, vertex[i]);
		    result.min = (projection < result.min) ? projection : result.min;
		    result.max = (projection > result.max) ? projection : result.max;
	    }

	    return result;
    }

    static bool OverlapOnAxis(OBB3d obb1, OBB3d obb2, Vector3 axis)
    {
        Interval a = GetInterval(obb1, axis);
        Interval b = GetInterval(obb2, axis);
        return ((b.min <= a.max) && (a.min <= b.max));
    }

    public static bool OBB3dWithOBB3d(OBB3d obb1, OBB3d obb2) {
	    float[] o1 = obb1.GetOrientationMatrixArray();
	    float[] o2 = obb2.GetOrientationMatrixArray();

	    Vector3[] test2 = {
		    new Vector3(o1[0], o1[1], o1[2]),
		    new Vector3(o1[3], o1[4], o1[5]),
		    new Vector3(o1[6], o1[7], o1[8]),
		    new Vector3(o2[0], o2[1], o2[2]),
		    new Vector3(o2[3], o2[4], o2[5]),
		    new Vector3(o2[6], o2[7], o2[8])
	    };

        Vector3[] test = new Vector3[15];
        Array.Copy(test2, test, test2.Length);

	    for (int i = 0; i < 3; ++i) { // Fill out rest of axis
            test[6 + i * 3 + 0] = Vector3.Cross(test[i], test[0]);
            test[6 + i * 3 + 1] = Vector3.Cross(test[i], test[1]);
            test[6 + i * 3 + 2] = Vector3.Cross(test[i], test[2]);
	    }

	    for (int i = 0; i < 15; ++i) {
		    if (!OverlapOnAxis(obb1, obb2, test[i])) {
			    return false; // Seperating axis found
		    }
	    }

	    return true; // Seperating axis not found
    }
    #endregion

    public static bool Obb3dWithPlane3d(OBB3d obb, Plane3d plane)
    {
        // Local variables for readability only
	    float[] o = obb.GetOrientationMatrixArray();
	    Vector3[] rot = { // rotation / orientation
		    new Vector3(o[0], o[1], o[2]),
		    new Vector3(o[3], o[4], o[5]),
		    new Vector3(o[6], o[7], o[8]),
	    };
        Vector3 normal = plane.m_planeNormal;

	    // Project the half extents of the AABB onto the plane normal
        float pLen = obb.GetHalfSize().x * Mathf.Abs(Vector3.Dot(normal, rot[0])) +
                    obb.GetHalfSize().y * Mathf.Abs(Vector3.Dot(normal, rot[1])) +
                    obb.GetHalfSize().z * Mathf.Abs(Vector3.Dot(normal, rot[2]));
	    // Find the distance from the center of the OBB to the plane
	    float dist = Vector3.Dot(plane.m_planeNormal, obb.m_pos) - plane.GetDistanceFromOrigin();
	    // Intersection occurs if the distance falls within the projected side
	    return Mathf.Abs(dist) <= pLen;
    }

    public bool Plane3dWithPlane3d(Plane3d plane1, Plane3d plane2)
    {
        Vector3 d = Vector3.Cross(plane1.m_planeNormal, plane2.m_planeNormal);
        return Mathf.Approximately(Vector3.Dot(d, d), 0);
    }
}

