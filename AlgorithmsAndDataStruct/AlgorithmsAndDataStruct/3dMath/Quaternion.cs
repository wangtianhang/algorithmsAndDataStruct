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
    public float x;
    public float y;
    public float z;
    public float w;

    public static Quaternion operator *(Quaternion a, Quaternion b)
    {
        return new Quaternion();
    }

    public static Vector3 operator *(Quaternion a, Vector3 b)
    {
        return new Vector3();
    }
}

