using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public struct OrientedRectangle2d
{
    public Vector2L m_pos;
    // 假设为xz平面的2d物体
    public QuaternionL m_rotation;
    // 定义和朝向一致的方向未长，和朝向垂直的方向为宽
    public FloatL m_width;
    public FloatL m_length;
}

