using System;
using System.Collections.Generic;
using System.Text;


class SortOther
{
    //The selection sort algorithm sorts an array by repeatedly finding the minimum element (considering ascending order) from unsorted part and putting it at the beginning. 
    static void SelectionSort(int[] arr)
    {
        int n = arr.Length;

        // One by one move boundary of unsorted subarray 
        for (int i = 0; i < n - 1; i++)
        {
            // Find the minimum element in unsorted array 
            int min_idx = i;
            for (int j = i + 1; j < n; j++)
                if (arr[j] < arr[min_idx])
                    min_idx = j;

            // Swap the found minimum element with the first 
            // element 
            int temp = arr[min_idx];
            arr[min_idx] = arr[i];
            arr[i] = temp;
        }
    }

    /// <summary>
    /// 冒泡法
    /// </summary>
    /// <param name="arr"></param>
    static void bubbleSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
            for (int j = 0; j < n - i - 1; j++)
                if (arr[j] > arr[j + 1])
                {
                    // swap temp and arr[i] 
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
    }

    /// <summary>
    /// 插入法 时间复杂度 O^2
    /// </summary>
    /// <param name="arr"></param>
    void InsertSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 1; i < n; ++i)
        {
            int key = arr[i];
            int j = i - 1;

            // Move elements of arr[0..i-1], 
            // that are greater than key,  
            // to one position ahead of 
            // their current position 
            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j = j - 1;
            }
            arr[j + 1] = key;
        }
    }

    #region MergeSort
    // Merges two subarrays of arr[]. 
    // First subarray is arr[l..m] 
    // Second subarray is arr[m+1..r] 
    void merge(int[] arr, int l, int m, int r) 
    { 
        // Find sizes of two subarrays to be merged 
        int n1 = m - l + 1; 
        int n2 = r - m; 
  
        /* Create temp arrays */
        int[] L = new int [n1]; 
        int[] R = new int [n2]; 
  
        /*Copy data to temp arrays*/
        for (int i = 0; i < n1; ++i)
            L[i] = arr[l + i];
        for (int j = 0; j < n2; ++j)
            R[j] = arr[m + 1 + j];


        {
            /* Merge the temp arrays */

            // Initial indexes of first and second subarrays 
            int i = 0, j = 0;

            // Initial index of merged subarry array 
            int k = l;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }

            /* Copy remaining elements of L[] if any */
            while (i < n1)
            {
                arr[k] = L[i];
                i++;
                k++;
            }

            /* Copy remaining elements of R[] if any */
            while (j < n2)
            {
                arr[k] = R[j];
                j++;
                k++;
            } 
        }

    } 

    // Main function that sorts arr[l..r] using 
    // merge() 
    // 合并排序 最好 最坏 平均 时间复杂度都是nLogn
    void MergeSort(int[] arr, int l, int r) 
    { 
        if (l < r) 
        { 
            // Find the middle point 
            int m = (l+r)/2; 
  
            // Sort first and second halves 
            MergeSort(arr, l, m); 
            MergeSort(arr , m+1, r); 
  
            // Merge the sorted halves 
            merge(arr, l, m, r); 
        } 
    }
    #endregion

    #region HeapSort
    /// <summary>
    /// 时间复杂度  O(Logn)
    /// Heap sort algorithm has limited uses because Quicksort and Mergesort are better in practice. 
    /// </summary>
    /// <param name="arr"></param>
    public void HeapSort(int[] arr)
    {
        int n = arr.Length;

        // Build heap (rearrange array) 
        for (int i = n / 2 - 1; i >= 0; i--)
            heapify(arr, n, i);

        // One by one extract an element from heap 
        for (int i = n - 1; i >= 0; i--)
        {
            // Move current root to end 
            int temp = arr[0];
            arr[0] = arr[i];
            arr[i] = temp;

            // call max heapify on the reduced heap 
            heapify(arr, i, 0);
        }
    }

    // To heapify a subtree rooted with node i which is 
    // an index in arr[]. n is size of heap 
    void heapify(int[] arr, int n, int i)
    {
        int largest = i; // Initialize largest as root 
        int l = 2 * i + 1; // left = 2*i + 1 
        int r = 2 * i + 2; // right = 2*i + 2 

        // If left child is larger than root 
        if (l < n && arr[l] > arr[largest])
            largest = l;

        // If right child is larger than largest so far 
        if (r < n && arr[r] > arr[largest])
            largest = r;

        // If largest is not root 
        if (largest != i)
        {
            int swap = arr[i];
            arr[i] = arr[largest];
            arr[largest] = swap;

            // Recursively heapify the affected sub-tree 
            heapify(arr, n, largest);
        }
    }

    #endregion

    #region QuickSort
    /* This function takes last element as pivot, 
    places the pivot element at its correct 
    position in sorted array, and places all 
    smaller (smaller than pivot) to left of 
    pivot and all greater elements to right 
    of pivot */
    static int partition(int[] arr, int low,
                                   int high)
    {
        int pivot = arr[high];

        // index of smaller element 
        int i = (low - 1);
        for (int j = low; j < high; j++)
        {
            // If current element is smaller  
            // than or equal to pivot 
            if (arr[j] <= pivot)
            {
                i++;

                // swap arr[i] and arr[j] 
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        // swap arr[i+1] and arr[high] (or pivot) 
        int temp1 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp1;

        return i + 1;
    }


    /* The main function that implements QuickSort() 
    arr[] --> Array to be sorted, 
    low --> Starting index, 
    high --> Ending index */
    // 不稳定排序（但是可以被调整为稳定算法by index） 最坏情况时间复杂度O^2
    /*Why Quick Sort is preferred over MergeSort for sorting Arrays
Quick Sort in its general form is an in-place sort (i.e. it doesn’t require any extra storage) whereas merge sort requires O(N) extra storage, N denoting the array size which may be quite expensive. Allocating and de-allocating the extra space used for merge sort increases the running time of the algorithm. Comparing average complexity we find that both type of sorts have O(NlogN) average complexity but the constants differ. For arrays, merge sort loses due to the use of extra O(N) storage space.

Most practical implementations of Quick Sort use randomized version. The randomized version has expected time complexity of O(nLogn). The worst case is possible in randomized version also, but worst case doesn’t occur for a particular pattern (like sorted array) and randomized Quick Sort works well in practice.

Quick Sort is also a cache friendly sorting algorithm as it has good locality of reference when used for arrays.

Quick Sort is also tail recursive, therefore tail call optimizations is done.
     */

    /*
    Why MergeSort is preferred over QuickSort for Linked Lists?
    In case of linked lists the case is different mainly due to difference in memory allocation of arrays and linked lists. Unlike arrays, linked list nodes may not be adjacent in memory. Unlike array, in linked list, we can insert items in the middle in O(1) extra space and O(1) time. Therefore merge operation of merge sort can be implemented without extra space for linked lists.
In arrays, we can do random access as elements are continuous in memory. Let us say we have an integer (4-byte) array A and let the address of A[0] be x then to access A[i], we can directly access the memory at (x + i*4). Unlike arrays, we can not do random access in linked list. Quick Sort requires a lot of this kind of access. In linked list to access i’th index, we have to travel each and every node from the head to i’th node as we don’t have continuous block of memory. Therefore, the overhead increases for quick sort. Merge sort accesses data sequentially and the need of random access is low.
     */
    static void quickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {

            /* pi is partitioning index, arr[pi] is  
            now at right place */
            int pi = partition(arr, low, high);

            // Recursively sort elements before 
            // partition and after partition 
            quickSort(arr, low, pi - 1);
            quickSort(arr, pi + 1, high);
        }
    } 
    #endregion

    #region RadixSort

    // A utility function to get maximum value in arr[] 
    static int getMax(int[] arr, int n) 
    { 
        int mx = arr[0]; 
        for (int i = 1; i < n; i++) 
            if (arr[i] > mx) 
                mx = arr[i]; 
        return mx; 
    } 
  
    // A function to do counting sort of arr[] according to 
    // the digit represented by exp. 
    static void countSort(int[] arr, int n, int exp) 
    { 
        int[] output = new int[n]; // output array 
        int i; 
        int[] count = new int[10]; 
        //Arrays.fill(count,0); 
  
        // Store count of occurrences in count[] 
        for (i = 0; i < n; i++) 
            count[ (arr[i]/exp)%10 ]++; 
  
        // Change count[i] so that count[i] now contains 
        // actual position of this digit in output[] 
        for (i = 1; i < 10; i++) 
            count[i] += count[i - 1]; 
  
        // Build the output array 
        for (i = n - 1; i >= 0; i--) 
        { 
            output[count[ (arr[i]/exp)%10 ] - 1] = arr[i]; 
            count[ (arr[i]/exp)%10 ]--; 
        } 
  
        // Copy the output array to arr[], so that arr[] now 
        // contains sorted numbers according to curent digit 
        for (i = 0; i < n; i++) 
            arr[i] = output[i]; 
    } 
  
    // The main function to that sorts arr[] of size n using 
    // Radix Sort 基数排序
    /*
     * Is Radix Sort preferable to Comparison based sorting algorithms like Quick-Sort?
If we have log2n bits for every digit, the running time of Radix appears to be better than Quick Sort for a wide range of input numbers. The constant factors hidden in asymptotic notation are higher for Radix Sort and Quick-Sort uses hardware caches more effectively. Also, Radix sort uses counting sort as a subroutine and counting sort takes extra space to sort numbers.
     */
    static void radixsort(int[] arr) 
    {
        int n = arr.Length;
        // Find the maximum number to know number of digits 
        int m = getMax(arr, n); 
  
        // Do counting sort for every digit. Note that instead 
        // of passing digit number, exp is passed. exp is 10^i 
        // where i is current digit number 
        for (int exp = 1; m/exp > 0; exp *= 10) 
            countSort(arr, n, exp); 
    } 

    #endregion

    /// <summary>
    /// 计数排序 适合输入数据集在一个小范围内的情况
    /// </summary>
    /// <param name="arr"></param>
    static void countsort(char[] arr)
    {
        int n = arr.Length;

        // The output character array that 
        // will have sorted arr 
        char[] output = new char[n];

        // Create a count array to store  
        // count of inidividul characters  
        // and initialize count array as 0 
        int[] count = new int[256];

        for (int i = 0; i < 256; ++i)
            count[i] = 0;

        // store count of each character 
        for (int i = 0; i < n; ++i)
            ++count[arr[i]];

        // Change count[i] so that count[i]  
        // now contains actual position of  
        // this character in output array 
        for (int i = 1; i <= 255; ++i)
            count[i] += count[i - 1];

        // Build the output character array 
        // To make it stable we are operating in reverse order. 
        for (int i = n - 1; i >= 0; i--)
        {
            output[count[arr[i]] - 1] = arr[i];
            --count[arr[i]];
        }

        // Copy the output array to arr, so 
        // that arr now contains sorted  
        // characters 
        for (int i = 0; i < n; ++i)
            arr[i] = output[i];
    } 

    // Function to sort arr[] of size n using bucket sort
    // 桶排序 可以适用于浮点数的情况
    void BucketSort(float[] arr)
    {
        int n = arr.Length;
         // 1) Create n empty buckets 
        //float[] b = new float[n]; 
        List<float>[] b = new List<float>[n];
        for (int i = 0; i < n; ++i )
        {
            b[i] = new List<float>();
        }
     
        // 2) Put array elements in different buckets 
        for (int i=0; i<n; i++) 
        { 
           int bi = (int)(n*arr[i]); // Index in bucket 
           b[bi].Add(arr[i]); 
        } 
  
        // 3) Sort individual buckets 
        for (int i=0; i<n; i++) 
           b[i].Sort(); 
  
        // 4) Concatenate all buckets into arr[] 
        int index = 0; 
        for (int i = 0; i < n; i++) 
            for (int j = 0; j < b[i].Count; j++) 
              arr[index++] = b[i][j];
 
     }


    /* function to sort arr using shellSort */
    // 希尔排序
    int ShellSort(int[] arr)
    {
        int n = arr.Length;

        // Start with a big gap,  
        // then reduce the gap 
        for (int gap = n / 2; gap > 0; gap /= 2)
        {
            // Do a gapped insertion sort for this gap size. 
            // The first gap elements a[0..gap-1] are already 
            // in gapped order keep adding one more element 
            // until the entire array is gap sorted 
            for (int i = gap; i < n; i += 1)
            {
                // add a[i] to the elements that have 
                // been gap sorted save a[i] in temp and 
                // make a hole at position i 
                int temp = arr[i];

                // shift earlier gap-sorted elements up until 
                // the correct location for a[i] is found 
                int j;
                for (j = i; j >= gap && arr[j - gap] > temp; j -= gap)
                    arr[j] = arr[j - gap];

                // put temp (the original a[i])  
                // in its correct location 
                arr[j] = temp;
            }
        }
        return 0;
    }

    #region CombSort

    // To find gap between elements 
    static int getNextGap(int gap)
    {
        // Shrink gap by Shrink factor 
        gap = (gap * 10) / 13;
        if (gap < 1)
            return 1;
        return gap;
    }

    // Function to sort arr[] using Comb Sort 
    // 冒泡排序的变种（貌似间隔比较 类似希尔排序）
    static void CombSort(int[] arr)
    {
        int n = arr.Length;

        // initialize gap 
        int gap = n;

        // Initialize swapped as true to  
        // make sure that loop runs 
        bool swapped = true;

        // Keep running while gap is more than  
        // 1 and last iteration caused a swap 
        while (gap != 1 || swapped == true)
        {
            // Find next gap 
            gap = getNextGap(gap);

            // Initialize swapped as false so that we can 
            // check if swap happened or not 
            swapped = false;

            // Compare all elements with current gap 
            for (int i = 0; i < n - gap; i++)
            {
                if (arr[i] > arr[i + gap])
                {
                    // Swap arr[i] and arr[i+gap] 
                    int temp = arr[i];
                    arr[i] = arr[i + gap];
                    arr[i + gap] = temp;

                    // Set swapped 
                    swapped = true;
                }
            }
        }
    } 

    #endregion

    /// <summary>
    /// 鸽巢排序 桶排序变种 额 文档上都说适用环境很少。。
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="n"></param>
    public static void PigeonholeSort(int[] arr,
                                   int n)
    {
        int min = arr[0];
        int max = arr[0];
        int range, i, j, index;

        for (int a = 0; a < n; a++)
        {
            if (arr[a] > max)
                max = arr[a];
            if (arr[a] < min)
                min = arr[a];
        }

        range = max - min + 1;
        int[] phole = new int[range];

        for (i = 0; i < n; i++)
            phole[i] = 0;

        for (i = 0; i < n; i++)
            phole[arr[i] - min]++;


        index = 0;

        for (j = 0; j < range; j++)
            while (phole[j]-- > 0)
                arr[index++] = j + min;

    }

    // Function sort the array using Cycle sort 
    // 圈排序(交换次数最少的排序，适合写内存非常costly的情况) 时间复杂度 O^2 
    public static void CycleSort(int[] arr, int n)
    {
        // count number of memory writes 
        int writes = 0;

        // traverse array elements and  
        // put it to on the right place 
        for (int cycle_start = 0; cycle_start <= n - 2; cycle_start++)
        {
            // initialize item as starting point 
            int item = arr[cycle_start];

            // Find position where we put the item.  
            // We basically count all smaller elements  
            // on right side of item. 
            int pos = cycle_start;
            for (int i = cycle_start + 1; i < n; i++)
                if (arr[i] < item)
                    pos++;

            // If item is already in correct position 
            if (pos == cycle_start)
                continue;

            // ignore all duplicate elements 
            while (item == arr[pos])
                pos += 1;

            // put the item to it's right position 
            if (pos != cycle_start)
            {
                int temp = item;
                item = arr[pos];
                arr[pos] = temp;
                writes++;
            }

            // Rotate rest of the cycle 
            while (pos != cycle_start)
            {
                pos = cycle_start;

                // Find position where we put the element 
                for (int i = cycle_start + 1; i < n; i++)
                    if (arr[i] < item)
                        pos += 1;

                // ignore all duplicate elements 
                while (item == arr[pos])
                    pos += 1;

                // put the item to it's right position 
                if (item != arr[pos])
                {
                    int temp = item;
                    item = arr[pos];
                    arr[pos] = temp;
                    writes++;
                }
            }
        }
    } 
}

