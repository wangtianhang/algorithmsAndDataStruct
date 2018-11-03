using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public struct Triangle3d
{
    public Triangle3d(Vector3L pos0, Vector3L pos1, Vector3L pos2)
    {
        m_point0 = pos0;
        m_point1 = pos1;
        m_point2 = pos2;
    }

    public Vector3L m_point0;
    public Vector3L m_point1;
    public Vector3L m_point2;

    public Vector3L GetPoint(int index)
    {
        if(index == 0)
        {
            return m_point0;
        }
        else if(index == 1)
        {
            return m_point1;
        }
        else if(index == 2)
        {
            return m_point2;
        }
        else
        {
            throw new System.NotImplementedException();
        }
    }
}

