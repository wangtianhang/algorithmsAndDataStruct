using System;
using System.Collections.Generic;
using System.Text;


class AABB3d
{
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
}

