//using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;

namespace FixPoint
{
    public struct QuaternionL
    {
        public static FloatL kEpsilon = new FloatL(0.001f);

        public FloatL x;
        public FloatL y;
        public FloatL z;
        public FloatL w;

        public override string ToString()
        {
            return "(" + x.ToString() + "," + y.ToString() + "," + z.ToString() + "," + w.ToString() + ")";
        }

        public QuaternionL(FloatL x, FloatL y, FloatL z, FloatL w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }


        public static bool operator ==(QuaternionL lhs, QuaternionL rhs)
        {
            return QuaternionL.Dot(lhs, rhs) > 0.999999d;
        }

        public static bool operator !=(QuaternionL lhs, QuaternionL rhs)
        {
            return QuaternionL.Dot(lhs, rhs) <= 0.999999d;
        }

        public static QuaternionL operator *(QuaternionL lhs, QuaternionL rhs)
        {
            return new QuaternionL(lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y, lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z, lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x, lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
        }

        public static Vector3L operator *(QuaternionL rotation, Vector3L point)
        {
            FloatL num = rotation.x * 2f;
            FloatL num2 = rotation.y * 2f;
            FloatL num3 = rotation.z * 2f;
            FloatL num4 = rotation.x * num;
            FloatL num5 = rotation.y * num2;
            FloatL num6 = rotation.z * num3;
            FloatL num7 = rotation.x * num2;
            FloatL num8 = rotation.x * num3;
            FloatL num9 = rotation.y * num3;
            FloatL num10 = rotation.w * num;
            FloatL num11 = rotation.w * num2;
            FloatL num12 = rotation.w * num3;
            Vector3L result;
            result.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
            result.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
            result.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
            return result;
        }

        public static QuaternionL identity
        {
            get
            {
                return new QuaternionL(0, 0, 0, 1);
            }
        }

        public FloatL this[int index]
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

        public static FloatL Angle(QuaternionL a, QuaternionL b)
        {
            FloatL f = QuaternionL.Dot(a, b);
            return FixPointMath.Acos(FixPointMath.Min(FixPointMath.Abs(f), 1f)) * 2f * 57.29578d;
        }

        public static QuaternionL AngleAxis(FloatL degress, Vector3L axis)
        {
            if (axis.sqrMagnitude == 0.0f)
                return identity;

            QuaternionL result = identity;
            FloatL radians = degress * FixPointMath.Deg2Rad;
            radians *= 0.5f;
            axis.Normalize();
            axis = axis * FixPointMath.Sin(radians);
            result.x = axis.x;
            result.y = axis.y;
            result.z = axis.z;
            result.w = FixPointMath.Cos(radians);

            return Normalize(result);
        }

        public Vector3L eulerAngles
        {
            get
            {

                return QuaternionL.ToEulerRad(this) * 57.29578d;
            }
            set
            {

                this = QuaternionL.FromEulerRad(value * 0.0174532924d);
            }
        }

        public static FloatL Dot(QuaternionL a, QuaternionL b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        public static QuaternionL Euler(Vector3L euler)
        {
            Vector3L eulerRad = euler * FixPointMath.Deg2Rad;
            return FromEulerRad(eulerRad);
        }

        public static QuaternionL Euler(FloatL x, FloatL y, FloatL z)
        {
            return Euler(new Vector3L(x, y, z));
        }

        public static QuaternionL FromToRotation(Vector3L v1, Vector3L v2)
        {
            return QuaternionL.AngleAxis(Vector3L.Angle(v1, v2), Vector3L.Cross(v1, v2));
        }

        public static QuaternionL Inverse(QuaternionL rotation)
        {
            FloatL lengthSq = rotation.LengthSquared;
            if (lengthSq != 0.0)
            {
                FloatL i = 1.0f / lengthSq;
                return new QuaternionL(rotation.xyz * -i, rotation.w * i);
            }
            return rotation;
        }

        public static QuaternionL Lerp(QuaternionL from, QuaternionL to, FloatL t)
        {
            if (t > 1) t = 1;
            if (t < 0) t = 0;
            return Slerp(from, to, t);
        }

        // 四元数转欧拉角
        static Vector3L ToEulerRad(QuaternionL rotation)
        {
            FloatL sqw = rotation.w * rotation.w;
            FloatL sqx = rotation.x * rotation.x;
            FloatL sqy = rotation.y * rotation.y;
            FloatL sqz = rotation.z * rotation.z;
            FloatL unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
            FloatL test = rotation.x * rotation.w - rotation.y * rotation.z;
            Vector3L v;

            if (test > 0.4995d * unit)
            { // singularity at north pole
                v.y = 2f * FixPointMath.Atan2(rotation.y, rotation.x);
                v.x = FixPointMath.PI / 2;
                v.z = 0;
                return NormalizeAngles(v * FixPointMath.Rad2Deg);
            }
            if (test < -0.4995d * unit)
            { // singularity at south pole
                v.y = -2f * FixPointMath.Atan2(rotation.y, rotation.x);
                v.x = -FixPointMath.PI / 2;
                v.z = 0;
                return NormalizeAngles(v * FixPointMath.Rad2Deg);
            }
            QuaternionL q = new QuaternionL(rotation.w, rotation.z, rotation.x, rotation.y);
            v.y = FixPointMath.Atan2(2f * q.x * q.w + 2f * q.y * q.z, 1 - 2f * (q.z * q.z + q.w * q.w));     // Yaw
            v.x = FixPointMath.Asin(2f * (q.x * q.z - q.w * q.y));                             // Pitch
            v.z = FixPointMath.Atan2(2f * q.x * q.y + 2f * q.z * q.w, 1 - 2f * (q.y * q.y + q.z * q.z));      // Roll
            return NormalizeAngles(v * FixPointMath.Rad2Deg) * FixPointMath.Deg2Rad;
        }

        static Vector3L NormalizeAngles(Vector3L angles)
        {
            angles.x = NormalizeAngle(angles.x);
            angles.y = NormalizeAngle(angles.y);
            angles.z = NormalizeAngle(angles.z);
            return angles;
        }

        static FloatL NormalizeAngle(FloatL angle)
        {
            while (angle > 360)
                angle -= 360;
            while (angle < 0)
                angle += 360;
            return angle;
        }

        // 欧拉角转四元数
        static QuaternionL FromEulerRad(Vector3L euler)
        {
            FloatL yaw = euler.z;
            FloatL pitch = euler.x;
            FloatL roll = euler.y;

            FloatL yawOver2 = yaw * 0.5f;
            FloatL sinYawOver2 = FixPointMath.Sin(yawOver2);
            FloatL cosYawOver2 = FixPointMath.Cos(yawOver2);

            FloatL pitchOver2 = pitch * 0.5f;
            FloatL sinPitchOver2 = FixPointMath.Sin(pitchOver2);
            FloatL cosPitchOver2 = FixPointMath.Cos(pitchOver2);

            FloatL rollOver2 = roll * 0.5f;
            FloatL sinRollOver2 = FixPointMath.Sin(rollOver2);
            FloatL cosRollOver2 = FixPointMath.Cos(rollOver2);

            QuaternionL result;
            result.w = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
            result.x = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2;
            result.y = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;
            result.z = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2;
            return result;
        }

        public static QuaternionL LookRotation(Vector3L forward)
        {
            return LookRotation(forward, Vector3L.up);
        }

        public static QuaternionL LookRotation(Vector3L forward, Vector3L up)
        {
            forward = Vector3L.Normalize(forward);
            Vector3L right = Vector3L.Normalize(Vector3L.Cross(up, forward));
            up = Vector3L.Cross(forward, right);
            FloatL m00 = right.x;
            FloatL m01 = right.y;
            FloatL m02 = right.z;
            FloatL m10 = up.x;
            FloatL m11 = up.y;
            FloatL m12 = up.z;
            FloatL m20 = forward.x;
            FloatL m21 = forward.y;
            FloatL m22 = forward.z;


            FloatL num8 = (m00 + m11) + m22;
            QuaternionL quaternion = new QuaternionL();
            if (num8 > 0f)
            {
                FloatL num = FixPointMath.Sqrt(num8 + 1f);
                quaternion.w = num * 0.5f;
                num = 0.5f / num;
                quaternion.x = (m12 - m21) * num;
                quaternion.y = (m20 - m02) * num;
                quaternion.z = (m01 - m10) * num;
                return quaternion;
            }
            if ((m00 >= m11) && (m00 >= m22))
            {
                FloatL num7 = FixPointMath.Sqrt(((1f + m00) - m11) - m22);
                FloatL num4 = 0.5f / num7;
                quaternion.x = 0.5f * num7;
                quaternion.y = (m01 + m10) * num4;
                quaternion.z = (m02 + m20) * num4;
                quaternion.w = (m12 - m21) * num4;
                return quaternion;
            }
            if (m11 > m22)
            {
                FloatL num6 = FixPointMath.Sqrt(((1f + m11) - m00) - m22);
                FloatL num3 = 0.5f / num6;
                quaternion.x = (m10 + m01) * num3;
                quaternion.y = 0.5f * num6;
                quaternion.z = (m21 + m12) * num3;
                quaternion.w = (m20 - m02) * num3;
                return quaternion;
            }
            FloatL num5 = FixPointMath.Sqrt(((1f + m22) - m00) - m11);
            FloatL num2 = 0.5f / num5;
            quaternion.x = (m20 + m02) * num2;
            quaternion.y = (m21 + m12) * num2;
            quaternion.z = 0.5f * num5;
            quaternion.w = (m01 - m10) * num2;
            return quaternion;
        }

        public static QuaternionL RotateTowards(QuaternionL from, QuaternionL to, FloatL maxDegreesDelta)
        {
            FloatL num = QuaternionL.Angle(from, to);
            if (num == 0f)
            {
                return to;
            }
            FloatL t = FixPointMath.Min(1f, maxDegreesDelta / num);
            return QuaternionL.SlerpUnclamped(from, to, t);
        }

        public void Set(FloatL new_x, FloatL new_y, FloatL new_z, FloatL new_w)
        {
            x = new_x;
            y = new_y;
            z = new_z;
            w = new_w;
        }

        public void SetFromToRotation(Vector3L fromDirection, Vector3L toDirection)
        {
            this = FromToRotation(fromDirection, toDirection);
        }

        public void SetLookRotation(Vector3L view)
        {
            this = LookRotation(view);
        }

        public void SetLookRotation(Vector3L view, Vector3L up)
        {
            this = LookRotation(view, up);
        }

        private static QuaternionL SlerpUnclamped(QuaternionL a, QuaternionL b, FloatL t)
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


            FloatL cosHalfAngle = a.w * b.w + Vector3L.Dot(a.xyz, b.xyz);

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

            FloatL blendA;
            FloatL blendB;
            if (cosHalfAngle < 0.99f)
            {
                // do proper slerp for big angles
                FloatL halfAngle = FixPointMath.Acos(cosHalfAngle);
                FloatL sinHalfAngle = FixPointMath.Sin(halfAngle);
                FloatL oneOverSinHalfAngle = 1.0f / sinHalfAngle;
                blendA = FixPointMath.Sin(halfAngle * (1.0f - t)) * oneOverSinHalfAngle;
                blendB = FixPointMath.Sin(halfAngle * t) * oneOverSinHalfAngle;
            }
            else
            {
                // do lerp if angle is really small.
                blendA = 1.0f - t;
                blendB = t;
            }

            QuaternionL result = new QuaternionL(blendA * a.xyz + blendB * b.xyz, blendA * a.w + blendB * b.w);
            if (result.LengthSquared > 0.0f)
                return Normalize(result);
            else
                return identity;
        }

        QuaternionL(Vector3L v, FloatL w)
        {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
            this.w = w;
        }

        [XmlIgnore]
        public Vector3L xyz
        {
            set
            {
                x = value.x;
                y = value.y;
                z = value.z;
            }
            get
            {
                return new Vector3L(x, y, z);
            }
        }

        [XmlIgnore]
        public FloatL LengthSquared
        {
            get
            {
                return x * x + y * y + z * z + w * w;
            }
        }

        [XmlIgnore]
        public FloatL Length
        {
            get
            {
                return FixPointMath.Sqrt(x * x + y * y + z * z + w * w);
            }
        }

        public static QuaternionL Slerp(QuaternionL a, QuaternionL b, FloatL t)
        {
            if (t > 1) t = 1;
            if (t < 0) t = 0;
            return SlerpUnclamped(a, b, t);
        }

        static QuaternionL Normalize(QuaternionL q)
        {
            FloatL scale = 1.0f / q.Length;
            QuaternionL result = new QuaternionL(q.xyz * scale, q.w * scale);
            return result;
        }

        public void ToAngleAxis(out FloatL angle, out Vector3L axis)
        {
            QuaternionL.ToAxisAngleRad(this, out axis, out angle);
            angle *= FixPointMath.Rad2Deg;
        }

        void Normalize()
        {
            FloatL scale = 1.0f / this.Length;
            xyz *= scale;
            w *= scale;
        }

        static void ToAxisAngleRad(QuaternionL q, out Vector3L axis, out FloatL angle)
        {
            if (FixPointMath.Abs(q.w) > 1.0f)
                q.Normalize();
            angle = 2.0f * FixPointMath.Acos(q.w); // angle
            FloatL den = FixPointMath.Sqrt(1.0f - q.w * q.w);
            if (den > 0.0001f)
            {
                axis = q.xyz / den;
            }
            else
            {
                // This occurs when the angle is zero. 
                // Not a problem: just set an arbitrary normalized axis.
                axis = new Vector3L(1, 0, 0);
            }
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
        }

        public override bool Equals(object other)
        {
            if (!(other is QuaternionL))
            {
                return false;
            }
            QuaternionL quaternion = (QuaternionL)other;
            return this.x.Equals(quaternion.x) && this.y.Equals(quaternion.y) && this.z.Equals(quaternion.z) && this.w.Equals(quaternion.w);
        }
    }
}
