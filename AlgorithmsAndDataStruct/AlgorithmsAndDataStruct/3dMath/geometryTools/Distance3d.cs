using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

class Distance3d
{
    /// <summary>
    /// 3d空间中点到直线距离
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="s"></param>
    /// <returns></returns>
//     public static FloatL DistanceOfPoint3dWithLine3d(Line3d line3d, Vector3L s)
//     {
//         FloatL ab = FixPointMath.Sqrt(FixPointMath.Pow((line3d.m_point1.x - line3d.m_point2.x), 2.0f) + FixPointMath.Pow((line3d.m_point1.y - line3d.m_point2.y), 2.0f) + FixPointMath.Pow((line3d.m_point1.z - line3d.m_point2.z), 2.0f));
//         FloatL as2 = FixPointMath.Sqrt(FixPointMath.Pow((line3d.m_point1.x - s.x), 2.0f) + FixPointMath.Pow((line3d.m_point1.y - s.y), 2.0f) + FixPointMath.Pow((line3d.m_point1.z - s.z), 2.0f));
//         FloatL bs = FixPointMath.Sqrt(FixPointMath.Pow((s.x - line3d.m_point2.x), 2.0f) + FixPointMath.Pow((s.y - line3d.m_point2.y), 2.0f) + FixPointMath.Pow((s.z - line3d.m_point2.z), 2.0f));
//         FloatL cos_A = (FixPointMath.Pow(as2, 2.0f) + FixPointMath.Pow(ab, 2.0f) - FixPointMath.Pow(bs, 2.0f)) / (2 * ab * as2);
//         FloatL sin_A = FixPointMath.Sqrt(1 - FixPointMath.Pow(cos_A, 2.0f));
//         return as2 * sin_A;
//     }
    public static Vector3L ClosestPointOfPoint3dWithSegment3d(Vector3L point, Segment3d line)
    {
        Vector3L lVec = line.m_point2 - line.m_point1; // Line Vector
        FloatL t = Vector3L.Dot(point - line.m_point1, lVec) / Vector3L.Dot(lVec, lVec);
        t = FixPointMath.Max(t, 0.0f); // Clamp to 0
        t = FixPointMath.Min(t, 1.0f); // Clamp to 1
        return line.m_point1 + lVec * t;
    }

    public static Vector3L ClosestPointOfPoint3dWithLine3d(Vector3L point, Line3d line)
    {
        Vector3L lVec = line.m_point2 - line.m_point1; // Line Vector
        FloatL t = Vector3L.Dot(point - line.m_point1, lVec) / Vector3L.Dot(lVec, lVec);
        return line.m_point1 + lVec * t;
    }

    public static Vector3L ClosestPointOfPoint3dWithAABB3d(Vector3L point, AABB3d aabb)
    {
        Vector3L result = point;
        Vector3L min = aabb.GetMin();
        Vector3L max = aabb.GetMax();
        result.x = (result.x < min.x) ? min.x : result.x;
        result.y = (result.y < min.x) ? min.y : result.y;
        result.z = (result.z < min.x) ? min.z : result.z;
        result.x = (result.x > max.x) ? max.x : result.x;
        result.y = (result.y > max.x) ? max.y : result.y;
        result.z = (result.z > max.x) ? max.z : result.z;
        return result;
    }

    public static Vector3L ClosestPointOfPoint3dWithOBB3d(Vector3L point, OBB3d obb)
    {
        //Vector3L objMin = obb.GetAABBMin();
        //Vector3L objMax = obb.GetAABBMax();
        Matrix4x4L obj2World = obb.GetObjToWorld();
        Matrix4x4L worldToObj = obj2World.inverse;
        Vector3L objMin = (Vector3L)(worldToObj * obb.m_pos) - obb.GetHalfSize();
        Vector3L objMax = (Vector3L)(worldToObj * obb.m_pos) + obb.GetHalfSize();
        Vector3L objPoint = worldToObj * point;
        
        Vector3L objResult = objPoint;
        objResult.x = (objResult.x < objMin.x) ? objMin.x : objResult.x;
        objResult.y = (objResult.y < objMin.x) ? objMin.y : objResult.y;
        objResult.z = (objResult.z < objMin.x) ? objMin.z : objResult.z;
        objResult.x = (objResult.x > objMax.x) ? objMax.x : objResult.x;
        objResult.y = (objResult.y > objMax.x) ? objMax.y : objResult.y;
        objResult.z = (objResult.z > objMax.x) ? objMax.z : objResult.z;

        Vector3L worldResult = obj2World * objResult;
        return worldResult;
    }

    public static Vector3L ClosestPointOfPoint3dWithPlane3d(Vector3L point, Plane3d plane)
    {
        FloatL dot = Vector3L.Dot(plane.m_planeNormal, point);
        FloatL distance = dot - plane.GetDistanceFromOrigin();
        return point - plane.m_planeNormal * distance;
    }

    public static Vector3L ClosestPointOfPoint3dWithRay3d(Vector3L point, Ray3d ray)
    {
        FloatL t = Vector3L.Dot(point - ray.m_rayOrigin, ray.m_rayDir);
        // We assume the direction of the ray is normalized
        // If for some reason the direction is not normalized
        // the below division is needed. So long as the ray
        // direction is normalized, we don't need this divide
        // t /= Dot(ray.direction, ray.direction);
        t = FixPointMath.Max(t, 0.0f);
        return ray.m_rayOrigin + ray.m_rayDir * t;
    }

    public static Plane3d FromTriangle(Triangle3d t)
    {
        Plane3d result = new Plane3d(Vector3L.Cross(t.m_point1 - t.m_point0, t.m_point2 - t.m_point0), t.m_point0);
        return result;
    }

    public static Vector3L ClosestPointOfPoint3dWithTriangle3d(Triangle3d t, Vector3L p) 
    {
	    Plane3d plane = FromTriangle(t);
        Vector3L closest = ClosestPointOfPoint3dWithPlane3d(p, plane);

	    // Closest point was inside triangle
        if (IntersectionTest3D.Point3dWithTriangle(closest, t))
        {
		    return closest;
	    }

        Vector3L c1 = ClosestPointOfPoint3dWithSegment3d(closest, new Segment3d(t.m_point0, t.m_point1)); // Line AB
        Vector3L c2 = ClosestPointOfPoint3dWithSegment3d(closest, new Segment3d(t.m_point1, t.m_point2)); // Line BC
        Vector3L c3 = ClosestPointOfPoint3dWithSegment3d(closest, new Segment3d(t.m_point2, t.m_point0)); // Line CA

	    FloatL magSq1 = (closest - c1).sqrMagnitude;
        FloatL magSq2 = (closest - c2).sqrMagnitude;
        FloatL magSq3 = (closest - c3).sqrMagnitude;

	    if (magSq1 < magSq2 && magSq1 < magSq3) {
		    return c1;
	    }
	    else if (magSq2 < magSq1 && magSq2 < magSq3) {
		    return c2;
	    }

	    return c3;
    }
}


