using System;
using System.Collections.Generic;

using System.Text;

using System.Xml.Serialization;

/// <summary>
/// 坐标系与unity保持一致 为左手坐标系
/// </summary>
struct Quaternion
{
    public static void Test()
    {
        Quaternion rotate = Quaternion.identity;
        rotate.eulerAngles = new Vector3(40, 50, 60);
        Console.WriteLine("测试euler to Quaternion \n" + rotate);

        Vector3 euler = rotate.eulerAngles;
        Console.WriteLine("测试quaternion to euler \n" + euler);

        Vector3 dir = new Vector3(0.3f, 0.4f, 0.5f);
        dir.Normalize();
        Quaternion lookRotation = Quaternion.LookRotation(dir, Vector3.up);
        Console.WriteLine("测试look rotation \n" + lookRotation);

        Quaternion rotate2 = Quaternion.identity;
        rotate2.eulerAngles = new Vector3(70, 80, 90);

        Quaternion slerp = Quaternion.Slerp(rotate, rotate2, 0.5f);
        Console.WriteLine("测试slerp \n" + slerp);

        //Console.ReadLine();
    }

    public const float kEpsilon = 1e-006f;

    public float x;
    public float y;
    public float z;
    public float w;

    public override string ToString()
    {
        return x.ToString("f6") + ",\t" + y.ToString("f6") + ",\t" + z.ToString("f6") + ",\t" + w.ToString("f6");
    }

    public Quaternion(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }



    public static bool operator ==(Quaternion lhs, Quaternion rhs)
    {
        return Quaternion.Dot(lhs, rhs) > 0.999999f;
    }

    public static bool operator !=(Quaternion lhs, Quaternion rhs)
    {
        return Quaternion.Dot(lhs, rhs) <= 0.999999f;
    }

