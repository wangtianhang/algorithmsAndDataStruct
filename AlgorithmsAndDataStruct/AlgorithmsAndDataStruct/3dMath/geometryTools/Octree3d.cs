using System;
using System.Collections.Generic;
using System.Text;

public class OctreeNode
{
    public AABB3d m_bounds;
    public OctreeNode[] m_children;
    public List<Model3d> m_models = new List<Model3d>();
}

public class Octree3d
{
    public static void SplitTree(OctreeNode node, int depth)
    {
        if (depth-- <= 0)
        { // Decrements depth
            return;
        }

        if (node.m_children == null)
        {
            node.m_children = new OctreeNode[8];

            Vector3 c = node.m_bounds.m_pos;
            Vector3 e = node.m_bounds.GetHalfSize() * 0.5f;

            node.m_children[0].m_bounds = new AABB3d(c + new Vector3(-e.x, +e.y, -e.z), node.m_bounds.GetHalfSize());
            node.m_children[1].m_bounds = new AABB3d(c + new Vector3(+e.x, +e.y, -e.z), node.m_bounds.GetHalfSize());
            node.m_children[2].m_bounds = new AABB3d(c + new Vector3(-e.x, +e.y, +e.z), node.m_bounds.GetHalfSize());
            node.m_children[3].m_bounds = new AABB3d(c + new Vector3(+e.x, +e.y, +e.z), node.m_bounds.GetHalfSize());
            node.m_children[4].m_bounds = new AABB3d(c + new Vector3(-e.x, -e.y, -e.z), node.m_bounds.GetHalfSize());
            node.m_children[5].m_bounds = new AABB3d(c + new Vector3(+e.x, -e.y, -e.z), node.m_bounds.GetHalfSize());
            node.m_children[6].m_bounds = new AABB3d(c + new Vector3(-e.x, -e.y, +e.z), node.m_bounds.GetHalfSize());
            node.m_children[7].m_bounds = new AABB3d(c + new Vector3(+e.x, -e.y, +e.z), node.m_bounds.GetHalfSize());
        }

        if (node.m_children != null && node.m_models.Count > 0)
        {
            for (int i = 0; i < 8; ++i)
            { // For each child
                for (int j = 0, size = node.m_models.Count; j < size; ++j)
                {
                    OBB3d obb = node.m_models[j].GetOBB();
                    if (IntersectionTest3D.AABB3dWithOBB3d(node.m_children[i].m_bounds, obb))
                    {
                        node.m_children[i].m_models.Add(node.m_models[j]);
                    }
                }
            }
            node.m_models.Clear();

            // Recurse
            for (int i = 0; i < 8; ++i)
            {
                SplitTree(node.m_children[i], depth);
            }
        }
    }
}

