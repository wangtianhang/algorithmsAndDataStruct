using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

class Tween
{
    public enum Method
    {
        Linear,
        EaseIn, // 慢速开始
        EaseOut, // 慢速结束
        EaseInOut,
    }

    public static float Sample(float factor, Method method)
    {
        float val = Mathf.Clamp01(factor);

        if (method == Method.EaseIn)
        {
            val = 1f - Mathf.Sin(0.5f * Mathf.PI * (1f - val));
        }
        else if (method == Method.EaseOut)
        {
            val = Mathf.Sin(0.5f * Mathf.PI * val);
        }
        else if (method == Method.EaseInOut)
        {
            const float pi2 = Mathf.PI * 2f;
            val = val - Mathf.Sin(val * pi2) / pi2;
        }

        return val;
    }

    public static Vector3 CurvePos(float factor, Method method, Vector3 srcPos, Vector3 desPos)
    {
        float factorNew = Sample(factor, method);
        Vector3 curPos = srcPos * (1f - factorNew) + desPos * factorNew;
        return curPos;
    }

    public static Quaternion CurveQua(float factor, Method method, Quaternion srcQua, Quaternion desQua)
    {
        float factorNew = Sample(factor, method);
        return Quaternion.Slerp(srcQua, desQua, factorNew);
    }
}

