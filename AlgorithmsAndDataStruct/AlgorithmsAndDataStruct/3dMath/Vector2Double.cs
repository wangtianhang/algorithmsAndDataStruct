using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public struct Vector2Double : IComparable<Vector2Double>
{
    public const double kEpsilon = 1E-10f;

    public double x;

    public double y;

    public double this[int index]
    {
        get
        {
            if (index == 0)
            {
                return this.x;
            }
            if (index != 1)
            {
                throw new IndexOutOfRangeException("Invalid Vector2Double index!");
            }
            return this.y;
        }
        set
        {
            if (index != 0)
            {
                if (index != 1)
                {
                    throw new IndexOutOfRangeException("Invalid Vector2Double index!");
                }
                this.y = value;
            }
            else
            {
                this.x = value;
            }
        }
    }

    public Vector2Double normalized
    {
        get
        {
            Vector2Double result = new Vector2Double(this.x, this.y);
            result.Normalize();
            return result;
        }
    }

    public double magnitude
    {
        get
        {
            return Math.Sqrt(this.x * this.x + this.y * this.y);
        }
    }

    public double sqrMagnitude
    {
        get
        {
            return this.x * this.x + this.y * this.y;
        }
    }

    public static Vector2Double zero
    {
        get
        {
            return new Vector2Double(0f, 0f);
        }
    }

    public static Vector2Double one
    {
        get
        {
            return new Vector2Double(1f, 1f);
        }
    }

    public static Vector2Double up
    {
        get
        {
            return new Vector2Double(0f, 1f);
        }
    }

    public static Vector2Double right
    {
        get
        {
            return new Vector2Double(1f, 0f);
        }
    }

    public Vector2Double(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public void Set(double new_x, double new_y)
    {
        this.x = new_x;
        this.y = new_y;
    }

    public static Vector2Double Lerp(Vector2Double from, Vector2Double to, double t)
    {
        t = Mathf.Clamp01(t);
        return new Vector2Double(from.x + (to.x - from.x) * t, from.y + (to.y - from.y) * t);
    }

    public static Vector2Double MoveTowards(Vector2Double current, Vector2Double target, double maxDistanceDelta)
    {
        Vector2Double a = target - current;
        double magnitude = a.magnitude;
        if (magnitude <= maxDistanceDelta || magnitude == 0f)
        {
            return target;
        }
        return current + a / magnitude * maxDistanceDelta;
    }

    public static Vector2Double Scale(Vector2Double a, Vector2Double b)
    {
        return new Vector2Double(a.x * b.x, a.y * b.y);
    }

    public void Scale(Vector2Double scale)
    {
        this.x *= scale.x;
        this.y *= scale.y;
    }

    public void Normalize()
    {
        double magnitude = this.magnitude;
        if (magnitude > 1E-05f)
        {
            this /= magnitude;
        }
        else
        {
            this = Vector2Double.zero;
        }
    }

    public override string ToString()
    {
        return string.Format("({0:F1}, {1:F1})", new object[]
	{
		this.x,
		this.y
	});
    }

    public string ToString(string format)
    {
        return string.Format("({0}, {1})", new object[]
	{
		this.x.ToString(format),
		this.y.ToString(format)
	});
    }

    public override int GetHashCode()
    {
        return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
    }

    public override bool Equals(object other)
    {
        if (!(other is Vector2Double))
        {
            return false;
        }
        Vector2Double vector = (Vector2Double)other;
        return this.x.Equals(vector.x) && this.y.Equals(vector.y);
    }

    public static double Dot(Vector2Double lhs, Vector2Double rhs)
    {
        return lhs.x * rhs.x + lhs.y * rhs.y;
    }

    public static double Angle(Vector2Double from, Vector2Double to)
    {
        return Math.Acos(Mathf.Clamp(Vector2Double.Dot(from.normalized, to.normalized), -1d, 1d)) * 57.29578f;
    }

    public static double Distance(Vector2Double a, Vector2Double b)
    {
        return (a - b).magnitude;
    }

    public static Vector2Double ClampMagnitude(Vector2Double vector, double maxLength)
    {
        if (vector.sqrMagnitude > maxLength * maxLength)
        {
            return vector.normalized * maxLength;
        }
        return vector;
    }

    public static double SqrMagnitude(Vector2Double a)
    {
        return a.x * a.x + a.y * a.y;
    }

    public double SqrMagnitude()
    {
        return this.x * this.x + this.y * this.y;
    }

    public static Vector2Double Min(Vector2Double lhs, Vector2Double rhs)
    {
        return new Vector2Double(Math.Min(lhs.x, rhs.x), Math.Min(lhs.y, rhs.y));
    }

    public static Vector2Double Max(Vector2Double lhs, Vector2Double rhs)
    {
        return new Vector2Double(Math.Max(lhs.x, rhs.x), Math.Max(lhs.y, rhs.y));
    }

    public static Vector2Double SmoothDamp(Vector2Double current, Vector2Double target, ref Vector2Double currentVelocity, double smoothTime, double maxSpeed, double deltaTime)
    {
        smoothTime = Math.Max(0.0001f, smoothTime);
        double num = 2f / smoothTime;
        double num2 = num * deltaTime;
        double d = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
        Vector2Double vector = current - target;
        Vector2Double Vector2Double = target;
        double maxLength = maxSpeed * smoothTime;
        vector = Vector2Double.ClampMagnitude(vector, maxLength);
        target = current - vector;
        Vector2Double vector3 = (currentVelocity + num * vector) * deltaTime;
        currentVelocity = (currentVelocity - num * vector3) * d;
        Vector2Double vector4 = target + (vector + vector3) * d;
        if (Vector2Double.Dot(Vector2Double - current, vector4 - Vector2Double) > 0f)
        {
            vector4 = Vector2Double;
            currentVelocity = (vector4 - Vector2Double) / deltaTime;
        }
        return vector4;
    }

    public static Vector2Double operator +(Vector2Double a, Vector2Double b)
    {
        return new Vector2Double(a.x + b.x, a.y + b.y);
    }

    public static Vector2Double operator -(Vector2Double a, Vector2Double b)
    {
        return new Vector2Double(a.x - b.x, a.y - b.y);
    }

    public static Vector2Double operator -(Vector2Double a)
    {
        return new Vector2Double(-a.x, -a.y);
    }

    public static Vector2Double operator *(Vector2Double a, double d)
    {
        return new Vector2Double(a.x * d, a.y * d);
    }

    public static Vector2Double operator *(double d, Vector2Double a)
    {
        return new Vector2Double(a.x * d, a.y * d);
    }

    public static Vector2Double operator /(Vector2Double a, double d)
    {
        return new Vector2Double(a.x / d, a.y / d);
    }

    public static bool operator ==(Vector2Double lhs, Vector2Double rhs)
    {
        return Vector2Double.SqrMagnitude(lhs - rhs) < 9.99999944E-11f;
    }

    public static bool operator !=(Vector2Double lhs, Vector2Double rhs)
    {
        return Vector2Double.SqrMagnitude(lhs - rhs) >= 9.99999944E-11f;
    }

    public static implicit operator Vector2Double(Vector3 v)
    {
        return new Vector2Double(v.x, v.y);
    }

    public int CompareTo(Vector2Double other)
    {
        if (x > other.x)
        {
            return 1;
        }
        else if (x < other.x)
        {
            return -1;
        }
        else
        {
            if (y > other.y)
            {
                return 1;
            }
            else if (y < other.y)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}


