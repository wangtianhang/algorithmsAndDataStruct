using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public struct Ray3d
{
    public Ray3d(Vector3L origin, Vector3L dir)
    {
        m_rayOrigin = origin;
        m_rayDir = dir;
        m_rayDir.Normalize();
    }
    public Vector3L m_rayOrigin;
    public Vector3L m_rayDir;
}

