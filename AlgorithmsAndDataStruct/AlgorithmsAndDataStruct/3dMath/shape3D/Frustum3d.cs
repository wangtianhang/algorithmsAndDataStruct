using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;


public struct Frustum3d
{
    public Plane3d[] m_planeArray;

    public Frustum3d(Plane3d[] planeArray)
    {
        m_planeArray = planeArray;
    }
}

