using System;
using System.Collections.Generic;
using System.Text;


class OrientedRectangle2d
{
    public Vector2 m_pos;
    // 假设为xz平面的2d物体
    public Quaternion m_rotation;
    // 定义和朝向一致的方向未长，和朝向垂直的方向为宽
    public float m_width = 0;
    public float m_length = 0;
}

