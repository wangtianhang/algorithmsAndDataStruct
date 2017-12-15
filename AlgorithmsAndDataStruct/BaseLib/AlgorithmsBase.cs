using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;


public class AlgorithmsBase
{
    public static void Test()
    {
        String str1 = "fjssharpsword";
        String str2 = "helloworld";

        //计算lcs递归矩阵  
        int[][] re = AlgorithmsBase.longestCommonSubsequence(str1, str2);
        //打印矩阵  
        for (int i = 0; i <= str1.Length; i++)
        {
            string outPut = "";
            for (int j = 0; j <= str2.Length; j++)
            {
                outPut += re[i][j] + "   ";
            }
            Debug.Log(outPut);
        }

        Debug.Log("");
        Debug.Log("");

        StringBuilder str = new StringBuilder();
        AlgorithmsBase.printLCS(re, str1, str2, str1.Length, str2.Length, ref str);
        Debug.Log(str.ToString());
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
}


