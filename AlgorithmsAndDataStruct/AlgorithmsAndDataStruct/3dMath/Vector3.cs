using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 坐标系与unity保持一致 为左手坐标系
/// </summary>
/// 
public struct Vector3
{
    public float x;
    public float y;
    public float z;

    public Vector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static Vector3 operator -(Vector3 a)
    {
        Vector3 ret = new Vector3();
        ret.x = -a.x;
        ret.y = -a.y;
        ret.z = -a.z;
        return ret;
    }

    public static Vector3 operator -(Vector3 a, Vector3 b)
    {
        Vector3 ret = new Vector3();
        ret.x = a.x - b.x;
        ret.y = a.y - b.y;
        ret.z = a.z - b.z;
        return ret;
    }

    public static bool operator !=(Vector3 lhs, Vector3 rhs)
    {
        return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z;
    }

    public static Vector3 operator *(float d, Vector3 a)
    {
        Vector3 ret = new Vector3();
        ret.x = a.x * d;
        ret.y = a.y * d;
        ret.z = a.z * d;
        return ret;
    }

    public static Vector3 operator *(Vector3 a, float d)
    {
        Vector3 ret = new Vector3();
        ret.x = a.x * d;
        ret.y = a.y * d;
        ret.z = a.z * d;
        return ret;
    }

    public static Vector3 operator /(Vector3 a, float d)
    {
        Vector3 ret = new Vector3();
        ret.x = a.x / d;
        ret.y = a.y / d;
        ret.z = a.z / d;
        return ret;
    }

    public static Vector3 operator +(Vector3 a, Vector3 b)
    {
        Vector3 ret = new Vector3();
        ret.x = a.x + b.x;
        ret.y = a.y + b.y;
        ret.z = a.z + b.z;
        return ret;
    }

    public static bool operator ==(Vector3 lhs, Vector3 rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
    }

    public static float Dot(Vector3 lhs, Vector3 rhs)
    {
        return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
    }

    public static Vector3 forward
    {
        get { return new Vector3(0, 0, 1); }
    }
}
