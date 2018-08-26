using System;
using System.Collections.Generic;
using System.Text;


public class OBB3d
{
    public Vector3 m_pos;
    public Quaternion m_rotation;
    public float m_xLength;
    public float m_yLength;
    public float m_zLength;

    public Vector3 GetAABBMin()
    {
        return new Vector3(m_pos.x - m_xLength * 0.5f, m_pos.y - m_yLength * 0.5f, m_pos.z - m_zLength * 0.5f);
    }

    public Vector3 GetAABBMax()
    {
        return new Vector3(m_pos.x + m_xLength * 0.5f, m_pos.y + m_yLength * 0.5f, m_pos.z + m_zLength * 0.5f);
    }

    public Matrix4x4 GetObjToWorld()
    {
        return Matrix4x4.TRS(m_pos, m_rotation, Vector3.one);
    }
}

