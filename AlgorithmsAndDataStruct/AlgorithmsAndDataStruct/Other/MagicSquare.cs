using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 幻方
/// </summary>
public class MagicSquare
{
    public static void Test()
    {
        MagicSquare test = new MagicSquare();
        int[][] square = test.Gen(9);
        foreach(var iter in square)
        {
            string str = "";
            foreach(var iter2 in iter)
            {
                str += iter2 + "\t";
            }
            Debug.Log(str);
        }
    }

    int[][] m_array = null;  

    public int[][] Gen(int n)
    {
        if(n % 2 != 1)
        {
            Debug.LogError("n必须为奇数");
            return null;
        }
        m_array = new int[n + 1][];
        for (int i = 0; i < n + 1; ++i )
        {
            m_array[i] = new int[n + 1];
        }

        lao_bo_er(n, 1, 1, 1);

        int[][] ret = new int[n][];
        for (int i = 0; i < n; ++i)
        {
            ret[i] = new int[n];
        }

        for (int i = 1; i <= n; ++i )
        {
            for (int j = 1; j <= n; ++j )
            {
                ret[i - 1][j - 1] = m_array[i][j];
            }
        }
        return ret;
    }

    //劳伯法  
    void lao_bo_er(int degree, int x, int y, int num)      
    {
        int i;
        int j;
        int k;

        i = y;
        j = degree / 2 + x;
        for (k = num; k <= num + degree * degree - 1; k++)
        {
            m_array[i][j] = k;
            if ((k - num + 1) % degree == 0)
            {            //如果这个数所要放的格已经有数填入  
                i = (i - y + 1) % degree + y;
            }
            else
            {                                 //每一个数放在前一个数的右上一格  
                i = (i - y - 1 + degree) % degree + y;
                j = (j - x + 1) % degree + x;
            }
        }
        //return 0;
    }  
}

