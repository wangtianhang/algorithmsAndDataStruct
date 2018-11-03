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
    public static Vector3L GetForward(QuaternionL rotation)
    {
        return rotation * Vector3L.forward;
    }

    public static Vector3 GetForward(Quaternion rotation)
    {
        return rotation * Vector3.forward;
    }

    /// <summary>
    /// 从水平方向转换rotation
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static QuaternionL LookAt(Vector3L dir)
    {
        return QuaternionL.LookRotation(dir, Vector3L.up);
    }

    public static Quaternion LookAt(Vector3 dir)
    {
        return Quaternion.LookRotation(dir, Vector3.up);
    }

    /// <summary>
    /// 各个方向转rotation
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public static QuaternionL DirectionToRotation(Vector3L dir)
    {
        return QuaternionL.FromToRotation(Vector3L.forward, dir);
    }

    public static Quaternion DirectionToRotation(Vector3 dir)
    {
        return Quaternion.FromToRotation(Vector3.forward, dir);
    }

    /// <summary>
    /// 从矩阵中提取Quaternion
    /// </summary>
    /// <param name="m"></param>
    /// <returns></returns>
    public static QuaternionL GetRotationFromMatrix(Matrix4x4L m)
    {
        return QuaternionL.LookRotation(m.GetColumn(2), m.GetColumn(1));
    }

    public static Quaternion GetRotationFromMatrix(Matrix4x4 m)
    {
        return Quaternion.LookRotation(m.GetColumn(2), m.GetColumn(1));
    }
}

