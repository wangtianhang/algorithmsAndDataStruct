using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

class RotateHelper
{
    /// <summary>
    /// 从rotation获取方向
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static Vector3 GetForward(Quaternion rotation)
    {
        return rotation * Vector3.forward;
    }

    public static FixPoint.Vector3L GetForward(FixPoint.QuaternionL rotation)
    {
        return rotation * FixPoint.Vector3L.forward;
    }

    /// <summary>
    /// 从水平方向转换rotation
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static Quaternion LookAt(Vector3 dir)
    {
        return Quaternion.LookRotation(dir, Vector3.up);
    }

    public static FixPoint.QuaternionL LookAt(FixPoint.Vector3L dir)
    {
        return FixPoint.QuaternionL.LookRotation(dir, FixPoint.Vector3L.up);
    }

    /// <summary>
    /// 各个方向转rotation
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public static Quaternion DirectionToRotation(Vector3 dir)
    {
        return Quaternion.FromToRotation(Vector3.forward, dir);
    }

    public static FixPoint.QuaternionL DirectionToRotation(FixPoint.Vector3L dir)
    {
        return FixPoint.QuaternionL.FromToRotation(FixPoint.Vector3L.forward, dir);
    }

    /// <summary>
    /// 从矩阵中提取Quaternion
    /// </summary>
    /// <param name="m"></param>
    /// <returns></returns>
    public static Quaternion GetRotationFromMatrix(Matrix4x4 m)
    {
        return Quaternion.LookRotation(m.GetColumn(2), m.GetColumn(1));
    }

    public static FixPoint.QuaternionL GetRotationFromMatrix(FixPoint.Matrix4x4L m)
    {
        return FixPoint.QuaternionL.LookRotation(m.GetColumn(2), m.GetColumn(1));
    }
}

