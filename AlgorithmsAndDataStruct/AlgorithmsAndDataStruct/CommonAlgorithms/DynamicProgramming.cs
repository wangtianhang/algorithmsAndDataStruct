using System;
using System.Collections.Generic;
using System.Text;


public class DynamicProgramming
{

    public static void Test()
    {
        int[] p = new int[11];
        p[0] = 0;
        p[1] = 1;
        p[2] = 5;
        p[3] = 8;
        p[4] = 9;
        p[5] = 10;
        p[6] = 17;
        p[7] = 19;
        p[8] = 20;
        p[9] = 24;
        p[10] = 30;
        Debug.Log("CUT_ROD " + CUT_ROD_Recursion(p, 10).ToString());
    }

    /// <summary>
    /// 算法导论切钢条问题
    /// p为价格表
    /// n为长度
    /// </summary>
    /// <returns></returns>
    public static int CUT_ROD_Recursion(int[] p, int n)
    {
        if(n == 0)
        {
            return 0;
        }
        int q = int.MinValue;
        for (int i = 1; i <= n; ++i )
        {
            q = Math.Max(q, p[i] + CUT_ROD_Recursion(p, n - i));
        }
        return q;
    }
}

