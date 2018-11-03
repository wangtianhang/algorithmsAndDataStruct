using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public struct Plane3d
{
    public Plane3d(Vector3L normal, Vector3L point)
    {
        m_planeNormal = normal;
        m_planeNormal.Normalize();
        m_planeOnePoint = point;
    }
    public Vector3L m_planeNormal;
    public Vector3L m_planeOnePoint;

    public FloatL GetDistanceFromOrigin()
    {
        return Vector3L.Dot(m_planeOnePoint, m_planeNormal);
    }

    public static FloatL PlaneEquation(Vector3L point, Plane3d plane)
    {
        return Vector3L.Dot(point, plane.m_planeNormal) - plane.GetDistanceFromOrigin();
    }
}

