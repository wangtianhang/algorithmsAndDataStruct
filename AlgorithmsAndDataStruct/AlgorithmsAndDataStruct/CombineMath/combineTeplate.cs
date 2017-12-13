using System;
using System.Collections.Generic;

using System.Text;


namespace leetCode
{
    class combineTeplate
    {
        void Test()
        {
            const int N = 4;
            const int M = 3;
            int []a = new int[N];
            for(int i=0;i<N;i++)
              a[i] = i+1;
            int[] b = new int[M];
            combine(a,N,M,b,M); 
        }
        /// 求从数组a[1..n]中任选m个元素的所有组合。
        /// a[1..n]表示候选集，m表示一个组合的元素个数。
        /// b[1..M]用来存储当前组合中的元素, 常量M表示一个组合中元素的个数。
        void combine( int [] a, int n, int m,  int [] b, int M )
        {
          for(int i=n; i>=m; i--)   // 注意这里的循环范围
          {
            b[m-1] = i - 1;
            if (m > 1)
            {
                combine(a, i - 1, m - 1, b, M);
            }
            else                     // m == 1, 输出一个组合
            {
              for(int j=M-1; j>=0; j--)
              {
                  Console.Write(a[b[j]]);
              }

              Console.WriteLine();
            }
 
          }
        }
    }


}
