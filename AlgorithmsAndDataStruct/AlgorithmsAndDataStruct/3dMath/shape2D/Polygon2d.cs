using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Polygon2d
{
    public Vector2L m_pos = Vector2L.zero;
    // 假设为xz平面的2d物体
    public QuaternionL m_rotation = QuaternionL.identity;

    /// <summary>
    /// 需要最后一个点和第一个点不重合
    /// </summary>
    public List<Vector2L> m_pointList = new List<Vector2L>();

    public List<Vector2L> GetWorldPosList()
    {
        List<Vector2L> ret = new List<Vector2L>();
        Vector3L pos3d = new Vector3L(m_pos.x, 0, m_pos.y);
        Matrix4x4L trs = Matrix4x4L.TRS(pos3d, m_rotation, Vector3L.one);
        foreach(var iter in m_pointList)
        {
            Vector3L objPoint3d = new Vector3L(iter.x, 0, iter.y);
            Vector3L worldPoint3d = trs * objPoint3d;
            ret.Add(new Vector2L(worldPoint3d.x, worldPoint3d.z));
        }
        return ret;
    }

    public List<Vector2L> GetRotateList()
    {
        List<Vector2L> ret = new List<Vector2L>();
        Matrix4x4L trs = Matrix4x4L.TRS(Vector3L.zero, m_rotation, Vector3L.one);
        foreach (var iter in m_pointList)
        {
            Vector3L objPoint3d = new Vector3L(iter.x, 0, iter.y);
            Vector3L worldPoint3d = trs * objPoint3d;
            ret.Add(new Vector2L(worldPoint3d.x, worldPoint3d.z));
        }
        return ret;
    }
}

