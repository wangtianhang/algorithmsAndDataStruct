using System;
using System.Collections.Generic;
using System.Text;

[System.Serializable]
public struct Vector2ixy
{
    public int m_x;
    public int m_y;

    public int this[int index]
    {
        get
        {
            if (index == 0)
            {
                return this.m_x;
            }
            if (index != 1)
            {
                throw new IndexOutOfRangeException("Invalid Vector2 index!");
            }
            return this.m_y;
        }
        set
        {
            if (index != 0)
            {
                if (index != 1)
                {
                    throw new IndexOutOfRangeException("Invalid Vector2 index!");
                }
                this.m_y = value;
            }
            else
            {
                this.m_x = value;
            }
        }
    }

    public float magnitude
    {
        get
        {
            float sqr_magnitude = m_x * m_x + m_y * m_y;
            return (float)Math.Sqrt(sqr_magnitude);
        }

    }

    public float sqrMagnitude
    {
        get
        {
            return m_x * m_x + m_y * m_y;
        }
    }

    public static Vector2ixy zero
    {
        get
        {
            return new Vector2ixy(0, 0);
        }
    }

    public static Vector2ixy one
    {
        get
        {
            return new Vector2ixy(1, 1);
        }
    }

    public Vector2ixy(int x, int y)
    {
        m_x = x;
        m_y = y;
    }

    public void Set(int new_x, int new_y)
    {
        this.m_x = new_x;
        this.m_y = new_y;
    }

    //     public Vector3 ToVector3() {
    //         return new Vector3( m_x,m_y,0 );
    //     }

    public override string ToString()
    {
        return " (" + m_x + "," + m_y + ") ";
    }

    public static float Distance(Vector2ixy a, Vector2ixy b)
    {
        Vector2ixy vector = new Vector2ixy(a.m_x - b.m_x, a.m_y - b.m_y);
        return vector.magnitude;
    }

    public static float SqrMagnitude(Vector2ixy a)
    {
        return a.m_x * a.m_x + a.m_y * a.m_y;
    }

    public float SqrMagnitude()
    {
        return this.m_x * this.m_x + this.m_y * this.m_y;
    }

    public static Vector2ixy operator +(Vector2ixy a, Vector2ixy b)
    {
        Vector2ixy ret = new Vector2ixy(a.m_x + b.m_x, a.m_y + b.m_y);
        return ret;
    }

    public static Vector2ixy operator -(Vector2ixy a, Vector2ixy b)
    {
        Vector2ixy ret = new Vector2ixy(a.m_x - b.m_x, a.m_y - b.m_y);
        return ret;
    }

    public static Vector2ixy operator -(Vector2ixy a)
    {
        return new Vector2ixy(-a.m_x, -a.m_y);
    }

    public static Vector2ixy operator *(Vector2ixy a, float d)
    {
        return new Vector2ixy((int)(a.m_x * d), (int)(a.m_y * d));
    }

    public static Vector2ixy operator *(float d, Vector2ixy a)
    {
        return new Vector2ixy((int)(a.m_x * d), (int)(a.m_y * d));
    }

    public static Vector2ixy operator /(Vector2ixy a, float d)
    {
        return new Vector2ixy((int)(a.m_x / d), (int)(a.m_y / d));
    }

    public static bool operator ==(Vector2ixy lhs, Vector2ixy rhs)
    {
        if (lhs.m_x == rhs.m_x
            && lhs.m_y == rhs.m_y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator !=(Vector2ixy lhs, Vector2ixy rhs)
    {
        if (lhs.m_x != rhs.m_x
            || lhs.m_y != rhs.m_y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return this.m_x.GetHashCode() ^ this.m_y.GetHashCode() << 2;
    }

    public override bool Equals(object other)
    {
        if (!(other is Vector2ixy))
        {
            return false;
        }
        Vector2ixy vector = (Vector2ixy)other;
        return this.m_x.Equals(vector.m_x) && this.m_y.Equals(vector.m_y);
    }


}