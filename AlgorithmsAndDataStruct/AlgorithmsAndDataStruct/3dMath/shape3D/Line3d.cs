using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public struct Line3d
{
    public Line3d(Vector3L point1, Vector3L point2)
    {
        m_point1 = point1;
        m_point2 = point2;
    }
    public Vector3L m_point1;
    public Vector3L m_point2;
}

