using System;
using System.Collections.Generic;
using System.Text;


class RandomAlgorithm
{
    /// <summary>
    /// 完美洗牌算法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    public static void Shuffle<T>(T[] a)
    {
        int n = a.Length;
        for (int i = 0; i < n; ++i)
        {
            int random = Random.Range(0, n - i);
            //int r = i + uniform(n - i);
            int r = i + random;
            T temp = a[i];
            a[i] = a[r];
            a[r] = temp;
        }
    }

    // A function to randomly select k items from stream[0..n-1]. 
    // 水塘采样
    // 水塘抽样是一系列的随机算法，其目的在于从包含n个项目的集合S中选取k个样本，其中n为一很大或未知的数量，尤其适用于不能把所有n个项目都存放到主内存的情况。
    static void ReservoirSampling(int[] stream, int n, int k) 
    { 
        int i;   // index for elements in stream[] 
          
        // reservoir[] is the output array. Initialize it with 
        // first k elements from stream[] 
        int[] reservoir = new int[k]; 
        for (i = 0; i < k; i++) 
            reservoir[i] = stream[i];

        System.Random r = new System.Random(); 
          
        // Iterate from the (k+1)th element to nth element 
        for (; i < n; i++) 
        { 
            // Pick a random index from 0 to i. 
            int j = r.Next(i + 1); 
              
            // If the randomly  picked index is smaller than k, 
            // then replace the element present at the index 
            // with new element from stream 
            if(j < k) 
                reservoir[j] = stream[i];             
        } 
          
        UnityEngine.Debug.Log("Following are k randomly selected items"); 
        //UnityEngine.Debug.Log(Arrays.toString(reservoir)); 
    } 
}

