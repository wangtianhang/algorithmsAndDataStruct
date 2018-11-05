using System;
using System.Collections.Generic;

using System.Text;



class QuickSort<T>
{
    static Comparer<T> m_comparer;

    public QuickSort(Comparer<T> comparer)
    {
        m_comparer = comparer;
    }

    public static void Sort(T[] a)
    {
        RandomAlgorithm.Shuffle<T>(a);
        Sort(a, 0, a.Length - 1);
    }

    static void Sort(T[]a, int lo, int hi)
    {
        if(hi <= lo)
        {
            return;
        }
        int j = Partition(a, lo, hi);
        Sort(a, lo, j-1);
        Sort(a, j + 1, hi);
    }

    public static int Partition(T[] a, int lo, int hi)
    {
        int i = lo, j = hi + 1;
        T v = a[lo];
        while(true)
        {
            while(Less(a[++i], v))
            {
                if(i == hi)
                {
                    break;
                }
            }
            while(Less(v, a[--j]))
            {
                if(j == lo)
                {
                    break;
                }
            }
            if(i >= j)
            {
                break;
            }
            Exch(a, i, j);
        }
        Exch(a, lo, j);
        return j;
    }

    static bool Less(T a, T b)
    {
        int ret = m_comparer.Compare(a, b);
        if (ret > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    static void Exch(T[] a, int i, int j)
    {
        T t = a[i];
        a[i] = a[j];
        a[j] = t;
    }
}

