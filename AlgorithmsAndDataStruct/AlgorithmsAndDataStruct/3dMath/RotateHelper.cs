using System;
using System.Collections.Generic;
using System.Text;


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

    /// <summary>
    /// 从方向转换rotation
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static Quaternion LookAt(Vector3 dir)
    {
        return Quaternion.LookRotation(dir, Vector3.up);
    }
}

