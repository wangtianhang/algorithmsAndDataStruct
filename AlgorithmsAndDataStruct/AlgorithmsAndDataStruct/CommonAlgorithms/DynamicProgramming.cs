using System;
using System.Collections.Generic;
using System.Text;


public class DynamicProgramming
{

    public static void Test()
    {
//         int[] p = new int[11];
//         p[0] = 0;
//         p[1] = 1;
//         p[2] = 5;
//         p[3] = 8;
//         p[4] = 9;
//         p[5] = 10;
//         p[6] = 17;
//         p[7] = 19;
//         p[8] = 20;
//         p[9] = 24;
//         p[10] = 30;
        List<int> p = new List<int>();
        for (int i = 0; i < 100;  ++i)
        {
            p.Add(i);
        }
        //Debug.Log("CUT_ROD " + CUT_ROD_Recursion(p.ToArray(), p.Count - 1).ToString());
        Debug.Log("CUT_ROD " + CUT_ROD_DP(p.ToArray(), p.Count - 1).ToString());

        List<Vector2i> matrixList = new List<Vector2i>();
        matrixList.Add(new Vector2i(30, 35));
        matrixList.Add(new Vector2i(35, 15));
        matrixList.Add(new Vector2i(15, 5));
        matrixList.Add(new Vector2i(5, 10));
        matrixList.Add(new Vector2i(10, 20));
        matrixList.Add(new Vector2i(20, 25));
        int[][] s = null;
        int[][] m = null;
        MATRAIX_CHAIN_ORDER(matrixList, out s, out m);
        Debug.Log("s matrix");
        for (int i = 1; i <= matrixList.Count; ++i )
        {
            string oneLine = "";
            for (int j = 1; j <= matrixList.Count; ++j)
            {
                oneLine += s[i][j].ToString() + "\t";
            }
            Debug.Log(oneLine);
        }
        Debug.Log("m matrix");
        for (int i = 1; i <= matrixList.Count; ++i)
        {
            string oneLine = "";
            for (int j = 1; j <= matrixList.Count; ++j)
            {
                oneLine += m[i][j].ToString() + "\t";
            }
            Debug.Log(oneLine);
        }
        Debug.Log("matrix min " + m[1][6]);
        string str = "";
        PRINT_OPTIMAL_PARENS(s, 1, matrixList.Count, ref str);
        Debug.Log("分割 " + str);

    }

    /// <summary>
    /// 算法导论切钢条问题
    /// p为价格表
    /// n为长度 p.length = n;
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

    public static int CUT_ROD_DP(int[] p, int n)
    {
        int[] r = new int[p.Length];
        r[0] = 0;
        for (int j = 1; j <= n; ++j)
        {
            int q = int.MinValue;
            for (int i = 1; i <= j; ++i )
            {
                q = Math.Max(q, p[i] + r[j - i]);
            }
            r[j] = q;
        }
        return r[n];
    }

    public static void MATRAIX_CHAIN_ORDER(List<Vector2i> matrix, out int[][] s, out int[][] m)
    {
        //int[] p = new int[matrix.Count + 2];
        List<int> tmpP = new List<int>();
        //tmpP.Add(0);
        for(int i = 0; i < matrix.Count; ++i)
        {
            if(i == 0)
            {
                tmpP.Add(matrix[i].m_x);
                tmpP.Add(matrix[i].m_z);
            }
            else
            {
                tmpP.Add(matrix[i].m_z);
            }
        }
        int[] p = tmpP.ToArray();

        int n = matrix.Count;
        m = new int[n + 1][];
        for (int i = 0; i < n + 1; ++i )
        {
            m[i] = new int[n + 1];
        }
        s = new int[n + 1][];
        for (int i = 0; i < n + 1; ++i)
        {
            s[i] = new int[n + 1];
        }
        for (int l = 2; l <= n; ++l )
        {
            for (int i = 1; i <= n - l + 1; ++i )
            {
                int j = i + l - 1;
                m[i][j] = int.MaxValue;
                for (int k = i; k <= j - 1; ++k )
                {
                    int q = m[i][k] + m[k + 1][j] + p[i - 1] * p[k] *p[j];
                    if(q < m[i][j])
                    {
                        m[i][j] = q;
                        s[i][j] = k;
                    }
                }
            }
        }
    }

    public static void PRINT_OPTIMAL_PARENS(int[][] s, int i, int j, ref string str)
    {
        if(i == j)
        {
            str += ("A" + i);
        }
        else
        {
            str += ("(");
            PRINT_OPTIMAL_PARENS(s, i, s[i][j], ref str);
            PRINT_OPTIMAL_PARENS(s, s[i][j] + 1, j, ref str);
            str += (")");
        }
    }
}

