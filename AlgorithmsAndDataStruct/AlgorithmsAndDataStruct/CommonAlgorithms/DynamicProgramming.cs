using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

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

        {
            List<Vector2ixy> matrixList = new List<Vector2ixy>();
            matrixList.Add(new Vector2ixy(30, 35));
            matrixList.Add(new Vector2ixy(35, 15));
            matrixList.Add(new Vector2ixy(15, 5));
            matrixList.Add(new Vector2ixy(5, 10));
            matrixList.Add(new Vector2ixy(10, 20));
            matrixList.Add(new Vector2ixy(20, 25));
            int[][] s = null;
            int[][] m = null;
            MATRAIX_CHAIN_ORDER(matrixList, out s, out m);
            Debug.Log("s matrix");
            for (int i = 1; i <= matrixList.Count; ++i)
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


        {
            String str1 = "fjssharpsword";
            String str2 = "helloworld";

            //计算lcs递归矩阵  
            int[][] re = longestCommonSubsequence(str1, str2);
            //打印矩阵  
            for (int i = 0; i <= str1.Length; i++)
            {
                string outPut = "";
                for (int j = 0; j <= str2.Length; j++)
                {
                    outPut += re[i][j] + "   ";
                }
                UnityEngine.Debug.Log(outPut);
            }

            UnityEngine.Debug.Log("");
            UnityEngine.Debug.Log("");

            StringBuilder str = new StringBuilder();
            printLCS(re, str1, str2, str1.Length, str2.Length, ref str);
            UnityEngine.Debug.Log(str.ToString());
        }


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

    public static void MATRAIX_CHAIN_ORDER(List<Vector2ixy> matrix, out int[][] s, out int[][] m)
    {
        //int[] p = new int[matrix.Count + 2];
        List<int> tmpP = new List<int>();
        //tmpP.Add(0);
        for(int i = 0; i < matrix.Count; ++i)
        {
            if(i == 0)
            {
                tmpP.Add(matrix[i].m_x);
                tmpP.Add(matrix[i].m_y);
            }
            else
            {
                tmpP.Add(matrix[i].m_y);
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

    #region 最长公共子序列

    public static string LCS(String str1, String str2)
    {
        int[][] re = longestCommonSubsequence(str1, str2);
        StringBuilder str = new StringBuilder();
        printLCS(re, str1, str2, str1.Length, str2.Length, ref str);
        return str.ToString();
    }

    /// <summary>
    /// 最长公共子序列
    /// </summary>
    /// <returns></returns>
    // 假如返回两个字符串的最长公共子序列的长度  
    static int[][] longestCommonSubsequence(String str1, String str2)
    {
        int[][] matrix = new int[str1.Length + 1][];//建立二维矩阵  
        for (int i = 0; i < str1.Length + 1; ++i)
        {
            matrix[i] = new int[str2.Length + 1];
        }
        // 初始化边界条件  
        for (int i = 0; i <= str1.Length; i++)
        {
            matrix[i][0] = 0;//每行第一列置零  
        }
        for (int j = 0; j <= str2.Length; j++)
        {
            matrix[0][j] = 0;//每列第一行置零  
        }
        // 填充矩阵  
        for (int i = 1; i <= str1.Length; i++)
        {
            for (int j = 1; j <= str2.Length; j++)
            {
                if (str1[i - 1] == str2[j - 1])
                {
                    matrix[i][j] = matrix[i - 1][j - 1] + 1;
                }
                else
                {
                    matrix[i][j] = (matrix[i - 1][j] >= matrix[i][j - 1] ? matrix[i - 1][j]
                                    : matrix[i][j - 1]);
                }
            }
        }
        return matrix;
    }

    //根据矩阵输出LCS  
    static void printLCS(int[][] opt, String X, String Y, int i, int j, ref StringBuilder outStr)
    {
        if (i == 0 || j == 0)
        {
            return;
        }
        if (X[i - 1] == Y[j - 1])
        {
            printLCS(opt, X, Y, i - 1, j - 1, ref outStr);
            outStr.Append(X[i - 1]);
        }
        else if (opt[i - 1][j] >= opt[i][j])
        {
            printLCS(opt, X, Y, i - 1, j, ref outStr);
        }
        else
        {
            printLCS(opt, X, Y, i, j - 1, ref outStr);
        }
    }
    #endregion

    /// <summary>
    /// 最大子序列和
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static int maxSubSum4(int[] nums)
    {
        int maxsum, maxhere;
        maxsum = maxhere = nums[0];   //初始化最大和为a【0】  
        for (int i = 1; i < nums.Length; i++)
        {
            if (maxhere <= 0)
                maxhere = nums[i];  //如果前面位置最大连续子序列和小于等于0，则以当前位置i结尾的最大连续子序列和为a[i]  
            else
                maxhere += nums[i]; //如果前面位置最大连续子序列和大于0，则以当前位置i结尾的最大连续子序列和为它们两者之和  
            if (maxhere > maxsum)
            {
                maxsum = maxhere;  //更新最大连续子序列和  
            }
        }
        return maxsum;
    }

    /// <summary>
    /// 卡特兰数
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static long CatlanDP(int n)
    {
        // Table to store results of subproblems 
        long[] catalan = new long[n + 1]; 
  
        // Initialize first two values in table 
        catalan[0] = catalan[1] = 1; 
  
        // Fill entries in catalan[] using recursive formula 
        for (int i=2; i<=n; i++) 
        { 
            catalan[i] = 0; 
            for (int j=0; j<i; j++) 
                catalan[i] += catalan[j] * catalan[i-j-1]; 
        } 
  
        // Return last entry 
        return catalan[n]; 
    }
}

