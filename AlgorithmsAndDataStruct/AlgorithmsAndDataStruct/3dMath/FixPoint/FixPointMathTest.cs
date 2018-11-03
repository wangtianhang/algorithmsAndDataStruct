using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

class FixPointMathTest
{
    public static void Test()
    {
        Debug.Log("IsLittleEndian " + BitConverter.IsLittleEndian);
        Debug.Log(FixPointMathTest.ConvertFloatToBinStr(1f));
        Debug.Log(FixPointMathTest.ConvertFloatToBinStr(17.625f));

        Debug.Log(FixPointMath.SqrtOld(new FloatL(0.955d)));
        Debug.Log(Mathf.Sqrt(0.955f));
        Debug.Log(FixPointMath.Sqrt(new FloatL(0.955d)));
        Vector3L dir = new Vector3L(-0.093d, -0.093d, 0.988d);
        FloatL angle1 = Vector3L_Angle(Vector3L.forward, dir);
        float angle2 = Vector3_Angle(Vector3.forward, dir.Convert());
        Debug.Log("angle1 " + angle1 + " angle2 " + angle2);

        Debug.Log("sin 10 " + Mathf.Sin(10 * Mathf.Deg2Rad));
        Debug.Log("sinL 10 " + FixPointMath.Sin(10 * Mathf.Deg2Rad));

        Debug.Log("cos 10 " + Mathf.Cos(10 * Mathf.Deg2Rad));
        Debug.Log("cosL 10 " + FixPointMath.Cos(10 * Mathf.Deg2Rad));

        Debug.Log("acos 0.1 " + Mathf.Acos(0.1f));
        Debug.Log("acosL 0.1 " + FixPointMath.Acos(0.1f));

        Debug.Log("atan2 3/2 " + Mathf.Atan2(3, 2));
        Debug.Log("atan2L 3/2 " + FixPointMath.Atan2(3, 2));

        Debug.Log("asin 0.6 " + Mathf.Asin(0.6f));
        Debug.Log("asinL 0.6 " + FixPointMath.Asin(0.6f));
    }

    public static float Vector3_Angle(Vector3 from, Vector3 to)
    {
        Debug.Log("x " + to.normalized.x + " y " + to.normalized.y + " z " + to.normalized.z);
        float dot = Vector3.Dot(from.normalized, to.normalized);
        Debug.Log("dot " + dot);
        float acos = Mathf.Acos(Mathf.Clamp(dot, -1f, 1f));
        Debug.Log("acos " + dot);
        return acos * 57.29578f;
    }

    public static FloatL Vector3L_Angle(Vector3L from, Vector3L to)
    {
        Debug.Log("x " + to.normalized.x + " y " + to.normalized.y + " z " + to.normalized.z);
        FloatL dot = Vector3L.Dot(from.normalized, to.normalized);
        Debug.Log("dotL " + dot);
        FloatL acos = FixPointMath.Acos(FixPointMath.Clamp(dot, -1f, 1f));
        Debug.Log("acosL " + acos);
        return acos * 57.29578d;
    }

    public static string ConvertFloatToBinStr(float data)
    {
        byte[] bytes = BitConverter.GetBytes(data);
        return ByteArrayToBinString(bytes);
    }

    public static string ConvertDoubleToBinStr(double data)
    {
        byte[] bytes = BitConverter.GetBytes(data);
        return ByteArrayToBinString(bytes);
    }

    public static string ByteArrayToBinString(byte[] data)
    {
        string ret = "";
        for(int i = data.Length - 1; i >= 0; --i )
        {
            byte iter = data[i];
            //ret += iter.ToString() + "";
            string oneByteBin = System.Convert.ToString(iter, 2);
            if(oneByteBin.Length < 8)
            {
                string prefix = "";
                for(int j = 0; j < 8 - oneByteBin.Length; ++j)
                {
                    prefix += "0";
                }

                oneByteBin = prefix + oneByteBin;
            }
            ret += oneByteBin + " ";
        }
        return ret.Trim();
    }
}

