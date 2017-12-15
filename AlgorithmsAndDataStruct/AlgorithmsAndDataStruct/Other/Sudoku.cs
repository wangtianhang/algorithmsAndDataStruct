//using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 数独
/// </summary>
public class Sudoku
{
    public static void Test()
    {
        Sudoku test = new Sudoku();
        List<int[][]> result = test.GetSudoku(100);
        for (int i = 0; i < result.Count; ++i )
        {
            Debug.Log("Sudoku result " + (i + 1));
            test.SodokuPrint(result[i]);
        }
    }

    public List<int[][]> GetSudoku(int wantToNum)
    {
        Dictionary<string, int[][]> noRepeatDic = new Dictionary<string, int[][]>();
        while (noRepeatDic.Count < wantToNum)
        {
            int[][] matrix = GenerateValidMatrix();
            if (matrix != null)
            {
                //             for (int i = 0; i < matrixList.Count; ++i )
                //             {
                //Debug.Log("Sudoku result " + (i + 1));
                //test.SodokuPrint(matrix);
                /*            }*/
                StringBuilder str = new StringBuilder();
                for (int i = 0; i < 9; ++i )
                {
                    for (int j = 0; j < 9; ++j )
                    {
                        str.Append(matrix[i][j] + "_");
                    }
                }
                string key = str.ToString();
                if (!noRepeatDic.ContainsKey(key))
                {
                    noRepeatDic.Add(key, matrix);
                }
            }
            else
            {
                Debug.Log("GenerateValidMatrix no result");
            }
        }

        List<int[][]> resultList = new List<int[][]>();
        foreach(var iter in noRepeatDic)
        {
            resultList.Add(iter.Value);
        }
        return resultList;
    }

    bool is_digital_match(int[][] sudoku, int i, int j)  
    {  
        int temp = sudoku[i][j];  
        int p, q;  
        int m, n;  
      
        for(p=0; p<9; p++)  
            if(p!=i && sudoku[p][j]==temp)  
                return false;  
        for(p=0; p<9; p++)  
            if(p!=j && sudoku[i][p]==temp)  
                return false;  
  
        p = i/3;  
        q = j/3;  
        for(m=p*3; m<p*3+3; m++)  
            for(n=q*3; n<q*3+3; n++)  
                if(m!=i && n!=j && sudoku[m][n]==temp)  
                    return false;  
      
        return true;  
    }  

    int[][] GenerateValidMatrix()
    {
        //List<int[][]> ret = new List<int[][]>();

        int[][] matrix = new int[9][];
        for (int i = 0; i < 9; ++i )
        {
            matrix[i] = new int[9];
        }

        // 添加随机性?
        for (int i = 0; i < 9; ++i )
        {
            int tmp = Random.Range(0, 81);
            matrix[tmp / 9][tmp % 9] = i + 1;
        }

        {
            int k = 0;
            int i = 0;
            int j = 0;
            //int sudokuNum = 0;
            while (true)
            {
                i = k / 9;
                j = k % 9;

                while (true)
                {
                    matrix[i][j]++;
                    if (matrix[i][j] == 10)
                    {
                        matrix[i][j] = 0;
                        --k;
                        if(k < 0)
                        {
                            k = 0;
                        }
                        break;
                    }
                    else if (is_digital_match(matrix, i, j))
                    {
                        ++k;
                        break;
                    }
                }

                if (k == 81)
                {
                    //Debug.Log("Proper sudoku matrix " + (++sudokuNum).ToString() );
                    //sudoku_print(sudoku);
                    //if (num >= SUDOKU_NUM)
                    //SodokuPrint(matrix);
                    //ret.Add(matrix);
                    return matrix;
//                     if (sudokuNum >= WantToGenNum)
//                     {
//                         return ret;
//                     }
                    
                    --k;
                }
            }
        }

        return null;
    }

    void SodokuPrint(int[][] matrix)
    {
        if (matrix != null)
        {
            foreach (var iter in matrix)
            {
                string oneline = "";
                foreach (var iter2 in iter)
                {
                    oneline += iter2 + "\t";
                }
                Debug.Log(oneline);
            }
        }
    }
}
