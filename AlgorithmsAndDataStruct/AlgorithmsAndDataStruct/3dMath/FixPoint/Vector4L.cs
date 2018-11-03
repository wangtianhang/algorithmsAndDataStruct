//using UnityEngine;
using System.Collections;
using System;

//namespace FixPoint
//{
[System.Serializable]
public struct Vector4L
    {
        public static FloatL kEpsilon = FloatL.Epsilon;

        public FloatL x;

        public FloatL y;

        public FloatL z;

        public FloatL w;

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

        public Vector4L normalized
        {
            get
            {
                return Vector4L.Normalize(this);
            }
        }

        public FloatL magnitude
        {
            get
            {
                return FixPointMath.Sqrt(Vector4L.Dot(this, this));
            }
        }

        public FloatL sqrMagnitude
        {
            get
            {
                return Vector4L.Dot(this, this);
            }
        }

        public static Vector4L zero
        {
            get
            {
                return new Vector4L(0f, 0f, 0f, 0f);
            }
        }

        public static Vector4L one
        {
            get
            {
                return new Vector4L(1f, 1f, 1f, 1f);
            }
        }

        public Vector4L(FloatL x, FloatL y, FloatL z, FloatL w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector4L(FloatL x, FloatL y, FloatL z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = 0f;
        }

        public Vector4L(FloatL x, FloatL y)
        {
            this.x = x;
            this.y = y;
            this.z = 0f;
            this.w = 0f;
        }

        public void Set(FloatL new_x, FloatL new_y, FloatL new_z, FloatL new_w)
        {
            this.x = new_x;
            this.y = new_y;
            this.z = new_z;
            this.w = new_w;
        }

        public static Vector4L Lerp(Vector4L from, Vector4L to, FloatL t)
        {
            t = FixPointMath.Clamp01(t);
            return new Vector4L(from.x + (to.x - from.x) * t, from.y + (to.y - from.y) * t, from.z + (to.z - from.z) * t, from.w + (to.w - from.w) * t);
        }

        public static Vector4L MoveTowards(Vector4L current, Vector4L target, FloatL maxDistanceDelta)
        {
            Vector4L a = target - current;
            FloatL magnitude = a.magnitude;
            if (magnitude <= maxDistanceDelta || magnitude == 0f)
            {
                return target;
            }
            return current + a / magnitude * maxDistanceDelta;
        }

        public static Vector4L Scale(Vector4L a, Vector4L b)
        {
            return new Vector4L(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        }

        public void Scale(Vector4L scale)
        {
            this.x *= scale.x;
            this.y *= scale.y;
            this.z *= scale.z;
            this.w *= scale.w;
        }

        public static Vector4L Normalize(Vector4L a)
        {
            FloatL num = Vector4L.Magnitude(a);
            if (num > FloatL.Epsilon)
            {
                return a / num;
            }
            return Vector4L.zero;
        }

        public void Normalize()
        {
            FloatL num = Vector4L.Magnitude(this);
            if (num > 1E-05f)
            {
                this /= num;
            }
            else
            {
                this = Vector4L.zero;
            }
        }

        public override string ToString()
        {
            return "(" + x.ToString() + "," + y.ToString() + "," + z.ToString() + "," + w.ToString() + ")";
        }

        public static FloatL Dot(Vector4L a, Vector4L b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        public static Vector4L Project(Vector4L a, Vector4L b)
        {
            return b * Vector4L.Dot(a, b) / Vector4L.Dot(b, b);
        }

        public static FloatL Distance(Vector4L a, Vector4L b)
        {
            return Vector4L.Magnitude(a - b);
        }

        public static FloatL Magnitude(Vector4L a)
        {
            return FixPointMath.Sqrt(Vector4L.Dot(a, a));
        }

        public static FloatL SqrMagnitude(Vector4L a)
        {
            return Vector4L.Dot(a, a);
        }

        public FloatL SqrMagnitude()
        {
            return Vector4L.Dot(this, this);
        }

        public static Vector4L Min(Vector4L lhs, Vector4L rhs)
        {
            return new Vector4L(FixPointMath.Min(lhs.x, rhs.x), FixPointMath.Min(lhs.y, rhs.y), FixPointMath.Min(lhs.z, rhs.z), FixPointMath.Min(lhs.w, rhs.w));
        }

        public static Vector4L Max(Vector4L lhs, Vector4L rhs)
        {
            return new Vector4L(FixPointMath.Max(lhs.x, rhs.x), FixPointMath.Max(lhs.y, rhs.y), FixPointMath.Max(lhs.z, rhs.z), FixPointMath.Max(lhs.w, rhs.w));
        }

        public static Vector4L operator +(Vector4L a, Vector4L b)
        {
            return new Vector4L(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        public static Vector4L operator -(Vector4L a, Vector4L b)
        {
            return new Vector4L(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }

        public static Vector4L operator -(Vector4L a)
        {
            return new Vector4L(-a.x, -a.y, -a.z, -a.w);
        }

        public static Vector4L operator *(Vector4L a, FloatL d)
        {
            return new Vector4L(a.x * d, a.y * d, a.z * d, a.w * d);
        }

        public static Vector4L operator *(FloatL d, Vector4L a)
        {
            return new Vector4L(a.x * d, a.y * d, a.z * d, a.w * d);
        }

        public static Vector4L operator /(Vector4L a, FloatL d)
        {
            return new Vector4L(a.x / d, a.y / d, a.z / d, a.w / d);
        }

        public static bool operator ==(Vector4L lhs, Vector4L rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z && lhs.w == rhs.w;
        }

        public static bool operator !=(Vector4L lhs, Vector4L rhs)
        {
            return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z || lhs.z != rhs.z;
        }

        public static implicit operator Vector4L(Vector3L v)
        {
            return new Vector4L(v.x, v.y, v.z, 0f);
        }

        public static implicit operator Vector3L(Vector4L v)
        {
            return new Vector3L(v.x, v.y, v.z);
        }

        public static implicit operator Vector4L(Vector2L v)
        {
            return new Vector4L(v.x, v.y, 0f, 0f);
        }

        public static implicit operator Vector2L(Vector4L v)
        {
            return new Vector2L(v.x, v.y);
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector4L))
            {
                return false;
            }
            Vector4L vector = (Vector4L)other;
            return this.x.Equals(vector.x) && this.y.Equals(vector.y) && this.z.Equals(vector.z) && this.w.Equals(vector.w);
        }
    }
//}
