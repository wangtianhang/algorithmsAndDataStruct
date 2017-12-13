using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 坐标系与unity保持一致 为左手坐标系
/// </summary>

class Transform
{
    Vector3 m_position;

    //Vector3 m_localScale;

    Quaternion m_rotation;

    public static Vector3 GetForward(Quaternion rotation)
    {
        return rotation * Vector3.forward;
    }
}