    public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
    {
        return new Quaternion(lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y, lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z, lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x, lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
    }

    public static Vector3 operator *(Quaternion rotation, Vector3 point)
    {
        float num = rotation.x * 2f;
        float num2 = rotation.y * 2f;
        float num3 = rotation.z * 2f;
        float num4 = rotation.x * num;
        float num5 = rotation.y * num2;
        float num6 = rotation.z * num3;
        float num7 = rotation.x * num2;
        float num8 = rotation.x * num3;
        float num9 = rotation.y * num3;
        float num10 = rotation.w * num;
        float num11 = rotation.w * num2;
        float num12 = rotation.w * num3;
        Vector3 result;
        result.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
        result.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
        result.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
        return result;
    }

    public static Quaternion identity
    {
        get
        {
            return new Quaternion(0, 0, 0, 1);
        }
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
                    throw new IndexOutOfRangeException("Invalid Quaternion index!");
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
                    throw new IndexOutOfRangeException("Invalid Quaternion index!");
            }
        }
    }

    public Vector3 eulerAngles
    {
        get
        {

            return Quaternion.ToEulerRad(this) * 57.29578f;
        }
        set
        {

            this = Quaternion.FromEulerRad(value * 0.0174532924f);
        }
    }

    public static float Dot(Quaternion a, Quaternion b)
    {
        return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
    }

    // 四元数转欧拉角
    public static Vector3 ToEulerRad(Quaternion rotation)
    {
        float sqw = rotation.w * rotation.w;
        float sqx = rotation.x * rotation.x;
        float sqy = rotation.y * rotation.y;
        float sqz = rotation.z * rotation.z;
        float unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
        float test = rotation.x * rotation.w - rotation.y * rotation.z;
        Vector3 v;

        if (test > 0.4995f * unit)
        { // singularity at north pole
            v.y = 2f * (float)Math.Atan2(rotation.y, rotation.x);
            v.x = (float)Math.PI / 2;
            v.z = 0;
            return NormalizeAngles(v * Math3d.Rad2Deg);
        }
        if (test < -0.4995f * unit)
        { // singularity at south pole
            v.y = -2f * (float)Math.Atan2(rotation.y, rotation.x);
            v.x = -(float)Math.PI / 2;
            v.z = 0;
            return NormalizeAngles(v * Math3d.Rad2Deg);
        }
        Quaternion q = new Quaternion(rotation.w, rotation.z, rotation.x, rotation.y);
        v.y = (float)System.Math.Atan2(2f * q.x * q.w + 2f * q.y * q.z, 1 - 2f * (q.z * q.z + q.w * q.w));     // Yaw
        v.x = (float)System.Math.Asin(2f * (q.x * q.z - q.w * q.y));                             // Pitch
        v.z = (float)System.Math.Atan2(2f * q.x * q.y + 2f * q.z * q.w, 1 - 2f * (q.y * q.y + q.z * q.z));      // Roll
        return NormalizeAngles(v * Math3d.Rad2Deg) * Math3d.Deg2Rad;
    }

    static Vector3 NormalizeAngles(Vector3 angles)
    {
        angles.x = NormalizeAngle(angles.x);
        angles.y = NormalizeAngle(angles.y);
        angles.z = NormalizeAngle(angles.z);
        return angles;
    }

    static float NormalizeAngle(float angle)
    {
        while (angle > 360)
            angle -= 360;
        while (angle < 0)
            angle += 360;
        return angle;
    }

    // 欧拉角转四元数
    public static Quaternion FromEulerRad(Vector3 euler)
    {
        var yaw = euler.z;
        var pitch = euler.x;
        var roll = euler.y;

        float yawOver2 = yaw * 0.5f;
        float sinYawOver2 = (float)System.Math.Sin((float)yawOver2);
        float cosYawOver2 = (float)System.Math.Cos((float)yawOver2);

        float pitchOver2 = pitch * 0.5f;
        float sinPitchOver2 = (float)System.Math.Sin((float)pitchOver2);
        float cosPitchOver2 = (float)System.Math.Cos((float)pitchOver2);

        float rollOver2 = roll * 0.5f;
        float sinRollOver2 = (float)System.Math.Sin((float)rollOver2);
        float cosRollOver2 = (float)System.Math.Cos((float)rollOver2);

        Quaternion result;
        result.w = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
        result.x = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2;
        result.y = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;
        result.z = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2;
        return result;
    }

    public static Quaternion LookRotation(Vector3 forward, Vector3 up)
    {
        forward = Vector3.Normalize(forward);
        Vector3 right = Vector3.Normalize(Vector3.Cross(up, forward));
        up = Vector3.Cross(forward, right);
        var m00 = right.x;
        var m01 = right.y;
        var m02 = right.z;
        var m10 = up.x;
        var m11 = up.y;
        var m12 = up.z;
        var m20 = forward.x;
        var m21 = forward.y;
        var m22 = forward.z;


        float num8 = (m00 + m11) + m22;
        var quaternion = new Quaternion();
        if (num8 > 0f)
        {
            var num = (float)System.Math.Sqrt(num8 + 1f);
            quaternion.w = num * 0.5f;
            num = 0.5f / num;
            quaternion.x = (m12 - m21) * num;
            quaternion.y = (m20 - m02) * num;
            quaternion.z = (m01 - m10) * num;
            return quaternion;
        }
        if ((m00 >= m11) && (m00 >= m22))
        {
            var num7 = (float)System.Math.Sqrt(((1f + m00) - m11) - m22);
            var num4 = 0.5f / num7;
            quaternion.x = 0.5f * num7;
            quaternion.y = (m01 + m10) * num4;
            quaternion.z = (m02 + m20) * num4;
            quaternion.w = (m12 - m21) * num4;
            return quaternion;
        }
        if (m11 > m22)
        {
            var num6 = (float)System.Math.Sqrt(((1f + m11) - m00) - m22);
            var num3 = 0.5f / num6;
            quaternion.x = (m10 + m01) * num3;
            quaternion.y = 0.5f * num6;
            quaternion.z = (m21 + m12) * num3;
            quaternion.w = (m20 - m02) * num3;
            return quaternion;
        }
        var num5 = (float)System.Math.Sqrt(((1f + m22) - m00) - m11);
        var num2 = 0.5f / num5;
        quaternion.x = (m20 + m02) * num2;
        quaternion.y = (m21 + m12) * num2;
        quaternion.z = 0.5f * num5;
        quaternion.w = (m01 - m10) * num2;
        return quaternion;
    }

    /// <summary>
    /// Construct a new MyQuaternion from vector and w components
    /// </summary>
    /// <param name="v">The vector part</param>
    /// <param name="w">The w part</param>
    public Quaternion(Vector3 v, float w)
    {
        this.x = v.x;
        this.y = v.y;
        this.z = v.z;
        this.w = w;
    }

    [XmlIgnore]
    public Vector3 xyz
    {
        set
        {
            x = value.x;
            y = value.y;
            z = value.z;
        }
        get
        {
            return new Vector3(x, y, z);
        }
    }

    /// <summary>
    /// Gets the square of the quaternion length (magnitude).
    /// </summary>
    [XmlIgnore]
    public float LengthSquared
    {
        get
        {
            return x * x + y * y + z * z + w * w;
        }
    }

    /// <summary>
    /// Gets the length (magnitude) of the quaternion.
    /// </summary>
    /// <seealso cref="LengthSquared"/>
    [XmlIgnore]
    public float Length
    {
        get
        {
            return (float)System.Math.Sqrt(x * x + y * y + z * z + w * w);
        }
    }

    public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
    {
        // if either input is zero, return the other.
        if (a.LengthSquared == 0.0f)
        {
            if (b.LengthSquared == 0.0f)
            {
                return identity;
            }
            return b;
        }
        else if (b.LengthSquared == 0.0f)
        {
            return a;
        }


        float cosHalfAngle = a.w * b.w + Vector3.Dot(a.xyz, b.xyz);

        if (cosHalfAngle >= 1.0f || cosHalfAngle <= -1.0f)
        {
            // angle = 0.0f, so just return one input.
            return a;
        }
        else if (cosHalfAngle < 0.0f)
        {
            b.xyz = -b.xyz;
            b.w = -b.w;
            cosHalfAngle = -cosHalfAngle;
        }

        float blendA;
        float blendB;
        if (cosHalfAngle < 0.99f)
        {
            // do proper slerp for big angles
            float halfAngle = (float)System.Math.Acos(cosHalfAngle);
            float sinHalfAngle = (float)System.Math.Sin(halfAngle);
            float oneOverSinHalfAngle = 1.0f / sinHalfAngle;
            blendA = (float)System.Math.Sin(halfAngle * (1.0f - t)) * oneOverSinHalfAngle;
            blendB = (float)System.Math.Sin(halfAngle * t) * oneOverSinHalfAngle;
        }
        else
        {
            // do lerp if angle is really small.
            blendA = 1.0f - t;
            blendB = t;
        }

        Quaternion result = new Quaternion(blendA * a.xyz + blendB * b.xyz, blendA * a.w + blendB * b.w);
        if (result.LengthSquared > 0.0f)
            return Normalize(result);
        else
            return identity;
    }

    /// <summary>
    /// Scale the given quaternion to unit length
    /// </summary>
    /// <param name="q">The quaternion to normalize</param>
    /// <returns>The normalized quaternion</returns>
    public static Quaternion Normalize(Quaternion q)
    {
        float scale = 1.0f / q.Length;
        Quaternion result = new Quaternion(q.xyz * scale, q.w * scale);
        return result;
    }
}

