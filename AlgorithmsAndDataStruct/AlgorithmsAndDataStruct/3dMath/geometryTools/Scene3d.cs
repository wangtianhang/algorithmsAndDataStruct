using System;
using System.Collections.Generic;
using System.Text;


public class Scene3d
{
    List<Model3d> m_modleList = new List<Model3d>();

    public void AddModel(Model3d model)
    {
        if(m_modleList.Contains(model))
        {
            return;
        }
        m_modleList.Add(model);
    }

    public void RemoveModel(Model3d model)
    {
        m_modleList.Remove(model);
    }

    public void UpdateModel(Model3d model)
    {

    }

    public List<Model3d> FindChildren(Model3d model)
    {
        List<Model3d> ret = new List<Model3d>();
        foreach (var iter in m_modleList)
        {
            if(iter.m_parent == model)
            {
                ret.Add(iter);
            }
        }
        return ret;
    }

    public Model3d Raycast(Ray3d ray)
    {
        Model3d result = null;
        float result_t = -1;

        for (int i = 0, size = m_modleList.Count; i < size; ++i)
        {
            float t = IntersectionTest3D.Ray3dWithModel3d(ray, m_modleList[i]);
            if (result == null && t >= 0)
            {
                result = m_modleList[i];
                result_t = t;
            }
            else if (result != null && t < result_t)
            {
                result = m_modleList[i];
                result_t = t;
            }
        }

        return result;
    }

    public List<Model3d> Query(Sphere3d sphere)
    {
        List<Model3d> result = new List<Model3d>();
        for (int i = 0, size = m_modleList.Count; i < size; ++i)
        {
            OBB3d bounds = m_modleList[i].GetOBB();
            if (IntersectionTest3D.Sphere3dWithObb3d(sphere, bounds))
            {
                result.Add(m_modleList[i]);
            }
        }
        return result;
    }

    public List<Model3d> Query(AABB3d aabb)
    {
        List<Model3d> result = new List<Model3d>();
        for (int i = 0, size = m_modleList.Count; i < size; ++i)
        {
            OBB3d bounds = m_modleList[i].GetOBB();
            if (IntersectionTest3D.AABB3dWithOBB3d(aabb, bounds))
            {
                result.Add(m_modleList[i]);
            }
        }
        return result;
    }
}

