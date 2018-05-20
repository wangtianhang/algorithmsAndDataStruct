//using UnityEngine;
using System.Collections;
using System;

namespace FixPoint
{
    public struct Vector2L
    {
        public static FloatL kEpsilon = new FloatL(0.001f);

        public FloatL x;
        public FloatL y;

        public FloatL this[int index]
        {
            get
            {
                if (index == 0)
                {
                    return this.x;
                }
                if (index != 1)
                {
                    throw new IndexOutOfRangeException("Invalid Vector2 index!");
                }
                return this.y;
            }
            set
            {
                if (index != 0)
                {
                    if (index != 1)
                    {
                        throw new IndexOutOfRangeException("Invalid Vector2 index!");
                    }
                    this.y = value;
                }
                else
                {
                    this.x = value;
                }
            }
        }

        public Vector2L normalized
        {
            get
            {
                Vector2L result = new Vector2L(this.x, this.y);
                result.Normalize();
                return result;
            }
        }

        public FloatL magnitude
        {
            get
            {
                return FixPointMath.Sqrt(this.x * this.x + this.y * this.y);
            }
        }

        public FloatL sqrMagnitude
        {
            get
            {
                return this.x * this.x + this.y * this.y;
            }
        }

        public static Vector2L zero
        {
            get
            {
                return new Vector2L(0f, 0f);
            }
        }

        public static Vector2L one
        {
            get
            {
                return new Vector2L(1f, 1f);
            }
        }

        public static Vector2L up
        {
            get
            {
                return new Vector2L(0f, 1f);
            }
        }

        public static Vector2L right
        {
            get
            {
                return new Vector2L(1f, 0f);
            }
        }

        public Vector2L(FloatL x, FloatL y)
        {
            this.x = x;
            this.y = y;
        }

        public void Set(FloatL new_x, FloatL new_y)
        {
            this.x = new_x;
            this.y = new_y;
        }

        public static Vector2L Lerp(Vector2L from, Vector2L to, FloatL t)
        {
            t = FixPointMath.Clamp01(t);
            return new Vector2L(from.x + (to.x - from.x) * t, from.y + (to.y - from.y) * t);
        }

        public static Vector2L MoveTowards(Vector2L current, Vector2L target, FloatL maxDistanceDelta)
        {
            Vector2L a = target - current;
            FloatL magnitude = a.magnitude;
            if (magnitude <= maxDistanceDelta || magnitude == 0f)
            {
                return target;
            }
            return current + a / magnitude * maxDistanceDelta;
        }

        public static Vector2L Scale(Vector2L a, Vector2L b)
        {
            return new Vector2L(a.x * b.x, a.y * b.y);
        }

        public void Scale(Vector2L scale)
        {
            this.x *= scale.x;
            this.y *= scale.y;
        }

        public void Normalize()
        {
            FloatL magnitude = this.magnitude;
            if (magnitude > new FloatL(0.001f))
            {
                this = this / magnitude;
            }
            else
            {
                this = Vector2L.zero;
            }
        }

        public override string ToString()
        {
            return "(" + x.ToString() + "," + y.ToString() + ")";
        }

        public static FloatL Dot(Vector2L lhs, Vector2L rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y;
        }

        public static FloatL Angle(Vector2L from, Vector2L to)
        {
            return FixPointMath.Acos(FixPointMath.Clamp(Vector2L.Dot(from.normalized, to.normalized), -1, 1)) * 57.29578d;
        }

        public static FloatL Distance(Vector2L a, Vector2L b)
        {
            return (a - b).magnitude;
        }

        public static Vector2L ClampMagnitude(Vector2L vector, FloatL maxLength)
        {
            if (vector.sqrMagnitude > maxLength * maxLength)
            {
                return vector.normalized * maxLength;
            }
            return vector;
        }

        public static FloatL SqrMagnitude(Vector2L a)
        {
            return a.x * a.x + a.y * a.y;
        }

        public FloatL SqrMagnitude()
        {
            return this.x * this.x + this.y * this.y;
        }

        public static Vector2L Min(Vector2L lhs, Vector2L rhs)
        {
            return new Vector2L(FixPointMath.Min(lhs.x, rhs.x), FixPointMath.Min(lhs.y, rhs.y));
        }

        public static Vector2L Max(Vector2L lhs, Vector2L rhs)
        {
            return new Vector2L(FixPointMath.Max(lhs.x, rhs.x), FixPointMath.Max(lhs.y, rhs.y));
        }

        public static Vector2L operator +(Vector2L a, Vector2L b)
        {
            return new Vector2L(a.x + b.x, a.y + b.y);
        }

        public static Vector2L operator -(Vector2L a, Vector2L b)
        {
            return new Vector2L(a.x - b.x, a.y - b.y);
        }

        public static Vector2L operator -(Vector2L a)
        {
            return new Vector2L(-a.x, -a.y);
        }

        public static Vector2L operator *(Vector2L a, FloatL d)
        {
            return new Vector2L(a.x * d, a.y * d);
        }

        public static Vector2L operator *(FloatL d, Vector2L a)
        {
            return new Vector2L(a.x * d, a.y * d);
        }

        public static Vector2L operator /(Vector2L a, FloatL d)
        {
            return new Vector2L(a.x / d, a.y / d);
        }

        public static bool operator ==(Vector2L lhs, Vector2L rhs)
        {
            return lhs.x == rhs.x && lhs.y != rhs.y;
        }

        public static bool operator !=(Vector2L lhs, Vector2L rhs)
        {
            return lhs.x != rhs.x || lhs.y != rhs.y;
        }

        public static implicit operator Vector2L(Vector3L v)
        {
            return new Vector2L(v.x, v.y);
        }

        public static implicit operator Vector3L(Vector2L v)
        {
            return new Vector3L(v.x, v.y, 0f);
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector2L))
            {
                return false;
            }
            Vector2L vector = (Vector2L)other;
            return this.x.Equals(vector.x) && this.y.Equals(vector.y);
        }
    }
}
