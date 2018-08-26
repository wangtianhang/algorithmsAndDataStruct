using System;
using System.Collections.Generic;
using System.Text;


public class Ray3d
{
    public Ray3d(Vector3 origin, Vector3 dir)
    {
        m_rayOrigin = origin;
        m_rayDir = dir;
    }
    public Vector3 m_rayOrigin;
    public Vector3 m_rayDir;
}

