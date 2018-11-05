using System;
using System.Collections.Generic;
using System.Text;


class SearchOther
{
    public static int LinearSearch(int[] arr, int n, int x)
    {
        for (int i = 0; i < n; i++) 
        { 
            // Return the index of the element if the element 
            // is found 
            if (arr[i] == x) 
                return i; 
        } 
   
        // return -1 if the element is not found 
        return -1; 
    }

    /// <summary>
    /// Jump Search
    /// 适用于回溯成本很高的数据集
    /// Binary Search is better than Jump Search, but Jump search has an advantage that we traverse back only once (Binary Search may require up to O(Log n) jumps, consider a situation where the element to be search is the smallest element or smaller than the smallest). So in a systems where jumping back is costly, we use Jump Search.
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static int jumpSearch(int[] arr, int x)
    {
        int n = arr.Length;

        // Finding block size to be jumped 
        int step = (int)Math.Floor(Math.Sqrt(n));

        // Finding the block where element is 
        // present (if it is present) 
        int prev = 0;
        while (arr[Math.Min(step, n) - 1] < x)
        {
            prev = step;
            step += (int)Math.Floor(Math.Sqrt(n));
            if (prev >= n)
                return -1;
        }

        // Doing a linear search for x in block 
        // beginning with prev. 
        while (arr[prev] < x)
        {
            prev++;

            // If we reached next block or end of 
            // array, element is not present. 
            if (prev == Math.Min(step, n))
                return -1;
        }

        // If element is found 
        if (arr[prev] == x)
            return prev;

        return -1;
    }

    /// <summary>
    /// 适用于均匀分布的数据集，这种数据集上速度超过二分法
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    static int interpolationSearch(int[] arr, int x)
    {
        // Find indexes of 
        // two corners 
        int lo = 0, hi = (arr.Length - 1);

        // Since array is sorted,  
        // an element present in 
        // array must be in range 
        // defined by corner 
        while (lo <= hi &&
                x >= arr[lo] &&
                x <= arr[hi])
        {
            // Probing the position  
            // with keeping uniform  
            // distribution in mind. 
            int pos = lo + (((hi - lo) /
                             (arr[hi] - arr[lo])) *
                                   (x - arr[lo]));

            // Condition of  
            // target found 
            if (arr[pos] == x)
                return pos;

            // If x is larger, x 
            // is in upper part 
            if (arr[pos] < x)
                lo = pos + 1;

            // If x is smaller, x  
            // is in the lower part 
            else
                hi = pos - 1;
        }
        return -1;
    }

    // 适用于超级大，没有头的数据集
    //Exponential Binary Search is particularly useful for unbounded searches, where size of array is infinite.
    static int exponentialSearch(int[] arr,
                         int n, int x)
    {

        // If x is present at  
        // first location itself 
        if (arr[0] == x)
            return 0;

        // Find range for binary search  
        // by repeated doubling 
        int i = 1;
        while (i < n && arr[i] <= x)
            i = i * 2;

        // Call binary search for 
        // the found range. 
        return binarySearch(arr, i / 2,
                           Math.Min(i, n), x);
    }

    static int binarySearch(int[] arr, int l,
                        int r, int x)
    {
        if (r >= l)
        {
            int mid = l + (r - l) / 2;

            // If the element is present 
            // at the middle itself 
            if (arr[mid] == x)
                return mid;

            // If element is smaller than 
            // mid, then it can only be  
            // present n left subarray 
            if (arr[mid] > x)
                return binarySearch(arr, l, mid - 1, x);

            // Else the element can only  
            // be present in right subarray 
            return binarySearch(arr, mid + 1, r, x);
        }

        // We reach here when element 
        // is not present in array 
        return -1;
    }

    /// <summary>
    /// 找到一组数中第k小元素 最坏时间复杂度O^2 期望时间复杂度O(n)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static T Select<T>(T[] a, int k)
    {
        RandomAlgorithm.Shuffle<T>(a);
        int lo = 0, hi = a.Length - 1;
        while (hi > lo)
        {
            int j = QuickSort<T>.Partition(a, lo, hi);
            if (j == k)
            {
                return a[k];
            }
            else if (j > k)
            {
                hi = j - 1;
            }
            else if (j < k)
            {
                lo = j + 1;
            }
        }

        return a[k];
    }
}

