using System;
using System.Collections.Generic;
using System.Text;


class OgreOctree
{
    int mNumNodes = 0;
    OgreOctree mParent = null;
    List<OgreOctreeNode> mNodes = new List<OgreOctreeNode>();

    public int numNodes()
    {
        return mNumNodes;
    }

    public void _addNode(OgreOctreeNode node)
    {
        mNodes.Add(node);

        node.setOctant(this);

        _ref();
    }

    public void _removeNode(OgreOctreeNode node)
    {
        mNodes.Remove(node);

        node.setOctant(null);

        _unref();
    }

    void _ref()
    {
        mNumNodes++;

        if (mParent != null)
        {
            mParent._ref();
        }
    }

    void _unref()
    {
        mNumNodes--;
        if(mParent != null)
        {
            mParent._unref();
        }
    }
}

