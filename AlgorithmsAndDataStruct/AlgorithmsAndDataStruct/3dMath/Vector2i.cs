using System;
using System.Collections.Generic;
using System.Text;


public struct Vector2i
{
    public Vector2i(int x, int z)
    {
        m_x = x;
        m_z = z;
    }

    public int m_x;
    public int m_z;

    public float magnitude()
    {
        float sqr_magnitude = m_x * m_x + m_z * m_z;
        return Mathf.Sqrt(sqr_magnitude);
    }

    public float sqrMagnitude()
    {
        return m_x * m_x + m_z * m_z;
    }

    public static Vector2i operator -(Vector2i a, Vector2i b)
    {
        Vector2i ret = new Vector2i(a.m_x - b.m_x, a.m_z - b.m_z);
        return ret;
    }

    public static Vector2i operator +(Vector2i a, Vector2i b)
    {
        Vector2i ret = new Vector2i(a.m_x + b.m_x, a.m_z + b.m_z);
        return ret;
    }

    public static bool operator ==(Vector2i lhs, Vector2i rhs)
    {
        if (lhs.m_x == rhs.m_x
            && lhs.m_z == rhs.m_z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator !=(Vector2i lhs, Vector2i rhs)
    {
        if (lhs.m_x != rhs.m_x
            || lhs.m_z != rhs.m_z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

