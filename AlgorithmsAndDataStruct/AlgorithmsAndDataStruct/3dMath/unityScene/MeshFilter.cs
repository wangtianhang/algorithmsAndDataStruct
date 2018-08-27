using System;
using System.Collections.Generic;
using System.Text;


class MeshFilter : Component
{
    Mesh3d m_mesh3d = null;

    public Mesh3d sharedMesh
    {
        get
        {
            return m_mesh3d;
        }
    }
}

