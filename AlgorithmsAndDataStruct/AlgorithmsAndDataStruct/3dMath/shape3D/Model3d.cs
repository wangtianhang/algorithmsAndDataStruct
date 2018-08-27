using System;
using System.Collections.Generic;
using System.Text;


public class Model3d
{
    public Vector3 m_pos;
    public Quaternion m_rotation = Quaternion.identity;

    Mesh3d m_mesh3d;

    public Mesh3d GetMesh()
    {
        return m_mesh3d;
    }

    public Matrix4x4 GetObj2WorldMatrix()
    {
        return Matrix4x4.TRS(m_pos, m_rotation, Vector3.one);
    }
}

