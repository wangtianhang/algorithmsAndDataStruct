using System;
using System.Collections.Generic;
using System.Text;


class OgreOctreeNode
{
    OgreOctree mOctant;

    List<Model3d> m_modelList = new List<Model3d>();

    public void setOctant(OgreOctree o)
    {
        mOctant = o;
    }

    public List<Model3d> GetModelList()
    {
        return m_modelList;
    }


}


