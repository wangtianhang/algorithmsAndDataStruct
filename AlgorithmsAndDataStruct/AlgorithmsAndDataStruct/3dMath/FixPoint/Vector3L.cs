//using UnityEngine;
using System.Collections;
using System;

//namespace FixPoint
//{
    [System.Serializable]
    public struct Vector3L
    {
        public static void Test()
        {

        }

        public static FloatL kEpsilon = FloatL.Epsilon;

        public FloatL x;
        public FloatL y;
        public FloatL z;

        public Vector3L(FloatL x, FloatL y)
        {
            this.x = x;
            this.y = y;
            this.z = new FloatL(0);
        }

        public Vector3L(FloatL x, FloatL y, FloatL z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3L operator -(Vector3L a)
        {
            Vector3L ret = new Vector3L();
            ret.x = -a.x;
            ret.y = -a.y;
            ret.z = -a.z;
            return ret;
        }

        public static Vector3L operator +(Vector3L a)
        {
            return a;
        }

        public static Vector3L operator -(Vector3L a, Vector3L b)
        {
            Vector3L ret = new Vector3L();
            ret.x = a.x - b.x;
            ret.y = a.y - b.y;
            ret.z = a.z - b.z;
            return ret;
        }

        public static bool operator !=(Vector3L lhs, Vector3L rhs)
        {
            return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z;
        }


        public static Vector3L operator *(FloatL d, Vector3L a)
        {
            Vector3L ret = new Vector3L();
            ret.x = a.x* d;
            ret.y = a.y* d;
            ret.z = a.z* d;
            return ret;
        }

        public static Vector3L operator *(Vector3L a, FloatL d)
        {
            Vector3L ret = new Vector3L();
            ret.x = a.x * d;
            ret.y = a.y * d;
            ret.z = a.z * d;
            return ret;
        }

        public static Vector3L operator /(Vector3L a, FloatL d)
        {
            Vector3L ret = new Vector3L();
            ret.x = a.x / d;
            ret.y = a.y / d;
            ret.z = a.z / d;
            return ret;
        }

        public static Vector3L operator +(Vector3L a, Vector3L b)
        {
            Vector3L ret = new Vector3L();
            ret.x = a.x + b.x;
            ret.y = a.y + b.y;
            ret.z = a.z + b.z;
            return ret;
        }


        public static bool operator ==(Vector3L lhs, Vector3L rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
        }


        public static Vector3L back
        {
            get { return new Vector3L(0, 0, -1); }
        }

        public static Vector3L forward
        {
            get { return new Vector3L(0, 0, 1); }
        }

        public static Vector3L up
        {
            get { return new Vector3L(0, 1, 0); }
        }

        public static Vector3L down
        {
            get { return new Vector3L(0, -1, 0); }
        }

        public static Vector3L right
        {
            get { return new Vector3L(1, 0, 0); }
        }

        public static Vector3L left
        {
            get { return new Vector3L(-1, 0, 0); }
        }

        public static Vector3L zero
        {
            get { return new Vector3L(0, 0, 0); }
        }

        public static Vector3L one
        {
            get { return new Vector3L(1, 1, 1); }
        }

        public FloatL magnitude
        {
            get
            {
                return FixPointMath.Sqrt(sqrMagnitude);
            }
        }

        public FloatL sqrMagnitude
        {
            get
            {
                return this.x * this.x + this.y * this.y + this.z * this.z;
            }
        }

        public Vector3L normalized
        {
            get
            {
                return Vector3L.Normalize(this);
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
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index!");
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
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index!");
                }
            }
        }

        public static FloatL Angle(Vector3L from, Vector3L to)
        {
            return FixPointMath.Acos(FixPointMath.Clamp(Vector3L.Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578d;
        }

        public static Vector3L ClampMagnitude(Vector3L vector, FloatL maxLength)
        {
            if (vector.sqrMagnitude > maxLength * maxLength)
            {
                return vector.normalized * maxLength;
            }
            return vector;
        }

        public static Vector3L Cross(Vector3L lhs, Vector3L rhs)
        {
            return new Vector3L(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
        }

        public static FloatL Distance(Vector3L a, Vector3L b)
        {
            Vector3L vector = new Vector3L(a.x - b.x, a.y - b.y, a.z - b.z);
            return FixPointMath.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
        }

        public static FloatL Dot(Vector3L lhs, Vector3L rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
        }

        public static Vector3L Lerp(Vector3L from, Vector3L to, FloatL t)
        {
            t = FixPointMath.Clamp01(t);
            return new Vector3L(from.x + (to.x - from.x) * t, from.y + (to.y - from.y) * t, from.z + (to.z - from.z) * t);
        }

        public static FloatL Magnitude(Vector3L a)
        {
            return FixPointMath.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
        }

        public static Vector3L Max(Vector3L lhs, Vector3L rhs)
        {
            return new Vector3L(FixPointMath.Max(lhs.x, rhs.x), FixPointMath.Max(lhs.y, rhs.y), FixPointMath.Max(lhs.z, rhs.z));
        }

        public static Vector3L Min(Vector3L lhs, Vector3L rhs)
        {
            return new Vector3L(FixPointMath.Min(lhs.x, rhs.x), FixPointMath.Min(lhs.y, rhs.y), FixPointMath.Min(lhs.z, rhs.z));
        }

        public static Vector3L MoveTowards(Vector3L current, Vector3L target, FloatL maxDistanceDelta)
        {
            Vector3L a = target - current;
            FloatL magnitude = a.magnitude;
            if (magnitude <= maxDistanceDelta || magnitude == 0f)
            {
                return target;
            }
            return current + a / magnitude * maxDistanceDelta;
        }

        public void Normalize()
        {
            FloatL num = Vector3L.Magnitude(this);
            if (num > FloatL.Epsilon)
            {
                this /= num;
            }
            else
            {
                this = Vector3L.zero;
            }
        }

        public static Vector3L Normalize(Vector3L value)
        {
            FloatL num = Vector3L.Magnitude(value);
            if (num > FloatL.Epsilon)
            {
                return value / num;
            }
            return Vector3L.zero;
        }

        public static Vector3L Project(Vector3L vector, Vector3L onNormal)
        {
            FloatL num = Vector3L.Dot(onNormal, onNormal);
            if (num < FloatL.Epsilon)
            {
                return Vector3L.zero;
            }
            return onNormal * Vector3L.Dot(vector, onNormal) / num;
        }

        public static Vector3L ProjectOnPlane(Vector3L vector, Vector3L planeNormal)
        {
            return vector - Vector3L.Project(vector, planeNormal);
        }

        public static Vector3L Reflect(Vector3L inDirection, Vector3L inNormal)
        {
            return -2f * Vector3L.Dot(inNormal, inDirection) * inNormal + inDirection;
        }

        public void Scale(Vector3L scale)
        {
            this.x *= scale.x;
            this.y *= scale.y;
            this.z *= scale.z;
        }

        public static Vector3L Scale(Vector3L a, Vector3L b)
        {
            return new Vector3L(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public void Set(FloatL new_x, FloatL new_y, FloatL new_z)
        {
            x = new_x;
            y = new_y;
            z = new_z;
        }

        public static FloatL SqrMagnitude(Vector3L a)
        {
            return a.x * a.x + a.y * a.y + a.z * a.z;
        }

        public static Vector3L Slerp(Vector3L a, Vector3L b, FloatL t)
        {
            if (t <= 0)
            {
                return a;
            }
            else if (t >= 1)
            {
                return b;
            }

            Vector3L v = RotateTo(a, b, Vector3L.Angle(a, b) * t);

            //向量的长度，跟线性插值一样计算
            FloatL length = b.magnitude * t + a.magnitude * (1 - t);
            return v.normalized * length;
        }

        static Vector3L RotateTo(Vector3L from, Vector3L to, FloatL angle)
        {
            //如果两向量角度为0
            if (Vector3L.Angle(from, to) == 0)
            {
                return from;
            }

            //旋转轴
            Vector3L n = Vector3L.Cross(from, to);

            //旋转轴规范化
            n.Normalize();

            //旋转矩阵
            Matrix4x4L rotateMatrix = new Matrix4x4L();

            //旋转的弧度
            double radian = (angle * FixPointMath.PI / 180).ToDouble();
            FloatL cosAngle = FixPointMath.Cos(radian);
            FloatL sinAngle = FixPointMath.Sin(radian);

            //矩阵的数据
            //这里看不懂的自行科普矩阵知识
            rotateMatrix.SetRow(0, new Vector4L(n.x * n.x * (1 - cosAngle) + cosAngle, n.x * n.y * (1 - cosAngle) + n.z * sinAngle, n.x * n.z * (1 - cosAngle) - n.y * sinAngle, 0));
            rotateMatrix.SetRow(1, new Vector4L(n.x * n.y * (1 - cosAngle) - n.z * sinAngle, n.y * n.y * (1 - cosAngle) + cosAngle, n.y * n.z * (1 - cosAngle) + n.x * sinAngle, 0));
            rotateMatrix.SetRow(2, new Vector4L(n.x * n.z * (1 - cosAngle) + n.y * sinAngle, n.y * n.z * (1 - cosAngle) - n.x * sinAngle, n.z * n.z * (1 - cosAngle) + cosAngle, 0));
            rotateMatrix.SetRow(3, new Vector4L(0, 0, 0, 1));

            Vector4L v = Vector3L.ToVector4(from);
            Vector3L vector = new Vector3L();
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; j++)
                {
                    vector[i] += v[j] * rotateMatrix[j, i];
                }
            }
            return vector;
        }

        static Vector4L ToVector4(Vector3L v)
        {
            return new Vector4L(v.x, v.y, v.z, 0);
        }

        public override string ToString()
        {
            return "(" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ")";
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2;
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector3L))
            {
                return false;
            }
            Vector3L vector = (Vector3L)other;
            return this.x.Equals(vector.x) && this.y.Equals(vector.y) && this.z.Equals(vector.z);
        }

    public static implicit operator Vector3L(UnityEngine.Vector3 vec)
    {
        return new Vector3L(vec.x, vec.y, vec.z);
    }

    public UnityEngine.Vector3 Convert()
    {
        return new UnityEngine.Vector3(x.ToFloat(), y.ToFloat(), z.ToFloat());
    }

    }
//}
