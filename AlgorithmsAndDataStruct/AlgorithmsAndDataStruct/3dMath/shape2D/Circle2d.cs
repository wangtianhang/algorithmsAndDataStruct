using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public struct Circle2d
{
    public Circle2d(Vector2L pos, FloatL radius)
    {
        m_pos = pos;
        m_radius = radius;
    }
    public Vector2L m_pos;
    public FloatL m_radius;
}

