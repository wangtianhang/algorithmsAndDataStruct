using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public struct Sphere3d
{
    public Sphere3d(Vector3L pos, FloatL radius)
    {
        m_pos = pos;
        m_radius = radius;
    }
    public Vector3L m_pos;
    public FloatL m_radius;
}

