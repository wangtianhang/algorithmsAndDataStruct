using System;
using System.Collections.Generic;
using System.Text;


class Distance3d
{
    /// <summary>
    /// 3d空间中点到直线距离
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="s"></param>
    /// <returns></returns>
//     public static float DistanceOfPoint3dWithLine3d(Line3d line3d, Vector3 s)
//     {
//         float ab = Mathf.Sqrt(Mathf.Pow((line3d.m_point1.x - line3d.m_point2.x), 2.0f) + Mathf.Pow((line3d.m_point1.y - line3d.m_point2.y), 2.0f) + Mathf.Pow((line3d.m_point1.z - line3d.m_point2.z), 2.0f));
//         float as2 = Mathf.Sqrt(Mathf.Pow((line3d.m_point1.x - s.x), 2.0f) + Mathf.Pow((line3d.m_point1.y - s.y), 2.0f) + Mathf.Pow((line3d.m_point1.z - s.z), 2.0f));
//         float bs = Mathf.Sqrt(Mathf.Pow((s.x - line3d.m_point2.x), 2.0f) + Mathf.Pow((s.y - line3d.m_point2.y), 2.0f) + Mathf.Pow((s.z - line3d.m_point2.z), 2.0f));
//         float cos_A = (Mathf.Pow(as2, 2.0f) + Mathf.Pow(ab, 2.0f) - Mathf.Pow(bs, 2.0f)) / (2 * ab * as2);
//         float sin_A = Mathf.Sqrt(1 - Mathf.Pow(cos_A, 2.0f));
//         return as2 * sin_A;
//     }
    public static Vector3 ClosestPointOfPoint3dWithSegment3d(Vector3 point, Segment3d line)
    {
        Vector3 lVec = line.m_point2 - line.m_point1; // Line Vector
        float t = Vector3.Dot(point - line.m_point1, lVec) / Vector3.Dot(lVec, lVec);
        t = Mathf.Max(t, 0.0f); // Clamp to 0
        t = Mathf.Min(t, 1.0f); // Clamp to 1
        return line.m_point1 + lVec * t;
    }

    public static Vector3 ClosestPointOfPoint3dWithLine3d(Vector3 point, Line3d line)
    {
        Vector3 lVec = line.m_point2 - line.m_point1; // Line Vector
        float t = Vector3.Dot(point - line.m_point1, lVec) / Vector3.Dot(lVec, lVec);
        return line.m_point1 + lVec * t;
    }

    public static Vector3 ClosestPointOfPoint3dWithAABB3d(Vector3 point, AABB3d aabb)
    {
        Vector3 result = point;
        Vector3 min = aabb.GetMin();
        Vector3 max = aabb.GetMax();
        result.x = (result.x < min.x) ? min.x : result.x;
        result.y = (result.y < min.x) ? min.y : result.y;
        result.z = (result.z < min.x) ? min.z : result.z;
        result.x = (result.x > max.x) ? max.x : result.x;
        result.y = (result.y > max.x) ? max.y : result.y;
        result.z = (result.z > max.x) ? max.z : result.z;
        return result;
    }

    public static Vector3 ClosestPointOfPoint3dWithOBB3d(Vector3 point, OBB3d obb)
    {
        Vector3 objMin = obb.GetAABBMin();
        Vector3 objMax = obb.GetAABBMax();
        Matrix4x4 obj2World = obb.GetObjToWorld();
        Matrix4x4 worldToObj = obj2World.inverse;
        Vector3 objPoint = worldToObj * point;
        
        Vector3 objResult = objPoint;
        objResult.x = (objResult.x < objMin.x) ? objMin.x : objResult.x;
        objResult.y = (objResult.y < objMin.x) ? objMin.y : objResult.y;
        objResult.z = (objResult.z < objMin.x) ? objMin.z : objResult.z;
        objResult.x = (objResult.x > objMax.x) ? objMax.x : objResult.x;
        objResult.y = (objResult.y > objMax.x) ? objMax.y : objResult.y;
        objResult.z = (objResult.z > objMax.x) ? objMax.z : objResult.z;

        Vector3 worldResult = worldToObj * objResult;
        return worldResult;
    }

    public static Vector3 ClosestPointOfPoint3dWithPlane3d(Vector3 point, Plane3d plane)
    {
        float dot = Vector3.Dot(plane.m_planeNormal, point);
        float distance = dot - plane.GetDistanceFromOrigin();
        return point - plane.m_planeNormal * distance;
    }

    public static Vector3 ClosestPointOfPoint3dWithRay3d(Vector3 point, Ray3d ray)
    {
        float t = Vector3.Dot(point - ray.m_rayOrigin, ray.m_rayDir);
        // We assume the direction of the ray is normalized
        // If for some reason the direction is not normalized
        // the below division is needed. So long as the ray
        // direction is normalized, we don't need this divide
        // t /= Dot(ray.direction, ray.direction);
        t = Mathf.Max(t, 0.0f);
        return ray.m_rayOrigin + ray.m_rayDir * t;
    }
}


