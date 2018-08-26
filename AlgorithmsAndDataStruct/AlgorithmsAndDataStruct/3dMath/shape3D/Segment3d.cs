using System;
using System.Collections.Generic;
using System.Text;


public class Segment3d
{
    public Vector3 m_point1;
    public Vector3 m_point2;

    public float GetLength()
    {
        return (m_point2 - m_point1).magnitude;
    }

    public float GetLengthSqr()
    {
        return (m_point2 - m_point1).sqrMagnitude;
    }
}

