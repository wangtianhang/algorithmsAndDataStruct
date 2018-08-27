﻿using System;
using System.Collections.Generic;
using System.Text;


public class Model3d
{
    public Vector3 m_pos;
    public Quaternion m_rotation = Quaternion.identity;

    public Model3d m_parent = null;

    Mesh3d m_mesh3d;
    AABB3d m_aabb;

    public void SetContent(Mesh3d mesh)
    {
        m_mesh3d = mesh;

        Vector3[] vertices = mesh.GetVertices();
        Vector3 min = vertices[0];
        Vector3 max = vertices[0];

        for (int i = 1; i < mesh.m_triangleList.Count * 3; ++i)
        {
            min.x = Mathf.Min(vertices[i].x, min.x);
            min.y = Mathf.Min(vertices[i].y, min.y);
            min.z = Mathf.Min(vertices[i].z, min.z);

            max.x = Mathf.Max(vertices[i].x, max.x);
            max.y = Mathf.Max(vertices[i].y, max.y);
            max.z = Mathf.Max(vertices[i].z, max.z);
        }
        m_aabb = Mesh3d.FromMinMax(min, max);
    }

    /// <summary>
    /// 无位移 无旋转 原始aabb
    /// </summary>
    /// <returns></returns>
    public AABB3d GetOriginAABB()
    {
        return m_aabb;
    }

    public OBB3d GetOBB()
    {
        Matrix4x4 obj2World = GetObj2WorldMatrix();
        OBB3d obb = new OBB3d(obj2World * m_aabb.m_pos, m_rotation, m_aabb.m_xLength, m_aabb.m_yLength, m_aabb.m_zLength);
        return obb;
    }

    public Mesh3d GetMesh()
    {
        return m_mesh3d;
    }

    public Matrix4x4 GetObj2WorldMatrix()
    {
        return Matrix4x4.TRS(m_pos, m_rotation, Vector3.one);
    }
}

