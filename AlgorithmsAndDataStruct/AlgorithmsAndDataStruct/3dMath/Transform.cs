using System;
using System.Collections.Generic;

using System.Text;


/// <summary>
/// 坐标系与unity保持一致 为左手坐标系
/// </summary>

class Transform
{
    Vector3 m_position;

    //Vector3 m_localScale;

    Quaternion m_rotation;

    // 旋转转朝向
    public static Vector3 GetForward(Quaternion rotation)
    {
        return rotation * Vector3.forward;
    }

    // 朝向转旋转
    public static Quaternion LookAt(Vector3 dir)
    {
        return Quaternion.LookRotation(dir, Vector3.up);
    }
}

