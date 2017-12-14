using System;
using System.Collections.Generic;
using System.Text;


struct Vector4
{
    public const float kEpsilon = 1E-05f;

    public float x;

    public float y;

    public float z;

    public float w;

//     public Vector4()
//     {
//         x = 0;
//         y = 0;
//         z = 0;
//         w = 0;
//     }

    public Vector4(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public float this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                    return this.x;
                case 1:
                    return this.y;
                case 2:
                    return this.z;
                case 3:
                    return this.w;
                default:
                    throw new IndexOutOfRangeException("Invalid Vector4 index!");
            }
        }
        set
        {
            switch (index)
            {
                case 0:
                    this.x = value;
                    break;
                case 1:
                    this.y = value;
                    break;
                case 2:
                    this.z = value;
                    break;
                case 3:
                    this.w = value;
                    break;
                default:
                    throw new IndexOutOfRangeException("Invalid Vector4 index!");
            }
        }
    }

    public static float Dot(Vector4 a, Vector4 b)
    {
        return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
    }

    public static float SqrMagnitude(Vector4 a)
    {
        return Vector4.Dot(a, a);
    }

    public static bool operator ==(Vector4 lhs, Vector4 rhs)
    {
        return Vector4.SqrMagnitude(lhs - rhs) < 9.99999944E-11f;
    }

    public static bool operator !=(Vector4 lhs, Vector4 rhs)
    {
        return Vector4.SqrMagnitude(lhs - rhs) >= 9.99999944E-11f;
    }

    public static Vector4 operator -(Vector4 a, Vector4 b)
    {
        return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    }
}

