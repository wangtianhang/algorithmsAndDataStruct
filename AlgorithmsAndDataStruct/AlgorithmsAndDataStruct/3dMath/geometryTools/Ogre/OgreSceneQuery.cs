using System;
using System.Collections.Generic;
using System.Text;


/*class OgreSceneQuery
{

    public static List<Model3d> Query(AABB3d aabb, OgreSceneManager sceneManager)
    {
        List<Model3d> result = new List<Model3d>();
        List<OgreOctreeNode> nodeList = new List<OgreOctreeNode>();
        sceneManager.findNodesIn(aabb, ref nodeList);
        foreach(var node in nodeList)
        {
            foreach(var model in node.GetModelList())
            {
                if(IntersectionTest3D.Model3dWithAABB3d(model, aabb))
                {
                    result.Add(model);
                }
            }
        }
        return result;
    }

    public static List<Model3d> Query(Ray3d ray, OgreSceneManager sceneManager)
    {
        List<Model3d> result = new List<Model3d>();
        List<OgreOctreeNode> nodeList = new List<OgreOctreeNode>();
        sceneManager.findNodesIn(ray, ref nodeList);
        foreach (var node in nodeList)
        {
            foreach (var model in node.GetModelList())
            {
                float t = IntersectionTest3D.Ray3dWithModel3d(ray, model);
                if (t > 0)
                {
                    result.Add(model);
                }
            }
        }
        return result;
    }

    public static List<Model3d> Query(Sphere3d sphere, OgreSceneManager sceneManager)
    {
        List<Model3d> result = new List<Model3d>();
        List<OgreOctreeNode> nodeList = new List<OgreOctreeNode>();
        sceneManager.findNodesIn(sphere, ref nodeList);
        foreach (var node in nodeList)
        {
            foreach (var model in node.GetModelList())
            {
                if (IntersectionTest3D.Model3dWithSphere3d(model, sphere))
                {
                    result.Add(model);
                }
            }
        }
        return result;
    }

    public static List<Model3d> Query(PlaneBoundedVolume3d planeBoundVolume, OgreSceneManager sceneManager)
    {
        List<Model3d> result = new List<Model3d>();
        List<OgreOctreeNode> nodeList = new List<OgreOctreeNode>();
        sceneManager.findNodesIn(planeBoundVolume, ref nodeList);
        foreach (var node in nodeList)
        {
            foreach (var model in node.GetModelList())
            {
                OBB3d obb = model.GetOBB();
                if(IntersectionTest3D.Frustum3dWithOBB3d(planeBoundVolume.m_planeArray, obb))
                {
                    result.Add(model);
                }
            }
        }
        return result;
    }
}*/

