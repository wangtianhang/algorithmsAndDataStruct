using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public struct AABB3d
{
    public AABB3d(Vector3L pos, Vector3L size)
    {
        m_pos = pos;
        m_size = size;
    }

    public Vector3L m_pos;
    public Vector3L m_size;
    //public FloatL m_xLength;
    //public FloatL m_yLength;
    //public FloatL m_zLength;

    public Vector3L GetMin()
    {
        //return new Vector3L(m_pos.x - m_size.x * 0.5f, m_pos.y - m_size.y * 0.5f, m_pos.z - m_size * 0.5f);
        return m_pos - GetHalfSize();
    }

    public Vector3L GetMax()
    {
        //return new Vector3L(m_pos.x + m_size * 0.5f, m_pos.y + m_size * 0.5f, m_pos.z + m_size * 0.5f);
        return m_pos + GetHalfSize();
    }

    public Vector3L GetHalfSize()
    {
        return m_size / 2;
    }

    public Vector3L AnyPointFast()
    {
        return GetMin();
    }

    public Vector3L ExtremePoint(Vector3L direction)
    {
        Vector3L maxPoint = GetMax();
        Vector3L minPoint = GetMin();
        return new Vector3L(
            direction.x >= 0 ? maxPoint.x : minPoint.x,
            direction.y >= 0 ? maxPoint.y : minPoint.y,
            direction.z >= 0 ? maxPoint.z : minPoint.z
            );
    }

    public Vector3L ExtremePoint(Vector3L direction, ref FloatL projectionDistance)
    {
        Vector3L extremePoint = ExtremePoint(direction);
        projectionDistance = Vector3L.Dot(extremePoint, direction);
        return extremePoint;
    }

    public AABB3d Merge(AABB3d other)
    {
        Vector3L selfMin = GetMin();
        Vector3L otherMin = other.GetMin();
        Vector3L selfMax = GetMax();
        Vector3L otherMax = GetMax();

        Vector3L totalMin = new Vector3L(FixPointMath.Min(selfMin.x, otherMin.x), FixPointMath.Min(selfMin.y, otherMin.y), FixPointMath.Min(selfMin.z, otherMin.z));
        Vector3L totalMax = new Vector3L(FixPointMath.Max(selfMax.x, otherMax.x), FixPointMath.Max(selfMax.y, otherMax.y), FixPointMath.Max(selfMax.z, otherMax.z));

        return new AABB3d((totalMin + totalMax) / 2, totalMax - totalMin);
    }

    public bool Contains(AABB3d other)
    {
        Vector3L selfMin = GetMin();
        Vector3L otherMin = other.GetMin();
        Vector3L selfMax = GetMax();
        Vector3L otherMax = GetMax();

        return otherMin.x >= selfMin.x &&
            otherMax.x <= selfMax.x &&
            otherMin.y >= selfMin.y &&
            otherMax.y <= selfMax.y &&
            otherMin.z >= selfMin.z &&
            otherMax.z <= selfMax.z;
    }

    public FloatL CalculateSurfaceArea()
    {
        return 2 * (m_size.x * m_size.y +  m_size.x * m_size.z + m_size.y * m_size.z); 
    }

    public bool Overlaps(AABB3d other)
    {
        Vector3L selfMin = GetMin();
        Vector3L otherMin = other.GetMin();
        Vector3L selfMax = GetMax();
        Vector3L otherMax = GetMax();

        // y is deliberately first in the list of checks below as it is seen as more likely than things
        // collide on x,z but not on y than they do on y thus we drop out sooner on a y fail
        return selfMax.x > otherMin.x &&
            selfMin.x < otherMax.x &&
            selfMax.y > otherMin.y &&
            selfMin.y < otherMax.y &&
            selfMax.z > otherMin.z &&
            selfMin.z < otherMax.z;
    }
}

