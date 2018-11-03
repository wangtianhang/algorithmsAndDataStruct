using System;
using System.Collections.Generic;
using System.Text;


class ConvertHelper
{
    public static void Test()
    {
        string tmp = TenToTwo(10);
        Debug.Log(tmp);
        Debug.Log(TwoToTen(tmp).ToString());

        string tmp2 = TenToSixteen(10);
        Debug.Log(tmp2);
        Debug.Log(SixteenToTen(tmp2).ToString());
    }

    /// <summary>
    /// 十进制转二进制
    /// </summary>
    /// <returns></returns>
    public static string TenToTwo(int x)
    {
        return System.Convert.ToString(x, 2);
    }

    /// <summary>
    /// 二进制转十进制
    /// </summary>
    /// <returns></returns>
    public static int TwoToTen(string s)
    {
        return System.Convert.ToInt32(s, 2);
    }

    /// <summary>
    /// 十进制转十六进制
    /// </summary>
    /// <returns></returns>
    public static string TenToSixteen(int x)
    {
        return string.Format("{0:X}", x);
    }

    /// <summary>
    /// 十六进制转十进制
    /// </summary>
    /// <returns></returns>
    public static int SixteenToTen(string s)
    {
        return System.Convert.ToInt32(s, 16);
    }
}

