using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

struct AARectangle
{
    public AARectangle(Vector2L pos, FloatL xWidth, FloatL zLength)
    {
        m_pos = pos;
        m_width = xWidth;
        m_length = zLength;
    }
    public Vector2L m_pos;
    public FloatL m_width; // 对应x轴
    public FloatL m_length; // 对应z轴
}

