using System;
using System.Collections.Generic;
using System.Text;


public class Sphere3d
{
    public Sphere3d(Vector3 pos, float radius)
    {
        m_pos = pos;
        m_radius = radius;
    }
    public Vector3 m_pos;
    public float m_radius;
}

