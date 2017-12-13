using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 坐标系与unity保持一致 为左手坐标系
/// </summary>
struct Quaternion
{
    public float m_x;
    public float m_y;
    public float m_z;
    public float m_w;

    public Quaternion(float x, float y, float z, float w)
    {
        m_x = x;
        m_y = y;
        m_z = z;
        m_w = w;
    }

    public static Quaternion idendity
    {
        get
        {
            return new Quaternion(0, 0, 0, 1);
        }
    }

    public static Quaternion operator *(Quaternion a, Quaternion b)
    {
        return idendity;
    }

    public static Vector3 operator *(Quaternion a, Vector3 b)
    {
        return Vector3.zero;
    }
}

