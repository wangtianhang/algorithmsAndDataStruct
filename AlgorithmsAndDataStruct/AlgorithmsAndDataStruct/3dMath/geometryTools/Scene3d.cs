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
}

