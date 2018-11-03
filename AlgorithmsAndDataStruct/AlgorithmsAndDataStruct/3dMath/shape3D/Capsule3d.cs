using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;


public struct Capsule3d
{
    public Vector3L m_point1;
    public Vector3L m_point2;
    public FloatL m_radius;

    public Capsule3d(Vector3L point1, Vector3L point2, FloatL radius)
    {
        m_point1 = point1;
        m_point2 = point2;
        m_radius = radius;
    }

    public Vector3L AnyPointFast()
    {
        return m_point1;
    }

    public Vector3L ExtremePoint(Vector3L direction)
    {
        FloatL len = direction.magnitude;
        return Vector3L.Dot(direction, m_point2 -  m_point1) >= 0 ? m_point2 : m_point1  + direction * (m_radius / len);
    }

    public Vector3L ExtremePoint(Vector3L direction, ref FloatL projectionDistance)
    {
        Vector3L extremePoint = ExtremePoint(direction);
        projectionDistance = Vector3L.Dot(extremePoint, direction);
        return extremePoint;
    }
}

