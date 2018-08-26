using System;
using System.Collections.Generic;
using System.Text;


public class AABB3d
{
    public AABB3d(Vector3 pos, Vector3 size)
    {
        m_pos = pos;
        m_xLength = size.x;
        m_yLength = size.y;
        m_zLength = size.z;
    }

    public Vector3 m_pos;
    public float m_xLength;
    public float m_yLength;
    public float m_zLength;

    public Vector3 GetMin()
    {
        return new Vector3(m_pos.x - m_xLength * 0.5f, m_pos.y - m_yLength * 0.5f, m_pos.z - m_zLength * 0.5f);
    }

    public Vector3 GetMax()
    {
        return new Vector3(m_pos.x + m_xLength * 0.5f, m_pos.y + m_yLength * 0.5f, m_pos.z + m_zLength * 0.5f);
    }

    public Vector3 GetHalfSize()
    {
        return new Vector3(m_xLength / 2, m_yLength / 2, m_zLength / 2);
    }
}

