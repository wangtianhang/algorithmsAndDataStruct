using System;
using System.Collections.Generic;

using System.Text;



public class AlgorithmsMisc
{
    public static void Test()
    {
        Console.WriteLine(JosephRing(10, 2));

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
    /// 约瑟夫环问题
    /// </summary>
    /// <param name="n"></param>
    /// <param name="m"></param>
    /// <returns></returns>
    static int JosephRing(int n, int m)
    {
        int result = 0;
        for (int i = 2; i <= n; ++i )
        {
            result = (result + m) % i;
        }
        return result + 1;
    }

    /// <summary>
    /// 不用多余变量交换两个int型。。又是无聊的trick题目
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    public static void Swap(ref int a, ref int b)
    {
        a = a + b;
        b = a - b;
        a = a - b;
    }

    //埃拉托色尼筛选法
    //求一定范围内的质数
    public static void SieveOfEratosthenes(int n)
    {

        // Create a boolean array "prime[0..n]" and initialize 
        // all entries it as true. A value in prime[i] will 
        // finally be false if i is Not a prime, else true. 

        bool[] prime = new bool[n + 1];

        for (int i = 0; i < n; i++)
            prime[i] = true;

        for (int p = 2; p * p <= n; p++)
        {
            // If prime[p] is not changed, 
            // then it is a prime 
            if (prime[p] == true)
            {
                // Update all multiples of p 
                for (int i = p * 2; i <= n; i += p)
                    prime[i] = false;
            }
        }

        // Print all prime numbers 
        for (int i = 2; i <= n; i++)
        {
            if (prime[i] == true)
                Console.Write(i + " ");
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
}

